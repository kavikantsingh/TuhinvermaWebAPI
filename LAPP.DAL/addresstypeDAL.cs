using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class AddressTypeDAL : BaseDAL
    {
        public int Save_AddressType(AddressType objAddressType)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("AddressTypeId", objAddressType.AddressTypeId));
            lstParameter.Add(new MySqlParameter("AddressTypeCode", objAddressType.AddressTypeCode));
            lstParameter.Add(new MySqlParameter("AddressTypeDesc", objAddressType.AddressTypeDesc));

            lstParameter.Add(new MySqlParameter("IsActive", objAddressType.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objAddressType.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objAddressType.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objAddressType.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objAddressType.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objAddressType.ModifiedOn));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "addresstype_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<AddressType> Get_All_AddressType()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ADDRESSTYPE_GET_ALL");
            List<AddressType> lstEntity = new List<AddressType>();
            AddressType objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public AddressType Get_AddressType_byAddressTypeId(int AddressId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParam = new List<MySqlParameter>();
            lstParam.Add(new MySqlParameter("G_AddressTypeId", AddressId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "addresstype_Get_By_AddressTypeId");

            AddressType objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);

            }
            return objEntity;
        }

        private AddressType FetchEntity(DataRow dr)
        {
            AddressType objEntity = new AddressType();
            if (dr.Table.Columns.Contains("AddressTypeId") && dr["AddressTypeId"] != DBNull.Value)
            {
                objEntity.AddressTypeId = Convert.ToInt32(dr["AddressTypeId"]);
            }
            if (dr.Table.Columns.Contains("AddressTypeCode") && dr["AddressTypeCode"] != DBNull.Value)
            {
                objEntity.AddressTypeCode = Convert.ToString(dr["AddressTypeCode"]);
            }
            if (dr.Table.Columns.Contains("AddressTypeDesc") && dr["AddressTypeDesc"] != DBNull.Value)
            {
                objEntity.AddressTypeDesc = Convert.ToString(dr["AddressTypeDesc"]);
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
