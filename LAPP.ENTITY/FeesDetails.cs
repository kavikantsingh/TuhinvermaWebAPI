using System;
namespace LAPP.ENTITY
{
    public class FeesDetails :BaseEntity
    {
        public int FeeId { get; set; }
        public string  Description { get; set; }
        public string  FeeType { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }

    }
}