using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class IndividualContactDAL : BaseDAL
    {
        public int Save_IndividualContact(IndividualContact objIndividualContact)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualContactId", objIndividualContact.IndividualContactId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualContact.IndividualId));
            lstParameter.Add(new MySqlParameter("ContactId", objIndividualContact.ContactId));
            lstParameter.Add(new MySqlParameter("ContactTypeId", objIndividualContact.ContactTypeId));
            lstParameter.Add(new MySqlParameter("BeginDate", objIndividualContact.BeginDate));
            lstParameter.Add(new MySqlParameter("EndDate", objIndividualContact.EndDate));
            lstParameter.Add(new MySqlParameter("IsPreferredContact", objIndividualContact.IsPreferredContact));
            lstParameter.Add(new MySqlParameter("IsMobile", objIndividualContact.IsMobile));
            lstParameter.Add(new MySqlParameter("IsActive", objIndividualContact.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualContact.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualContact.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objIndividualContact.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objIndividualContact.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objIndividualContact.ModifiedOn));
            lstParameter.Add(new MySqlParameter("IndividualContactGuid", objIndividualContact.IndividualContactGuid));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "individualcontact_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<IndividualContact> Get_All_IndividualContact()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "INDIVIDUALCONTACT_GET_ALL");
            List<IndividualContact> lstEntity = new List<IndividualContact>();
            IndividualContact objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<IndividualContact> Get_IndividualContact_By_IndividualId(int IndividualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "INDIVIDUALCONTACT_GET_BY_IndividualId", lstParameter.ToArray());
            List<IndividualContact> lstEntity = new List<IndividualContact>();
            IndividualContact objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public IndividualContact Get_IndividualContact_By_IndividualContactId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("_IndividualContactId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "INDIVIDUALCONTACT_GET_BY_IndividualContactId", lstParameter.ToArray());
            IndividualContact objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private IndividualContact FetchEntity(DataRow dr)
        {
            IndividualContact objEntity = new IndividualContact();
            if (dr.Table.Columns.Contains("IndividualContactId") && dr["IndividualContactId"] != DBNull.Value)
            {
                objEntity.IndividualContactId = Convert.ToInt32(dr["IndividualContactId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("ContactId") && dr["ContactId"] != DBNull.Value)
            {
                objEntity.ContactId = Convert.ToInt32(dr["ContactId"]);
            }
            if (dr.Table.Columns.Contains("ContactTypeId") && dr["ContactTypeId"] != DBNull.Value)
            {
                objEntity.ContactTypeId = Convert.ToInt32(dr["ContactTypeId"]);
            }
            if (dr.Table.Columns.Contains("BeginDate") && dr["BeginDate"] != DBNull.Value)
            {
                objEntity.BeginDate = Convert.ToDateTime(dr["BeginDate"]);
            }
            if (dr.Table.Columns.Contains("EndDate") && dr["EndDate"] != DBNull.Value)
            {
                objEntity.EndDate = Convert.ToDateTime(dr["EndDate"]);
            }
            if (dr.Table.Columns.Contains("IsPreferredContact") && dr["IsPreferredContact"] != DBNull.Value)
            {
                objEntity.IsPreferredContact = Convert.ToBoolean(dr["IsPreferredContact"]);
            }
            if (dr.Table.Columns.Contains("IsMobile") && dr["IsMobile"] != DBNull.Value)
            {
                objEntity.IsMobile = Convert.ToBoolean(dr["IsMobile"]);
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
            if (dr.Table.Columns.Contains("IndividualContactGuid") && dr["IndividualContactGuid"] != DBNull.Value)
            {
                objEntity.IndividualContactGuid = Convert.ToString(dr["IndividualContactGuid"]);
            }


            if (dr.Table.Columns.Contains("ContactFirstName") && dr["ContactFirstName"] != DBNull.Value)
            {
                objEntity.ContactFirstName = Convert.ToString(dr["ContactFirstName"]);
            }
            if (dr.Table.Columns.Contains("ContactLastName") && dr["ContactLastName"] != DBNull.Value)
            {
                objEntity.ContactLastName = Convert.ToString(dr["ContactLastName"]);
            }
            if (dr.Table.Columns.Contains("ContactMiddleName") && dr["ContactMiddleName"] != DBNull.Value)
            {
                objEntity.ContactMiddleName = Convert.ToString(dr["ContactMiddleName"]);
            }
            if (dr.Table.Columns.Contains("ContactTypeId") && dr["ContactTypeId"] != DBNull.Value)
            {
                objEntity.ContactTypeId = Convert.ToInt32(dr["ContactTypeId"]);
            }
            if (dr.Table.Columns.Contains("Code") && dr["Code"] != DBNull.Value)
            {
                objEntity.Code = Convert.ToString(dr["Code"]);
            }
            if (dr.Table.Columns.Contains("ContactInfo") && dr["ContactInfo"] != DBNull.Value)
            {
                objEntity.ContactInfo = Convert.ToString(dr["ContactInfo"]);
            }
            if (dr.Table.Columns.Contains("DateContactValidated") && dr["DateContactValidated"] != DBNull.Value)
            {
                objEntity.DateContactValidated = Convert.ToDateTime(dr["DateContactValidated"]);
            }
            return objEntity;

        }
    }
}
