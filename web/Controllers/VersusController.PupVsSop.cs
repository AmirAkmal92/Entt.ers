using Entt.Ers.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Entt.Ers.Controllers
{
    public partial class VersusController
    {
        public ActionResult PupVsSop()
        {
            var reportViewer = ReportEngine.Create();
            ViewBag.Branches = GetUserViewBranches().Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            ViewBag.ReportViewer = reportViewer;
            ViewBag.ReportDays = ApplicationHelper.GetReportDays().Select(w => new SelectListItem { Text = w.Value, Value = w.Key.ToString() });
            var model = new PrefixReportViewModel { ReportDate = DateTime.Today, ReportDay = 7.ToString() };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PupVsSop(PrefixReportViewModel model)
        {
            var reportViewer = ReportEngine.Create();

            if (ModelState.IsValid)
            {
                var day = int.Parse(model.ReportDay);
                DataSet dataset;
                dataset = model.SelectedBranch == "All" ?
                    m_enttContext.GetPupVsSopReportDataSet(model.ReportDate, day) :
                    m_enttContext.GetPupVsSopBranchReportDataSet(model.ReportDate, day, model.SelectedBranch);
                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Versus\PupVsSop.rdlc";

                var parameters = new List<ReportParameter>
                {
                    new ReportParameter("reportDate", model.ReportDate.ToShortDateString()),
                    new ReportParameter("day", model.ReportDay)
                };
                reportViewer.LocalReport.EnableHyperlinks = true;
                reportViewer.LocalReport.SetParameters(parameters);
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dataset.Tables[0]));
            }

            ViewBag.ReportViewer = reportViewer;
            ViewBag.Branches = GetUserViewBranches().Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            ViewBag.ReportDays = ApplicationHelper.GetReportDays().Select(w => new SelectListItem { Text = w.Value, Value = w.Key.ToString() });
            return View(model);
        }

        public ActionResult PupVsSopDetails(string branchCode, double date, string day = "7")
        {
            var user = m_dbContext.Users.Single(u => u.UserName == User.Identity.Name);
            if (null != user && !string.IsNullOrEmpty(user.BranchCode) && user.BranchCode != branchCode)
            {
                if (!User.IsInRole("VersusHQ"))
                    return new HttpStatusCodeResult(403, "You are not allow to view the data");
            }

            var reportDate = DateTime.FromOADate(date);
            ViewBag.Branches = m_enttContext.GetBranchInfo(branchCode).Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            ViewBag.ReportDays = ApplicationHelper.GetReportDays().Select(w => new SelectListItem { Text = w.Value, Value = w.Key.ToString() });
            var reportViewer = ReportEngine.Create();

            var dataset = m_enttContext.GetPupVsSopDetailsReportDataSet(reportDate.Date, int.Parse(day), branchCode);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Versus\PupVsSopDetails.rdlc";
            var parameters = new List<ReportParameter>
                {
                    new ReportParameter("reportDate", reportDate.ToShortDateString()),
                    new ReportParameter("day", day),
                    new ReportParameter("branchCode", branchCode)
                };
            reportViewer.LocalReport.SetParameters(parameters);
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dataset.Tables[0]));
            ViewBag.TotalRows = dataset.Tables[0].Rows.Count;
            ViewBag.ReportViewer = reportViewer;
            var model = new PrefixReportViewModel { ReportDate = reportDate, ReportDay = day, SelectedBranch = branchCode };
            return View(model);
        }
    }
}