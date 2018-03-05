using System;

namespace Entt.Ers.Models
{
    public class Acceptance
    {
        public string Id { get; set; }
        public string ConsignmentNo { get; set; }
        public DateTime DateTime { get; set; }
        public string SystemId { get; set; }
        public string SystemName { get; set; }
        public string ProductType { get; set; }
        public string ProductTypeDesc { get; set; }
        public string ItemCategory { get; set; }
        public string ItemCategoryDesc { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public decimal? Length { get; set; }
        public string CourierId { get; set; }
        public string LocationId { get; set; }
        public string LocationName { get; set; }
        public string Comment { get; set; }
        public string ScannerId { get; set; }
        public DateTime CreatedDate { get; set; }   
    }
}