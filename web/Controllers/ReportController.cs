using Entt.Ers.Models;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Entt.Ers.Controllers
{
    public class ReportController : ApplicationBaseController
    {
        public ActionResult Acceptance()
        {
            var reportViewer = ReportEngine.Create();
            ViewBag.ReportViewer = reportViewer;
            ViewBag.Branches = GetUserViewBranches().Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            var model = new StandardReportViewModel { ReportDate = DateTime.Today };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Acceptance(StandardReportViewModel model)
        {
            var reportViewer = ReportEngine.Create();
            if (ModelState.IsValid)
            {
                DataSet dataset;
                dataset = m_enttContext.AcceptanceBranchReportDataSet(model.ReportDate, model.SelectedBranch);
                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Entt\Acceptance.rdlc";

                var parameters = new List<ReportParameter>
                {
                    new ReportParameter("reportDate", model.ReportDate.ToShortDateString()),
                    new ReportParameter("branchCode", model.SelectedBranch)
                };
                reportViewer.LocalReport.EnableHyperlinks = true;
                reportViewer.LocalReport.SetParameters(parameters);
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dataset.Tables[0]));
            }

            ViewBag.ReportViewer = reportViewer;
            ViewBag.Branches = GetUserViewBranches().Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            return View(model);
        }

        public ActionResult AcceptanceDetails(string branchCode, double date)
        {
            var user = m_dbContext.Users.Single(u => u.UserName == User.Identity.Name);
            if (null != user && !string.IsNullOrEmpty(user.BranchCode) && user.BranchCode != branchCode)
            {
                return new HttpStatusCodeResult(403, "You are not allow to view the data");
            }

            var reportDate = DateTime.FromOADate(date);
            ViewBag.Branches = m_enttContext.GetBranchInfo(branchCode).Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            var reportViewer = ReportEngine.Create();

            var dataset = m_enttContext.AcceptanceBranchDetailsReportDataSet(reportDate.Date, branchCode);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Entt\AcceptanceDetails.rdlc";
            var parameters = new List<ReportParameter>
                {
                    new ReportParameter("reportDate", reportDate.ToShortDateString()),
                    new ReportParameter("branchCode", branchCode)
                };
            reportViewer.LocalReport.SetParameters(parameters);
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dataset.Tables[0]));
            ViewBag.ReportViewer = reportViewer;
            var model = new StandardReportViewModel { ReportDate = reportDate, SelectedBranch = branchCode };
            return View(model);
        }

        public ActionResult Viewer()
        {
            var reportViewer = new ReportViewer()
            {
                ProcessingMode = ProcessingMode.Local,
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                Height = Unit.Percentage(100)
            };

            var dataset = new DataSet();
            using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["NonCoreConnectionString"].ConnectionString))
            {
                string queryString = "SELECT * FROM dbo.uv_GetAssetByItemReportRegistered";
                var sqlDataAapter = new SqlDataAdapter(queryString, sqlConnection);
                
                sqlDataAapter.Fill(dataset);
            }
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Report1.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dataset.Tables[0]));
            //reportViewer.LocalReport.SetParameters(GetParametersLocal());

            ViewBag.ReportViewer = reportViewer;
            return View();
        }
    }
}