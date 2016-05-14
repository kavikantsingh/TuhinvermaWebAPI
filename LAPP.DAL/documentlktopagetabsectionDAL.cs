using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class DocumentLkToPageTabSectionDAL : BaseDAL
    {
        public int Save_DocumentLkToPageTabSection(DocumentLkToPageTabSection objDocumentLkToPageTabSection)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("DocumentLkToPageTabSectionId", objDocumentLkToPageTabSection.DocumentLkToPageTabSectionId));
            lstParameter.Add(new MySqlParameter("DocumentLkToPageTabSectionCode", objDocumentLkToPageTabSection.DocumentLkToPageTabSectionCode));
            lstParameter.Add(new MySqlParameter("DocumentTypeName", objDocumentLkToPageTabSection.DocumentTypeName));
            lstParameter.Add(new MySqlParameter("DocumentTypeDesc", objDocumentLkToPageTabSection.DocumentTypeDesc));
            lstParameter.Add(new MySqlParameter("Max_size", objDocumentLkToPageTabSection.Max_size));
            lstParameter.Add(new MySqlParameter("MasterTransactionId", objDocumentLkToPageTabSection.MasterTransactionId));
            lstParameter.Add(new MySqlParameter("PageModuleId", objDocumentLkToPageTabSection.PageModuleId));
            lstParameter.Add(new MySqlParameter("PageModuleTabSubModuleId", objDocumentLkToPageTabSection.PageModuleTabSubModuleId));
            lstParameter.Add(new MySqlParameter("PageTabSectionId", objDocumentLkToPageTabSection.PageTabSectionId));
            lstParameter.Add(new MySqlParameter("EffectiveDate", objDocumentLkToPageTabSection.EffectiveDate));
            lstParameter.Add(new MySqlParameter("EndDate", objDocumentLkToPageTabSection.EndDate));
            lstParameter.Add(new MySqlParameter("IsEnabled", objDocumentLkToPageTabSection.IsEnabled));
            lstParameter.Add(new MySqlParameter("IsEditable", objDocumentLkToPageTabSection.IsEditable));
            lstParameter.Add(new MySqlParameter("IsActive", objDocumentLkToPageTabSection.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objDocumentLkToPageTabSection.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objDocumentLkToPageTabSection.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objDocumentLkToPageTabSection.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objDocumentLkToPageTabSection.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objDocumentLkToPageTabSection.ModifiedOn));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "DocumentLkToPageTabSection_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<DocumentLkToPageTabSection> Get_All_DocumentLkToPageTabSection()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "DocumentLkToPageTabSection_GET_ALL");
            List<DocumentLkToPageTabSection> lstEntity = new List<DocumentLkToPageTabSection>();
            DocumentLkToPageTabSection objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private DocumentLkToPageTabSection FetchEntity(DataRow dr)
        {
            DocumentLkToPageTabSection objEntity = new DocumentLkToPageTabSection();
            if (dr.Table.Columns.Contains("DocumentLkToPageTabSectionId") && dr["DocumentLkToPageTabSectionId"] != DBNull.Value)
            {
                objEntity.DocumentLkToPageTabSectionId = Convert.ToInt32(dr["DocumentLkToPageTabSectionId"]);
            }
            if (dr.Table.Columns.Contains("DocumentLkToPageTabSectionCode") && dr["DocumentLkToPageTabSectionCode"] != DBNull.Value)
            {
                objEntity.DocumentLkToPageTabSectionCode = Convert.ToString(dr["DocumentLkToPageTabSectionCode"]);
            }
            if (dr.Table.Columns.Contains("DocumentTypeName") && dr["DocumentTypeName"] != DBNull.Value)
            {
                objEntity.DocumentTypeName = Convert.ToString(dr["DocumentTypeName"]);
            }
            if (dr.Table.Columns.Contains("DocumentTypeDesc") && dr["DocumentTypeDesc"] != DBNull.Value)
            {
                objEntity.DocumentTypeDesc = Convert.ToString(dr["DocumentTypeDesc"]);
            }
            if (dr.Table.Columns.Contains("Max_size") && dr["Max_size"] != DBNull.Value)
            {
                objEntity.Max_size = Convert.ToInt32(dr["Max_size"]);
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
