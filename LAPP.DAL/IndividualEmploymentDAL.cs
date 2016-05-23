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
    public class IndividualEmploymentDAL : BaseDAL
    {
        public int Save_IndividualEmployment(IndividualEmployment objIndividualEmployment)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualEmploymentId", objIndividualEmployment.IndividualEmploymentId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualEmployment.IndividualId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objIndividualEmployment.ApplicationId));
            lstParameter.Add(new MySqlParameter("ProviderId", objIndividualEmployment.ProviderId));
            lstParameter.Add(new MySqlParameter("EmploymentHistoryTypeId", objIndividualEmployment.EmploymentHistoryTypeId));
            lstParameter.Add(new MySqlParameter("EmploymentStartDate", objIndividualEmployment.EmploymentStartDate));
            lstParameter.Add(new MySqlParameter("EmploymentEndDate", objIndividualEmployment.EmploymentEndDate));
            lstParameter.Add(new MySqlParameter("EmploymentStatusId", objIndividualEmployment.EmploymentStatusId));
            lstParameter.Add(new MySqlParameter("EmploymentTypeId", objIndividualEmployment.EmploymentTypeId));
            lstParameter.Add(new MySqlParameter("PositionId", objIndividualEmployment.PositionId));
            lstParameter.Add(new MySqlParameter("IsWorkinginFieldofApplication", objIndividualEmployment.IsWorkinginFieldofApplication));
            lstParameter.Add(new MySqlParameter("EverWorkedinFieldofApplication", objIndividualEmployment.EverWorkedinFieldofApplication));
            lstParameter.Add(new MySqlParameter("ReferenceNumber", objIndividualEmployment.ReferenceNumber));
            lstParameter.Add(new MySqlParameter("Role", objIndividualEmployment.Role));
            lstParameter.Add(new MySqlParameter("IsActive", objIndividualEmployment.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualEmployment.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualEmployment.CreatedBy));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objIndividualEmployment.ModifiedBy));

            lstParameter.Add(new MySqlParameter("CreatedOn", objIndividualEmployment.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objIndividualEmployment.ModifiedOn));

            lstParameter.Add(new MySqlParameter("IndividualEmploymentGuid", objIndividualEmployment.IndividualEmploymentGuid));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "individualemployment_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<IndividualEmployment> Get_All_IndividualEmployment()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "INDIVIDUALEMPLOYMENT_GET_ALL");
            List<IndividualEmployment> lstEntity = new List<IndividualEmployment>();
            IndividualEmployment objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<IndividualEmployment> Get_IndividualEmployment_by_ApplicationId(int ApplicationId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_ApplicationId", ApplicationId));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualemployment_GET_BY_ApplicationId", lstParameter.ToArray());
            List<IndividualEmployment> lstEntity = new List<IndividualEmployment>();
            IndividualEmployment objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<IndividualEmployment> Get_IndividualEmployment_by_IndividualId(int IndividualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualemployment_GET_BY_IndividualId", lstParameter.ToArray());
            List<IndividualEmployment> lstEntity = new List<IndividualEmployment>();
            IndividualEmployment objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }


        public IndividualEmployment Get_IndividualEmployment_By_IndividualEmploymentId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualEmploymentId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualemployment_GET_BY_IndividualEmploymentId", lstParameter.ToArray());
            IndividualEmployment objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }
        public void IndividualEmployment_SoftDelete_by_ApplicationId(int _ApplicationId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("_ApplicationId", _ApplicationId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualemployment_SoftDelete_by_ApplicationId", lstParameter.ToArray());
            
        }
        private IndividualEmployment FetchEntity(DataRow dr)
        {
            IndividualEmployment objEntity = new IndividualEmployment();
            if (dr.Table.Columns.Contains("IndividualEmploymentId") && dr["IndividualEmploymentId"] != DBNull.Value)
            {
                objEntity.IndividualEmploymentId = Convert.ToInt32(dr["IndividualEmploymentId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationId") && dr["ApplicationId"] != DBNull.Value)
            {
                objEntity.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
            }
            if (dr.Table.Columns.Contains("ProviderId") && dr["ProviderId"] != DBNull.Value)
            {
                objEntity.ProviderId = Convert.ToInt32(dr["ProviderId"]);
            }
            if (dr.Table.Columns.Contains("EmploymentHistoryTypeId") && dr["EmploymentHistoryTypeId"] != DBNull.Value)
            {
                objEntity.EmploymentHistoryTypeId = Convert.ToInt32(dr["EmploymentHistoryTypeId"]);
            }
            if (dr.Table.Columns.Contains("EmploymentStartDate") && dr["EmploymentStartDate"] != DBNull.Value)
            {
                objEntity.EmploymentStartDate = Convert.ToDateTime(dr["EmploymentStartDate"]);
            }
            if (dr.Table.Columns.Contains("EmploymentEndDate") && dr["EmploymentEndDate"] != DBNull.Value)
            {
                objEntity.EmploymentEndDate = Convert.ToDateTime(dr["EmploymentEndDate"]);
            }
            if (dr.Table.Columns.Contains("EmploymentStatusId") && dr["EmploymentStatusId"] != DBNull.Value)
            {
                objEntity.EmploymentStatusId = Convert.ToInt32(dr["EmploymentStatusId"]);
            }
            if (dr.Table.Columns.Contains("EmploymentTypeId") && dr["EmploymentTypeId"] != DBNull.Value)
            {
                objEntity.EmploymentTypeId = Convert.ToInt32(dr["EmploymentTypeId"]);
            }
            if (dr.Table.Columns.Contains("PositionId") && dr["PositionId"] != DBNull.Value)
            {
                objEntity.PositionId = Convert.ToInt32(dr["PositionId"]);
            }
            if (dr.Table.Columns.Contains("IsWorkinginFieldofApplication") && dr["IsWorkinginFieldofApplication"] != DBNull.Value)
            {
                objEntity.IsWorkinginFieldofApplication = Convert.ToBoolean(dr["IsWorkinginFieldofApplication"]);
            }
            if (dr.Table.Columns.Contains("EverWorkedinFieldofApplication") && dr["EverWorkedinFieldofApplication"] != DBNull.Value)
            {
                objEntity.EverWorkedinFieldofApplication = Convert.ToBoolean(dr["EverWorkedinFieldofApplication"]);
            }
            if (dr.Table.Columns.Contains("ReferenceNumber") && dr["ReferenceNumber"] != DBNull.Value)
            {
                objEntity.ReferenceNumber = Convert.ToString(dr["ReferenceNumber"]);
            }
            if (dr.Table.Columns.Contains("Role") && dr["Role"] != DBNull.Value)
            {
                objEntity.Role = Convert.ToString(dr["Role"]);
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
            if (dr.Table.Columns.Contains("IndividualEmploymentGuid") && dr["IndividualEmploymentGuid"] != DBNull.Value)
            {
                objEntity.IndividualEmploymentGuid = dr["IndividualEmploymentGuid"].ToString();
            }

            if (dr.Table.Columns.Contains("EmployerName") && dr["EmployerName"] != DBNull.Value)
            {
                objEntity.EmployerName = dr["EmployerName"].ToString();
            }
            return objEntity;

        }
    }
}
