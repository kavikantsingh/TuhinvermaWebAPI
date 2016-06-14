using LAPP.ENTITY;
using LAPP.GlobalFunctions;
using LAPP.WS.App_Helper;
using LAPP.WS.App_Helper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace LAPP.WS.ValidateController.Login
{
    public class UserControllerValidation
    {

        public static string ValidateUserLoginInfoObject(LoginInfo objLoginInfo)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            if (objLoginInfo.LoginWithoutEmail == false && !string.IsNullOrEmpty(objLoginInfo.Email))
            {
                objResponseList = Validations.IsValidEmailProperty(nameof(objLoginInfo.Email), objLoginInfo.Email, objResponseList);
            }

            if (objLoginInfo.LoginWithoutEmail == true && !string.IsNullOrEmpty(objLoginInfo.AccessCode))
            {
                objResponseList = Validations.IsRequiredPropertyMaxLength(nameof(objLoginInfo.AccessCode), objLoginInfo.AccessCode, objResponseList, 4);
            }

            if (objResponseList.Count() > 0)
            {
                return GeneralFunctions.GetJsonStringFromList(objResponseList);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ValidateChangePassword(ChangePasswordRequest objRequest)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            objResponseList = Validations.IsIntGreaterThanZero(nameof(objRequest.UserId), objRequest.UserId, objResponseList);


            objResponseList = Validations.IsRequiredProperty(nameof(objRequest.OldPassword), objRequest.OldPassword, objResponseList);
            objResponseList = Validations.IsRequiredProperty(nameof(objRequest.NewPassword), objRequest.NewPassword, objResponseList);


            objResponseList = Validations.IsRequiredProperty(nameof(objRequest.ConfirmPassword), objRequest.ConfirmPassword, objResponseList);
            objResponseList = Validations.CompareStrings(nameof(objRequest.NewPassword), nameof(objRequest.ConfirmPassword), objRequest.NewPassword, objRequest.ConfirmPassword, objResponseList);


            if (objResponseList.Count > 0)
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