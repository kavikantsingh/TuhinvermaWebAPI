using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class ApplicationStatusDAL : BaseDAL
    {
        public int Save_ApplicationStatus(ApplicationStatus objApplicationStatus)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("ApplicationStatusId", objApplicationStatus.ApplicationStatusId));
            lstParameter.Add(new MySqlParameter("Name", objApplicationStatus.Name));
            lstParameter.Add(new MySqlParameter("SortOrder", objApplicationStatus.SortOrder));

            lstParameter.Add(new MySqlParameter("IsActive", objApplicationStatus.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objApplicationStatus.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objApplicationStatus.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objApplicationStatus.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objApplicationStatus.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objApplicationStatus.ModifiedOn));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "applicationstatus_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<ApplicationStatus> Get_All_ApplicationStatus()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "applicationStatus_GetAll");
            List<ApplicationStatus> lstEntity = new List<ApplicationStatus>();
            ApplicationStatus objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public ApplicationStatus Get_ApplicationStatus_byApplicationStatusId(int ApplicationStatusID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParam = new List<MySqlParameter>();
            lstParam.Add(new MySqlParameter("G_ApplicationStatusId", ApplicationStatusID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "applicationStatus_GetByApplicationStatusId");

            ApplicationStatus objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);

            }
            return objEntity;
        }

        private ApplicationStatus FetchEntity(DataRow dr)
        {
            ApplicationStatus objEntity = new ApplicationStatus();


            if (dr.Table.Columns.Contains("ApplicationStatusId") && dr["ApplicationStatusId"] != DBNull.Value)
            {
                objEntity.ApplicationStatusId = Convert.ToInt32(dr["ApplicationStatusId"]);
            }
            if (dr.Table.Columns.Contains("Name") && dr["Name"] != DBNull.Value)
            {
                objEntity.Name = Convert.ToString(dr["Name"]);
            }
            if (dr.Table.Columns.Contains("SortOrder") && dr["SortOrder"] != DBNull.Value)
            {
                objEntity.SortOrder = Convert.ToInt32(dr["SortOrder"]);
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

        public ApplicationStatus Get_ApplicationStatus_byApplicationId(int ApplicationId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParam = new List<MySqlParameter>();
            lstParam.Add(new MySqlParameter("G_ApplicationId", ApplicationId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "applicationStatus_Get_By_ApplicationId", lstParam.ToArray());

            ApplicationStatus objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);

            }
            return objEntity;
        }

    }
}
