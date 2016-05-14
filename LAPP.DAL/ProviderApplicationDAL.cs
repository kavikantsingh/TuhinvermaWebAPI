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
    public class ProviderApplicationDAL : BaseDAL
    {
        public int Save_ProviderApplication(ProviderApplication objProviderApplication)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ProviderId", objProviderApplication.ProviderId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objProviderApplication.ApplicationId));
            lstParameter.Add(new MySqlParameter("CreatedBy", objProviderApplication.CreatedBy));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objProviderApplication.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ProviderApplicationGuid", objProviderApplication.ProviderApplicationGuid));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "PROVIDERAPPLICATION_SAVE", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<ProviderApplication> Get_All_ProviderApplication()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "PROVIDERAPPLICATION_GET_ALL");
            List<ProviderApplication> lstEntity = new List<ProviderApplication>();
            ProviderApplication objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private ProviderApplication FetchEntity(DataRow dr)
        {
            ProviderApplication objEntity = new ProviderApplication();
            if (dr.Table.Columns.Contains("ProviderId") && dr["ProviderId"] != DBNull.Value)
            {
                objEntity.ProviderId = Convert.ToInt32(dr["ProviderId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationId") && dr["ApplicationId"] != DBNull.Value)
            {
                objEntity.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
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
            if (dr.Table.Columns.Contains("ProviderApplicationGuid") && dr["ProviderApplicationGuid"] != DBNull.Value)
            {
                objEntity.ProviderApplicationGuid = (Guid)dr["ProviderApplicationGuid"];
            }
            return objEntity;

        }
    }
}
