using LAPP.ENTITY;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.DAL
{
    public class CountryDAL : BaseDAL
    {
        public int Save_Country(Country objCountry)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();


            lstParameter.Add(new MySqlParameter("CountryId", objCountry.CountryId));
            lstParameter.Add(new MySqlParameter("Code", objCountry.Code));

            lstParameter.Add(new MySqlParameter("CreatedBy", objCountry.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objCountry.CreatedOn));
            lstParameter.Add(new MySqlParameter("IsActive", objCountry.IsActive));
            lstParameter.Add(new MySqlParameter("IsDelete", objCountry.IsDelete));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objCountry.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objCountry.ModifiedOn));
            lstParameter.Add(new MySqlParameter("Name", objCountry.Name));
            lstParameter.Add(new MySqlParameter("StateLabel", objCountry.StateLabel));
            lstParameter.Add(new MySqlParameter("ZipLabel", objCountry.ZipLabel));
            lstParameter.Add(new MySqlParameter("ZipRegex", objCountry.ZipRegex));



            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "Country_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        public Country Get_Country_byID(int countryId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_CountryID", countryId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Country_get_ByID", lstParameter.ToArray());

            Country objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);

            }
            return objEntity;
        }
        public void Delete_Country_byID(int countryId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("U_CountryId", countryId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Country_Delete_ByID", lstParameter.ToArray());


        }
        //public int Update_Country(Country objCountry)
        //{
        //    DBHelper objDB = new DBHelper();
        //    List<MySqlParameter> lstParameter = new List<MySqlParameter>();
        //    lstParameter.Add(new MySqlParameter("U_CountryId", objCountry.CountryId));
        //    lstParameter.Add(new MySqlParameter("U_Code", objCountry.Code));

        //    lstParameter.Add(new MySqlParameter("U_CreatedBy", objCountry.CreatedBy));
        //    lstParameter.Add(new MySqlParameter("U_CreatedOn", objCountry.CreatedOn));
        //    lstParameter.Add(new MySqlParameter("U_IsActive", objCountry.IsActive));
        //    lstParameter.Add(new MySqlParameter("U_IsDelete", objCountry.IsDelete));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedBy", objCountry.ModifiedBy));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedOn", objCountry.ModifiedOn));
        //    lstParameter.Add(new MySqlParameter("U_Name", objCountry.Name));
        //    lstParameter.Add(new MySqlParameter("U_StateLabel", objCountry.StateLabel));
        //    lstParameter.Add(new MySqlParameter("U_ZipLabel", objCountry.ZipLabel));
        //    lstParameter.Add(new MySqlParameter("U_ZipRegex", objCountry.ZipRegex));

        //    int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "Country_Update", lstParameter.ToArray());
        //    return returnValue;
        //}
        public List<Country> GetAll_Country()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Country_get_all");
            List<Country> lstCountry = new List<Country>();
            Country objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                {
                    lstCountry.Add(objEntity);
                }
            }
            return lstCountry;
        }

        private Country FetchEntity(DataRow dr)
        {
            Country objEntity = new Country();

            if (dr.Table.Columns.Contains("CountryId") && dr["CountryId"] != DBNull.Value)
            {
                objEntity.CountryId = Convert.ToInt32(dr["CountryId"]);
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
            if (dr.Table.Columns.Contains("Name") && dr["Name"] != DBNull.Value)
            {
                objEntity.Name = Convert.ToString(dr["Name"]);
            }
            if (dr.Table.Columns.Contains("Code") && dr["Code"] != DBNull.Value)
            {
                objEntity.Code = Convert.ToString(dr["Code"]);
            }
            if (dr.Table.Columns.Contains("CountryId") && dr["CountryId"] != DBNull.Value)
            {
                objEntity.CountryId = Convert.ToInt32(dr["CountryId"]);
            }

            if (dr.Table.Columns.Contains("IsActive") && dr["IsActive"] != DBNull.Value)
            {
                objEntity.IsActive = Convert.ToBoolean(dr["IsActive"]);
            }
            if (dr.Table.Columns.Contains("ZipLabel") && dr["ZipLabel"] != DBNull.Value)
            {
                objEntity.ZipLabel = Convert.ToString(dr["ZipLabel"]);
            }
            if (dr.Table.Columns.Contains("ZipRegex") && dr["ZipRegex"] != DBNull.Value)
            {
                objEntity.ZipRegex = Convert.ToString(dr["ZipRegex"]);
            }
            if (dr.Table.Columns.Contains("StateLabel") && dr["StateLabel"] != DBNull.Value)
            {
                objEntity.StateLabel = Convert.ToString(dr["StateLabel"]);
            }
            return objEntity;
        }
    }
}
