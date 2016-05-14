using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class IndividualAccessCodeDAL : BaseDAL
    {
        public int Save_IndividualAccessCode(IndividualAccessCode objIndividualAccessCode)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualAccessCodeId", objIndividualAccessCode.IndividualAccessCodeId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualAccessCode.IndividualId));
            lstParameter.Add(new MySqlParameter("AccessCodeHash", objIndividualAccessCode.AccessCodeHash));
            lstParameter.Add(new MySqlParameter("AccessCodeSalt", objIndividualAccessCode.AccessCodeSalt));
            lstParameter.Add(new MySqlParameter("AccessCodeExpirationDate", objIndividualAccessCode.AccessCodeExpirationDate));
            lstParameter.Add(new MySqlParameter("AccessCodeChangedOn", objIndividualAccessCode.AccessCodeChangedOn));
            lstParameter.Add(new MySqlParameter("LastLoginDate", objIndividualAccessCode.LastLoginDate));
            lstParameter.Add(new MySqlParameter("LastLoginIp", objIndividualAccessCode.LastLoginIp));
            lstParameter.Add(new MySqlParameter("EulaAcceptedOn", objIndividualAccessCode.EulaAcceptedOn));
            lstParameter.Add(new MySqlParameter("SourceId", objIndividualAccessCode.SourceId));
            lstParameter.Add(new MySqlParameter("IsPending", objIndividualAccessCode.IsPending));
            lstParameter.Add(new MySqlParameter("IsActive", objIndividualAccessCode.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualAccessCode.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualAccessCode.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objIndividualAccessCode.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objIndividualAccessCode.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objIndividualAccessCode.ModifiedOn));
            lstParameter.Add(new MySqlParameter("IndividualAccessCodeGuid", objIndividualAccessCode.IndividualAccessCodeGuid));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "individualaccesscode_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<IndividualAccessCode> Get_All_IndividualAccessCode()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "INDIVIDUALACCESSCODE_GET_ALL");
            List<IndividualAccessCode> lstEntity = new List<IndividualAccessCode>();
            IndividualAccessCode objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public IndividualAccessCode Get_IndividualAccessCode_By_IndividualAccessCodeId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualAccessCodeId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "INDIVIDUALACCESSCODE_GET_BY_IndividualAccessCodeId", lstParameter.ToArray());
            IndividualAccessCode objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private IndividualAccessCode FetchEntity(DataRow dr)
        {
            IndividualAccessCode objEntity = new IndividualAccessCode();
            if (dr.Table.Columns.Contains("IndividualAccessCodeId") && dr["IndividualAccessCodeId"] != DBNull.Value)
            {
                objEntity.IndividualAccessCodeId = Convert.ToInt32(dr["IndividualAccessCodeId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("AccessCodeHash") && dr["AccessCodeHash"] != DBNull.Value)
            {
                objEntity.AccessCodeHash = Convert.ToString(dr["AccessCodeHash"]);
            }
            if (dr.Table.Columns.Contains("AccessCodeSalt") && dr["AccessCodeSalt"] != DBNull.Value)
            {
                objEntity.AccessCodeSalt = Convert.ToString(dr["AccessCodeSalt"]);
            }
            if (dr.Table.Columns.Contains("AccessCodeExpirationDate") && dr["AccessCodeExpirationDate"] != DBNull.Value)
            {
                objEntity.AccessCodeExpirationDate = Convert.ToDateTime(dr["AccessCodeExpirationDate"]);
            }
            if (dr.Table.Columns.Contains("AccessCodeChangedOn") && dr["AccessCodeChangedOn"] != DBNull.Value)
            {
                objEntity.AccessCodeChangedOn = Convert.ToDateTime(dr["AccessCodeChangedOn"]);
            }
            if (dr.Table.Columns.Contains("LastLoginDate") && dr["LastLoginDate"] != DBNull.Value)
            {
                objEntity.LastLoginDate = Convert.ToDateTime(dr["LastLoginDate"]);
            }
            if (dr.Table.Columns.Contains("LastLoginIp") && dr["LastLoginIp"] != DBNull.Value)
            {
                objEntity.LastLoginIp = Convert.ToString(dr["LastLoginIp"]);
            }
            if (dr.Table.Columns.Contains("EulaAcceptedOn") && dr["EulaAcceptedOn"] != DBNull.Value)
            {
                objEntity.EulaAcceptedOn = Convert.ToDateTime(dr["EulaAcceptedOn"]);
            }
            if (dr.Table.Columns.Contains("SourceId") && dr["SourceId"] != DBNull.Value)
            {
                objEntity.SourceId = Convert.ToInt32(dr["SourceId"]);
            }
            if (dr.Table.Columns.Contains("IsPending") && dr["IsPending"] != DBNull.Value)
            {
                objEntity.IsPending = Convert.ToBoolean(dr["IsPending"]);
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
            if (dr.Table.Columns.Contains("IndividualAccessCodeGuid") && dr["IndividualAccessCodeGuid"] != DBNull.Value)
            {
                objEntity.IndividualAccessCodeGuid = Convert.ToString(dr["IndividualAccessCodeGuid"]);
            }
            return objEntity;

        }
    }
}
