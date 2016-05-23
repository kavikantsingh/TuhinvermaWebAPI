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
    public class IndividualCommunicationLogDAL : BaseDAL
    {
        public int Save_IndividualCommunicationLog(IndividualCommunicationLog objIndividualCommunicationLog)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ApplicationId", objIndividualCommunicationLog.ApplicationId));

            lstParameter.Add(new MySqlParameter("CommunicationLogDate", objIndividualCommunicationLog.CommunicationLogDate));
            lstParameter.Add(new MySqlParameter("CommunicationSource", objIndividualCommunicationLog.CommunicationSource));
            lstParameter.Add(new MySqlParameter("CommunicationText", objIndividualCommunicationLog.CommunicationText));

            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualCommunicationLog.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objIndividualCommunicationLog.CreatedOn));
            lstParameter.Add(new MySqlParameter("EffectiveDate", objIndividualCommunicationLog.EffectiveDate));
            lstParameter.Add(new MySqlParameter("EndDate", objIndividualCommunicationLog.EndDate));
            lstParameter.Add(new MySqlParameter("IndividualCommunicationLogGuid", objIndividualCommunicationLog.IndividualCommunicationLogGuid));
            lstParameter.Add(new MySqlParameter("IndividualCommunicationLogId", objIndividualCommunicationLog.IndividualCommunicationLogId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualCommunicationLog.IndividualId));
            lstParameter.Add(new MySqlParameter("IsActive", objIndividualCommunicationLog.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualCommunicationLog.IsDeleted));
            lstParameter.Add(new MySqlParameter("IsForInvestigationOnly", objIndividualCommunicationLog.IsForInvestigationOnly));
            lstParameter.Add(new MySqlParameter("IsForPublic", objIndividualCommunicationLog.IsForPublic));
            lstParameter.Add(new MySqlParameter("IsInternalOnly", objIndividualCommunicationLog.IsInternalOnly));

            lstParameter.Add(new MySqlParameter("MasterTransactionId", objIndividualCommunicationLog.MasterTransactionId));
            lstParameter.Add(new MySqlParameter("PageModuleId", objIndividualCommunicationLog.PageModuleId));
            lstParameter.Add(new MySqlParameter("PageModuleTabSubModuleId", objIndividualCommunicationLog.PageModuleTabSubModuleId));
            lstParameter.Add(new MySqlParameter("PageTabSectionId", objIndividualCommunicationLog.PageTabSectionId));
            lstParameter.Add(new MySqlParameter("ReferenceNumber", objIndividualCommunicationLog.ReferenceNumber));
            lstParameter.Add(new MySqlParameter("Type", objIndividualCommunicationLog.Type));

            lstParameter.Add(new MySqlParameter("Subject", objIndividualCommunicationLog.Subject));
            lstParameter.Add(new MySqlParameter("EmailFrom", objIndividualCommunicationLog.EmailFrom));
            lstParameter.Add(new MySqlParameter("CommunicationStatus", objIndividualCommunicationLog.CommunicationStatus));
            lstParameter.Add(new MySqlParameter("UserIdFrom", objIndividualCommunicationLog.UserIdFrom));



            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "IndividualCommunicationLog_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }
    }
}
