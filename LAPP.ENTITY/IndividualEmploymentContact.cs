using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class IndividualEmploymentContactResponse : ContactResponse
    {
        public int IndividualEmploymentContactId { get; set; }
        public int IndividualId { get; set; }
        public int ContactId { get; set; }
        public int IndividualEmploymentId { get; set; }
        public int ContactTypeId { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsPreferredContact { get; set; }
        public bool IsMobile { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class IndividualEmploymentContact : IndividualEmploymentContactResponse
    {
        
        
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string IndividualEmploymentContactGuid { get; set; }
    }
}
