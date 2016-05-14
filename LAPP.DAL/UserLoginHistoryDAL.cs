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
    public class UserLoginHistoryDAL : BaseDAL
    {
        public int Save_UserLoginHistory(UserLoginHistory objUserLoginHistory)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("UserId", objUserLoginHistory.UserId));
            lstParameter.Add(new MySqlParameter("IndividualId", objUserLoginHistory.IndividualId));
            lstParameter.Add(new MySqlParameter("UserName", objUserLoginHistory.UserName));
            lstParameter.Add(new MySqlParameter("Email", objUserLoginHistory.Email));
            lstParameter.Add(new MySqlParameter("PasswordChangedOn", objUserLoginHistory.PasswordChangedOn));
            lstParameter.Add(new MySqlParameter("LoginDate", objUserLoginHistory.LoginDate));
            lstParameter.Add(new MySqlParameter("LogoutDate", objUserLoginHistory.LogoutDate));
            lstParameter.Add(new MySqlParameter("LoginIp", objUserLoginHistory.LoginIp));
            lstParameter.Add(new MySqlParameter("MachineName", objUserLoginHistory.MachineName));
            lstParameter.Add(new MySqlParameter("UserAgent", objUserLoginHistory.UserAgent));
            lstParameter.Add(new MySqlParameter("UserHostAddress", objUserLoginHistory.UserHostAddress));
            lstParameter.Add(new MySqlParameter("UserHostName", objUserLoginHistory.UserHostName));
            lstParameter.Add(new MySqlParameter("UserLoginHistoryGuid", objUserLoginHistory.UserLoginHistoryGuid));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "UserLoginHistory_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        //public int Update_UserLoginHistory(UserLoginHistory objUserLoginHistory)
        //{
        //    DBHelper objDB = new DBHelper();
        //    List<MySqlParameter> lstParameter = new List<MySqlParameter>();

        //    lstParameter.Add(new MySqlParameter("U_UserId", objUserLoginHistory.UserId));
        //    lstParameter.Add(new MySqlParameter("U_IndividualId", objUserLoginHistory.IndividualId));

        //    lstParameter.Add(new MySqlParameter("U_UserName", objUserLoginHistory.UserName));
        //    lstParameter.Add(new MySqlParameter("U_Email", objUserLoginHistory.Email));
        //    lstParameter.Add(new MySqlParameter("U_PasswordChangedOn", objUserLoginHistory.PasswordChangedOn));
        //    lstParameter.Add(new MySqlParameter("U_LoginDate", objUserLoginHistory.LoginDate));
        //    lstParameter.Add(new MySqlParameter("U_LogoutDate", objUserLoginHistory.LogoutDate));

        //    lstParameter.Add(new MySqlParameter("U_LoginIp", objUserLoginHistory.LoginIp));
        //    lstParameter.Add(new MySqlParameter("U_MachineName", objUserLoginHistory.MachineName));
        //    lstParameter.Add(new MySqlParameter("U_UserAgent", objUserLoginHistory.UserAgent));
        //    lstParameter.Add(new MySqlParameter("U_UserHostAddress", objUserLoginHistory.UserHostAddress));
        //    lstParameter.Add(new MySqlParameter("U_UserHostName", objUserLoginHistory.UserHostName));
        //    lstParameter.Add(new MySqlParameter("U_UserLoginHistoryGuid", objUserLoginHistory.UserLoginHistoryGuid));

        //    int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "UserLoginHistory_Update", lstParameter.ToArray());
        //    return returnValue;
        //}

        public UserLoginHistory Get_UserLoginHistory_byUserId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_UserId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "UserLoginHistory_Get_BY_UserId", lstParameter.ToArray());
            UserLoginHistory objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<UserLoginHistory> Get_All_UserLoginHistory()
        {

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "UserLoginHistory_Get_All");
            List<UserLoginHistory> lstEntity = new List<UserLoginHistory>();
            UserLoginHistory objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private UserLoginHistory FetchEntity(DataRow dr)
        {
            UserLoginHistory objEntity = new UserLoginHistory();

            if (dr.Table.Columns.Contains("UserId") && dr["UserId"] != DBNull.Value)
            {
                objEntity.UserId = Convert.ToInt32(dr["UserId"]);
            }

            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }

            if (dr.Table.Columns.Contains("UserName") && dr["UserName"] != DBNull.Value)
            {
                objEntity.UserName = Convert.ToString(dr["UserName"]);
            }

            if (dr.Table.Columns.Contains("Email") && dr["Email"] != DBNull.Value)
            {
                objEntity.Email = Convert.ToString(dr["Email"]);
            }

            if (dr.Table.Columns.Contains("PasswordChangedOn") && dr["PasswordChangedOn"] != DBNull.Value)
            {
                objEntity.PasswordChangedOn = Convert.ToDateTime(dr["PasswordChangedOn"]);
            }

            if (dr.Table.Columns.Contains("LoginDate") && dr["LoginDate"] != DBNull.Value)
            {
                objEntity.LoginDate = Convert.ToDateTime(dr["LoginDate"]);
            }

            if (dr.Table.Columns.Contains("LogoutDate") && dr["LogoutDate"] != DBNull.Value)
            {
                objEntity.LogoutDate = Convert.ToDateTime(dr["LogoutDate"]);
            }



            if (dr.Table.Columns.Contains("LoginIp") && dr["LoginIp"] != DBNull.Value)
            {
                objEntity.LoginIp = Convert.ToString(dr["LoginIp"]);
            }

            if (dr.Table.Columns.Contains("MachineName") && dr["MachineName"] != DBNull.Value)
            {
                objEntity.MachineName = Convert.ToString(dr["MachineName"]);
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
            if (dr.Table.Columns.Contains("UserLoginHistoryGuid") && dr["UserLoginHistoryGuid"] != DBNull.Value)
            {
                objEntity.UserLoginHistoryGuid = Convert.ToString(dr["UserLoginHistoryGuid"]);
            }

            return objEntity;
        }


    }
}
