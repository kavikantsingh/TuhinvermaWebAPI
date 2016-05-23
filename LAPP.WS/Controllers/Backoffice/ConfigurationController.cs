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
    }
}
