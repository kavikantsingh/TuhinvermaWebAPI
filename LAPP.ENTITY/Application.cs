using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{

    public class ApplicationResponse : BaseEntity
    {
        public int ApplicationId { get; set; }
        public int ApplicationTypeId { get; set; }
        public int ApplicationStatusId { get; set; }
        public int? ApplicationStatusReasonId { get; set; }
        public string ApplicationNumber { get; set; }
        public string ApplicationSubmitMode { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public DateTime? ApplicationStatusDate { get; set; }
        public DateTime? PaymentDeadlineDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string ConfirmationNumber { get; set; }
        public string ReferenceNumber { get; set; }
        public bool IsFingerprintingNotRequired { get; set; }
        public bool IsPaymentRequired { get; set; }
        public bool CanProvisionallyHire { get; set; }
        public bool GoPaperless { get; set; }
        public int? LicenseRequirementId { get; set; }
        public int? WithdrawalReasonId { get; set; }
        public int? LicenseTypeId { get; set; }

        public bool IsActive { get; set; }
        public string ApplicationType { get; set; }
    }

    public class Application : ApplicationResponse

    {

        public bool IsDeleted { get; set; }
        public bool IsArchive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public string ApplicationGuid { get; set; }
    }
    public class ApplicationResponseGet : BaseEntityServiceResponse
    {
        public List<ApplicationResponse> ApplicationResponseList { get; set; }
    }

}
