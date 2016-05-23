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
    public class IndividualCommentLogDAL : BaseDAL
    {
        public int Save_IndividualCommentLog(IndividualCommentLog objIndividualCommentLog)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ApplicationId", objIndividualCommentLog.ApplicationId));

            lstParameter.Add(new MySqlParameter("CommentLogDate", objIndividualCommentLog.CommentLogDate));
            lstParameter.Add(new MySqlParameter("CommentLogSource", objIndividualCommentLog.CommentLogSource));
            lstParameter.Add(new MySqlParameter("CommentLogText", objIndividualCommentLog.CommentLogText));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualCommentLog.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objIndividualCommentLog.CreatedOn));
            lstParameter.Add(new MySqlParameter("EffectiveDate", objIndividualCommentLog.EffectiveDate));
            lstParameter.Add(new MySqlParameter("EndDate", objIndividualCommentLog.EndDate));
            lstParameter.Add(new MySqlParameter("IndividualCommentLogGuid", objIndividualCommentLog.IndividualCommentLogGuid));
            lstParameter.Add(new MySqlParameter("IndividualCommentLogId", objIndividualCommentLog.IndividualCommentLogId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualCommentLog.IndividualId));
            lstParameter.Add(new MySqlParameter("IsActive", objIndividualCommentLog.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualCommentLog.IsDeleted));
            lstParameter.Add(new MySqlParameter("IsForInvestigationOnly", objIndividualCommentLog.IsForInvestigationOnly));
            lstParameter.Add(new MySqlParameter("IsForPublic", objIndividualCommentLog.IsForPublic));
            lstParameter.Add(new MySqlParameter("IsInternalOnly", objIndividualCommentLog.IsInternalOnly));

            lstParameter.Add(new MySqlParameter("MasterTransactionId", objIndividualCommentLog.MasterTransactionId));
            lstParameter.Add(new MySqlParameter("PageModuleId", objIndividualCommentLog.PageModuleId));
            lstParameter.Add(new MySqlParameter("PageModuleTabSubModuleId", objIndividualCommentLog.PageModuleTabSubModuleId));
            lstParameter.Add(new MySqlParameter("PageTabSectionId", objIndividualCommentLog.PageTabSectionId));
            lstParameter.Add(new MySqlParameter("ReferenceNumber", objIndividualCommentLog.ReferenceNumber));
            lstParameter.Add(new MySqlParameter("Type", objIndividualCommentLog.Type));


            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "IndividualCommentLog_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }
    }
}
