using LAPP.ENTITY;
using LAPP.GlobalFunctions;
using LAPP.WS.App_Helper;
using LAPP.WS.App_Helper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LAPP.WS.ValidateController.Backoffice
{
    public class ConfigurationValidation
    {

        public static string ValidateConfigurationObject(Configuration objConfiguration)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            objResponseList = Validations.IsValidIntProperty(nameof(objConfiguration.ConfigurationTypeId), objConfiguration.ConfigurationTypeId.ToString(), objResponseList, 10);

            objResponseList = Validations.IsValidDateMMDDYYYYProperty(nameof(objConfiguration.CreatedOn), Convert.ToDateTime(objConfiguration.CreatedOn).ToShortDateString(), objResponseList, 10);

            objResponseList = Validations.IsRequiredProperty(nameof(objConfiguration.CreatedBy), objConfiguration.CreatedBy.ToString(), objResponseList, 20);


            if (objResponseList.Count() > 0)
            {
                return GeneralFunctions.GetJsonStringFromList(objResponseList);
            }
            else
            {
                return string.Empty;
            }
        }


        public static string ValidateConfigurationTypeObject(ConfigurationType objConfigurationType)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            objResponseList = Validations.IsRequiredProperty(nameof(objConfigurationType.Setting), objConfigurationType.Setting.ToString(), objResponseList, 100);
            objResponseList = Validations.IsRequiredProperty(nameof(objConfigurationType.DataType), objConfigurationType.DataType.ToString(), objResponseList, 30);


            objResponseList = Validations.IsValidDateMMDDYYYYProperty(nameof(objConfigurationType.CreatedOn), Convert.ToDateTime(objConfigurationType.CreatedOn).ToShortDateString(), objResponseList, 20);

            objResponseList = Validations.IsRequiredProperty(nameof(objConfigurationType.CreatedBy), objConfigurationType.CreatedBy.ToString(), objResponseList, 128);


            if (objResponseList.Count() > 0)
            {
                return GeneralFunctions.GetJsonStringFromList(objResponseList);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}