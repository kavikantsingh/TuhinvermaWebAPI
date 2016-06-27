using LAPP.DAL;
using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.BAL
{
    public class ApprovalAgencyBAL
    {
        ApprovalAgencyDAL objDAL = new ApprovalAgencyDAL();

        public int Save_ApprovalAgency(ApprovalAgency objApprovalAgency)
        {
            return objDAL.Save_ApprovalAgency(objApprovalAgency);
        }

        //public int Update_ApprovalAgency(ApprovalAgency objApprovalAgency)
        //{
        //    return objDAL.Update_ApprovalAgency(objApprovalAgency);
        //}

        public int Individual_User_Save(ApprovalAgency objApprovalAgency)
        {
            return objDAL.Individual_User_Save(objApprovalAgency);

        }

        public ApprovalAgency Get_ApprovalAgency_byApprovalAgencyId(int ID)
        {
            return objDAL.Get_ApprovalAgency_byUserId(ID);
        }
        
        public List<ApprovalAgency> Get_All_ApprovalAgency()
        {
            return objDAL.Get_All_ApprovalAgency();
        }

        public List<ApprovalAgency> Search_ApprovalAgency(ApprovalAgencySearch objApprovalAgency)
        {
            return objDAL.Search_ApprovalAgency(objApprovalAgency);
        }

        public List<ApprovalAgency> Search_ApprovalAgency_WithPager(ApprovalAgencySearch objApprovalAgency, int CurrentPage, int PagerSize)
        {
            return objDAL.Search_ApprovalAgency_WithPager(objApprovalAgency, CurrentPage, PagerSize);
        }
        public ApprovalAgency Get_ApprovalAgency_byIndividualId(int individualId)
        {
            return objDAL.Get_ApprovalAgency_byIndividualId(individualId);
        }
    }
}
