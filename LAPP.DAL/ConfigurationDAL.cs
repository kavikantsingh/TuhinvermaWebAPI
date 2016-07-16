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

        public List<LAPP_DeficiencyTemplate> Get_lapp_application_Deficiency_Template_By_Query_List(string Query)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.Text, Query);
            List<LAPP_DeficiencyTemplate> lstEntity = new List<LAPP_DeficiencyTemplate>();
            LAPP_DeficiencyTemplate objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchDeficiencyTemplateEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private LAPP_DeficiencyTemplate FetchDeficiencyTemplateEntity(DataRow dr)
        {
            LAPP_DeficiencyTemplate objEntity = new LAPP_DeficiencyTemplate();
            if (dr.Table.Columns.Contains("DeficiencyTemplateId") && dr["DeficiencyTemplateId"] != DBNull.Value)
            {
                objEntity.Deficiency_Template_ID = Convert.ToInt32(dr["DeficiencyTemplateId"]);
            }
            if (dr.Table.Columns.Contains("DeficiencyTemplateName") && dr["DeficiencyTemplateName"] != DBNull.Value)
            {
                objEntity.Deficiency_Template_Name = Convert.ToString(dr["DeficiencyTemplateName"]);
            }
            if (dr.Table.Columns.Contains("IsActive") && dr["IsActive"] != DBNull.Value)
            {
                objEntity.Is_Active = Convert.ToBoolean(dr["IsActive"]);
            }
            if (dr.Table.Columns.Contains("IsDeleted") && dr["IsDeleted"] != DBNull.Value)
            {
                objEntity.Is_Deleted = Convert.ToBoolean(dr["IsDeleted"]);
            }
            if (dr.Table.Columns.Contains("CreatedOn") && dr["CreatedOn"] != DBNull.Value)
            {
                objEntity.Created_On = Convert.ToDateTime(dr["CreatedOn"]);
            }
            if (dr.Table.Columns.Contains("CreatedBy") && dr["CreatedBy"] != DBNull.Value)
            {
                objEntity.Created_By = Convert.ToInt32(dr["CreatedBy"]);
            }
            if (dr.Table.Columns.Contains("ModifiedOn") && dr["ModifiedOn"] != DBNull.Value)
            {
                objEntity.Modified_On = Convert.ToDateTime(dr["ModifiedOn"]);
            }
            if (dr.Table.Columns.Contains("ModifiedBy") && dr["ModifiedBy"] != DBNull.Value)
            {
                objEntity.Modified_By = Convert.ToInt32(dr["ModifiedBy"]);
            }
            if (dr.Table.Columns.Contains("mastertransactionName") && dr["mastertransactionName"] != DBNull.Value)
            {
                objEntity.Name = Convert.ToString(dr["mastertransactionName"]);
            }
            if (dr.Table.Columns.Contains("mastertransactionid") && dr["mastertransactionid"] != DBNull.Value)
            {
                objEntity.Master_Transaction_Id = Convert.ToInt32(dr["mastertransactionid"]);
            }
            if (dr.Table.Columns.Contains("EndDate") && dr["EndDate"] != DBNull.Value)
            {
                objEntity.End_Date = Convert.ToDateTime(dr["EndDate"]);
            }
            if (dr.Table.Columns.Contains("DeficiencyTemplateSubject") && dr["DeficiencyTemplateSubject"] != DBNull.Value)
            {
                objEntity.Deficiency_Template_Subject = Convert.ToString(dr["DeficiencyTemplateSubject"]);
            }
            if (dr.Table.Columns.Contains("DeficiencyTemplateMessage") && dr["DeficiencyTemplateMessage"] != DBNull.Value)
            {
                objEntity.Deficiency_Template_Message = Convert.ToString(dr["DeficiencyTemplateMessage"]);
            }
            if (dr.Table.Columns.Contains("IsEditable") && dr["IsEditable"] != DBNull.Value)
            {
                objEntity.Is_Editable = Convert.ToBoolean(dr["IsEditable"]);
            }
            return objEntity;
        }

        public List<MasterTransaction> Get_All_LAPP_MasterTransaction()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "lapp_MasterTransaction_get_all");
            List<MasterTransaction> lstEntity = new List<MasterTransaction>();
            MasterTransaction objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchMasterTransactionEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private MasterTransaction FetchMasterTransactionEntity(DataRow dr)
        {
            MasterTransaction objEntity = new MasterTransaction();
            if (dr.Table.Columns.Contains("MasterTransactionId") && dr["MasterTransactionId"] != DBNull.Value)
            {
                objEntity.MasterTransactionId = Convert.ToInt32(dr["MasterTransactionId"]);
            }
            if (dr.Table.Columns.Contains("MasterTransactionName") && dr["MasterTransactionName"] != DBNull.Value)
            {
                objEntity.MasterTransactionName = Convert.ToString(dr["MasterTransactionName"]);
            }

            return objEntity;
        }

        public int SaveDeficiencyTemplate(LAPP_DeficiencyTemplate objlapp_deficiency_template)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("Deficiency_Template_Name", objlapp_deficiency_template.Deficiency_Template_Name));
            lstParameter.Add(new MySqlParameter("Deficiency_Template_Message", objlapp_deficiency_template.Deficiency_Template_Message));
            lstParameter.Add(new MySqlParameter("Deficiency_Template_Subject", objlapp_deficiency_template.Deficiency_Template_Subject));
            lstParameter.Add(new MySqlParameter("Master_Transaction_Id", objlapp_deficiency_template.Master_Transaction_Id));
            lstParameter.Add(new MySqlParameter("Is_Active", objlapp_deficiency_template.Is_Active));
            lstParameter.Add(new MySqlParameter("Is_Deleted", objlapp_deficiency_template.Is_Deleted));
            lstParameter.Add(new MySqlParameter("Created_On", objlapp_deficiency_template.Created_On));
            lstParameter.Add(new MySqlParameter("Created_By", objlapp_deficiency_template.Created_By));
            lstParameter.Add(new MySqlParameter("Modified_On", objlapp_deficiency_template.Modified_On));
            lstParameter.Add(new MySqlParameter("Modified_By", objlapp_deficiency_template.Modified_By));
            //MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            //returnParam.Direction = ParameterDirection.ReturnValue;
            //lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "lapp_deficiency_template_Save", lstParameter.ToArray());
            return returnValue;
        }

        public LAPP_DeficiencyTemplate GetDeficiencyTemplate(int G_Deficiency_Template_ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_Deficiency_Template_ID", G_Deficiency_Template_ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "lapp__deficiency_template_by_Deficiency_Template_ID", lstParameter.ToArray());
            LAPP_DeficiencyTemplate objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchDeficiencyTemplateEntity(dr);
            }
            return objEntity;
        }

        public int UpdateDeficiencyTemplate(LAPP_DeficiencyTemplate objlapp_deficiency_template)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("Deficiency_Template_ID", objlapp_deficiency_template.Deficiency_Template_ID));
            lstParameter.Add(new MySqlParameter("Deficiency_Template_Name", objlapp_deficiency_template.Deficiency_Template_Name));
            lstParameter.Add(new MySqlParameter("Deficiency_Template_Message", objlapp_deficiency_template.Deficiency_Template_Message));
            lstParameter.Add(new MySqlParameter("Deficiency_Template_Subject", objlapp_deficiency_template.Deficiency_Template_Subject));
            lstParameter.Add(new MySqlParameter("Master_Transaction_Id", objlapp_deficiency_template.Master_Transaction_Id));
            lstParameter.Add(new MySqlParameter("Is_Active", objlapp_deficiency_template.Is_Active));
            lstParameter.Add(new MySqlParameter("Is_Deleted", objlapp_deficiency_template.Is_Deleted));
            lstParameter.Add(new MySqlParameter("Created_On", objlapp_deficiency_template.Created_On));
            lstParameter.Add(new MySqlParameter("Created_By", objlapp_deficiency_template.Created_By));
            lstParameter.Add(new MySqlParameter("Modified_On", objlapp_deficiency_template.Modified_On));
            lstParameter.Add(new MySqlParameter("Modified_By", objlapp_deficiency_template.Modified_By));
            //MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            //returnParam.Direction = ParameterDirection.ReturnValue;
            //lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "lapp_deficiency_template_Update", lstParameter.ToArray());
            return returnValue;
        }

    }
}
