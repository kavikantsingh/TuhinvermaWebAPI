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
    public class ProviderContactDAL : BaseDAL
    {
        public int Save_ProviderContact(ProviderContact objProviderContact)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ProviderContactId", objProviderContact.ProviderContactId));
            lstParameter.Add(new MySqlParameter("ProviderId", objProviderContact.ProviderId));
            lstParameter.Add(new MySqlParameter("ContactId", objProviderContact.ContactId));
            lstParameter.Add(new MySqlParameter("ContactTypeId", objProviderContact.ContactTypeId));
            lstParameter.Add(new MySqlParameter("BeginDate", objProviderContact.BeginDate));
            lstParameter.Add(new MySqlParameter("EndDate", objProviderContact.EndDate));
            lstParameter.Add(new MySqlParameter("IsPreferredContact", objProviderContact.IsPreferredContact));
            lstParameter.Add(new MySqlParameter("IsMobile", objProviderContact.IsMobile));
            lstParameter.Add(new MySqlParameter("IsActive", objProviderContact.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objProviderContact.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objProviderContact.CreatedBy));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objProviderContact.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ProviderContactGuid", objProviderContact.ProviderContactGuid));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "providercontact_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<ProviderContact> Get_All_ProviderContact()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "providercontact_Get_All");
            List<ProviderContact> lstEntity = new List<ProviderContact>();
            ProviderContact objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public ProviderContact Get_ProviderContact_By_ProviderContactId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_ProviderContactId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "providercontact_Get_By_ProviderContactId", lstParameter.ToArray());
            ProviderContact objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private ProviderContact FetchEntity(DataRow dr)
        {
            ProviderContact objEntity = new ProviderContact();
            if (dr.Table.Columns.Contains("ProviderContactId") && dr["ProviderContactId"] != DBNull.Value)
            {
                objEntity.ProviderContactId = Convert.ToInt32(dr["ProviderContactId"]);
            }
            if (dr.Table.Columns.Contains("ProviderId") && dr["ProviderId"] != DBNull.Value)
            {
                objEntity.ProviderId = Convert.ToInt32(dr["ProviderId"]);
            }
            if (dr.Table.Columns.Contains("ContactId") && dr["ContactId"] != DBNull.Value)
            {
                objEntity.ContactId = Convert.ToInt32(dr["ContactId"]);
            }
            if (dr.Table.Columns.Contains("ContactTypeId") && dr["ContactTypeId"] != DBNull.Value)
            {
                objEntity.ContactTypeId = Convert.ToInt32(dr["ContactTypeId"]);
            }
            if (dr.Table.Columns.Contains("BeginDate") && dr["BeginDate"] != DBNull.Value)
            {
                objEntity.BeginDate = Convert.ToDateTime(dr["BeginDate"]);
            }
            if (dr.Table.Columns.Contains("EndDate") && dr["EndDate"] != DBNull.Value)
            {
                objEntity.EndDate = Convert.ToDateTime(dr["EndDate"]);
            }
            if (dr.Table.Columns.Contains("IsPreferredContact") && dr["IsPreferredContact"] != DBNull.Value)
            {
                objEntity.IsPreferredContact = Convert.ToBoolean(dr["IsPreferredContact"]);
            }
            if (dr.Table.Columns.Contains("IsMobile") && dr["IsMobile"] != DBNull.Value)
            {
                objEntity.IsMobile = Convert.ToBoolean(dr["IsMobile"]);
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
            if (dr.Table.Columns.Contains("ProviderContactGuid") && dr["ProviderContactGuid"] != DBNull.Value)
            {
                objEntity.ProviderContactGuid = (Guid)dr["ProviderContactGuid"];
            }
            return objEntity;

        }
    }
}
