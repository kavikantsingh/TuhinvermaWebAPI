using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class ProvClinicHoursDAL
    {
        public int Save_ProvClinicHours(ProvClinicHours objProvClinicHours)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ProvClinicHoursId", objProvClinicHours.ProvClinicHoursId));
            lstParameter.Add(new MySqlParameter("ProviderId", objProvClinicHours.ProviderId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objProvClinicHours.ApplicationId));
            lstParameter.Add(new MySqlParameter("ClinicHours", objProvClinicHours.ClinicHours));
            lstParameter.Add(new MySqlParameter("ReferenceNumber", objProvClinicHours.ReferenceNumber));
            lstParameter.Add(new MySqlParameter("IsActive", objProvClinicHours.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objProvClinicHours.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objProvClinicHours.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objProvClinicHours.CreatedOn));

            lstParameter.Add(new MySqlParameter("ModifiedBy", objProvClinicHours.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objProvClinicHours.ModifiedOn));
            lstParameter.Add(new MySqlParameter("ProvClinicHoursGuid", objProvClinicHours.ProvClinicHoursGuid));

            //MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            //returnParam.Direction = ParameterDirection.ReturnValue;
            //lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "ProvClinicHours_Save", lstParameter.ToArray());
            return returnValue;
        }

        public ProvClinicHours Get_ProvClinicHours(int ProviderId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ProviderId", ProviderId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ProvClinicHours_Get_By_ProviderId", lstParameter.ToArray());
            //List<ProvReqCourseTitle> lstEntity = new List<ProvReqCourseTitle>();
            ProvClinicHours objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private ProvClinicHours FetchEntity(DataRow dr)
        {
            ProvClinicHours objEntity = new ProvClinicHours();
            if (dr.Table.Columns.Contains("ProvClinicHoursId") && dr["ProvClinicHoursId"] != DBNull.Value)
            {
                objEntity.ProvClinicHoursId = Convert.ToInt32(dr["ProvClinicHoursId"]);
            }
            if (dr.Table.Columns.Contains("ProviderId") && dr["ProviderId"] != DBNull.Value)
            {
                objEntity.ProviderId = Convert.ToInt32(dr["ProviderId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationId") && dr["ApplicationId"] != DBNull.Value)
            {
                objEntity.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
            }
            if (dr.Table.Columns.Contains("ClinicHours") && dr["ClinicHours"] != DBNull.Value)
            {
                objEntity.ClinicHours = Convert.ToInt32(dr["ClinicHours"]);
            }
            if (dr.Table.Columns.Contains("ReferenceNumber") && dr["ReferenceNumber"] != DBNull.Value)
            {
                objEntity.ReferenceNumber = Convert.ToString(dr["ReferenceNumber"]);
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
            if (dr.Table.Columns.Contains("ProvClinicHoursGuid") && dr["ProvClinicHoursGuid"] != DBNull.Value)
            {
                objEntity.ProvClinicHoursGuid = Convert.ToString(dr["ProvClinicHoursGuid"]);
            }
            return objEntity;

        }

    }
}
