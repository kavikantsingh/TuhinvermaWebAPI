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
    public class ProvidersitevisittypeDAL : BaseDAL
    {

        public int Save_Providersitevisittype(Providersitevisittype objProvidersitevisittype)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ProviderSiteVisitTypeId", objProvidersitevisittype.ProviderSiteVisitTypeId));
            lstParameter.Add(new MySqlParameter("ProviderSiteVisitTypeCode", objProvidersitevisittype.ProviderSiteVisitTypeCode));
            lstParameter.Add(new MySqlParameter("ProviderSiteVisitTypeName", objProvidersitevisittype.ProviderSiteVisitTypeName));
            lstParameter.Add(new MySqlParameter("IsActive", objProvidersitevisittype.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objProvidersitevisittype.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objProvidersitevisittype.CreatedBy));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objProvidersitevisittype.ModifiedBy));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "licensetype_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<Providersitevisittype> Get_All_Providersitevisittype()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "licensetype_Get_All");
            List<Providersitevisittype> lstEntity = new List<Providersitevisittype>();
            Providersitevisittype objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private Providersitevisittype FetchEntity(DataRow dr)
        {
            Providersitevisittype objEntity = new Providersitevisittype();
            if (dr.Table.Columns.Contains("ProviderSiteVisitTypeId") && dr["ProviderSiteVisitTypeId"] != DBNull.Value)
            {
                objEntity.ProviderSiteVisitTypeId = Convert.ToInt32(dr["ProviderSiteVisitTypeId"]);
            }
            if (dr.Table.Columns.Contains("ProviderSiteVisitTypeCode") && dr["ProviderSiteVisitTypeCode"] != DBNull.Value)
            {
                objEntity.ProviderSiteVisitTypeCode = Convert.ToString(dr["ProviderSiteVisitTypeCode"]);
            }
            if (dr.Table.Columns.Contains("ProviderSiteVisitTypeName") && dr["ProviderSiteVisitTypeName"] != DBNull.Value)
            {
                objEntity.ProviderSiteVisitTypeName = Convert.ToString(dr["ProviderSiteVisitTypeName"]);
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

