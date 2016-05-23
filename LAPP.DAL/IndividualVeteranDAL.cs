using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class IndividualVeteranDAL : BaseDAL
    {
        public int Save_IndividualVeteran(IndividualVeteran objIndividualVeteran)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualVeteranId", objIndividualVeteran.IndividualVeteranId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualVeteran.IndividualId));
            lstParameter.Add(new MySqlParameter("ServedInMilitary", objIndividualVeteran.ServedInMilitary));
            lstParameter.Add(new MySqlParameter("SpouseofActiveMilitaryMember", objIndividualVeteran.SpouseofActiveMilitaryMember));
            lstParameter.Add(new MySqlParameter("MilitaryOccupationSpeciality", objIndividualVeteran.MilitaryOccupationSpeciality));
            lstParameter.Add(new MySqlParameter("ServiceDateFrom", objIndividualVeteran.ServiceDateFrom));
            lstParameter.Add(new MySqlParameter("ServiceDateTo", objIndividualVeteran.ServiceDateTo));

            lstParameter.Add(new MySqlParameter("IsActive", objIndividualVeteran.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualVeteran.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualVeteran.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objIndividualVeteran.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objIndividualVeteran.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objIndividualVeteran.ModifiedOn));

            lstParameter.Add(new MySqlParameter("IndividualVeteranGuid", objIndividualVeteran.IndividualVeteranGuid));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "individualveteran_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public IndividualVeteran Get_IndividualVeteran_By_IndividualId(int IndividualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
          
            lstParameter.Add(new MySqlParameter("_IndividualId", IndividualId));
            lstParameter.Add(new MySqlParameter("_IndividualVeteranGuid", Guid.NewGuid().ToString()));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualveteran_Getby_IndividualId", lstParameter.ToArray());
            IndividualVeteran objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<IndividualVeteran> Get_All_IndividualVeteran()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualVeteran_Get_All");
            List<IndividualVeteran> lstEntity = new List<IndividualVeteran>();
            IndividualVeteran objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public IndividualVeteran Get_IndividualVeteran_By_IndividualVeteranId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualVeteranId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualVeteran_GET_BY_IndividualVeteranId", lstParameter.ToArray());
            IndividualVeteran objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private IndividualVeteran FetchEntity(DataRow dr)
        {
            IndividualVeteran objEntity = new IndividualVeteran();

            if (dr.Table.Columns.Contains("IndividualVeteranId") && dr["IndividualVeteranId"] != DBNull.Value)
            {
                objEntity.IndividualVeteranId = Convert.ToInt32(dr["IndividualVeteranId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("ServedInMilitary") && dr["ServedInMilitary"] != DBNull.Value)
            {
                objEntity.ServedInMilitary = Convert.ToBoolean(dr["ServedInMilitary"]);
            }
            if (dr.Table.Columns.Contains("SpouseofActiveMilitaryMember") && dr["SpouseofActiveMilitaryMember"] != DBNull.Value)
            {
                objEntity.SpouseofActiveMilitaryMember = Convert.ToBoolean(dr["SpouseofActiveMilitaryMember"]);
            }
            if (dr.Table.Columns.Contains("MilitaryOccupationSpeciality") && dr["MilitaryOccupationSpeciality"] != DBNull.Value)
            {
                objEntity.MilitaryOccupationSpeciality = Convert.ToString(dr["MilitaryOccupationSpeciality"]);
            }
            if (dr.Table.Columns.Contains("ServiceDateFrom") && dr["ServiceDateFrom"] != DBNull.Value)
            {
                objEntity.ServiceDateFrom = Convert.ToDateTime(dr["ServiceDateFrom"]);
            }
            if (dr.Table.Columns.Contains("ServiceDateTo") && dr["ServiceDateTo"] != DBNull.Value)
            {
                objEntity.ServiceDateTo = Convert.ToDateTime(dr["ServiceDateTo"]);
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

            if (dr.Table.Columns.Contains("IndividualVeteranGuid") && dr["IndividualVeteranGuid"] != DBNull.Value)
            {
                objEntity.IndividualVeteranGuid = Convert.ToString(dr["IndividualVeteranGuid"]);
            }
            return objEntity;

        }
    }
}
