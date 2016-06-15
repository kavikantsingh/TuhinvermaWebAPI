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
    public class IndividualCECourseDAL : BaseDAL
    {
        public int Save_IndividualCECourse(IndividualCECourse objIndividualCECourse)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualCECourseId", objIndividualCECourse.IndividualCECourseId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualCECourse.IndividualId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objIndividualCECourse.ApplicationId));
            lstParameter.Add(new MySqlParameter("CECourseTypeId", objIndividualCECourse.CECourseTypeId));
            lstParameter.Add(new MySqlParameter("CECourseActivityTypeId", objIndividualCECourse.CECourseActivityTypeId));
            lstParameter.Add(new MySqlParameter("CECourseStartDate", objIndividualCECourse.CECourseStartDate));
            lstParameter.Add(new MySqlParameter("CECourseEndDate", objIndividualCECourse.CECourseEndDate));
            lstParameter.Add(new MySqlParameter("CECourseDueDate", objIndividualCECourse.CECourseDueDate));
            lstParameter.Add(new MySqlParameter("CECourseDate", objIndividualCECourse.CECourseDate));
            lstParameter.Add(new MySqlParameter("CECourseHours", objIndividualCECourse.CECourseHours));
            lstParameter.Add(new MySqlParameter("CECourseUnits", objIndividualCECourse.CECourseUnits));
            lstParameter.Add(new MySqlParameter("ProgramSponsor", objIndividualCECourse.ProgramSponsor));
            lstParameter.Add(new MySqlParameter("CourseNameTitle", objIndividualCECourse.CourseNameTitle));
            lstParameter.Add(new MySqlParameter("CourseSponsor", objIndividualCECourse.CourseSponsor));
            lstParameter.Add(new MySqlParameter("CECourseReportingYear", objIndividualCECourse.CECourseReportingYear));
            lstParameter.Add(new MySqlParameter("CECourseStatusId", objIndividualCECourse.CECourseStatusId));
            lstParameter.Add(new MySqlParameter("InstructorBiography", objIndividualCECourse.InstructorBiography));
            lstParameter.Add(new MySqlParameter("ActivityDesc", objIndividualCECourse.ActivityDesc));
            lstParameter.Add(new MySqlParameter("ReferenceNumber", objIndividualCECourse.ReferenceNumber));
            lstParameter.Add(new MySqlParameter("IsActive", objIndividualCECourse.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualCECourse.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualCECourse.CreatedBy));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objIndividualCECourse.ModifiedBy));

            lstParameter.Add(new MySqlParameter("CreatedOn", objIndividualCECourse.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objIndividualCECourse.ModifiedOn));

            lstParameter.Add(new MySqlParameter("IndividualCECourseGuid", objIndividualCECourse.IndividualCECourseGuid));
            lstParameter.Add(new MySqlParameter("IndividualLicenseId", objIndividualCECourse.IndividualLicenseId));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "INDIVIDUALCECOURSE_SAVE", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<IndividualCECourse> Get_All_IndividualCECourse()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "INDIVIDUALCECOURSE_GET_ALL");
            List<IndividualCECourse> lstEntity = new List<IndividualCECourse>();
            IndividualCECourse objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public IndividualCECourse Get_IndividualCECourse_By_IndividualCECourseId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualCECourseId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualcecourse_GET_BY_IndividualCECourseId", lstParameter.ToArray());
            IndividualCECourse objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public void IndividualCECourse_SoftDelete_by_ApplicationId(int _ApplicationId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("_ApplicationId", _ApplicationId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualCECourse_SoftDelete_by_ApplicationId", lstParameter.ToArray());

        }

        public List<IndividualCECourse> Get_IndividualCECourse_By_ApplicationId(int ApplicationId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_ApplicationId", ApplicationId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualcecourse_GET_BY_ApplicationId", lstParameter.ToArray());
            List<IndividualCECourse> lstEntity = new List<IndividualCECourse>();
            IndividualCECourse objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<IndividualCECourse> Get_IndividualCECourse_By_IndividualLicenseId(int IndividualLicenseId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualLicenseId", IndividualLicenseId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualcecourse_GET_BY_IndividualLicenseId", lstParameter.ToArray());
            List<IndividualCECourse> lstEntity = new List<IndividualCECourse>();
            IndividualCECourse objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<IndividualCECourse> Get_IndividualCECourse_By_IndividualId(int IndividualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualcecourse_GET_BY_IndividualId", lstParameter.ToArray());
            List<IndividualCECourse> lstEntity = new List<IndividualCECourse>();
            IndividualCECourse objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }



        private IndividualCECourse FetchEntity(DataRow dr)
        {
            IndividualCECourse objEntity = new IndividualCECourse();
            if (dr.Table.Columns.Contains("IndividualCECourseId") && dr["IndividualCECourseId"] != DBNull.Value)
            {
                objEntity.IndividualCECourseId = Convert.ToInt32(dr["IndividualCECourseId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationId") && dr["ApplicationId"] != DBNull.Value)
            {
                objEntity.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
            }
            if (dr.Table.Columns.Contains("CECourseTypeId") && dr["CECourseTypeId"] != DBNull.Value)
            {
                objEntity.CECourseTypeId = Convert.ToInt32(dr["CECourseTypeId"]);
            }
            if (dr.Table.Columns.Contains("CECourseActivityTypeId") && dr["CECourseActivityTypeId"] != DBNull.Value)
            {
                objEntity.CECourseActivityTypeId = Convert.ToInt32(dr["CECourseActivityTypeId"]);
            }
            if (dr.Table.Columns.Contains("CECourseStartDate") && dr["CECourseStartDate"] != DBNull.Value)
            {
                objEntity.CECourseStartDate = Convert.ToDateTime(dr["CECourseStartDate"]);
            }
            if (dr.Table.Columns.Contains("CECourseEndDate") && dr["CECourseEndDate"] != DBNull.Value)
            {
                objEntity.CECourseEndDate = Convert.ToDateTime(dr["CECourseEndDate"]);
            }
            if (dr.Table.Columns.Contains("CECourseDueDate") && dr["CECourseDueDate"] != DBNull.Value)
            {
                objEntity.CECourseDueDate = Convert.ToDateTime(dr["CECourseDueDate"]);
            }
            if (dr.Table.Columns.Contains("CECourseDate") && dr["CECourseDate"] != DBNull.Value)
            {
                objEntity.CECourseDate = Convert.ToDateTime(dr["CECourseDate"]);
            }
            if (dr.Table.Columns.Contains("CECourseHours") && dr["CECourseHours"] != DBNull.Value)
            {
                objEntity.CECourseHours = Convert.ToDecimal(dr["CECourseHours"]);
            }
            if (dr.Table.Columns.Contains("CECourseUnits") && dr["CECourseUnits"] != DBNull.Value)
            {
                objEntity.CECourseUnits = Convert.ToDecimal(dr["CECourseUnits"]);
            }
            if (dr.Table.Columns.Contains("ProgramSponsor") && dr["ProgramSponsor"] != DBNull.Value)
            {
                objEntity.ProgramSponsor = Convert.ToString(dr["ProgramSponsor"]);
            }
            if (dr.Table.Columns.Contains("CourseNameTitle") && dr["CourseNameTitle"] != DBNull.Value)
            {
                objEntity.CourseNameTitle = Convert.ToString(dr["CourseNameTitle"]);
            }
            if (dr.Table.Columns.Contains("CourseSponsor") && dr["CourseSponsor"] != DBNull.Value)
            {
                objEntity.CourseSponsor = Convert.ToString(dr["CourseSponsor"]);
            }
            if (dr.Table.Columns.Contains("CECourseReportingYear") && dr["CECourseReportingYear"] != DBNull.Value)
            {
                objEntity.CECourseReportingYear = Convert.ToInt32(dr["CECourseReportingYear"]);
            }
            if (dr.Table.Columns.Contains("CECourseStatusId") && dr["CECourseStatusId"] != DBNull.Value)
            {
                objEntity.CECourseStatusId = Convert.ToInt32(dr["CECourseStatusId"]);
            }
            if (dr.Table.Columns.Contains("InstructorBiography") && dr["InstructorBiography"] != DBNull.Value)
            {
                objEntity.InstructorBiography = Convert.ToString(dr["InstructorBiography"]);
            }
            if (dr.Table.Columns.Contains("ActivityDesc") && dr["ActivityDesc"] != DBNull.Value)
            {
                objEntity.ActivityDesc = Convert.ToString(dr["ActivityDesc"]);
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
            if (dr.Table.Columns.Contains("IndividualCECourseGuid") && dr["IndividualCECourseGuid"] != DBNull.Value)
            {
                objEntity.IndividualCECourseGuid = dr["IndividualCECourseGuid"].ToString();
            }

            if (dr.Table.Columns.Contains("IndividualLicenseId") && dr["IndividualLicenseId"] != DBNull.Value)
            {
                objEntity.IndividualLicenseId = Convert.ToInt32(dr["IndividualLicenseId"]);
            }
            return objEntity;

        }
    }
}
