using System;

namespace Entt.Ers.Models
{
    public class HomeIndexViewModel
    {
        public DateTime Date { get; set; }
        public DailyStatistics Statistic { get; set; }
        public DailyStatistics BranchStatistic { get; set; }
        public bool ShowBranchInfo { get; set; }
        public string SearchKey { get; set; }
    }

    public class DailyStatistics
    {
        public int Acceptances { get; set; }
        public int Deliveries { get; set; }
        public int Unknowns { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
    }

    public class HomeSearchViewModel
    {
        public Acceptance Acceptance { get; set; }
        public string SearchKey { get; set; }
    }
}