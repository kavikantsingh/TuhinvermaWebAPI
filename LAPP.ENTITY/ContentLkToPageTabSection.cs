using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class ContentLkToPageTabSection : BaseEntity
    {
        public int ContentLkToPageTabSectionId { get; set; }
        public string ContentTypeName { get; set; }
        public int MasterTransactionId { get; set; }
        public int PageModuleId { get; set; }
        public int PageModuleTabSubModuleId { get; set; }
        public int PageTabSectionId { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsEditable { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class ContentLkToPageTabSectionResponse : BaseEntityServiceResponse
    {
        public object ContentLkToPageTabSection { get; set; }
    }
}
