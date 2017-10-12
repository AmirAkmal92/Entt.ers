using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;

namespace Entt.Ers.Models
{
    public class ReportEngine
    {
        public static ReportViewer Create()
        {
            return new ReportViewer()
            {
                KeepSessionAlive = false,
                ProcessingMode = ProcessingMode.Local,
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                Height = Unit.Percentage(100)
            };
        }
    }
}