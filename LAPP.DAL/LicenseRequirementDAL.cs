using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class LicenseRequirementDAL : BaseDAL
    {
        public int Save_LicenseRequirement(LicenseRequirement objLicenseRequirement)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("LicenseRequirementId", objLicenseRequirement.LicenseRequirementId));
            lstParameter.Add(new MySqlParameter("Text", objLicenseRequirement.Text));
            lstParameter.Add(new MySqlParameter("Code", objLicenseRequirement.Code));

            lstParameter.Add(new MySqlParameter("IsActive", objLicenseRequirement.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objLicenseRequirement.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objLicenseRequirement.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objLicenseRequirement.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objLicenseRequirement.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objLicenseRequirement.ModifiedOn));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "licenserequirement_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<LicenseRequirement> Get_All_LicenseRequirement()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ADDRESSTYPE_GET_ALL");
            List<LicenseRequirement> lstEntity = new List<LicenseRequirement>();
            LicenseRequirement objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public LicenseRequirement Get_LicenseRequirement_byLicenseRequirementId(int AddressId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParam = new List<MySqlParameter>();
            lstParam.Add(new MySqlParameter("G_LicenseRequirementId", AddressId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "addresstype_Get_By_LicenseRequirementId");

            LicenseRequirement objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);

            }
            return objEntity;
        }

        private LicenseRequirement FetchEntity(DataRow dr)
        {
            LicenseRequirement objEntity = new LicenseRequirement();


            if (dr.Table.Columns.Contains("LicenseRequirementId") && dr["LicenseRequirementId"] != DBNull.Value)
            {
                objEntity.LicenseRequirementId = Convert.ToInt32(dr["LicenseRequirementId"]);
            }
            if (dr.Table.Columns.Contains("Text") && dr["Text"] != DBNull.Value)
            {
                objEntity.Text = Convert.ToString(dr["Text"]);
            }
            if (dr.Table.Columns.Contains("Code") && dr["Code"] != DBNull.Value)
            {
                objEntity.Code = Convert.ToString(dr["Code"]);
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
