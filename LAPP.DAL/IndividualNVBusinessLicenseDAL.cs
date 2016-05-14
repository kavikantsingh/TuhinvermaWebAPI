using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class IndividualNVBusinessLicenseDAL : BaseDAL
    {
        public int Save_IndividualNVBusinessLicense(IndividualNVBusinessLicense objIndividualNVBusinessLicense)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualNVBusinessLicenseId", objIndividualNVBusinessLicense.IndividualNVBusinessLicenseId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualNVBusinessLicense.IndividualId));
            lstParameter.Add(new MySqlParameter("ContentItemLkId", objIndividualNVBusinessLicense.ContentItemLkId));
            lstParameter.Add(new MySqlParameter("ContentItemHash", objIndividualNVBusinessLicense.ContentItemHash));
            lstParameter.Add(new MySqlParameter("ContentItemResponse", objIndividualNVBusinessLicense.ContentItemResponse));
            lstParameter.Add(new MySqlParameter("Status", objIndividualNVBusinessLicense.Status));
            lstParameter.Add(new MySqlParameter("NameonBusinessLicense", objIndividualNVBusinessLicense.NameonBusinessLicense));
            lstParameter.Add(new MySqlParameter("BusinessLicenseHash", objIndividualNVBusinessLicense.BusinessLicenseHash));

            lstParameter.Add(new MySqlParameter("IsActive", objIndividualNVBusinessLicense.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualNVBusinessLicense.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualNVBusinessLicense.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objIndividualNVBusinessLicense.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objIndividualNVBusinessLicense.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objIndividualNVBusinessLicense.ModifiedOn));

            lstParameter.Add(new MySqlParameter("IndividualNVBusinessLicenseGuid", objIndividualNVBusinessLicense.IndividualNVBusinessLicenseGuid));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "individualnvbusinesslicense_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<IndividualNVBusinessLicense> Get_All_IndividualNVBusinessLicense()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualNVBusinessLicense_Get_All");
            List<IndividualNVBusinessLicense> lstEntity = new List<IndividualNVBusinessLicense>();
            IndividualNVBusinessLicense objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public IndividualNVBusinessLicense Get_IndividualNVBusinessLicense_By_IndividualNVBusinessLicenseId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualNVBusinessLicenseId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualNVBusinessLicense_GET_BY_IndividualNVBusinessLicenseId", lstParameter.ToArray());
            IndividualNVBusinessLicense objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private IndividualNVBusinessLicense FetchEntity(DataRow dr)
        {
            IndividualNVBusinessLicense objEntity = new IndividualNVBusinessLicense();

            if (dr.Table.Columns.Contains("IndividualNVBusinessLicenseId") && dr["IndividualNVBusinessLicenseId"] != DBNull.Value)
            {
                objEntity.IndividualNVBusinessLicenseId = Convert.ToInt32(dr["IndividualNVBusinessLicenseId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("ContentItemLkId") && dr["ContentItemLkId"] != DBNull.Value)
            {
                objEntity.ContentItemLkId = Convert.ToInt32(dr["ContentItemLkId"]);
            }
            if (dr.Table.Columns.Contains("ContentItemHash") && dr["ContentItemHash"] != DBNull.Value)
            {
                objEntity.ContentItemHash = Convert.ToInt32(dr["ContentItemHash"]);
            }
            if (dr.Table.Columns.Contains("ContentItemResponse") && dr["ContentItemResponse"] != DBNull.Value)
            {
                objEntity.ContentItemResponse = Convert.ToBoolean(dr["ContentItemResponse"]);
            }
            if (dr.Table.Columns.Contains("Status") && dr["Status"] != DBNull.Value)
            {
                objEntity.Status = Convert.ToString(dr["Status"]);
            }
            if (dr.Table.Columns.Contains("NameonBusinessLicense") && dr["NameonBusinessLicense"] != DBNull.Value)
            {
                objEntity.NameonBusinessLicense = Convert.ToString(dr["NameonBusinessLicense"]);
            }
            if (dr.Table.Columns.Contains("BusinessLicenseHash") && dr["BusinessLicenseHash"] != DBNull.Value)
            {
                objEntity.BusinessLicenseHash = Convert.ToString(dr["BusinessLicenseHash"]);
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

            if (dr.Table.Columns.Contains("IndividualNVBusinessLicenseGuid") && dr["IndividualNVBusinessLicenseGuid"] != DBNull.Value)
            {
                objEntity.IndividualNVBusinessLicenseGuid = Convert.ToString(dr["IndividualNVBusinessLicenseGuid"]);
            }
            return objEntity;

        }
    }
}
