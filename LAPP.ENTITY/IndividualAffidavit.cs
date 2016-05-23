using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{

    public class IndividualAffidavitResponse : BaseEntity

    {
        public int IndividualAffidavitId { get; set; }
        public int IndividualId { get; set; }
        public int ContentItemLkId { get; set; }
        public int ContentItemNumber { get; set; }
        public bool ContentItemResponse { get; set; }
        public string Desc { get; set; }
        public string ContentDescription { get; set; }

        public bool IsActive { get; set; }

    }
    public class IndividualAffidavit : IndividualAffidavitResponse

    {

        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public string IndividualAffidavitGuid { get; set; }
    }
}
