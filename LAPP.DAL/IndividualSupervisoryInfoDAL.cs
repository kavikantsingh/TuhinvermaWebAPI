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
    public class IndividualSupervisoryInfoDAL : BaseDAL
    {
        public int Save_IndividualSupervisoryInfo(IndividualSupervisoryInfo objIndividualSupervisoryInfo)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualSupervisoryInfoId", objIndividualSupervisoryInfo.IndividualSupervisoryInfoId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualSupervisoryInfo.IndividualId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objIndividualSupervisoryInfo.ApplicationId));
            lstParameter.Add(new MySqlParameter("Doyousupervise", objIndividualSupervisoryInfo.Doyousupervise));
            lstParameter.Add(new MySqlParameter("SupervisedIndividualId", objIndividualSupervisoryInfo.SupervisedIndividualId));
            lstParameter.Add(new MySqlParameter("SupervisedWorkAddressId", objIndividualSupervisoryInfo.SupervisedWorkAddressId));
            lstParameter.Add(new MySqlParameter("SupervisedMailingAddressId", objIndividualSupervisoryInfo.SupervisedMailingAddressId));
            lstParameter.Add(new MySqlParameter("SupervisedWorkPhoneContactId", objIndividualSupervisoryInfo.SupervisedWorkPhoneContactId));
            lstParameter.Add(new MySqlParameter("SupervisedWorkEmailContactId", objIndividualSupervisoryInfo.SupervisedWorkEmailContactId));
            lstParameter.Add(new MySqlParameter("SupervisedWorkFaxContactId", objIndividualSupervisoryInfo.SupervisedWorkFaxContactId));
            lstParameter.Add(new MySqlParameter("SupervisedLicenseNumber", objIndividualSupervisoryInfo.SupervisedLicenseNumber));
            lstParameter.Add(new MySqlParameter("SupervisedStateLicensed", objIndividualSupervisoryInfo.SupervisedStateLicensed));
            lstParameter.Add(new MySqlParameter("SupervisedLicenseExpirationDate", objIndividualSupervisoryInfo.SupervisedLicenseExpirationDate));
            lstParameter.Add(new MySqlParameter("Areyousupervised", objIndividualSupervisoryInfo.Areyousupervised));
            lstParameter.Add(new MySqlParameter("SupervisorIndividualId", objIndividualSupervisoryInfo.SupervisorIndividualId));
            lstParameter.Add(new MySqlParameter("SupervisorWorkAddressId", objIndividualSupervisoryInfo.SupervisorWorkAddressId));
            lstParameter.Add(new MySqlParameter("SupervisorMailingAddressId", objIndividualSupervisoryInfo.SupervisorMailingAddressId));
            lstParameter.Add(new MySqlParameter("SupervisorWorkPhoneContactId", objIndividualSupervisoryInfo.SupervisorWorkPhoneContactId));
            lstParameter.Add(new MySqlParameter("SupervisorWorkEmailContactId", objIndividualSupervisoryInfo.SupervisorWorkEmailContactId));
            lstParameter.Add(new MySqlParameter("SupervisorWorkFaxContactId", objIndividualSupervisoryInfo.SupervisorWorkFaxContactId));
            lstParameter.Add(new MySqlParameter("SupervisorLicenseNumber", objIndividualSupervisoryInfo.SupervisorLicenseNumber));
            lstParameter.Add(new MySqlParameter("SupervisorStateLicensed", objIndividualSupervisoryInfo.SupervisorStateLicensed));
            lstParameter.Add(new MySqlParameter("SupervisorLicenseExpirationDate", objIndividualSupervisoryInfo.SupervisorLicenseExpirationDate));
            lstParameter.Add(new MySqlParameter("IndividualNameId", objIndividualSupervisoryInfo.IndividualNameId));

            lstParameter.Add(new MySqlParameter("ReferenceNumber", objIndividualSupervisoryInfo.ReferenceNumber));
            lstParameter.Add(new MySqlParameter("IsActive", objIndividualSupervisoryInfo.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualSupervisoryInfo.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualSupervisoryInfo.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objIndividualSupervisoryInfo.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objIndividualSupervisoryInfo.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objIndividualSupervisoryInfo.ModifiedOn));

            lstParameter.Add(new MySqlParameter("IndividualSupervisoryInfoGuid", objIndividualSupervisoryInfo.IndividualSupervisoryInfoGuid));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "individualsupervisoryinfo_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public IndividualSupervisoryInfo Get_IndividualSupervisoryInfo_By_ApplicationId(int applicationId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("_ApplicationId", applicationId));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualsupervisoryinfo_Get_By_ApplicationId", lstParameter.ToArray());
            IndividualSupervisoryInfo objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<IndividualSupervisoryInfo> Get_All_IndividualSupervisoryInfo()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "INDIVIDUALSUPERVISORYINFO_GET_ALL");
            List<IndividualSupervisoryInfo> lstEntity = new List<IndividualSupervisoryInfo>();
            IndividualSupervisoryInfo objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<IndividualSupervisoryInfo> Get_IndividualSupervisoryInfo_By_IndividualId(int IndividualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualsupervisoryinfo_Get_By_IndividualId", lstParameter.ToArray());
            List<IndividualSupervisoryInfo> lstEntity = new List<IndividualSupervisoryInfo>();
            IndividualSupervisoryInfo objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public IndividualSupervisoryInfo Get_IndividualSupervisoryInfo_By_IndividualSupervisoryInfoId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualSupervisoryInfoId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualsupervisoryinfo_Get_By_IndividualSupervisoryInfoId", lstParameter.ToArray());
            IndividualSupervisoryInfo objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private IndividualSupervisoryInfo FetchEntity(DataRow dr)
        {
            IndividualSupervisoryInfo objEntity = new IndividualSupervisoryInfo();
            if (dr.Table.Columns.Contains("IndividualSupervisoryInfoId") && dr["IndividualSupervisoryInfoId"] != DBNull.Value)
            {
                objEntity.IndividualSupervisoryInfoId = Convert.ToInt32(dr["IndividualSupervisoryInfoId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationId") && dr["ApplicationId"] != DBNull.Value)
            {
                objEntity.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
            }
            if (dr.Table.Columns.Contains("Doyousupervise") && dr["Doyousupervise"] != DBNull.Value)
            {
                objEntity.Doyousupervise = Convert.ToBoolean(dr["Doyousupervise"]);
            }
            if (dr.Table.Columns.Contains("SupervisedIndividualId") && dr["SupervisedIndividualId"] != DBNull.Value)
            {
                objEntity.SupervisedIndividualId = Convert.ToInt32(dr["SupervisedIndividualId"]);
            }
            if (dr.Table.Columns.Contains("SupervisedWorkAddressId") && dr["SupervisedWorkAddressId"] != DBNull.Value)
            {
                objEntity.SupervisedWorkAddressId = Convert.ToInt32(dr["SupervisedWorkAddressId"]);
            }
            if (dr.Table.Columns.Contains("SupervisedMailingAddressId") && dr["SupervisedMailingAddressId"] != DBNull.Value)
            {
                objEntity.SupervisedMailingAddressId = Convert.ToInt32(dr["SupervisedMailingAddressId"]);
            }
            if (dr.Table.Columns.Contains("SupervisedWorkPhoneContactId") && dr["SupervisedWorkPhoneContactId"] != DBNull.Value)
            {
                objEntity.SupervisedWorkPhoneContactId = Convert.ToInt32(dr["SupervisedWorkPhoneContactId"]);
            }
            if (dr.Table.Columns.Contains("SupervisedWorkEmailContactId") && dr["SupervisedWorkEmailContactId"] != DBNull.Value)
            {
                objEntity.SupervisedWorkEmailContactId = Convert.ToInt32(dr["SupervisedWorkEmailContactId"]);
            }
            if (dr.Table.Columns.Contains("SupervisedWorkFaxContactId") && dr["SupervisedWorkFaxContactId"] != DBNull.Value)
            {
                objEntity.SupervisedWorkFaxContactId = Convert.ToInt32(dr["SupervisedWorkFaxContactId"]);
            }
            if (dr.Table.Columns.Contains("SupervisedLicenseNumber") && dr["SupervisedLicenseNumber"] != DBNull.Value)
            {
                objEntity.SupervisedLicenseNumber = Convert.ToString(dr["SupervisedLicenseNumber"]);
            }
            if (dr.Table.Columns.Contains("SupervisedStateLicensed") && dr["SupervisedStateLicensed"] != DBNull.Value)
            {
                objEntity.SupervisedStateLicensed = Convert.ToString(dr["SupervisedStateLicensed"]);
            }
            if (dr.Table.Columns.Contains("SupervisedLicenseExpirationDate") && dr["SupervisedLicenseExpirationDate"] != DBNull.Value)
            {
                objEntity.SupervisedLicenseExpirationDate = Convert.ToDateTime(dr["SupervisedLicenseExpirationDate"]);
            }
            if (dr.Table.Columns.Contains("Areyousupervised") && dr["Areyousupervised"] != DBNull.Value)
            {
                objEntity.Areyousupervised = Convert.ToBoolean(dr["Areyousupervised"]);
            }
            if (dr.Table.Columns.Contains("SupervisorIndividualId") && dr["SupervisorIndividualId"] != DBNull.Value)
            {
                objEntity.SupervisorIndividualId = Convert.ToInt32(dr["SupervisorIndividualId"]);
            }
            if (dr.Table.Columns.Contains("SupervisorWorkAddressId") && dr["SupervisorWorkAddressId"] != DBNull.Value)
            {
                objEntity.SupervisorWorkAddressId = Convert.ToInt32(dr["SupervisorWorkAddressId"]);
            }
            if (dr.Table.Columns.Contains("SupervisorMailingAddressId") && dr["SupervisorMailingAddressId"] != DBNull.Value)
            {
                objEntity.SupervisorMailingAddressId = Convert.ToInt32(dr["SupervisorMailingAddressId"]);
            }
            if (dr.Table.Columns.Contains("SupervisorWorkPhoneContactId") && dr["SupervisorWorkPhoneContactId"] != DBNull.Value)
            {
                objEntity.SupervisorWorkPhoneContactId = Convert.ToInt32(dr["SupervisorWorkPhoneContactId"]);
            }
            if (dr.Table.Columns.Contains("SupervisorWorkEmailContactId") && dr["SupervisorWorkEmailContactId"] != DBNull.Value)
            {
                objEntity.SupervisorWorkEmailContactId = Convert.ToInt32(dr["SupervisorWorkEmailContactId"]);
            }
            if (dr.Table.Columns.Contains("SupervisorWorkFaxContactId") && dr["SupervisorWorkFaxContactId"] != DBNull.Value)
            {
                objEntity.SupervisorWorkFaxContactId = Convert.ToInt32(dr["SupervisorWorkFaxContactId"]);
            }
            if (dr.Table.Columns.Contains("SupervisorLicenseNumber") && dr["SupervisorLicenseNumber"] != DBNull.Value)
            {
                objEntity.SupervisorLicenseNumber = Convert.ToString(dr["SupervisorLicenseNumber"]);
            }
            if (dr.Table.Columns.Contains("SupervisorStateLicensed") && dr["SupervisorStateLicensed"] != DBNull.Value)
            {
                objEntity.SupervisorStateLicensed = Convert.ToString(dr["SupervisorStateLicensed"]);
            }
            if (dr.Table.Columns.Contains("SupervisorLicenseExpirationDate") && dr["SupervisorLicenseExpirationDate"] != DBNull.Value)
            {
                objEntity.SupervisorLicenseExpirationDate = Convert.ToDateTime(dr["SupervisorLicenseExpirationDate"]);
            }
            if (dr.Table.Columns.Contains("ReferenceNumber") && dr["ReferenceNumber"] != DBNull.Value)
            {
                objEntity.ReferenceNumber = Convert.ToString(dr["ReferenceNumber"]);
            }

            if (dr.Table.Columns.Contains("IndividualNameId") && dr["IndividualNameId"] != DBNull.Value)
            {
                objEntity.IndividualNameId = Convert.ToInt32(dr["IndividualNameId"]);
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
            if (dr.Table.Columns.Contains("IndividualSupervisoryInfoGuid") && dr["IndividualSupervisoryInfoGuid"] != DBNull.Value)
            {
                objEntity.IndividualSupervisoryInfoGuid =  dr["IndividualSupervisoryInfoGuid"].ToString();
            }

            if (dr.Table.Columns.Contains("FirstName") && dr["FirstName"] != DBNull.Value)
            {
                objEntity.FirstName = dr["FirstName"].ToString();
            }

            if (dr.Table.Columns.Contains("LastName") && dr["LastName"] != DBNull.Value)
            {
                objEntity.LastName = dr["LastName"].ToString();
            }

            if (dr.Table.Columns.Contains("MiddleName") && dr["MiddleName"] != DBNull.Value)
            {
                objEntity.MiddleName = dr["MiddleName"].ToString();
            }

            return objEntity;

        }
    }
}
