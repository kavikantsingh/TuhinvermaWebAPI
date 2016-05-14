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
    public class SourceValidation
    {

        public static string ValidateSourceObject(SourceRequest objSource)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            objResponseList = Validations.IsValidIntProperty(nameof(objSource.SourceId), objSource.SourceId.ToString(), objResponseList, 4);

            objResponseList = Validations.IsRequiredProperty(nameof(objSource.Name), objSource.Name, objResponseList, 30);

          //  objResponseList = Validations.IsValidDateMMDDYYYYProperty(nameof(objSource.CreatedOn), Convert.ToDateTime(objSource.CreatedOn).ToShortDateString(), objResponseList);
           // objResponseList = Validations.IsValidIntProperty(nameof(objSource.CreatedBy), objSource.CreatedBy.ToString(), objResponseList);

            objResponseList = Validations.IsValidbool(nameof(objSource.IsActive), objSource.IsActive.ToString(), objResponseList);
            objResponseList = Validations.IsValidbool(nameof(objSource.IsDeleted), objSource.IsDeleted.ToString(), objResponseList);


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