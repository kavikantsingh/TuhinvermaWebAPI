using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class ContentItemLkDAL : BaseDAL
    {
        public int Save_ContentItemLk(ContentItemLk objContentItemLk)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ContentItemLkId", objContentItemLk.ContentItemLkId));
            lstParameter.Add(new MySqlParameter("ContentLkToPageTabSectionId", objContentItemLk.ContentLkToPageTabSectionId));
            lstParameter.Add(new MySqlParameter("ContentItemLkCode", objContentItemLk.ContentItemLkCode));
            lstParameter.Add(new MySqlParameter("ContentItemHash", objContentItemLk.ContentItemHash));
            lstParameter.Add(new MySqlParameter("ContentItemLkDesc", objContentItemLk.ContentItemLkDesc));
            lstParameter.Add(new MySqlParameter("SortOrder", objContentItemLk.SortOrder));
            lstParameter.Add(new MySqlParameter("EffectiveDate", objContentItemLk.EffectiveDate));
            lstParameter.Add(new MySqlParameter("EndDate", objContentItemLk.EndDate));
            lstParameter.Add(new MySqlParameter("IsEnabled", objContentItemLk.IsEnabled));
            lstParameter.Add(new MySqlParameter("IsEditable", objContentItemLk.IsEditable));

            lstParameter.Add(new MySqlParameter("IsActive", objContentItemLk.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objContentItemLk.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objContentItemLk.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objContentItemLk.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objContentItemLk.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objContentItemLk.ModifiedOn));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "contentitemlk_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<ContentItemLk> Get_All_ContentItemLk()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ContentItemLk_Get_All");
            List<ContentItemLk> lstEntity = new List<ContentItemLk>();
            ContentItemLk objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public ContentItemLk Get_ContentItemLk_By_ContentItemLkId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ContentItemLkId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ContentItemLk_GET_BY_ContentItemLkId", lstParameter.ToArray());
            ContentItemLk objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public ContentItemLk Get_ContentItemLk_By_ContentItemLkCode_And_ContentItemLkHash(string Code, int Hash)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ContentItemLkCode", Code));
            lstParameter.Add(new MySqlParameter("ContentItemLkHash", Hash));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ContentItemLk_GET_BY_Code_And_Hash", lstParameter.ToArray());
            ContentItemLk objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<ContentItemLk> Get_ContentItemLk_By_ContentItemLkCode(string Code)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ContentItemLkCode", Code));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ContentItemLk_GET_BY_Code", lstParameter.ToArray());
            List<ContentItemLk> lstEntity = new List<ContentItemLk>();
            ContentItemLk objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<ContentItemLk> Get_ContentItemLk_By_ContentLkToPageTabSectionId(int PageTabSectionId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ContentLkToPageTabSectionId", PageTabSectionId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ContentItemLk_GET_BY_PageTabSectionId", lstParameter.ToArray());
            List<ContentItemLk> lstEntity = new List<ContentItemLk>();
            ContentItemLk objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private ContentItemLk FetchEntity(DataRow dr)
        {
            ContentItemLk objEntity = new ContentItemLk();
            if (dr.Table.Columns.Contains("ContentItemLkId") && dr["ContentItemLkId"] != DBNull.Value)
            {
                objEntity.ContentItemLkId = Convert.ToInt32(dr["ContentItemLkId"]);
            }
            if (dr.Table.Columns.Contains("ContentLkToPageTabSectionId") && dr["ContentLkToPageTabSectionId"] != DBNull.Value)
            {
                objEntity.ContentLkToPageTabSectionId = Convert.ToInt32(dr["ContentLkToPageTabSectionId"]);
            }
            if (dr.Table.Columns.Contains("ContentItemLkCode") && dr["ContentItemLkCode"] != DBNull.Value)
            {
                objEntity.ContentItemLkCode = Convert.ToString(dr["ContentItemLkCode"]);
            }
            if (dr.Table.Columns.Contains("ContentItem#") && dr["ContentItem#"] != DBNull.Value)
            {
                objEntity.ContentItemHash = Convert.ToInt32(dr["ContentItem#"]);
            }
            if (dr.Table.Columns.Contains("ContentItemLkDesc") && dr["ContentItemLkDesc"] != DBNull.Value)
            {
                objEntity.ContentItemLkDesc = Convert.ToString(dr["ContentItemLkDesc"]);
            }
            if (dr.Table.Columns.Contains("SortOrder") && dr["SortOrder"] != DBNull.Value)
            {
                objEntity.SortOrder = Convert.ToInt32(dr["SortOrder"]);
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
