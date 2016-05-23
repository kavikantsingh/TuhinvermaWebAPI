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
    public class RevFeeMasterDAL : BaseDAL
    {
        public int Save_RevFeeMaster(RevFeeMaster objRevFeeMaster)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("RevFeeMasterId", objRevFeeMaster.RevFeeMasterId));
            lstParameter.Add(new MySqlParameter("FeeAccountCode", objRevFeeMaster.FeeAccountCode));
            lstParameter.Add(new MySqlParameter("FeeName", objRevFeeMaster.FeeName));
            lstParameter.Add(new MySqlParameter("FeeAmount", objRevFeeMaster.FeeAmount));
            lstParameter.Add(new MySqlParameter("FeeTypeId", objRevFeeMaster.FeeTypeId));
            lstParameter.Add(new MySqlParameter("LicenseTypeId", objRevFeeMaster.LicenseTypeId));
            lstParameter.Add(new MySqlParameter("FiscalYear", objRevFeeMaster.FiscalYear));
            lstParameter.Add(new MySqlParameter("EffectiveDate", objRevFeeMaster.EffectiveDate));
            lstParameter.Add(new MySqlParameter("EndDate", objRevFeeMaster.EndDate));
            lstParameter.Add(new MySqlParameter("FeeAcctId", objRevFeeMaster.FeeAcctId));
            lstParameter.Add(new MySqlParameter("FeeFundId", objRevFeeMaster.FeeFundId));
            lstParameter.Add(new MySqlParameter("FeeGrantId", objRevFeeMaster.FeeGrantId));
            lstParameter.Add(new MySqlParameter("FeeProjectId", objRevFeeMaster.FeeProjectId));
            lstParameter.Add(new MySqlParameter("FeeOrder", objRevFeeMaster.FeeOrder));

            lstParameter.Add(new MySqlParameter("IsFeeOverrideAllowed", objRevFeeMaster.IsFeeOverrideAllowed));
            lstParameter.Add(new MySqlParameter("IsFeeExemptionAllowed", objRevFeeMaster.IsFeeExemptionAllowed));
            lstParameter.Add(new MySqlParameter("IsRecurringFee", objRevFeeMaster.IsRecurringFee));
            lstParameter.Add(new MySqlParameter("IsFeeRefundable", objRevFeeMaster.IsFeeRefundable));

            lstParameter.Add(new MySqlParameter("IsActive", objRevFeeMaster.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objRevFeeMaster.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objRevFeeMaster.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objRevFeeMaster.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objRevFeeMaster.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objRevFeeMaster.ModifiedOn));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "RevFeeMaster_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<RevFeeMaster> Get_RevFeeMaster_By_LicenseTypeId(int? licenseTypeId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_licenseTypeId", licenseTypeId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "RevFeeMaster_GET_BY_LicenseTypeId", lstParameter.ToArray());
            List<RevFeeMaster> lstEntity = new List<RevFeeMaster>();
            RevFeeMaster objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<RevFeeMaster> Get_All_RevFeeMaster()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "RevFeeMaster_GET_ALL");
            List<RevFeeMaster> lstEntity = new List<RevFeeMaster>();
            RevFeeMaster objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public RevFeeMaster Get_RevFeeMaster_By_RevFeeMasterId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_RevFeeMasterId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "RevFeeMaster_GET_BY_RevFeeMasterId", lstParameter.ToArray());
            RevFeeMaster objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private RevFeeMaster FetchEntity(DataRow dr)
        {
            RevFeeMaster objEntity = new RevFeeMaster();

            if (dr.Table.Columns.Contains("RevFeeMasterId") && dr["RevFeeMasterId"] != DBNull.Value)
            {
                objEntity.RevFeeMasterId = Convert.ToInt32(dr["RevFeeMasterId"]);
            }
            if (dr.Table.Columns.Contains("FeeAccountCode") && dr["FeeAccountCode"] != DBNull.Value)
            {
                objEntity.FeeAccountCode = Convert.ToString(dr["FeeAccountCode"]);
            }
            if (dr.Table.Columns.Contains("FeeName") && dr["FeeName"] != DBNull.Value)
            {
                objEntity.FeeName = Convert.ToString(dr["FeeName"]);
            }
            if (dr.Table.Columns.Contains("FeeAmount") && dr["FeeAmount"] != DBNull.Value)
            {
                objEntity.FeeAmount = Convert.ToInt32(dr["FeeAmount"]);
            }
            if (dr.Table.Columns.Contains("FeeTypeId") && dr["FeeTypeId"] != DBNull.Value)
            {
                objEntity.FeeTypeId = Convert.ToInt32(dr["FeeTypeId"]);
            }
            if (dr.Table.Columns.Contains("LicenseTypeId") && dr["LicenseTypeId"] != DBNull.Value)
            {
                objEntity.LicenseTypeId = Convert.ToInt32(dr["LicenseTypeId"]);
            }
            if (dr.Table.Columns.Contains("FiscalYear") && dr["FiscalYear"] != DBNull.Value)
            {
                objEntity.FiscalYear = Convert.ToInt32(dr["FiscalYear"]);
            }
            if (dr.Table.Columns.Contains("EffectiveDate") && dr["EffectiveDate"] != DBNull.Value)
            {
                objEntity.EffectiveDate = Convert.ToDateTime(dr["EffectiveDate"]);
            }
            if (dr.Table.Columns.Contains("EndDate") && dr["EndDate"] != DBNull.Value)
            {
                objEntity.EndDate = Convert.ToDateTime(dr["EndDate"]);
            }
            if (dr.Table.Columns.Contains("FeeAcctId") && dr["FeeAcctId"] != DBNull.Value)
            {
                objEntity.FeeAcctId = Convert.ToInt32(dr["FeeAcctId"]);
            }
            if (dr.Table.Columns.Contains("FeeFundId") && dr["FeeFundId"] != DBNull.Value)
            {
                objEntity.FeeFundId = Convert.ToInt32(dr["FeeFundId"]);
            }
            if (dr.Table.Columns.Contains("FeeGrantId") && dr["FeeGrantId"] != DBNull.Value)
            {
                objEntity.FeeGrantId = Convert.ToInt32(dr["FeeGrantId"]);
            }
            if (dr.Table.Columns.Contains("FeeProjectId") && dr["FeeProjectId"] != DBNull.Value)
            {
                objEntity.FeeProjectId = Convert.ToInt32(dr["FeeProjectId"]);
            }
            if (dr.Table.Columns.Contains("FeeOrder") && dr["FeeOrder"] != DBNull.Value)
            {
                objEntity.FeeOrder = Convert.ToInt32(dr["FeeOrder"]);
            }
            if (dr.Table.Columns.Contains("IsFeeOverrideAllowed") && dr["IsFeeOverrideAllowed"] != DBNull.Value)
            {
                objEntity.IsFeeOverrideAllowed = Convert.ToBoolean(dr["IsFeeOverrideAllowed"]);
            }
            if (dr.Table.Columns.Contains("IsFeeExemptionAllowed") && dr["IsFeeExemptionAllowed"] != DBNull.Value)
            {
                objEntity.IsFeeExemptionAllowed = Convert.ToBoolean(dr["IsFeeExemptionAllowed"]);
            }
            if (dr.Table.Columns.Contains("IsRecurringFee") && dr["IsRecurringFee"] != DBNull.Value)
            {
                objEntity.IsRecurringFee = Convert.ToBoolean(dr["IsRecurringFee"]);
            }
            if (dr.Table.Columns.Contains("IsFeeRefundable") && dr["IsFeeRefundable"] != DBNull.Value)
            {
                objEntity.IsFeeRefundable = Convert.ToBoolean(dr["IsFeeRefundable"]);
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
