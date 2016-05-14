using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class IndividualOtherNameDAL : BaseDAL
    {
        public int Save_IndividualOtherName(IndividualOtherName objIndividualOtherName)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualOtherNameId", objIndividualOtherName.IndividualOtherNameId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualOtherName.IndividualId));
            lstParameter.Add(new MySqlParameter("FirstName", objIndividualOtherName.FirstName));
            lstParameter.Add(new MySqlParameter("MiddleName", objIndividualOtherName.MiddleName));
            lstParameter.Add(new MySqlParameter("LastName", objIndividualOtherName.LastName));

            lstParameter.Add(new MySqlParameter("IsActive", objIndividualOtherName.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualOtherName.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualOtherName.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objIndividualOtherName.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objIndividualOtherName.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objIndividualOtherName.ModifiedOn));

            lstParameter.Add(new MySqlParameter("IndividualOtherNameGuid", objIndividualOtherName.IndividualOtherNameGuid));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "individualothername_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<IndividualOtherName> Get_All_IndividualOtherName()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualOtherName_Get_All");
            List<IndividualOtherName> lstEntity = new List<IndividualOtherName>();
            IndividualOtherName objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public IndividualOtherName Get_IndividualOtherName_By_IndividualOtherNameId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualOtherNameId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualOtherName_GET_BY_IndividualOtherNameId", lstParameter.ToArray());
            IndividualOtherName objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private IndividualOtherName FetchEntity(DataRow dr)
        {
            IndividualOtherName objEntity = new IndividualOtherName();

            if (dr.Table.Columns.Contains("IndividualOtherNameId") && dr["IndividualOtherNameId"] != DBNull.Value)
            {
                objEntity.IndividualOtherNameId = Convert.ToInt32(dr["IndividualOtherNameId"]);
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

            if (dr.Table.Columns.Contains("IndividualOtherNameGuid") && dr["IndividualOtherNameGuid"] != DBNull.Value)
            {
                objEntity.IndividualOtherNameGuid = Convert.ToString(dr["IndividualOtherNameGuid"]);
            }
            return objEntity;

        }
    }
}
