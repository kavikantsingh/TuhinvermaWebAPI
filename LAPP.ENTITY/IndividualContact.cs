using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class IndividualContactResponse : ContactResponse

    {
        public int IndividualContactId { get; set; }
        public int IndividualId { get; set; }
        public int ContactId { get; set; }
        public int ContactTypeId { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsPreferredContact { get; set; }
        public bool IsMobile { get; set; }
        public bool IsActive { get; set; }

    }

    public class IndividualContact : IndividualContactResponse

    {

        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string IndividualContactGuid { get; set; }
    }

    public class IndividualContactResponseRequest : BaseEntityServiceResponse
    {
        public List<IndividualContactResponse> IndividualContactResponse { get; set; }
    }
}
