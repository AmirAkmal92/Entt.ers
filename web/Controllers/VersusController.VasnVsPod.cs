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
        public ActionResult VasnVsPod()
        {
            var reportViewer = ReportEngine.Create();

            ViewBag.ReportViewer = reportViewer;
            ViewBag.Branches = GetUserViewBranches().Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            ViewBag.ReportDays = ApplicationHelper.GetReportDays().Select(w => new SelectListItem { Text = w.Value, Value = w.Key.ToString() });
            var model = new PrefixReportViewModel { ReportDate = DateTime.Today, ReportDay = 7.ToString() };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VasnVsPod(PrefixReportViewModel model)
        {
            var reportViewer = ReportEngine.Create();

            if (ModelState.IsValid)
            {
                var day = int.Parse(model.ReportDay);
                DataSet dataset;
                dataset = model.SelectedBranch == "All" ?
                    m_enttContext.GetVasnVsPodReportDataSet(model.ReportDate, day) :
                    m_enttContext.GetVasnVsPodBranchReportDataSet(model.ReportDate, day, model.SelectedBranch);
                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Versus\VasnVsPod.rdlc";

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

        public ActionResult VasnVsPodDetails(string branchCode, double date, string day = "7")
        {
            var reportDate = DateTime.FromOADate(date);
            ViewBag.Branches = m_enttContext.GetBranchInfo(branchCode).Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            ViewBag.ReportDays = ApplicationHelper.GetReportDays().Select(w => new SelectListItem { Text = w.Value, Value = w.Key.ToString() });
            var reportViewer = ReportEngine.Create();

            var dataset = m_enttContext.GetVasnVsPodDetailsReportDataSet(reportDate.Date, int.Parse(day), branchCode);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Versus\VasnVsPodDetails.rdlc";
            var parameters = new List<ReportParameter>
                {
                    new ReportParameter("reportDate", reportDate.ToShortDateString()),
                    new ReportParameter("day", day),
                    new ReportParameter("branchCode", branchCode)
                };
            reportViewer.LocalReport.EnableHyperlinks = true;
            reportViewer.LocalReport.SetParameters(parameters);
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dataset.Tables[0]));
            ViewBag.TotalRows = dataset.Tables[0].Rows.Count;
            ViewBag.ReportViewer = reportViewer;
            var model = new PrefixReportViewModel { ReportDate = reportDate, ReportDay = day, SelectedBranch = branchCode };
            return View(model);
        }
    }
}