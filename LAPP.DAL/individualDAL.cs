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
            lstParameter.Add(new MySqlParameter("FirstName", objIndividual.FirstName));
            lstParameter.Add(new MySqlParameter("MiddleName", objIndividual.MiddleName));
            lstParameter.Add(new MySqlParameter("LastName", objIndividual.LastName));
            lstParameter.Add(new MySqlParameter("SuffixId", objIndividual.SuffixId));
            lstParameter.Add(new MySqlParameter("SSN", objIndividual.SSN));
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
            }
            return objEntity;
        }
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
            return objEntity;

        }
    }
}
