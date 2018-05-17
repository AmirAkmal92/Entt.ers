using Entt.Ers.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;

namespace Entt.Ers.Controllers
{
    public partial class VersusController
    {
        public ActionResult ItemOverDeclared()
        {
            var reportViewer = ReportEngine.Create();
            ViewBag.Branches = GetUserViewBranches().Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            ViewBag.ReportViewer = reportViewer;
            ViewBag.ReportDays = ApplicationHelper.GetReportDays().Select(w => new SelectListItem { Text = w.Value, Value = w.Key.ToString() });
            var model = new StandardReportViewModel { ReportDate = DateTime.Today };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ItemOverDeclared(StandardReportViewModel model)
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
                DataSet dataset;
                dataset = model.SelectedBranch == "All" ?
                        m_enttContext.GetIpcItemOverDeclaredDataSet(model.ReportDate) :
                        m_enttContext.GetIpcItemOverDeclaredBranchDataSet(model.ReportDate, model.SelectedBranch);
                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Versus\IpcItemOverDeclared.rdlc";

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

        public ActionResult ItemOverDeclaredDetails(string branchCode, double date)
        {
            var reportDate = DateTime.FromOADate(date);
            ViewBag.Branches = m_enttContext.GetBranchInfo(branchCode).Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            var reportViewer = ReportEngine.Create();

            var dataset = m_enttContext.GetIpcItemOverDeclaredBranchDetailsDataSet(reportDate.Date, branchCode);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Versus\IpcItemOverDeclaredDetails.rdlc";
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

        public ActionResult ItemUnderDeclared()
        {
            var reportViewer = ReportEngine.Create();
            ViewBag.Branches = GetUserViewBranches().Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            ViewBag.ReportViewer = reportViewer;
            ViewBag.ReportDays = ApplicationHelper.GetReportDays().Select(w => new SelectListItem { Text = w.Value, Value = w.Key.ToString() });
            var model = new StandardReportViewModel { ReportDate = DateTime.Today };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ItemUnderDeclared(StandardReportViewModel model)
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
                DataSet dataset;
                dataset = model.SelectedBranch == "All" ?
                        m_enttContext.GetIpcItemUnderDeclaredDataSet(model.ReportDate) :
                        m_enttContext.GetIpcItemUnderDeclaredBranchDataSet(model.ReportDate, model.SelectedBranch);
                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Versus\IpcItemUnderDeclared.rdlc";

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


        public ActionResult ItemUnderDeclaredDetails(string branchCode, double date)
        {
            var reportDate = DateTime.FromOADate(date);
            ViewBag.Branches = m_enttContext.GetBranchInfo(branchCode).Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            var reportViewer = ReportEngine.Create();

            var dataset = m_enttContext.GetIpcItemUnderDeclaredBranchDetailsDataSet(reportDate.Date, branchCode);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Versus\IpcItemUnderDeclaredDetails.rdlc";
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

        public ActionResult IpcNoAcceptance()
        {
            var reportViewer = ReportEngine.Create();
            ViewBag.Branches = GetUserViewBranches().Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            ViewBag.ReportViewer = reportViewer;
            ViewBag.ReportDays = ApplicationHelper.GetReportDays().Select(w => new SelectListItem { Text = w.Value, Value = w.Key.ToString() });
            var model = new StandardReportViewModel { ReportDate = DateTime.Today };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IpcNoAcceptance(StandardReportViewModel model)
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
                DataSet dataset;
                dataset = model.SelectedBranch == "All" ?
                        m_enttContext.GetIpcNoAcceptanceDataSet(model.ReportDate) :
                        m_enttContext.GetIpcNoAcceptanceBranchDataSet(model.ReportDate, model.SelectedBranch);
                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Versus\IpcNoAcceptance.rdlc";

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

        public ActionResult IpcNoAcceptanceDetails(string branchCode, double date)
        {
            var reportDate = DateTime.FromOADate(date);
            ViewBag.Branches = m_enttContext.GetBranchInfo(branchCode).Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            var reportViewer = ReportEngine.Create();

            var dataset = m_enttContext.GetIpcNoAcceptanceBranchDetailsDataSet(reportDate.Date, branchCode);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Versus\IpcNoAcceptanceDetails.rdlc";
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