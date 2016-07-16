using LAPP.BAL;
using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.LOGING;
using LAPP.WS.App_Helper;
using LAPP.WS.App_Helper.Common;
using LAPP.WS.ValidateController.Backoffice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace LAPP.WS.Controllers.Backoffice
{
    public class ConfigurationController : ApiController
    {

        #region Configuration

        /// <summary>
        /// Looks up all data by Key For Configuration.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("GET")]
        [ActionName("ConfigurationGetAll")]
        public ConfigurationResponse ConfigurationGetAll(string Key)
        {

            LogingHelper.SaveAuditInfo(Key);

            ConfigurationResponse objResponse = new ConfigurationResponse();
            ConfigurationBAL objBAL = new ConfigurationBAL();
            Configuration objEntity = new Configuration();
            List<Configuration> lstConfiguration = new List<Configuration>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ConfigurationList = null;
                    return objResponse;

                }

                lstConfiguration = objBAL.GetALL_Configuration_WithConfigurationType();
                if (lstConfiguration != null && lstConfiguration.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    List<ConfigurationGet> lstConfigurationSelected = lstConfiguration.Select(ConfigurationL => new ConfigurationGet
                    {
                        ConfigurationTypeId = ConfigurationL.ConfigurationTypeId,
                        ConfigurationId = ConfigurationL.ConfigurationId,
                        Setting = ConfigurationL.Setting,
                        Description = ConfigurationL.Description,
                        DataType = ConfigurationL.DataType,
                        Category = ConfigurationL.Category,
                        ValidationRegEx = ConfigurationL.ValidationRegEx,
                        ValidationMessage = ConfigurationL.ValidationMessage,
                        DefaultValue = ConfigurationL.DefaultValue,
                        SupportsDoesNotApply = ConfigurationL.SupportsDoesNotApply,
                        IsEnabled = ConfigurationL.IsEnabled,
                        IsEditable = ConfigurationL.IsEditable,
                        IsActive = ConfigurationL.IsActive,
                        Value = ConfigurationL.Value
                    }).ToList();


                    objResponse.ConfigurationList = lstConfigurationSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ConfigurationList = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ConfigurationGetAll", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ConfigurationList = null;

            }
            return objResponse;


        }


        /// <summary>
        /// Get Method to get Configuration by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ID">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("ConfigurationGetBYID")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ConfigurationResponse ConfigurationGetBYID(string Key, int ID)
        {
            LogingHelper.SaveAuditInfo(Key);

            ConfigurationResponse objResponse = new ConfigurationResponse();
            ConfigurationBAL objBAL = new ConfigurationBAL();
            Configuration objEntity = new Configuration();
            List<Configuration> lstConfiguration = new List<Configuration>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ConfigurationList = null;
                    return objResponse;
                }

                objEntity = objBAL.Get_Configuration_By_ConfigurationId(ID);
                if (objEntity != null)
                {
                    lstConfiguration.Add(objEntity);

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    List<ConfigurationGet> lstConfigurationSelected = lstConfiguration.Select(ConfigurationL => new ConfigurationGet
                    {
                        ConfigurationId = ConfigurationL.ConfigurationId,
                        ConfigurationTypeId = ConfigurationL.ConfigurationTypeId,
                        ConfigurationType = ConfigurationL.ConfigurationType,
                        DepartmentId = ConfigurationL.DepartmentId,
                        DepartmentName = ConfigurationL.DepartmentName,
                        Value = ConfigurationL.Value,
                        IsActive = ConfigurationL.IsActive
                    }).ToList();


                    objResponse.ConfigurationList = lstConfigurationSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ConfigurationList = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ConfigurationGetBYID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.ConfigurationList = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Get Method to get ConfigurationType by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="Setting">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("ConfigurationGetbySettings")]
        public ConfigurationTypeResponse ConfigurationGetbySettings(string Key, string Setting)
        {
            LogingHelper.SaveAuditInfo(Key);

            ConfigurationTypeResponse objResponse = new ConfigurationTypeResponse();
            ConfigurationTypeBAL objBAL = new ConfigurationTypeBAL();
            ConfigurationType objEntity = new ConfigurationType();
            List<ConfigurationType> lstConfigurationType = new List<ConfigurationType>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ConfigurationList = null;
                    return objResponse;
                }

                lstConfigurationType = objBAL.Get_Configuration_By_Settings(Setting.ToLower());
                if (lstConfigurationType != null && lstConfigurationType.Count > 0)
                {

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    List<ConfigurationTypeGet> lstConfigurationTypeSelected = lstConfigurationType.Select(Configuration => new ConfigurationTypeGet
                    {
                        ConfigurationTypeId = Configuration.ConfigurationTypeId,
                        Setting = Configuration.Setting,
                        Description = Configuration.Description,
                        DataType = Configuration.DataType,
                        Category = Configuration.Category,
                        ValidationRegEx = Configuration.ValidationRegEx,
                        ValidationMessage = Configuration.ValidationMessage,
                        DefaultValue = Configuration.DefaultValue,
                        SupportsDoesNotApply = Configuration.SupportsDoesNotApply,
                        IsEnabled = Configuration.IsEnabled,
                        IsEditable = Configuration.IsEditable,
                        IsActive = Configuration.IsActive,
                        Value = Configuration.Value
                    }).ToList();

                    objResponse.ConfigurationList = lstConfigurationTypeSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Message = "No record found.";
                    objResponse.ConfigurationList = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ConfigurationTypeGetBYID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.ConfigurationList = null;

            }
            return objResponse;
        }




        /// <summary>
        ///  Method to Search Configuration by key and objConfigurationSearch.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="objConfigurationSearch">Record ID.</param>
        [AcceptVerbs("POST")]
        [ActionName("ConfigurationSearch")]
        public ConfigurationSearchResponse ConfigurationSearch(string Key, ConfigurationSearch objConfigurationSearch)
        {
            LogingHelper.SaveAuditInfo(Key);

            ConfigurationSearchResponse objResponse = new ConfigurationSearchResponse();
            ConfigurationBAL objBAL = new ConfigurationBAL();
            Configuration objEntity = new Configuration();
            List<Configuration> lstConfiguration = new List<Configuration>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.Configuration = null;
                    return objResponse;
                }

                lstConfiguration = objBAL.GetAll_Configuration();
                if (lstConfiguration != null && lstConfiguration.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    List<ConfigurationGet> lstConfigurationSelected = lstConfiguration.Select(ConfigurationL => new ConfigurationGet
                    {
                        ConfigurationId = ConfigurationL.ConfigurationId,
                        ConfigurationTypeId = ConfigurationL.ConfigurationTypeId,
                        ConfigurationType = ConfigurationL.ConfigurationType,
                        DepartmentId = ConfigurationL.DepartmentId,
                        DepartmentName = ConfigurationL.DepartmentName,
                        Value = ConfigurationL.Value,
                        IsActive = ConfigurationL.IsActive
                    }).ToList();



                    objResponse.Configuration = lstConfigurationSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Configuration = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ConfigurationGetBYID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Configuration = null;

            }
            return objResponse;
        }



        /// <summary>
        /// Save or Update the data For Configuration
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objConfiguration">Object of Configuration</param>
        [AcceptVerbs("POST")]
        [ActionName("ConfigurationSave")]
        public ConfigurationRequestResponse ConfigurationSave(string Key, Configuration objConfiguration)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo(Key);

            ConfigurationRequestResponse objResponse = new ConfigurationRequestResponse();
            ConfigurationBAL objBAL = new ConfigurationBAL();
            Configuration objEntity = new Configuration();
            List<Configuration> lstEntity = new List<Configuration>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.Configuration = null;
                return objResponse;
            }

            string ValidationResponse = ConfigurationValidation.ValidateConfigurationObject(objConfiguration);

            if (!string.IsNullOrEmpty(ValidationResponse))
            {


                objResponse.Message = "Validation Error";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                objResponse.ResponseReason = ValidationResponse;
                return objResponse;

            }

            try
            {
                if (objConfiguration.ConfigurationId > 0)
                {
                    objEntity = objBAL.Get_Configuration_By_ConfigurationId(objConfiguration.ConfigurationId);
                    if (objEntity != null)
                    {
                        objConfiguration.ModifiedOn = DateTime.Now;
                        objConfiguration.ModifiedBy = CreatedOrMoifiy;
                        //objBAL.Update_Configuration(objConfiguration);
                        objResponse.Message = Messages.UpdateSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }
                }
                else
                {
                    objConfiguration.ConfigurationGuid = Guid.NewGuid().ToString();
                    objConfiguration.CreatedOn = DateTime.Now;
                    objConfiguration.CreatedBy = CreatedOrMoifiy;

                    //objConfiguration.ModifiedOn = null;
                    objConfiguration.ConfigurationId = objBAL.Save_Configuration(objConfiguration);
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                }

                lstEntity.Add(objConfiguration);
                objResponse.Status = true;

                objResponse.Configuration = lstEntity;

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ConfigurationSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.Configuration = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }


        #endregion

        #region ConfigurationType

        /// <summary>
        /// Looks up all data by Key For ConfigurationType.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("GET")]
        [ActionName("ConfigurationTypeGetAll")]
        public ConfigurationTypeResponse ConfigurationTypeGetAll(string Key)
        {

            LogingHelper.SaveAuditInfo(Key);

            ConfigurationTypeResponse objResponse = new ConfigurationTypeResponse();
            ConfigurationTypeBAL objBAL = new ConfigurationTypeBAL();
            ConfigurationType objEntity = new ConfigurationType();
            List<ConfigurationType> lstConfigurationType = new List<ConfigurationType>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ConfigurationList = null;
                    return objResponse;
                }
                lstConfigurationType = objBAL.GetAll_ConfigurationType();
                if (lstConfigurationType != null && lstConfigurationType.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    List<ConfigurationTypeGet> lstConfigurationTypeSelected = lstConfigurationType.Select(Configuration => new ConfigurationTypeGet
                    {
                        ConfigurationTypeId = Configuration.ConfigurationTypeId,
                        Setting = Configuration.Setting,
                        Description = Configuration.Description,
                        DataType = Configuration.DataType,
                        Category = Configuration.Category,
                        ValidationRegEx = Configuration.ValidationRegEx,
                        ValidationMessage = Configuration.ValidationMessage,
                        DefaultValue = Configuration.DefaultValue,
                        SupportsDoesNotApply = Configuration.SupportsDoesNotApply,
                        IsEnabled = Configuration.IsEnabled,
                        IsEditable = Configuration.IsEditable,
                        IsActive = Configuration.IsActive
                    }).ToList();


                    objResponse.ConfigurationList = lstConfigurationTypeSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ConfigurationList = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ConfigurationTypeGetAll", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ConfigurationList = null;

            }
            return objResponse;


        }


        /// <summary>
        /// Get Method to get ConfigurationType by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ID">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("ConfigurationTypeGetBYID")]
        public ConfigurationTypeResponse ConfigurationTypeGetBYID(string Key, int ID)
        {
            LogingHelper.SaveAuditInfo(Key);

            ConfigurationTypeResponse objResponse = new ConfigurationTypeResponse();
            ConfigurationTypeBAL objBAL = new ConfigurationTypeBAL();
            ConfigurationType objEntity = new ConfigurationType();
            List<ConfigurationType> lstConfigurationType = new List<ConfigurationType>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ConfigurationList = null;
                    return objResponse;
                }

                objEntity = objBAL.Get_ConfigurationType_By_ConfigurationTypeId(ID);
                if (objEntity != null)
                {
                    lstConfigurationType.Add(objEntity);

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    List<ConfigurationTypeGet> lstConfigurationTypeSelected = lstConfigurationType.Select(Configuration => new ConfigurationTypeGet
                    {
                        ConfigurationTypeId = Configuration.ConfigurationTypeId,
                        Setting = Configuration.Setting,
                        Description = Configuration.Description,
                        DataType = Configuration.DataType,
                        Category = Configuration.Category,
                        ValidationRegEx = Configuration.ValidationRegEx,
                        ValidationMessage = Configuration.ValidationMessage,
                        DefaultValue = Configuration.DefaultValue,
                        SupportsDoesNotApply = Configuration.SupportsDoesNotApply,
                        IsEnabled = Configuration.IsEnabled,
                        IsEditable = Configuration.IsEditable,
                        IsActive = Configuration.IsActive
                    }).ToList();

                    objResponse.ConfigurationList = lstConfigurationTypeSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Message = "No record found.";
                    objResponse.ConfigurationList = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ConfigurationTypeGetBYID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.ConfigurationList = null;

            }
            return objResponse;
        }



        /// <summary>
        /// Save or Update the data For ConfigurationType
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objConfigurationType">Object of ConfigurationType</param>
        [AcceptVerbs("POST")]
        [ActionName("ConfigurationTypeSave")]
        public ConfigurationTypeRequestResponse ConfigurationTypeSave(string Key, ConfigurationType objConfigurationType)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;
            LogingHelper.SaveAuditInfo(Key);

            ConfigurationTypeRequestResponse objResponse = new ConfigurationTypeRequestResponse();
            ConfigurationTypeBAL objBAL = new ConfigurationTypeBAL();
            ConfigurationType objEntity = new ConfigurationType();
            List<ConfigurationType> lstEntity = new List<ConfigurationType>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.ConfigurationType = null;
                return objResponse;
            }

            string ValidationResponse = ConfigurationValidation.ValidateConfigurationTypeObject(objConfigurationType);

            if (!string.IsNullOrEmpty(ValidationResponse))
            {
                objResponse.Message = "Validation Error";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                objResponse.ResponseReason = ValidationResponse;
                return objResponse;

            }

            try
            {
                if (objConfigurationType.ConfigurationTypeId > 0)
                {
                    objEntity = objBAL.Get_ConfigurationType_By_ConfigurationTypeId(objConfigurationType.ConfigurationTypeId);
                    if (objEntity != null)
                    {
                        objConfigurationType.ModifiedOn = DateTime.Now;
                        objConfigurationType.ModifiedBy = CreatedOrMoifiy;

                        //objBAL.Update_ConfigurationType(objConfigurationType);
                        objResponse.Message = Messages.UpdateSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }
                }
                else
                {

                    objConfigurationType.ConfigurationTypeGuid = Guid.NewGuid().ToString();
                    objConfigurationType.CreatedOn = DateTime.Now;
                    objConfigurationType.CreatedBy = CreatedOrMoifiy;

                    //objConfigurationType.ModifiedOn = null;
                    objConfigurationType.ConfigurationTypeId = objBAL.Save_ConfigurationType(objConfigurationType);
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                }

                lstEntity.Add(objConfigurationType);
                objResponse.Status = true;

                objResponse.ConfigurationType = lstEntity;

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ConfigurationTypeSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Message = ex.Message;
                objResponse.ConfigurationType = null;
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");

            }
            return objResponse;
        }




        #endregion

        /// <summary>
        /// DeficiencyTemplateResponseGet all data.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("POST")]
        public DeficiencyTemplateResponseGet GetDeficiencyTemplate(string Key, DeficiencyTemplateSearch search)
        {
            
            LogingHelper.SaveAuditInfo(Key);

            DeficiencyTemplateResponseGet objResponse = new DeficiencyTemplateResponseGet();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.ResponseReason = null;
                return objResponse;
            }

            ConfigurationBAL objConfigurationBAL = new ConfigurationBAL();
            

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.DeficiencyTemplateResponseList = null;
                    return objResponse;
                }

                try
                {
                    objResponse.Message = "";
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";
                    string Filter = "";
                    if (search.IsSearch)
                    {
                        if (search.MasterTransactionId != "-1")
                        {
                            Filter = Filter + " and dr.mastertransactionid=" + Convert.ToInt32(search.MasterTransactionId);
                        }
                        if (search.DeficiencyTemplateName != "")
                        {
                            Filter = Filter + " and dr.DeficiencyTemplateName Like '" + search.DeficiencyTemplateName + "%'";
                        }
                        if (search.IsActive)
                        {
                            Filter = Filter + " and dr.IsActive=1";
                        }
                    }
                    string Query = "SELECT *,f.mastertransactionName FROM deficiencytemplate dr JOIN mastertransaction f ON dr.mastertransactionid=f.mastertransactionid WHERE dr.IsDeleted=0 " + Filter + " ORDER BY dr.mastertransactionid,dr.CreatedOn DESC";

                    List<LAPP_DeficiencyTemplate> lstDeficiencyTemplate = objConfigurationBAL.Get_lapp_application_Deficiency_Template_By_Query_List(Query);
                    objResponse.DeficiencyTemplateResponseList = lstDeficiencyTemplate;

                    return objResponse;

                }
                catch (Exception ex)
                {
                    LogingHelper.SaveExceptionInfo(Key, ex, "ValidateIndividual", ENTITY.Enumeration.eSeverity.Error);

                    objResponse.Status = false;
                    objResponse.Message = ex.Message;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                    objResponse.DeficiencyTemplateResponseList = null;

                }


            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ValidateIndividual", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.DeficiencyTemplateResponseList = null;

            }
            return objResponse;
        }

        /// <summary>
        /// DeficiencyTemplateResponseGet all data.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("GET")]
        public MasterTransactionResponse GetAllMasterTransaction(string Key)
        {
            LogingHelper.SaveAuditInfo(Key);

            MasterTransactionResponse objResponse = new MasterTransactionResponse();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.ResponseReason = null;
                return objResponse;
            }

            ConfigurationBAL objConfigurationBAL = new ConfigurationBAL();


            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.MasterTransactionList = null;
                    return objResponse;
                }

                try
                {
                    objResponse.Message = "";
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";

                    List<MasterTransaction> lstMasterTransaction = objConfigurationBAL.Get_All_MasterTransaction();
                    objResponse.MasterTransactionList = lstMasterTransaction;

                    return objResponse;

                }
                catch (Exception ex)
                {
                    LogingHelper.SaveExceptionInfo(Key, ex, "ValidateIndividual", ENTITY.Enumeration.eSeverity.Error);

                    objResponse.Status = false;
                    objResponse.Message = ex.Message;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                    objResponse.MasterTransactionList = null;

                }


            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ValidateIndividual", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.MasterTransactionList = null;

            }
            return objResponse;
        }

        /// <summary>
        /// Save the data For Configuration
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objlapp_deficiency_template">Object of Configuration</param>
        [AcceptVerbs("POST")]
        public DeficiencyTemplateResponseGet SaveDeficiencyTemplate(string Key, SaveDeficiencyTemplate objlapp_deficiency_template)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo(Key);

            DeficiencyTemplateResponseGet objResponse = new DeficiencyTemplateResponseGet();
            ConfigurationBAL objBAL = new ConfigurationBAL();
            LAPP_DeficiencyTemplate objEntity = new LAPP_DeficiencyTemplate();
            List<Configuration> lstEntity = new List<Configuration>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.DeficiencyTemplateResponseList = null;
                return objResponse;
            }
            
            try
            {
               
                if (objlapp_deficiency_template.Deficiency_Template_ID > 0)
                {
                    objEntity = objBAL.GetDeficiencyTemplate(objlapp_deficiency_template.Deficiency_Template_ID);
                    if (objEntity != null)
                    {
                        objEntity.Deficiency_Template_ID = objlapp_deficiency_template.Deficiency_Template_ID;
                        objEntity.Deficiency_Template_Name = objlapp_deficiency_template.Deficiency_Template_Name;
                        objEntity.Deficiency_Template_Message = objlapp_deficiency_template.Deficiency_Template_Message;
                        objEntity.Deficiency_Template_Subject = objlapp_deficiency_template.Deficiency_Template_Subject;
                        objEntity.Master_Transaction_Id = objlapp_deficiency_template.Master_Transaction_Id;
                        objEntity.Is_Active = objlapp_deficiency_template.Is_Active;
                        objEntity.Is_Deleted = objlapp_deficiency_template.Is_Deleted;

                        objEntity.Modified_On = DateTime.Now;
                        objEntity.Modified_By = CreatedOrMoifiy;
                        objEntity.Created_On = DateTime.Now;
                        objEntity.Created_By = CreatedOrMoifiy;

                        int i = objBAL.UpdateDeficiencyTemplate(objEntity);
                        objResponse.Message = Messages.UpdateSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }
                }
                else
                {
                    objEntity.Deficiency_Template_ID = objlapp_deficiency_template.Deficiency_Template_ID;
                    objEntity.Deficiency_Template_Name = objlapp_deficiency_template.Deficiency_Template_Name;
                    objEntity.Deficiency_Template_Message = objlapp_deficiency_template.Deficiency_Template_Message;
                    objEntity.Deficiency_Template_Subject = objlapp_deficiency_template.Deficiency_Template_Subject;
                    objEntity.Master_Transaction_Id = objlapp_deficiency_template.Master_Transaction_Id;
                    objEntity.Is_Active = objlapp_deficiency_template.Is_Active;
                    objEntity.Is_Deleted = objlapp_deficiency_template.Is_Deleted;

                    objEntity.End_Date = DateTime.Now;
                    objEntity.Created_On = DateTime.Now;
                    objEntity.Created_By = CreatedOrMoifiy;
                    objEntity.Modified_On = DateTime.Now;
                    objEntity.Modified_By = CreatedOrMoifiy;
                    int i= objBAL.SaveDeficiencyTemplate(objEntity);
                   
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                }
                string Query = "SELECT *,f.mastertransactionName FROM deficiencytemplate dr JOIN mastertransaction f ON dr.mastertransactionid=f.mastertransactionid WHERE dr.IsDeleted=0  ORDER BY dr.mastertransactionid,dr.CreatedOn DESC";

                List<LAPP_DeficiencyTemplate> lstDeficiencyTemplate = objBAL.Get_lapp_application_Deficiency_Template_By_Query_List(Query);
                objResponse.DeficiencyTemplateResponseList = lstDeficiencyTemplate;

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ConfigurationSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.DeficiencyTemplateResponseList = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }
    }
}
