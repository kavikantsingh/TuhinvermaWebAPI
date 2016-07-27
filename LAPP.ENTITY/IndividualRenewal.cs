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

        public List<SponsorInformationResponse> SponsorInformation { get; set; }

        public List<IndividualNVBusinessLicenseResponse> BusinessLicenseInformation { get; set; }

        public List<IndividualChildSupportResponse> IndividualChildSupport { get; set; }

        public IndividualVeteranResponse IndividualVeteran { get; set; }

        public List<IndividualLegalResponse> IndividualLegal { get; set; }

         public List<IndividualCEHResponse> IndividualCEH { get; set; }
        public List<IndividualCECourseResponse> IndividualCECourse { get; set; }
        public IndividualAffidavitResponse IndividualAffidavit { get; set; }

        public List<FeeDetails> FeesDetails { get; set; }
        public int RequestedLicenseStatusTypeId { get; set; }
        public string AffirmativeAction { get; set; }
        public string Action { get; set; }
        public string SentFrom { get; set; }
    }

    public class RenewalGet : BaseEntity
    {
        public int ApplicationId { get; set; }
        public int ApplicationTypeId { get; set; }
        public int ApplicationStatusId { get; set; }
        public int ApplicationStatusReasonId { get; set; }
        public string ApplicationNumber { get; set; }
        public string ApplicationSubmitMode { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public DateTime ApplicationStatusDate { get; set; }
        public DateTime PaymentDeadlineDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public string ConfirmationNumber { get; set; }
        public string ReferenceNumber { get; set; }
        public bool IsFingerprintingNotRequired { get; set; }
        public bool IsPaymentRequired { get; set; }
        public bool CanProvisionallyHire { get; set; }
        public bool GoPaperless { get; set; }
        public int LicenseRequirementId { get; set; }
        public int WithdrawalReasonId { get; set; }
        public int LicenseTypeId { get; set; }

        public bool IsActive { get; set; }

        public bool IsPaid { get; set; }
        public int IndividualId { get; set; }
        public string LicenseNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ApplicationStatus { get; set; }
        public string Name { get; set; }
        public string SSN { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string Phone { get; set; }

        public int Total_Recard { get; set; }

    }


    public class RenewalGetSelected : BaseEntity
    {
        public int IndividualId { get; set; }
        public string LicenseNumber { get; set; }
        public string ApplicationNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public string ApplicationStatus { get; set; }
        public bool IsActive { get; set; }
        public bool IsPaid { get; set; }
        public string Name { get; set; }
        public string SSN { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string Phone { get; set; }
        public int Total_Recard { get; set; }

    }
    public class RenewalApplication : BaseEntity
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string LicenseNumber { get; set; }
        public string SSN { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public int IndividualId { get; set; }
        public string ApplicationNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public bool IsPaid { get; set; }
        // public int Total_Recard { get; set; }
        public int ApplicationId { get; set; }
        public int ApplicationStatusId { get; set; }
    }

    public class RenewalGetResponse : BaseEntityServiceResponse
    {
        public List<RenewalGetSelected> RenewalGetList { get; set; }
    }

    public class IndividualRenewalResponse : BaseEntityServiceResponse
    {
        public DateTime LicenseExpirationDate { get; set; }
        public IndividualRenewal IndividualRenewal { get; set; }
    }
    public class ConfigurationValueResponse : BaseEntityServiceResponse
    {
        public string SettingKey { get; set; }
        public bool IsValid { get; set; }

        public int Configurationvalue { get; set; }

    }
    public class RenewalSearchResponse : BaseEntityServiceResponse
    {
        public int Total_Recard { get; set; }
        public List<RenewalApplication> RenewalApplicationList { get; set; }
    }

    public class ConfirmAndApproveRequest : BaseEntity
    {
        public int ApplicationId { get; set; }
        public int IndividualLicenseID { get; set; }
        public int RequestedLicenseStatusTypeId { get; set; }
        public string AffirmativeAction { get; set; }
        public string Action { get; set; }
        public string SentFrom { get; set; }

    }

    public class ConfirmAndApproveResponse : BaseEntityServiceResponse
    {
       
    }


}
