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
        public int Save_DocumentMaster(DocumentMaster objDocumentMaster)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("DocumentMasterId", objDocumentMaster.DocumentMasterId));
            lstParameter.Add(new MySqlParameter("DocumentId", objDocumentMaster.DocumentId));
            lstParameter.Add(new MySqlParameter("DocumentCd", objDocumentMaster.DocumentCd));
            lstParameter.Add(new MySqlParameter("DocumentName", objDocumentMaster.DocumentName));
            lstParameter.Add(new MySqlParameter("DocumentTypeId", objDocumentMaster.DocumentTypeId));
            lstParameter.Add(new MySqlParameter("DocumentTypeIdName", objDocumentMaster.DocumentTypeIdName));
            lstParameter.Add(new MySqlParameter("DocumentTypeDesc", objDocumentMaster.DocumentTypeDesc));
            lstParameter.Add(new MySqlParameter("MaxSize", objDocumentMaster.Max_size));
            lstParameter.Add(new MySqlParameter("MasterTransactionId", objDocumentMaster.MasterTransactionId));
            lstParameter.Add(new MySqlParameter("PageModuleId", objDocumentMaster.PageModuleId));
            lstParameter.Add(new MySqlParameter("PageModuleTabSubModuleId", objDocumentMaster.PageModuleTabSubModuleId));
            lstParameter.Add(new MySqlParameter("PageTabSectionId", objDocumentMaster.PageTabSectionId));
            //lstParameter.Add(new MySqlParameter("EffectiveDate", objDocumentMaster.EffectiveDate));
            lstParameter.Add(new MySqlParameter("EndDate", objDocumentMaster.EndDate));

            lstParameter.Add(new MySqlParameter("CreatedBy", objDocumentMaster.CreatedBy));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objDocumentMaster.ModifiedBy));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "DocumentMaster_Save", lstParameter.ToArray());
            //return returnValue;
            return Convert.ToInt32(returnParam.Value);
        }
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

        public List<DocumentViewModel> GetDocumentResultSet()
        {
            var queryData = @"SELECT MT.MasterTransactionName,PM.PageModuleName,PMSM.PageModuleTabSubModuleName, 
                              PTS.PageTabSectionName,DM.DocumentName, DM.DocumentTypeIdName, DM.Max_size AS MaxSize, 
                              DM.EndDate, DM.DocumentMasterId, DM.IsEditable FROM mastertransaction MT JOIN pagemodule PM ON MT.MasterTransactionId = PM.MasterTransactionId 
                              JOIN pagemoduletabsubmodule PMSM ON PM.PageModuleId = PMSM.PageModuleId JOIN pagetabsection PTS ON 
                              PTS.PageModuleTabSubModuleId = PMSM.PageModuleTabSubModuleId JOIN DocumentMaster DM ON DM.MasterTransactionId = MT.MasterTransactionId";
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            ds = objDB.ExecuteDataSet(CommandType.Text, queryData);
            List<DocumentViewModel> lstEntity = new List<DocumentViewModel>();
            DocumentViewModel objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FatchResultSetEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }
        public List<DocumentMasterGET> Get_All_DocumentMaster()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "documentmaster_Get_All", lstParameter.ToArray());
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

        private DocumentViewModel FatchResultSetEntity(DataRow dr)
        {
            DocumentViewModel objEntity = new DocumentViewModel();
            if (dr.Table.Columns.Contains("MasterTransactionName") && dr["MasterTransactionName"] != DBNull.Value)
            {
                objEntity.MasterTransactionName = Convert.ToString(dr["MasterTransactionName"]);
            }
            if (dr.Table.Columns.Contains("PageModuleName") && dr["PageModuleName"] != DBNull.Value)
            {
                objEntity.PageModuleName = Convert.ToString(dr["PageModuleName"]);
            }
            if (dr.Table.Columns.Contains("PageModuleTabSubModuleName") && dr["PageModuleTabSubModuleName"] != DBNull.Value)
            {
                objEntity.PageModuleTabSubModuleName = Convert.ToString(dr["PageModuleTabSubModuleName"]);
            }
            if (dr.Table.Columns.Contains("PageTabSectionName") && dr["PageTabSectionName"] != DBNull.Value)
            {
                objEntity.PageTabSectionName = Convert.ToString(dr["PageTabSectionName"]);
            }
            if (dr.Table.Columns.Contains("DocumentName") && dr["DocumentName"] != DBNull.Value)
            {
                objEntity.DocumentName = Convert.ToString(dr["DocumentName"]);
            }
            if (dr.Table.Columns.Contains("DocumentTypeIdName") && dr["DocumentTypeIdName"] != DBNull.Value)
            {
                objEntity.DocumentTypeIdName = Convert.ToString(dr["DocumentTypeIdName"]);
            }
            if (dr.Table.Columns.Contains("MaxSize") && dr["MaxSize"] != DBNull.Value)
            {
                objEntity.MaxSize = Convert.ToString(dr["MaxSize"]);
            }
            if (dr.Table.Columns.Contains("EndDate") && dr["EndDate"] != DBNull.Value)
            {
                objEntity.EndDate = Convert.ToDateTime(dr["EndDate"]);
            }
            if (dr.Table.Columns.Contains("DocumentMasterId") && dr["DocumentMasterId"] != DBNull.Value)
            {
                objEntity.DocumentMasterId = Convert.ToInt32(dr["DocumentMasterId"]);
            }
            if (dr.Table.Columns.Contains("IsEditable") && dr["IsEditable"] != DBNull.Value)
            {
                objEntity.IsEditable = Convert.ToInt32(dr["IsEditable"]);
            }
            return objEntity;
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
            if (dr.Table.Columns.Contains("IsActive") && dr["IsActive"] != DBNull.Value)
            {
                objEntity.IsActive = Convert.ToBoolean(dr["IsActive"]);
            }
            if (dr.Table.Columns.Contains("PageTabSectionId") && dr["PageTabSectionId"] != DBNull.Value)
            {
                objEntity.PageTabSectionId = Convert.ToInt32(dr["PageTabSectionId"]);
            }
            return objEntity;
        }
    }
}
