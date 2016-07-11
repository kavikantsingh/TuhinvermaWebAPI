using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{


    public class ProviderEligibility : BaseEntity
    {
        public int ProviderEligibilityId { get; set; }
        public int ProviderId { get; set; }
        public int ApplicationId { get; set; }
        public int ContentItemLkId { get; set; }
        public int ContentLkToPageTabSectionId { get; set; }
        public int ContentItemNo { get; set; }
        public bool IsChecked { get; set; }
        public string ProviderEligibilityIdGuid { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class ProviderEligibilityResponse : BaseEntityServiceResponse
    {
        public List<ProviderEligibility> ProviderEligibilitResponseList { get; set; }
    }


}
