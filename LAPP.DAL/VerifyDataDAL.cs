using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;

namespace LAPP.DAL
{
    public class VerifyDataDAL : BaseDAL
    {
        public List<VerifyDataEntity> Get_All_VerifyData()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Get_All_VerifyData", lstParameter.ToArray());

            List<VerifyDataEntity> lstEntity = new List<VerifyDataEntity>();
            VerifyDataEntity objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }
        private VerifyDataEntity FetchEntity(DataRow dr)
        {
            VerifyDataEntity objEntity = new VerifyDataEntity();
            if (dr.Table.Columns.Contains("FirstName") && dr["FirstName"] != DBNull.Value)
            {
                objEntity.firstname = Convert.ToString(dr["FirstName"]);
            }
            if (dr.Table.Columns.Contains("LastName") && dr["LastName"] != DBNull.Value)
            {
                objEntity.lastname = Convert.ToString(dr["LastName"]);
            }
            if (dr.Table.Columns.Contains("middlename") && dr["middlename"] != DBNull.Value)
            {
                objEntity.middlename = Convert.ToString(dr["middlename"]);
            }
            if (dr.Table.Columns.Contains("LicenseNumber") && dr["LicenseNumber"] != DBNull.Value)
            {
                objEntity.license = Convert.ToString(dr["LicenseNumber"]);
            }
            if (dr.Table.Columns.Contains("Status") && dr["Status"] != DBNull.Value)
            {
                objEntity.status = Convert.ToString(dr["Status"]);
            }
            if (dr.Table.Columns.Contains("LicenseExpirationDate") && dr["LicenseExpirationDate"] != DBNull.Value)
            {
                objEntity.ExpirationDate = Convert.ToString(dr["LicenseExpirationDate"]);
            }
            if (dr.Table.Columns.Contains("LicenseEffectiveDate") && dr["LicenseEffectiveDate"] != DBNull.Value)
            {
                objEntity.CurrentLicenseDate = Convert.ToString(dr["LicenseEffectiveDate"]);
            }
            if (dr.Table.Columns.Contains("OriginalLicenseDate") && dr["OriginalLicenseDate"] != DBNull.Value)
            {
                objEntity.OriginalLicenseDate = Convert.ToString(dr["OriginalLicenseDate"]);
            }
           
            return objEntity;

        }
    }
}
