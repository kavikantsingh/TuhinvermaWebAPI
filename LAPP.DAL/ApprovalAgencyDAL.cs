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

    public class ApprovalAgencyDAL : BaseDAL
    {

        public int Save_ApprovalAgency(ApprovalAgency objApprovalAgency)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("UserId", objApprovalAgency.UserId));
            lstParameter.Add(new MySqlParameter("UserName", objApprovalAgency.UserName));
            lstParameter.Add(new MySqlParameter("UserTypeId", objApprovalAgency.UserTypeId));
            lstParameter.Add(new MySqlParameter("FirstName", objApprovalAgency.FirstName));
            lstParameter.Add(new MySqlParameter("LastName", objApprovalAgency.LastName));
            lstParameter.Add(new MySqlParameter("EntityName", objApprovalAgency.EntityName));
            lstParameter.Add(new MySqlParameter("Phone", objApprovalAgency.Phone));
            lstParameter.Add(new MySqlParameter("CellPhone", objApprovalAgency.CellPhone));
            lstParameter.Add(new MySqlParameter("PositionTitle", objApprovalAgency.PositionTitle));
            lstParameter.Add(new MySqlParameter("Email", objApprovalAgency.Email));
            lstParameter.Add(new MySqlParameter("PasswordHash", objApprovalAgency.PasswordHash));
            lstParameter.Add(new MySqlParameter("PasswordSalt", EncryptionKey.GetSalt()));// objApprovalAgency.PasswordSalt));
            lstParameter.Add(new MySqlParameter("PasswordExpirationDate", objApprovalAgency.PasswordExpirationDate));
            lstParameter.Add(new MySqlParameter("PasswordChangedOn", objApprovalAgency.PasswordChangedOn));
            lstParameter.Add(new MySqlParameter("LastLoginDate", objApprovalAgency.LastLoginDate));
            lstParameter.Add(new MySqlParameter("LastLoginIp", objApprovalAgency.LastLoginIp));
            lstParameter.Add(new MySqlParameter("FailedLogins", objApprovalAgency.FailedLogins));
            lstParameter.Add(new MySqlParameter("ApprovalAgencytatusId", objApprovalAgency.ApprovalAgencytatusId));

            lstParameter.Add(new MySqlParameter("EulaAcceptedOn", objApprovalAgency.EulaAcceptedOn));
            lstParameter.Add(new MySqlParameter("UserExternalId", objApprovalAgency.UserExternalId));
            lstParameter.Add(new MySqlParameter("IndividualId", objApprovalAgency.IndividualId));
            lstParameter.Add(new MySqlParameter("SourceId", objApprovalAgency.SourceId));
            lstParameter.Add(new MySqlParameter("SignatureFileId", objApprovalAgency.SignatureFileId));
            lstParameter.Add(new MySqlParameter("IsPending", objApprovalAgency.IsPending));
            lstParameter.Add(new MySqlParameter("UserGuid", objApprovalAgency.UserGuid));

            lstParameter.Add(new MySqlParameter("Gender", objApprovalAgency.Gender));
            lstParameter.Add(new MySqlParameter("DateOfBirth", objApprovalAgency.DateOfBirth));



            lstParameter.Add(new MySqlParameter("IsActive", objApprovalAgency.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objApprovalAgency.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objApprovalAgency.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objApprovalAgency.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objApprovalAgency.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objApprovalAgency.ModifiedOn));
            lstParameter.Add(new MySqlParameter("TemporaryPassword", objApprovalAgency.TemporaryPassword));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "ApprovalAgency_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        public int Individual_User_Save(ApprovalAgency objApprovalAgency)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("UserId", objApprovalAgency.UserId));
            lstParameter.Add(new MySqlParameter("UserName", objApprovalAgency.UserName));
            lstParameter.Add(new MySqlParameter("UserTypeId", objApprovalAgency.UserTypeId));
            lstParameter.Add(new MySqlParameter("FirstName", objApprovalAgency.FirstName));
            lstParameter.Add(new MySqlParameter("LastName", objApprovalAgency.LastName));
            lstParameter.Add(new MySqlParameter("EntityName", objApprovalAgency.EntityName));
            lstParameter.Add(new MySqlParameter("Phone", objApprovalAgency.Phone));
            lstParameter.Add(new MySqlParameter("CellPhone", objApprovalAgency.CellPhone));
            lstParameter.Add(new MySqlParameter("PositionTitle", objApprovalAgency.PositionTitle));
            lstParameter.Add(new MySqlParameter("Email", objApprovalAgency.Email));
            lstParameter.Add(new MySqlParameter("PasswordHash", objApprovalAgency.PasswordHash));
            lstParameter.Add(new MySqlParameter("PasswordSalt", EncryptionKey.GetSalt()));// objApprovalAgency.PasswordSalt));
            lstParameter.Add(new MySqlParameter("PasswordExpirationDate", objApprovalAgency.PasswordExpirationDate));
            lstParameter.Add(new MySqlParameter("PasswordChangedOn", objApprovalAgency.PasswordChangedOn));
            lstParameter.Add(new MySqlParameter("LastLoginDate", objApprovalAgency.LastLoginDate));
            lstParameter.Add(new MySqlParameter("LastLoginIp", objApprovalAgency.LastLoginIp));
            lstParameter.Add(new MySqlParameter("FailedLogins", objApprovalAgency.FailedLogins));
            lstParameter.Add(new MySqlParameter("ApprovalAgencytatusId", objApprovalAgency.ApprovalAgencytatusId));

            lstParameter.Add(new MySqlParameter("EulaAcceptedOn", objApprovalAgency.EulaAcceptedOn));
            lstParameter.Add(new MySqlParameter("UserExternalId", objApprovalAgency.UserExternalId));
            lstParameter.Add(new MySqlParameter("IndividualId", objApprovalAgency.IndividualId));
            lstParameter.Add(new MySqlParameter("SourceId", objApprovalAgency.SourceId));
            lstParameter.Add(new MySqlParameter("SignatureFileId", objApprovalAgency.SignatureFileId));
            lstParameter.Add(new MySqlParameter("IsPending", objApprovalAgency.IsPending));
            lstParameter.Add(new MySqlParameter("UserGuid", objApprovalAgency.UserGuid));
            lstParameter.Add(new MySqlParameter("IndividualGuid", objApprovalAgency.IndividualGuid));
            lstParameter.Add(new MySqlParameter("Authenticator", objApprovalAgency.Authenticator));

            lstParameter.Add(new MySqlParameter("Gender", objApprovalAgency.Gender));
            lstParameter.Add(new MySqlParameter("DateOfBirth", objApprovalAgency.DateOfBirth));



            lstParameter.Add(new MySqlParameter("IsActive", objApprovalAgency.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objApprovalAgency.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objApprovalAgency.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objApprovalAgency.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objApprovalAgency.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objApprovalAgency.ModifiedOn));

            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            lstParameter.Add(new MySqlParameter("TemporaryPassword", objApprovalAgency.TemporaryPassword));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "Individual_User_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        //public int Update_ApprovalAgency(ApprovalAgency objApprovalAgency)
        //{
        //    DBHelper objDB = new DBHelper();
        //    List<MySqlParameter> lstParameter = new List<MySqlParameter>();

        //    lstParameter.Add(new MySqlParameter("U_UserId", objApprovalAgency.UserId));
        //    lstParameter.Add(new MySqlParameter("U_UserName", objApprovalAgency.UserName));
        //    lstParameter.Add(new MySqlParameter("U_UserTypeId", objApprovalAgency.UserTypeId));
        //    lstParameter.Add(new MySqlParameter("U_FirstName", objApprovalAgency.FirstName));
        //    lstParameter.Add(new MySqlParameter("U_LastName", objApprovalAgency.LastName));
        //    lstParameter.Add(new MySqlParameter("U_EntityName", objApprovalAgency.EntityName));
        //    lstParameter.Add(new MySqlParameter("U_Phone", objApprovalAgency.Phone));
        //    lstParameter.Add(new MySqlParameter("U_CellPhone", objApprovalAgency.CellPhone));
        //    lstParameter.Add(new MySqlParameter("U_PositionTitle", objApprovalAgency.PositionTitle));
        //    lstParameter.Add(new MySqlParameter("U_Email", objApprovalAgency.Email));
        //    lstParameter.Add(new MySqlParameter("U_PasswordHash", objApprovalAgency.PasswordHash));
        //    lstParameter.Add(new MySqlParameter("U_PasswordSalt", objApprovalAgency.PasswordSalt));
        //    lstParameter.Add(new MySqlParameter("U_PasswordExpirationDate", objApprovalAgency.PasswordExpirationDate));
        //    lstParameter.Add(new MySqlParameter("U_PasswordChangedOn", objApprovalAgency.PasswordChangedOn));
        //    lstParameter.Add(new MySqlParameter("U_LastLoginDate", objApprovalAgency.LastLoginDate));
        //    lstParameter.Add(new MySqlParameter("U_LastLoginIp", objApprovalAgency.LastLoginIp));
        //    lstParameter.Add(new MySqlParameter("U_FailedLogins", objApprovalAgency.FailedLogins));
        //    lstParameter.Add(new MySqlParameter("U_ApprovalAgencytatusId", objApprovalAgency.ApprovalAgencytatusId));

        //    lstParameter.Add(new MySqlParameter("U_EulaAcceptedOn", objApprovalAgency.EulaAcceptedOn));
        //    lstParameter.Add(new MySqlParameter("U_UserExternalId", objApprovalAgency.UserExternalId));
        //    lstParameter.Add(new MySqlParameter("U_IndividualId", objApprovalAgency.IndividualId));
        //    lstParameter.Add(new MySqlParameter("U_SourceId", objApprovalAgency.SourceId));
        //    lstParameter.Add(new MySqlParameter("U_SignatureFileId", objApprovalAgency.SignatureFileId));
        //    lstParameter.Add(new MySqlParameter("U_IsPending", objApprovalAgency.IsPending));
        //    lstParameter.Add(new MySqlParameter("U_UserGuid", objApprovalAgency.UserGuid));
        //    lstParameter.Add(new MySqlParameter("U_Gender", objApprovalAgency.Gender));
        //    lstParameter.Add(new MySqlParameter("U_DateOfBirth", objApprovalAgency.DateOfBirth));

        //    lstParameter.Add(new MySqlParameter("U_IsActive", objApprovalAgency.IsActive));
        //    lstParameter.Add(new MySqlParameter("U_IsDeleted", objApprovalAgency.IsDeleted));
        //    lstParameter.Add(new MySqlParameter("U_CreatedBy", objApprovalAgency.CreatedBy));
        //    lstParameter.Add(new MySqlParameter("U_CreatedOn", objApprovalAgency.CreatedOn));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedBy", objApprovalAgency.ModifiedBy));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedOn", objApprovalAgency.ModifiedOn));

        //    int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "ApprovalAgency_Update", lstParameter.ToArray());
        //    return returnValue;
        //}

        public List<ApprovalAgency> Search_ApprovalAgency(ApprovalAgencySearch objApprovalAgency)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));

            lstParameter.Add(new MySqlParameter("_UserName", objApprovalAgency.UserName));
            lstParameter.Add(new MySqlParameter("_UserTypeId", objApprovalAgency.UserTypeId));
            lstParameter.Add(new MySqlParameter("G_FirstName", objApprovalAgency.FirstName));
            lstParameter.Add(new MySqlParameter("_LastName", objApprovalAgency.LastName));
            lstParameter.Add(new MySqlParameter("_Phone", objApprovalAgency.Phone));
            lstParameter.Add(new MySqlParameter("_PositionTitle", objApprovalAgency.PositionTitle));
            lstParameter.Add(new MySqlParameter("_Email", objApprovalAgency.Email));
            lstParameter.Add(new MySqlParameter("_SourceId", objApprovalAgency.SourceId));
            lstParameter.Add(new MySqlParameter("_ApprovalAgencytatusId", objApprovalAgency.ApprovalAgencytatusId));
            lstParameter.Add(new MySqlParameter("_IsPending", objApprovalAgency.IsPending));

            lstParameter.Add(new MySqlParameter("_Gender", objApprovalAgency.Gender));
            lstParameter.Add(new MySqlParameter("_DateOfBirth", objApprovalAgency.DateOfBirth));

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "User_By_Search", lstParameter.ToArray());

            List<ApprovalAgency> lstEntity = new List<ApprovalAgency>();
            ApprovalAgency objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;


        }

        public List<ApprovalAgency> Search_ApprovalAgency_WithPager(ApprovalAgencySearch objApprovalAgency, int CurrentPage, int PagerSize)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));

            lstParameter.Add(new MySqlParameter("UserName", objApprovalAgency.UserName));
            lstParameter.Add(new MySqlParameter("UserTypeId", objApprovalAgency.UserTypeId));
            lstParameter.Add(new MySqlParameter("FirstName", objApprovalAgency.FirstName));
            lstParameter.Add(new MySqlParameter("LastName", objApprovalAgency.LastName));
            lstParameter.Add(new MySqlParameter("Phone", objApprovalAgency.Phone));
            lstParameter.Add(new MySqlParameter("PositionTitle", objApprovalAgency.PositionTitle));
            lstParameter.Add(new MySqlParameter("Email", objApprovalAgency.Email));
            lstParameter.Add(new MySqlParameter("SourceId", objApprovalAgency.SourceId));
            lstParameter.Add(new MySqlParameter("ApprovalAgencytatusId", objApprovalAgency.ApprovalAgencytatusId));
            lstParameter.Add(new MySqlParameter("IsPending", objApprovalAgency.IsPending));
            lstParameter.Add(new MySqlParameter("RoleId", objApprovalAgency.RoleId));

            lstParameter.Add(new MySqlParameter("Gender", objApprovalAgency.Gender));
            lstParameter.Add(new MySqlParameter("DateOfBirth", objApprovalAgency.DateOfBirth));
            lstParameter.Add(new MySqlParameter("PageNo", CurrentPage));
            lstParameter.Add(new MySqlParameter("Pager", PagerSize));

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "User_By_Search_WithPager", lstParameter.ToArray());

            List<ApprovalAgency> lstEntity = new List<ApprovalAgency>();
            ApprovalAgency objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public ApprovalAgency Get_ApprovalAgency_byUserId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_UserId", ID));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ApprovalAgency_Get_BY_UserId", lstParameter.ToArray());
            ApprovalAgency objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }
        public ApprovalAgency Get_ApprovalAgency_byIndividualId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("_IndividualId", ID));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ApprovalAgency_Get_BY_IndividualId", lstParameter.ToArray());
            ApprovalAgency objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }
        public ApprovalAgency Get_ApprovalAgency_by_Email(string Email)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_Email", Email));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ApprovalAgency_Get_BY_Email", lstParameter.ToArray());
            ApprovalAgency objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public ApprovalAgency Get_ApprovalAgency_by_UserName(string UserName)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_UserName", UserName));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ApprovalAgency_Get_BY_UserName", lstParameter.ToArray());
            ApprovalAgency objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public ApprovalAgency Get_ApprovalAgency_by_Email_And_Password(string Email, string Password)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_Email", Email));
            lstParameter.Add(new MySqlParameter("G_PasswordHash", Password));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ApprovalAgency_Get_BY_Email_And_Password", lstParameter.ToArray());
            ApprovalAgency objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<ApprovalAgency> Get_All_ApprovalAgency()
        {

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ApprovalAgency_Get_All", lstParameter.ToArray());
            List<ApprovalAgency> lstEntity = new List<ApprovalAgency>();
            ApprovalAgency objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private ApprovalAgency FetchEntity(DataRow dr)
        {
            ApprovalAgency objEntity = new ApprovalAgency();

            if (dr.Table.Columns.Contains("UserId") && dr["UserId"] != DBNull.Value)
            {
                objEntity.UserId = Convert.ToInt32(dr["UserId"]);
            }

            if (dr.Table.Columns.Contains("UserName") && dr["UserName"] != DBNull.Value)
            {
                objEntity.UserName = Convert.ToString(dr["UserName"]);
            }

            if (dr.Table.Columns.Contains("UserTypeId") && dr["UserTypeId"] != DBNull.Value)
            {
                objEntity.UserTypeId = Convert.ToInt32(dr["UserTypeId"]);
            }

            if (dr.Table.Columns.Contains("FirstName") && dr["FirstName"] != DBNull.Value)
            {
                objEntity.FirstName = Convert.ToString(dr["FirstName"]);
            }

            if (dr.Table.Columns.Contains("LastName") && dr["LastName"] != DBNull.Value)
            {
                objEntity.LastName = Convert.ToString(dr["LastName"]);
            }

            if (dr.Table.Columns.Contains("EntityName") && dr["EntityName"] != DBNull.Value)
            {
                objEntity.EntityName = Convert.ToString(dr["EntityName"]);
            }
            if (dr.Table.Columns.Contains("Phone") && dr["Phone"] != DBNull.Value)
            {
                objEntity.Phone = Convert.ToString(dr["Phone"]);
            }
            if (dr.Table.Columns.Contains("CellPhone") && dr["CellPhone"] != DBNull.Value)
            {
                objEntity.CellPhone = Convert.ToString(dr["CellPhone"]);
            }
            if (dr.Table.Columns.Contains("PositionTitle") && dr["PositionTitle"] != DBNull.Value)
            {
                objEntity.PositionTitle = Convert.ToString(dr["PositionTitle"]);
            }

            if (dr.Table.Columns.Contains("Email") && dr["Email"] != DBNull.Value)
            {
                objEntity.Email = Convert.ToString(dr["Email"]);
            }
            if (dr.Table.Columns.Contains("PasswordHash") && dr["PasswordHash"] != DBNull.Value)
            {
                objEntity.PasswordHash = Convert.ToString(dr["PasswordHash"]);
            }
            if (dr.Table.Columns.Contains("PasswordSalt") && dr["PasswordSalt"] != DBNull.Value)
            {
                objEntity.PasswordSalt = Convert.ToString(dr["PasswordSalt"]);
            }
            if (dr.Table.Columns.Contains("PasswordExpirationDate") && dr["PasswordExpirationDate"] != DBNull.Value)
            {
                objEntity.PasswordExpirationDate = Convert.ToDateTime(dr["PasswordExpirationDate"]);
            }
            if (dr.Table.Columns.Contains("PasswordChangedOn") && dr["PasswordChangedOn"] != DBNull.Value)
            {
                objEntity.PasswordChangedOn = Convert.ToDateTime(dr["PasswordChangedOn"]);
            }
            if (dr.Table.Columns.Contains("LastLoginDate") && dr["LastLoginDate"] != DBNull.Value)
            {
                objEntity.LastLoginDate = Convert.ToDateTime(dr["LastLoginDate"]);
            }

            if (dr.Table.Columns.Contains("LastLoginIp") && dr["LastLoginIp"] != DBNull.Value)
            {
                objEntity.LastLoginIp = Convert.ToString(dr["LastLoginIp"]);
            }
            if (dr.Table.Columns.Contains("FailedLogins") && dr["FailedLogins"] != DBNull.Value)
            {
                objEntity.FailedLogins = Convert.ToInt32(dr["FailedLogins"]);
            }

            if (dr.Table.Columns.Contains("ApprovalAgencytatusId") && dr["ApprovalAgencytatusId"] != DBNull.Value)
            {
                objEntity.ApprovalAgencytatusId = Convert.ToInt32(dr["ApprovalAgencytatusId"]);
            }

            if (dr.Table.Columns.Contains("EulaAcceptedOn") && dr["EulaAcceptedOn"] != DBNull.Value)
            {
                objEntity.EulaAcceptedOn = Convert.ToDateTime(dr["EulaAcceptedOn"]);
            }
            if (dr.Table.Columns.Contains("UserExternalId") && dr["UserExternalId"] != DBNull.Value)
            {
                objEntity.UserExternalId = Convert.ToString(dr["UserExternalId"]);
            }

            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("SourceId") && dr["SourceId"] != DBNull.Value)
            {
                objEntity.SourceId = Convert.ToInt32(dr["SourceId"]);
            }
            if (dr.Table.Columns.Contains("SignatureFileId") && dr["SignatureFileId"] != DBNull.Value)
            {
                objEntity.SignatureFileId = Convert.ToInt32(dr["SignatureFileId"]);
            }

            if (dr.Table.Columns.Contains("DateOfBirth") && dr["DateOfBirth"] != DBNull.Value)
            {
                objEntity.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]);
            }
            if (dr.Table.Columns.Contains("Gender") && dr["Gender"] != DBNull.Value)
            {
                objEntity.Gender = Convert.ToString(dr["Gender"]);
            }

            if (dr.Table.Columns.Contains("IsPending") && dr["IsPending"] != DBNull.Value)
            {
                objEntity.IsPending = Convert.ToBoolean(dr["IsPending"]);
            }
            if (dr.Table.Columns.Contains("UserGuid") && dr["UserGuid"] != DBNull.Value)
            {
                objEntity.UserGuid = Convert.ToString(dr["UserGuid"]);
            }
            if (dr.Table.Columns.Contains("TemporaryPassword") && dr["TemporaryPassword"] != DBNull.Value)
            {
                objEntity.TemporaryPassword = Convert.ToBoolean(dr["TemporaryPassword"]);
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


            // Joined  Field For View Only

            if (dr.Table.Columns.Contains("SourceName") && dr["SourceName"] != DBNull.Value)
            {
                objEntity.SourceName = Convert.ToString(dr["SourceName"]);
            }
            if (dr.Table.Columns.Contains("IndividualName") && dr["IndividualName"] != DBNull.Value)
            {
                objEntity.IndividualName = Convert.ToString(dr["IndividualName"]);
            }
            if (dr.Table.Columns.Contains("UserTypeName") && dr["UserTypeName"] != DBNull.Value)
            {
                objEntity.UserTypeName = Convert.ToString(dr["UserTypeName"]);
            }
            if (dr.Table.Columns.Contains("ApprovalAgencytatusName") && dr["ApprovalAgencytatusName"] != DBNull.Value)
            {
                objEntity.ApprovalAgencytatusName = Convert.ToString(dr["ApprovalAgencytatusName"]);
            }

            if (dr.Table.Columns.Contains("Total_Recard") && dr["Total_Recard"] != DBNull.Value)
            {
                objEntity.Total_Recard = Convert.ToInt32(dr["Total_Recard"]);
            }

            return objEntity;
        }

        //private ApprovalAgencySearch FetchApprovalAgencySearchEntity(DataRow dr)
        //{
        //    ApprovalAgencySearch objEntity = new ApprovalAgencySearch();

        //    if (dr.Table.Columns.Contains("UserName") && dr["UserName"] != DBNull.Value)
        //    {
        //        objEntity.UserName = Convert.ToString(dr["UserName"]);
        //    }

        //    if (dr.Table.Columns.Contains("UserTypeId") && dr["UserTypeId"] != DBNull.Value)
        //    {
        //        objEntity.UserTypeId = Convert.ToInt32(dr["UserTypeId"]);
        //    }

        //    if (dr.Table.Columns.Contains("FirstName") && dr["FirstName"] != DBNull.Value)
        //    {
        //        objEntity.FirstName = Convert.ToString(dr["FirstName"]);
        //    }

        //    if (dr.Table.Columns.Contains("LastName") && dr["LastName"] != DBNull.Value)
        //    {
        //        objEntity.LastName = Convert.ToString(dr["LastName"]);
        //    }

        //    if (dr.Table.Columns.Contains("Phone") && dr["Phone"] != DBNull.Value)
        //    {
        //        objEntity.Phone = Convert.ToString(dr["Phone"]);
        //    }

        //    if (dr.Table.Columns.Contains("PositionTitle") && dr["PositionTitle"] != DBNull.Value)
        //    {
        //        objEntity.PositionTitle = Convert.ToString(dr["PositionTitle"]);
        //    }

        //    if (dr.Table.Columns.Contains("Email") && dr["Email"] != DBNull.Value)
        //    {
        //        objEntity.Email = Convert.ToString(dr["Email"]);
        //    }

        //    if (dr.Table.Columns.Contains("DateOfBirth") && dr["DateOfBirth"] != DBNull.Value)
        //    {
        //        objEntity.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]);
        //    }
        //    if (dr.Table.Columns.Contains("Gender") && dr["Gender"] != DBNull.Value)
        //    {
        //        objEntity.Gender = Convert.ToString(dr["Gender"]);
        //    }

        //    if (dr.Table.Columns.Contains("ApprovalAgencytatusId") && dr["ApprovalAgencytatusId"] != DBNull.Value)
        //    {
        //        objEntity.ApprovalAgencytatusId = Convert.ToInt32(dr["ApprovalAgencytatusId"]);
        //    }

        //    if (dr.Table.Columns.Contains("SourceId") && dr["SourceId"] != DBNull.Value)
        //    {
        //        objEntity.SourceId = Convert.ToInt32(dr["SourceId"]);
        //    }

        //    if (dr.Table.Columns.Contains("IsPending") && dr["IsPending"] != DBNull.Value)
        //    {
        //        objEntity.IsPending = Convert.ToBoolean(dr["IsPending"]);
        //    }


        //    // Joined  Field For View Only

        //    if (dr.Table.Columns.Contains("SourceName") && dr["SourceName"] != DBNull.Value)
        //    {
        //        objEntity.SourceName = Convert.ToString(dr["SourceName"]);
        //    }

        //    if (dr.Table.Columns.Contains("UserTypeName") && dr["UserTypeName"] != DBNull.Value)
        //    {
        //        objEntity.UserTypeName = Convert.ToString(dr["UserTypeName"]);
        //    }
        //    if (dr.Table.Columns.Contains("ApprovalAgencytatusName") && dr["ApprovalAgencytatusName"] != DBNull.Value)
        //    {
        //        objEntity.ApprovalAgencytatusName = Convert.ToString(dr["ApprovalAgencytatusName"]);
        //    }


        //    return objEntity;
        //}
    }
}
