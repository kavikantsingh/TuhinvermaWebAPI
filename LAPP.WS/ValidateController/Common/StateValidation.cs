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
    public class StateValidation
    {
        public static string ValidateStateObject(State objState)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            objResponseList = Validations.IsRequiredProperty(nameof(objState.Name), objState.Name, objResponseList);

            objResponseList = Validations.IsRequiredProperty(nameof(objState.StateCode), objState.StateCode, objResponseList);

            objResponseList = Validations.IsValidIntProperty(nameof(objState.CountryId), objState.CountryId.ToString(), objResponseList);

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