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

        public IndividualCommentLog Get_IndividualCommentLog_By_IndividualCommentLogId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualCommentLogId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualcommentlog_Get_By_IndividualCommentLogId", lstParameter.ToArray());
            IndividualCommentLog objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<IndividualCommentLog> Get_IndividualCommentLog_by_IndividualId(int IndividualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            //lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualcommentlog_Get_By_IndividualId", lstParameter.ToArray());
            List<IndividualCommentLog> lstEntity = new List<IndividualCommentLog>();
            IndividualCommentLog objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<IndividualCommentLog> Get_IndividualCommentLog_by_IndividualIdANDTYPE(int IndividualId, string Type)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            lstParameter.Add(new MySqlParameter("G_Type", Type));
            //lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualcommentlog_Get_By_IndividualId_AND_Type", lstParameter.ToArray());
            List<IndividualCommentLog> lstEntity = new List<IndividualCommentLog>();
            IndividualCommentLog objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }
        private IndividualCommentLog FetchEntity(DataRow dr)
        {
            IndividualCommentLog objEntity = new IndividualCommentLog();

            if (dr.Table.Columns.Contains("IndividualCommentLogId") && dr["IndividualCommentLogId"] != DBNull.Value)
            {
                objEntity.IndividualCommentLogId = Convert.ToInt32(dr["IndividualCommentLogId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationId") && dr["ApplicationId"] != DBNull.Value)
            {
                objEntity.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
            }
            if (dr.Table.Columns.Contains("MasterTransactionId") && dr["MasterTransactionId"] != DBNull.Value)
            {
                objEntity.MasterTransactionId = Convert.ToInt32(dr["MasterTransactionId"]);
            }
            if (dr.Table.Columns.Contains("PageModuleId") && dr["PageModuleId"] != DBNull.Value)
            {
                objEntity.PageModuleId = Convert.ToInt32(dr["PageModuleId"]);
            }

            if (dr.Table.Columns.Contains("PageModuleTabSubModuleId") && dr["PageModuleTabSubModuleId"] != DBNull.Value)
            {
                objEntity.PageModuleTabSubModuleId = Convert.ToInt32(dr["PageModuleTabSubModuleId"]);
            }
            if (dr.Table.Columns.Contains("PageTabSectionId") && dr["PageTabSectionId"] != DBNull.Value)
            {
                objEntity.PageTabSectionId = Convert.ToInt32(dr["PageTabSectionId"]);
            }

            if (dr.Table.Columns.Contains("EffectiveDate") && dr["EffectiveDate"] != DBNull.Value)
            {
                objEntity.EffectiveDate = Convert.ToDateTime(dr["EffectiveDate"]);
            }
            if (dr.Table.Columns.Contains("EndDate") && dr["EndDate"] != DBNull.Value)
            {
                objEntity.EndDate = Convert.ToDateTime(dr["EndDate"]);
            }
            if (dr.Table.Columns.Contains("CommentLogDate") && dr["CommentLogDate"] != DBNull.Value)
            {
                objEntity.CommentLogDate = Convert.ToDateTime(dr["CommentLogDate"]);
            }
            if (dr.Table.Columns.Contains("Type") && dr["Type"] != DBNull.Value)
            {
                objEntity.Type = Convert.ToString(dr["Type"]);
            }
            if (dr.Table.Columns.Contains("CommentLogSource") && dr["CommentLogSource"] != DBNull.Value)
            {
                objEntity.CommentLogSource = Convert.ToString(dr["CommentLogSource"]);
            }
            if (dr.Table.Columns.Contains("CommentLogText") && dr["CommentLogText"] != DBNull.Value)
            {
                objEntity.CommentLogText = Convert.ToString(dr["CommentLogText"]);
            }
            if (dr.Table.Columns.Contains("ReferenceNumber") && dr["ReferenceNumber"] != DBNull.Value)
            {
                objEntity.ReferenceNumber = Convert.ToString(dr["ReferenceNumber"]);
            }

            if (dr.Table.Columns.Contains("IsInternalOnly") && dr["IsInternalOnly"] != DBNull.Value)
            {
                objEntity.IsInternalOnly = Convert.ToBoolean(dr["IsInternalOnly"]);
            }
            if (dr.Table.Columns.Contains("IsForInvestigationOnly") && dr["IsForInvestigationOnly"] != DBNull.Value)
            {
                objEntity.IsForInvestigationOnly = Convert.ToBoolean(dr["IsForInvestigationOnly"]);
            }
            if (dr.Table.Columns.Contains("IsForPublic") && dr["IsForPublic"] != DBNull.Value)
            {
                objEntity.IsForPublic = Convert.ToBoolean(dr["IsForPublic"]);
            }

            if (dr.Table.Columns.Contains("IsActive") && dr["IsActive"] != DBNull.Value)
            {
                objEntity.IsActive = Convert.ToBoolean(dr["IsActive"]);
            }
            if (dr.Table.Columns.Contains("IsDeleted") && dr["IsDeleted"] != DBNull.Value)
            {
                objEntity.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);
            }
            if (dr.Table.Columns.Contains("CreatedBy") && dr["CreatedBy"] != DBNull.Value)
            {
                objEntity.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
            }
            if (dr.Table.Columns.Contains("CreatedOn") && dr["CreatedOn"] != DBNull.Value)
            {
                objEntity.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
            }

            if (dr.Table.Columns.Contains("IndividualCommentLogGuid") && dr["IndividualCommentLogGuid"] != DBNull.Value)
            {
                objEntity.IndividualCommentLogGuid = dr["IndividualCommentLogGuid"].ToString();
            }

            return objEntity;

        }
    }
}
