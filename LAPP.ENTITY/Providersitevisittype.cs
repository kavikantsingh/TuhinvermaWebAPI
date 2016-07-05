using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class Providersitevisittype : ProvidersitevisittypeResponse
    {

        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class ProvidersitevisittypeResponse : BaseEntity
    {

        public int ProviderSiteVisitTypeId { get; set; }
        public string ProviderSiteVisitTypeCode { get; set; }
        public string ProviderSiteVisitTypeName { get; set; }
        public bool IsActive { get; set; }
    }

    public class ProvidersitevisittypeRequestResponse : BaseEntityServiceResponse
    {
        public List<Providersitevisittype> ProvidersitevisittypeGetList { get; set; }
    }

}
