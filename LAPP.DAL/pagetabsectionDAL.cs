using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class PageTabSectionDAL : BaseDAL
    {
        public int Save_PageTabSection(PageTabSection objPageTabSection)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("PageTabSectionId", objPageTabSection.PageTabSectionId));
            lstParameter.Add(new MySqlParameter("PageTabSectionCode", objPageTabSection.PageTabSectionCode));
            lstParameter.Add(new MySqlParameter("PageTabSectionName", objPageTabSection.PageTabSectionName));
            lstParameter.Add(new MySqlParameter("PageTabSectionDesc", objPageTabSection.PageTabSectionDesc));
            lstParameter.Add(new MySqlParameter("MasterTransactionId", objPageTabSection.MasterTransactionId));
            lstParameter.Add(new MySqlParameter("PageModuleId", objPageTabSection.PageModuleId));
            lstParameter.Add(new MySqlParameter("PageModuleTabSubModuleId", objPageTabSection.PageModuleTabSubModuleId));
            lstParameter.Add(new MySqlParameter("IsEnabled", objPageTabSection.IsEnabled));
            lstParameter.Add(new MySqlParameter("IsReadOnly", objPageTabSection.IsReadOnly));
            lstParameter.Add(new MySqlParameter("IsActive", objPageTabSection.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objPageTabSection.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objPageTabSection.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objPageTabSection.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objPageTabSection.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objPageTabSection.ModifiedOn));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "pagetabsection_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<PageTabSection> Get_All_PageTabSection()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "PAGETABSECTION_GET_ALL");
            List<PageTabSection> lstEntity = new List<PageTabSection>();
            PageTabSection objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<PageTabSection> Get_All_PageTabSection_By_PageModuleTabSubModuleId(int PageModuleTabSubModuleId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("PageModuleTabSubModuleId", PageModuleTabSubModuleId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "PageTabSection_Get_All", lstParameter.ToArray());
            List<PageTabSection> lstEntity = new List<PageTabSection>();
            PageTabSection objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private PageTabSection FetchEntity(DataRow dr)
        {
            PageTabSection objEntity = new PageTabSection();
            if (dr.Table.Columns.Contains("PageTabSectionId") && dr["PageTabSectionId"] != DBNull.Value)
            {
                objEntity.PageTabSectionId = Convert.ToInt32(dr["PageTabSectionId"]);
            }
            if (dr.Table.Columns.Contains("PageTabSectionCode") && dr["PageTabSectionCode"] != DBNull.Value)
            {
                objEntity.PageTabSectionCode = Convert.ToString(dr["PageTabSectionCode"]);
            }
            if (dr.Table.Columns.Contains("PageTabSectionName") && dr["PageTabSectionName"] != DBNull.Value)
            {
                objEntity.PageTabSectionName = Convert.ToString(dr["PageTabSectionName"]);
            }
            if (dr.Table.Columns.Contains("PageTabSectionDesc") && dr["PageTabSectionDesc"] != DBNull.Value)
            {
                objEntity.PageTabSectionDesc = Convert.ToString(dr["PageTabSectionDesc"]);
            }
            if (dr.Table.Columns.Contains("MasterTransactionId") && dr["MasterTransactionId"] != DBNull.Value)
            {
                objEntity.MasterTransactionId = Convert.ToInt32(dr["MasterTransactionId"]);
            }
            if (dr.Table.Columns.Contains("PageModuleId") && dr["PageModuleId"] != DBNull.Value)
            {
                objEntity.PageModuleId = Convert.ToInt32(dr["PageModuleId"]);
            }
            if (dr.Table.Columns.Contains("PageModuleTabSubModuleId") && dr["PageModuleTabSubModuleId"] != DBNull.Value)
            {
                objEntity.PageModuleTabSubModuleId = Convert.ToInt32(dr["PageModuleTabSubModuleId"]);
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
