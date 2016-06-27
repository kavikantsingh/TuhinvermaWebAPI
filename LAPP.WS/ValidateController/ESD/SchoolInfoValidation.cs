using LAPP.BAL;
using LAPP.ENTITY;
using LAPP.GlobalFunctions;
using LAPP.WS.App_Helper;
using LAPP.WS.App_Helper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LAPP.WS.ValidateController.SchoolInfo
{
    public class ApprovalAgencyValidation
    {
        public static string ValidateApprovalAgencyObject(ApprovalAgencyRequest objApprovalAgency)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            //objResponseList = Validations.IsRequiredProperty(nameof(objApprovalAgency.UserName), objApprovalAgency.UserName, objResponseList, 128);

            objResponseList = Validations.IsRequiredProperty(nameof(objApprovalAgency.FirstName), objApprovalAgency.FirstName, objResponseList, 35);
            objResponseList = Validations.IsRequiredProperty(nameof(objApprovalAgency.LastName), objApprovalAgency.LastName, objResponseList, 35);
            objResponseList = Validations.IsValidIntProperty(nameof(objApprovalAgency.UserTypeId), objApprovalAgency.UserTypeId.ToString(), objResponseList, 4);
            objResponseList = Validations.IsValidEmailProperty(nameof(objApprovalAgency.Email), objApprovalAgency.Email, objResponseList, 128);

            if (!string.IsNullOrEmpty(objApprovalAgency.Gender))
            {
                objResponseList = Validations.IsValidateLengthProperty(nameof(objApprovalAgency.Gender), objApprovalAgency.Gender, objResponseList, 1);
            }

            if (!string.IsNullOrEmpty(objApprovalAgency.DateOfBirth.ToString()))
            {
                objResponseList = Validations.IsValidDateMMDDYYYYProperty(nameof(objApprovalAgency.DateOfBirth), Convert.ToDateTime(objApprovalAgency.DateOfBirth).ToShortDateString(), objResponseList, 128);
            }

            if (Validations.IsValidEmail(objApprovalAgency.Email))
            {
                if (objApprovalAgency.UserId > 0)
                {
                    ApprovalAgency objuser = new ApprovalAgency();
                    ApprovalAgencyBAL objuserBal = new ApprovalAgencyBAL();
                    objuser = objuserBal.Get_ApprovalAgency_byApprovalAgencyId(objApprovalAgency.UserId);
                    if (objuser != null)
                    {
                        if (objuser.Email != objApprovalAgency.Email)
                        {
                            objResponseList = Validations.IsValidEmailFromUser(nameof(objApprovalAgency.Email), objApprovalAgency.Email, objResponseList);
                        }
                    }
                    else
                    {
                        objResponseList = Validations.IsValidEmailFromUser(nameof(objApprovalAgency.Email), objApprovalAgency.Email, objResponseList);
                    }
                }
                else
                {
                    objResponseList = Validations.IsValidEmailFromUser(nameof(objApprovalAgency.Email), objApprovalAgency.Email, objResponseList);
                }
            }



            if (objApprovalAgency.UserName != "")
            {
                if (objApprovalAgency.UserId > 0)
                {
                    ApprovalAgency objuser = new ApprovalAgency();
                    ApprovalAgencyBAL objuserBal = new ApprovalAgencyBAL();
                    objuser = objuserBal.Get_ApprovalAgency_byApprovalAgencyId(objApprovalAgency.UserId);
                    if (objuser != null)
                    {
                        if (objuser.UserName != objApprovalAgency.UserName)
                        {
                            objResponseList = Validations.IsValidUserNameFromUser(nameof(objApprovalAgency.UserName), objApprovalAgency.UserName, objResponseList);
                        }
                    }
                    else
                    {
                        objResponseList = Validations.IsValidUserNameFromUser(nameof(objApprovalAgency.UserName), objApprovalAgency.UserName, objResponseList);
                    }
                }
                else
                {
                    objResponseList = Validations.IsValidUserNameFromUser(nameof(objApprovalAgency.UserName), objApprovalAgency.UserName, objResponseList);
                }
            }


            if (!string.IsNullOrEmpty(objApprovalAgency.Phone))
            {
                objResponseList = Validations.IsValidUSPhoneNoProperty(nameof(objApprovalAgency.Phone), objApprovalAgency.Phone, objResponseList, 25);
            }

            //if (!string.IsNullOrEmpty(objApprovalAgency.CellPhone))
            //{
            //    objResponseList = Validations.IsValidUSPhoneNoProperty(nameof(objApprovalAgency.CellPhone), objApprovalAgency.CellPhone, objResponseList, 25);
            //}

            objResponseList = Validations.IsValidIntProperty(nameof(objApprovalAgency.ApprovalAgencytatusId), objApprovalAgency.ApprovalAgencytatusId.ToString(), objResponseList);

            objResponseList = Validations.IsValidbool(nameof(objApprovalAgency.IsPending), objApprovalAgency.IsPending.ToString(), objResponseList);


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

        //public static string ValidateApprovalAgencytatusObject(ApprovalAgencytatus objUserType)
        //{
        //    List<ResponseReason> objResponseList = new List<ResponseReason>();

        //    objResponseList = Validations.IsValidIntProperty(nameof(objUserType.ApprovalAgencytatusId), objUserType.ApprovalAgencytatusId.ToString(), objResponseList, 4);

        //    objResponseList = Validations.IsRequiredProperty(nameof(objUserType.Name), objUserType.Name, objResponseList, 30);

        //    objResponseList = Validations.IsValidIntProperty(nameof(objUserType.SortOrder), objUserType.SortOrder.ToString(), objResponseList, 4);


        //    objResponseList = Validations.IsValidDateMMDDYYYYProperty(nameof(objUserType.CreatedOn), Convert.ToDateTime(objUserType.CreatedOn).ToShortDateString(), objResponseList);
        //    objResponseList = Validations.IsValidIntProperty(nameof(objUserType.CreatedBy), objUserType.CreatedBy.ToString(), objResponseList);

        //    objResponseList = Validations.IsValidbool(nameof(objUserType.IsActive), objUserType.IsActive.ToString(), objResponseList);
        //    objResponseList = Validations.IsValidbool(nameof(objUserType.IsDeleted), objUserType.IsDeleted.ToString(), objResponseList);


        //    if (objResponseList.Count() > 0)
        //    {
        //        return GeneralFunctions.GetJsonStringFromList(objResponseList);
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        //public static string ValidateApprovalAgencyecurityQuestionObject(ApprovalAgencyecurityQuestion objApprovalAgencyecurityQuestion)
        //{
        //    List<ResponseReason> objResponseList = new List<ResponseReason>();

        //    objResponseList = Validations.IsValidIntProperty(nameof(objApprovalAgencyecurityQuestion.UserId), objApprovalAgencyecurityQuestion.UserId.ToString(), objResponseList, 11);
        //    objResponseList = Validations.IsRequiredProperty(nameof(objApprovalAgencyecurityQuestion.UserName), objApprovalAgencyecurityQuestion.UserName, objResponseList, 128);

        //    objResponseList = Validations.IsValidIntProperty(nameof(objApprovalAgencyecurityQuestion.SecurityQuestionId), objApprovalAgencyecurityQuestion.SecurityQuestionId.ToString(), objResponseList, 11);
        //    objResponseList = Validations.IsRequiredProperty(nameof(objApprovalAgencyecurityQuestion.SecurityAnswerHash), objApprovalAgencyecurityQuestion.SecurityAnswerHash.ToString(), objResponseList, 128);


        //    objResponseList = Validations.IsValidDateMMDDYYYYProperty(nameof(objApprovalAgencyecurityQuestion.CreatedOn), Convert.ToDateTime(objApprovalAgencyecurityQuestion.CreatedOn).ToShortDateString(), objResponseList);
        //    objResponseList = Validations.IsValidIntProperty(nameof(objApprovalAgencyecurityQuestion.CreatedBy), objApprovalAgencyecurityQuestion.CreatedBy.ToString(), objResponseList);

        //    objResponseList = Validations.IsValidbool(nameof(objApprovalAgencyecurityQuestion.IsActive), objApprovalAgencyecurityQuestion.IsActive.ToString(), objResponseList);
        //    objResponseList = Validations.IsValidbool(nameof(objApprovalAgencyecurityQuestion.IsDeleted), objApprovalAgencyecurityQuestion.IsDeleted.ToString(), objResponseList);


        //    if (objResponseList.Count() > 0)
        //    {
        //        return GeneralFunctions.GetJsonStringFromList(objResponseList);
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

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