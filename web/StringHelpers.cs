using System;

namespace Entt.Ers
{
    public static class StringHelpers
    {
        public static string ToRankDescription(this string rank)
        {
            var description = string.Empty;
            switch(rank)
            {
                case "LAKSDYA": description = "Laksamana Madya"; break;
                case "LAKSDA": description = "Laksamana Muda"; break;
                case "LAKSMA": description = "Laksamana Pertama"; break;
                case "KEPT": description = "Kepten"; break;
                case "KDR": description = "Komander"; break;
                case "LT KDR": description = "Leftenan Komander"; break;
                case "LT": description = "Leftenan"; break;
                case "LT DYA": description = "Leftenan Madya"; break;
                case "LT MDA": description = "Leftenan Muda"; break;
                case "PKK": description = "Pegawai Kadet Kanan"; break;
                case "KDT": description = "Kadet"; break;
            }
            return description;
        }
    }
}