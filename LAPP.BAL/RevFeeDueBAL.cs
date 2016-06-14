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
    public class RevFeeDueBAL : BaseBAL
    {
        RevFeeDueDAL objDal = new RevFeeDueDAL();

        public int Save_RevFeeDue(RevFeeDue objRevFeeDue)
        {
            return objDal.Save_RevFeeDue(objRevFeeDue);
        }

        public List<RevFeeDue> Get_All_RevFeeDue()
        {
            return objDal.Get_All_RevFeeDue();
        }

        public List<RevFeeDue> Get_RevFeeDue_by_IndividualIdAnd_ApplicationId(int IndividualId, int ApplicationId)
        {
            return objDal.Get_RevFeeDue_by_IndividualIdAnd_ApplicationId(IndividualId, ApplicationId);
        }

        public List<RevFeeDue> Get_RevFeeDue_by_IndividualId(int IndividualId)
        {
            return objDal.Get_RevFeeDue_by_IndividualId(IndividualId);
        }

        public List<RevFeeDue> Get_RevFeeDue_by_TransactionId(int TransactionId)
        {
            return objDal.Get_RevFeeDue_by_TransactionId(TransactionId);
        }

        public RevFeeDue Get_RevFeeDue_By_RevFeeDueId(int ID)
        {
            return objDal.Get_RevFeeDue_By_RevFeeDueId(ID);
        }

        public List<RevFeeDue> Get_Unpaid_RevFeeDue_by_IndividualId(int individualId)
        {
              return objDal.Get_Unpaid_RevFeeDue_by_IndividualId(individualId);
        }
    }
}
