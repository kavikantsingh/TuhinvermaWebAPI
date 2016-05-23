using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{


    public class IndividualNVBusinessLicenseResponse : BaseEntity

    {
        public int IndividualNVBusinessLicenseId { get; set; }
        public int IndividualId { get; set; }
        public int ContentItemLkId { get; set; }
        public int ContentItemHash { get; set; }
        public bool ContentItemResponse { get; set; }
        public string Status { get; set; }
        public string NameonBusinessLicense { get; set; }
        public string BusinessLicenseNumber { get; set; }
        public string ContentDescription { get; set; }

        public bool IsActive { get; set; }

       
    }
    public class IndividualNVBusinessLicense : IndividualNVBusinessLicenseResponse

    {
 
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public string IndividualNVBusinessLicenseGuid { get; set; }
    }
}
