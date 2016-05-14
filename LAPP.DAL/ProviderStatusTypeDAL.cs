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
    public class ProviderStatusTypeDAL : BaseDAL
    {
        public int Save_ProviderStatusType(ProviderStatusType objProviderStatusType)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ProviderStatusTypeId", objProviderStatusType.ProviderStatusTypeId));
            lstParameter.Add(new MySqlParameter("ProviderStatusTypeCode", objProviderStatusType.ProviderStatusTypeCode));
            lstParameter.Add(new MySqlParameter("ProviderStatusTypeName", objProviderStatusType.ProviderStatusTypeName));
            lstParameter.Add(new MySqlParameter("StatusTypeColorCode", objProviderStatusType.StatusTypeColorCode));
            lstParameter.Add(new MySqlParameter("EffectiveDate", objProviderStatusType.EffectiveDate));
            lstParameter.Add(new MySqlParameter("EndDate", objProviderStatusType.EndDate));
            lstParameter.Add(new MySqlParameter("IsActive", objProviderStatusType.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objProviderStatusType.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objProviderStatusType.CreatedBy));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objProviderStatusType.ModifiedBy));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "providerstatustype_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<ProviderStatusType> Get_All_ProviderStatusType()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "PROVIDERSTATUSTYPE_GET_ALL");
            List<ProviderStatusType> lstEntity = new List<ProviderStatusType>();
            ProviderStatusType objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public ProviderStatusType Get_Provider_By_ProviderStatusTypeId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_ProviderStatusTypeId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "providerstatustype_Get_By_ProviderStatusTypeId", lstParameter.ToArray());
            ProviderStatusType objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private ProviderStatusType FetchEntity(DataRow dr)
        {
            ProviderStatusType objEntity = new ProviderStatusType();
            if (dr.Table.Columns.Contains("ProviderStatusTypeId") && dr["ProviderStatusTypeId"] != DBNull.Value)
            {
                objEntity.ProviderStatusTypeId = Convert.ToInt32(dr["ProviderStatusTypeId"]);
            }
            if (dr.Table.Columns.Contains("ProviderStatusTypeCode") && dr["ProviderStatusTypeCode"] != DBNull.Value)
            {
                objEntity.ProviderStatusTypeCode = Convert.ToString(dr["ProviderStatusTypeCode"]);
            }
            if (dr.Table.Columns.Contains("ProviderStatusTypeName") && dr["ProviderStatusTypeName"] != DBNull.Value)
            {
                objEntity.ProviderStatusTypeName = Convert.ToString(dr["ProviderStatusTypeName"]);
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
