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
    }
}
