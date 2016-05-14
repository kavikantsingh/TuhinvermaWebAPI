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
    public class LicenseTypeDAL : BaseDAL
    {
        public int Save_LicenseType(LicenseType objLicenseType)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("LicenseTypeId", objLicenseType.LicenseTypeId));
            lstParameter.Add(new MySqlParameter("LicenseTypeCode", objLicenseType.LicenseTypeCode));
            lstParameter.Add(new MySqlParameter("LicenseTypeName", objLicenseType.LicenseTypeName));
            lstParameter.Add(new MySqlParameter("IsActive", objLicenseType.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objLicenseType.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objLicenseType.CreatedBy));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objLicenseType.ModifiedBy));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "licensetype_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<LicenseType> Get_All_LicenseType()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "licensetype_Get_All");
            List<LicenseType> lstEntity = new List<LicenseType>();
            LicenseType objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private LicenseType FetchEntity(DataRow dr)
        {
            LicenseType objEntity = new LicenseType();
            if (dr.Table.Columns.Contains("LicenseTypeId") && dr["LicenseTypeId"] != DBNull.Value)
            {
                objEntity.LicenseTypeId = Convert.ToInt32(dr["LicenseTypeId"]);
            }
            if (dr.Table.Columns.Contains("LicenseTypeCode") && dr["LicenseTypeCode"] != DBNull.Value)
            {
                objEntity.LicenseTypeCode = Convert.ToString(dr["LicenseTypeCode"]);
            }
            if (dr.Table.Columns.Contains("LicenseTypeName") && dr["LicenseTypeName"] != DBNull.Value)
            {
                objEntity.LicenseTypeName = Convert.ToString(dr["LicenseTypeName"]);
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
