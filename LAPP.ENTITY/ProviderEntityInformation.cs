using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{

    public class ProviderEntityInformation : BaseEntityServiceResponse
    {
        public int ProviderId { get; set; }
        public int ApplicationId { get; set; }

        public int ProviderEligibilityId1 { get; set; }
        public int ContentItemLkId1 { get; set; }
        public bool IsChecked1 { get; set; }

        public int ProviderEligibilityId2 { get; set; }
        public int ContentItemLkId2 { get; set; }
        public bool IsChecked2 { get; set; }

        public int ProviderEligibilityId3 { get; set; }
        public int ContentItemLkId3 { get; set; }
        public bool IsChecked3 { get; set; }

        public int ProviderEligibilityId4 { get; set; }
        public int ContentItemLkId4 { get; set; }
        public bool IsChecked4 { get; set; }

    }
}