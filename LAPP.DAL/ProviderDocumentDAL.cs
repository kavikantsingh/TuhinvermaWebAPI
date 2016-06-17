using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;

namespace LAPP.DAL
{
    public class ProviderDocumentDAL
    {
        public List<ProviderDocumentGET> Get_ProviderDocument_By_ProviderId_DocumentId(int ProviderId, int DocumentId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ProviderId", ProviderId));
            lstParameter.Add(new MySqlParameter("DocumentId", DocumentId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ProviderDocument_GET_BY_ProviderId_And_DocumentId", lstParameter.ToArray());
            List<ProviderDocumentGET> lstEntity = new List<ProviderDocumentGET>();
            ProviderDocumentGET objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntityGET(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public int Save_ProviderDocument(ProviderDocument objProvDoc)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            //lstParameter.Add(new MySqlParameter("ProviderDocumentId", objProvDoc.ProviderDocumentId));
            lstParameter.Add(new MySqlParameter("ProviderId", objProvDoc.ProviderId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objProvDoc.ApplicationId));
            lstParameter.Add(new MySqlParameter("DocumentId", objProvDoc.DocumentId));
            lstParameter.Add(new MySqlParameter("DocumentCd", objProvDoc.DocumentCd));
            lstParameter.Add(new MySqlParameter("DocumentTypeId", objProvDoc.DocumentTypeId));
            lstParameter.Add(new MySqlParameter("DocumentLkToPageTabSectionId", objProvDoc.DocumentLkToPageTabSectionId));
            lstParameter.Add(new MySqlParameter("DocumentLkToPageTabSectionCode", objProvDoc.DocumentLkToPageTabSectionCode));
            lstParameter.Add(new MySqlParameter("DocumentName", objProvDoc.DocumentName));
            lstParameter.Add(new MySqlParameter("OtherDocumentTypeName", objProvDoc.OtherDocumentTypeName));
            lstParameter.Add(new MySqlParameter("DocumentPath", objProvDoc.DocumentPath));
            lstParameter.Add(new MySqlParameter("EffectiveDate", objProvDoc.EffectiveDate));
            lstParameter.Add(new MySqlParameter("EndDate", objProvDoc.EndDate));
            lstParameter.Add(new MySqlParameter("IsDocumentUploadedbyProvider", objProvDoc.IsDocumentUploadedbyProvider));
            lstParameter.Add(new MySqlParameter("IsDocumentUploadedbyStaff", objProvDoc.IsDocumentUploadedbyStaff));
            lstParameter.Add(new MySqlParameter("ReferenceNumber", objProvDoc.ReferenceNumber));
            lstParameter.Add(new MySqlParameter("IsActive", objProvDoc.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objProvDoc.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objProvDoc.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objProvDoc.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objProvDoc.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objProvDoc.ModifiedOn));
            lstParameter.Add(new MySqlParameter("ProviderDocumentGuid", objProvDoc.ProviderDocumentGuid));

            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "ProviderDocument_Save", lstParameter.ToArray());
            return returnValue;
        }

        public int Delete_ProviderDocument_By_ProviderDocId_And_ProviderId(int? ProviderDocId, int? ProviderId, int? ModBy)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ProviderDocId", ProviderDocId));
            lstParameter.Add(new MySqlParameter("ProviderId", ProviderId));
            lstParameter.Add(new MySqlParameter("ModBy", ModBy));
            
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "ProviderDocument_Delete", lstParameter.ToArray());
            return returnValue;
        }

        private ProviderDocumentGET FetchEntityGET(DataRow dr)
        {
            ProviderDocumentGET objEntity = new ProviderDocumentGET();
            if (dr.Table.Columns.Contains("ProviderId") && dr["ProviderId"] != DBNull.Value)
            {
                objEntity.ProviderId = Convert.ToInt32(dr["ProviderId"]);
            }
            if (dr.Table.Columns.Contains("DocumentId") && dr["DocumentId"] != DBNull.Value)
            {
                objEntity.DocumentId = Convert.ToInt32(dr["DocumentId"]);
            }
            if (dr.Table.Columns.Contains("ProviderDocumentId") && dr["ProviderDocumentId"] != DBNull.Value)
            {
                objEntity.ProviderDocumentId = Convert.ToInt32(dr["ProviderDocumentId"]);
            }
            if (dr.Table.Columns.Contains("DocumentTypeIdName") && dr["DocumentTypeIdName"] != DBNull.Value)
            {
                objEntity.DocumentTypeIdName = Convert.ToString(dr["DocumentTypeIdName"]);
            }
            if (dr.Table.Columns.Contains("DocumentTypeDesc") && dr["DocumentTypeDesc"] != DBNull.Value)
            {
                objEntity.DocumentTypeDesc = Convert.ToString(dr["DocumentTypeDesc"]);
            }

            return objEntity;
        }
    }
}
