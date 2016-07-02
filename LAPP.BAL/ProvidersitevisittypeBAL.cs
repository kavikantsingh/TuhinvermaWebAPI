using LAPP.DAL;
using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.BAL
{
    public class ProvidersitevisittypeBAL : BaseBAL
    {

        ProvidersitevisittypeDAL objDal = new ProvidersitevisittypeDAL();

        public int Save_Providersitevisittype(Providersitevisittype objProvidersitevisittype)
        {
            return objDal.Save_Providersitevisittype(objProvidersitevisittype);
        }

        public List<Providersitevisittype> Get_All_Providersitevisittype()
        {
            return objDal.Get_All_Providersitevisittype();
        }
    }
}
