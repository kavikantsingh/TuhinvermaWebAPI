using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class IndividualCitizenshipDAL : BaseDAL
    {
        public int Save_IndividualCitizenship(IndividualCitizenship objIndividualCitizenship)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualCitizenshipId", objIndividualCitizenship.IndividualCitizenshipId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualCitizenship.IndividualId));
            lstParameter.Add(new MySqlParameter("IsUSCitizen", objIndividualCitizenship.IsUSCitizen));
            lstParameter.Add(new MySqlParameter("IsPermanentResidence", objIndividualCitizenship.IsPermanentResidence));
            lstParameter.Add(new MySqlParameter("IsOnVisa", objIndividualCitizenship.IsOnVisa));
            lstParameter.Add(new MySqlParameter("VisaTypeId", objIndividualCitizenship.VisaTypeId));
            lstParameter.Add(new MySqlParameter("CitizenshipCountryId", objIndividualCitizenship.CitizenshipCountryId));
            lstParameter.Add(new MySqlParameter("DHSStatus", objIndividualCitizenship.DHSStatus));
            lstParameter.Add(new MySqlParameter("A", objIndividualCitizenship.A));
            lstParameter.Add(new MySqlParameter("VisaBeginDate", objIndividualCitizenship.VisaBeginDate));
            lstParameter.Add(new MySqlParameter("VisaEndDate", objIndividualCitizenship.VisaEndDate));

            lstParameter.Add(new MySqlParameter("IsActive", objIndividualCitizenship.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualCitizenship.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualCitizenship.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objIndividualCitizenship.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objIndividualCitizenship.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objIndividualCitizenship.ModifiedOn));

            lstParameter.Add(new MySqlParameter("IndividualCitizenshipGuid", objIndividualCitizenship.IndividualCitizenshipGuid));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "individualcitizenship_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<IndividualCitizenship> Get_All_IndividualCitizenship()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualCitizenship_GET_ALL");
            List<IndividualCitizenship> lstEntity = new List<IndividualCitizenship>();
            IndividualCitizenship objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public IndividualCitizenship Get_IndividualCitizenship_By_IndividualCitizenshipId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualCitizenshipId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualCitizenship_GET_BY_IndividualCitizenshipId", lstParameter.ToArray());
            IndividualCitizenship objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private IndividualCitizenship FetchEntity(DataRow dr)
        {
            IndividualCitizenship objEntity = new IndividualCitizenship();

            if (dr.Table.Columns.Contains("IndividualCitizenshipId") && dr["IndividualCitizenshipId"] != DBNull.Value)
            {
                objEntity.IndividualCitizenshipId = Convert.ToInt32(dr["IndividualCitizenshipId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("IsUSCitizen") && dr["IsUSCitizen"] != DBNull.Value)
            {
                objEntity.IsUSCitizen = Convert.ToBoolean(dr["IsUSCitizen"]);
            }
            if (dr.Table.Columns.Contains("IsPermanentResidence") && dr["IsPermanentResidence"] != DBNull.Value)
            {
                objEntity.IsPermanentResidence = Convert.ToBoolean(dr["IsPermanentResidence"]);
            }
            if (dr.Table.Columns.Contains("IsOnVisa") && dr["IsOnVisa"] != DBNull.Value)
            {
                objEntity.IsOnVisa = Convert.ToBoolean(dr["IsOnVisa"]);
            }
            if (dr.Table.Columns.Contains("VisaTypeId") && dr["VisaTypeId"] != DBNull.Value)
            {
                objEntity.VisaTypeId = Convert.ToInt32(dr["VisaTypeId"]);
            }
            if (dr.Table.Columns.Contains("CitizenshipCountryId") && dr["CitizenshipCountryId"] != DBNull.Value)
            {
                objEntity.CitizenshipCountryId = Convert.ToInt32(dr["CitizenshipCountryId"]);
            }
            if (dr.Table.Columns.Contains("DHSStatus") && dr["DHSStatus"] != DBNull.Value)
            {
                objEntity.DHSStatus = Convert.ToString(dr["DHSStatus"]);
            }
            if (dr.Table.Columns.Contains("A") && dr["A"] != DBNull.Value)
            {
                objEntity.A = Convert.ToString(dr["A"]);
            }
            if (dr.Table.Columns.Contains("VisaBeginDate") && dr["VisaBeginDate"] != DBNull.Value)
            {
                objEntity.VisaBeginDate = Convert.ToDateTime(dr["VisaBeginDate"]);
            }

            if (dr.Table.Columns.Contains("VisaEndDate") && dr["VisaEndDate"] != DBNull.Value)
            {
                objEntity.VisaEndDate = Convert.ToDateTime(dr["VisaEndDate"]);
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

            if (dr.Table.Columns.Contains("IndividualCitizenshipGuid") && dr["IndividualCitizenshipGuid"] != DBNull.Value)
            {
                objEntity.IndividualCitizenshipGuid = Convert.ToString(dr["IndividualCitizenshipGuid"]);
            }
            return objEntity;

        }
    }
}
