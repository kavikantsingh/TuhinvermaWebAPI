using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class ProviderStatusType : BaseEntity

    {
        public int ProviderStatusTypeId { get; set; }
        public string ProviderStatusTypeCode { get; set; }
        public string ProviderStatusTypeName { get; set; }
        public string StatusTypeColorCode { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
