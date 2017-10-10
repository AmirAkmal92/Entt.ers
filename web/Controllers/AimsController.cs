using Entt.Ers.Models;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Entt.Ers.Controllers
{
    [Authorize]
    public class AimsController : ApplicationBaseController
    {
        private AimsReportDataContext m_context = new AimsReportDataContext();

        public ActionResult DeviceAtBranchSummary()
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
            ViewBag.Branches = m_context.GetBranches(false).Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            var model = new DeviceAtBranchSummaryViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeviceAtBranchSummary(DeviceAtBranchSummaryViewModel model)
        {
            var reportViewer = new ReportViewer()
            {
                KeepSessionAlive = false,
                ProcessingMode = ProcessingMode.Local,
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                Height = Unit.Percentage(100)
            };

            if (ModelState.IsValid && model.SearchBranches != null)
            {
                var datasetDeviceAtPpl = m_context.GetBranchDeviceSummaryAtPplDataSet(model.SearchBranches);
                var datasetDeviceOutRepair = m_context.GetBranchDeviceSummaryOutRepairDataSet(model.SearchBranches);
                var datasetDevicePendingDelivery = m_context.GetBranchDeviceSummaryPendingDeliveryDataSet(model.SearchBranches);
                var datasetDeviceRelocated = m_context.GetBranchDeviceSummaryItemRelocatedDataSet(model.SearchBranches);

                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Aims\AimsBranchDeviceSummaryReport.rdlc";
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", datasetDeviceAtPpl.Tables[0]));
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", datasetDeviceOutRepair.Tables[0]));
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", datasetDevicePendingDelivery.Tables[0]));
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", datasetDeviceRelocated.Tables[0]));
            }
                        
            ViewBag.ReportViewer = reportViewer;
            ViewBag.Branches = m_context.GetBranches(false).Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            return View(model);
        }

        public ActionResult DeviceAtBranchDetails()
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
            ViewBag.Branches = m_context.GetBranches(false).Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            var model = new DeviceAtBranchSummaryViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeviceAtBranchDetails(DeviceAtBranchSummaryViewModel model)
        {
            var reportViewer = new ReportViewer()
            {
                KeepSessionAlive = false,
                ProcessingMode = ProcessingMode.Local,
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                Height = Unit.Percentage(100)
            };

            if (ModelState.IsValid && model.SearchBranches != null)
            {
                var datasetDeviceAtPpl = m_context.GetBranchDeviceDetailsAtPplDataSet(model.SearchBranches);
                var datasetDeviceOutRepair = m_context.GetBranchDeviceDetailsOutRepairDataSet(model.SearchBranches);
                var datasetDevicePendingDelivery = m_context.GetBranchDeviceSummaryPendingDeliveryDataSet(model.SearchBranches);
                var datasetDeviceRelocated = m_context.GetBranchDeviceDetailsItemRelocatedDataSet(model.SearchBranches);

                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Aims\AimsBranchDeviceDetailsReport.rdlc";
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", datasetDeviceAtPpl.Tables[0]));
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", datasetDeviceOutRepair.Tables[0]));
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", datasetDevicePendingDelivery.Tables[0]));
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", datasetDeviceRelocated.Tables[0]));
            }

            ViewBag.ReportViewer = reportViewer;
            ViewBag.Branches = m_context.GetBranches(false).Select(w => new SelectListItem { Text = w.Name, Value = w.Code });
            return View(model);
        }

        public ActionResult StockStatus()
        {
            ViewBag.Categories = m_context.GetAssetCategoryList().Select(w => new SelectListItem { Text = w, Value = w });

            var reportViewer = new ReportViewer()
            {
                KeepSessionAlive = false,
                ProcessingMode = ProcessingMode.Local,
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                Height = Unit.Percentage(100)
            };

            ViewBag.ReportViewer = reportViewer;
            var model = new AimsStockStatusViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StockStatus(AimsStockStatusViewModel model)
        {
            var reportViewer = new ReportViewer()
            {
                KeepSessionAlive = false,
                ProcessingMode = ProcessingMode.Local,
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                Height = Unit.Percentage(100)
            };

            if (ModelState.IsValid && model.SearchCategories != null)
            {
                var dataset = m_context.GetStockStatusDataSet(model.SearchCategories);
                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Aims\AimsStockStatusReport.rdlc";
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dataset.Tables[0]));
                //var categoryParameter = new ReportParameter("searchKey", model.SearchCategory);
                //reportViewer.LocalReport.SetParameters(categoryParameter);
                
            }

            ViewBag.ReportViewer = reportViewer;
            ViewBag.Categories = m_context.GetAssetCategoryList().Select(w => new SelectListItem { Text = w, Value = w });
            return View(model);
        }

        public ActionResult HtmlTableSample()
        {
            var model = new DeviceAtBranchSummaryViewModel { ItemsAtPpl = new List<ItemAtPpl>(), ItemsOutAtPpl = new List<ItemAtPpl>() };
            ViewBag.Branches = ApplicationHelper.GetBranches().Select(w => new SelectListItem { Text = w.Value, Value = w.Key });
            return View(model);
        }
    }
}