using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class ApplicationStatusReasonDAL : BaseDAL
    {
        public int Save_ApplicationStatusReason(ApplicationStatusReason objApplicationStatusReason)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("ApplicationStatusReasonId", objApplicationStatusReason.ApplicationStatusReasonId));
            lstParameter.Add(new MySqlParameter("Name", objApplicationStatusReason.Name));
            lstParameter.Add(new MySqlParameter("SortOrder", objApplicationStatusReason.SortOrder));

            lstParameter.Add(new MySqlParameter("IsActive", objApplicationStatusReason.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objApplicationStatusReason.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objApplicationStatusReason.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objApplicationStatusReason.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objApplicationStatusReason.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objApplicationStatusReason.ModifiedOn));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "applicationstatusreason_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<ApplicationStatusReason> Get_All_ApplicationStatusReason()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ADDRESSTYPE_GET_ALL");
            List<ApplicationStatusReason> lstEntity = new List<ApplicationStatusReason>();
            ApplicationStatusReason objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public ApplicationStatusReason Get_ApplicationStatusReason_byApplicationStatusReasonId(int AddressId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParam = new List<MySqlParameter>();
            lstParam.Add(new MySqlParameter("G_ApplicationStatusReasonId", AddressId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "addresstype_Get_By_ApplicationStatusReasonId");

            ApplicationStatusReason objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);

            }
            return objEntity;
        }

        private ApplicationStatusReason FetchEntity(DataRow dr)
        {
            ApplicationStatusReason objEntity = new ApplicationStatusReason();


            if (dr.Table.Columns.Contains("ApplicationStatusReasonId") && dr["ApplicationStatusReasonId"] != DBNull.Value)
            {
                objEntity.ApplicationStatusReasonId = Convert.ToInt32(dr["ApplicationStatusReasonId"]);
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
    }
}
