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
    public class ProviderApplicationBAL : BaseBAL
    {
        ProviderApplicationDAL objDal = new ProviderApplicationDAL();

        public int Save_ProviderApplication(ProviderApplicationGET objProviderApplication)
        {
            return objDal.Save_ProviderApplication(objProviderApplication);
        }

        public List<ProviderApplicationGET> Get_All_ProviderApplication()
        {
            return objDal.Get_All_ProviderApplication();
        }


    }
}
