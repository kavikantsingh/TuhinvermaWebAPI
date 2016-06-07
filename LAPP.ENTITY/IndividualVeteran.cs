using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class IndividualVeteranResponse : BaseEntity

    {
        public int IndividualVeteranId { get; set; }
        public int IndividualId { get; set; }
        public bool ServedInMilitary { get; set; }
        public bool SpouseofActiveMilitaryMember { get; set; }
        public string MilitaryOccupationSpeciality { get; set; }
        public DateTime? ServiceDateFrom { get; set; }
        public DateTime? ServiceDateTo { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public List<IndividualVeteranBranchResponse> VeteranBranches { get; set; }
    }


    public class IndividualVeteran : IndividualVeteranResponse

    {
      
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public string IndividualVeteranGuid { get; set; }
    }
}
