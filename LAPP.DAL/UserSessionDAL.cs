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
    public class UserSessionDAL : BaseDAL
    {
        public int Save_UserSession(UserSession objUserSession)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("TokenID", objUserSession.TokenID));
            lstParameter.Add(new MySqlParameter("UserID", objUserSession.UserID));
            lstParameter.Add(new MySqlParameter("AppDomainName", objUserSession.AppDomainName));
            lstParameter.Add(new MySqlParameter("BrowserUniqueID", objUserSession.BrowserUniqueID));
            lstParameter.Add(new MySqlParameter("RequestBrowsertypeVersion", objUserSession.RequestBrowsertypeVersion));
            lstParameter.Add(new MySqlParameter("SessionGUID", objUserSession.SessionGUID));
            lstParameter.Add(new MySqlParameter("UserHostIPAddress", objUserSession.UserHostIPAddress));
            lstParameter.Add(new MySqlParameter("Key", objUserSession.Key));

            lstParameter.Add(new MySqlParameter("ExpiredOn", objUserSession.ExpiredOn));
            lstParameter.Add(new MySqlParameter("Expired", objUserSession.Expired));
            lstParameter.Add(new MySqlParameter("IssuedOn", objUserSession.IssuedOn));


            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "usersession_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        public int Update_UserSession(UserSession objUserSession)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("U_TokenID", objUserSession.TokenID));
            lstParameter.Add(new MySqlParameter("U_UserID", objUserSession.UserID));
            lstParameter.Add(new MySqlParameter("U_AppDomainName", objUserSession.AppDomainName));
            lstParameter.Add(new MySqlParameter("U_BrowserUniqueID", objUserSession.BrowserUniqueID));
            lstParameter.Add(new MySqlParameter("U_Key", objUserSession.Key));
            lstParameter.Add(new MySqlParameter("U_RequestBrowsertypeVersion", objUserSession.RequestBrowsertypeVersion));
            lstParameter.Add(new MySqlParameter("U_SessionGUID", objUserSession.SessionGUID));
            lstParameter.Add(new MySqlParameter("U_UserHostIPAddress", objUserSession.UserHostIPAddress));

            lstParameter.Add(new MySqlParameter("U_ExpiredOn", objUserSession.ExpiredOn));
            lstParameter.Add(new MySqlParameter("U_Expired", objUserSession.Expired));
            lstParameter.Add(new MySqlParameter("U_IssuedOn", objUserSession.IssuedOn));

            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "usersession_update", lstParameter.ToArray());
            return returnValue;
        }

        public UserSession Get_UserSession_By_TokenID(Int64 TokenID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_TokenID", TokenID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "usersession_Get_ById", lstParameter.ToArray());

            UserSession objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);

            }
            return objEntity;
        }

        public List<UserSession> GetAll_UserSession()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "usersession_Get_All");
            List<UserSession> lstCountry = new List<UserSession>();
            UserSession objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                {
                    lstCountry.Add(objEntity);
                }
            }
            return lstCountry;
        }

        private UserSession FetchEntity(DataRow dr)
        {
            UserSession objEntity = new UserSession();

            if (dr.Table.Columns.Contains("TokenID") && dr["TokenID"] != DBNull.Value)
            {
                objEntity.TokenID = Convert.ToInt64(dr["TokenID"]);
            }
            if (dr.Table.Columns.Contains("UserID") && dr["UserID"] != DBNull.Value)
            {
                objEntity.UserID = Convert.ToInt32(dr["UserID"]);
            }
            if (dr.Table.Columns.Contains("AppDomainName") && dr["AppDomainName"] != DBNull.Value)
            {
                objEntity.AppDomainName = Convert.ToString(dr["AppDomainName"]);
            }
            if (dr.Table.Columns.Contains("BrowserUniqueID") && dr["BrowserUniqueID"] != DBNull.Value)
            {
                objEntity.BrowserUniqueID = Convert.ToString(dr["BrowserUniqueID"]);
            }
            if (dr.Table.Columns.Contains("Key") && dr["Key"] != DBNull.Value)
            {
                objEntity.Key = Convert.ToString(dr["Key"]);
            }

            if (dr.Table.Columns.Contains("RequestBrowsertypeVersion") && dr["RequestBrowsertypeVersion"] != DBNull.Value)
            {
                objEntity.RequestBrowsertypeVersion = Convert.ToString(dr["RequestBrowsertypeVersion"]);
            }

            if (dr.Table.Columns.Contains("SessionGUID") && dr["SessionGUID"] != DBNull.Value)
            {
                objEntity.SessionGUID = Convert.ToString(dr["SessionGUID"]);
            }

            if (dr.Table.Columns.Contains("UserHostIPAddress") && dr["UserHostIPAddress"] != DBNull.Value)
            {
                objEntity.UserHostIPAddress = Convert.ToString(dr["UserHostIPAddress"]);
            }

            if (dr.Table.Columns.Contains("Expired") && dr["Expired"] != DBNull.Value)
            {
                objEntity.Expired = Convert.ToBoolean(dr["Expired"]);
            }


            if (dr.Table.Columns.Contains("ExpiredOn") && dr["ExpiredOn"] != DBNull.Value)
            {
                objEntity.ExpiredOn = Convert.ToDateTime(dr["ExpiredOn"]);
            }


            if (dr.Table.Columns.Contains("IssuedOn") && dr["IssuedOn"] != DBNull.Value)
            {
                objEntity.IssuedOn = Convert.ToDateTime(dr["IssuedOn"]);
            }

            return objEntity;
        }
    }
}
