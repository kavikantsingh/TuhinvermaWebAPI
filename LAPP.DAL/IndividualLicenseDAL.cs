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
    public class IndividualLicenseDAL : BaseDAL
    {
        public int Save_IndividualLicense(IndividualLicense objIndividualLicense)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualLicenseId", objIndividualLicense.IndividualLicenseId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualLicense.IndividualId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objIndividualLicense.ApplicationId));
            lstParameter.Add(new MySqlParameter("ApplicationTypeId", objIndividualLicense.ApplicationTypeId));
            lstParameter.Add(new MySqlParameter("LicenseTypeId", objIndividualLicense.LicenseTypeId));
            lstParameter.Add(new MySqlParameter("IsLicenseTemporary", objIndividualLicense.IsLicenseTemporary));
            lstParameter.Add(new MySqlParameter("IsLicenseActive", objIndividualLicense.IsLicenseActive));
            lstParameter.Add(new MySqlParameter("LicenseNumber", objIndividualLicense.LicenseNumber));
            lstParameter.Add(new MySqlParameter("OriginalLicenseDate", objIndividualLicense.OriginalLicenseDate));
            lstParameter.Add(new MySqlParameter("LicenseEffectiveDate", objIndividualLicense.LicenseEffectiveDate));
            lstParameter.Add(new MySqlParameter("LicenseExpirationDate", objIndividualLicense.LicenseExpirationDate));
            lstParameter.Add(new MySqlParameter("LicenseStatusTypeId", objIndividualLicense.LicenseStatusTypeId));
            lstParameter.Add(new MySqlParameter("IsActive", objIndividualLicense.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualLicense.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualLicense.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objIndividualLicense.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objIndividualLicense.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objIndividualLicense.ModifiedOn));
            lstParameter.Add(new MySqlParameter("IndividualLicenseGuid", objIndividualLicense.IndividualLicenseGuid));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "individuallicense_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public IndividualLicense Get_Latest_IndividualLicense_By_IndividualId(int IndividualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individuallicense_GET_Latest_BY_IndividualId", lstParameter.ToArray());

            IndividualLicense objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);

            }
            return objEntity;
        }

        public List<IndividualLicense> Get_All_IndividualLicense()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "INDIVIDUALLICENSE_GET_ALL");
            List<IndividualLicense> lstEntity = new List<IndividualLicense>();
            IndividualLicense objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public IndividualLicense Get_IndividualLicense_By_IndividualLicenseId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualLicenseId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individuallicense_GET_BY_IndividualLicenseId", lstParameter.ToArray());
            IndividualLicense objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public IndividualLicense Get_Pending_IndividualLicense_By_IndividualId(int IndividualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individuallicense_GET_Pending_BY_IndividualId", lstParameter.ToArray());

            IndividualLicense objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);

            }
            return objEntity;
        }

        public List<IndividualLicense> Get_IndividualLicense_By_IndividualId(int IndividualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individuallicense_GET_BY_IndividualId", lstParameter.ToArray());
            List<IndividualLicense> lstEntity = new List<IndividualLicense>();
            IndividualLicense objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<IndividualLicense> GetALL_IndividualLicense_By_IndividualId(int IndividualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individuallicense_GETALL_BY_IndividualId", lstParameter.ToArray());
            List<IndividualLicense> lstEntity = new List<IndividualLicense>();
            IndividualLicense objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private IndividualLicense FetchEntity(DataRow dr)
        {
            IndividualLicense objEntity = new IndividualLicense();
            if (dr.Table.Columns.Contains("IndividualLicenseId") && dr["IndividualLicenseId"] != DBNull.Value)
            {
                objEntity.IndividualLicenseId = Convert.ToInt32(dr["IndividualLicenseId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationId") && dr["ApplicationId"] != DBNull.Value)
            {
                objEntity.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationTypeId") && dr["ApplicationTypeId"] != DBNull.Value)
            {
                objEntity.ApplicationTypeId = Convert.ToInt32(dr["ApplicationTypeId"]);
            }
            if (dr.Table.Columns.Contains("LicenseTypeId") && dr["LicenseTypeId"] != DBNull.Value)
            {
                objEntity.LicenseTypeId = Convert.ToInt32(dr["LicenseTypeId"]);
            }
            if (dr.Table.Columns.Contains("IsLicenseTemporary") && dr["IsLicenseTemporary"] != DBNull.Value)
            {
                objEntity.IsLicenseTemporary = Convert.ToBoolean(dr["IsLicenseTemporary"]);
            }
            if (dr.Table.Columns.Contains("IsLicenseActive") && dr["IsLicenseActive"] != DBNull.Value)
            {
                objEntity.IsLicenseActive = Convert.ToBoolean(dr["IsLicenseActive"]);
            }
            if (dr.Table.Columns.Contains("LicenseNumber") && dr["LicenseNumber"] != DBNull.Value)
            {
                objEntity.LicenseNumber = Convert.ToString(dr["LicenseNumber"]);
            }
            if (dr.Table.Columns.Contains("OriginalLicenseDate") && dr["OriginalLicenseDate"] != DBNull.Value)
            {
                objEntity.OriginalLicenseDate = Convert.ToDateTime(dr["OriginalLicenseDate"]);
            }
            if (dr.Table.Columns.Contains("LicenseEffectiveDate") && dr["LicenseEffectiveDate"] != DBNull.Value)
            {
                objEntity.LicenseEffectiveDate = Convert.ToDateTime(dr["LicenseEffectiveDate"]);
            }
            if (dr.Table.Columns.Contains("LicenseExpirationDate") && dr["LicenseExpirationDate"] != DBNull.Value)
            {
                objEntity.LicenseExpirationDate = Convert.ToDateTime(dr["LicenseExpirationDate"]);
            }
            if (dr.Table.Columns.Contains("LicenseStatusTypeId") && dr["LicenseStatusTypeId"] != DBNull.Value)
            {
                objEntity.LicenseStatusTypeId = Convert.ToInt32(dr["LicenseStatusTypeId"]);
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
            if (dr.Table.Columns.Contains("IndividualLicenseGuid") && dr["IndividualLicenseGuid"] != DBNull.Value)
            {
                objEntity.IndividualLicenseGuid = Convert.ToString(dr["IndividualLicenseGuid"]);
            }

            if (dr.Table.Columns.Contains("LicenseStatusTypeCode") && dr["LicenseStatusTypeCode"] != DBNull.Value)
            {
                objEntity.LicenseStatusTypeCode = Convert.ToString(dr["LicenseStatusTypeCode"]);
            }
            if (dr.Table.Columns.Contains("LicenseStatusTypeName") && dr["LicenseStatusTypeName"] != DBNull.Value)
            {
                objEntity.LicenseStatusTypeName = Convert.ToString(dr["LicenseStatusTypeName"]);
            }
            if (dr.Table.Columns.Contains("LicenseTypeName") && dr["LicenseTypeName"] != DBNull.Value)
            {
                objEntity.LicenseTypeName = Convert.ToString(dr["LicenseTypeName"]);
            }

            if (dr.Table.Columns.Contains("LicenseDetail") && dr["LicenseDetail"] != DBNull.Value)
            {
                objEntity.LicenseDetail = Convert.ToString(dr["LicenseDetail"]);
            }
            if (dr.Table.Columns.Contains("LicenseStatusColorCode") && dr["LicenseStatusColorCode"] != DBNull.Value)
            {
                objEntity.LicenseStatusColorCode = Convert.ToString(dr["LicenseStatusColorCode"]);
            }

            return objEntity;

        }
    }
}
