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
    public class ConfigurationDAL : BaseDAL
    {
        public int Save_Configuration(Configuration objConfiguration)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("ConfigurationId", objConfiguration.ConfigurationId));
            lstParameter.Add(new MySqlParameter("ConfigurationTypeId", objConfiguration.ConfigurationTypeId));

            lstParameter.Add(new MySqlParameter("DepartmentId", objConfiguration.DepartmentId));
            lstParameter.Add(new MySqlParameter("ConfigurationGuid", objConfiguration.ConfigurationGuid));

            lstParameter.Add(new MySqlParameter("Value", objConfiguration.Value));

            lstParameter.Add(new MySqlParameter("CreatedBy", objConfiguration.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objConfiguration.CreatedOn));
            lstParameter.Add(new MySqlParameter("IsActive", objConfiguration.IsActive));
            lstParameter.Add(new MySqlParameter("IsDelete", objConfiguration.IsDelete));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objConfiguration.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objConfiguration.ModifiedOn));


            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "configuration_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        //public int Update_Configuration(Configuration objConfiguration)
        //{
        //    DBHelper objDB = new DBHelper();
        //    List<MySqlParameter> lstParameter = new List<MySqlParameter>();

        //    lstParameter.Add(new MySqlParameter("U_ConfigurationId", objConfiguration.ConfigurationId));
        //    lstParameter.Add(new MySqlParameter("U_ConfigurationTypeId", objConfiguration.ConfigurationTypeId));
        //    lstParameter.Add(new MySqlParameter("U_Value", objConfiguration.Value));

        //    lstParameter.Add(new MySqlParameter("U_DepartmentId", objConfiguration.DepartmentId));
        //    lstParameter.Add(new MySqlParameter("U_ConfigurationGuid", objConfiguration.ConfigurationGuid));

        //    lstParameter.Add(new MySqlParameter("U_CreatedBy", objConfiguration.CreatedBy));
        //    lstParameter.Add(new MySqlParameter("U_CreatedOn", objConfiguration.CreatedOn));
        //    lstParameter.Add(new MySqlParameter("U_IsActive", objConfiguration.IsActive));
        //    lstParameter.Add(new MySqlParameter("U_IsDelete", objConfiguration.IsDelete));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedBy", objConfiguration.ModifiedBy));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedOn", objConfiguration.ModifiedOn));

        //    int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "configuration_Update", lstParameter.ToArray());
        //    return returnValue;
        //}

        public Configuration Get_Configuration_By_ConfigurationId(int ConfigurationId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_ConfigurationId", ConfigurationId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "configuration_Get_By_ConfigurationId", lstParameter.ToArray());

            Configuration objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);

            }
            return objEntity;
        }

        public Configuration Get_Configuration_By_ConfigurationTypeId(int ConfigurationTypeId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_ConfigurationTypeId", ConfigurationTypeId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "configuration_Get_By_ConfigurationTypeId", lstParameter.ToArray());

            Configuration objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);

            }
            return objEntity;
        }

        public List<Configuration> GetAll_Configuration()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "configuration_Get_All");
            List<Configuration> lstCountry = new List<Configuration>();
            Configuration objEntity = null;
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

        public List<Configuration> GetALL_Configuration_WithConfigurationType()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "configuration_GetALL_WithConfigType");
            List<Configuration> lstEntity = new List<Configuration>();
            Configuration objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                {
                    lstEntity.Add(objEntity);
                }
            }
            return lstEntity;
        }



        private Configuration FetchEntity(DataRow dr)
        {
            Configuration objEntity = new Configuration();

            if (dr.Table.Columns.Contains("ConfigurationId") && dr["ConfigurationId"] != DBNull.Value)
            {
                objEntity.ConfigurationId = Convert.ToInt32(dr["ConfigurationId"]);
            }
            if (dr.Table.Columns.Contains("ConfigurationTypeId") && dr["ConfigurationTypeId"] != DBNull.Value)
            {
                objEntity.ConfigurationTypeId = Convert.ToInt32(dr["ConfigurationTypeId"]);
            }

            if (dr.Table.Columns.Contains("DepartmentId") && dr["DepartmentId"] != DBNull.Value)
            {
                objEntity.DepartmentId = Convert.ToInt32(dr["DepartmentId"]);
            }


            if (dr.Table.Columns.Contains("Value") && dr["Value"] != DBNull.Value)
            {
                objEntity.Value = Convert.ToString(dr["Value"]);
            }
            if (dr.Table.Columns.Contains("ConfigurationGuid") && dr["ConfigurationGuid"] != DBNull.Value)
            {
                objEntity.ConfigurationGuid = Convert.ToString(dr["ConfigurationGuid"]);
            }


            if (dr.Table.Columns.Contains("IsActive") && dr["IsActive"] != DBNull.Value)
            {
                objEntity.IsActive = Convert.ToBoolean(dr["IsActive"]);
            }

            if (dr.Table.Columns.Contains("IsDelete") && dr["IsDelete"] != DBNull.Value)
            {
                objEntity.IsDelete = Convert.ToBoolean(dr["IsDelete"]);
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


            // Joined  Field For View Only

            if (dr.Table.Columns.Contains("DepartmentName") && dr["DepartmentName"] != DBNull.Value)
            {
                objEntity.DepartmentName = Convert.ToString(dr["DepartmentName"]);
            }
            if (dr.Table.Columns.Contains("ConfigurationType") && dr["ConfigurationType"] != DBNull.Value)
            {
                objEntity.ConfigurationType = Convert.ToString(dr["ConfigurationType"]);
            }
            //Get

            if (dr.Table.Columns.Contains("Category") && dr["Category"] != DBNull.Value)
            {
                objEntity.Category = Convert.ToString(dr["Category"]);
            }
            if (dr.Table.Columns.Contains("DataType") && dr["DataType"] != DBNull.Value)
            {
                objEntity.DataType = Convert.ToString(dr["DataType"]);
            }
            if (dr.Table.Columns.Contains("DefaultValue") && dr["DefaultValue"] != DBNull.Value)
            {
                objEntity.DefaultValue = Convert.ToString(dr["DefaultValue"]);
            }
            if (dr.Table.Columns.Contains("Description") && dr["Description"] != DBNull.Value)
            {
                objEntity.Description = Convert.ToString(dr["Description"]);
            }

            if (dr.Table.Columns.Contains("IsEnabled") && dr["IsEnabled"] != DBNull.Value)
            {
                objEntity.IsEnabled = Convert.ToBoolean(dr["IsEnabled"]);
            }

            if (dr.Table.Columns.Contains("IsEditable") && dr["IsEditable"] != DBNull.Value)
            {
                objEntity.IsEditable = Convert.ToBoolean(dr["IsEditable"]);
            }
            if (dr.Table.Columns.Contains("Setting") && dr["Setting"] != DBNull.Value)
            {
                objEntity.Setting = Convert.ToString(dr["Setting"]);
            }
            if (dr.Table.Columns.Contains("SupportsDoesNotApply") && dr["SupportsDoesNotApply"] != DBNull.Value)
            {
                objEntity.SupportsDoesNotApply = Convert.ToBoolean(dr["SupportsDoesNotApply"]);
            }
            if (dr.Table.Columns.Contains("ValidationMessage") && dr["ValidationMessage"] != DBNull.Value)
            {
                objEntity.ValidationMessage = Convert.ToString(dr["ValidationMessage"]);
            }
            if (dr.Table.Columns.Contains("ValidationRegEx") && dr["ValidationRegEx"] != DBNull.Value)
            {
                objEntity.ValidationRegEx = Convert.ToString(dr["ValidationRegEx"]);
            }




            return objEntity;
        }
    }
}
