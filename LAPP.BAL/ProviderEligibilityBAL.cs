using LAPP.DAL;
using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.BAL
{
    
    public class ProviderEligibilityBAL : BaseBAL
    {

        ProviderEligibilityDAL objDal = new ProviderEligibilityDAL();
        public int Save_ProviderEligibility(ProviderEligibility objProviderEligibility)
        {
            return objDal.Save_ProviderEligibility(objProviderEligibility);
        }

        public List<ProviderEligibility> Get_All_ProviderEligibility(int ProviderId, int ProviderEligibilityId)
        {
            return objDal.Get_All_ProviderEligibility(ProviderId, ProviderEligibilityId);
        }

    }

}
