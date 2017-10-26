using System;

namespace Entt.Ers.Models
{
    public class Acceptance
    {
        public string ConsignmentNo { get; set; }
        public DateTime DateTime { get; set; }
        public string CourierId { get; set; }
        public string LocationId { get; set; }
        public string Comment { get; set; }
        public string ScannerId { get; set; }
        public DateTime CreatedDate { get; set; }   
    }
}