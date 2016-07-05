using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class ProviderOtherProgramName : BaseEntity
    {
        public int ProviderOtherProgramId { get; set; }
        public int ProviderId { get; set; }
        public int ApplicationId { get; set; }
        public string ProgramOtherName { get; set; }
        public string ReferenceNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ProviderOtherProgramGuid { get; set; }
        public string Action { get; set; }
    }

    public class ProviderOtherProgramNameGetResponse : BaseEntityServiceResponse
    {
        public List<ProviderOtherProgramName> ProviderOtherProgramList { get; set; }
    }
}
