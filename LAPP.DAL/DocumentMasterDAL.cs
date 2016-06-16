using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;

namespace LAPP.DAL
{
    public class DocumentMasterDAL
    {
        public List<DocumentMasterGET> Get_DocumentMaster_By_DocId_And_DocCode(int DocId, string DocCode)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("DocId", DocId));
            lstParameter.Add(new MySqlParameter("DocCode", DocCode));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "DocumentMaster_GET_BY_DocId_And_DocCode", lstParameter.ToArray());
            //List<DocumentMaster> lstEntity = new List<DocumentMaster>();
            //DocumentMaster objEntity = null;
            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    objEntity = FetchEntity(dr);
            //    if (objEntity != null)
            //        lstEntity.Add(objEntity);
            //}
            List<DocumentMasterGET> lstEntity = new List<DocumentMasterGET>();
            DocumentMasterGET objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntityGET(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private DocumentMaster FetchEntity(DataRow dr)
        {
            DocumentMaster objEntity = new DocumentMaster();
            if (dr.Table.Columns.Contains("DocumentMasterId") && dr["DocumentMasterId"] != DBNull.Value)
            {
                objEntity.DocumentMasterId = Convert.ToInt32(dr["DocumentMasterId"]);
            }
            if (dr.Table.Columns.Contains("DocumentId") && dr["DocumentId"] != DBNull.Value)
            {
                objEntity.DocumentId = Convert.ToInt32(dr["DocumentId"]);
            }
            if (dr.Table.Columns.Contains("DocumentCd") && dr["DocumentCd"] != DBNull.Value)
            {
                objEntity.DocumentCd = Convert.ToString(dr["DocumentCd"]);
            }
            if (dr.Table.Columns.Contains("DocumentName") && dr["DocumentName"] != DBNull.Value)
            {
                objEntity.DocumentName = Convert.ToString(dr["DocumentName"]);
            }
            if (dr.Table.Columns.Contains("DocumentTypeId") && dr["DocumentTypeId"] != DBNull.Value)
            {
                objEntity.DocumentTypeId = Convert.ToInt32(dr["DocumentTypeId"]);
            }
            if (dr.Table.Columns.Contains("DocumentTypeIdName") && dr["DocumentTypeIdName"] != DBNull.Value)
            {
                objEntity.DocumentTypeIdName = Convert.ToString(dr["DocumentTypeIdName"]);
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

        private DocumentMasterGET FetchEntityGET(DataRow dr)
        {
            DocumentMasterGET objEntity = new DocumentMasterGET();
            if (dr.Table.Columns.Contains("DocumentTypeId") && dr["DocumentTypeId"] != DBNull.Value)
            {
                objEntity.DocumentTypeId = Convert.ToInt32(dr["DocumentTypeId"]);
            }
            if (dr.Table.Columns.Contains("DocumentTypeIdName") && dr["DocumentTypeIdName"] != DBNull.Value)
            {
                objEntity.DocumentTypeIdName = Convert.ToString(dr["DocumentTypeIdName"]);
            }
            if (dr.Table.Columns.Contains("DocumentTypeDesc") && dr["DocumentTypeDesc"] != DBNull.Value)
            {
                objEntity.DocumentTypeDesc = Convert.ToString(dr["DocumentTypeDesc"]);
            }
            if (dr.Table.Columns.Contains("Max_size") && dr["Max_size"] != DBNull.Value)
            {
                objEntity.Max_size = Convert.ToInt32(dr["Max_size"]);
            }

            return objEntity;
        }
    }
}
