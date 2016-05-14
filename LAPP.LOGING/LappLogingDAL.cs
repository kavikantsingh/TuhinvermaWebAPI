using LAPP.DAL;
using LAPP.LOGING.ENTITY;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.LOGING.DAL
{
    class LappLogingDAL
    {
    }

    #region CategoryDAL

    public class CategoryDAL
    {
        public int Save_Category(Category objCategory)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("CategoryID", objCategory.CategoryID));
            lstParameter.Add(new MySqlParameter("CategoryName", objCategory.CategoryName));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "category_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        public int Update_Category(Category objCategory)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("U_CategoryID", objCategory.CategoryID));
            lstParameter.Add(new MySqlParameter("U_CategoryName", objCategory.CategoryName));
            //MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            //returnParam.Direction = ParameterDirection.ReturnValue;
            //lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "category_Update", lstParameter.ToArray());
            return returnValue;
        }

        public Category Category_Get_By_CategoryID(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_CategoryID", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "category_get_by_CategoryID", lstParameter.ToArray());
            Category objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public void Delete_Category_By_CategoryID(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("D_CategoryID", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "category_Delete_by_CategoryID", lstParameter.ToArray());
        }

        private Category FetchEntity(DataRow dr)
        {
            Category objEntity = new Category();

            if (dr.Table.Columns.Contains("CategoryID") && dr["CategoryID"] != DBNull.Value)
            {
                objEntity.CategoryID = Convert.ToInt32(dr["CategoryID"]);
            }
            if (dr.Table.Columns.Contains("CategoryName") && dr["CategoryName"] != DBNull.Value)
            {
                objEntity.CategoryName = Convert.ToString(dr["CategoryName"]);
            }

            return objEntity;
        }
    }
    #endregion

    #region CalegoryLogDAL

    public class CategoryLogDAL
    {
        public int Save_categorylog(CategoryLog objcategorylog)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("CategoryLogID", objcategorylog.CategoryLogID));
            lstParameter.Add(new MySqlParameter("LogID", objcategorylog.LogID));
            lstParameter.Add(new MySqlParameter("CategoryID", objcategorylog.CategoryID));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "categorylog_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        public int Update_CategoryLog(CategoryLog objCategoryLog)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("U_CategoryLogID", objCategoryLog.CategoryLogID));
            lstParameter.Add(new MySqlParameter("U_CategoryID", objCategoryLog.CategoryID));

            lstParameter.Add(new MySqlParameter("U_LogID", objCategoryLog.LogID));
            //MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            //returnParam.Direction = ParameterDirection.ReturnValue;
            //lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "categorylog_Update", lstParameter.ToArray());
            return returnValue;
        }

        public CategoryLog CategoryLog_Get_By_CategoryLogID(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_CategoryLogID", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "categorylog_get_by_CategoryLogID", lstParameter.ToArray());
            CategoryLog objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public void Delete_CategoryLog_By_CategoryLogID(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("D_CategoryLogID", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "categorylog_delete_by_CategoryLogID", lstParameter.ToArray());
        }

        private CategoryLog FetchEntity(DataRow dr)
        {
            CategoryLog objEntity = new CategoryLog();

            if (dr.Table.Columns.Contains("CategoryLogID") && dr["CategoryLogID"] != DBNull.Value)
            {
                objEntity.CategoryLogID = Convert.ToInt32(dr["CategoryLogID"]);
            }
            if (dr.Table.Columns.Contains("CategoryID") && dr["CategoryID"] != DBNull.Value)
            {
                objEntity.CategoryID = Convert.ToInt32(dr["CategoryID"]);
            }
            if (dr.Table.Columns.Contains("LogID") && dr["LogID"] != DBNull.Value)
            {
                objEntity.LogID = Convert.ToInt32(dr["LogID"]);
            }
            return objEntity;
        }

    }

    #endregion

    #region LogDAL

    public class LogDAL
    {
        public int Save_Log(Log objlog)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("LogID", objlog.LogID));
            lstParameter.Add(new MySqlParameter("AppDomainName", objlog.AppDomainName));
            lstParameter.Add(new MySqlParameter("Application", objlog.Application));
            lstParameter.Add(new MySqlParameter("CreatedOn", objlog.CreatedOn));
            lstParameter.Add(new MySqlParameter("ElapsedMs", objlog.ElapsedMs));
            lstParameter.Add(new MySqlParameter("EntityId", objlog.EntityId));
            lstParameter.Add(new MySqlParameter("EventID", objlog.EventID));
            lstParameter.Add(new MySqlParameter("Exception", objlog.Exception));
            lstParameter.Add(new MySqlParameter("FormattedMessage", objlog.FormattedMessage));
            lstParameter.Add(new MySqlParameter("IndividualId", objlog.IndividualId));
            lstParameter.Add(new MySqlParameter("Source", objlog.Source));
            lstParameter.Add(new MySqlParameter("IsDebug", objlog.IsDebug));
            lstParameter.Add(new MySqlParameter("MachineName", objlog.MachineName));
            lstParameter.Add(new MySqlParameter("Message", objlog.Message));
            lstParameter.Add(new MySqlParameter("Priority", objlog.Priority));
            lstParameter.Add(new MySqlParameter("ProcessID", objlog.ProcessID));
            lstParameter.Add(new MySqlParameter("ProcessName", objlog.ProcessName));
            lstParameter.Add(new MySqlParameter("RequestBrowserTypeVersion", objlog.RequestBrowserTypeVersion));
            lstParameter.Add(new MySqlParameter("RequestUrl", objlog.RequestUrl));
            lstParameter.Add(new MySqlParameter("RequestUrlReferrer", objlog.RequestUrlReferrer));
            lstParameter.Add(new MySqlParameter("SessionId", objlog.SessionId));
            lstParameter.Add(new MySqlParameter("Severity", objlog.Severity));
            lstParameter.Add(new MySqlParameter("StackTrace", objlog.StackTrace));
            lstParameter.Add(new MySqlParameter("ThreadName", objlog.ThreadName));
            lstParameter.Add(new MySqlParameter("Timestamp", objlog.Timestamp));
            lstParameter.Add(new MySqlParameter("Title", objlog.Title));
            lstParameter.Add(new MySqlParameter("UserAgent", objlog.UserAgent));
            lstParameter.Add(new MySqlParameter("UserHostAddress", objlog.UserHostAddress));
            lstParameter.Add(new MySqlParameter("UserHostName", objlog.UserHostName));
            lstParameter.Add(new MySqlParameter("UserId", objlog.UserId));
            lstParameter.Add(new MySqlParameter("UserName", objlog.UserName));
            lstParameter.Add(new MySqlParameter("Win32ThreadId", objlog.Win32ThreadId));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "log_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        public int Update_Log(Log objlog)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("U_LogID", objlog.LogID));
            lstParameter.Add(new MySqlParameter("U_AppDomainName", objlog.AppDomainName));
            lstParameter.Add(new MySqlParameter("U_Application", objlog.Application));
            lstParameter.Add(new MySqlParameter("U_CreatedOn", objlog.CreatedOn));
            lstParameter.Add(new MySqlParameter("U_ElapsedMs", objlog.ElapsedMs));
            lstParameter.Add(new MySqlParameter("U_EntityId", objlog.EntityId));
            lstParameter.Add(new MySqlParameter("U_Source", objlog.Source));
            lstParameter.Add(new MySqlParameter("U_EventID", objlog.EventID));
            lstParameter.Add(new MySqlParameter("U_Exception", objlog.Exception));
            lstParameter.Add(new MySqlParameter("U_FormattedMessage", objlog.FormattedMessage));
            lstParameter.Add(new MySqlParameter("U_IndividualId", objlog.IndividualId));
            lstParameter.Add(new MySqlParameter("U_IsDebug", objlog.IsDebug));
            lstParameter.Add(new MySqlParameter("U_MachineName", objlog.MachineName));
            lstParameter.Add(new MySqlParameter("U_Message", objlog.Message));
            lstParameter.Add(new MySqlParameter("U_Priority", objlog.Priority));
            lstParameter.Add(new MySqlParameter("U_ProcessID", objlog.ProcessID));
            lstParameter.Add(new MySqlParameter("U_ProcessName", objlog.ProcessName));
            lstParameter.Add(new MySqlParameter("U_RequestBrowserTypeVersion", objlog.RequestBrowserTypeVersion));
            lstParameter.Add(new MySqlParameter("U_RequestUrl", objlog.RequestUrl));
            lstParameter.Add(new MySqlParameter("U_RequestUrlReferrer", objlog.RequestUrlReferrer));
            lstParameter.Add(new MySqlParameter("U_SessionId", objlog.SessionId));
            lstParameter.Add(new MySqlParameter("U_Severity", objlog.Severity));
            lstParameter.Add(new MySqlParameter("U_StackTrace", objlog.StackTrace));
            lstParameter.Add(new MySqlParameter("U_ThreadName", objlog.ThreadName));
            lstParameter.Add(new MySqlParameter("U_Timestamp", objlog.Timestamp));
            lstParameter.Add(new MySqlParameter("U_Title", objlog.Title));
            lstParameter.Add(new MySqlParameter("U_UserAgent", objlog.UserAgent));
            lstParameter.Add(new MySqlParameter("U_UserHostAddress", objlog.UserHostAddress));
            lstParameter.Add(new MySqlParameter("U_UserHostName", objlog.UserHostName));
            lstParameter.Add(new MySqlParameter("U_UserId", objlog.UserId));
            lstParameter.Add(new MySqlParameter("U_UserName", objlog.UserName));
            lstParameter.Add(new MySqlParameter("U_Win32ThreadId", objlog.Win32ThreadId));

            //MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            //returnParam.Direction = ParameterDirection.ReturnValue;
            //lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "log_Update", lstParameter.ToArray());
            return returnValue;
        }

        public Log Log_Get_By_LogID(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_LogID", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "log_get_by_LogID", lstParameter.ToArray());
            Log objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public void Delete_Log_By_LogID(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("D_LogID", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "log_delete_by_LogID", lstParameter.ToArray());
        }

        private Log FetchEntity(DataRow dr)
        {
            Log objEntity = new Log();

            if (dr.Table.Columns.Contains("LogID") && dr["LogID"] != DBNull.Value)
            {
                objEntity.LogID = Convert.ToInt32(dr["LogID"]);
            }
            if (dr.Table.Columns.Contains("Application") && dr["Application"] != DBNull.Value)
            {
                objEntity.Application = Convert.ToString(dr["Application"]);
            }
            if (dr.Table.Columns.Contains("AppDomainName") && dr["AppDomainName"] != DBNull.Value)
            {
                objEntity.AppDomainName = Convert.ToString(dr["AppDomainName"]);
            }

            if (dr.Table.Columns.Contains("CreatedOn") && dr["CreatedOn"] != DBNull.Value)
            {
                objEntity.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
            }

            if (dr.Table.Columns.Contains("ElapsedMs") && dr["ElapsedMs"] != DBNull.Value)
            {
                objEntity.ElapsedMs = Convert.ToInt32(dr["ElapsedMs"]);
            }
            if (dr.Table.Columns.Contains("EntityId") && dr["EntityId"] != DBNull.Value)
            {
                objEntity.EntityId = Convert.ToInt32(dr["EntityId"]);
            }

            if (dr.Table.Columns.Contains("Source") && dr["Source"] != DBNull.Value)
            {
                objEntity.Source = Convert.ToString(dr["Source"]);
            }
            if (dr.Table.Columns.Contains("EventID") && dr["EventID"] != DBNull.Value)
            {
                objEntity.EventID = Convert.ToInt32(dr["EventID"]);
            }
            if (dr.Table.Columns.Contains("Exception") && dr["Exception"] != DBNull.Value)
            {
                objEntity.Exception = Convert.ToString(dr["Exception"]);
            }
            if (dr.Table.Columns.Contains("FormattedMessage") && dr["FormattedMessage"] != DBNull.Value)
            {
                objEntity.FormattedMessage = Convert.ToString(dr["FormattedMessage"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }


            if (dr.Table.Columns.Contains("IsDebug") && dr["IsDebug"] != DBNull.Value)
            {
                objEntity.IsDebug = Convert.ToBoolean(dr["IsDebug"]);
            }
            if (dr.Table.Columns.Contains("MachineName") && dr["MachineName"] != DBNull.Value)
            {
                objEntity.MachineName = Convert.ToString(dr["MachineName"]);
            }
            if (dr.Table.Columns.Contains("Message") && dr["Message"] != DBNull.Value)
            {
                objEntity.Message = Convert.ToString(dr["Message"]);
            }
            if (dr.Table.Columns.Contains("Priority") && dr["Priority"] != DBNull.Value)
            {
                objEntity.Priority = Convert.ToInt32(dr["Priority"]);
            }
            if (dr.Table.Columns.Contains("ProcessID") && dr["ProcessID"] != DBNull.Value)
            {
                objEntity.ProcessID = Convert.ToString(dr["ProcessID"]);
            }
            if (dr.Table.Columns.Contains("ProcessName") && dr["ProcessName"] != DBNull.Value)
            {
                objEntity.ProcessName = Convert.ToString(dr["ProcessName"]);
            }
            if (dr.Table.Columns.Contains("RequestBrowserTypeVersion") && dr["RequestBrowserTypeVersion"] != DBNull.Value)
            {
                objEntity.RequestBrowserTypeVersion = Convert.ToString(dr["RequestBrowserTypeVersion"]);
            }
            if (dr.Table.Columns.Contains("RequestUrl") && dr["RequestUrl"] != DBNull.Value)
            {
                objEntity.RequestUrl = Convert.ToString(dr["RequestUrl"]);
            }
            if (dr.Table.Columns.Contains("RequestUrlReferrer") && dr["RequestUrlReferrer"] != DBNull.Value)
            {
                objEntity.RequestUrlReferrer = Convert.ToString(dr["RequestUrlReferrer"]);
            }

            if (dr.Table.Columns.Contains("SessionId") && dr["SessionId"] != DBNull.Value)
            {
                objEntity.SessionId = Convert.ToString(dr["SessionId"]);
            }
            if (dr.Table.Columns.Contains("Severity") && dr["Severity"] != DBNull.Value)
            {
                objEntity.Severity = Convert.ToString(dr["Severity"]);
            }
            if (dr.Table.Columns.Contains("StackTrace") && dr["StackTrace"] != DBNull.Value)
            {
                objEntity.StackTrace = Convert.ToString(dr["StackTrace"]);
            }
            if (dr.Table.Columns.Contains("ThreadName") && dr["ThreadName"] != DBNull.Value)
            {
                objEntity.ThreadName = Convert.ToString(dr["ThreadName"]);
            }
            if (dr.Table.Columns.Contains("Timestamp") && dr["Timestamp"] != DBNull.Value)
            {
                objEntity.Timestamp = Convert.ToDateTime(dr["Timestamp"]);
            }
            if (dr.Table.Columns.Contains("Title") && dr["Title"] != DBNull.Value)
            {
                objEntity.Title = Convert.ToString(dr["Title"]);
            }
            if (dr.Table.Columns.Contains("UserAgent") && dr["UserAgent"] != DBNull.Value)
            {
                objEntity.UserAgent = Convert.ToString(dr["UserAgent"]);
            }
            if (dr.Table.Columns.Contains("UserHostAddress") && dr["UserHostAddress"] != DBNull.Value)
            {
                objEntity.UserHostAddress = Convert.ToString(dr["UserHostAddress"]);
            }
            if (dr.Table.Columns.Contains("UserHostName") && dr["UserHostName"] != DBNull.Value)
            {
                objEntity.UserHostName = Convert.ToString(dr["UserHostName"]);
            }
            if (dr.Table.Columns.Contains("UserId") && dr["UserId"] != DBNull.Value)
            {
                objEntity.UserId = Convert.ToInt32(dr["UserId"]);
            }

            if (dr.Table.Columns.Contains("UserName") && dr["UserName"] != DBNull.Value)
            {
                objEntity.UserName = Convert.ToString(dr["UserName"]);
            }
            if (dr.Table.Columns.Contains("Win32ThreadId") && dr["Win32ThreadId"] != DBNull.Value)
            {
                objEntity.Win32ThreadId = Convert.ToString(dr["Win32ThreadId"]);
            }

            return objEntity;
        }
    }


    #endregion

    #region DataLogDAL

    public class DatalogDAL
    {
        public int Save_Datalog(Datalog objDatalog)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("DataLogId", objDatalog.DataLogId));
            lstParameter.Add(new MySqlParameter("Action", objDatalog.Action));
            lstParameter.Add(new MySqlParameter("AfterValue", objDatalog.AfterValue));
            lstParameter.Add(new MySqlParameter("BeforeValue", objDatalog.BeforeValue));
            lstParameter.Add(new MySqlParameter("ColumnName", objDatalog.ColumnName));
            lstParameter.Add(new MySqlParameter("IsSystem", objDatalog.IsSystem));
            lstParameter.Add(new MySqlParameter("LogDateTime", objDatalog.LogDateTime));
            lstParameter.Add(new MySqlParameter("RowIdValue", objDatalog.RowIdValue));
            lstParameter.Add(new MySqlParameter("TableName", objDatalog.TableName));
            lstParameter.Add(new MySqlParameter("UserName", objDatalog.UserName));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "datalog_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        public int Update_Datalog(Datalog objDatalog)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("U_DataLogId", objDatalog.DataLogId));
            lstParameter.Add(new MySqlParameter("U_Action", objDatalog.Action));
            lstParameter.Add(new MySqlParameter("U_AfterValue", objDatalog.AfterValue));
            lstParameter.Add(new MySqlParameter("U_BeforeValue", objDatalog.BeforeValue));
            lstParameter.Add(new MySqlParameter("U_ColumnName", objDatalog.ColumnName));
            lstParameter.Add(new MySqlParameter("U_IsSystem", objDatalog.IsSystem));
            lstParameter.Add(new MySqlParameter("U_LogDateTime", objDatalog.LogDateTime));
            lstParameter.Add(new MySqlParameter("U_RowIdValue", objDatalog.RowIdValue));
            lstParameter.Add(new MySqlParameter("U_TableName", objDatalog.TableName));
            lstParameter.Add(new MySqlParameter("U_UserName", objDatalog.UserName));

            ////MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            ////returnParam.Direction = ParameterDirection.ReturnValue;
            ////lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "datalog_Update",  lstParameter.ToArray());
            return returnValue;
        }

        public Datalog Datalog_Get_By_DatalogID(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_DataLogId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "datalog_get_by_DataLogID", lstParameter.ToArray());
            Datalog objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public void Delete_DataLog_By_DataLogID(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("D_DataLogId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "datalog_delete_by_DataLogID", lstParameter.ToArray());
        }

        private Datalog FetchEntity(DataRow dr)
        {
            Datalog objEntity = new Datalog();

            if (dr.Table.Columns.Contains("DataLogId") && dr["DataLogId"] != DBNull.Value)
            {
                objEntity.DataLogId = Convert.ToInt32(dr["DataLogId"]);
            }
            if (dr.Table.Columns.Contains("Action") && dr["Action"] != DBNull.Value)
            {
                objEntity.Action = Convert.ToString(dr["Action"]);
            }
            if (dr.Table.Columns.Contains("AfterValue") && dr["AfterValue"] != DBNull.Value)
            {
                objEntity.AfterValue = Convert.ToString(dr["AfterValue"]);
            }

            if (dr.Table.Columns.Contains("BeforeValue") && dr["BeforeValue"] != DBNull.Value)
            {
                objEntity.BeforeValue = Convert.ToString(dr["BeforeValue"]);
            }
            if (dr.Table.Columns.Contains("ColumnName") && dr["ColumnName"] != DBNull.Value)
            {
                objEntity.ColumnName = Convert.ToString(dr["ColumnName"]);
            }

            if (dr.Table.Columns.Contains("IsSystem") && dr["IsSystem"] != DBNull.Value)
            {
                objEntity.IsSystem = Convert.ToBoolean(dr["IsSystem"]);
            }
            if (dr.Table.Columns.Contains("LogDateTime") && dr["LogDateTime"] != DBNull.Value)
            {
                objEntity.LogDateTime = Convert.ToDateTime(dr["LogDateTime"]);
            }
            if (dr.Table.Columns.Contains("RowIdValue") && dr["RowIdValue"] != DBNull.Value)
            {
                objEntity.RowIdValue = Convert.ToInt32(dr["RowIdValue"]);
            }
            if (dr.Table.Columns.Contains("TableName") && dr["TableName"] != DBNull.Value)
            {
                objEntity.TableName = Convert.ToString(dr["TableName"]);
            }
            if (dr.Table.Columns.Contains("UserName") && dr["UserName"] != DBNull.Value)
            {
                objEntity.UserName = Convert.ToString(dr["UserName"]);
            }

            return objEntity;
        }
    }



    #endregion

    #region AuditVisitInfoDAL

    public class AuditVisitInfoDAL
    {
        public int Save_AuditvisitInfo(AuditvisitInfo objAuditvisitInfo)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("ID", objAuditvisitInfo.ID));
            lstParameter.Add(new MySqlParameter("AppDomainName", objAuditvisitInfo.AppDomainName));
            lstParameter.Add(new MySqlParameter("DeviceId", objAuditvisitInfo.DeviceId));
            lstParameter.Add(new MySqlParameter("EntityId", objAuditvisitInfo.EntityId));
            lstParameter.Add(new MySqlParameter("HostIPAddress", objAuditvisitInfo.HostIPAddress));
            lstParameter.Add(new MySqlParameter("IndividualId", objAuditvisitInfo.IndividualId));
            lstParameter.Add(new MySqlParameter("RequestBrowserTypeVersion", objAuditvisitInfo.RequestBrowserTypeVersion));
            lstParameter.Add(new MySqlParameter("RequestUrl", objAuditvisitInfo.RequestUrl));
            lstParameter.Add(new MySqlParameter("RequestUrlReferrer", objAuditvisitInfo.RequestUrlReferrer));
            lstParameter.Add(new MySqlParameter("UserHostAddress", objAuditvisitInfo.UserHostAddress));
            lstParameter.Add(new MySqlParameter("UserHostName", objAuditvisitInfo.UserHostName));
            lstParameter.Add(new MySqlParameter("UserId", objAuditvisitInfo.UserId));
            lstParameter.Add(new MySqlParameter("UserName", objAuditvisitInfo.UserName));
            lstParameter.Add(new MySqlParameter("IsActiveXControlEnabled", objAuditvisitInfo.IsActiveXControlEnabled));
            lstParameter.Add(new MySqlParameter("IsCookieEnabled", objAuditvisitInfo.IsCookieEnabled));
            lstParameter.Add(new MySqlParameter("IsCrawler", objAuditvisitInfo.IsCrawler));
            lstParameter.Add(new MySqlParameter("IsJavascriptEnabled", objAuditvisitInfo.IsJavascriptEnabled));
            lstParameter.Add(new MySqlParameter("MachineDeviceName", objAuditvisitInfo.MachineDeviceName));
            lstParameter.Add(new MySqlParameter("PageName", objAuditvisitInfo.PageName));
            lstParameter.Add(new MySqlParameter("Platform", objAuditvisitInfo.Platform));
            lstParameter.Add(new MySqlParameter("SessionID", objAuditvisitInfo.SessionID));
            lstParameter.Add(new MySqlParameter("TimeStamp", objAuditvisitInfo.TimeStamp));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "AuditvisitInfo_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        public int Update_AuditvisitInfo(AuditvisitInfo objAuditvisitInfo)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("U_ID", objAuditvisitInfo.ID));
            lstParameter.Add(new MySqlParameter("U_AppDomainName", objAuditvisitInfo.AppDomainName));
            lstParameter.Add(new MySqlParameter("U_DeviceId", objAuditvisitInfo.DeviceId));
            lstParameter.Add(new MySqlParameter("U_EntityId", objAuditvisitInfo.EntityId));
            lstParameter.Add(new MySqlParameter("U_HostIPAddress", objAuditvisitInfo.HostIPAddress));
            lstParameter.Add(new MySqlParameter("U_IndividualId", objAuditvisitInfo.IndividualId));
            lstParameter.Add(new MySqlParameter("U_RequestBrowserTypeVersion", objAuditvisitInfo.RequestBrowserTypeVersion));
            lstParameter.Add(new MySqlParameter("U_RequestUrl", objAuditvisitInfo.RequestUrl));
            lstParameter.Add(new MySqlParameter("U_RequestUrlReferrer", objAuditvisitInfo.RequestUrlReferrer));
            lstParameter.Add(new MySqlParameter("U_UserHostAddress", objAuditvisitInfo.UserHostAddress));
            lstParameter.Add(new MySqlParameter("U_UserHostName", objAuditvisitInfo.UserHostName));
            lstParameter.Add(new MySqlParameter("U_UserId", objAuditvisitInfo.UserId));
            lstParameter.Add(new MySqlParameter("U_UserName", objAuditvisitInfo.UserName));
            lstParameter.Add(new MySqlParameter("U_IsActiveXControlEnabled", objAuditvisitInfo.IsActiveXControlEnabled));
            lstParameter.Add(new MySqlParameter("U_IsCookieEnabled", objAuditvisitInfo.IsCookieEnabled));
            lstParameter.Add(new MySqlParameter("U_IsCrawler", objAuditvisitInfo.IsCrawler));
            lstParameter.Add(new MySqlParameter("U_IsJavascriptEnabled", objAuditvisitInfo.IsJavascriptEnabled));
            lstParameter.Add(new MySqlParameter("U_MachineDeviceName", objAuditvisitInfo.MachineDeviceName));
            lstParameter.Add(new MySqlParameter("U_PageName", objAuditvisitInfo.PageName));
            lstParameter.Add(new MySqlParameter("U_Platform", objAuditvisitInfo.Platform));
            lstParameter.Add(new MySqlParameter("U_SessionID", objAuditvisitInfo.SessionID));
            lstParameter.Add(new MySqlParameter("U_TimeStamp", objAuditvisitInfo.TimeStamp));

            //MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            //returnParam.Direction = ParameterDirection.ReturnValue;
            //lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "auditvisitinfo_Update", lstParameter.ToArray());
            return returnValue;
        }

        public AuditvisitInfo AuditvisitInfo_Get_By_ID(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_ID", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "auditvisitinfo_get_by_ID", lstParameter.ToArray());
            AuditvisitInfo objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public void Delete_AuditvisitInfo_By_ID(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("D_ID", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "auditvisitinfo_Delete_by_ID", lstParameter.ToArray());
        }

        private AuditvisitInfo FetchEntity(DataRow dr)
        {
            AuditvisitInfo objEntity = new AuditvisitInfo();

            if (dr.Table.Columns.Contains("ID") && dr["ID"] != DBNull.Value)
            {
                objEntity.ID = Convert.ToInt32(dr["ID"]);
            }
            if (dr.Table.Columns.Contains("AppDomainName") && dr["AppDomainName"] != DBNull.Value)
            {
                objEntity.AppDomainName = Convert.ToString(dr["AppDomainName"]);
            }

            if (dr.Table.Columns.Contains("DeviceId") && dr["DeviceId"] != DBNull.Value)
            {
                objEntity.DeviceId = Convert.ToString(dr["DeviceId"]);
            }

            if (dr.Table.Columns.Contains("EntityId") && dr["EntityId"] != DBNull.Value)
            {
                objEntity.EntityId = Convert.ToInt32(dr["EntityId"]);
            }

            if (dr.Table.Columns.Contains("HostIPAddress") && dr["HostIPAddress"] != DBNull.Value)
            {
                objEntity.HostIPAddress = Convert.ToString(dr["HostIPAddress"]);
            }

            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }

            if (dr.Table.Columns.Contains("IsActiveXControlEnabled") && dr["IsActiveXControlEnabled"] != DBNull.Value)
            {
                objEntity.IsActiveXControlEnabled = Convert.ToBoolean(dr["IsActiveXControlEnabled"]);
            }
            if (dr.Table.Columns.Contains("IsCookieEnabled") && dr["IsCookieEnabled"] != DBNull.Value)
            {
                objEntity.IsCookieEnabled = Convert.ToBoolean(dr["IsCookieEnabled"]);
            }
            if (dr.Table.Columns.Contains("IsCrawler") && dr["IsCrawler"] != DBNull.Value)
            {
                objEntity.IsCrawler = Convert.ToBoolean(dr["IsCrawler"]);
            }
            if (dr.Table.Columns.Contains("IsJavascriptEnabled") && dr["IsJavascriptEnabled"] != DBNull.Value)
            {
                objEntity.IsJavascriptEnabled = Convert.ToBoolean(dr["IsJavascriptEnabled"]);
            }

            if (dr.Table.Columns.Contains("MachineDeviceName") && dr["MachineDeviceName"] != DBNull.Value)
            {
                objEntity.MachineDeviceName = Convert.ToString(dr["MachineDeviceName"]);
            }
            if (dr.Table.Columns.Contains("PageName") && dr["PageName"] != DBNull.Value)
            {
                objEntity.PageName = Convert.ToString(dr["PageName"]);
            }

            if (dr.Table.Columns.Contains("Platform") && dr["Platform"] != DBNull.Value)
            {
                objEntity.Platform = Convert.ToString(dr["Platform"]);
            }
            if (dr.Table.Columns.Contains("RequestBrowserTypeVersion") && dr["RequestBrowserTypeVersion"] != DBNull.Value)
            {
                objEntity.RequestBrowserTypeVersion = Convert.ToString(dr["RequestBrowserTypeVersion"]);
            }
            if (dr.Table.Columns.Contains("RequestUrl") && dr["RequestUrl"] != DBNull.Value)
            {
                objEntity.RequestUrl = Convert.ToString(dr["RequestUrl"]);
            }
            if (dr.Table.Columns.Contains("RequestUrlReferrer") && dr["RequestUrlReferrer"] != DBNull.Value)
            {
                objEntity.RequestUrlReferrer = Convert.ToString(dr["RequestUrlReferrer"]);
            }

            if (dr.Table.Columns.Contains("SessionID") && dr["SessionID"] != DBNull.Value)
            {
                objEntity.SessionID = Convert.ToString(dr["SessionID"]);
            }
            if (dr.Table.Columns.Contains("TimeStamp") && dr["TimeStamp"] != DBNull.Value)
            {
                objEntity.TimeStamp = Convert.ToDateTime(dr["TimeStamp"]);
            }

            if (dr.Table.Columns.Contains("UserHostAddress") && dr["UserHostAddress"] != DBNull.Value)
            {
                objEntity.UserHostAddress = Convert.ToString(dr["UserHostAddress"]);
            }
            if (dr.Table.Columns.Contains("UserHostName") && dr["UserHostName"] != DBNull.Value)
            {
                objEntity.UserHostName = Convert.ToString(dr["UserHostName"]);
            }
            if (dr.Table.Columns.Contains("UserId") && dr["UserId"] != DBNull.Value)
            {
                objEntity.UserId = Convert.ToInt32(dr["UserId"]);
            }

            if (dr.Table.Columns.Contains("UserName") && dr["UserName"] != DBNull.Value)
            {
                objEntity.UserName = Convert.ToString(dr["UserName"]);
            }


            return objEntity;
        }
    }

    #endregion

}
