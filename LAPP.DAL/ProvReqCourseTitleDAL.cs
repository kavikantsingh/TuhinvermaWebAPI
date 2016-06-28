using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;

namespace LAPP.DAL
{
    public class ProvReqCourseTitleDAL
    {
        public int Save_ProvReqCourseTitle(ProvReqCourseTitle objProvReqCourseTitle)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            //lstParameter.Add(new MySqlParameter("ProvReqCourseTitleId", objProvReqCourseTitle.ProvReqCourseTitleId));
            lstParameter.Add(new MySqlParameter("ProvReqCourseofStudyId", objProvReqCourseTitle.ProvReqCourseofStudyId));
            lstParameter.Add(new MySqlParameter("ProviderId", objProvReqCourseTitle.ProviderId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objProvReqCourseTitle.ApplicationId));
            lstParameter.Add(new MySqlParameter("CourseTitleName", objProvReqCourseTitle.CourseTitleName));
            lstParameter.Add(new MySqlParameter("CourseHours", objProvReqCourseTitle.CourseHours));
            lstParameter.Add(new MySqlParameter("ReferenceNumber", objProvReqCourseTitle.ReferenceNumber));
            lstParameter.Add(new MySqlParameter("IsActive", objProvReqCourseTitle.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objProvReqCourseTitle.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objProvReqCourseTitle.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objProvReqCourseTitle.CreatedOn));
            //lstParameter.Add(new MySqlParameter("ModifiedBy", objProvReqCourseTitle.ModifiedBy));
            //lstParameter.Add(new MySqlParameter("ModifiedOn", objProvReqCourseTitle.ModifiedOn));
            lstParameter.Add(new MySqlParameter("ProviderOtherProgramGuid", objProvReqCourseTitle.ProviderOtherProgramGuid));

            //MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            //returnParam.Direction = ParameterDirection.ReturnValue;
            //lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "ProvReqCourseTitle_Save", lstParameter.ToArray());
            return returnValue;
        }

        public List<ProvReqCourseTitle> Get_All_ProvReqCourseTitle_By_CourseOfStudyId(int CourseOfStudyId, int ProviderId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("CourseOfStudyId", CourseOfStudyId));
            lstParameter.Add(new MySqlParameter("ProviderId", ProviderId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ProvReqCourseTitle_Get_All", lstParameter.ToArray());
            List<ProvReqCourseTitle> lstEntity = new List<ProvReqCourseTitle>();
            ProvReqCourseTitle objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<ProvReqCourseTitle> Get_ProvReqCourseTitle_By_ProvReqCourseTitleId(int CourseTitleId, int ProviderId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("CourseTitleId", CourseTitleId));
            lstParameter.Add(new MySqlParameter("ProviderId", ProviderId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ProvReqCourseTitle_Get_By_CourseTitleId", lstParameter.ToArray());
            List<ProvReqCourseTitle> lstEntity = new List<ProvReqCourseTitle>();
            ProvReqCourseTitle objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public int Update_ProvReqCourseTitle(ProvReqCourseTitle objProvReqCourseTitle)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("CourseTitleId", objProvReqCourseTitle.ProvReqCourseTitleId));
            //lstParameter.Add(new MySqlParameter("ProvReqCourseofStudyId", objProvReqCourseTitle.ProvReqCourseofStudyId));
            lstParameter.Add(new MySqlParameter("ProviderId", objProvReqCourseTitle.ProviderId));
            //lstParameter.Add(new MySqlParameter("ApplicationId", objProvReqCourseTitle.ApplicationId));
            lstParameter.Add(new MySqlParameter("CourseTitleName", objProvReqCourseTitle.CourseTitleName));
            lstParameter.Add(new MySqlParameter("CourseHours", objProvReqCourseTitle.CourseHours));
            //lstParameter.Add(new MySqlParameter("ReferenceNumber", objProvReqCourseTitle.ReferenceNumber));
            //lstParameter.Add(new MySqlParameter("IsActive", objProvReqCourseTitle.IsActive));
            //lstParameter.Add(new MySqlParameter("IsDeleted", objProvReqCourseTitle.IsDeleted));
            //lstParameter.Add(new MySqlParameter("CreatedBy", objProvReqCourseTitle.CreatedBy));
            //lstParameter.Add(new MySqlParameter("CreatedOn", objProvReqCourseTitle.CreatedOn));
            //lstParameter.Add(new MySqlParameter("ModifiedBy", objProvReqCourseTitle.ModifiedBy));
            //lstParameter.Add(new MySqlParameter("ModifiedOn", objProvReqCourseTitle.ModifiedOn));
            lstParameter.Add(new MySqlParameter("ProviderOtherProgramGuid", objProvReqCourseTitle.ProviderOtherProgramGuid));

            //MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            //returnParam.Direction = ParameterDirection.ReturnValue;
            //lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "ProvReqCourseTitle_Update", lstParameter.ToArray());
            return returnValue;
        }

        public int Delete_ProvReqCourseTitle(ProvReqCourseTitle objProvReqCourseTitle)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("CourseTitleId", objProvReqCourseTitle.ProvReqCourseTitleId));
            //lstParameter.Add(new MySqlParameter("ProvReqCourseofStudyId", objProvReqCourseTitle.ProvReqCourseofStudyId));
            lstParameter.Add(new MySqlParameter("ProviderId", objProvReqCourseTitle.ProviderId));
            //lstParameter.Add(new MySqlParameter("ApplicationId", objProvReqCourseTitle.ApplicationId));
            //lstParameter.Add(new MySqlParameter("CourseTitleName", objProvReqCourseTitle.CourseTitleName));
            //lstParameter.Add(new MySqlParameter("CourseHours", objProvReqCourseTitle.CourseHours));
            //lstParameter.Add(new MySqlParameter("ReferenceNumber", objProvReqCourseTitle.ReferenceNumber));
            //lstParameter.Add(new MySqlParameter("IsActive", objProvReqCourseTitle.IsActive));
            //lstParameter.Add(new MySqlParameter("IsDeleted", objProvReqCourseTitle.IsDeleted));
            //lstParameter.Add(new MySqlParameter("CreatedBy", objProvReqCourseTitle.CreatedBy));
            //lstParameter.Add(new MySqlParameter("CreatedOn", objProvReqCourseTitle.CreatedOn));
            //lstParameter.Add(new MySqlParameter("ModifiedBy", objProvReqCourseTitle.ModifiedBy));
            //lstParameter.Add(new MySqlParameter("ModifiedOn", objProvReqCourseTitle.ModifiedOn));
            lstParameter.Add(new MySqlParameter("ProviderOtherProgramGuid", objProvReqCourseTitle.ProviderOtherProgramGuid));

            //MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            //returnParam.Direction = ParameterDirection.ReturnValue;
            //lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "ProvReqCourseTitle_Delete", lstParameter.ToArray());
            return returnValue;
        }

        private ProvReqCourseTitle FetchEntity(DataRow dr)
        {
            ProvReqCourseTitle objEntity = new ProvReqCourseTitle();
            if (dr.Table.Columns.Contains("ProvReqCourseTitleId") && dr["ProvReqCourseTitleId"] != DBNull.Value)
            {
                objEntity.ProvReqCourseTitleId = Convert.ToInt32(dr["ProvReqCourseTitleId"]);
            }
            if (dr.Table.Columns.Contains("ProvReqCourseofStudyId") && dr["ProvReqCourseofStudyId"] != DBNull.Value)
            {
                objEntity.ProvReqCourseofStudyId = Convert.ToInt32(dr["ProvReqCourseofStudyId"]);
            }
            if (dr.Table.Columns.Contains("ProviderId") && dr["ProviderId"] != DBNull.Value)
            {
                objEntity.ProviderId = Convert.ToInt32(dr["ProviderId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationId") && dr["ApplicationId"] != DBNull.Value)
            {
                objEntity.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
            }
            if (dr.Table.Columns.Contains("CourseTitleName") && dr["CourseTitleName"] != DBNull.Value)
            {
                objEntity.CourseTitleName = Convert.ToString(dr["CourseTitleName"]);
            }//
            if (dr.Table.Columns.Contains("CourseHours") && dr["CourseHours"] != DBNull.Value)
            {
                objEntity.CourseHours = Convert.ToInt32(dr["CourseHours"]);
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
            if (dr.Table.Columns.Contains("ProviderOtherProgramGuid") && dr["ProviderOtherProgramGuid"] != DBNull.Value)
            {
                objEntity.ProviderOtherProgramGuid = Convert.ToString(dr["ProviderOtherProgramGuid"]);
            }
            return objEntity;

        }
    }
}
