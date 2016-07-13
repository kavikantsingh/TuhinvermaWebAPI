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
    public class ProviderNameBAL : BaseBAL
    {
        ProviderNameDAL objDal = new ProviderNameDAL();
        public int SavePreviousSchoolDetails(ProviderNames objProvider)
        {
            return objDal.SavePreviousSchoolDetails(objProvider);
        }


        public List<Provider> Get_All_Provider()
        {
            return objDal.Get_All_Provider();
        }


        public Provider Get_Provider_By_ProviderId(int ID)
        {
            return objDal.Get_Provider_By_ProviderId(ID);
        }

        public int SaveProviderNameAddress(ProviderNameAddress objProvider)
        {
            return objDal.SaveProviderNameAddress(objProvider);
        }

        public int SaveProviderNameContact(ProviderNameContact objProvider)
        {
            return objDal.SaveProviderNameContact(objProvider);
        }
    }
}
