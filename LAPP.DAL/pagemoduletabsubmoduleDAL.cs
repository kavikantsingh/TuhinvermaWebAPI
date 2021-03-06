using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class PageModuletabsubmoduleDAL : BaseDAL
    {
        public int Save_PageModuletabsubmodule(PageModuleTabSubModule objPageModuletabsubmodule)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("PageModuleTabSubModuleId", objPageModuletabsubmodule.PageModuleTabSubModuleId));
            lstParameter.Add(new MySqlParameter("PageModuleTabSubModuleCode", objPageModuletabsubmodule.PageModuleTabSubModuleCode));
            lstParameter.Add(new MySqlParameter("PageModuleTabSubModuleName", objPageModuletabsubmodule.PageModuleTabSubModuleName));
            lstParameter.Add(new MySqlParameter("PageModuleTabSubModuleDesc", objPageModuletabsubmodule.PageModuleTabSubModuleDesc));
            lstParameter.Add(new MySqlParameter("MasterTransactionId", objPageModuletabsubmodule.MasterTransactionId));
            lstParameter.Add(new MySqlParameter("PageModuleId", objPageModuletabsubmodule.PageModuleId));
            lstParameter.Add(new MySqlParameter("IsEnabled", objPageModuletabsubmodule.IsEnabled));
            lstParameter.Add(new MySqlParameter("IsReadOnly", objPageModuletabsubmodule.IsReadOnly));
            lstParameter.Add(new MySqlParameter("IsActive", objPageModuletabsubmodule.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objPageModuletabsubmodule.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objPageModuletabsubmodule.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objPageModuletabsubmodule.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objPageModuletabsubmodule.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objPageModuletabsubmodule.ModifiedOn));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "pagemoduletabsubmodule_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<PageModuleTabSubModule> Get_All_PageModuletabsubmodule()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "PAGEMODULETABSUBMODULE_GET_ALL");
            List<PageModuleTabSubModule> lstEntity = new List<PageModuleTabSubModule>();
            PageModuleTabSubModule objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<PageModuleTabSubModule> Get_All_PageModuletabsubmodule_By_PageModuleId(int PageModuleId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("PageModuleId", PageModuleId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "PageModuleTabSubModule_Get_All", lstParameter.ToArray());
            List<PageModuleTabSubModule> lstEntity = new List<PageModuleTabSubModule>();
            PageModuleTabSubModule objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private PageModuleTabSubModule FetchEntity(DataRow dr)
        {
            PageModuleTabSubModule objEntity = new PageModuleTabSubModule();
            if (dr.Table.Columns.Contains("PageModuleTabSubModuleId") && dr["PageModuleTabSubModuleId"] != DBNull.Value)
            {
                objEntity.PageModuleTabSubModuleId = Convert.ToInt32(dr["PageModuleTabSubModuleId"]);
            }
            if (dr.Table.Columns.Contains("PageModuleTabSubModuleCode") && dr["PageModuleTabSubModuleCode"] != DBNull.Value)
            {
                objEntity.PageModuleTabSubModuleCode = Convert.ToString(dr["PageModuleTabSubModuleCode"]);
            }
            if (dr.Table.Columns.Contains("PageModuleTabSubModuleName") && dr["PageModuleTabSubModuleName"] != DBNull.Value)
            {
                objEntity.PageModuleTabSubModuleName = Convert.ToString(dr["PageModuleTabSubModuleName"]);
            }
            if (dr.Table.Columns.Contains("PageModuleTabSubModuleDesc") && dr["PageModuleTabSubModuleDesc"] != DBNull.Value)
            {
                objEntity.PageModuleTabSubModuleDesc = Convert.ToString(dr["PageModuleTabSubModuleDesc"]);
            }
            if (dr.Table.Columns.Contains("MasterTransactionId") && dr["MasterTransactionId"] != DBNull.Value)
            {
                objEntity.MasterTransactionId = Convert.ToInt32(dr["MasterTransactionId"]);
            }
            if (dr.Table.Columns.Contains("PageModuleId") && dr["PageModuleId"] != DBNull.Value)
            {
                objEntity.PageModuleId = Convert.ToInt32(dr["PageModuleId"]);
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
