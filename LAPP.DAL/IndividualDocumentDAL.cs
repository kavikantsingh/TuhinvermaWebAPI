using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using LAPP.ENTITY;
using MySql.Data.MySqlClient;
namespace LAPP.DAL
{
    public class IndividualDocumentDAL : BaseDAL
    {
        public int Save_IndividualDocument(IndividualDocument objIndividualDocument)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualDocumentId", objIndividualDocument.IndividualDocumentId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualDocument.IndividualId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objIndividualDocument.ApplicationId));
            lstParameter.Add(new MySqlParameter("DocumentLkToPageTabSectionId", objIndividualDocument.DocumentLkToPageTabSectionId));
            lstParameter.Add(new MySqlParameter("DocumentLkToPageTabSectionCode", objIndividualDocument.DocumentLkToPageTabSectionCode));
            lstParameter.Add(new MySqlParameter("DocumentTypeName", objIndividualDocument.DocumentTypeName));
            lstParameter.Add(new MySqlParameter("EffectiveDate", objIndividualDocument.EffectiveDate));
            lstParameter.Add(new MySqlParameter("EndDate", objIndividualDocument.EndDate));
            lstParameter.Add(new MySqlParameter("IsDocumentUploadedbyIndividual", objIndividualDocument.IsDocumentUploadedbyIndividual));
            lstParameter.Add(new MySqlParameter("IsDocumentUploadedbyStaff", objIndividualDocument.IsDocumentUploadedbyStaff));
            lstParameter.Add(new MySqlParameter("ReferenceNumber", objIndividualDocument.ReferenceNumber));

            lstParameter.Add(new MySqlParameter("IsActive", objIndividualDocument.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualDocument.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualDocument.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objIndividualDocument.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objIndividualDocument.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objIndividualDocument.ModifiedOn));

            lstParameter.Add(new MySqlParameter("IndividualDocumentGuid", objIndividualDocument.IndividualDocumentGuid));
            lstParameter.Add(new MySqlParameter("DocumentPath", GetNullValue.ByDataType(objIndividualDocument.DocumentPath)));

            lstParameter.Add(new MySqlParameter("DocumentId", GetNullValue.ByDataType(objIndividualDocument.DocumentId)));
            lstParameter.Add(new MySqlParameter("DocumentCd", GetNullValue.ByDataType(objIndividualDocument.DocumentCd)));
            lstParameter.Add(new MySqlParameter("DocumentTypeId", GetNullValue.ByDataType(objIndividualDocument.DocumentTypeId)));
            lstParameter.Add(new MySqlParameter("DocumentName", GetNullValue.ByDataType(objIndividualDocument.DocumentName)));
            lstParameter.Add(new MySqlParameter("OtherDocumentTypeName", GetNullValue.ByDataType(objIndividualDocument.OtherDocumentTypeName)));
            lstParameter.Add(new MySqlParameter("LicenseeReprint", objIndividualDocument.LicenseeReprint));

            //LicenseeReprint

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "IndividualDocument_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<IndividualDocument> Get_All_IndividualDocument()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualDocument_GET_ALL");
            List<IndividualDocument> lstEntity = new List<IndividualDocument>();
            IndividualDocument objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<IndividualDocument> Get_IndividualDocument_by_IndividualId(int IndividualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            //lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualDocument_GET_BY_IndividualId", lstParameter.ToArray());
            List<IndividualDocument> lstEntity = new List<IndividualDocument>();
            IndividualDocument objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }
        public List<IndividualDocument> Get_IndividualDocument_by_IndividualIdAndApplicationId(int IndividualId, int ApplicationId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            lstParameter.Add(new MySqlParameter("G_ApplicationId", ApplicationId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualDocument_GET_BY_IndividualIdAndApplicationId", lstParameter.ToArray());
            List<IndividualDocument> lstEntity = new List<IndividualDocument>();
            IndividualDocument objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }
        public IndividualDocument Get_IndividualDocument_By_IndividualDocumentId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualDocumentId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualDocument_GET_BY_IndividualDocumentId", lstParameter.ToArray());
            IndividualDocument objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private IndividualDocument FetchEntity(DataRow dr)
        {
            IndividualDocument objEntity = new IndividualDocument();

            if (dr.Table.Columns.Contains("IndividualDocumentId") && dr["IndividualDocumentId"] != DBNull.Value)
            {
                objEntity.IndividualDocumentId = Convert.ToInt32(dr["IndividualDocumentId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationId") && dr["ApplicationId"] != DBNull.Value)
            {
                objEntity.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
            }
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
            if (dr.Table.Columns.Contains("EffectiveDate") && dr["EffectiveDate"] != DBNull.Value)
            {
                objEntity.EffectiveDate = Convert.ToDateTime(dr["EffectiveDate"]);
            }
            if (dr.Table.Columns.Contains("EndDate") && dr["EndDate"] != DBNull.Value)
            {
                objEntity.EndDate = Convert.ToDateTime(dr["EndDate"]);
            }

            if (dr.Table.Columns.Contains("IsDocumentUploadedbyIndividual") && dr["IsDocumentUploadedbyIndividual"] != DBNull.Value)
            {
                objEntity.IsDocumentUploadedbyIndividual = Convert.ToBoolean(dr["IsDocumentUploadedbyIndividual"]);
            }
            if (dr.Table.Columns.Contains("IsDocumentUploadedbyStaff") && dr["IsDocumentUploadedbyStaff"] != DBNull.Value)
            {
                objEntity.IsDocumentUploadedbyStaff = Convert.ToBoolean(dr["IsDocumentUploadedbyStaff"]);
            }

            if (dr.Table.Columns.Contains("ReferenceNumber") && dr["ReferenceNumber"] != DBNull.Value)
            {
                objEntity.ReferenceNumber = Convert.ToString(dr["ReferenceNumber"]);
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

            if (dr.Table.Columns.Contains("IndividualDocumentGuid") && dr["IndividualDocumentGuid"] != DBNull.Value)
            {
                objEntity.IndividualDocumentGuid = dr["IndividualDocumentGuid"].ToString();
            }


            if (dr.Table.Columns.Contains("DocumentPath") && dr["DocumentPath"] != DBNull.Value)
            {
                objEntity.DocumentPath = dr["DocumentPath"].ToString();
            }

            if (dr.Table.Columns.Contains("DocumentId") && dr["DocumentId"] != DBNull.Value)
            {
                objEntity.DocumentId = Convert.ToInt32(dr["DocumentId"].ToString());
            }

            if (dr.Table.Columns.Contains("DocumentCd") && dr["DocumentCd"] != DBNull.Value)
            {
                objEntity.DocumentCd = dr["DocumentCd"].ToString();
            }

            if (dr.Table.Columns.Contains("DocumentTypeId") && dr["DocumentTypeId"] != DBNull.Value)
            {
                objEntity.DocumentTypeId = Convert.ToInt32(dr["DocumentTypeId"].ToString());
            }

            if (dr.Table.Columns.Contains("DocumentName") && dr["DocumentName"] != DBNull.Value)
            {
                objEntity.DocumentName = dr["DocumentName"].ToString();
            }

            if (dr.Table.Columns.Contains("OtherDocumentTypeName") && dr["OtherDocumentTypeName"] != DBNull.Value)
            {
                objEntity.OtherDocumentTypeName = dr["OtherDocumentTypeName"].ToString();
            }

            if (dr.Table.Columns.Contains("LicenseeReprint") && dr["LicenseeReprint"] != DBNull.Value)
            {
                objEntity.LicenseeReprint = Convert.ToBoolean(dr["LicenseeReprint"].ToString());
            }


            return objEntity;

        }
    }
}
