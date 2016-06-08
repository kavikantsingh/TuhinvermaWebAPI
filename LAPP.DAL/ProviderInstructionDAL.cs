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


    public class ProviderInstructionsDAL : BaseDAL
    {
        public int SaveButtonOfInstructions(ProviderInstructions objProviderInstructions)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("ProviderId", objProviderInstructions.ProviderId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objProviderInstructions.ApplicationId));
            lstParameter.Add(new MySqlParameter("ContentItemLkId", objProviderInstructions.ContentItemLkId));
            lstParameter.Add(new MySqlParameter("ContentItemLkCode", objProviderInstructions.ContentItemLkCode));
            lstParameter.Add(new MySqlParameter("ReferenceNumber", objProviderInstructions.ReferenceNumber));
            lstParameter.Add(new MySqlParameter("InstructionsAcceptedBy", objProviderInstructions.InstructionsAcceptedBy));
            lstParameter.Add(new MySqlParameter("InstructionsAcceptanceDate", objProviderInstructions.InstructionsAcceptanceDate));
            lstParameter.Add(new MySqlParameter("IsActive", objProviderInstructions.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objProviderInstructions.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objProviderInstructions.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objProviderInstructions.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objProviderInstructions.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objProviderInstructions.ModifiedOn));
            lstParameter.Add(new MySqlParameter("ProviderInstructionsGuid", objProviderInstructions.ProviderInstructionsGuid));
            
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "ProviderInstruction_SaveButtonOfInstructions", true, lstParameter.ToArray());
            return returnValue;
        }

        public int CheckInitialTabActive(int applicationId, int providerId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_applicationId", applicationId));
            lstParameter.Add(new MySqlParameter("G_providerId", providerId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "applicationtabstatus_CheckInitialTabActive", lstParameter.ToArray());
            return ds.Tables[0].Rows.Count; ;
        }

        public int SavePreviousSchoolDetails(ProviderInstructions objProviderInstructions)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("ProviderId", objProviderInstructions.ProviderId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objProviderInstructions.ApplicationId));
            lstParameter.Add(new MySqlParameter("ContentItemLkId", objProviderInstructions.ContentItemLkId));
            lstParameter.Add(new MySqlParameter("ContentItemLkCode", objProviderInstructions.ContentItemLkCode));
            lstParameter.Add(new MySqlParameter("ReferenceNumber", objProviderInstructions.ReferenceNumber));
            lstParameter.Add(new MySqlParameter("InstructionsAcceptedBy", objProviderInstructions.InstructionsAcceptedBy));
            lstParameter.Add(new MySqlParameter("InstructionsAcceptanceDate", objProviderInstructions.InstructionsAcceptanceDate));
            lstParameter.Add(new MySqlParameter("IsActive", objProviderInstructions.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objProviderInstructions.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objProviderInstructions.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objProviderInstructions.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objProviderInstructions.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objProviderInstructions.ModifiedOn));
            lstParameter.Add(new MySqlParameter("ProviderInstructionsGuid", objProviderInstructions.ProviderInstructionsGuid));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "ProviderInstruction_SaveButtonOfInstructions", true, lstParameter.ToArray());
            return returnValue;
        }

















        public int Save_Users(Users objUsers)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("UserId", objUsers.UserId));
            lstParameter.Add(new MySqlParameter("UserName", objUsers.UserName));
            lstParameter.Add(new MySqlParameter("UserTypeId", objUsers.UserTypeId));
            lstParameter.Add(new MySqlParameter("FirstName", objUsers.FirstName));
            lstParameter.Add(new MySqlParameter("LastName", objUsers.LastName));
            lstParameter.Add(new MySqlParameter("EntityName", objUsers.EntityName));
            lstParameter.Add(new MySqlParameter("Phone", objUsers.Phone));
            lstParameter.Add(new MySqlParameter("CellPhone", objUsers.CellPhone));
            lstParameter.Add(new MySqlParameter("PositionTitle", objUsers.PositionTitle));
            lstParameter.Add(new MySqlParameter("Email", objUsers.Email));
            lstParameter.Add(new MySqlParameter("PasswordHash", objUsers.PasswordHash));
            lstParameter.Add(new MySqlParameter("PasswordSalt", EncryptionKey.GetSalt()));// objUsers.PasswordSalt));
            lstParameter.Add(new MySqlParameter("PasswordExpirationDate", objUsers.PasswordExpirationDate));
            lstParameter.Add(new MySqlParameter("PasswordChangedOn", objUsers.PasswordChangedOn));
            lstParameter.Add(new MySqlParameter("LastLoginDate", objUsers.LastLoginDate));
            lstParameter.Add(new MySqlParameter("LastLoginIp", objUsers.LastLoginIp));
            lstParameter.Add(new MySqlParameter("FailedLogins", objUsers.FailedLogins));
            lstParameter.Add(new MySqlParameter("UserStatusId", objUsers.UserStatusId));

            lstParameter.Add(new MySqlParameter("EulaAcceptedOn", objUsers.EulaAcceptedOn));
            lstParameter.Add(new MySqlParameter("UserExternalId", objUsers.UserExternalId));
            lstParameter.Add(new MySqlParameter("IndividualId", objUsers.IndividualId));
            lstParameter.Add(new MySqlParameter("SourceId", objUsers.SourceId));
            lstParameter.Add(new MySqlParameter("SignatureFileId", objUsers.SignatureFileId));
            lstParameter.Add(new MySqlParameter("IsPending", objUsers.IsPending));
            lstParameter.Add(new MySqlParameter("UserGuid", objUsers.UserGuid));

            lstParameter.Add(new MySqlParameter("Gender", objUsers.Gender));
            lstParameter.Add(new MySqlParameter("DateOfBirth", objUsers.DateOfBirth));



            lstParameter.Add(new MySqlParameter("IsActive", objUsers.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objUsers.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objUsers.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objUsers.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objUsers.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objUsers.ModifiedOn));
            lstParameter.Add(new MySqlParameter("TemporaryPassword", objUsers.TemporaryPassword));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "Users_Save", true, lstParameter.ToArray());
            return returnValue;
        }



        public int Individual_User_Save(Users objUsers)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("UserId", objUsers.UserId));
            lstParameter.Add(new MySqlParameter("UserName", objUsers.UserName));
            lstParameter.Add(new MySqlParameter("UserTypeId", objUsers.UserTypeId));
            lstParameter.Add(new MySqlParameter("FirstName", objUsers.FirstName));
            lstParameter.Add(new MySqlParameter("LastName", objUsers.LastName));
            lstParameter.Add(new MySqlParameter("EntityName", objUsers.EntityName));
            lstParameter.Add(new MySqlParameter("Phone", objUsers.Phone));
            lstParameter.Add(new MySqlParameter("CellPhone", objUsers.CellPhone));
            lstParameter.Add(new MySqlParameter("PositionTitle", objUsers.PositionTitle));
            lstParameter.Add(new MySqlParameter("Email", objUsers.Email));
            lstParameter.Add(new MySqlParameter("PasswordHash", objUsers.PasswordHash));
            lstParameter.Add(new MySqlParameter("PasswordSalt", EncryptionKey.GetSalt()));// objUsers.PasswordSalt));
            lstParameter.Add(new MySqlParameter("PasswordExpirationDate", objUsers.PasswordExpirationDate));
            lstParameter.Add(new MySqlParameter("PasswordChangedOn", objUsers.PasswordChangedOn));
            lstParameter.Add(new MySqlParameter("LastLoginDate", objUsers.LastLoginDate));
            lstParameter.Add(new MySqlParameter("LastLoginIp", objUsers.LastLoginIp));
            lstParameter.Add(new MySqlParameter("FailedLogins", objUsers.FailedLogins));
            lstParameter.Add(new MySqlParameter("UserStatusId", objUsers.UserStatusId));

            lstParameter.Add(new MySqlParameter("EulaAcceptedOn", objUsers.EulaAcceptedOn));
            lstParameter.Add(new MySqlParameter("UserExternalId", objUsers.UserExternalId));
            lstParameter.Add(new MySqlParameter("IndividualId", objUsers.IndividualId));
            lstParameter.Add(new MySqlParameter("SourceId", objUsers.SourceId));
            lstParameter.Add(new MySqlParameter("SignatureFileId", objUsers.SignatureFileId));
            lstParameter.Add(new MySqlParameter("IsPending", objUsers.IsPending));
            lstParameter.Add(new MySqlParameter("UserGuid", objUsers.UserGuid));
            lstParameter.Add(new MySqlParameter("IndividualGuid", objUsers.IndividualGuid));
            lstParameter.Add(new MySqlParameter("Authenticator", objUsers.Authenticator));

            lstParameter.Add(new MySqlParameter("Gender", objUsers.Gender));
            lstParameter.Add(new MySqlParameter("DateOfBirth", objUsers.DateOfBirth));



            lstParameter.Add(new MySqlParameter("IsActive", objUsers.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objUsers.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objUsers.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objUsers.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objUsers.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objUsers.ModifiedOn));

            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            lstParameter.Add(new MySqlParameter("TemporaryPassword", objUsers.TemporaryPassword));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "Individual_User_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        //public int Update_Users(Users objUsers)
        //{
        //    DBHelper objDB = new DBHelper();
        //    List<MySqlParameter> lstParameter = new List<MySqlParameter>();

        //    lstParameter.Add(new MySqlParameter("U_UserId", objUsers.UserId));
        //    lstParameter.Add(new MySqlParameter("U_UserName", objUsers.UserName));
        //    lstParameter.Add(new MySqlParameter("U_UserTypeId", objUsers.UserTypeId));
        //    lstParameter.Add(new MySqlParameter("U_FirstName", objUsers.FirstName));
        //    lstParameter.Add(new MySqlParameter("U_LastName", objUsers.LastName));
        //    lstParameter.Add(new MySqlParameter("U_EntityName", objUsers.EntityName));
        //    lstParameter.Add(new MySqlParameter("U_Phone", objUsers.Phone));
        //    lstParameter.Add(new MySqlParameter("U_CellPhone", objUsers.CellPhone));
        //    lstParameter.Add(new MySqlParameter("U_PositionTitle", objUsers.PositionTitle));
        //    lstParameter.Add(new MySqlParameter("U_Email", objUsers.Email));
        //    lstParameter.Add(new MySqlParameter("U_PasswordHash", objUsers.PasswordHash));
        //    lstParameter.Add(new MySqlParameter("U_PasswordSalt", objUsers.PasswordSalt));
        //    lstParameter.Add(new MySqlParameter("U_PasswordExpirationDate", objUsers.PasswordExpirationDate));
        //    lstParameter.Add(new MySqlParameter("U_PasswordChangedOn", objUsers.PasswordChangedOn));
        //    lstParameter.Add(new MySqlParameter("U_LastLoginDate", objUsers.LastLoginDate));
        //    lstParameter.Add(new MySqlParameter("U_LastLoginIp", objUsers.LastLoginIp));
        //    lstParameter.Add(new MySqlParameter("U_FailedLogins", objUsers.FailedLogins));
        //    lstParameter.Add(new MySqlParameter("U_UserStatusId", objUsers.UserStatusId));

        //    lstParameter.Add(new MySqlParameter("U_EulaAcceptedOn", objUsers.EulaAcceptedOn));
        //    lstParameter.Add(new MySqlParameter("U_UserExternalId", objUsers.UserExternalId));
        //    lstParameter.Add(new MySqlParameter("U_IndividualId", objUsers.IndividualId));
        //    lstParameter.Add(new MySqlParameter("U_SourceId", objUsers.SourceId));
        //    lstParameter.Add(new MySqlParameter("U_SignatureFileId", objUsers.SignatureFileId));
        //    lstParameter.Add(new MySqlParameter("U_IsPending", objUsers.IsPending));
        //    lstParameter.Add(new MySqlParameter("U_UserGuid", objUsers.UserGuid));
        //    lstParameter.Add(new MySqlParameter("U_Gender", objUsers.Gender));
        //    lstParameter.Add(new MySqlParameter("U_DateOfBirth", objUsers.DateOfBirth));

        //    lstParameter.Add(new MySqlParameter("U_IsActive", objUsers.IsActive));
        //    lstParameter.Add(new MySqlParameter("U_IsDeleted", objUsers.IsDeleted));
        //    lstParameter.Add(new MySqlParameter("U_CreatedBy", objUsers.CreatedBy));
        //    lstParameter.Add(new MySqlParameter("U_CreatedOn", objUsers.CreatedOn));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedBy", objUsers.ModifiedBy));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedOn", objUsers.ModifiedOn));

        //    int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "Users_Update", lstParameter.ToArray());
        //    return returnValue;
        //}

        public List<UsersSearch> Search_Users(UsersSearch objUsers)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));

            lstParameter.Add(new MySqlParameter("_UserName", objUsers.UserName));
            lstParameter.Add(new MySqlParameter("_UserTypeId", objUsers.UserTypeId));
            lstParameter.Add(new MySqlParameter("G_FirstName", objUsers.FirstName));
            lstParameter.Add(new MySqlParameter("_LastName", objUsers.LastName));
            lstParameter.Add(new MySqlParameter("_Phone", objUsers.Phone));
            lstParameter.Add(new MySqlParameter("_PositionTitle", objUsers.PositionTitle));
            lstParameter.Add(new MySqlParameter("_Email", objUsers.Email));
            lstParameter.Add(new MySqlParameter("_SourceId", objUsers.SourceId));
            lstParameter.Add(new MySqlParameter("_UserStatusId", objUsers.UserStatusId));
            lstParameter.Add(new MySqlParameter("_IsPending", objUsers.IsPending));

            lstParameter.Add(new MySqlParameter("_Gender", objUsers.Gender));
            lstParameter.Add(new MySqlParameter("_DateOfBirth", objUsers.DateOfBirth));

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "User_By_Search", lstParameter.ToArray());

            List<UsersSearch> lstEntity = new List<UsersSearch>();
            UsersSearch objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchUsersSearchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;


        }

        public Users Get_Users_byUserId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_UserId", ID));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Users_Get_BY_UserId", lstParameter.ToArray());
            Users objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }
        public Users Get_Users_byIndividualId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("_IndividualId", ID));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Users_Get_BY_IndividualId", lstParameter.ToArray());
            Users objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }
        public Users Get_Users_by_Email(string Email)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_Email", Email));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Users_Get_BY_Email", lstParameter.ToArray());
            Users objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public Users Get_Users_by_Email_And_Password(string Email, string Password)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_Email", Email));
            lstParameter.Add(new MySqlParameter("G_PasswordHash", Password));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Users_Get_BY_Email_And_Password", lstParameter.ToArray());
            Users objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<Users> Get_All_Users()
        {

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Users_Get_All", lstParameter.ToArray());
            List<Users> lstEntity = new List<Users>();
            Users objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private Users FetchEntity(DataRow dr)
        {
            Users objEntity = new Users();

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

            if (dr.Table.Columns.Contains("UserStatusId") && dr["UserStatusId"] != DBNull.Value)
            {
                objEntity.UserStatusId = Convert.ToInt32(dr["UserStatusId"]);
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
            if (dr.Table.Columns.Contains("UserStatusName") && dr["UserStatusName"] != DBNull.Value)
            {
                objEntity.UserStatusName = Convert.ToString(dr["UserStatusName"]);
            }

            return objEntity;
        }

        private UsersSearch FetchUsersSearchEntity(DataRow dr)
        {
            UsersSearch objEntity = new UsersSearch();



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

            if (dr.Table.Columns.Contains("Phone") && dr["Phone"] != DBNull.Value)
            {
                objEntity.Phone = Convert.ToString(dr["Phone"]);
            }

            if (dr.Table.Columns.Contains("PositionTitle") && dr["PositionTitle"] != DBNull.Value)
            {
                objEntity.PositionTitle = Convert.ToString(dr["PositionTitle"]);
            }

            if (dr.Table.Columns.Contains("Email") && dr["Email"] != DBNull.Value)
            {
                objEntity.Email = Convert.ToString(dr["Email"]);
            }


            if (dr.Table.Columns.Contains("DateOfBirth") && dr["DateOfBirth"] != DBNull.Value)
            {
                objEntity.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]);
            }
            if (dr.Table.Columns.Contains("Gender") && dr["Gender"] != DBNull.Value)
            {
                objEntity.Gender = Convert.ToString(dr["Gender"]);
            }

            if (dr.Table.Columns.Contains("UserStatusId") && dr["UserStatusId"] != DBNull.Value)
            {
                objEntity.UserStatusId = Convert.ToInt32(dr["UserStatusId"]);
            }

            if (dr.Table.Columns.Contains("SourceId") && dr["SourceId"] != DBNull.Value)
            {
                objEntity.SourceId = Convert.ToInt32(dr["SourceId"]);
            }


            if (dr.Table.Columns.Contains("IsPending") && dr["IsPending"] != DBNull.Value)
            {
                objEntity.IsPending = Convert.ToBoolean(dr["IsPending"]);
            }


            // Joined  Field For View Only

            if (dr.Table.Columns.Contains("SourceName") && dr["SourceName"] != DBNull.Value)
            {
                objEntity.SourceName = Convert.ToString(dr["SourceName"]);
            }

            if (dr.Table.Columns.Contains("UserTypeName") && dr["UserTypeName"] != DBNull.Value)
            {
                objEntity.UserTypeName = Convert.ToString(dr["UserTypeName"]);
            }
            if (dr.Table.Columns.Contains("UserStatusName") && dr["UserStatusName"] != DBNull.Value)
            {
                objEntity.UserStatusName = Convert.ToString(dr["UserStatusName"]);
            }


            return objEntity;
        }
    }
}
