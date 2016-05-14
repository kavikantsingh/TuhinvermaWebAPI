using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class PriorPasswordDAL : BaseDAL
    {
        public int Save_PriorPassword(PriorPassword objPriorPassword)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("PriorPasswordId", objPriorPassword.PriorPasswordId));
            lstParameter.Add(new MySqlParameter("UserId", objPriorPassword.UserId));
            lstParameter.Add(new MySqlParameter("IndividualId", objPriorPassword.IndividualId));
            lstParameter.Add(new MySqlParameter("PasswordHash", objPriorPassword.PasswordHash));
            lstParameter.Add(new MySqlParameter("PasswordSalt", objPriorPassword.PasswordSalt));
            lstParameter.Add(new MySqlParameter("AccessCodeHash", objPriorPassword.AccessCodeHash));
            lstParameter.Add(new MySqlParameter("AccessCodeSalt", objPriorPassword.AccessCodeSalt));
            lstParameter.Add(new MySqlParameter("IsActive", objPriorPassword.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objPriorPassword.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objPriorPassword.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objPriorPassword.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objPriorPassword.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objPriorPassword.ModifiedOn));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "priorpassword_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<PriorPassword> Get_All_PriorPassword()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "PRIORPASSWORD_GET_ALL");
            List<PriorPassword> lstEntity = new List<PriorPassword>();
            PriorPassword objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public PriorPassword Get_PriorPassword_By_PriorPasswordId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("PriorPasswordId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "PRIORPASSWORD_GET_BY_PriorPasswordId", lstParameter.ToArray());
            PriorPassword objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private PriorPassword FetchEntity(DataRow dr)
        {
            PriorPassword objEntity = new PriorPassword();
            if (dr.Table.Columns.Contains("PriorPasswordId") && dr["PriorPasswordId"] != DBNull.Value)
            {
                objEntity.PriorPasswordId = Convert.ToInt32(dr["PriorPasswordId"]);
            }
            if (dr.Table.Columns.Contains("UserId") && dr["UserId"] != DBNull.Value)
            {
                objEntity.UserId = Convert.ToInt32(dr["UserId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("PasswordHash") && dr["PasswordHash"] != DBNull.Value)
            {
                objEntity.PasswordHash = Convert.ToString(dr["PasswordHash"]);
            }
            if (dr.Table.Columns.Contains("PasswordSalt") && dr["PasswordSalt"] != DBNull.Value)
            {
                objEntity.PasswordSalt = Convert.ToString(dr["PasswordSalt"]);
            }
            if (dr.Table.Columns.Contains("AccessCodeHash") && dr["AccessCodeHash"] != DBNull.Value)
            {
                objEntity.AccessCodeHash = Convert.ToString(dr["AccessCodeHash"]);
            }
            if (dr.Table.Columns.Contains("AccessCodeSalt") && dr["AccessCodeSalt"] != DBNull.Value)
            {
                objEntity.AccessCodeSalt = Convert.ToString(dr["AccessCodeSalt"]);
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
    }
}
