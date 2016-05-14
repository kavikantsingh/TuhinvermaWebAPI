using LAPP.ENTITY;
using LAPP.GlobalFunctions;
using LAPP.WS.App_Helper;
using LAPP.WS.App_Helper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LAPP.WS.ValidateController.Common
{
    public class LookupValidation
    {


        public static string ValidateLookupObject(LookupPost objLookup)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            objResponseList = Validations.IsValidIntProperty(nameof(objLookup.LookupTypeId), objLookup.LookupTypeId.ToString(), objResponseList);

            objResponseList = Validations.IsRequiredProperty(nameof(objLookup.LookupCode), objLookup.LookupCode, objResponseList, 30);

            objResponseList = Validations.IsValidIntProperty(nameof(objLookup.SortOrder), objLookup.SortOrder.ToString(), objResponseList);


            objResponseList = Validations.IsValidbool(nameof(objLookup.IsActive), objLookup.IsActive.ToString(), objResponseList);
            objResponseList = Validations.IsValidbool(nameof(objLookup.IsDeleted), objLookup.IsDeleted.ToString(), objResponseList);


            if (objResponseList.Count() > 0)
            {
                return GeneralFunctions.GetJsonStringFromList(objResponseList);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ValidateLookupTypeObject(LookupTypePost objLookupType)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            objResponseList = Validations.IsValidIntProperty(nameof(objLookupType.DivisionId), objLookupType.DivisionId.ToString(), objResponseList);
            objResponseList = Validations.IsValidIntProperty(nameof(objLookupType.DepartmentId), objLookupType.DepartmentId.ToString(), objResponseList);

            objResponseList = Validations.IsRequiredProperty(nameof(objLookupType.Name), objLookupType.Name, objResponseList, 75);

            if (!string.IsNullOrEmpty(objLookupType.StateCode))
            {
                objResponseList = Validations.IsValidOnlyMaxLength(nameof(objLookupType.StateCode), objLookupType.StateCode, objResponseList, 2);
            }

            objResponseList = Validations.IsValidbool(nameof(objLookupType.IsActive), objLookupType.IsActive.ToString(), objResponseList);
            objResponseList = Validations.IsValidbool(nameof(objLookupType.IsDeleted), objLookupType.IsDeleted.ToString(), objResponseList);


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