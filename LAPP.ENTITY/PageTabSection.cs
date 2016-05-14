using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class PageTabSection : BaseEntity

    {
        public int PageTabSectionId { get; set; }
        public string PageTabSectionCode { get; set; }
        public string PageTabSectionName { get; set; }
        public string PageTabSectionDesc { get; set; }
        public int MasterTransactionId { get; set; }
        public int PageModuleId { get; set; }
        public int PageModuleTabSubModuleId { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
