using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class IndividualNameDAL : BaseDAL
    {
        public int Save_IndividualName(IndividualName objIndividualName)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualNameId", objIndividualName.IndividualNameId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualName.IndividualId));
            lstParameter.Add(new MySqlParameter("FirstName", objIndividualName.FirstName));
            lstParameter.Add(new MySqlParameter("MiddleName", objIndividualName.MiddleName));
            lstParameter.Add(new MySqlParameter("LastName", objIndividualName.LastName));
            lstParameter.Add(new MySqlParameter("SuffixId", objIndividualName.SuffixId));
            lstParameter.Add(new MySqlParameter("IndividualNameStatusId", objIndividualName.IndividualNameStatusId));
            lstParameter.Add(new MySqlParameter("IndividualNameTypeId", objIndividualName.IndividualNameTypeId));

            lstParameter.Add(new MySqlParameter("IsActive", objIndividualName.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualName.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualName.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objIndividualName.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objIndividualName.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objIndividualName.ModifiedOn));


            lstParameter.Add(new MySqlParameter("IndividualNameGuid", objIndividualName.IndividualNameGuid));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "individualname_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<IndividualName> Get_All_IndividualName()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualname_Get_All", lstParameter.ToArray());

            List<IndividualName> lstEntity = new List<IndividualName>();
            IndividualName objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<IndividualName> Get_IndividualName_By_IndividualId(int IndividualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualname_Get_By_IndividualId", lstParameter.ToArray());
            List<IndividualName> lstEntity = new List<IndividualName>();
            IndividualName objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<IndividualName> Get_IndividualName_By_IndividualIdANDIndividualNameTypeId(int IndividualId, int IndividualNameTypeId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            lstParameter.Add(new MySqlParameter("G_IndividualNameTypeId", IndividualNameTypeId));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualname_Get_By_IndividualIdANDIndividualNameTyId", lstParameter.ToArray());
            List<IndividualName> lstEntity = new List<IndividualName>();
            IndividualName objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public IndividualName Get_IndividualName_By_IndividualNameId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualNameId", ID));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualname_Get_By_IndividualNameId", lstParameter.ToArray());
            IndividualName objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private IndividualName FetchEntity(DataRow dr)
        {
            IndividualName objEntity = new IndividualName();

            if (dr.Table.Columns.Contains("IndividualNameId") && dr["IndividualNameId"] != DBNull.Value)
            {
                objEntity.IndividualNameId = Convert.ToInt32(dr["IndividualNameId"]);
            }
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

            if (dr.Table.Columns.Contains("IndividualNameStatusId") && dr["IndividualNameStatusId"] != DBNull.Value)
            {
                objEntity.IndividualNameStatusId = Convert.ToInt32(dr["IndividualNameStatusId"]);
            }
            if (dr.Table.Columns.Contains("IndividualNameTypeId") && dr["IndividualNameTypeId"] != DBNull.Value)
            {
                objEntity.IndividualNameTypeId = Convert.ToInt32(dr["IndividualNameTypeId"]);
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

            if (dr.Table.Columns.Contains("IndividualNameGuid") && dr["IndividualNameGuid"] != DBNull.Value)
            {
                objEntity.IndividualNameGuid = Convert.ToString(dr["IndividualNameGuid"]);
            }


            return objEntity;

        }


    }
}
