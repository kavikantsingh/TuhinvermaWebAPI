using LAPP.DAL;
using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.BAL
{
    
    public class ProviderApprovalAgencyBAL : BaseBAL
    {

        ProviderApprovalAgencyDAL objDal = new ProviderApprovalAgencyDAL();
        public int Save_ProviderApprovalAgency(ProviderApprovalAgency objProviderApprovalAgency)
        {
            return objDal.Save_ProviderApprovalAgency(objProviderApprovalAgency);
        }

        public List<ProviderApprovalAgency> Get_All_ProviderApprovalAgency(ProviderApprovalAgency objProviderApprovalAgency)
        {
            return objDal.Get_All_ProviderApprovalAgency(objProviderApprovalAgency);
        }


        public int DeleteProviderApprovalAgency(ProviderApprovalAgency objProviderApprovalAgency)
        {
            return objDal.DeleteProviderApprovalAgency(objProviderApprovalAgency);
        }
        


    }

}
