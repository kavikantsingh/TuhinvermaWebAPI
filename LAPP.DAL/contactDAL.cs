using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class ContactDAL : BaseDAL
    {
        public int Save_Contact(Contact objContact)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ContactId", objContact.ContactId));
            lstParameter.Add(new MySqlParameter("ContactFirstName", objContact.ContactFirstName.NullString()));
            lstParameter.Add(new MySqlParameter("ContactLastName", objContact.ContactLastName.NullString()));
            lstParameter.Add(new MySqlParameter("ContactMiddleName", objContact.ContactMiddleName.NullString()));
            lstParameter.Add(new MySqlParameter("ContactTypeId", objContact.ContactTypeId));
            lstParameter.Add(new MySqlParameter("Code", objContact.Code.NullString()));
            lstParameter.Add(new MySqlParameter("ContactInfo", objContact.ContactInfo.NullString()));
            lstParameter.Add(new MySqlParameter("DateContactValidated", objContact.DateContactValidated));
            lstParameter.Add(new MySqlParameter("IsActive", objContact.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objContact.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objContact.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objContact.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objContact.CreatedOn));

            lstParameter.Add(new MySqlParameter("ModifiedBy", objContact.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ContactGuid", objContact.ContactGuid));
            lstParameter.Add(new MySqlParameter("Authenticator", objContact.Authenticator));

            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "contact_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }
        //public int Update_Contact(Contact objContact)
        //{
        //    DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
        //    lstParameter.Add(new MySqlParameter("U_ContactId", objContact.ContactId));
        //    lstParameter.Add(new MySqlParameter("U_ContactFirstName", objContact.ContactFirstName));
        //    lstParameter.Add(new MySqlParameter("U_ContactLastName", objContact.ContactLastName));
        //    lstParameter.Add(new MySqlParameter("U_ContactMiddleName", objContact.ContactMiddleName));
        //    lstParameter.Add(new MySqlParameter("U_ContactTypeId", objContact.ContactTypeId));
        //    lstParameter.Add(new MySqlParameter("U_Code", objContact.Code));
        //    lstParameter.Add(new MySqlParameter("U_ContactInfo", objContact.ContactInfo));
        //    lstParameter.Add(new MySqlParameter("U_DateContactValidated", objContact.DateContactValidated));
        //    lstParameter.Add(new MySqlParameter("U_IsActive", objContact.IsActive));
        //    lstParameter.Add(new MySqlParameter("U_IsDeleted", objContact.IsDeleted));
        //    lstParameter.Add(new MySqlParameter("U_CreatedBy", objContact.CreatedBy));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedBy", objContact.ModifiedBy));
        //    lstParameter.Add(new MySqlParameter("U_ContactGuid", objContact.ContactGuid));
        //    lstParameter.Add(new MySqlParameter("U_Authenticator", objContact.Authenticator));

        //    int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "CONTACT_Update", lstParameter.ToArray());

        //    return returnValue;
        //}

        public List<Contact> Get_All_Contact()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();

            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "contact_Get_All", lstParameter.ToArray());
            List<Contact> lstEntity = new List<Contact>();
            Contact objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public Contact Get_Contact_By_ContactId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            lstParameter.Add(new MySqlParameter("G_ContactId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "contact_Get_By_ContactId", lstParameter.ToArray());
            Contact objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private Contact FetchEntity(DataRow dr)
        {
            Contact objEntity = new Contact();
            if (dr.Table.Columns.Contains("ContactId") && dr["ContactId"] != DBNull.Value)
            {
                objEntity.ContactId = Convert.ToInt32(dr["ContactId"]);
            }
            if (dr.Table.Columns.Contains("ContactFirstName") && dr["ContactFirstName"] != DBNull.Value)
            {
                objEntity.ContactFirstName = Convert.ToString(dr["ContactFirstName"]);
            }
            if (dr.Table.Columns.Contains("ContactLastName") && dr["ContactLastName"] != DBNull.Value)
            {
                objEntity.ContactLastName = Convert.ToString(dr["ContactLastName"]);
            }
            if (dr.Table.Columns.Contains("ContactMiddleName") && dr["ContactMiddleName"] != DBNull.Value)
            {
                objEntity.ContactMiddleName = Convert.ToString(dr["ContactMiddleName"]);
            }
            if (dr.Table.Columns.Contains("ContactTypeId") && dr["ContactTypeId"] != DBNull.Value)
            {
                objEntity.ContactTypeId = Convert.ToInt32(dr["ContactTypeId"]);
            }
            if (dr.Table.Columns.Contains("Code") && dr["Code"] != DBNull.Value)
            {
                objEntity.Code = Convert.ToString(dr["Code"]);
            }
            if (dr.Table.Columns.Contains("ContactInfo") && dr["ContactInfo"] != DBNull.Value)
            {
                objEntity.ContactInfo = Convert.ToString(dr["ContactInfo"]);
            }
            if (dr.Table.Columns.Contains("DateContactValidated") && dr["DateContactValidated"] != DBNull.Value)
            {
                objEntity.DateContactValidated = Convert.ToDateTime(dr["DateContactValidated"]);
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
            if (dr.Table.Columns.Contains("ContactGuid") && dr["ContactGuid"] != DBNull.Value)
            {
                objEntity.ContactGuid = Convert.ToString(dr["ContactGuid"]);
            }
            if (dr.Table.Columns.Contains("Authenticator") && dr["Authenticator"] != DBNull.Value)
            {
                objEntity.Authenticator = Convert.ToString(dr["Authenticator"]);
            }
            return objEntity;

        }


        public int Save_ContactAndProviderContact(ProviderInformation objContact)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ContactType_Id", objContact.ContactTypeId));
            lstParameter.Add(new MySqlParameter("ContactInfo", objContact.ContactInfo));
            lstParameter.Add(new MySqlParameter("CreatedBy", objContact.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", DateTime.Now));
            lstParameter.Add(new MySqlParameter("Provider_Id", objContact.ProviderId));
            lstParameter.Add(new MySqlParameter("IsPreferredContact", objContact.IsPreferredContact));
            lstParameter.Add(new MySqlParameter("IsMobile", objContact.IsMobile));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            
            //objDB.ExecuteNonQuery(CommandType.StoredProcedure, "contact_SaveContactAndProviderContact", true, lstParameter.ToArray());
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "contact_SaveContactAndProviderContactSchoolInfo", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public ProviderInformation Get_ContactAndProviderContactByProviderId(int ProviderId, int ContactTypeId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            lstParameter.Add(new MySqlParameter("ProviderId", ProviderId));
            lstParameter.Add(new MySqlParameter("ContactTypeId", ContactTypeId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "contact_Get_ContactAndProviderContactByProviderId", lstParameter.ToArray());
            ProviderInformation objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchContactEntity(dr);
            }
            return objEntity;
        }

        private ProviderInformation FetchContactEntity(DataRow dr)
        {
            ProviderInformation objEntity = new ProviderInformation();

            if (dr.Table.Columns.Contains("ContactInfo") && dr["ContactInfo"] != DBNull.Value)
            {
                objEntity.ContactInfo = Convert.ToString(dr["ContactInfo"]);
            }
            if (dr.Table.Columns.Contains("IsMobile") && dr["IsMobile"] != DBNull.Value)
            {
                objEntity.IsMobile = Convert.ToBoolean(dr["IsMobile"]);
            }
            return objEntity;

        }
    }
}
