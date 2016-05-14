using LAPP.ENTITY;
using LAPP.GlobalFunctions;
using LAPP.WS.App_Helper;
using LAPP.WS.App_Helper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LAPP.WS.ValidateController.User
{
    public class MenuValidation
    {

        public static string ValidateMenuObject(Menu objMenu)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            objResponseList = Validations.IsRequiredProperty(nameof(objMenu.Name), objMenu.Name.ToString(), objResponseList, 100);
            objResponseList = Validations.IsRequiredProperty(nameof(objMenu.Description), objMenu.Description.ToString(), objResponseList);

            objResponseList = Validations.IsValidDateMMDDYYYYProperty(nameof(objMenu.CreatedOn), Convert.ToDateTime(objMenu.CreatedOn).ToShortDateString(), objResponseList);
            objResponseList = Validations.IsValidIntProperty(nameof(objMenu.CreatedBy), objMenu.CreatedBy.ToString(), objResponseList);

            objResponseList = Validations.IsValidbool(nameof(objMenu.IsActive), objMenu.IsActive.ToString(), objResponseList);
            objResponseList = Validations.IsValidbool(nameof(objMenu.IsDeleted), objMenu.IsDeleted.ToString(), objResponseList);


            if (objResponseList.Count() > 0)
            {
                return GeneralFunctions.GetJsonStringFromList(objResponseList);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ValidateMenuUserTypeObject(MenuUserType objMenuUserType)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            objResponseList = Validations.IsValidIntDecimalProperty(nameof(objMenuUserType.MenuId), objMenuUserType.MenuId.ToString(), objResponseList, 100);
            objResponseList = Validations.IsValidIntDecimalProperty(nameof(objMenuUserType.UserTypeId), objMenuUserType.UserTypeId.ToString(), objResponseList, 100);
            objResponseList = Validations.IsValidIntDecimalProperty(nameof(objMenuUserType.BoardAuthorityId), objMenuUserType.BoardAuthorityId.ToString(), objResponseList, 100);

            objResponseList = Validations.IsValidDateMMDDYYYYProperty(nameof(objMenuUserType.CreatedOn), Convert.ToDateTime(objMenuUserType.CreatedOn).ToShortDateString(), objResponseList);
            objResponseList = Validations.IsValidIntProperty(nameof(objMenuUserType.CreatedBy), objMenuUserType.CreatedBy.ToString(), objResponseList);

            objResponseList = Validations.IsValidbool(nameof(objMenuUserType.IsActive), objMenuUserType.IsActive.ToString(), objResponseList);
            objResponseList = Validations.IsValidbool(nameof(objMenuUserType.IsDeleted), objMenuUserType.IsDeleted.ToString(), objResponseList);


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