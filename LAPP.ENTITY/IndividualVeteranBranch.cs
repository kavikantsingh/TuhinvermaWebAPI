using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class IndividualVeteranBranchResponse : BaseEntity

    {
        public int IndividualVeteranBranchId { get; set; }
        public int IndividualId { get; set; }
        public int IndividualVeteranId { get; set; }
        public int BranchofServicesId { get; set; }
        public bool BranchofServicesIdResponse { get; set; }

        public bool IsActive { get; set; }

    }

    public class IndividualVeteranBranch : IndividualVeteranBranchResponse

    {

        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public string IndividualVeteranBranchGuid { get; set; }
    }
}
