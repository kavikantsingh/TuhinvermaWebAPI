using LAPP.BAL;
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
    public class UsersValidation
    {
        public static string ValidateUsersObject(UsersRequest objUsers)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            //objResponseList = Validations.IsRequiredProperty(nameof(objUsers.UserName), objUsers.UserName, objResponseList, 128);

            objResponseList = Validations.IsRequiredProperty(nameof(objUsers.FirstName), objUsers.FirstName, objResponseList, 35);
            objResponseList = Validations.IsRequiredProperty(nameof(objUsers.LastName), objUsers.LastName, objResponseList, 35);
            objResponseList = Validations.IsValidIntProperty(nameof(objUsers.UserTypeId), objUsers.UserTypeId.ToString(), objResponseList, 4);
            objResponseList = Validations.IsValidEmailProperty(nameof(objUsers.Email), objUsers.Email, objResponseList, 128);

            if (!string.IsNullOrEmpty(objUsers.Gender))
            {
                objResponseList = Validations.IsValidateLengthProperty(nameof(objUsers.Gender), objUsers.Gender, objResponseList, 1);
            }

            if (!string.IsNullOrEmpty(objUsers.DateOfBirth.ToString()))
            {
                objResponseList = Validations.IsValidDateMMDDYYYYProperty(nameof(objUsers.DateOfBirth), Convert.ToDateTime(objUsers.DateOfBirth).ToShortDateString(), objResponseList, 128);
            }

            if (Validations.IsValidEmail(objUsers.Email))
            {
                if (objUsers.UserId > 0)
                {
                    Users objuser = new Users();
                    UsersBAL objuserBal = new UsersBAL();
                    objuser = objuserBal.Get_Users_byUsersId(objUsers.UserId);
                    if (objuser != null)
                    {
                        if (objuser.Email != objUsers.Email)
                        {
                            objResponseList = Validations.IsValidEmailFromUser(nameof(objUsers.Email), objUsers.Email, objResponseList);
                        }
                    }
                    else
                    {
                        objResponseList = Validations.IsValidEmailFromUser(nameof(objUsers.Email), objUsers.Email, objResponseList);
                    }
                }
                else
                {
                    objResponseList = Validations.IsValidEmailFromUser(nameof(objUsers.Email), objUsers.Email, objResponseList);
                }
            }

            if (!string.IsNullOrEmpty(objUsers.Phone))
            {
                objResponseList = Validations.IsValidUSPhoneNoProperty(nameof(objUsers.Phone), objUsers.Phone, objResponseList, 25);
            }

            //if (!string.IsNullOrEmpty(objUsers.CellPhone))
            //{
            //    objResponseList = Validations.IsValidUSPhoneNoProperty(nameof(objUsers.CellPhone), objUsers.CellPhone, objResponseList, 25);
            //}

            objResponseList = Validations.IsValidIntProperty(nameof(objUsers.UserStatusId), objUsers.UserStatusId.ToString(), objResponseList);

            objResponseList = Validations.IsValidbool(nameof(objUsers.IsPending), objUsers.IsPending.ToString(), objResponseList);


            if (objResponseList.Count() > 0)
            {
                return GeneralFunctions.GetJsonStringFromList(objResponseList);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ValidateUserTypeObject(UserType objUserType)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            objResponseList = Validations.IsValidIntProperty(nameof(objUserType.UserTypeId), objUserType.UserTypeId.ToString(), objResponseList);

            objResponseList = Validations.IsRequiredProperty(nameof(objUserType.Code), objUserType.Code, objResponseList, 25);

            objResponseList = Validations.IsValidDateMMDDYYYYProperty(nameof(objUserType.CreatedOn), Convert.ToDateTime(objUserType.CreatedOn).ToShortDateString(), objResponseList);
            objResponseList = Validations.IsValidIntProperty(nameof(objUserType.CreatedBy), objUserType.CreatedBy.ToString(), objResponseList);

            objResponseList = Validations.IsValidbool(nameof(objUserType.IsActive), objUserType.IsActive.ToString(), objResponseList);
            objResponseList = Validations.IsValidbool(nameof(objUserType.IsDeleted), objUserType.IsDeleted.ToString(), objResponseList);


            if (objResponseList.Count() > 0)
            {
                return GeneralFunctions.GetJsonStringFromList(objResponseList);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ValidateUserStatusObject(UserStatus objUserType)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            objResponseList = Validations.IsValidIntProperty(nameof(objUserType.UserStatusId), objUserType.UserStatusId.ToString(), objResponseList, 4);

            objResponseList = Validations.IsRequiredProperty(nameof(objUserType.Name), objUserType.Name, objResponseList, 30);

            objResponseList = Validations.IsValidIntProperty(nameof(objUserType.SortOrder), objUserType.SortOrder.ToString(), objResponseList, 4);


            objResponseList = Validations.IsValidDateMMDDYYYYProperty(nameof(objUserType.CreatedOn), Convert.ToDateTime(objUserType.CreatedOn).ToShortDateString(), objResponseList);
            objResponseList = Validations.IsValidIntProperty(nameof(objUserType.CreatedBy), objUserType.CreatedBy.ToString(), objResponseList);

            objResponseList = Validations.IsValidbool(nameof(objUserType.IsActive), objUserType.IsActive.ToString(), objResponseList);
            objResponseList = Validations.IsValidbool(nameof(objUserType.IsDeleted), objUserType.IsDeleted.ToString(), objResponseList);


            if (objResponseList.Count() > 0)
            {
                return GeneralFunctions.GetJsonStringFromList(objResponseList);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ValidateUserSecurityQuestionObject(UserSecurityQuestion objUserSecurityQuestion)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            objResponseList = Validations.IsValidIntProperty(nameof(objUserSecurityQuestion.UserId), objUserSecurityQuestion.UserId.ToString(), objResponseList, 11);
            objResponseList = Validations.IsRequiredProperty(nameof(objUserSecurityQuestion.UserName), objUserSecurityQuestion.UserName, objResponseList, 128);

            objResponseList = Validations.IsValidIntProperty(nameof(objUserSecurityQuestion.SecurityQuestionId), objUserSecurityQuestion.SecurityQuestionId.ToString(), objResponseList, 11);
            objResponseList = Validations.IsRequiredProperty(nameof(objUserSecurityQuestion.SecurityAnswerHash), objUserSecurityQuestion.SecurityAnswerHash.ToString(), objResponseList, 128);


            objResponseList = Validations.IsValidDateMMDDYYYYProperty(nameof(objUserSecurityQuestion.CreatedOn), Convert.ToDateTime(objUserSecurityQuestion.CreatedOn).ToShortDateString(), objResponseList);
            objResponseList = Validations.IsValidIntProperty(nameof(objUserSecurityQuestion.CreatedBy), objUserSecurityQuestion.CreatedBy.ToString(), objResponseList);

            objResponseList = Validations.IsValidbool(nameof(objUserSecurityQuestion.IsActive), objUserSecurityQuestion.IsActive.ToString(), objResponseList);
            objResponseList = Validations.IsValidbool(nameof(objUserSecurityQuestion.IsDeleted), objUserSecurityQuestion.IsDeleted.ToString(), objResponseList);


            if (objResponseList.Count() > 0)
            {
                return GeneralFunctions.GetJsonStringFromList(objResponseList);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ValidateUserRoleObject(UserRole objUserRole)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            objResponseList = Validations.IsValidIntProperty(nameof(objUserRole.UserId), objUserRole.UserId.ToString(), objResponseList, 11);
            objResponseList = Validations.IsValidIntProperty(nameof(objUserRole.RoleId), objUserRole.RoleId.ToString(), objResponseList, 11);

            objResponseList = Validations.IsValidbool(nameof(objUserRole.IsGrantable), objUserRole.IsGrantable.ToString(), objResponseList);


            objResponseList = Validations.IsValidDateMMDDYYYYProperty(nameof(objUserRole.CreatedOn), Convert.ToDateTime(objUserRole.CreatedOn).ToShortDateString(), objResponseList);
            objResponseList = Validations.IsValidIntProperty(nameof(objUserRole.CreatedBy), objUserRole.CreatedBy.ToString(), objResponseList);

            objResponseList = Validations.IsValidbool(nameof(objUserRole.IsActive), objUserRole.IsActive.ToString(), objResponseList);
            objResponseList = Validations.IsValidbool(nameof(objUserRole.IsDeleted), objUserRole.IsDeleted.ToString(), objResponseList);


            if (objResponseList.Count() > 0)
            {
                return GeneralFunctions.GetJsonStringFromList(objResponseList);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ValidateUserDivisionObject(UserDivision objUserDivision)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            objResponseList = Validations.IsValidIntProperty(nameof(objUserDivision.UserId), objUserDivision.UserId.ToString(), objResponseList, 11);
            objResponseList = Validations.IsValidIntProperty(nameof(objUserDivision.DivisionId), objUserDivision.DivisionId.ToString(), objResponseList, 11);

            objResponseList = Validations.IsValidDateMMDDYYYYProperty(nameof(objUserDivision.CreatedOn), Convert.ToDateTime(objUserDivision.CreatedOn).ToShortDateString(), objResponseList);
            objResponseList = Validations.IsValidIntProperty(nameof(objUserDivision.CreatedBy), objUserDivision.CreatedBy.ToString(), objResponseList);

            objResponseList = Validations.IsValidbool(nameof(objUserDivision.IsActive), objUserDivision.IsActive.ToString(), objResponseList);
            objResponseList = Validations.IsValidbool(nameof(objUserDivision.IsDeleted), objUserDivision.IsDeleted.ToString(), objResponseList);


            if (objResponseList.Count() > 0)
            {
                return GeneralFunctions.GetJsonStringFromList(objResponseList);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ValidateUserBoardAuthorityObject(UserBoardAuthority objUserBoardAuthority)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            objResponseList = Validations.IsValidIntProperty(nameof(objUserBoardAuthority.UserId), objUserBoardAuthority.UserId.ToString(), objResponseList, 11);
            objResponseList = Validations.IsValidIntProperty(nameof(objUserBoardAuthority.BoardAuthorityId), objUserBoardAuthority.BoardAuthorityId.ToString(), objResponseList, 11);

            objResponseList = Validations.IsValidDateMMDDYYYYProperty(nameof(objUserBoardAuthority.CreatedOn), Convert.ToDateTime(objUserBoardAuthority.CreatedOn).ToShortDateString(), objResponseList);
            objResponseList = Validations.IsValidIntProperty(nameof(objUserBoardAuthority.CreatedBy), objUserBoardAuthority.CreatedBy.ToString(), objResponseList);

            objResponseList = Validations.IsValidbool(nameof(objUserBoardAuthority.IsActive), objUserBoardAuthority.IsActive.ToString(), objResponseList);
            objResponseList = Validations.IsValidbool(nameof(objUserBoardAuthority.IsDeleted), objUserBoardAuthority.IsDeleted.ToString(), objResponseList);


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