using System.Collections.Generic;

namespace Entt.Ers
{
    public static class AppHelper
    {
        public static Dictionary<string, string> GetBranches()
        {
            var list = new Dictionary<string, string>
            {
                { "7185", "Pusat POS Laju Nilai" },
                { "4038", "Commercial Courier" },
                { "6101", "HUB POS Laju Domestik KLIA" }
            };
            return list;
        }
    }
}