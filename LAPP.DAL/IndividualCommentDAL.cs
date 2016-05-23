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
    public class IndividualCommentDAL : BaseDAL
    {
        public int Save_IndividualComment(IndividualComment objIndividualComment)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualCommentId", objIndividualComment.IndividualCommentId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualComment.IndividualId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objIndividualComment.ApplicationId));
            lstParameter.Add(new MySqlParameter("MasterTransactionId", objIndividualComment.MasterTransactionId));
            lstParameter.Add(new MySqlParameter("PageModuleId", objIndividualComment.PageModuleId));
            lstParameter.Add(new MySqlParameter("PageModuleTabSubModuleId", objIndividualComment.PageModuleTabSubModuleId));
            lstParameter.Add(new MySqlParameter("PageTabSectionId", objIndividualComment.PageTabSectionId));
            lstParameter.Add(new MySqlParameter("EffectiveDate", objIndividualComment.EffectiveDate));
            lstParameter.Add(new MySqlParameter("EndDate", objIndividualComment.EndDate));
            lstParameter.Add(new MySqlParameter("CommentText", objIndividualComment.CommentText));
            lstParameter.Add(new MySqlParameter("ReferenceNumber", objIndividualComment.ReferenceNumber));

            lstParameter.Add(new MySqlParameter("IsActive", objIndividualComment.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualComment.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualComment.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objIndividualComment.CreatedOn));

            lstParameter.Add(new MySqlParameter("IndividualCommentGuid", objIndividualComment.IndividualCommentGuid));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "IndividualComment_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<IndividualComment> Get_All_IndividualComment()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualComment_GET_ALL");
            List<IndividualComment> lstEntity = new List<IndividualComment>();
            IndividualComment objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<IndividualComment> Get_IndividualComment_by_IndividualId(int IndividualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            //lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualComment_GET_BY_IndividualId", lstParameter.ToArray());
            List<IndividualComment> lstEntity = new List<IndividualComment>();
            IndividualComment objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public IndividualComment Get_IndividualComment_By_IndividualCommentId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualCommentId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualComment_GET_BY_IndividualCommentId", lstParameter.ToArray());
            IndividualComment objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private IndividualComment FetchEntity(DataRow dr)
        {
            IndividualComment objEntity = new IndividualComment();

            if (dr.Table.Columns.Contains("IndividualCommentId") && dr["IndividualCommentId"] != DBNull.Value)
            {
                objEntity.IndividualCommentId = Convert.ToInt32(dr["IndividualCommentId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationId") && dr["ApplicationId"] != DBNull.Value)
            {
                objEntity.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
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

            if (dr.Table.Columns.Contains("CommentText") && dr["CommentText"] != DBNull.Value)
            {
                objEntity.CommentText = Convert.ToString(dr["CommentText"]);
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

            if (dr.Table.Columns.Contains("IndividualCommentGuid") && dr["IndividualCommentGuid"] != DBNull.Value)
            {
                objEntity.IndividualCommentGuid = dr["IndividualCommentGuid"].ToString();
            }

            return objEntity;

        }
    }
}
