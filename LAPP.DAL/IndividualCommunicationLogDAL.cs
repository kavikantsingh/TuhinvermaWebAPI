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


        public IndividualCommunicationLog Get_IndividualCommunicationLog_By_IndividualCommunicationLogId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualCommunicationLogId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualCommunicationLog_Get_By_CommunicationLogId", lstParameter.ToArray());
            IndividualCommunicationLog objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<IndividualCommunicationLog> Get_IndividualCommunicationLog_by_IndividualId(int IndividualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            //lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualCommunicationLog_Get_By_IndividualId", lstParameter.ToArray());
            List<IndividualCommunicationLog> lstEntity = new List<IndividualCommunicationLog>();
            IndividualCommunicationLog objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private IndividualCommunicationLog FetchEntity(DataRow dr)
        {
            IndividualCommunicationLog objEntity = new IndividualCommunicationLog();

            if (dr.Table.Columns.Contains("IndividualCommunicationLogId") && dr["IndividualCommunicationLogId"] != DBNull.Value)
            {
                objEntity.IndividualCommunicationLogId = Convert.ToInt32(dr["IndividualCommunicationLogId"]);
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
            if (dr.Table.Columns.Contains("CommunicationLogDate") && dr["CommunicationLogDate"] != DBNull.Value)
            {
                objEntity.CommunicationLogDate = Convert.ToDateTime(dr["CommunicationLogDate"]);
            }

            if (dr.Table.Columns.Contains("Type") && dr["Type"] != DBNull.Value)
            {
                objEntity.Type = Convert.ToString(dr["Type"]);
            }

            if (dr.Table.Columns.Contains("EmailFrom") && dr["EmailFrom"] != DBNull.Value)
            {
                objEntity.EmailFrom = Convert.ToString(dr["EmailFrom"]);
            }

            if (dr.Table.Columns.Contains("UserIdFrom") && dr["UserIdFrom"] != DBNull.Value)
            {
                objEntity.UserIdFrom = Convert.ToInt32(dr["UserIdFrom"]);
            }

            if (dr.Table.Columns.Contains("Subject") && dr["Subject"] != DBNull.Value)
            {
                objEntity.Subject = Convert.ToString(dr["Subject"]);
            }
            if (dr.Table.Columns.Contains("CommunicationSource") && dr["CommunicationSource"] != DBNull.Value)
            {
                objEntity.CommunicationSource = Convert.ToString(dr["CommunicationSource"]);
            }
            if (dr.Table.Columns.Contains("CommunicationText") && dr["CommunicationText"] != DBNull.Value)
            {
                objEntity.CommunicationText = Convert.ToString(dr["CommunicationText"]);
            }
            if (dr.Table.Columns.Contains("CommunicationStatus") && dr["CommunicationStatus"] != DBNull.Value)
            {
                objEntity.CommunicationStatus = Convert.ToString(dr["CommunicationStatus"]);
            }

            if (dr.Table.Columns.Contains("ReferenceNumber") && dr["ReferenceNumber"] != DBNull.Value)
            {
                objEntity.ReferenceNumber = Convert.ToString(dr["ReferenceNumber"]);
            }
            if (dr.Table.Columns.Contains("EmailTo") && dr["EmailTo"] != DBNull.Value)
            {
                objEntity.EmailTo = Convert.ToString(dr["EmailTo"]);
            }

            if (dr.Table.Columns.Contains("UserIdTo") && dr["UserIdTo"] != DBNull.Value)
            {
                objEntity.UserIdTo = Convert.ToInt32(dr["UserIdTo"]);
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

            if (dr.Table.Columns.Contains("IndividualCommunicationLogGuid") && dr["IndividualCommunicationLogGuid"] != DBNull.Value)
            {
                objEntity.IndividualCommunicationLogGuid = dr["IndividualCommunicationLogGuid"].ToString();
            }

            return objEntity;

        }
    }
}
