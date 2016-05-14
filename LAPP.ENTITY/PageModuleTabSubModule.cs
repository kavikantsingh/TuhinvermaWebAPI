using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class PageModuleTabSubModule : BaseEntity

    {
        public int PageModuleTabSubModuleId { get; set; }
        public string PageModuleTabSubModuleCode { get; set; }
        public string PageModuleTabSubModuleName { get; set; }
        public string PageModuleTabSubModuleDesc { get; set; }
        public int MasterTransactionId { get; set; }
        public int PageModuleId { get; set; }
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
