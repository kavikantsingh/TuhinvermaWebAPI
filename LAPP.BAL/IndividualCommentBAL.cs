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
    public class IndividualCommentBAL : BaseBAL
    {
        IndividualCommentDAL objDal = new IndividualCommentDAL();

        public int Save_IndividualComment(IndividualComment objIndividualComment)
        {
            return objDal.Save_IndividualComment(objIndividualComment);
        }

        public List<IndividualComment> Get_All_IndividualComment()
        {
            return objDal.Get_All_IndividualComment();
        }

        public List<IndividualComment> Get_IndividualComment_by_IndividualId(int IndividualId)
        {
            return objDal.Get_IndividualComment_by_IndividualId(IndividualId);
        }

        public IndividualComment Get_IndividualComment_By_IndividualCommentId(int ID)
        {
            return objDal.Get_IndividualComment_By_IndividualCommentId(ID);
        }

    }
}
