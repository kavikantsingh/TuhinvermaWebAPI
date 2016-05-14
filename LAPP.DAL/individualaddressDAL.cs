using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class IndividualAddressDAL : BaseDAL
    {
        public int Save_IndividualAddress(IndividualAddress objIndividualAddress)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualAddressId", objIndividualAddress.IndividualAddressId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualAddress.IndividualId));
            lstParameter.Add(new MySqlParameter("AddressId", objIndividualAddress.AddressId));
            lstParameter.Add(new MySqlParameter("AddressTypeId", objIndividualAddress.AddressTypeId));
            lstParameter.Add(new MySqlParameter("BeginDate", objIndividualAddress.BeginDate));
            lstParameter.Add(new MySqlParameter("EndDate", objIndividualAddress.EndDate));
            lstParameter.Add(new MySqlParameter("IsMailingSameasPhysical", objIndividualAddress.IsMailingSameasPhysical));
            lstParameter.Add(new MySqlParameter("IsActive", objIndividualAddress.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualAddress.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualAddress.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objIndividualAddress.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objIndividualAddress.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objIndividualAddress.ModifiedOn));
            lstParameter.Add(new MySqlParameter("IndividualAddressGuid", objIndividualAddress.IndividualAddressGuid));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "individualaddress_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<IndividualAddress> Get_All_IndividualAddress()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "INDIVIDUALADDRESS_GET_ALL");
            List<IndividualAddress> lstEntity = new List<IndividualAddress>();
            IndividualAddress objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<IndividualAddress> Get_IndividualAddress_By_IndividualId(int IndividualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "INDIVIDUALADDRESS_GET_BY_IndividualId", lstParameter.ToArray());
            List<IndividualAddress> lstEntity = new List<IndividualAddress>();
            IndividualAddress objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public IndividualAddress Get_IndividualAddress_By_IndividualAddressId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualAddressId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "INDIVIDUALADDRESS_GET_BY_IndividualAddressId", lstParameter.ToArray());
            IndividualAddress objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private IndividualAddress FetchEntity(DataRow dr)
        {
            IndividualAddress objEntity = new IndividualAddress();
            if (dr.Table.Columns.Contains("IndividualAddressId") && dr["IndividualAddressId"] != DBNull.Value)
            {
                objEntity.IndividualAddressId = Convert.ToInt32(dr["IndividualAddressId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
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
            if (dr.Table.Columns.Contains("IndividualAddressGuid") && dr["IndividualAddressGuid"] != DBNull.Value)
            {
                objEntity.IndividualAddressGuid = Convert.ToString(dr["IndividualAddressGuid"]);
            }
            return objEntity;

        }
    }
}
