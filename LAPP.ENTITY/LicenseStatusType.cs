using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class LicenseStatusType : BaseEntity

    {
        public int LicenseStatusTypeId { get; set; }
        public string LicenseStatusTypeCode { get; set; }
        public string LicenseStatusTypeName { get; set; }
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

    public class LicenseStatusTypeGet : BaseEntity

    {
        public int LicenseStatusTypeId { get; set; }
        public string LicenseStatusTypeCode { get; set; }
        public string LicenseStatusTypeName { get; set; }
        public string StatusTypeColorCode { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class LicenseStatusTypeGetResponse : BaseEntityServiceResponse
    {
        public List<LicenseStatusTypeGet> LicenseStatusTypeGetList { get; set; }
    }
}
