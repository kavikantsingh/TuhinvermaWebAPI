using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class IndividualCertificationResponse : BaseEntity
    {
        public int IndividualCertificationId { get; set; }
        public int IndividualId { get; set; }
        public int? CertificationTypeId { get; set; }
        public string ClinicalComptence { get; set; }
        public bool IsClinicalComptence { get; set; }
        public DateTime? DateIssued { get; set; }
        public string ABAMember { get; set; }
        public string PraxisExam { get; set; }
        public bool NoChanges { get; set; }
        public bool IsNBCHIS { get; set; }
        public string NBCHISAccount { get; set; }
        public string NBCHISCertificate { get; set; }
        public DateTime? DatePassed { get; set; }
        public string ABA { get; set; }
        public string ASHA { get; set; }

        public bool IsNBCOTCertified { get; set; }
        public bool IsNBCOTAppliedforRenewal { get; set; }
        public bool IsNBCOTExamScheduled { get; set; }

        public DateTime? NBCOTDateTaken { get; set; }
        public DateTime? NBCOTDatePassed { get; set; }
        public DateTime? NBCOTDateScheduled { get; set; }


        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }


    }

    public class IndividualCertification : IndividualCertificationResponse
    {

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public string IndividualCertificationGuid { get; set; }

    }
}
