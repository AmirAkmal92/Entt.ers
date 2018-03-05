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
        public ActionResult SipVsVasnVsPod()
        {
            var reportViewer = ReportEngine.Create();
            ViewBag.Branches = GetUserViewBranches().Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            ViewBag.ReportViewer = reportViewer;
            var model = new StandardReportViewModel { ReportDate = DateTime.Today };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SipVsVasnVsPod(StandardReportViewModel model)
        {
            var reportViewer = ReportEngine.Create();

            if (ModelState.IsValid)
            {
                DataSet dataset;
                dataset = model.SelectedBranch == "All" ?
                    m_enttContext.GetSipVsVasnVsPodReportDataSet(model.ReportDate) :
                    m_enttContext.GetSipVsVasnVsPodBranchReportDataSet(model.ReportDate, model.SelectedBranch);
                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Versus\SipVsVasnVsPod.rdlc";

                var parameters = new List<ReportParameter>
                {
                    new ReportParameter("reportDate", model.ReportDate.ToShortDateString()),
                };
                reportViewer.LocalReport.EnableHyperlinks = true;
                reportViewer.LocalReport.SetParameters(parameters);
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dataset.Tables[0]));
            }

            ViewBag.ReportViewer = reportViewer;
            ViewBag.Branches = GetUserViewBranches().Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            return View(model);
        }

        public ActionResult SipVsVasnVsPodDetails(string branchCode, double date)
        {
            var reportDate = DateTime.FromOADate(date);
            ViewBag.Branches = m_enttContext.GetBranchInfo(branchCode).Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            ViewBag.ReportDays = ApplicationHelper.GetReportDays().Select(w => new SelectListItem { Text = w.Value, Value = w.Key.ToString() });
            var reportViewer = ReportEngine.Create();

            var dataset = m_enttContext.GetSipVsVasnVsPodDetailsReportDataSet(reportDate.Date, branchCode);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Versus\SipVsVasnVsPodDetails.rdlc";
            var parameters = new List<ReportParameter>
                {
                    new ReportParameter("reportDate", reportDate.ToShortDateString()),
                    new ReportParameter("branchCode", branchCode)
                };
            reportViewer.LocalReport.EnableHyperlinks = true;
            reportViewer.LocalReport.SetParameters(parameters);
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dataset.Tables[0]));
            ViewBag.TotalRows = dataset.Tables[0].Rows.Count;
            ViewBag.ReportViewer = reportViewer;
            var model = new StandardReportViewModel { ReportDate = reportDate, SelectedBranch = branchCode };
            return View(model);
        }
    }

}