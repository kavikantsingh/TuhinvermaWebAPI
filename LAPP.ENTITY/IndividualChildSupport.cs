using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class IndividualChildSupportResponse : BaseEntity

    {
        public int IndividualChildSupportId { get; set; }
        public int IndividualId { get; set; }
        public int ContentItemLkId { get; set; }
        public int ContentItemHash { get; set; }
        public bool ContentItemResponse { get; set; }

        public bool IsActive { get; set; }
       
    }

    public class IndividualChildSupport : IndividualChildSupportResponse

    {
       
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public string IndividualChildSupportGuid { get; set; }
    }
}
