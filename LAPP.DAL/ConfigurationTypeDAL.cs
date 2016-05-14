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
    public class ConfigurationTypeDAL : BaseDAL
    {
        public int Save_ConfigurationType(ConfigurationType objConfigurationType)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("ConfigurationTypeId", objConfigurationType.ConfigurationTypeId));
            lstParameter.Add(new MySqlParameter("Category", objConfigurationType.Category));
            lstParameter.Add(new MySqlParameter("DataType", objConfigurationType.DataType));
            lstParameter.Add(new MySqlParameter("DefaultValue", objConfigurationType.DefaultValue));
            lstParameter.Add(new MySqlParameter("Description", objConfigurationType.Description));
            lstParameter.Add(new MySqlParameter("IsEditable", objConfigurationType.IsEditable));
            lstParameter.Add(new MySqlParameter("Setting", objConfigurationType.Setting));
            lstParameter.Add(new MySqlParameter("SupportsDoesNotApply", objConfigurationType.SupportsDoesNotApply));
            lstParameter.Add(new MySqlParameter("ValidationMessage", objConfigurationType.ValidationMessage));
            lstParameter.Add(new MySqlParameter("ValidationRegEx", objConfigurationType.ValidationRegEx));

            lstParameter.Add(new MySqlParameter("IsEnabled", objConfigurationType.IsEnabled));
            lstParameter.Add(new MySqlParameter("ConfigurationTypeGuid", objConfigurationType.ConfigurationTypeGuid));


            lstParameter.Add(new MySqlParameter("CreatedBy", objConfigurationType.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objConfigurationType.CreatedOn));
            lstParameter.Add(new MySqlParameter("IsActive", objConfigurationType.IsActive));
            lstParameter.Add(new MySqlParameter("IsDelete", objConfigurationType.IsDelete));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objConfigurationType.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objConfigurationType.ModifiedOn));


            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "configurationtype_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        //public int Update_ConfigurationType(ConfigurationType objConfigurationType)
        //{
        //    DBHelper objDB = new DBHelper();
        //    List<MySqlParameter> lstParameter = new List<MySqlParameter>();

        //    lstParameter.Add(new MySqlParameter("U_ConfigurationTypeId", objConfigurationType.ConfigurationTypeId));
        //    lstParameter.Add(new MySqlParameter("U_Category", objConfigurationType.Category));
        //    lstParameter.Add(new MySqlParameter("U_DataType", objConfigurationType.DataType));
        //    lstParameter.Add(new MySqlParameter("U_DefaultValue", objConfigurationType.DefaultValue));
        //    lstParameter.Add(new MySqlParameter("U_Description", objConfigurationType.Description));
        //    lstParameter.Add(new MySqlParameter("U_IsEditable", objConfigurationType.IsEditable));
        //    lstParameter.Add(new MySqlParameter("U_Setting", objConfigurationType.Setting));
        //    lstParameter.Add(new MySqlParameter("U_SupportsDoesNotApply", objConfigurationType.SupportsDoesNotApply));
        //    lstParameter.Add(new MySqlParameter("U_ValidationMessage", objConfigurationType.ValidationMessage));
        //    lstParameter.Add(new MySqlParameter("U_ValidationRegEx", objConfigurationType.ValidationRegEx));

        //    lstParameter.Add(new MySqlParameter("U_IsEnabled", objConfigurationType.IsEnabled));
        //    lstParameter.Add(new MySqlParameter("U_ConfigurationTypeGuid", objConfigurationType.ConfigurationTypeGuid));

        //    lstParameter.Add(new MySqlParameter("U_CreatedBy", objConfigurationType.CreatedBy));
        //    lstParameter.Add(new MySqlParameter("U_CreatedOn", objConfigurationType.CreatedOn));
        //    lstParameter.Add(new MySqlParameter("U_IsActive", objConfigurationType.IsActive));
        //    lstParameter.Add(new MySqlParameter("U_IsDelete", objConfigurationType.IsDelete));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedBy", objConfigurationType.ModifiedBy));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedOn", objConfigurationType.ModifiedOn));

        //    int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "configurationtype_Update", lstParameter.ToArray());
        //    return returnValue;
        //}

        public ConfigurationType Get_ConfigurationType_By_ConfigurationTypeId(int ConfigurationTypeId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_ConfigurationTypeId", ConfigurationTypeId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "configurationtype_Get_By_ConfigurationTypeId", lstParameter.ToArray());

            ConfigurationType objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);

            }
            return objEntity;
        }

        public List<ConfigurationType> Get_Configuration_By_Settings(string Setting)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_Setting", Setting));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "configuration_Get_By_Settings", lstParameter.ToArray());
            List<ConfigurationType> lstCountry = new List<ConfigurationType>();
            ConfigurationType objEntity = null;
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
        public List<ConfigurationType> GetAll_ConfigurationType()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "configurationtype_Get_All");
            List<ConfigurationType> lstCountry = new List<ConfigurationType>();
            ConfigurationType objEntity = null;
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

        private ConfigurationType FetchEntity(DataRow dr)
        {
            ConfigurationType objEntity = new ConfigurationType();

            if (dr.Table.Columns.Contains("ConfigurationTypeId") && dr["ConfigurationTypeId"] != DBNull.Value)
            {
                objEntity.ConfigurationTypeId = Convert.ToInt32(dr["ConfigurationTypeId"]);
            }
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


            //Used for view only

            if (dr.Table.Columns.Contains("Value") && dr["Value"] != DBNull.Value)
            {
                objEntity.Value = Convert.ToString(dr["Value"]);
            }

            return objEntity;
        }
    }
}
