using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class PageModuleDAL : BaseDAL
    {
        public int Save_PageModule(PageModule objPageModule)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("PageModuleId", objPageModule.PageModuleId));
            lstParameter.Add(new MySqlParameter("PageModuleCode", objPageModule.PageModuleCode));
            lstParameter.Add(new MySqlParameter("PageModuleName", objPageModule.PageModuleName));
            lstParameter.Add(new MySqlParameter("PageModuleDesc", objPageModule.PageModuleDesc));
            lstParameter.Add(new MySqlParameter("MasterTransactionId", objPageModule.MasterTransactionId));
            lstParameter.Add(new MySqlParameter("IsEnabled", objPageModule.IsEnabled));
            lstParameter.Add(new MySqlParameter("IsReadOnly", objPageModule.IsReadOnly));
            lstParameter.Add(new MySqlParameter("IsActive", objPageModule.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objPageModule.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objPageModule.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objPageModule.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objPageModule.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objPageModule.ModifiedOn));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "pagemodule_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<PageModule> Get_All_PageModule()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "PageModule_Get_All");
            List<PageModule> lstEntity = new List<PageModule>();
            PageModule objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private PageModule FetchEntity(DataRow dr)
        {
            PageModule objEntity = new PageModule();
            if (dr.Table.Columns.Contains("PageModuleId") && dr["PageModuleId"] != DBNull.Value)
            {
                objEntity.PageModuleId = Convert.ToInt32(dr["PageModuleId"]);
            }
            if (dr.Table.Columns.Contains("PageModuleCode") && dr["PageModuleCode"] != DBNull.Value)
            {
                objEntity.PageModuleCode = Convert.ToString(dr["PageModuleCode"]);
            }
            if (dr.Table.Columns.Contains("PageModuleName") && dr["PageModuleName"] != DBNull.Value)
            {
                objEntity.PageModuleName = Convert.ToString(dr["PageModuleName"]);
            }
            if (dr.Table.Columns.Contains("PageModuleDesc") && dr["PageModuleDesc"] != DBNull.Value)
            {
                objEntity.PageModuleDesc = Convert.ToString(dr["PageModuleDesc"]);
            }
            if (dr.Table.Columns.Contains("MasterTransactionId") && dr["MasterTransactionId"] != DBNull.Value)
            {
                objEntity.MasterTransactionId = Convert.ToInt32(dr["MasterTransactionId"]);
            }
            if (dr.Table.Columns.Contains("IsEnabled") && dr["IsEnabled"] != DBNull.Value)
            {
                objEntity.IsEnabled = Convert.ToBoolean(dr["IsEnabled"]);
            }
            if (dr.Table.Columns.Contains("IsReadOnly") && dr["IsReadOnly"] != DBNull.Value)
            {
                objEntity.IsReadOnly = Convert.ToBoolean(dr["IsReadOnly"]);
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
