using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class DocumentLkToPageTabSection : BaseEntity

    {
        public int DocumentLkToPageTabSectionId { get; set; }
        public string DocumentLkToPageTabSectionCode { get; set; }
        public string DocumentTypeName { get; set; }
        public string DocumentTypeDesc { get; set; }
        public int Max_size { get; set; }
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
}
