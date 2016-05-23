using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class IndividualRenewalDAL : BaseDAL
    {
        public List<RenewalGet> Get_All_Renewal()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Renewal_Get_All", lstParameter.ToArray());

            List<RenewalGet> lstEntity = new List<RenewalGet>();
            RenewalGet objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private RenewalGet FetchEntity(DataRow dr)
        {
            RenewalGet objEntity = new RenewalGet();

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

            if (dr.Table.Columns.Contains("IsActive") && dr["IsActive"] != DBNull.Value)
            {
                objEntity.IsActive = Convert.ToBoolean(dr["IsActive"]);
            }


            //join View only

            if (dr.Table.Columns.Contains("FirstName") && dr["FirstName"] != DBNull.Value)
            {
                objEntity.FirstName = Convert.ToString(dr["FirstName"]);
            }
            if (dr.Table.Columns.Contains("LastName") && dr["LastName"] != DBNull.Value)
            {
                objEntity.LastName = Convert.ToString(dr["LastName"]);
            }
            if (dr.Table.Columns.Contains("LicenseNumber") && dr["LicenseNumber"] != DBNull.Value)
            {
                objEntity.LicenseNumber = Convert.ToString(dr["LicenseNumber"]);
            }
            if (dr.Table.Columns.Contains("ApplicationStatus") && dr["ApplicationStatus"] != DBNull.Value)
            {
                objEntity.ApplicationStatus = Convert.ToString(dr["ApplicationStatus"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }

            return objEntity;

        }
    }
}
