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
    public class ProviderAddressDAL : BaseDAL
    {
        public int Save_ProviderAddress(ProviderAddress objProviderAddress)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ProviderAddressId", objProviderAddress.ProviderAddressId));
            lstParameter.Add(new MySqlParameter("ProviderId", objProviderAddress.ProviderId));
            lstParameter.Add(new MySqlParameter("AddressId", objProviderAddress.AddressId));
            lstParameter.Add(new MySqlParameter("AddressTypeId", objProviderAddress.AddressTypeId));
            lstParameter.Add(new MySqlParameter("BeginDate", objProviderAddress.BeginDate));
            lstParameter.Add(new MySqlParameter("EndDate", objProviderAddress.EndDate));
            lstParameter.Add(new MySqlParameter("IsMailingSameasPhysical", objProviderAddress.IsMailingSameasPhysical));
            lstParameter.Add(new MySqlParameter("IsActive", objProviderAddress.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objProviderAddress.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objProviderAddress.CreatedBy));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objProviderAddress.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ProviderAddressGuid", objProviderAddress.ProviderAddressGuid));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "provideraddress_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<ProviderAddress> Get_All_ProviderAddress()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "provideraddress_Get_All");
            List<ProviderAddress> lstEntity = new List<ProviderAddress>();
            ProviderAddress objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public ProviderAddress Get_ProviderAddress_By_ProviderAddressId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_ProviderAddressId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "provideraddress_Get_By_provideraddressId", lstParameter.ToArray());
            ProviderAddress objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private ProviderAddress FetchEntity(DataRow dr)
        {
            ProviderAddress objEntity = new ProviderAddress();
            if (dr.Table.Columns.Contains("ProviderAddressId") && dr["ProviderAddressId"] != DBNull.Value)
            {
                objEntity.ProviderAddressId = Convert.ToInt32(dr["ProviderAddressId"]);
            }
            if (dr.Table.Columns.Contains("ProviderId") && dr["ProviderId"] != DBNull.Value)
            {
                objEntity.ProviderId = Convert.ToInt32(dr["ProviderId"]);
            }
            if (dr.Table.Columns.Contains("AddressId") && dr["AddressId"] != DBNull.Value)
            {
                objEntity.AddressId = Convert.ToInt32(dr["AddressId"]);
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
            if (dr.Table.Columns.Contains("ProviderAddressGuid") && dr["ProviderAddressGuid"] != DBNull.Value)
            {
                objEntity.ProviderAddressGuid = (Guid)dr["ProviderAddressGuid"];
            }
            return objEntity;

        }
    }
}
