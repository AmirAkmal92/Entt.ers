using System;

namespace Entt.Ers.Models
{
    public class StandardReportViewModel
    {
        public DateTime ReportDate { get; set; }
        public string SelectedBranch { get; set; }
    }

    public class PrefixReportViewModel
    {
        public DateTime ReportDate { get; set; }
        public string ReportDay { get; set; }
    }
}