using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class IndividualDeclarationDAL : BaseDAL
    {
        public int Save_IndividualDeclaration(IndividualDeclaration objIndividualDeclaration)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualDeclarationId", objIndividualDeclaration.IndividualDeclarationId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualDeclaration.IndividualId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objIndividualDeclaration.ApplicationId));
            lstParameter.Add(new MySqlParameter("ContentItemLkId", objIndividualDeclaration.ContentItemLkId));
            lstParameter.Add(new MySqlParameter("ContentItem", objIndividualDeclaration.ContentItem));
            lstParameter.Add(new MySqlParameter("ContentItemResponse", objIndividualDeclaration.ContentItemResponse));
            lstParameter.Add(new MySqlParameter("Desc", objIndividualDeclaration.Desc));
            lstParameter.Add(new MySqlParameter("DeclarationDate", objIndividualDeclaration.DeclarationDate));
            lstParameter.Add(new MySqlParameter("DateofApplication", objIndividualDeclaration.DateofApplication));
            lstParameter.Add(new MySqlParameter("NoticeofMandatoryReporter", objIndividualDeclaration.NoticeofMandatoryReporter));
            lstParameter.Add(new MySqlParameter("IsActive", objIndividualDeclaration.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualDeclaration.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualDeclaration.CreatedBy));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objIndividualDeclaration.ModifiedBy));
            lstParameter.Add(new MySqlParameter("IndividualDeclarationGuid", objIndividualDeclaration.IndividualDeclarationGuid));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "INDIVIDUALDECLARATION_SAVE", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<IndividualDeclaration> Get_All_IndividualDeclaration()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "INDIVIDUALDECLARATION_GET_ALL");
            List<IndividualDeclaration> lstEntity = new List<IndividualDeclaration>();
            IndividualDeclaration objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public IndividualDeclaration Get_IndividualDeclaration_By_IndividualDeclarationId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualDeclarationId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "INDIVIDUALDECLARATION_GET_BY_IndividualDeclarationId", lstParameter.ToArray());
            IndividualDeclaration objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private IndividualDeclaration FetchEntity(DataRow dr)
        {
            IndividualDeclaration objEntity = new IndividualDeclaration();
            if (dr.Table.Columns.Contains("IndividualDeclarationId") && dr["IndividualDeclarationId"] != DBNull.Value)
            {
                objEntity.IndividualDeclarationId = Convert.ToInt32(dr["IndividualDeclarationId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationId") && dr["ApplicationId"] != DBNull.Value)
            {
                objEntity.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
            }
            if (dr.Table.Columns.Contains("ContentItemLkId") && dr["ContentItemLkId"] != DBNull.Value)
            {
                objEntity.ContentItemLkId = Convert.ToInt32(dr["ContentItemLkId"]);
            }
            if (dr.Table.Columns.Contains("ContentItem#") && dr["ContentItem#"] != DBNull.Value)
            {
                objEntity.ContentItem = Convert.ToInt32(dr["ContentItem#"]);
            }
            if (dr.Table.Columns.Contains("ContentItemResponse") && dr["ContentItemResponse"] != DBNull.Value)
            {
                objEntity.ContentItemResponse = Convert.ToBoolean(dr["ContentItemResponse"]);
            }
            if (dr.Table.Columns.Contains("Desc") && dr["Desc"] != DBNull.Value)
            {
                objEntity.Desc = Convert.ToString(dr["Desc"]);
            }
            if (dr.Table.Columns.Contains("DeclarationDate") && dr["DeclarationDate"] != DBNull.Value)
            {
                objEntity.DeclarationDate = Convert.ToDateTime(dr["DeclarationDate"]);
            }
            if (dr.Table.Columns.Contains("DateofApplication") && dr["DateofApplication"] != DBNull.Value)
            {
                objEntity.DateofApplication = Convert.ToDateTime(dr["DateofApplication"]);
            }
            if (dr.Table.Columns.Contains("NoticeofMandatoryReporter") && dr["NoticeofMandatoryReporter"] != DBNull.Value)
            {
                objEntity.NoticeofMandatoryReporter = Convert.ToBoolean(dr["NoticeofMandatoryReporter"]);
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
            if (dr.Table.Columns.Contains("IndividualDeclarationGuid") && dr["IndividualDeclarationGuid"] != DBNull.Value)
            {
                objEntity.IndividualDeclarationGuid = (Guid)dr["IndividualDeclarationGuid"];
            }
            return objEntity;

        }
    }
}
