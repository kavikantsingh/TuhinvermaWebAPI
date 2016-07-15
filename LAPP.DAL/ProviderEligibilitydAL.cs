using LAPP.ENTITY;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.DAL
{

    public class ProviderEligibilityDAL : BaseDAL
    {
        public int Save_ProviderEligibility(ProviderEligibility objProviderEligibility)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("ProviderEligibilityId", objProviderEligibility.ProviderEligibilityId));
            lstParameter.Add(new MySqlParameter("ProviderId", objProviderEligibility.ProviderId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objProviderEligibility.ApplicationId));
            lstParameter.Add(new MySqlParameter("ContentItemLkId", objProviderEligibility.ContentItemLkId));
            lstParameter.Add(new MySqlParameter("ContentItemNo", objProviderEligibility.ContentItemNo));
            lstParameter.Add(new MySqlParameter("ContentLkToPageTabSectionId", objProviderEligibility.ContentLkToPageTabSectionId));
            lstParameter.Add(new MySqlParameter("IsChecked", objProviderEligibility.IsChecked));

            lstParameter.Add(new MySqlParameter("CreatedBy", objProviderEligibility.CreatedBy));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objProviderEligibility.ModifiedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", DateTime.Now));
            lstParameter.Add(new MySqlParameter("ModifiedOn", DateTime.Now));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            lstParameter.Add(new MySqlParameter("IsDeleted", objProviderEligibility.IsDeleted));
            lstParameter.Add(new MySqlParameter("IsActive", objProviderEligibility.IsActive));
            lstParameter.Add(new MySqlParameter("ProviderEligibilityIdGuid", objProviderEligibility.ProviderEligibilityIdGuid));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "ProviderEligibility_save_SchoolInfo", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<ProviderEligibility> Get_All_ProviderEligibility(int ProviderId, int ProviderEligibilityId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ProviderId", ProviderId));
            lstParameter.Add(new MySqlParameter("ContentItemLkId", ProviderEligibilityId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ProviderEligibility_Get_All_SchoolInfo", lstParameter.ToArray());
            List<ProviderEligibility> lstEntity = new List<ProviderEligibility>();
            ProviderEligibility objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private ProviderEligibility FetchEntity(DataRow dr)
        {
            ProviderEligibility objEntity = new ProviderEligibility();
            if (dr.Table.Columns.Contains("ProviderId") && dr["ProviderId"] != DBNull.Value)
            {
                objEntity.ProviderId = Convert.ToInt32(dr["ProviderId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationId") && dr["ApplicationId"] != DBNull.Value)
            {
                objEntity.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
            }
            if (dr.Table.Columns.Contains("ContentItemLkId") && dr["ContentItemLkId"] != DBNull.Value)
            {
                objEntity.ContentItemLkId = Convert.ToInt32(dr["ContentItemLkId"]);
            }
            if (dr.Table.Columns.Contains("ContentLkToPageTabSectionId") && dr["ContentLkToPageTabSectionId"] != DBNull.Value)
            {
                objEntity.ContentLkToPageTabSectionId = Convert.ToInt32(dr["ContentLkToPageTabSectionId"]);
            }
            if (dr.Table.Columns.Contains("ContentItemNo") && dr["ContentItemNo"] != DBNull.Value)
            {
                objEntity.ContentItemNo = Convert.ToInt32(dr["ContentItemNo"]);
            }
            
            if (dr.Table.Columns.Contains("ProviderEligibilityId") && dr["ProviderEligibilityId"] != DBNull.Value)
            {
                objEntity.ProviderEligibilityId = Convert.ToInt32(dr["ProviderEligibilityId"]);
            }

            if (dr.Table.Columns.Contains("IsChecked") && dr["IsChecked"] != DBNull.Value)
            {
                objEntity.IsChecked= Convert.ToBoolean(dr["IsChecked"]);
            }


            return objEntity;

        }

    }
}
