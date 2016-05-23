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
    public class IndividualEmploymentAddressDAL : BaseDAL
    {
        public int Save_IndividualEmploymentAddress(IndividualEmploymentAddress objIndividualEmploymentAddress)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualEmploymentAddressId", objIndividualEmploymentAddress.IndividualEmploymentAddressId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualEmploymentAddress.IndividualId));
            lstParameter.Add(new MySqlParameter("AddressId", objIndividualEmploymentAddress.AddressId));
            lstParameter.Add(new MySqlParameter("IndividualEmploymentId", objIndividualEmploymentAddress.IndividualEmploymentId));
            lstParameter.Add(new MySqlParameter("AddressTypeId", objIndividualEmploymentAddress.AddressTypeId));
            lstParameter.Add(new MySqlParameter("BeginDate", objIndividualEmploymentAddress.BeginDate));
            lstParameter.Add(new MySqlParameter("EndDate", objIndividualEmploymentAddress.EndDate));
            lstParameter.Add(new MySqlParameter("IsMailingSameasPhysical", objIndividualEmploymentAddress.IsMailingSameasPhysical));
            lstParameter.Add(new MySqlParameter("IsActive", objIndividualEmploymentAddress.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualEmploymentAddress.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualEmploymentAddress.CreatedBy));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objIndividualEmploymentAddress.ModifiedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objIndividualEmploymentAddress.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objIndividualEmploymentAddress.ModifiedOn));
            lstParameter.Add(new MySqlParameter("IndividualEmploymentAddressGuid", objIndividualEmploymentAddress.IndividualEmploymentAddressGuid));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "individualemploymentaddress_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<IndividualEmploymentAddress> Get_All_IndividualEmploymentAddress()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualemploymentaddress_Get_All");
            List<IndividualEmploymentAddress> lstEntity = new List<IndividualEmploymentAddress>();
            IndividualEmploymentAddress objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public IndividualEmploymentAddress Get_IndividualEmploymentAddress_By_IndividualEmploymentAddressId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualEmploymentAddressId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualemploymentaddress_GET_BY_IndividualEmploymentAddressId", lstParameter.ToArray());
            IndividualEmploymentAddress objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<IndividualEmploymentAddress> Get_IndividualEmploymentAddress_By_IndividualEmploymentId(int IndividualEmploymentId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualEmploymentId", IndividualEmploymentId));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualemploymentaddress_Get_By_IndividualEmploymentId", lstParameter.ToArray());
            List<IndividualEmploymentAddress> lstEntity = new List<IndividualEmploymentAddress>();
            IndividualEmploymentAddress objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private IndividualEmploymentAddress FetchEntity(DataRow dr)
        {
            IndividualEmploymentAddress objEntity = new IndividualEmploymentAddress();
            if (dr.Table.Columns.Contains("IndividualEmploymentAddressId") && dr["IndividualEmploymentAddressId"] != DBNull.Value)
            {
                objEntity.IndividualEmploymentAddressId = Convert.ToInt32(dr["IndividualEmploymentAddressId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("AddressId") && dr["AddressId"] != DBNull.Value)
            {
                objEntity.AddressId = Convert.ToInt32(dr["AddressId"]);
            }
            if (dr.Table.Columns.Contains("IndividualEmploymentId") && dr["IndividualEmploymentId"] != DBNull.Value)
            {
                objEntity.IndividualEmploymentId = Convert.ToInt32(dr["IndividualEmploymentId"]);
            }
            if (dr.Table.Columns.Contains("AddressTypeId") && dr["AddressTypeId"] != DBNull.Value)
            {
                objEntity.AddressTypeId = Convert.ToInt32(dr["AddressTypeId"]);
            }
            if (dr.Table.Columns.Contains("BeginDate") && dr["BeginDate"] != DBNull.Value)
            {
                objEntity.BeginDate = Convert.ToDateTime(dr["BeginDate"]);
            }
            if (dr.Table.Columns.Contains("EndDate") && dr["EndDate"] != DBNull.Value)
            {
                objEntity.EndDate = Convert.ToDateTime(dr["EndDate"]);
            }
            if (dr.Table.Columns.Contains("IsMailingSameasPhysical") && dr["IsMailingSameasPhysical"] != DBNull.Value)
            {
                objEntity.IsMailingSameasPhysical = Convert.ToBoolean(dr["IsMailingSameasPhysical"]);
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
            if (dr.Table.Columns.Contains("IndividualEmploymentAddressGuid") && dr["IndividualEmploymentAddressGuid"] != DBNull.Value)
            {
                objEntity.IndividualEmploymentAddressGuid =  dr["IndividualEmploymentAddressGuid"].ToString();
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
            return objEntity;

        }
    }
}
