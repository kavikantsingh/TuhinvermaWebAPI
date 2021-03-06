using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class ProviderDAL : BaseDAL
    {
        public int Save_Provider(Provider objProvider)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ProviderId", objProvider.ProviderId));
            lstParameter.Add(new MySqlParameter("ProviderNumber", objProvider.ProviderNumber));
            lstParameter.Add(new MySqlParameter("DepartmentId", objProvider.DepartmentId));
            lstParameter.Add(new MySqlParameter("ProviderTypeId", objProvider.ProviderTypeId));
            lstParameter.Add(new MySqlParameter("ProviderName", objProvider.ProviderName));
            lstParameter.Add(new MySqlParameter("ProviderDBAName", objProvider.ProviderDBAName));
            lstParameter.Add(new MySqlParameter("LicenseNumber", objProvider.LicenseNumber));
            lstParameter.Add(new MySqlParameter("ProviderStatusTypeId", objProvider.ProviderStatusTypeId));
            lstParameter.Add(new MySqlParameter("OwnershipCompany", objProvider.OwnershipCompany));
            lstParameter.Add(new MySqlParameter("BillingNumber", objProvider.BillingNumber));
            lstParameter.Add(new MySqlParameter("ClosedDate", objProvider.ClosedDate));
            lstParameter.Add(new MySqlParameter("TaxId", objProvider.TaxId));
            lstParameter.Add(new MySqlParameter("ReferenceNumber", objProvider.ReferenceNumber));
            lstParameter.Add(new MySqlParameter("IsEnabled", objProvider.IsEnabled));
            lstParameter.Add(new MySqlParameter("IsActive", objProvider.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objProvider.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objProvider.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objProvider.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objProvider.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objProvider.ModifiedOn));
            lstParameter.Add(new MySqlParameter("ProviderGuid", objProvider.ProviderGuid));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "Provider_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<Provider> Get_All_Provider()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();

            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "provider_Get_All", lstParameter.ToArray());
            List<Provider> lstEntity = new List<Provider>();
            Provider objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public Provider Get_Provider_By_ProviderId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_ProviderId", ID));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "provider_Get_By_providerId", lstParameter.ToArray());
            Provider objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private Provider FetchEntity(DataRow dr)
        {
            Provider objEntity = new Provider();
            if (dr.Table.Columns.Contains("ProviderId") && dr["ProviderId"] != DBNull.Value)
            {
                objEntity.ProviderId = Convert.ToInt32(dr["ProviderId"]);
            }
            if (dr.Table.Columns.Contains("ProviderNumber") && dr["ProviderNumber"] != DBNull.Value)
            {
                objEntity.ProviderNumber = Convert.ToString(dr["ProviderNumber"]);
            }
            if (dr.Table.Columns.Contains("DepartmentId") && dr["DepartmentId"] != DBNull.Value)
            {
                objEntity.DepartmentId = Convert.ToInt32(dr["DepartmentId"]);
            }
            if (dr.Table.Columns.Contains("ProviderTypeId") && dr["ProviderTypeId"] != DBNull.Value)
            {
                objEntity.ProviderTypeId = Convert.ToInt32(dr["ProviderTypeId"]);
            }
            if (dr.Table.Columns.Contains("ProviderName") && dr["ProviderName"] != DBNull.Value)
            {
                objEntity.ProviderName = Convert.ToString(dr["ProviderName"]);
            }
            if (dr.Table.Columns.Contains("ProviderDBAName") && dr["ProviderDBAName"] != DBNull.Value)
            {
                objEntity.ProviderDBAName = Convert.ToString(dr["ProviderDBAName"]);
            }
            if (dr.Table.Columns.Contains("LicenseNumber") && dr["LicenseNumber"] != DBNull.Value)
            {
                objEntity.LicenseNumber = Convert.ToString(dr["LicenseNumber"]);
            }
            if (dr.Table.Columns.Contains("ProviderStatusTypeId") && dr["ProviderStatusTypeId"] != DBNull.Value)
            {
                objEntity.ProviderStatusTypeId = Convert.ToInt32(dr["ProviderStatusTypeId"]);
            }
            if (dr.Table.Columns.Contains("OwnershipCompany") && dr["OwnershipCompany"] != DBNull.Value)
            {
                objEntity.OwnershipCompany = Convert.ToString(dr["OwnershipCompany"]);
            }
            if (dr.Table.Columns.Contains("BillingNumber") && dr["BillingNumber"] != DBNull.Value)
            {
                objEntity.BillingNumber = Convert.ToString(dr["BillingNumber"]);
            }
            if (dr.Table.Columns.Contains("ClosedDate") && dr["ClosedDate"] != DBNull.Value)
            {
                objEntity.ClosedDate = Convert.ToDateTime(dr["ClosedDate"]);
            }
            if (dr.Table.Columns.Contains("TaxId") && dr["TaxId"] != DBNull.Value)
            {
                objEntity.TaxId = Convert.ToString(dr["TaxId"]);
            }
            if (dr.Table.Columns.Contains("ReferenceNumber") && dr["ReferenceNumber"] != DBNull.Value)
            {
                objEntity.ReferenceNumber = Convert.ToString(dr["ReferenceNumber"]);
            }
            if (dr.Table.Columns.Contains("IsEnabled") && dr["IsEnabled"] != DBNull.Value)
            {
                objEntity.IsEnabled = Convert.ToBoolean(dr["IsEnabled"]);
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
            if (dr.Table.Columns.Contains("ProviderGuid") && dr["ProviderGuid"] != DBNull.Value)
            {
                objEntity.ProviderGuid = Convert.ToString(dr["ProviderGuid"]);
            }
            return objEntity;

        }



        public int SaveSchoolInformation(ProviderInformation objProvider)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            // lstParameter.Add(new MySqlParameter("ProviderId", objProvider.ProviderId));
            // lstParameter.Add(new MySqlParameter("ProviderNumber", objProvider.ProviderNumber));
            //lstParameter.Add(new MySqlParameter("DepartmentId", objProvider.DepartmentId));
            // lstParameter.Add(new MySqlParameter("ProviderTypeId", objProvider.ProviderTypeId));
            // lstParameter.Add(new MySqlParameter("ProviderName", objProvider.ProviderName));
            //lstParameter.Add(new MySqlParameter("ProviderDBAName", objProvider.ProviderDBAName));
            // lstParameter.Add(new MySqlParameter("LicenseNumber", objProvider.LicenseNumber));
            // lstParameter.Add(new MySqlParameter("ProviderStatusTypeId", objProvider.ProviderStatusTypeId));
            // lstParameter.Add(new MySqlParameter("OwnershipCompany", objProvider.OwnershipCompany));
            // lstParameter.Add(new MySqlParameter("BillingNumber", objProvider.BillingNumber));
            // lstParameter.Add(new MySqlParameter("ClosedDate", objProvider.ClosedDate));
            // lstParameter.Add(new MySqlParameter("TaxId", objProvider.TaxId));
            // lstParameter.Add(new MySqlParameter("ReferenceNumber", objProvider.ReferenceNumber));
            // lstParameter.Add(new MySqlParameter("IsEnabled", objProvider.IsEnabled));
            //  lstParameter.Add(new MySqlParameter("IsActive", objProvider.IsActive));
            //  lstParameter.Add(new MySqlParameter("IsDeleted", objProvider.IsDeleted));
            //  lstParameter.Add(new MySqlParameter("CreatedBy", objProvider.CreatedBy));
            //  lstParameter.Add(new MySqlParameter("CreatedOn", objProvider.CreatedOn));
            //  lstParameter.Add(new MySqlParameter("ModifiedBy", objProvider.ModifiedBy));
            //  lstParameter.Add(new MySqlParameter("ModifiedOn", objProvider.ModifiedOn));
            //   lstParameter.Add(new MySqlParameter("ProviderGuid", objProvider.ProviderGuid));
            //   lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "Provider_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        #region Shekhar

        public int SaveProviderStaff(ProviderStaff objProviderStaff)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ProviderStaffId", objProviderStaff.ProviderStaffId));
            lstParameter.Add(new MySqlParameter("ProviderIndvNameInfoId", objProviderStaff.ProviderIndvNameInfoId));
            lstParameter.Add(new MySqlParameter("ProviderId", objProviderStaff.ProviderId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objProviderStaff.ApplicationId));
            lstParameter.Add(new MySqlParameter("ProviderContactId", objProviderStaff.ProviderContactId));
            lstParameter.Add(new MySqlParameter("IsBackgroundCheckReq", objProviderStaff.IsBackgroundCheckReq));
            lstParameter.Add(new MySqlParameter("CAMTCNumber", objProviderStaff.CAMTCNumber));
            lstParameter.Add(new MySqlParameter("ReferenceNumber", objProviderStaff.ReferenceNumber));
            lstParameter.Add(new MySqlParameter("IsActive", objProviderStaff.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objProviderStaff.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objProviderStaff.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objProviderStaff.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objProviderStaff.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objProviderStaff.ModifiedOn));
            lstParameter.Add(new MySqlParameter("ProviderStaffGuid", objProviderStaff.ProviderStaffGuid));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "providerStaff_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public int SaveProvIndvNameTitle(ProvIndvNameTitle objProviderIndName)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ProvIndvNameTitlePosId", objProviderIndName.ProvIndvNameTitlePosId));
            lstParameter.Add(new MySqlParameter("ProviderIndvNameInfoId", objProviderIndName.ProviderIndvNameInfoId));
            lstParameter.Add(new MySqlParameter("ProviderId", objProviderIndName.ProviderId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objProviderIndName.ApplicationId));
            lstParameter.Add(new MySqlParameter("ProviderStaffId", objProviderIndName.ProviderStaffId));
            lstParameter.Add(new MySqlParameter("ProvIndvNameTitlePositionId", objProviderIndName.ProvIndvNameTitlePositionId));
            lstParameter.Add(new MySqlParameter("ProvIndvNameTitlePosition", objProviderIndName.ProvIndvNameTitlePosition));
            lstParameter.Add(new MySqlParameter("ReferenceNumber", objProviderIndName.ReferenceNumber));

            lstParameter.Add(new MySqlParameter("IsActive", objProviderIndName.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objProviderIndName.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objProviderIndName.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objProviderIndName.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objProviderIndName.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objProviderIndName.ModifiedOn));
            lstParameter.Add(new MySqlParameter("ProvIndvNameTitlePosGuid", objProviderIndName.ProvIndvNameTitlePosGuid));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "providerIndvNameTitle_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<ProviderStaff> GetAllProviderStaffDetails(int ApplicationId, int ProviderId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            lstParameter.Add(new MySqlParameter("ProviderId", ProviderId));
            lstParameter.Add(new MySqlParameter("ApplicationId", ApplicationId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Get_ProviderStaffByProviderId", lstParameter.ToArray());
            List<ProviderStaff> lstEntity = new List<ProviderStaff>();
            ProviderStaff objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchStaff(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private ProviderStaff FetchStaff(DataRow dr)
        {
            ProviderStaff objEntity = new ProviderStaff();
            if (dr.Table.Columns.Contains("FirstName") && dr["FirstName"] != DBNull.Value)
            {
                objEntity.ProviderStaffFirstName = Convert.ToString(dr["FirstName"]);
            }
            if (dr.Table.Columns.Contains("MiddleName") && dr["MiddleName"] != DBNull.Value)
            {
                objEntity.ProviderStaffMiddleName = Convert.ToString(dr["MiddleName"]);
            }
            if (dr.Table.Columns.Contains("active") && dr["active"] != DBNull.Value)
            {
                objEntity.ActionList = Convert.ToString(dr["active"]);
            }
            if (dr.Table.Columns.Contains("EmailId") && dr["EmailId"] != DBNull.Value)
            {
                objEntity.ProviderStaffEmail = Convert.ToString(dr["EmailId"]);
            }
            if (dr.Table.Columns.Contains("LastName") && dr["LastName"] != DBNull.Value)
            {
                objEntity.ProviderStaffLastName = Convert.ToString(dr["LastName"]);
            }
            if (dr.Table.Columns.Contains("ProviderStaffId") && dr["ProviderStaffId"] != DBNull.Value)
            {
                objEntity.ProviderStaffId = Convert.ToInt32(dr["ProviderStaffId"]);
            }
            if (dr.Table.Columns.Contains("ProviderIndvNameInfoId") && dr["ProviderIndvNameInfoId"] != DBNull.Value)
            {
                objEntity.ProviderIndvNameInfoId = Convert.ToInt32(dr["ProviderIndvNameInfoId"]);
            }
            if (dr.Table.Columns.Contains("IndividualNameId") && dr["IndividualNameId"] != DBNull.Value)
            {
                objEntity.InduvidualNameId = Convert.ToInt32(dr["IndividualNameId"]);

            }
            if (dr.Table.Columns.Contains("ProviderId") && dr["ProviderId"] != DBNull.Value)
            {
                objEntity.ProviderId = Convert.ToInt32(dr["ProviderId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationId") && dr["ApplicationId"] != DBNull.Value)
            {
                objEntity.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
            }
            if (dr.Table.Columns.Contains("ProviderContactId") && dr["ProviderContactId"] != DBNull.Value)
            {
                objEntity.ProviderContactId = Convert.ToInt32(dr["ProviderContactId"]);
            }
            if (dr.Table.Columns.Contains("ContactId") && dr["ContactId"] != DBNull.Value)
            {
                objEntity.ContactId = Convert.ToInt32(dr["ContactId"]);
            }
            if (dr.Table.Columns.Contains("IsBackgroundCheckReq") && dr["IsBackgroundCheckReq"] != DBNull.Value)
            {
                objEntity.IsBackgroundCheckReq = Convert.ToBoolean(dr["IsBackgroundCheckReq"]);
            }
            if (dr.Table.Columns.Contains("CAMTCNumber") && dr["CAMTCNumber"] != DBNull.Value)
            {
                objEntity.CAMTCNumber = Convert.ToString(dr["CAMTCNumber"]);
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
            if (dr.Table.Columns.Contains("ProviderStaffGuid") && dr["ProviderStaffGuid"] != DBNull.Value)
            {
                objEntity.ProviderStaffGuid = Convert.ToString(dr["ProviderStaffGuid"]);
            }
            if (dr.Table.Columns.Contains("posids") && dr["posids"] != DBNull.Value)
            {
                objEntity.posids = Convert.ToString(dr["posids"]);
            }


            if (dr.Table.Columns.Contains("ids") && dr["ids"] != DBNull.Value)
            {
                objEntity.ids = Convert.ToString(dr["ids"]);
            }
            if (dr.Table.Columns.Contains("titles") && dr["titles"] != DBNull.Value)
            {
                objEntity.titles = Convert.ToString(dr["titles"]);
            }
            return objEntity;

        }


        public int SaveProviderOtherProgram(ProviderOtherProgramName objProviderOthrPrgrm)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ProviderOtherProgramId", objProviderOthrPrgrm.ProviderOtherProgramId));
            lstParameter.Add(new MySqlParameter("ProviderId", objProviderOthrPrgrm.ProviderId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objProviderOthrPrgrm.ApplicationId));
            lstParameter.Add(new MySqlParameter("ProgramOtherName", objProviderOthrPrgrm.ProgramOtherName));
            lstParameter.Add(new MySqlParameter("ReferenceNumber", objProviderOthrPrgrm.ReferenceNumber));
            lstParameter.Add(new MySqlParameter("IsActive", objProviderOthrPrgrm.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objProviderOthrPrgrm.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objProviderOthrPrgrm.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objProviderOthrPrgrm.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objProviderOthrPrgrm.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objProviderOthrPrgrm.ModifiedOn));
            lstParameter.Add(new MySqlParameter("ProviderOtherProgramGuid", objProviderOthrPrgrm.ProviderOtherProgramGuid));
            lstParameter.Add(new MySqlParameter("Actions", objProviderOthrPrgrm.Action));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "providerOtherProgram_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<ProviderOtherProgramName> GetAllProviderOtherProgram(int ApplicationId, int ProviderId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            lstParameter.Add(new MySqlParameter("ProviderId", ProviderId));
            lstParameter.Add(new MySqlParameter("ApplicationId", ApplicationId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Get_ProviderProgramNameByProviderId", lstParameter.ToArray());
            List<ProviderOtherProgramName> lstEntity = new List<ProviderOtherProgramName>();
            ProviderOtherProgramName objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchOtherProgram(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private ProviderOtherProgramName FetchOtherProgram(DataRow dr)
        {
            ProviderOtherProgramName objEntity = new ProviderOtherProgramName();
            if (dr.Table.Columns.Contains("ProviderOtherProgramId") && dr["ProviderOtherProgramId"] != DBNull.Value)
            {
                objEntity.ProviderOtherProgramId = Convert.ToInt32(dr["ProviderOtherProgramId"]);
            }
            if (dr.Table.Columns.Contains("ProviderId") && dr["ProviderId"] != DBNull.Value)
            {
                objEntity.ProviderId = Convert.ToInt32(dr["ProviderId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationId") && dr["ApplicationId"] != DBNull.Value)
            {
                objEntity.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
            }
            if (dr.Table.Columns.Contains("ProgramOtherName") && dr["ProgramOtherName"] != DBNull.Value)
            {
                objEntity.ProgramOtherName = Convert.ToString(dr["ProgramOtherName"]);
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


        public int SaveProviderGraduatesNumber(ProviderGraduatesNumber objProviderGraduatesNo)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ProviderGraduatesNumberId", objProviderGraduatesNo.ProviderGraduatesNumberId));
            lstParameter.Add(new MySqlParameter("ProviderId", objProviderGraduatesNo.ProviderId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objProviderGraduatesNo.ApplicationId));
            lstParameter.Add(new MySqlParameter("CalendarYear", objProviderGraduatesNo.CalendarYear));
            lstParameter.Add(new MySqlParameter("CalendarYearEstGradCount", objProviderGraduatesNo.CalendarYearEstGradCount));
            lstParameter.Add(new MySqlParameter("CalendarYearActualGradCount", objProviderGraduatesNo.CalendarYearActualGradCount));
            lstParameter.Add(new MySqlParameter("IsActive", objProviderGraduatesNo.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objProviderGraduatesNo.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objProviderGraduatesNo.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objProviderGraduatesNo.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objProviderGraduatesNo.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objProviderGraduatesNo.ModifiedOn));
            lstParameter.Add(new MySqlParameter("ProviderGraduatesNumberGuid", objProviderGraduatesNo.ProviderGraduatesNumberGuid));
            lstParameter.Add(new MySqlParameter("Action", objProviderGraduatesNo.Action));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "providerGraduatesNumber_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<ProviderGraduatesNumber> GetAllProviderGraduatesNumber(int ApplicationId, int ProviderId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            lstParameter.Add(new MySqlParameter("ProviderId", ProviderId));
            lstParameter.Add(new MySqlParameter("ApplicationId", ApplicationId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Get_ProviderGraduatesNumberByProviderId", lstParameter.ToArray());
            List<ProviderGraduatesNumber> lstEntity = new List<ProviderGraduatesNumber>();
            ProviderGraduatesNumber objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchGraduatesNumber(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private ProviderGraduatesNumber FetchGraduatesNumber(DataRow dr)
        {
            ProviderGraduatesNumber objEntity = new ProviderGraduatesNumber();
            if (dr.Table.Columns.Contains("ProviderGraduatesNumberId") && dr["ProviderGraduatesNumberId"] != DBNull.Value)
            {
                objEntity.ProviderGraduatesNumberId = Convert.ToInt32(dr["ProviderGraduatesNumberId"]);
            }
            if (dr.Table.Columns.Contains("ProviderId") && dr["ProviderId"] != DBNull.Value)
            {
                objEntity.ProviderId = Convert.ToInt32(dr["ProviderId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationId") && dr["ApplicationId"] != DBNull.Value)
            {
                objEntity.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
            }
            if (dr.Table.Columns.Contains("CalendarYear") && dr["CalendarYear"] != DBNull.Value)
            {
                objEntity.CalendarYear = Convert.ToInt32(dr["CalendarYear"]);
            }
            if (dr.Table.Columns.Contains("CalendarYearEstGradCount") && dr["CalendarYearEstGradCount"] != DBNull.Value)
            {
                objEntity.CalendarYearEstGradCount = Convert.ToInt32(dr["CalendarYearEstGradCount"]);
            }
            if (dr.Table.Columns.Contains("CalendarYearActualGradCount") && dr["CalendarYearActualGradCount"] != DBNull.Value)
            {
                objEntity.CalendarYearActualGradCount = Convert.ToInt32(dr["CalendarYearActualGradCount"]);
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
            if (dr.Table.Columns.Contains("ProviderGraduatesNumberGuid") && dr["ProviderGraduatesNumberGuid"] != DBNull.Value)
            {
                objEntity.ProviderGraduatesNumberGuid = Convert.ToString(dr["ProviderGraduatesNumberGuid"]);
            }
            return objEntity;

        }


        public int SaveProviderBusinessType(ProviderBusinessType objProviderBusinessType)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ProviderBusinessTypeId", objProviderBusinessType.ProviderBusinessTypeId));
            lstParameter.Add(new MySqlParameter("ProviderId", objProviderBusinessType.ProviderId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objProviderBusinessType.ApplicationId));
            lstParameter.Add(new MySqlParameter("BusinessOrgTypeId", objProviderBusinessType.BusinessOrgTypeId));
            lstParameter.Add(new MySqlParameter("IsActive", objProviderBusinessType.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objProviderBusinessType.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objProviderBusinessType.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objProviderBusinessType.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objProviderBusinessType.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objProviderBusinessType.ModifiedOn));
            lstParameter.Add(new MySqlParameter("ProviderBusinessTypeGuid", objProviderBusinessType.ProviderBusinessTypeGuid));
            //lstParameter.Add(new MySqlParameter("Action", objProviderBusinessType.Action));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "providerbusinesstype_save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<ProviderBusinessType> GetProviderBusinessTypeByProviderId(int ApplicationId, int ProviderId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            lstParameter.Add(new MySqlParameter("ProviderId", ProviderId));
            lstParameter.Add(new MySqlParameter("ApplicationId", ApplicationId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Get_ProviderBusinessTypeByProviderId", lstParameter.ToArray());
            List<ProviderBusinessType> lstEntity = new List<ProviderBusinessType>();
            ProviderBusinessType objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchBusinessType(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private ProviderBusinessType FetchBusinessType(DataRow dr)
        {
            ProviderBusinessType objEntity = new ProviderBusinessType();
            if (dr.Table.Columns.Contains("ProviderBusinessTypeId") && dr["ProviderBusinessTypeId"] != DBNull.Value)
            {
                objEntity.ProviderBusinessTypeId = Convert.ToInt32(dr["ProviderBusinessTypeId"]);
            }
            if (dr.Table.Columns.Contains("ProviderId") && dr["ProviderId"] != DBNull.Value)
            {
                objEntity.ProviderId = Convert.ToInt32(dr["ProviderId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationId") && dr["ApplicationId"] != DBNull.Value)
            {
                objEntity.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
            }
            if (dr.Table.Columns.Contains("BusinessOrgTypeId") && dr["BusinessOrgTypeId"] != DBNull.Value)
            {
                objEntity.BusinessOrgTypeId = Convert.ToInt32(dr["BusinessOrgTypeId"]);
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
            if (dr.Table.Columns.Contains("ProviderBusinessTypeGuid") && dr["ProviderBusinessTypeGuid"] != DBNull.Value)
            {
                objEntity.ProviderBusinessTypeGuid = Convert.ToString(dr["ProviderBusinessTypeGuid"]);
            }
            return objEntity;

        }


        public int SaveProviderTabStatus(ProviderTabStatus ObjProviderTabStatus)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ApplicationTabStatusId", ObjProviderTabStatus.ApplicationTabStatusId));
            lstParameter.Add(new MySqlParameter("ApplicationId", ObjProviderTabStatus.ApplicationId));
            lstParameter.Add(new MySqlParameter("PageModuleId", ObjProviderTabStatus.PageModuleId));
            lstParameter.Add(new MySqlParameter("PageModuleTabSubModuleId", ObjProviderTabStatus.PageModuleTabSubModuleId));
            lstParameter.Add(new MySqlParameter("PageTabSectionId", ObjProviderTabStatus.PageTabSectionId));
            lstParameter.Add(new MySqlParameter("IndividualId", ObjProviderTabStatus.IndividualId));
            lstParameter.Add(new MySqlParameter("ProviderId", ObjProviderTabStatus.ProviderId));
            lstParameter.Add(new MySqlParameter("TabName", ObjProviderTabStatus.TabName));
            lstParameter.Add(new MySqlParameter("ApplicationTabStatus", ObjProviderTabStatus.ApplicationTabStatus));
            lstParameter.Add(new MySqlParameter("SortOrder", ObjProviderTabStatus.SortOrder));
            lstParameter.Add(new MySqlParameter("IsActive", ObjProviderTabStatus.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", ObjProviderTabStatus.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", ObjProviderTabStatus.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", ObjProviderTabStatus.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", ObjProviderTabStatus.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", ObjProviderTabStatus.ModifiedOn));
            
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "providerTabStatus_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<ProviderTabStatusGetResponse> GetAllProviderTabStatus(int ApplicationId, int ProviderId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            lstParameter.Add(new MySqlParameter("ProviderId", ProviderId));
            lstParameter.Add(new MySqlParameter("ApplicationId", ApplicationId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Get_ProviderTabStatusByProviderId", lstParameter.ToArray());
            List<ProviderTabStatusGetResponse> lstEntity = new List<ProviderTabStatusGetResponse>();
            ProviderTabStatusGetResponse objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchTabStatus(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private ProviderTabStatusGetResponse FetchTabStatus(DataRow dr)
        {
            ProviderTabStatusGetResponse objEntity = new ProviderTabStatusGetResponse();
            if (dr.Table.Columns.Contains("TabName") && dr["TabName"] != DBNull.Value)
            {
                objEntity.TabName = Convert.ToString(dr["TabName"]);
            }
            if (dr.Table.Columns.Contains("ApplicationTabStatus") && dr["ApplicationTabStatus"] != DBNull.Value)
            {
                objEntity.ApplicationTabStatus = Convert.ToBoolean(dr["ApplicationTabStatus"]);
            }
            return objEntity;

        }


        public int SaveProviderNames(ProviderNames objProvider)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("ProviderName_Id", objProvider.ProviderNameId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objProvider.ApplicationId));
            lstParameter.Add(new MySqlParameter("ProviderId", objProvider.ProviderId));
            lstParameter.Add(new MySqlParameter("IndividualId", objProvider.IndividualId));
            lstParameter.Add(new MySqlParameter("ProviderName", objProvider.ProviderName));
            lstParameter.Add(new MySqlParameter("DateofNameChange", objProvider.DateofNameChange));
            lstParameter.Add(new MySqlParameter("ProviderNameStatusId", objProvider.ProviderNameStatusId));
            lstParameter.Add(new MySqlParameter("ProviderNameTypeId", objProvider.ProviderNameTypeId));
            lstParameter.Add(new MySqlParameter("ReferenceNumber", objProvider.ReferenceNumber));
            lstParameter.Add(new MySqlParameter("CreatedBy", objProvider.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", DateTime.Now));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objProvider.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objProvider.ModifiedOn));
            lstParameter.Add(new MySqlParameter("ProviderNameGuid", objProvider.ProviderNameGuid));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "ProviderName_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }


        #endregion


    }
}
