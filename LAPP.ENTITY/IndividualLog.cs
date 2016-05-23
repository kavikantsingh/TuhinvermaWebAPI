using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class IndividualLog : BaseEntity

    {
        public int IndividualLogId { get; set; }
        public int IndividualId { get; set; }
        public int? ApplicationId { get; set; }
        public int? MasterTransactionId { get; set; }
        public int? PageModuleId { get; set; }
        public int? PageModuleTabSubModuleId { get; set; }
        public int? PageTabSectionId { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string LogSource { get; set; }
        public string LogText { get; set; }
        public string ReferenceNumber { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public string IndividualLogGuid { get; set; }
    }
}
