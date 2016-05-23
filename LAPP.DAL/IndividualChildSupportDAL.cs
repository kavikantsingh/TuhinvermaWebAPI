using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class IndividualChildSupportDAL : BaseDAL
    {
        public int Save_IndividualChildSupport(IndividualChildSupport objIndividualChildSupport)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualChildSupportId", objIndividualChildSupport.IndividualChildSupportId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualChildSupport.IndividualId));
            lstParameter.Add(new MySqlParameter("ContentItemLkId", objIndividualChildSupport.ContentItemLkId));
            lstParameter.Add(new MySqlParameter("ContentItemNumber", objIndividualChildSupport.ContentItemNumber));
            lstParameter.Add(new MySqlParameter("ContentItemResponse", objIndividualChildSupport.ContentItemResponse));

            lstParameter.Add(new MySqlParameter("IsActive", objIndividualChildSupport.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualChildSupport.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualChildSupport.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objIndividualChildSupport.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objIndividualChildSupport.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objIndividualChildSupport.ModifiedOn));

            lstParameter.Add(new MySqlParameter("IndividualChildSupportGuid", objIndividualChildSupport.IndividualChildSupportGuid));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "individualchildsupport_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<IndividualChildSupport> Get_All_IndividualChildSupport()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualChildSupport_Get_All");
            List<IndividualChildSupport> lstEntity = new List<IndividualChildSupport>();
            IndividualChildSupport objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }
        public List<IndividualChildSupport> Get_IndividualChildSupport_By_IndividualId(int IndividualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("_IndividualId", IndividualId));
            lstParameter.Add(new MySqlParameter("_IndividualChildSupportGuid", Guid.NewGuid().ToString()));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualchildsupport_Getby_IndividualId", lstParameter.ToArray());
            List<IndividualChildSupport> lstEntity = new List<IndividualChildSupport>();
            IndividualChildSupport objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public IndividualChildSupport Get_IndividualChildSupport_By_IndividualChildSupportId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("_IndividualChildSupportId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualchildsupport_Getby_IndividualchildsupportId", lstParameter.ToArray());
            IndividualChildSupport objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private IndividualChildSupport FetchEntity(DataRow dr)
        {
            IndividualChildSupport objEntity = new IndividualChildSupport();

            if (dr.Table.Columns.Contains("IndividualChildSupportId") && dr["IndividualChildSupportId"] != DBNull.Value)
            {
                objEntity.IndividualChildSupportId = Convert.ToInt32(dr["IndividualChildSupportId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("ContentItemLkId") && dr["ContentItemLkId"] != DBNull.Value)
            {
                objEntity.ContentItemLkId = Convert.ToInt32(dr["ContentItemLkId"]);
            }
            if (dr.Table.Columns.Contains("ContentItem#") && dr["ContentItem#"] != DBNull.Value)
            {
                objEntity.ContentItemNumber = Convert.ToInt32(dr["ContentItem#"]);
            }
            if (dr.Table.Columns.Contains("ContentItemResponse") && dr["ContentItemResponse"] != DBNull.Value)
            {
                objEntity.ContentItemResponse = Convert.ToBoolean(dr["ContentItemResponse"]);
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

            if (dr.Table.Columns.Contains("IndividualChildSupportGuid") && dr["IndividualChildSupportGuid"] != DBNull.Value)
            {
                objEntity.IndividualChildSupportGuid = Convert.ToString(dr["IndividualChildSupportGuid"]);
            }


            if (dr.Table.Columns.Contains("ContentItemLkCode") && dr["ContentItemLkCode"] != DBNull.Value)
            {
                objEntity.ContentItemLkCode = Convert.ToString(dr["ContentItemLkCode"]);
            }


            if (dr.Table.Columns.Contains("ContentDescription") && dr["ContentDescription"] != DBNull.Value)
            {
                objEntity.ContentDescription = Convert.ToString(dr["ContentDescription"]);
            }
            return objEntity;

        }
    }
}
