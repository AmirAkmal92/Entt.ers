using Entt.Ers.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Entt.Ers.Controllers
{
    public partial class VersusController : ApplicationBaseController
    {
        public ActionResult DeliveryExceptionReport()
        {
            var reportViewer = new ReportViewer()
            {
                KeepSessionAlive = false,
                ProcessingMode = ProcessingMode.Local,
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                Height = Unit.Percentage(100)
            };

            ViewBag.ReportViewer = reportViewer;
            ViewBag.ReportDays = ApplicationHelper.GetReportDays().Select(w => new SelectListItem { Text = w.Value, Value = w.Key.ToString() });
            ViewBag.Branches = GetUserViewBranches().Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            var model = new PrefixReportViewModel { ReportDate = DateTime.Today, ReportDay = 7.ToString() };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeliveryExceptionReport(PrefixReportViewModel model)
        {
            var reportViewer = new ReportViewer()
            {
                KeepSessionAlive = false,
                ProcessingMode = ProcessingMode.Local,
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                Height = Unit.Percentage(100)
            };

            if (ModelState.IsValid)
            {
                var day = int.Parse(model.ReportDay);
                DataSet dataset;
                dataset = model.SelectedBranch == "All" ?
                        m_enttContext.GetDeliveryExceptionReportDataSet(model.ReportDate, day) :
                        m_enttContext.GetDeliveryExceptionBranchReportDataSet(model.ReportDate, day, model.SelectedBranch);
                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Versus\DeliveryExceptionVsPod.rdlc";

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
            ViewBag.ReportDays = ApplicationHelper.GetReportDays().Select(w => new SelectListItem { Text = w.Value, Value = w.Key.ToString() });
            ViewBag.Branches = GetUserViewBranches().Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            return View(model);
        }

        public ActionResult DeliveryExceptionDetailsReport(string branchCode, double date, string day = "7")
        {
            var reportDate = DateTime.FromOADate(date);
            ViewBag.Branches = m_enttContext.GetBranchInfo(branchCode).Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            ViewBag.ReportDays = ApplicationHelper.GetReportDays().Select(w => new SelectListItem { Text = w.Value, Value = w.Key.ToString() });
            var reportViewer = ReportEngine.Create();

            var dataset = m_enttContext.GetDeliveryExceptionBranchDetailsReportDataSet(reportDate.Date, int.Parse(day), branchCode);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Versus\DeliveryExceptionVsPodDetails.rdlc";
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

        public ActionResult ExpectedArrivalReport()
        {
            var reportViewer = ReportEngine.Create();
            ViewBag.ReportViewer = reportViewer;
            ViewBag.Branches = GetUserViewBranches().Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            var model = new StandardReportViewModel { ReportDate = DateTime.Today };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExpectedArrivalReport(StandardReportViewModel model)
        {
            var reportViewer = ReportEngine.Create();
            if (ModelState.IsValid)
            {
                DataSet dataset;
                dataset = model.SelectedBranch == "All" ?
                            m_enttContext.ExpectedArrivalReportDataSet(model.ReportDate) :
                            m_enttContext.ExpectedArrivalBranchReportDataSet(model.ReportDate, model.SelectedBranch);
                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Versus\ExpectedArrivalDestinationOffice.rdlc";

                var parameters = new List<ReportParameter>
                {
                    new ReportParameter("reportDate", model.ReportDate.ToShortDateString())
                };
                reportViewer.LocalReport.EnableHyperlinks = true;
                reportViewer.LocalReport.SetParameters(parameters);
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dataset.Tables[0]));
            }

            ViewBag.ReportViewer = reportViewer;
            ViewBag.Branches = GetUserViewBranches().Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            return View(model);
        }

        public ActionResult ExpectedArrivalDetailsReport(string branchCode, double date)
        {
            var reportDate = DateTime.FromOADate(date);
            ViewBag.Branches = m_enttContext.GetBranchInfo(branchCode).Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            var reportViewer = ReportEngine.Create();

            var dataset = m_enttContext.ExpectedArrivalBranchReportDetailsDataSet(reportDate.Date, branchCode);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Versus\ExpectedArrivalDestinationOfficeDetails.rdlc";
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


        public ActionResult HandoverPodReport()
        {
            var reportViewer = ReportEngine.Create();
            ViewBag.ReportViewer = reportViewer;
            ViewBag.ReportDays = ApplicationHelper.GetReportDays().Select(w => new SelectListItem { Text = w.Value, Value = w.Key.ToString() });
            ViewBag.Branches = GetUserViewBranches().Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            var model = new PrefixReportViewModel { ReportDate = DateTime.Today, ReportDay = 7.ToString() };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandoverPodReport(PrefixReportViewModel model)
        {
            var reportViewer = ReportEngine.Create();
            if (ModelState.IsValid)
            {
                var prefix = ApplicationHelper.GetPrefix(int.Parse(model.ReportDay));
                DataSet dataset;
                dataset = model.SelectedBranch == "All" ?
                            m_enttContext.HandoverPodReportDataSet(model.ReportDate, int.Parse(model.ReportDay)) :
                            m_enttContext.HandoverPodBranchReportDataSet(model.ReportDate, int.Parse(model.ReportDay), model.SelectedBranch);
                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Versus\HandoverVsPod.rdlc";

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
            ViewBag.ReportDays = ApplicationHelper.GetReportDays().Select(w => new SelectListItem { Text = w.Value, Value = w.Key.ToString() });
            ViewBag.Branches = GetUserViewBranches().Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            return View(model);
        }

        public ActionResult HandoverPodDetailsReport(string branchCode, double date, string day = "7")
        {
            var reportDate = DateTime.FromOADate(date);
            ViewBag.Branches = m_enttContext.GetBranchInfo(branchCode).Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            ViewBag.ReportDays = ApplicationHelper.GetReportDays().Select(w => new SelectListItem { Text = w.Value, Value = w.Key.ToString() });
            var reportViewer = ReportEngine.Create();

            var dataset = m_enttContext.HandoverPodDetailsReportDataSet(reportDate.Date, int.Parse(day), branchCode);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Versus\HandoverVsPodDetails.rdlc";
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