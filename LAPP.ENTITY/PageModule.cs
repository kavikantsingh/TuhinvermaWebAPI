using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class PageModule : BaseEntity

    {
        public int PageModuleId { get; set; }
        public string PageModuleCode { get; set; }
        public string PageModuleName { get; set; }
        public string PageModuleDesc { get; set; }
        public int MasterTransactionId { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class PageModuleResponse : BaseEntityServiceResponse
    {
        public object PageModule { get; set; }
    }
}
