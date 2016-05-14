using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class IndividualLegalDAL : BaseDAL
    {
        public int Save_IndividualLegal(IndividualLegal objIndividualLegal)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualLegalId", objIndividualLegal.IndividualLegalId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualLegal.IndividualId));
            lstParameter.Add(new MySqlParameter("ContentItemLkId", objIndividualLegal.ContentItemLkId));
            lstParameter.Add(new MySqlParameter("ContentItemHash", objIndividualLegal.ContentItemHash));
            lstParameter.Add(new MySqlParameter("ContentItemResponse", objIndividualLegal.ContentItemResponse));
            lstParameter.Add(new MySqlParameter("Desc", objIndividualLegal.Desc));

            lstParameter.Add(new MySqlParameter("IsActive", objIndividualLegal.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualLegal.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualLegal.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objIndividualLegal.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objIndividualLegal.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objIndividualLegal.ModifiedOn));

            lstParameter.Add(new MySqlParameter("IndividualLegalGuid", objIndividualLegal.IndividualLegalGuid));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "individuallegal_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<IndividualLegal> Get_All_IndividualLegal()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualLegal_Get_All");
            List<IndividualLegal> lstEntity = new List<IndividualLegal>();
            IndividualLegal objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public IndividualLegal Get_IndividualLegal_By_IndividualLegalId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualLegalId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualLegal_GET_BY_IndividualLegalId", lstParameter.ToArray());
            IndividualLegal objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private IndividualLegal FetchEntity(DataRow dr)
        {
            IndividualLegal objEntity = new IndividualLegal();

            if (dr.Table.Columns.Contains("IndividualLegalId") && dr["IndividualLegalId"] != DBNull.Value)
            {
                objEntity.IndividualLegalId = Convert.ToInt32(dr["IndividualLegalId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("ContentItemLkId") && dr["ContentItemLkId"] != DBNull.Value)
            {
                objEntity.ContentItemLkId = Convert.ToInt32(dr["ContentItemLkId"]);
            }
            if (dr.Table.Columns.Contains("ContentItemHash") && dr["ContentItemHash"] != DBNull.Value)
            {
                objEntity.ContentItemHash = Convert.ToInt32(dr["ContentItemHash"]);
            }
            if (dr.Table.Columns.Contains("ContentItemResponse") && dr["ContentItemResponse"] != DBNull.Value)
            {
                objEntity.ContentItemResponse = Convert.ToBoolean(dr["ContentItemResponse"]);
            }
            if (dr.Table.Columns.Contains("Desc") && dr["Desc"] != DBNull.Value)
            {
                objEntity.Desc = Convert.ToString(dr["Desc"]);
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

            if (dr.Table.Columns.Contains("IndividualLegalGuid") && dr["IndividualLegalGuid"] != DBNull.Value)
            {
                objEntity.IndividualLegalGuid = Convert.ToString(dr["IndividualLegalGuid"]);
            }
            return objEntity;

        }
    }
}
