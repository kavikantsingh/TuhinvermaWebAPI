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
        public int ContentItemNumber { get; set; }
        public bool ContentItemResponse { get; set; }

        public bool IsActive { get; set; }

        public string ContentDescription { get; set; }
        public string ContentItemLkCode { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class IndividualChildSupport : IndividualChildSupportResponse

    {

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public string IndividualChildSupportGuid { get; set; }
    }

    public class IndividualChildSupportResponseRequest : BaseEntityServiceResponse
    {
        public List<IndividualChildSupportResponse> IndividualChildSupportResponseList { get; set; }
    }
}
