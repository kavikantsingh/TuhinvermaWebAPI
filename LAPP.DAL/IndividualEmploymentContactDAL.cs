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
    public class IndividualEmploymentContactDAL : BaseDAL
    {
        public int Save_IndividualEmploymentContact(IndividualEmploymentContact objIndividualEmploymentContact)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualEmploymentContactId", objIndividualEmploymentContact.IndividualEmploymentContactId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualEmploymentContact.IndividualId));
            lstParameter.Add(new MySqlParameter("ContactId", objIndividualEmploymentContact.ContactId));
            lstParameter.Add(new MySqlParameter("IndividualEmploymentId", objIndividualEmploymentContact.IndividualEmploymentId));
            lstParameter.Add(new MySqlParameter("ContactTypeId", objIndividualEmploymentContact.ContactTypeId));
            lstParameter.Add(new MySqlParameter("BeginDate", objIndividualEmploymentContact.BeginDate));
            lstParameter.Add(new MySqlParameter("EndDate", objIndividualEmploymentContact.EndDate));
            lstParameter.Add(new MySqlParameter("IsPreferredContact", objIndividualEmploymentContact.IsPreferredContact));
            lstParameter.Add(new MySqlParameter("IsMobile", objIndividualEmploymentContact.IsMobile));
            lstParameter.Add(new MySqlParameter("IsActive", objIndividualEmploymentContact.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualEmploymentContact.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualEmploymentContact.CreatedBy));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objIndividualEmploymentContact.ModifiedBy));
            lstParameter.Add(new MySqlParameter("IndividualEmploymentContactGuid", objIndividualEmploymentContact.IndividualEmploymentContactGuid));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "individualemploymentcontact_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<IndividualEmploymentContact> Get_All_IndividualEmploymentContact()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualemploymentcontact_Get_All");
            List<IndividualEmploymentContact> lstEntity = new List<IndividualEmploymentContact>();
            IndividualEmploymentContact objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public IndividualEmploymentContact Get_IndividualEmploymentContact_By_IndividualEmploymentContactId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualEmploymentContactId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualemploymentcontact_Get_By_IndividualEmploymentContactId", lstParameter.ToArray());
            IndividualEmploymentContact objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<IndividualEmploymentContact> Get_IndividualEmploymentContact_By_IndividualEmploymentId(int IndividualEmploymentId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualEmploymentId", IndividualEmploymentId));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualemploymentcontact_Get_By_IndividualEmploymentId", lstParameter.ToArray());
            List<IndividualEmploymentContact> lstEntity = new List<IndividualEmploymentContact>();
            IndividualEmploymentContact objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private IndividualEmploymentContact FetchEntity(DataRow dr)
        {
            IndividualEmploymentContact objEntity = new IndividualEmploymentContact();
            if (dr.Table.Columns.Contains("IndividualEmploymentContactId") && dr["IndividualEmploymentContactId"] != DBNull.Value)
            {
                objEntity.IndividualEmploymentContactId = Convert.ToInt32(dr["IndividualEmploymentContactId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("ContactId") && dr["ContactId"] != DBNull.Value)
            {
                objEntity.ContactId = Convert.ToInt32(dr["ContactId"]);
            }
            if (dr.Table.Columns.Contains("IndividualEmploymentId") && dr["IndividualEmploymentId"] != DBNull.Value)
            {
                objEntity.IndividualEmploymentId = Convert.ToInt32(dr["IndividualEmploymentId"]);
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
            if (dr.Table.Columns.Contains("IndividualEmploymentContactGuid") && dr["IndividualEmploymentContactGuid"] != DBNull.Value)
            {
                objEntity.IndividualEmploymentContactGuid =  dr["IndividualEmploymentContactGuid"].ToString();
            }
            return objEntity;

        }
    }
}
