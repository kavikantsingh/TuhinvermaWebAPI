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
    public class RoleValidation
    {
        public static string ValidateRoleObject(Role objRole)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            objResponseList = Validations.IsRequiredProperty(nameof(objRole.Name), objRole.Name.ToString(), objResponseList, 100);
            objResponseList = Validations.IsRequiredProperty(nameof(objRole.Description), objRole.Description.ToString(), objResponseList);

            objResponseList = Validations.IsValidIntProperty(nameof(objRole.BoardAuthorityId), objRole.BoardAuthorityId.ToString(), objResponseList);
            objResponseList = Validations.IsValidIntProperty(nameof(objRole.DivisionId), objRole.DivisionId.ToString(), objResponseList, 30);
            objResponseList = Validations.IsValidIntProperty(nameof(objRole.UserTypeId), objRole.UserTypeId.ToString(), objResponseList);

            objResponseList = Validations.IsValidDateMMDDYYYYProperty(nameof(objRole.CreatedOn), Convert.ToDateTime(objRole.CreatedOn).ToShortDateString(), objResponseList);
            objResponseList = Validations.IsValidIntProperty(nameof(objRole.CreatedBy), objRole.CreatedBy.ToString(), objResponseList);

            objResponseList = Validations.IsValidbool(nameof(objRole.IsActive), objRole.IsActive.ToString(), objResponseList);
            objResponseList = Validations.IsValidbool(nameof(objRole.IsDeleted), objRole.IsDeleted.ToString(), objResponseList);
            objResponseList = Validations.IsValidbool(nameof(objRole.IsEnabled), objRole.IsEnabled.ToString(), objResponseList);

            if (objResponseList.Count() > 0)
            {
                return GeneralFunctions.GetJsonStringFromList(objResponseList);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ValidateRoleMenuObject(RoleMenuRequest objRoleMenu)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();


            objResponseList = Validations.IsValidIntProperty(nameof(objRoleMenu.RoleId), objRoleMenu.RoleId.ToString(), objResponseList);
            objResponseList = Validations.IsValidIntProperty(nameof(objRoleMenu.MenuId), objRoleMenu.MenuId.ToString(), objResponseList, 30);

            //objResponseList = Validations.IsValidDateMMDDYYYYProperty(nameof(objRoleMenu.CreatedOn), Convert.ToDateTime(objRoleMenu.CreatedOn).ToShortDateString(), objResponseList);
           // objResponseList = Validations.IsValidIntProperty(nameof(objRoleMenu.CreatedBy), objRoleMenu.CreatedBy.ToString(), objResponseList);

            objResponseList = Validations.IsValidbool(nameof(objRoleMenu.IsActive), objRoleMenu.IsActive.ToString(), objResponseList);
            objResponseList = Validations.IsValidbool(nameof(objRoleMenu.IsDeleted), objRoleMenu.IsDeleted.ToString(), objResponseList);

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