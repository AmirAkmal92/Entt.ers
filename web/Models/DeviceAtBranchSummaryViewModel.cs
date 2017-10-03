using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Entt.Ers.Models
{
    public class DeviceAtBranchSummaryViewModel
    {
        public string[] SearchBranches { get; set; }
        public IList<ItemAtPpl> ItemsAtPpl { get; set; }
        public IList<ItemAtPpl> ItemsOutAtPpl { get; set; }
    }

    public class ItemAtPpl
    {
        public int No { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string ProductNo { get; set; }
        public string AssetCategory { get; set; }
        public string AssetName { get; set; }
        public int Quantity { get; set; }
        public DateTime? DateSendRepair { get; set; }
    }
}