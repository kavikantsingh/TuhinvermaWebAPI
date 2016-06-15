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
        public string Name { get; set; }
        public string SignatureName { get; set; }
        public DateTime? Date { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int ApplicationId { get; set; }
    }
    public class IndividualAffidavit : IndividualAffidavitResponse

    {

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public string IndividualAffidavitGuid { get; set; }
    }

    public class IndividualAffidavitResponseRequest : BaseEntityServiceResponse
    {
        public List<IndividualAffidavitResponse> IndividualAffidavitResponseList { get; set; }
    }
}
