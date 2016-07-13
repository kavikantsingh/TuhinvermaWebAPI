using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using LAPP.ENTITY;
using LAPP.DAL;

namespace LAPP.BAL
{
    public class ProviderRelatedSchoolsBAL : BaseBAL
    {
        ProviderRelatedSchoolsDAL objDal = new ProviderRelatedSchoolsDAL();

        public int SaveProviderRelatedSchools(ProviderRelatedSchools objProviderRS)
        {
            return objDal.SaveProviderRelatedSchools(objProviderRS);
        }

        public int SaveProviderRelatedSchoolAddressLK(ProviderRelatedSchoolsAddLK objProvider)
        {
            return objDal.SaveProviderRelatedSchoolAddressLK(objProvider);
        }

        public int SaveProviderRelatedSchoolContactLK(ProviderRelatedSchoolsConLK objProvider)
        {
            return objDal.SaveProviderRelatedSchoolContactLK(objProvider);
        }

        public List<ProviderRelatedSchools> Get_ProviderRelatedSchool_By_ProviderId(int ProviderId, int ApplicationId)
        {
            return objDal.Get_ProviderRelatedSchool_By_ProviderId(ProviderId, ApplicationId);
        }
    }
}
