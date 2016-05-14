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
    public class UserSecurityQuestionDAL : BaseDAL
    {
        public int Save_UserSecurityQuestion(UserSecurityQuestion objUserSecurityQuestion)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("UserSecurityQuestionId", objUserSecurityQuestion.UserSecurityQuestionId));
            lstParameter.Add(new MySqlParameter("UserId", objUserSecurityQuestion.UserId));
            lstParameter.Add(new MySqlParameter("UserName", objUserSecurityQuestion.UserName));
            lstParameter.Add(new MySqlParameter("SecurityQuestionId", objUserSecurityQuestion.SecurityQuestionId));
            lstParameter.Add(new MySqlParameter("SecurityAnswerHash", objUserSecurityQuestion.SecurityAnswerHash));
            lstParameter.Add(new MySqlParameter("SecurityAnswerSalt", objUserSecurityQuestion.SecurityAnswerSalt));


            lstParameter.Add(new MySqlParameter("IsActive", objUserSecurityQuestion.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objUserSecurityQuestion.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objUserSecurityQuestion.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objUserSecurityQuestion.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objUserSecurityQuestion.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objUserSecurityQuestion.ModifiedOn));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "UserSecurityQuestion_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        //public int Update_UserSecurityQuestion(UserSecurityQuestion objUserSecurityQuestion)
        //{
        //    DBHelper objDB = new DBHelper();
        //    List<MySqlParameter> lstParameter = new List<MySqlParameter>();

        //    lstParameter.Add(new MySqlParameter("U_UserSecurityQuestionId", objUserSecurityQuestion.UserSecurityQuestionId));
        //    lstParameter.Add(new MySqlParameter("U_UserId", objUserSecurityQuestion.UserId));
        //    lstParameter.Add(new MySqlParameter("U_SecurityQuestionId", objUserSecurityQuestion.SecurityQuestionId));
        //    lstParameter.Add(new MySqlParameter("U_UserName", objUserSecurityQuestion.UserName));
        //    lstParameter.Add(new MySqlParameter("U_SecurityAnswerHash", objUserSecurityQuestion.SecurityAnswerHash));
        //    lstParameter.Add(new MySqlParameter("U_SecurityAnswerSalt", objUserSecurityQuestion.SecurityAnswerSalt));

        //    lstParameter.Add(new MySqlParameter("U_IsActive", objUserSecurityQuestion.IsActive));
        //    lstParameter.Add(new MySqlParameter("U_IsDeleted", objUserSecurityQuestion.IsDeleted));
        //    lstParameter.Add(new MySqlParameter("U_CreatedBy", objUserSecurityQuestion.CreatedBy));
        //    lstParameter.Add(new MySqlParameter("U_CreatedOn", objUserSecurityQuestion.CreatedOn));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedBy", objUserSecurityQuestion.ModifiedBy));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedOn", objUserSecurityQuestion.ModifiedOn));

        //    int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "UserSecurityQuestion_Update", lstParameter.ToArray());
        //    return returnValue;
        //}

        public UserSecurityQuestion Get_UserSecurityQuestion_byUserSecurityQuestionId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_UserSecurityQuestionId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "UserSecurityQuestion_Get_BY_UserSecurityQuestionId", lstParameter.ToArray());

            UserSecurityQuestion objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public UserSecurityQuestion Get_UserSecurityQuestion_byUserId(int UserId)
        {

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_UserId", UserId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "UserSecurityQuestion_Get_BY_Userd", lstParameter.ToArray());
            UserSecurityQuestion objEntity = null;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<UserSecurityQuestion> GetAll_UserSecurityQuestion_byUserId(int UserId)
        {

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_UserId", UserId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "UserSecurityQuestion_Get_BY_Userd", lstParameter.ToArray());

            List<UserSecurityQuestion> lstEntity = new List<UserSecurityQuestion>();
            UserSecurityQuestion objEntity = null;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<UserSecurityQuestion> Get_All_UserSecurityQuestion()
        {

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "UserSecurityQuestion_Get_All");
            List<UserSecurityQuestion> lstEntity = new List<UserSecurityQuestion>();
            UserSecurityQuestion objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private UserSecurityQuestion FetchEntity(DataRow dr)
        {
            UserSecurityQuestion objEntity = new UserSecurityQuestion();

            if (dr.Table.Columns.Contains("UserSecurityQuestionId") && dr["UserSecurityQuestionId"] != DBNull.Value)
            {
                objEntity.UserSecurityQuestionId = Convert.ToInt32(dr["UserSecurityQuestionId"]);
            }

            if (dr.Table.Columns.Contains("UserId") && dr["UserId"] != DBNull.Value)
            {
                objEntity.UserId = Convert.ToInt32(dr["UserId"]);
            }

            if (dr.Table.Columns.Contains("SecurityQuestionId") && dr["SecurityQuestionId"] != DBNull.Value)
            {
                objEntity.SecurityQuestionId = Convert.ToInt32(dr["SecurityQuestionId"]);
            }

            if (dr.Table.Columns.Contains("UserName") && dr["UserName"] != DBNull.Value)
            {
                objEntity.UserName = Convert.ToString(dr["UserName"]);
            }

            if (dr.Table.Columns.Contains("SecurityAnswerHash") && dr["SecurityAnswerHash"] != DBNull.Value)
            {
                objEntity.SecurityAnswerHash = Convert.ToString(dr["SecurityAnswerHash"]);
            }

            if (dr.Table.Columns.Contains("SecurityAnswerSalt") && dr["SecurityAnswerSalt"] != DBNull.Value)
            {
                objEntity.SecurityAnswerSalt = Convert.ToString(dr["SecurityAnswerSalt"]);
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

            if (dr.Table.Columns.Contains("ModifiedBy") && dr["ModifiedBy"] != DBNull.Value)
            {
                objEntity.ModifiedBy = Convert.ToInt32(dr["ModifiedBy"]);
            }
            if (dr.Table.Columns.Contains("ModifiedOn") && dr["ModifiedOn"] != DBNull.Value)
            {
                objEntity.ModifiedOn = Convert.ToDateTime(dr["ModifiedOn"]);
            }

            return objEntity;
        }

    }
}
