using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class IndividualDAL : BaseDAL
    {
        public int Save_Individual(Individual objIndividual)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividual.IndividualId));
            lstParameter.Add(new MySqlParameter("FirstName", objIndividual.FirstName.NullString()));
            lstParameter.Add(new MySqlParameter("MiddleName", objIndividual.MiddleName.NullString()));
            lstParameter.Add(new MySqlParameter("LastName", objIndividual.LastName.NullString()));
            lstParameter.Add(new MySqlParameter("SuffixId", objIndividual.SuffixId));
            lstParameter.Add(new MySqlParameter("SSN", objIndividual.SSN.NullString()));
            lstParameter.Add(new MySqlParameter("IsItin", objIndividual.IsItin));
            lstParameter.Add(new MySqlParameter("DateOfBirth", objIndividual.DateOfBirth));
            lstParameter.Add(new MySqlParameter("RaceId", objIndividual.RaceId));
            lstParameter.Add(new MySqlParameter("Gender", objIndividual.Gender));
            lstParameter.Add(new MySqlParameter("HairColorId", objIndividual.HairColorId));
            lstParameter.Add(new MySqlParameter("EyeColorId", objIndividual.EyeColorId));
            lstParameter.Add(new MySqlParameter("Weight", objIndividual.Weight));
            lstParameter.Add(new MySqlParameter("Height", objIndividual.Height));
            lstParameter.Add(new MySqlParameter("PlaceOfBirth", objIndividual.PlaceOfBirth));
            lstParameter.Add(new MySqlParameter("CitizenshipId", objIndividual.CitizenshipId));
            lstParameter.Add(new MySqlParameter("ExternalId", objIndividual.ExternalId));
            lstParameter.Add(new MySqlParameter("ExternalId2", objIndividual.ExternalId2));
            lstParameter.Add(new MySqlParameter("IsArchived", objIndividual.IsArchived));
            lstParameter.Add(new MySqlParameter("IsActive", objIndividual.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividual.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividual.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objIndividual.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objIndividual.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objIndividual.ModifiedOn));
            lstParameter.Add(new MySqlParameter("IndividualGuid", objIndividual.IndividualGuid));
            lstParameter.Add(new MySqlParameter("Authenticator", objIndividual.Authenticator));

            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "individual_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<Individual> Get_All_Individual()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individual_Get_All", lstParameter.ToArray());

            List<Individual> lstEntity = new List<Individual>();
            Individual objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public Individual Get_Individual_By_IndividualId(int ID)
        {
            IndividualAddressDAL objADAL = new IndividualAddressDAL();
            IndividualContactDAL objCDAL = new IndividualContactDAL();

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("_IndividualId", ID));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "INDIVIDUAL_GET_BY_IndividualId", lstParameter.ToArray());
            Individual objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);

                objEntity.objIndividualAddress = objADAL.Get_Current_IndividualAddress_By_IndividualId(objEntity.IndividualId);
                objEntity.objIndividualContact = objCDAL.Get_Primary_IndividualContact_By_IndividualId(objEntity.IndividualId);

            }
            return objEntity;


        }

        public Individual Get_Individual_By_LastNameSSNCodeLicenseNumber(string lastName, string licenseNumber, string sSNCode)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("_lastName", lastName));
            lstParameter.Add(new MySqlParameter("_licenseNumber", licenseNumber));
            lstParameter.Add(new MySqlParameter("_sSNCode", sSNCode));

            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "INDIVIDUAL_GET_BY_LastNameSSNCodeLicenseNumber", lstParameter.ToArray());
            Individual objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<Individual> Search_Individual(IndividualSearch obj)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            string Name = "";
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            if (!string.IsNullOrEmpty(obj.Name))
            {
                Name = obj.Name.ToLower().Trim();
            }
            lstParameter.Add(new MySqlParameter("_Name", Name));
            lstParameter.Add(new MySqlParameter("_FirstName", obj.FirstName));
            lstParameter.Add(new MySqlParameter("_LastName", obj.LastName));
            lstParameter.Add(new MySqlParameter("_Phone", obj.Phone));
            lstParameter.Add(new MySqlParameter("_LicenseNumber", obj.LicenseNumber));
            lstParameter.Add(new MySqlParameter("_SSN", obj.SSN));
            lstParameter.Add(new MySqlParameter("_Email", obj.Email));

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Individual_Search", lstParameter.ToArray());

            List<Individual> lstEntity = new List<Individual>();
            Individual objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;


        }

        public List<Individual> Search_Individual_WithPager(IndividualSearch obj, int CurrentPage, int PagerSize)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            string Name = "";
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            if (!string.IsNullOrEmpty(obj.Name))
            {
                Name = obj.Name.ToLower().Trim();
            }
            lstParameter.Add(new MySqlParameter("_Name", Name));
            lstParameter.Add(new MySqlParameter("_FirstName", obj.FirstName));
            lstParameter.Add(new MySqlParameter("_LastName", obj.LastName));
            lstParameter.Add(new MySqlParameter("_Phone", obj.Phone));
            lstParameter.Add(new MySqlParameter("_LicenseNumber", obj.LicenseNumber));
            lstParameter.Add(new MySqlParameter("_SSN", obj.SSN));
            lstParameter.Add(new MySqlParameter("_Email", obj.Email));
            lstParameter.Add(new MySqlParameter("_DOB", obj.DateOfBirth));
            lstParameter.Add(new MySqlParameter("PageNo", CurrentPage));
            lstParameter.Add(new MySqlParameter("Pager", PagerSize));

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Individual_Search_WithPager", lstParameter.ToArray());

            List<Individual> lstEntity = new List<Individual>();
            Individual objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;


        }

        public List<Individual> Search_Renewal(RenewalApplication obj)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));

            lstParameter.Add(new MySqlParameter("_Name", obj.Name));
            lstParameter.Add(new MySqlParameter("_FirstName", obj.FirstName));
            lstParameter.Add(new MySqlParameter("_LastName", obj.LastName));
            lstParameter.Add(new MySqlParameter("_Phone", obj.Phone));
            lstParameter.Add(new MySqlParameter("_LicenseNumber", obj.LicenseNumber));
            lstParameter.Add(new MySqlParameter("_SSN", obj.SSN));
            lstParameter.Add(new MySqlParameter("_StatusId", obj.StatusId));

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Renewal_By_Search", lstParameter.ToArray());

            List<Individual> lstEntity = new List<Individual>();
            Individual objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;


        }

        //public List<Individual> Search_RenewalWithPager(RenewalApplication obj, int CurrentPage, int PagerSize)
        //{
        //    DataSet ds = new DataSet("DS");
        //    DBHelper objDB = new DBHelper();
        //    List<MySqlParameter> lstParameter = new List<MySqlParameter>();

        //    lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));

        //    lstParameter.Add(new MySqlParameter("_Name", obj.Name));
        //    lstParameter.Add(new MySqlParameter("_FirstName", obj.FirstName));
        //    lstParameter.Add(new MySqlParameter("_LastName", obj.LastName));
        //    lstParameter.Add(new MySqlParameter("_Phone", obj.Phone));
        //    lstParameter.Add(new MySqlParameter("_LicenseNumber", obj.LicenseNumber));
        //    lstParameter.Add(new MySqlParameter("_SSN", obj.SSN));
        //    lstParameter.Add(new MySqlParameter("_StatusId", obj.StatusId));
        //    lstParameter.Add(new MySqlParameter("PageNo", CurrentPage));
        //    lstParameter.Add(new MySqlParameter("Pager", PagerSize));

        //    ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Renewal_By_Search_WithPager", lstParameter.ToArray());

        //    List<Individual> lstEntity = new List<Individual>();
        //    Individual objEntity = null;
        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        objEntity = FetchEntity(dr);
        //        if (objEntity != null)
        //            lstEntity.Add(objEntity);
        //    }
        //    return lstEntity;


        //}


        private Individual FetchEntity(DataRow dr)
        {
            Individual objEntity = new Individual();
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("FirstName") && dr["FirstName"] != DBNull.Value)
            {
                objEntity.FirstName = Convert.ToString(dr["FirstName"]);
            }
            if (dr.Table.Columns.Contains("MiddleName") && dr["MiddleName"] != DBNull.Value)
            {
                objEntity.MiddleName = Convert.ToString(dr["MiddleName"]);
            }
            if (dr.Table.Columns.Contains("LastName") && dr["LastName"] != DBNull.Value)
            {
                objEntity.LastName = Convert.ToString(dr["LastName"]);
            }
            if (dr.Table.Columns.Contains("SuffixId") && dr["SuffixId"] != DBNull.Value)
            {
                objEntity.SuffixId = Convert.ToInt32(dr["SuffixId"]);
            }
            if (dr.Table.Columns.Contains("SSN") && dr["SSN"] != DBNull.Value)
            {
                objEntity.SSN = Convert.ToString(dr["SSN"]);
            }
            if (dr.Table.Columns.Contains("IsItin") && dr["IsItin"] != DBNull.Value)
            {
                objEntity.IsItin = Convert.ToBoolean(dr["IsItin"]);
            }
            if (dr.Table.Columns.Contains("DateOfBirth") && dr["DateOfBirth"] != DBNull.Value)
            {
                objEntity.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]);
            }
            if (dr.Table.Columns.Contains("RaceId") && dr["RaceId"] != DBNull.Value)
            {
                objEntity.RaceId = Convert.ToInt32(dr["RaceId"]);
            }
            if (dr.Table.Columns.Contains("Gender") && dr["Gender"] != DBNull.Value)
            {
                objEntity.Gender = Convert.ToString(dr["Gender"]);
            }
            if (dr.Table.Columns.Contains("HairColorId") && dr["HairColorId"] != DBNull.Value)
            {
                objEntity.HairColorId = Convert.ToInt32(dr["HairColorId"]);
            }
            if (dr.Table.Columns.Contains("EyeColorId") && dr["EyeColorId"] != DBNull.Value)
            {
                objEntity.EyeColorId = Convert.ToInt32(dr["EyeColorId"]);
            }
            if (dr.Table.Columns.Contains("Weight") && dr["Weight"] != DBNull.Value)
            {
                objEntity.Weight = Convert.ToInt32(dr["Weight"]);
            }
            if (dr.Table.Columns.Contains("Height") && dr["Height"] != DBNull.Value)
            {
                objEntity.Height = Convert.ToInt32(dr["Height"]);
            }
            if (dr.Table.Columns.Contains("PlaceOfBirth") && dr["PlaceOfBirth"] != DBNull.Value)
            {
                objEntity.PlaceOfBirth = Convert.ToString(dr["PlaceOfBirth"]);
            }
            if (dr.Table.Columns.Contains("CitizenshipId") && dr["CitizenshipId"] != DBNull.Value)
            {
                objEntity.CitizenshipId = Convert.ToInt32(dr["CitizenshipId"]);
            }
            if (dr.Table.Columns.Contains("ExternalId") && dr["ExternalId"] != DBNull.Value)
            {
                objEntity.ExternalId = Convert.ToString(dr["ExternalId"]);
            }
            if (dr.Table.Columns.Contains("ExternalId2") && dr["ExternalId2"] != DBNull.Value)
            {
                objEntity.ExternalId2 = Convert.ToString(dr["ExternalId2"]);
            }
            if (dr.Table.Columns.Contains("IsArchived") && dr["IsArchived"] != DBNull.Value)
            {
                objEntity.IsArchived = Convert.ToBoolean(dr["IsArchived"]);
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
            if (dr.Table.Columns.Contains("IndividualGuid") && dr["IndividualGuid"] != DBNull.Value)
            {
                objEntity.IndividualGuid = Convert.ToString(dr["IndividualGuid"]);
            }
            if (dr.Table.Columns.Contains("Authenticator") && dr["Authenticator"] != DBNull.Value)
            {
                objEntity.Authenticator = Convert.ToString(dr["Authenticator"]);
            }

            //For Search

            if (dr.Table.Columns.Contains("StatusId") && dr["StatusId"] != DBNull.Value)
            {
                objEntity.StatusId = Convert.ToInt32(dr["StatusId"]);
            }
            if (dr.Table.Columns.Contains("Name") && dr["Name"] != DBNull.Value)
            {
                objEntity.Name = Convert.ToString(dr["Name"]);
            }

            if (dr.Table.Columns.Contains("Phone") && dr["Phone"] != DBNull.Value)
            {
                objEntity.Phone = Convert.ToString(dr["Phone"]);
            }

            if (dr.Table.Columns.Contains("LicenseNumber") && dr["LicenseNumber"] != DBNull.Value)
            {
                objEntity.LicenseNumber = Convert.ToString(dr["LicenseNumber"]);
            }

            // Joined  Field For View Only
            if (dr.Table.Columns.Contains("Email") && dr["Email"] != DBNull.Value)
            {
                objEntity.Email = Convert.ToString(dr["Email"]);
            }
            if (dr.Table.Columns.Contains("StatusName") && dr["StatusName"] != DBNull.Value)
            {
                objEntity.StatusName = Convert.ToString(dr["StatusName"]);
            }
            if (dr.Table.Columns.Contains("SubmittedDate") && dr["SubmittedDate"] != DBNull.Value)
            {
                objEntity.SubmittedDate = Convert.ToDateTime(dr["SubmittedDate"]);
            }
            if (dr.Table.Columns.Contains("ApplicationNumber") && dr["ApplicationNumber"] != DBNull.Value)
            {
                objEntity.ApplicationNumber = Convert.ToString(dr["ApplicationNumber"]);
            }
            if (dr.Table.Columns.Contains("OriginalLicenseDate") && dr["OriginalLicenseDate"] != DBNull.Value)
            {
                objEntity.OriginalLicenseDate = Convert.ToDateTime(dr["OriginalLicenseDate"]);
            }

            if (dr.Table.Columns.Contains("Total_Recard") && dr["Total_Recard"] != DBNull.Value)
            {
                objEntity.Total_Recard = Convert.ToInt32(dr["Total_Recard"]);
            }

            if (dr.Table.Columns.Contains("LicenseStatusTypeName") && dr["LicenseStatusTypeName"] != DBNull.Value)
            {
                objEntity.LicenseStatusTypeName = Convert.ToString(dr["LicenseStatusTypeName"]);
            }

            if (dr.Table.Columns.Contains("StatusColorCode") && dr["StatusColorCode"] != DBNull.Value)
            {
                objEntity.StatusColorCode = Convert.ToString(dr["StatusColorCode"]);
            }

            if (dr.Table.Columns.Contains("LicenseTypeId") && dr["LicenseTypeId"] != DBNull.Value)
            {
                objEntity.LicenseTypeId = Convert.ToInt32(dr["LicenseTypeId"]);
            }
            if (dr.Table.Columns.Contains("LicenseTypeName") && dr["LicenseTypeName"] != DBNull.Value)
            {
                objEntity.LicenseTypeName = Convert.ToString(dr["LicenseTypeName"]);
            }

            if (dr.Table.Columns.Contains("LicenseStatusTypeId") && dr["LicenseStatusTypeId"] != DBNull.Value)
            {
                objEntity.LicenseStatusTypeId = Convert.ToInt32(dr["LicenseStatusTypeId"]);
            }
            if (dr.Table.Columns.Contains("LicenseStatusTypeCode") && dr["LicenseStatusTypeCode"] != DBNull.Value)
            {
                objEntity.LicenseStatusTypeCode = Convert.ToString(dr["LicenseStatusTypeCode"]);
            }

            if (dr.Table.Columns.Contains("LicenseStatusTypeCode") && dr["LicenseStatusTypeCode"] != DBNull.Value)
            {
                objEntity.LicenseStatusTypeCode = Convert.ToString(dr["LicenseStatusTypeCode"]);
            }

            if (dr.Table.Columns.Contains("IsNameChanged") && dr["IsNameChanged"] != DBNull.Value)
            {
                objEntity.IsNameChanged = Convert.ToBoolean(dr["IsNameChanged"]);
            }

            if (dr.Table.Columns.Contains("PlaceofBirthCity") && dr["PlaceofBirthCity"] != DBNull.Value)
            {
                objEntity.PlaceofBirthCity = Convert.ToString(dr["PlaceofBirthCity"]);
            }

            if (dr.Table.Columns.Contains("PlaceofBirthState") && dr["PlaceofBirthState"] != DBNull.Value)
            {
                objEntity.PlaceofBirthState = Convert.ToString(dr["PlaceofBirthState"]);
            }

            if (dr.Table.Columns.Contains("PlaceofBirthCountry") && dr["PlaceofBirthCountry"] != DBNull.Value)
            {
                objEntity.PlaceofBirthCountry = Convert.ToInt32(dr["PlaceofBirthCountry"]);
            }
            if (dr.Table.Columns.Contains("Picture") && dr["Picture"] != DBNull.Value)
            {
                objEntity.Picture = Convert.ToString(dr["Picture"]);
            }

            return objEntity;

        }


        public int Save_IndividualProvider(IndividualName objIndividual)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("FirstName", objIndividual.FirstName.NullString()));
            lstParameter.Add(new MySqlParameter("LastName", objIndividual.LastName.NullString()));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividual.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", DateTime.Now));
            lstParameter.Add(new MySqlParameter("IndividualNameTypeId", objIndividual.IndividualNameTypeId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividual.IndividualId));
            lstParameter.Add(new MySqlParameter("ProviderId", objIndividual.ProviderId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objIndividual.ApplicationId));
            lstParameter.Add(new MySqlParameter("ProvIndvJobTitle", objIndividual.ProvIndvJobTitle));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "individualname_SaveIndividualProvider", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }
    }
}
