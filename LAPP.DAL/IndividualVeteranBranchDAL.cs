using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class IndividualVeteranBranchDAL : BaseDAL
    {
        public int Save_IndividualVeteranBranch(IndividualVeteranBranch objIndividualVeteranBranch)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualVeteranBranchId", objIndividualVeteranBranch.IndividualVeteranBranchId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualVeteranBranch.IndividualId));
            lstParameter.Add(new MySqlParameter("IndividualVeteranId", objIndividualVeteranBranch.IndividualVeteranId));
            lstParameter.Add(new MySqlParameter("BranchofServicesId", objIndividualVeteranBranch.BranchofServicesId));
            lstParameter.Add(new MySqlParameter("BranchofServicesIdResponse", objIndividualVeteranBranch.BranchofServicesIdResponse));

            lstParameter.Add(new MySqlParameter("IsActive", objIndividualVeteranBranch.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualVeteranBranch.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualVeteranBranch.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objIndividualVeteranBranch.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objIndividualVeteranBranch.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objIndividualVeteranBranch.ModifiedOn));

            lstParameter.Add(new MySqlParameter("IndividualVeteranBranchGuid", objIndividualVeteranBranch.IndividualVeteranBranchGuid));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "individualveteranbranch_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<IndividualVeteranBranch> Get_All_IndividualVeteranBranch()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualVeteranBranch_Get_All");
            List<IndividualVeteranBranch> lstEntity = new List<IndividualVeteranBranch>();
            IndividualVeteranBranch objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }
        public List<IndividualVeteranBranch> Get_IndividualVeteranBranch_By_IndividualId_VeteranId(int IndividualId,int IndividualVeteranId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("_IndividualId", IndividualId));
            lstParameter.Add(new MySqlParameter("_IndividualVeteranId", IndividualVeteranId));
            lstParameter.Add(new MySqlParameter("_IndividualVeteranBranchGuid", Guid.NewGuid().ToString()));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualveteranbranch_Getby_IndividualId_VeteranId", lstParameter.ToArray());
            List<IndividualVeteranBranch> lstEntity = new List<IndividualVeteranBranch>();
            IndividualVeteranBranch objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }
        public IndividualVeteranBranch Get_IndividualVeteranBranch_By_IndividualVeteranBranchId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualVeteranBranchId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualVeteranBranch_GET_BY_IndividualVeteranBranchId", lstParameter.ToArray());
            IndividualVeteranBranch objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private IndividualVeteranBranch FetchEntity(DataRow dr)
        {
            IndividualVeteranBranch objEntity = new IndividualVeteranBranch();

            if (dr.Table.Columns.Contains("IndividualVeteranBranchId") && dr["IndividualVeteranBranchId"] != DBNull.Value)
            {
                objEntity.IndividualVeteranBranchId = Convert.ToInt32(dr["IndividualVeteranBranchId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("IndividualVeteranId") && dr["IndividualVeteranId"] != DBNull.Value)
            {
                objEntity.IndividualVeteranId = Convert.ToInt32(dr["IndividualVeteranId"]);
            }
            if (dr.Table.Columns.Contains("BranchofServicesId") && dr["BranchofServicesId"] != DBNull.Value)
            {
                objEntity.BranchofServicesId = Convert.ToInt32(dr["BranchofServicesId"]);
            }
            if (dr.Table.Columns.Contains("BranchofServicesIdResponse") && dr["BranchofServicesIdResponse"] != DBNull.Value)
            {
                objEntity.BranchofServicesIdResponse = Convert.ToBoolean(dr["BranchofServicesIdResponse"]);
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

            if (dr.Table.Columns.Contains("IndividualVeteranBranchGuid") && dr["IndividualVeteranBranchGuid"] != DBNull.Value)
            {
                objEntity.IndividualVeteranBranchGuid = Convert.ToString(dr["IndividualVeteranBranchGuid"]);
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
