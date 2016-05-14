using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class ContentLkToPageTabSectionDAL : BaseDAL
    {
        public int Save_ContentLkToPageTabSection(ContentLkToPageTabSection objContentLkToPageTabSection)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ContentLkToPageTabSectionId", objContentLkToPageTabSection.ContentLkToPageTabSectionId));
            lstParameter.Add(new MySqlParameter("ContentTypeName", objContentLkToPageTabSection.ContentTypeName));
            lstParameter.Add(new MySqlParameter("MasterTransactionId", objContentLkToPageTabSection.MasterTransactionId));
            lstParameter.Add(new MySqlParameter("PageModuleId", objContentLkToPageTabSection.PageModuleId));
            lstParameter.Add(new MySqlParameter("PageModuleTabSubModuleId", objContentLkToPageTabSection.PageModuleTabSubModuleId));
            lstParameter.Add(new MySqlParameter("PageTabSectionId", objContentLkToPageTabSection.PageTabSectionId));
            lstParameter.Add(new MySqlParameter("EffectiveDate", objContentLkToPageTabSection.EffectiveDate));
            lstParameter.Add(new MySqlParameter("EndDate", objContentLkToPageTabSection.EndDate));
            lstParameter.Add(new MySqlParameter("IsEnabled", objContentLkToPageTabSection.IsEnabled));
            lstParameter.Add(new MySqlParameter("IsEditable", objContentLkToPageTabSection.IsEditable));

            lstParameter.Add(new MySqlParameter("IsActive", objContentLkToPageTabSection.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objContentLkToPageTabSection.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objContentLkToPageTabSection.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objContentLkToPageTabSection.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objContentLkToPageTabSection.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objContentLkToPageTabSection.ModifiedOn));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "contentlktopagetabsection_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<ContentLkToPageTabSection> Get_All_ContentLkToPageTabSection()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ContentLkToPageTabSection_Get_All");
            List<ContentLkToPageTabSection> lstEntity = new List<ContentLkToPageTabSection>();
            ContentLkToPageTabSection objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public ContentLkToPageTabSection Get_ContentLkToPageTabSection_By_ContentLkToPageTabSectionId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ContentLkToPageTabSectionId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ContentLkToPageTabSection_GET_BY_ContentLkToPageTabSectionId", lstParameter.ToArray());
            ContentLkToPageTabSection objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private ContentLkToPageTabSection FetchEntity(DataRow dr)
        {
            ContentLkToPageTabSection objEntity = new ContentLkToPageTabSection();

            if (dr.Table.Columns.Contains("ContentLkToPageTabSectionId") && dr["ContentLkToPageTabSectionId"] != DBNull.Value)
            {
                objEntity.ContentLkToPageTabSectionId = Convert.ToInt32(dr["ContentLkToPageTabSectionId"]);
            }
            if (dr.Table.Columns.Contains("ContentTypeName") && dr["ContentTypeName"] != DBNull.Value)
            {
                objEntity.ContentTypeName = Convert.ToString(dr["ContentTypeName"]);
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
            if (dr.Table.Columns.Contains("PageTabSectionId") && dr["PageTabSectionId"] != DBNull.Value)
            {
                objEntity.PageTabSectionId = Convert.ToInt32(dr["PageTabSectionId"]);
            }
            if (dr.Table.Columns.Contains("EffectiveDate") && dr["EffectiveDate"] != DBNull.Value)
            {
                objEntity.EffectiveDate = Convert.ToDateTime(dr["EffectiveDate"]);
            }
            if (dr.Table.Columns.Contains("EndDate") && dr["EndDate"] != DBNull.Value)
            {
                objEntity.EndDate = Convert.ToDateTime(dr["EndDate"]);
            }
            if (dr.Table.Columns.Contains("IsEnabled") && dr["IsEnabled"] != DBNull.Value)
            {
                objEntity.IsEnabled = Convert.ToBoolean(dr["IsEnabled"]);
            }
            if (dr.Table.Columns.Contains("IsEditable") && dr["IsEditable"] != DBNull.Value)
            {
                objEntity.IsEditable = Convert.ToBoolean(dr["IsEditable"]);
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
