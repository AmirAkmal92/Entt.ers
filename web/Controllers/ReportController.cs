using OfficeOpenXml;
using Entt.Ers.Models;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Entt.Ers.Controllers
{
    public class ReportController : ApplicationBaseController
    {
        private ApplicationDbContext m_context = new ApplicationDbContext();

        public ActionResult Export(string unit)
        {
            using (var xlPackage = new ExcelPackage())
            {
                var ws = xlPackage.Workbook.Worksheets.Add("Sheet1");
                ws.SetValue(1, 1, "Updated this cell");
                return File(xlPackage.GetAsByteArray(), "application/excel", "test.xlsx");
            }
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