using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class IndividualLicenseResponse : BaseEntity

    {
        public int IndividualLicenseId { get; set; }
        public int IndividualId { get; set; }
        public int? ApplicationId { get; set; }
        public int? ApplicationTypeId { get; set; }
        public int LicenseTypeId { get; set; }
        public bool IsLicenseTemporary { get; set; }
        public bool IsLicenseActive { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime OriginalLicenseDate { get; set; }
        public DateTime LicenseEffectiveDate { get; set; }
        public DateTime LicenseExpirationDate { get; set; }

        public int LicenseStatusTypeId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public string LicenseStatusTypeCode { get; set; }
        public string LicenseStatusTypeName { get; set; }
        public string LicenseStatusColorCode { get; set; }

        public string LicenseTypeName { get; set; }

        public string Description
        {

            get
            {
                return "Renewal Period from " + LicenseEffectiveDate.ToShortDateString() + " to " + LicenseExpirationDate.ToShortDateString() + " " + LicenseStatusTypeName;
            }
        }

        public string LicenseDetail { get; set; }

    }

    public class IndividualLicense : IndividualLicenseResponse
    {


        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string IndividualLicenseGuid { get; set; }
    }

    public class IndividualLicenseResponseRequest : BaseEntityServiceResponse
    {
        public List<IndividualLicenseResponse> IndividualLicenseList { get; set; }
    }
}
