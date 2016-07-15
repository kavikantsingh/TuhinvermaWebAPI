using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;
using LAPP.ENTITY;

namespace LAPP.DAL
{
    public class ProviderRelatedSchoolsDAL : BaseDAL
    {

        public int SaveProviderRelatedSchools(ProviderRelatedSchools objProviderRS)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("ProviderRelatedSchoolId", objProviderRS.ProviderRelatedSchoolId));
            lstParameter.Add(new MySqlParameter("ProviderId", objProviderRS.ProviderId));
            lstParameter.Add(new MySqlParameter("ProviderNameId", objProviderRS.ProviderNameId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objProviderRS.ApplicationId));
            lstParameter.Add(new MySqlParameter("DateAssociated", objProviderRS.DateAssociated));
            lstParameter.Add(new MySqlParameter("IsActive", objProviderRS.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objProviderRS.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objProviderRS.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", DateTime.Now));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objProviderRS.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objProviderRS.ModifiedOn));
            lstParameter.Add(new MySqlParameter("ProviderRelatedSchoolGuid", objProviderRS.ProviderRelatedSchoolGuid));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "ProviderRelatedSchool_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }


        public int SaveProviderRelatedSchoolAddressLK(ProviderRelatedSchoolsAddLK objProvider)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("ProviderRelatedSchoolId", objProvider.ProviderRelatedSchoolId));
            lstParameter.Add(new MySqlParameter("ProviderId", objProvider.ProviderId));
            lstParameter.Add(new MySqlParameter("ProviderNameId", objProvider.ProviderNameId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objProvider.ApplicationId));
            lstParameter.Add(new MySqlParameter("ProviderNameAddressId", objProvider.ProviderNameAddressId));
            lstParameter.Add(new MySqlParameter("ProviderRelatedSchoolAddressLkGuid", objProvider.ProviderRelatedSchoolAddressLkGuid));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "ProviderRelatedSchoolAddLK_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public int SaveProviderRelatedSchoolContactLK(ProviderRelatedSchoolsConLK objProvider)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("ProviderRelatedSchoolId", objProvider.ProviderRelatedSchoolId));
            lstParameter.Add(new MySqlParameter("ProviderId", objProvider.ProviderId));
            lstParameter.Add(new MySqlParameter("ProviderNameId", objProvider.ProviderNameId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objProvider.ApplicationId));
            lstParameter.Add(new MySqlParameter("ProviderNameContactId", objProvider.ProviderNameContactId));
            lstParameter.Add(new MySqlParameter("ProviderRelatedSchoolContactLkGuid", objProvider.ProviderRelatedSchoolContactLkGuid));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "ProviderRelatedSchoolConLK_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<ProviderRelatedSchools> Get_ProviderRelatedSchool_By_ProviderId(int ProviderId, int ApplicationId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ProviderId", ProviderId));
            lstParameter.Add(new MySqlParameter("ApplicationId", ApplicationId));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "providerRelatedSchool_Get_By_providerId", lstParameter.ToArray());

            List<ProviderRelatedSchools> lstEntity = null;
            ProviderRelatedSchools objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private ProviderRelatedSchools FetchEntity(DataRow dr)
        {
            ProviderRelatedSchools objEntity = new ProviderRelatedSchools();

            if (dr.Table.Columns.Contains("ProviderRelatedSchoolId") && dr["ProviderRelatedSchoolId"] != DBNull.Value)
            {
                objEntity.ProviderRelatedSchoolId = Convert.ToInt32(dr["ProviderRelatedSchoolId"]);
            }
            if (dr.Table.Columns.Contains("ProviderId") && dr["ProviderId"] != DBNull.Value)
            {
                objEntity.ProviderId = Convert.ToInt32(dr["ProviderId"]);
            }
            if (dr.Table.Columns.Contains("ProviderNameId") && dr["ProviderNameId"] != DBNull.Value)
            {
                objEntity.ProviderNameId = Convert.ToInt32(dr["ProviderNameId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationId") && dr["ApplicationId"] != DBNull.Value)
            {
                objEntity.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
            }
            if (dr.Table.Columns.Contains("DateAssociated") && dr["DateAssociated"] != DBNull.Value)
            {
                objEntity.DateAssociated = Convert.ToDateTime(dr["DateAssociated"]);
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
            if (dr.Table.Columns.Contains("ProviderRelatedSchoolGuid") && dr["ProviderRelatedSchoolGuid"] != DBNull.Value)
            {
                objEntity.ProviderRelatedSchoolGuid = Convert.ToString(dr["ProviderRelatedSchoolGuid"]);
            }


            if (dr.Table.Columns.Contains("ProviderName") && dr["ProviderName"] != DBNull.Value)
            {
                objEntity.ProviderName = Convert.ToString(dr["ProviderName"]);
            }
            if (dr.Table.Columns.Contains("StreetLine1") && dr["StreetLine1"] != DBNull.Value)
            {
                objEntity.StreetLine1 = Convert.ToString(dr["StreetLine1"]);
            }
            if (dr.Table.Columns.Contains("StreetLine2") && dr["StreetLine2"] != DBNull.Value)
            {
                objEntity.StreetLine2 = Convert.ToString(dr["StreetLine2"]);
            }
            if (dr.Table.Columns.Contains("City") && dr["City"] != DBNull.Value)
            {
                objEntity.City = Convert.ToString(dr["City"]);
            }
            if (dr.Table.Columns.Contains("StateCode") && dr["StateCode"] != DBNull.Value)
            {
                objEntity.StateCode = Convert.ToString(dr["StateCode"]);
            }
            if (dr.Table.Columns.Contains("Zip") && dr["Zip"] != DBNull.Value)
            {
                objEntity.ZIP = Convert.ToString(dr["Zip"]);
            }
            if (dr.Table.Columns.Contains("CountyId") && dr["CountyId"] != DBNull.Value)
            {
                objEntity.CountyId = Convert.ToInt32(dr["CountryId"]);
            }
            if (dr.Table.Columns.Contains("CountryId") && dr["CountryId"] != DBNull.Value)
            {
                objEntity.CountryId = Convert.ToInt32(dr["CountryId"]);
            }
            if (dr.Table.Columns.Contains("ContactFirstName") && dr["ContactFirstName"] != DBNull.Value)
            {
                objEntity.ContactFirstName = Convert.ToString(dr["ContactFirstName"]);
            }
            if (dr.Table.Columns.Contains("ContactLastName") && dr["ContactLastName"] != DBNull.Value)
            {
                objEntity.ContactLastName = Convert.ToString(dr["ContactLastName"]);
            }
            if (dr.Table.Columns.Contains("Phone") && dr["Phone"] != DBNull.Value)
            {
                objEntity.Phone = Convert.ToString(dr["Phone"]);
            }
            if (dr.Table.Columns.Contains("Website") && dr["Website"] != DBNull.Value)
            {
                objEntity.Website = Convert.ToString(dr["Website"]);
            }
            if (dr.Table.Columns.Contains("Email") && dr["Email"] != DBNull.Value)
            {
                objEntity.Email = Convert.ToString(dr["Email"]);
            }
            return objEntity;

        }
    }
}
