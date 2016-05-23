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
    public class IndividualLogBAL : BaseBAL
    {
        IndividualLogDAL objDal = new IndividualLogDAL();

        public int Save_IndividualLog(IndividualLog objIndividualLog)
        {
            return objDal.Save_IndividualLog(objIndividualLog);
        }

        public List<IndividualLog> Get_All_IndividualLog()
        {
            return objDal.Get_All_IndividualLog();
        }

        public List<IndividualLog> Get_IndividualLog_by_IndividualId(int IndividualId)
        {
            return objDal.Get_IndividualLog_by_IndividualId(IndividualId);
        }

        public IndividualLog Get_IndividualLog_By_IndividualLogId(int ID)
        {
            return objDal.Get_IndividualLog_By_IndividualLogId(ID);
        }

    }
}
