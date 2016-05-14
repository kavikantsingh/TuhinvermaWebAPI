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
    public class IndividualCEHoursDAL : BaseDAL
    {
        public int Save_IndividualCEHours(IndividualCEHours objIndividualCEHours)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualCEHoursId", objIndividualCEHours.IndividualCEHoursId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualCEHours.IndividualId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objIndividualCEHours.ApplicationId));
            lstParameter.Add(new MySqlParameter("CEHoursTypeId", objIndividualCEHours.CEHoursTypeId));
            lstParameter.Add(new MySqlParameter("CEHoursStartDate", objIndividualCEHours.CEHoursStartDate));
            lstParameter.Add(new MySqlParameter("CEHoursEndDate", objIndividualCEHours.CEHoursEndDate));
            lstParameter.Add(new MySqlParameter("CEHoursDueDate", objIndividualCEHours.CEHoursDueDate));
            lstParameter.Add(new MySqlParameter("CEHoursReportingYear", objIndividualCEHours.CEHoursReportingYear));
            lstParameter.Add(new MySqlParameter("CEHoursStatusId", objIndividualCEHours.CEHoursStatusId));
            lstParameter.Add(new MySqlParameter("CECarryInHours", objIndividualCEHours.CECarryInHours));
            lstParameter.Add(new MySqlParameter("CERequiredHours", objIndividualCEHours.CERequiredHours));
            lstParameter.Add(new MySqlParameter("CECurrentReportedHours", objIndividualCEHours.CECurrentReportedHours));
            lstParameter.Add(new MySqlParameter("CERolloverHours", objIndividualCEHours.CERolloverHours));
            lstParameter.Add(new MySqlParameter("ReferenceNumber", objIndividualCEHours.ReferenceNumber));
            lstParameter.Add(new MySqlParameter("IsActive", objIndividualCEHours.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualCEHours.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualCEHours.CreatedBy));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objIndividualCEHours.ModifiedBy));
            lstParameter.Add(new MySqlParameter("IndividualCEHoursGuid", objIndividualCEHours.IndividualCEHoursGuid));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "INDIVIDUALCEHOURS_SAVE", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<IndividualCEHours> Get_All_IndividualCEHours()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "INDIVIDUALCEHOURS_GET_ALL");
            List<IndividualCEHours> lstEntity = new List<IndividualCEHours>();
            IndividualCEHours objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public IndividualCEHours Get_IndividualCEHours_By_IndividualCEHoursId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualCEHoursId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualcehours_GET_BY_IndividualCEHoursId", lstParameter.ToArray());
            IndividualCEHours objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<IndividualCEHours> Get_IndividualCEHours_By_IndividualId(int IndividualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualcehours_GET_BY_IndividualId", lstParameter.ToArray());
            List<IndividualCEHours> lstEntity = new List<IndividualCEHours>();
            IndividualCEHours objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private IndividualCEHours FetchEntity(DataRow dr)
        {
            IndividualCEHours objEntity = new IndividualCEHours();
            if (dr.Table.Columns.Contains("IndividualCEHoursId") && dr["IndividualCEHoursId"] != DBNull.Value)
            {
                objEntity.IndividualCEHoursId = Convert.ToInt32(dr["IndividualCEHoursId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationId") && dr["ApplicationId"] != DBNull.Value)
            {
                objEntity.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
            }
            if (dr.Table.Columns.Contains("CEHoursTypeId") && dr["CEHoursTypeId"] != DBNull.Value)
            {
                objEntity.CEHoursTypeId = Convert.ToInt32(dr["CEHoursTypeId"]);
            }
            if (dr.Table.Columns.Contains("CEHoursStartDate") && dr["CEHoursStartDate"] != DBNull.Value)
            {
                objEntity.CEHoursStartDate = Convert.ToDateTime(dr["CEHoursStartDate"]);
            }
            if (dr.Table.Columns.Contains("CEHoursEndDate") && dr["CEHoursEndDate"] != DBNull.Value)
            {
                objEntity.CEHoursEndDate = Convert.ToDateTime(dr["CEHoursEndDate"]);
            }
            if (dr.Table.Columns.Contains("CEHoursDueDate") && dr["CEHoursDueDate"] != DBNull.Value)
            {
                objEntity.CEHoursDueDate = Convert.ToDateTime(dr["CEHoursDueDate"]);
            }
            if (dr.Table.Columns.Contains("CEHoursReportingYear") && dr["CEHoursReportingYear"] != DBNull.Value)
            {
                objEntity.CEHoursReportingYear = Convert.ToInt32(dr["CEHoursReportingYear"]);
            }
            if (dr.Table.Columns.Contains("CEHoursStatusId") && dr["CEHoursStatusId"] != DBNull.Value)
            {
                objEntity.CEHoursStatusId = Convert.ToInt32(dr["CEHoursStatusId"]);
            }
            if (dr.Table.Columns.Contains("CECarryInHours") && dr["CECarryInHours"] != DBNull.Value)
            {
                objEntity.CECarryInHours = Convert.ToDecimal(dr["CECarryInHours"]);
            }
            if (dr.Table.Columns.Contains("CERequiredHours") && dr["CERequiredHours"] != DBNull.Value)
            {
                objEntity.CERequiredHours = Convert.ToDecimal(dr["CERequiredHours"]);
            }
            if (dr.Table.Columns.Contains("CECurrentReportedHours") && dr["CECurrentReportedHours"] != DBNull.Value)
            {
                objEntity.CECurrentReportedHours = Convert.ToDecimal(dr["CECurrentReportedHours"]);
            }
            if (dr.Table.Columns.Contains("CERolloverHours") && dr["CERolloverHours"] != DBNull.Value)
            {
                objEntity.CERolloverHours = Convert.ToDecimal(dr["CERolloverHours"]);
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
            if (dr.Table.Columns.Contains("IndividualCEHoursGuid") && dr["IndividualCEHoursGuid"] != DBNull.Value)
            {
                objEntity.IndividualCEHoursGuid = (Guid)dr["IndividualCEHoursGuid"];
            }
            return objEntity;

        }
    }
}
