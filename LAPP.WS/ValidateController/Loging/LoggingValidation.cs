using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LAPP.LOGING.ENTITY;
using LAPP.WS.App_Helper.Common;
using LAPP.WS.App_Helper;
using LAPP.GlobalFunctions;

namespace LAPP.WS.ValidateController.Loging
{
    public class LoggingValidation
    {

        public static string ValidateCategoryObject(Category objCategory)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            objResponseList = Validations.IsRequiredProperty(nameof(objCategory.CategoryName), objCategory.CategoryName, objResponseList);

            if (objResponseList.Count() > 0)
            {
                return GeneralFunctions.GetJsonStringFromList(objResponseList);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ValidateCategoryLogObject(CategoryLog objCategoryLog)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            objResponseList = Validations.IsValidIntProperty(nameof(objCategoryLog.CategoryID), objCategoryLog.CategoryID.ToString(), objResponseList);
            objResponseList = Validations.IsValidIntProperty(nameof(objCategoryLog.LogID), objCategoryLog.LogID.ToString(), objResponseList);

            if (objResponseList.Count() > 0)
            {
                return GeneralFunctions.GetJsonStringFromList(objResponseList);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ValidateDatalogObject(Datalog objDatalog)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            objResponseList = Validations.IsRequiredProperty(nameof(objDatalog.TableName), objDatalog.TableName, objResponseList, 50);
            objResponseList = Validations.IsValidIntProperty(nameof(objDatalog.RowIdValue), objDatalog.RowIdValue.ToString(), objResponseList, 11);
            objResponseList = Validations.IsRequiredProperty(nameof(objDatalog.Action), objDatalog.Action, objResponseList, 6);

            if (objResponseList.Count() > 0)
            {
                return GeneralFunctions.GetJsonStringFromList(objResponseList);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ValidateLogObject(Log objLog)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            objResponseList = Validations.IsRequiredProperty(nameof(objLog.Source), objLog.Source, objResponseList, 2);
            objResponseList = Validations.IsValidIntProperty(nameof(objLog.Priority), objLog.Priority.ToString(), objResponseList);
            objResponseList = Validations.IsRequiredProperty(nameof(objLog.Severity), objLog.Severity, objResponseList, 32);
            objResponseList = Validations.IsRequiredProperty(nameof(objLog.Title), objLog.Title, objResponseList);
            objResponseList = Validations.IsValidDateMMDDYYYYProperty(nameof(objLog.Timestamp), Convert.ToDateTime(objLog.Timestamp).ToShortDateString(), objResponseList);
            objResponseList = Validations.IsRequiredProperty(nameof(objLog.MachineName), objLog.MachineName, objResponseList);
            objResponseList = Validations.IsRequiredProperty(nameof(objLog.AppDomainName), objLog.AppDomainName, objResponseList);

            objResponseList = Validations.IsRequiredProperty(nameof(objLog.ProcessID), objLog.ProcessID, objResponseList);
            objResponseList = Validations.IsRequiredProperty(nameof(objLog.ProcessName), objLog.ProcessName, objResponseList);

            if (objResponseList.Count() > 0)
            {
                return GeneralFunctions.GetJsonStringFromList(objResponseList);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ValidateAuditvisitInfoObject(AuditvisitInfo objAuditvisitInfo)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            // objResponseList = Validations.IsRequiredProperty(nameof(objAuditvisitInfo.CategoryName), objAuditvisitInfo.CategoryName, objResponseList);


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