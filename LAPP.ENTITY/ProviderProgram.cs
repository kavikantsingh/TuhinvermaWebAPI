using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{

    public class ProviderProgram : BaseEntity
    {
        public int ProviderProgramId { get; set; }
        public int ProviderId { get; set; }
        public int ApplicationId { get; set; }
        public string ProgramName { get; set; }
        public decimal TotalNoofPgmHours { get; set; }
        public string ReferenceNumber { get; set; }

        public bool IsProgramApproved { get; set; }
        public DateTime? ProgramApprovalStartDate { get; set; }
        public DateTime? ProgramApprovalEndDate { get; set; }

        public string ProviderProgramGuid { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class ProviderProgramResponse : BaseEntityServiceResponse
    {
        public List<ProviderProgram> ProviderProgramResponseList { get; set; }
    }

}
