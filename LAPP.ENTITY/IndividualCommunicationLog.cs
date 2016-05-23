using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class IndividualCommunicationLog : IndividualCommunicationLogRequest
    {

         
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string IndividualCommunicationLogGuid { get; set; }
    }

    public class IndividualCommunicationLogRequest :BaseEntity
    {

        public int IndividualCommunicationLogId { get; set; }
        public int IndividualId { get; set; }
        public int? ApplicationId { get; set; }
        public int? MasterTransactionId { get; set; }
        public int? PageModuleId { get; set; }
        public int? PageModuleTabSubModuleId { get; set; }
        public int? PageTabSectionId { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CommunicationLogDate { get; set; }
        [Display(Description = "Type:  E  - Email, S - System Message; Max Length:1")]
        public string Type { get; set; }
        public string EmailFrom { get; set; }
        public int? UserIdFrom { get; set; }
        public string Subject { get; set; }
        public string CommunicationSource { get; set; }
        public string CommunicationText { get; set; }
        [Display(Description = "Status:  S  - Success, F - Fail;  Max Length:1")]
        public string CommunicationStatus { get; set; }
        public string ReferenceNumber { get; set; }
        public bool IsInternalOnly { get; set; }
        public bool IsForInvestigationOnly { get; set; }
        public bool IsForPublic { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
         public string EmailTo { get; set; }
        public int? UserIdTo { get; set; }
    }
}
