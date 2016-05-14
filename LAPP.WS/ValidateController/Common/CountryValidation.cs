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
    public class CountryValidation
    {
        public static string ValidateCountryObject(CountryRequest objCountry)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            objResponseList = Validations.IsRequiredProperty(nameof(objCountry.Name), objCountry.Name, objResponseList);

            objResponseList = Validations.IsRequiredProperty(nameof(objCountry.Code), objCountry.Code, objResponseList);

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