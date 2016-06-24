using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class ApplicationDAL : BaseDAL
    {
        public int Save_Application(Application objApplication)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ApplicationId", objApplication.ApplicationId));
            lstParameter.Add(new MySqlParameter("ApplicationTypeId", objApplication.ApplicationTypeId));
            lstParameter.Add(new MySqlParameter("ApplicationStatusId", objApplication.ApplicationStatusId));
            lstParameter.Add(new MySqlParameter("ApplicationStatusReasonId", objApplication.ApplicationStatusReasonId));
            lstParameter.Add(new MySqlParameter("ApplicationNumber", objApplication.ApplicationNumber));
            lstParameter.Add(new MySqlParameter("ApplicationSubmitMode", objApplication.ApplicationSubmitMode));
            lstParameter.Add(new MySqlParameter("StartedDate", objApplication.StartedDate));
            lstParameter.Add(new MySqlParameter("SubmittedDate", objApplication.SubmittedDate));
            lstParameter.Add(new MySqlParameter("ApplicationStatusDate", objApplication.ApplicationStatusDate));
            lstParameter.Add(new MySqlParameter("PaymentDeadlineDate", objApplication.PaymentDeadlineDate));
            lstParameter.Add(new MySqlParameter("PaymentDate", objApplication.PaymentDate));
            lstParameter.Add(new MySqlParameter("ConfirmationNumber", objApplication.ConfirmationNumber));
            lstParameter.Add(new MySqlParameter("ReferenceNumber", objApplication.ReferenceNumber));
            lstParameter.Add(new MySqlParameter("IsFingerprintingNotRequired", objApplication.IsFingerprintingNotRequired));
            lstParameter.Add(new MySqlParameter("IsPaymentRequired", objApplication.IsPaymentRequired));
            lstParameter.Add(new MySqlParameter("CanProvisionallyHire", objApplication.CanProvisionallyHire));
            lstParameter.Add(new MySqlParameter("GoPaperless", objApplication.GoPaperless));
            lstParameter.Add(new MySqlParameter("LicenseRequirementId", objApplication.LicenseRequirementId));
            lstParameter.Add(new MySqlParameter("WithdrawalReasonId", objApplication.WithdrawalReasonId));
            lstParameter.Add(new MySqlParameter("LicenseTypeId", objApplication.LicenseTypeId));
            lstParameter.Add(new MySqlParameter("IsArchive", objApplication.IsArchive));

            lstParameter.Add(new MySqlParameter("IsActive", objApplication.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objApplication.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objApplication.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objApplication.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objApplication.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objApplication.ModifiedOn));

            lstParameter.Add(new MySqlParameter("ApplicationGuid", objApplication.ApplicationGuid));



            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "application_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        //public int Update_Application(Application objApplication)
        //{
        //    DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
        //    lstParameter.Add(new MySqlParameter("U_ApplicationId", objApplication.ApplicationId));
        //    lstParameter.Add(new MySqlParameter("U_ApplicationTypeId", objApplication.ApplicationTypeId));
        //    lstParameter.Add(new MySqlParameter("U_ApplicationStatusId", objApplication.ApplicationStatusId));
        //    lstParameter.Add(new MySqlParameter("U_ApplicationStatusReasonId", objApplication.ApplicationStatusReasonId));
        //    lstParameter.Add(new MySqlParameter("U_ApplicationNumber", objApplication.ApplicationNumber));
        //    lstParameter.Add(new MySqlParameter("U_ApplicationSubmitMode", objApplication.ApplicationSubmitMode));
        //    lstParameter.Add(new MySqlParameter("U_Zip", objApplication.StartedDate));
        //    lstParameter.Add(new MySqlParameter("U_CountyId", objApplication.SubmittedDate));
        //    lstParameter.Add(new MySqlParameter("U_CountryId", objApplication.ApplicationStatusDate));
        //    lstParameter.Add(new MySqlParameter("U_DateValidated", objApplication.PaymentDeadlineDate));
        //    lstParameter.Add(new MySqlParameter("U_UseUserApplication", objApplication.PaymentDate));
        //    lstParameter.Add(new MySqlParameter("U_PaymentDate", objApplication.PaymentDate));
        //    lstParameter.Add(new MySqlParameter("U_IsActive", objApplication.IsActive));
        //    lstParameter.Add(new MySqlParameter("U_IsDeleted", objApplication.IsDeleted));
        //    lstParameter.Add(new MySqlParameter("U_CreatedBy", objApplication.CreatedBy));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedBy", objApplication.ModifiedBy));
        //    lstParameter.Add(new MySqlParameter("U_ApplicationGuid", objApplication.ApplicationGuid));
        //    lstParameter.Add(new MySqlParameter("U_ConfirmationNumber", objApplication.ConfirmationNumber));
        //    int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "Application_Update", lstParameter.ToArray());
        //    return returnValue;
        //}


        public List<Application> Get_All_Application()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Application_Get_All");
            List<Application> lstEntity = new List<Application>();
            Application objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<Application> Get_All_Renewa()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Renewal_Get_All");
            List<Application> lstEntity = new List<Application>();
            Application objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<Application> Get_Application_By_IndividualId(int IndividualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "application_Get_By_IndividualId", lstParameter.ToArray());
            List<Application> lstEntity = new List<Application>();
            Application objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public Application Get_Application_By_ApplicationId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_ApplicationId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Application_GET_BY_ApplicationId", lstParameter.ToArray());
            Application objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private Application FetchEntity(DataRow dr)
        {
            Application objEntity = new Application();
            if (dr.Table.Columns.Contains("ApplicationId") && dr["ApplicationId"] != DBNull.Value)
            {
                objEntity.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationTypeId") && dr["ApplicationTypeId"] != DBNull.Value)
            {
                objEntity.ApplicationTypeId = Convert.ToInt32(dr["ApplicationTypeId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationStatusId") && dr["ApplicationStatusId"] != DBNull.Value)
            {
                objEntity.ApplicationStatusId = Convert.ToInt32(dr["ApplicationStatusId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationStatusReasonId") && dr["ApplicationStatusReasonId"] != DBNull.Value)
            {
                objEntity.ApplicationStatusReasonId = Convert.ToInt32(dr["ApplicationStatusReasonId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationNumber") && dr["ApplicationNumber"] != DBNull.Value)
            {
                objEntity.ApplicationNumber = Convert.ToString(dr["ApplicationNumber"]);
            }
            if (dr.Table.Columns.Contains("ApplicationSubmitMode") && dr["ApplicationSubmitMode"] != DBNull.Value)
            {
                objEntity.ApplicationSubmitMode = Convert.ToString(dr["ApplicationSubmitMode"]);
            }
            if (dr.Table.Columns.Contains("StartedDate") && dr["StartedDate"] != DBNull.Value)
            {
                objEntity.StartedDate = Convert.ToDateTime(dr["StartedDate"]);
            }
            if (dr.Table.Columns.Contains("SubmittedDate") && dr["SubmittedDate"] != DBNull.Value)
            {
                objEntity.SubmittedDate = Convert.ToDateTime(dr["SubmittedDate"]);
            }
            if (dr.Table.Columns.Contains("ApplicationStatusDate") && dr["ApplicationStatusDate"] != DBNull.Value)
            {
                objEntity.ApplicationStatusDate = Convert.ToDateTime(dr["ApplicationStatusDate"]);
            }
            if (dr.Table.Columns.Contains("PaymentDeadlineDate") && dr["PaymentDeadlineDate"] != DBNull.Value)
            {
                objEntity.PaymentDeadlineDate = Convert.ToDateTime(dr["PaymentDeadlineDate"]);
            }
            if (dr.Table.Columns.Contains("PaymentDate") && dr["PaymentDate"] != DBNull.Value)
            {
                objEntity.PaymentDate = Convert.ToDateTime(dr["PaymentDate"]);
            }
            if (dr.Table.Columns.Contains("ConfirmationNumber") && dr["ConfirmationNumber"] != DBNull.Value)
            {
                objEntity.ConfirmationNumber = Convert.ToString(dr["ConfirmationNumber"]);
            }
            if (dr.Table.Columns.Contains("ReferenceNumber") && dr["ReferenceNumber"] != DBNull.Value)
            {
                objEntity.ReferenceNumber = Convert.ToString(dr["ReferenceNumber"]);
            }
            if (dr.Table.Columns.Contains("IsFingerprintingNotRequired") && dr["IsFingerprintingNotRequired"] != DBNull.Value)
            {
                objEntity.IsFingerprintingNotRequired = Convert.ToBoolean(dr["IsFingerprintingNotRequired"]);
            }
            if (dr.Table.Columns.Contains("IsPaymentRequired") && dr["IsPaymentRequired"] != DBNull.Value)
            {
                objEntity.IsPaymentRequired = Convert.ToBoolean(dr["IsPaymentRequired"]);
            }
            if (dr.Table.Columns.Contains("CanProvisionallyHire") && dr["CanProvisionallyHire"] != DBNull.Value)
            {
                objEntity.CanProvisionallyHire = Convert.ToBoolean(dr["CanProvisionallyHire"]);
            }
            if (dr.Table.Columns.Contains("GoPaperless") && dr["GoPaperless"] != DBNull.Value)
            {
                objEntity.GoPaperless = Convert.ToBoolean(dr["GoPaperless"]);
            }
            if (dr.Table.Columns.Contains("LicenseRequirementId") && dr["LicenseRequirementId"] != DBNull.Value)
            {
                objEntity.LicenseRequirementId = Convert.ToInt32(dr["LicenseRequirementId"]);
            }
            if (dr.Table.Columns.Contains("WithdrawalReasonId") && dr["WithdrawalReasonId"] != DBNull.Value)
            {
                objEntity.WithdrawalReasonId = Convert.ToInt32(dr["WithdrawalReasonId"]);
            }


            if (dr.Table.Columns.Contains("IsArchive") && dr["IsArchive"] != DBNull.Value)
            {
                objEntity.IsArchive = Convert.ToBoolean(dr["IsArchive"]);
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
            if (dr.Table.Columns.Contains("ApplicationGuid") && dr["ApplicationGuid"] != DBNull.Value)
            {
                objEntity.ApplicationGuid = Convert.ToString(dr["ApplicationGuid"]);
            }


            // Used for view


            if (dr.Table.Columns.Contains("IsPaid") && dr["IsPaid"] != DBNull.Value)
            {
                objEntity.IsPaid = Convert.ToBoolean(dr["IsPaid"]);
            }

            if (dr.Table.Columns.Contains("ApplicationType") && dr["ApplicationType"] != DBNull.Value)
            {
                objEntity.ApplicationType = Convert.ToString(dr["ApplicationType"]);
            }
            if (dr.Table.Columns.Contains("ApplicationStatus") && dr["ApplicationStatus"] != DBNull.Value)
            {
                objEntity.ApplicationStatus = Convert.ToString(dr["ApplicationStatus"]);
            }

            if (dr.Table.Columns.Contains("LicenseTypeId") && dr["LicenseTypeId"] != DBNull.Value)
            {
                objEntity.LicenseTypeId = Convert.ToInt32(dr["LicenseTypeId"]);
            }

            return objEntity;

        }

        // Application Count for Dashboard

        public ApplicationCount Get_DashboardApplicationCountRenewal()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "application_Renewal_count");
            ApplicationCount objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntityApplicationCount(dr);
            }
            return objEntity;
        }

        public ApplicationCount Get_DashboardApplicationCountApplications()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "application_Application_count");
            ApplicationCount objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntityApplicationCount(dr);
            }
            return objEntity;
        }

        private ApplicationCount FetchEntityApplicationCount(DataRow dr)
        {
            ApplicationCount objEntity = new ApplicationCount();

            if (dr.Table.Columns.Contains("ApplicationsSubmittedCount") && dr["ApplicationsSubmittedCount"] != DBNull.Value)
            {
                objEntity.ApplicationsSubmittedCount = Convert.ToInt32(dr["ApplicationsSubmittedCount"]);
            }
            if (dr.Table.Columns.Contains("ApplicationsApproved") && dr["ApplicationsApproved"] != DBNull.Value)
            {
                objEntity.ApplicationsApproved = Convert.ToInt32(dr["ApplicationsApproved"]);
            }
            if (dr.Table.Columns.Contains("ApplicationsUnderReview") && dr["ApplicationsUnderReview"] != DBNull.Value)
            {
                objEntity.ApplicationsUnderReview = Convert.ToInt32(dr["ApplicationsUnderReview"]);
            }
            if (dr.Table.Columns.Contains("ApplicationsDenied") && dr["ApplicationsDenied"] != DBNull.Value)
            {
                objEntity.ApplicationsDenied = Convert.ToInt32(dr["ApplicationsDenied"]);
            }

            if (dr.Table.Columns.Contains("ApplicationType") && dr["ApplicationType"] != DBNull.Value)
            {
                objEntity.ApplicationType = Convert.ToString(dr["ApplicationType"]);
            }


            return objEntity;

        }
    }
}
