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
    public class ProviderUserBAL : BaseBAL
    {
        ProviderUserDAL objDal = new ProviderUserDAL();
        public int Save_ProviderUser(ProviderUser objProviderUser)
        {
            return objDal.Save_ProviderUser(objProviderUser);
        }

        public ProviderUser Get_ProviderUser_By_UserId(int ID)
        {
            return objDal.Get_ProviderUser_By_UserId(ID);
        }

        //public List<Provider> Get_All_Provider()
        //{
        //    return objDal.Get_All_Provider();
        //}
        
    }
}
