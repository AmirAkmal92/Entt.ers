using System.Collections.Generic;

namespace Entt.Ers.Models
{
    public class HourlyInfo
    {
        public int Hour00 { get; set; }
        public int Hour01 { get; set; }
        public int Hour02 { get; set; }
        public int Hour03 { get; set; }
        public int Hour04 { get; set; }
        public int Hour05 { get; set; }
        public int Hour06 { get; set; }
        public int Hour07 { get; set; }
        public int Hour08 { get; set; }
        public int Hour09 { get; set; }
        public int Hour10 { get; set; }
        public int Hour11 { get; set; }
        public int Hour12 { get; set; }
        public int Hour13 { get; set; }
        public int Hour14 { get; set; }
        public int Hour15 { get; set; }
        public int Hour16 { get; set; }
        public int Hour17 { get; set; }
        public int Hour18 { get; set; }
        public int Hour19 { get; set; }
        public int Hour20 { get; set; }
        public int Hour21 { get; set; }
        public int Hour22 { get; set; }
        public int Hour23 { get; set; }
        public int Total { get; set; }

        public int[] ToArray()
        {
            var list = new List<int>
            {
                Hour00,
                Hour01,
                Hour02,
                Hour03,
                Hour04,
                Hour05,
                Hour06,
                Hour07,
                Hour08,
                Hour09,
                Hour10,
                Hour11,
                Hour12,
                Hour13,
                Hour14,
                Hour15,
                Hour16,
                Hour17,
                Hour18,
                Hour19,
                Hour20,
                Hour21,
                Hour22,
                Hour23
            };
            return list.ToArray();
        }
    }
}