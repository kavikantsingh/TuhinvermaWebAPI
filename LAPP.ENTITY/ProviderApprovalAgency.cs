using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    
    public class ProviderApprovalAgency : ProviderApprovalAgencyResponse
    {

        public int ProviderApprovalAgencyId { get; set; }
        public int ProviderId { get; set; }
        public int ApplicationId { get; set; }
        public int ApprovalAccreditingAgencyId { get; set; }
        public string ApprovalAccreditingAgencyName { get; set; }
        public string AgencySchoolCode { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsAdditional { get; set; }
        public string ReferenceNumber { get; set; }
        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ProviderApprovalAgencyGuid { get; set; }

    }
    



    public class ProviderApprovalAgencyResponse : BaseEntityServiceResponse
    {
        public List<ProviderApprovalAgency> ProviderApprovalAgencyResponseList { get; set; }
    }


}

