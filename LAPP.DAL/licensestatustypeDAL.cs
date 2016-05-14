using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class LicenseStatusTypeDAL : BaseDAL
    {
        public int Save_LicenseStatusType(LicenseStatusType objLicenseStatusType)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("LicenseStatusTypeId", objLicenseStatusType.LicenseStatusTypeId));
            lstParameter.Add(new MySqlParameter("LicenseStatusTypeCode", objLicenseStatusType.LicenseStatusTypeCode));
            lstParameter.Add(new MySqlParameter("LicenseStatusTypeName", objLicenseStatusType.LicenseStatusTypeName));
            lstParameter.Add(new MySqlParameter("StatusTypeColorCode", objLicenseStatusType.StatusTypeColorCode));
            lstParameter.Add(new MySqlParameter("EffectiveDate", objLicenseStatusType.EffectiveDate));
            lstParameter.Add(new MySqlParameter("EndDate", objLicenseStatusType.EndDate));
            lstParameter.Add(new MySqlParameter("IsActive", objLicenseStatusType.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objLicenseStatusType.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objLicenseStatusType.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objLicenseStatusType.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objLicenseStatusType.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objLicenseStatusType.ModifiedOn));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "licensestatustype_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<LicenseStatusType> Get_All_LicenseStatusType()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "LICENSESTATUSTYPE_GET_ALL");
            List<LicenseStatusType> lstEntity = new List<LicenseStatusType>();
            LicenseStatusType objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private LicenseStatusType FetchEntity(DataRow dr)
        {
            LicenseStatusType objEntity = new LicenseStatusType();
            if (dr.Table.Columns.Contains("LicenseStatusTypeId") && dr["LicenseStatusTypeId"] != DBNull.Value)
            {
                objEntity.LicenseStatusTypeId = Convert.ToInt32(dr["LicenseStatusTypeId"]);
            }
            if (dr.Table.Columns.Contains("LicenseStatusTypeCode") && dr["LicenseStatusTypeCode"] != DBNull.Value)
            {
                objEntity.LicenseStatusTypeCode = Convert.ToString(dr["LicenseStatusTypeCode"]);
            }
            if (dr.Table.Columns.Contains("LicenseStatusTypeName") && dr["LicenseStatusTypeName"] != DBNull.Value)
            {
                objEntity.LicenseStatusTypeName = Convert.ToString(dr["LicenseStatusTypeName"]);
            }
            if (dr.Table.Columns.Contains("StatusTypeColorCode") && dr["StatusTypeColorCode"] != DBNull.Value)
            {
                objEntity.StatusTypeColorCode = Convert.ToString(dr["StatusTypeColorCode"]);
            }
            if (dr.Table.Columns.Contains("EffectiveDate") && dr["EffectiveDate"] != DBNull.Value)
            {
                objEntity.EffectiveDate = Convert.ToDateTime(dr["EffectiveDate"]);
            }
            if (dr.Table.Columns.Contains("EndDate") && dr["EndDate"] != DBNull.Value)
            {
                objEntity.EndDate = Convert.ToDateTime(dr["EndDate"]);
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
