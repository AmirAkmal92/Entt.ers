using Entt.Ers.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Entt.Ers.Controllers
{
    public class VersusController : ApplicationBaseController
    {
        private EnttReportDataContext m_context = new EnttReportDataContext();

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
            var model = new DeliveryExceptionReportViewModel { ReportDate = DateTime.Today,  ReportDay = 7.ToString()};
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeliveryExceptionReport(DeliveryExceptionReportViewModel model)
        {
            var reportViewer = new ReportViewer()
            {
                KeepSessionAlive = false,
                ProcessingMode = ProcessingMode.Local,
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                Height = Unit.Percentage(100)
            };

            if (ModelState.IsValid && model.ReportDate.HasValue)
            {
                var day = int.Parse(model.ReportDay);
                var dataset = m_context.GetDeliveryExceptionReportDataSet(model.ReportDate.Value, day);
                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Versus\DeliveryExceptionVsPod.rdlc";
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dataset.Tables[0]));
            }

            ViewBag.ReportViewer = reportViewer;
            ViewBag.ReportDays = ApplicationHelper.GetReportDays().Select(w => new SelectListItem { Text = w.Value, Value = w.Key.ToString() });
            return View(model);
        }
    }
}