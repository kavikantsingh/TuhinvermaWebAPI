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
    public class ProviderUserDAL : BaseDAL
    {
        public int Save_ProviderUser(ProviderUser objProviderUser)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ProviderUserId", objProviderUser.ProviderUserId));
            lstParameter.Add(new MySqlParameter("ProviderId", objProviderUser.ProviderId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objProviderUser.ApplicationId));
            lstParameter.Add(new MySqlParameter("UserId", objProviderUser.UserId));
            lstParameter.Add(new MySqlParameter("IndividualId", objProviderUser.IndividualId));
            lstParameter.Add(new MySqlParameter("IndividualNameId", objProviderUser.IndividualNameId));
            lstParameter.Add(new MySqlParameter("StartDate", objProviderUser.StartDate));
            lstParameter.Add(new MySqlParameter("EndDate", objProviderUser.EndDate));
            lstParameter.Add(new MySqlParameter("IsActive", objProviderUser.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objProviderUser.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objProviderUser.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objProviderUser.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objProviderUser.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objProviderUser.ModifiedOn));
            lstParameter.Add(new MySqlParameter("ProviderUserGuid", objProviderUser.ProviderUserGuid));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "ProviderUser_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public ProviderUser Get_ProviderUser_By_UserId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_UserId", ID));
            //lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "providerUser_Get_By_UserId", lstParameter.ToArray());
            ProviderUser objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private ProviderUser FetchEntity(DataRow dr)
        {
            ProviderUser objEntity = new ProviderUser();
            if (dr.Table.Columns.Contains("ProviderUserId") && dr["ProviderUserId"] != DBNull.Value)
            {
                objEntity.ProviderUserId = Convert.ToInt32(dr["ProviderUserId"]);
            }
            if (dr.Table.Columns.Contains("ProviderId") && dr["ProviderId"] != DBNull.Value)
            {
                objEntity.ProviderId = Convert.ToInt32(dr["ProviderId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationId") && dr["ApplicationId"] != DBNull.Value)
            {
                objEntity.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
            }
            if (dr.Table.Columns.Contains("UserId") && dr["UserId"] != DBNull.Value)
            {
                objEntity.UserId = Convert.ToInt32(dr["UserId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("IndividualNameId") && dr["IndividualNameId"] != DBNull.Value)
            {
                objEntity.IndividualNameId = Convert.ToInt32(dr["IndividualNameId"]);
            }
            if (dr.Table.Columns.Contains("StartDate") && dr["StartDate"] != DBNull.Value)
            {
                objEntity.StartDate = Convert.ToDateTime(dr["StartDate"]);
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
            if (dr.Table.Columns.Contains("ProviderUserGuid") && dr["ProviderUserGuid"] != DBNull.Value)
            {
                objEntity.ProviderUserGuid = Convert.ToString(dr["ProviderUserGuid"]);
            }
            return objEntity;

        }
    }
}
