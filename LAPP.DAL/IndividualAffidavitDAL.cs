using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class IndividualAffidavitDAL : BaseDAL
    {
        public int Save_IndividualAffidavit(IndividualAffidavit objIndividualAffidavit)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualAffidavitId", objIndividualAffidavit.IndividualAffidavitId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualAffidavit.IndividualId));
            lstParameter.Add(new MySqlParameter("ContentItemLkId", objIndividualAffidavit.ContentItemLkId));
            lstParameter.Add(new MySqlParameter("ContentItemNumber", objIndividualAffidavit.ContentItemNumber));
            lstParameter.Add(new MySqlParameter("ContentItemResponse", objIndividualAffidavit.ContentItemResponse));
            lstParameter.Add(new MySqlParameter("Description", objIndividualAffidavit.Desc));
            lstParameter.Add(new MySqlParameter("Name", objIndividualAffidavit.Name));
            lstParameter.Add(new MySqlParameter("SignatureName", objIndividualAffidavit.SignatureName));
            lstParameter.Add(new MySqlParameter("Date", objIndividualAffidavit.Date));

            lstParameter.Add(new MySqlParameter("IsActive", objIndividualAffidavit.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualAffidavit.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualAffidavit.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objIndividualAffidavit.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objIndividualAffidavit.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objIndividualAffidavit.ModifiedOn));

            lstParameter.Add(new MySqlParameter("IndividualAffidavitGuid", objIndividualAffidavit.IndividualAffidavitGuid));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "individualaffidavit_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<IndividualAffidavit> Get_All_IndividualAffidavit()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualAffidavit_Get_All");
            List<IndividualAffidavit> lstEntity = new List<IndividualAffidavit>();
            IndividualAffidavit objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public IndividualAffidavit Get_IndividualAffidavit_By_IndividualAffidavitId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualAffidavitId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualAffidavit_GET_BY_IndividualAffidavitId", lstParameter.ToArray());
            IndividualAffidavit objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public IndividualAffidavit Get_IndividualAffidavit_By_IndividualId(int IndividualId)
        {
            string SGUID = Guid.NewGuid().ToString();

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            lstParameter.Add(new MySqlParameter("G_IndividualAffidavitGuid", SGUID));

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualaffidavit_Get_By_IndividualId", lstParameter.ToArray());
            IndividualAffidavit objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private IndividualAffidavit FetchEntity(DataRow dr)
        {
            IndividualAffidavit objEntity = new IndividualAffidavit();

            if (dr.Table.Columns.Contains("IndividualAffidavitId") && dr["IndividualAffidavitId"] != DBNull.Value)
            {
                objEntity.IndividualAffidavitId = Convert.ToInt32(dr["IndividualAffidavitId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("ContentItemLkId") && dr["ContentItemLkId"] != DBNull.Value)
            {
                objEntity.ContentItemLkId = Convert.ToInt32(dr["ContentItemLkId"]);
            }
            if (dr.Table.Columns.Contains("ContentItemNumber") && dr["ContentItemNumber"] != DBNull.Value)
            {
                objEntity.ContentItemNumber = Convert.ToInt32(dr["ContentItemNumber"]);
            }
            if (dr.Table.Columns.Contains("ContentItemResponse") && dr["ContentItemResponse"] != DBNull.Value)
            {
                objEntity.ContentItemResponse = Convert.ToBoolean(dr["ContentItemResponse"]);
            }
            if (dr.Table.Columns.Contains("Desc") && dr["Desc"] != DBNull.Value)
            {
                objEntity.Desc = Convert.ToString(dr["Desc"]);
            }

            if (dr.Table.Columns.Contains("Name") && dr["Name"] != DBNull.Value)
            {
                objEntity.Name = Convert.ToString(dr["Name"]);
            }
            if (dr.Table.Columns.Contains("SignatureName") && dr["SignatureName"] != DBNull.Value)
            {
                objEntity.SignatureName = Convert.ToString(dr["SignatureName"]);
            }

            if (dr.Table.Columns.Contains("Date") && dr["Date"] != DBNull.Value)
            {
                objEntity.Date = Convert.ToDateTime(dr["Date"]);
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

            if (dr.Table.Columns.Contains("IndividualAffidavitGuid") && dr["IndividualAffidavitGuid"] != DBNull.Value)
            {
                objEntity.IndividualAffidavitGuid = Convert.ToString(dr["IndividualAffidavitGuid"]);
            }

            if (dr.Table.Columns.Contains("ContentDescription") && dr["ContentDescription"] != DBNull.Value)
            {
                objEntity.ContentDescription = Convert.ToString(dr["ContentDescription"]);
            }

            return objEntity;

        }
    }
}
