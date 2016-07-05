using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAPP.ENTITY;
using LAPP.DAL;

namespace LAPP.BAL
{
    public class providermblexBAL : BaseBAL
    {

        ProvidermblexDAL objDal = new ProvidermblexDAL();
        public int Save_Providermblex(Providermblex objProvidermblex)
        {
            return objDal.Save_Providermblex(objProvidermblex);
        }

        public List<ProvidermblexResponse> Get_All_Providermblex(Providermblex objProvidermblex)
        {
            return objDal.Get_All_Providermblex(objProvidermblex);
        }

        
    }
}
