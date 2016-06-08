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
    public class ProviderBAL : BaseBAL
    {
        ProviderDAL objDal = new ProviderDAL();
        public int Save_Provider(Provider objProvider)
        {
            return objDal.Save_Provider(objProvider);
        }

        public List<Provider> Get_All_Provider()
        {
            return objDal.Get_All_Provider();
        }


        public Provider Get_Provider_By_ProviderId(int ID)
        {
            return objDal.Get_Provider_By_ProviderId(ID);
        }


        public int SaveSchoolInformation(ProviderInformation objProvider)
        {
            return objDal.SaveSchoolInformation(objProvider);
        }
    }
}
