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
    public class ProviderStatusTypeBAL : BaseBAL
    {
        ProviderStatusTypeDAL objDal = new ProviderStatusTypeDAL();
        public int Save_ProviderStatusType(ProviderStatusType objProviderStatusType)
        {
            return objDal.Save_ProviderStatusType(objProviderStatusType);
        }

        public List<ProviderStatusType> Get_All_ProviderStatusType()
        {
            return objDal.Get_All_ProviderStatusType();
        }
        public ProviderStatusType Get_Provider_By_ProviderStatusTypeId(int ID)
        {
            return objDal.Get_Provider_By_ProviderStatusTypeId(ID);
        }

    }
}
