using System;

namespace Entt.Ers.Models
{
    public class HomeIndexViewModel
    {
        public DateTime Date { get; set; }
        public DailyStatistics Statistic { get; set; }
    }

    public class DailyStatistics
    {
        public int Acceptances { get; set; }
        public int Deliveries { get; set; }
        public int Unknowns { get; set; }
    }
}