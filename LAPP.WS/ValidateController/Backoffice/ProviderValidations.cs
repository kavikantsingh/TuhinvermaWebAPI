using LAPP.ENTITY;
using LAPP.GlobalFunctions;
using System;
using System.Collections.Generic;
using LAPP.WS.App_Helper;
using System.Linq;
using System.Web;

namespace LAPP.WS.ValidateController.Backoffice
{
    public class ProviderValidations
    {
        public static string ValidateProviderAddressObject(ProviderAddressCommonResponse objProvAddr)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            //objResponseList = Validations.IsRequiredProperty(nameof(objUsers.UserName), objUsers.UserName, objResponseList, 128);

            objResponseList = Validations.IsRequiredPropertyMaxLength(nameof(objProvAddr.ObjProviderAddress.StreetLine1), objProvAddr.ObjProviderAddress.StreetLine1, objResponseList, 128);
            objResponseList = Validations.IsRequiredPropertyMaxLength(nameof(objProvAddr.ObjProviderAddress.City), objProvAddr.ObjProviderAddress.City, objResponseList, 128);
            objResponseList = Validations.IsRequiredPropertyMaxLength(nameof(objProvAddr.ObjProviderAddress.StateCode), objProvAddr.ObjProviderAddress.StateCode, objResponseList, 2);
            objResponseList = Validations.IsValidUSZIPProperty(nameof(objProvAddr.ObjProviderAddress.Zip), objProvAddr.ObjProviderAddress.Zip, objResponseList, 15);

            objResponseList = Validations.IsRequiredPropertyMaxLength(nameof(objProvAddr.ObjProviderAddress.StreetLine1), objProvAddr.ObjProviderAddress.StreetLine1, objResponseList, 128);
            objResponseList = Validations.IsRequiredPropertyMaxLength(nameof(objProvAddr.ObjProviderAddress.City), objProvAddr.ObjProviderAddress.City, objResponseList, 128);
            objResponseList = Validations.IsRequiredPropertyMaxLength(nameof(objProvAddr.ObjProviderAddress.StateCode), objProvAddr.ObjProviderAddress.StateCode, objResponseList, 2);
            objResponseList = Validations.IsValidUSZIPProperty(nameof(objProvAddr.ObjProviderAddress.Zip), objProvAddr.ObjProviderAddress.Zip, objResponseList, 15);


            objResponseList = Validations.IsRequiredPropertyMaxLength(nameof(objProvAddr.ObjMailingAddress.StreetLine1), objProvAddr.ObjMailingAddress.StreetLine1, objResponseList, 128);
            objResponseList = Validations.IsRequiredPropertyMaxLength(nameof(objProvAddr.ObjMailingAddress.City), objProvAddr.ObjMailingAddress.City, objResponseList, 128);
            objResponseList = Validations.IsRequiredPropertyMaxLength(nameof(objProvAddr.ObjMailingAddress.StateCode), objProvAddr.ObjMailingAddress.StateCode, objResponseList, 2);
            objResponseList = Validations.IsValidUSZIPProperty(nameof(objProvAddr.ObjMailingAddress.Zip), objProvAddr.ObjMailingAddress.Zip, objResponseList, 15);

            //objResponseList = Validations.IsValidEmailProperty(nameof(objProvAddr.ObjAdminEmail), objProvAddr.ObjAdminEmail., objResponseList, 15);
            



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