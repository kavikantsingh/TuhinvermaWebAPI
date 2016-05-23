using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class AddressDAL : BaseDAL
    {
        public int Save_address(Address objaddress)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("AddressId", objaddress.AddressId));
            lstParameter.Add(new MySqlParameter("Addressee",objaddress.Addressee.NullString()));
            lstParameter.Add(new MySqlParameter("StreetLine1", objaddress.StreetLine1.NullString()));
            lstParameter.Add(new MySqlParameter("StreetLine2", objaddress.StreetLine2.NullString()));
            lstParameter.Add(new MySqlParameter("City", objaddress.City.NullString()));
            lstParameter.Add(new MySqlParameter("StateCode", objaddress.StateCode.NullString()));
            lstParameter.Add(new MySqlParameter("Zip", objaddress.Zip.NullString()));
            lstParameter.Add(new MySqlParameter("CountyId", objaddress.CountyId));
            lstParameter.Add(new MySqlParameter("CountryId", objaddress.CountryId));
            lstParameter.Add(new MySqlParameter("DateValidated", objaddress.DateValidated));
            lstParameter.Add(new MySqlParameter("UseUserAddress", objaddress.UseUserAddress));
            lstParameter.Add(new MySqlParameter("UseVerifiedAddress", objaddress.UseVerifiedAddress));
            lstParameter.Add(new MySqlParameter("IsActive", objaddress.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objaddress.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objaddress.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objaddress.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objaddress.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objaddress.ModifiedOn));
            lstParameter.Add(new MySqlParameter("AddressGuid", objaddress.AddressGuid));
            lstParameter.Add(new MySqlParameter("Authenticator", objaddress.Authenticator));

            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "address_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        //public int Update_address(Address objAddress)
        //{
        //    DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
        //    lstParameter.Add(new MySqlParameter("U_AddressId", objAddress.AddressId));
        //    lstParameter.Add(new MySqlParameter("U_Addressee", objAddress.Addressee));
        //    lstParameter.Add(new MySqlParameter("U_StreetLine1", objAddress.StreetLine1));
        //    lstParameter.Add(new MySqlParameter("U_StreetLine2", objAddress.StreetLine2));
        //    lstParameter.Add(new MySqlParameter("U_City", objAddress.City));
        //    lstParameter.Add(new MySqlParameter("U_StateCode", objAddress.StateCode));
        //    lstParameter.Add(new MySqlParameter("U_Zip", objAddress.Zip));
        //    lstParameter.Add(new MySqlParameter("U_CountyId", objAddress.CountyId));
        //    lstParameter.Add(new MySqlParameter("U_CountryId", objAddress.CountryId));
        //    lstParameter.Add(new MySqlParameter("U_DateValidated", objAddress.DateValidated));
        //    lstParameter.Add(new MySqlParameter("U_UseUserAddress", objAddress.UseUserAddress));
        //    lstParameter.Add(new MySqlParameter("U_UseVerifiedAddress", objAddress.UseVerifiedAddress));
        //    lstParameter.Add(new MySqlParameter("U_IsActive", objAddress.IsActive));
        //    lstParameter.Add(new MySqlParameter("U_IsDeleted", objAddress.IsDeleted));
        //    lstParameter.Add(new MySqlParameter("U_CreatedBy", objAddress.CreatedBy));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedBy", objAddress.ModifiedBy));
        //    lstParameter.Add(new MySqlParameter("U_AddressGuid", objAddress.AddressGuid));
        //    lstParameter.Add(new MySqlParameter("U_Authenticator", objAddress.Authenticator));
        //    int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "address_Update", lstParameter.ToArray());
        //    return returnValue;
        //}


        public List<Address> Get_All_address()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "address_Get_All", lstParameter.ToArray());

            List<Address> lstEntity = new List<Address>();
            Address objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public Address Get_address_By_AddressId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_AddressId", ID));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ADDRESS_GET_BY_AddressId", lstParameter.ToArray());
            Address objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private Address FetchEntity(DataRow dr)
        {
            Address objEntity = new Address();
            if (dr.Table.Columns.Contains("AddressId") && dr["AddressId"] != DBNull.Value)
            {
                objEntity.AddressId = Convert.ToInt32(dr["AddressId"]);
            }
            if (dr.Table.Columns.Contains("Addressee") && dr["Addressee"] != DBNull.Value)
            {
                objEntity.Addressee = Convert.ToString(dr["Addressee"]);
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
                objEntity.Zip = Convert.ToString(dr["Zip"]);
            }
            if (dr.Table.Columns.Contains("CountyId") && dr["CountyId"] != DBNull.Value)
            {
                objEntity.CountyId = Convert.ToInt32(dr["CountyId"]);
            }
            if (dr.Table.Columns.Contains("CountryId") && dr["CountryId"] != DBNull.Value)
            {
                objEntity.CountryId = Convert.ToInt32(dr["CountryId"]);
            }
            if (dr.Table.Columns.Contains("DateValidated") && dr["DateValidated"] != DBNull.Value)
            {
                objEntity.DateValidated = Convert.ToDateTime(dr["DateValidated"]);
            }
            if (dr.Table.Columns.Contains("UseUserAddress") && dr["UseUserAddress"] != DBNull.Value)
            {
                objEntity.UseUserAddress = Convert.ToBoolean(dr["UseUserAddress"]);
            }
            if (dr.Table.Columns.Contains("UseVerifiedAddress") && dr["UseVerifiedAddress"] != DBNull.Value)
            {
                objEntity.UseVerifiedAddress = Convert.ToBoolean(dr["UseVerifiedAddress"]);
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
            if (dr.Table.Columns.Contains("AddressGuid") && dr["AddressGuid"] != DBNull.Value)
            {
                objEntity.AddressGuid = Convert.ToString(dr["AddressGuid"]);
            }
            if (dr.Table.Columns.Contains("Authenticator") && dr["Authenticator"] != DBNull.Value)
            {
                objEntity.Authenticator = Convert.ToString(dr["Authenticator"]);
            }
            return objEntity;

        }
    }
}
