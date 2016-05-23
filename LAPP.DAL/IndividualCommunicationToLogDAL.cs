using LAPP.ENTITY;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.DAL
{
    public class IndividualCommunicationToLogDAL : BaseDAL
    {
        public int Save_IndividualCommunicationToLog(IndividualCommunicationToLog objIndividualCommunicationToLog)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ApplicationId", objIndividualCommunicationToLog.ApplicationId));
            lstParameter.Add(new MySqlParameter("IndividualCommunicationToLogGuid", objIndividualCommunicationToLog.IndividualCommunicationToLogGuid));
            lstParameter.Add(new MySqlParameter("IndividualCommunicationToLogId", objIndividualCommunicationToLog.IndividualCommunicationToLogId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualCommunicationToLog.IndividualId));
            lstParameter.Add(new MySqlParameter("IndividualCommunicationLogId", objIndividualCommunicationToLog.IndividualCommunicationLogId));

            lstParameter.Add(new MySqlParameter("EmailTo", objIndividualCommunicationToLog.EmailTo));
            lstParameter.Add(new MySqlParameter("UserIdTo", objIndividualCommunicationToLog.UserIdTo));


            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "IndividualCommunicationToLog_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }
    }
}
