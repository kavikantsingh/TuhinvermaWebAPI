using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAPP.DAL;
using LAPP.ENTITY;

namespace LAPP.BAL
{
    public class IndividualCommentLogBAL : BaseBAL

    {
        IndividualCommentLogDAL objDal = new IndividualCommentLogDAL();

        public int Save_IndividualCommentLog(IndividualCommentLog objIndividualCommentLog)
        {
            return objDal.Save_IndividualCommentLog(objIndividualCommentLog);
        }
        public IndividualCommentLog Get_IndividualCommentLog_By_IndividualCommentLogId(int ID)
        {
            return objDal.Get_IndividualCommentLog_By_IndividualCommentLogId(ID);
        }
        public List<IndividualCommentLog> Get_IndividualCommentLog_by_IndividualId(int IndividualId)
        {
            return objDal.Get_IndividualCommentLog_by_IndividualId(IndividualId);
        }

        public List<IndividualCommentLog> Get_IndividualCommentLog_by_IndividualIdANDTYPE(int IndividualId, string Type)
        {
            return objDal.Get_IndividualCommentLog_by_IndividualIdANDTYPE(IndividualId, Type);
        }
    }
}
