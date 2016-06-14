using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;

namespace LAPP.DAL
{
    public class ProvReqCourseOfStudyDAL
    {
        public List<ProvReqCourseOfStudy> Get_All_ProvReqCourseOfStudy()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ProvReqCourseOfStudy_Get_All");
            List<ProvReqCourseOfStudy> lstEntity = new List<ProvReqCourseOfStudy>();
            ProvReqCourseOfStudy objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }
        private ProvReqCourseOfStudy FetchEntity(DataRow dr)
        {
            ProvReqCourseOfStudy objEntity = new ProvReqCourseOfStudy();
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
            if (dr.Table.Columns.Contains("ReqCourseofStudyNameId") && dr["ReqCourseofStudyNameId"] != DBNull.Value)
            {
                objEntity.ReqCourseofStudyNameId = Convert.ToInt32(dr["ReqCourseofStudyNameId"]);
            }
            if (dr.Table.Columns.Contains("ReqCourseofStudyName") && dr["ReqCourseofStudyName"] != DBNull.Value)
            {
                objEntity.ReqCourseofStudyName = Convert.ToString(dr["ReqCourseofStudyName"]);
            }//
            if (dr.Table.Columns.Contains("MinimumReqCourseHours") && dr["MinimumReqCourseHours"] != DBNull.Value)
            {
                objEntity.MinimumReqCourseHours = Convert.ToInt32(dr["MinimumReqCourseHours"]);
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
            if (dr.Table.Columns.Contains("ProvReqCourseofStudyGuid") && dr["ProvReqCourseofStudyGuid"] != DBNull.Value)
            {
                objEntity.ProvReqCourseofStudyGuid = Convert.ToString(dr["ProvReqCourseofStudyGuid"]);
            }
            return objEntity;

        }
    }
}
