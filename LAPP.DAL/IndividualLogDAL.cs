using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using LAPP.ENTITY;
using MySql.Data.MySqlClient;
namespace LAPP.DAL
{
    public class IndividualLogDAL : BaseDAL
    {
        public int Save_IndividualLog(IndividualLog objIndividualLog)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualLogId", objIndividualLog.IndividualLogId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualLog.IndividualId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objIndividualLog.ApplicationId));
            lstParameter.Add(new MySqlParameter("MasterTransactionId", objIndividualLog.MasterTransactionId));
            lstParameter.Add(new MySqlParameter("PageModuleId", objIndividualLog.PageModuleId));
            lstParameter.Add(new MySqlParameter("PageModuleTabSubModuleId", objIndividualLog.PageModuleTabSubModuleId));
            lstParameter.Add(new MySqlParameter("PageTabSectionId", objIndividualLog.PageTabSectionId));
            lstParameter.Add(new MySqlParameter("EffectiveDate", objIndividualLog.EffectiveDate));
            lstParameter.Add(new MySqlParameter("EndDate", objIndividualLog.EndDate));
            lstParameter.Add(new MySqlParameter("LogSource", objIndividualLog.LogSource));
            lstParameter.Add(new MySqlParameter("LogText", objIndividualLog.LogText));
            lstParameter.Add(new MySqlParameter("ReferenceNumber", objIndividualLog.ReferenceNumber));

            lstParameter.Add(new MySqlParameter("IsActive", objIndividualLog.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualLog.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualLog.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objIndividualLog.CreatedOn));

            lstParameter.Add(new MySqlParameter("IndividualLogGuid", objIndividualLog.IndividualLogGuid));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "IndividualLog_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<IndividualLog> Get_All_IndividualLog()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualLog_GET_ALL");
            List<IndividualLog> lstEntity = new List<IndividualLog>();
            IndividualLog objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<IndividualLog> Get_IndividualLog_by_IndividualId(int IndividualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            //lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualLog_GET_BY_IndividualId", lstParameter.ToArray());
            List<IndividualLog> lstEntity = new List<IndividualLog>();
            IndividualLog objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public IndividualLog Get_IndividualLog_By_IndividualLogId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualLogId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualLog_GET_BY_IndividualLogId", lstParameter.ToArray());
            IndividualLog objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private IndividualLog FetchEntity(DataRow dr)
        {
            IndividualLog objEntity = new IndividualLog();

            if (dr.Table.Columns.Contains("IndividualLogId") && dr["IndividualLogId"] != DBNull.Value)
            {
                objEntity.IndividualLogId = Convert.ToInt32(dr["IndividualLogId"]);
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

            if (dr.Table.Columns.Contains("LogSource") && dr["LogSource"] != DBNull.Value)
            {
                objEntity.LogSource = Convert.ToString(dr["LogSource"]);
            }
            if (dr.Table.Columns.Contains("LogText") && dr["LogText"] != DBNull.Value)
            {
                objEntity.LogText = Convert.ToString(dr["LogText"]);
            }
            if (dr.Table.Columns.Contains("ReferenceNumber") && dr["ReferenceNumber"] != DBNull.Value)
            {
                objEntity.ReferenceNumber = Convert.ToString(dr["ReferenceNumber"]);
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

            if (dr.Table.Columns.Contains("IndividualLogGuid") && dr["IndividualLogGuid"] != DBNull.Value)
            {
                objEntity.IndividualLogGuid = dr["IndividualLogGuid"].ToString();
            }

            return objEntity;

        }
    }
}
