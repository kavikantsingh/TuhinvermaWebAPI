using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class IndividualRenewal : BaseEntity
    {
        public List<IndividualLicenseResponse> IndividualLicense { get; set; }

        public IndividualResponse Individual { get; set; }
        public ApplicationResponse Application { get; set; }

        public List<IndividualAddressResponse> IndividualAddress { get; set; }
        public List<IndividualContactResponse> Contact { get; set; }

        public List<IndividualEmploymentResponse> IndividualEmployment { get; set; }

        public IndividualCertificationResponse IndividualCertification { get; set; }

        public SponsorInformationResponse SponsorInformation { get; set; }

        public List<IndividualNVBusinessLicenseResponse> BusinessLicenseInformation { get; set; }

        public List<IndividualChildSupportResponse> IndividualChildSupport { get; set; }

        public IndividualVeteranResponse IndividualVeteran { get; set; }

        public List<IndividualLegalResponse> IndividualLegal { get; set; }

        public List<IndividualCEHResponse> IndividualCEH { get; set; }
        public List<IndividualCECourseResponse> IndividualCECourse { get; set; }
        public IndividualAffidavitResponse IndividualAffidavit { get; set; }

        public List<FeesDetails> FeesDetails { get; set; }
    }


    public class RenewalGet : BaseEntity
    {
        public int IndividualId { get; set; }
        public string LicenseNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SubmittedOn { get; set; }
        public string Status { get; set; }
        public bool IsPaid { get; set; }

    }

    public class RenewalGetResponse : BaseEntityServiceResponse
    {

        public List<RenewalGet> RenewalGetList { get; set; }
    }


    public class IndividualRenewalResponse : BaseEntityServiceResponse
    {

        public IndividualRenewal IndividualRenewal { get; set; }
    }
}
