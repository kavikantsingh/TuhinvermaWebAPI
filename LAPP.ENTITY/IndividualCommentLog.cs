using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class IndividualCommentLog : IndividualCommentLogRequest
    {


        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string IndividualCommentLogGuid { get; set; }


    }

    public class IndividualCommentLogRequest : BaseEntity
    {
        public int IndividualCommentLogId { get; set; }
        public int IndividualId { get; set; }
        public int? ApplicationId { get; set; }
        public int? MasterTransactionId { get; set; }
        public int? PageModuleId { get; set; }
        public int? PageModuleTabSubModuleId { get; set; }
        public int? PageTabSectionId { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CommentLogDate { get; set; }
        [Display(Description = "Type: L - Log, C - Comment Max Length:1")]
        public string Type { get; set; }
        public string CommentLogSource { get; set; }
        public string CommentLogText { get; set; }
        public string ReferenceNumber { get; set; }
        public bool IsInternalOnly { get; set; }
        public bool IsForInvestigationOnly { get; set; }
        public bool IsForPublic { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class IndividualCommentLogRequestResponce : BaseEntityServiceResponse
    {
        public List<IndividualCommentLogRequest> IndividualCommentLogRequest { get; set; }
    }
}
