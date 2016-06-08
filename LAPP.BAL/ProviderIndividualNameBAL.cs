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
    public class ProviderIndividualNameBAL : BaseBAL
    {
        ProviderIndividualNameDAL objDal = new ProviderIndividualNameDAL();
        public int Save_ProviderIndividualName(ProviderIndividualName objProviderIndName)
        {
            return objDal.Save_ProviderIndividualName(objProviderIndName);
        }

        //public List<Provider> Get_All_Provider()
        //{
        //    return objDal.Get_All_Provider();
        //}


        //public Provider Get_Provider_By_ProviderId(int ID)
        //{
        //    return objDal.Get_Provider_By_ProviderId(ID);
        //}
    }
}
