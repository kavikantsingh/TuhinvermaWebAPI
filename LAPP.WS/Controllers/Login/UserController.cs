﻿using LAPP.BAL;
using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.WS.App_Helper;
using LAPP.WS.App_Helper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using LAPP.WS.ValidateController.Login;
using LAPP.GlobalFunctions;
using LAPP.LOGING;

namespace LAPP.WS.Controllers.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class UserController : ApiController
    {


        /// <summary>
        /// This API used for User Login.
        /// </summary>
        /// <param name="ObjLoginInfo">Request object for user login.</param>
        [AcceptVerbs("POST")]
        [ActionName("Login")]
        public LoginInfoResponse Login(LoginInfo ObjLoginInfo)
        {
            UsersBAL objUsersBAL = new UsersBAL();
            LogingHelper.SaveAuditInfo();

            LoginInfoResponse objResponse = new LoginInfoResponse();
            if (ObjLoginInfo == null)
            {
                objResponse.Message = "Invalid Object.";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                objResponse.ResponseReason = "";
                return objResponse;

            }



            try
            {
                string ValidationResponse = UserControllerValidation.ValidateUserLoginInfoObject(ObjLoginInfo);

                if (!string.IsNullOrEmpty(ValidationResponse))
                {
                    objResponse.Message = "Validation Error";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }

                if (ObjLoginInfo.LoginWithoutEmail)
                {

                    try
                    {
                        Individual objIndividual = new Individual();
                        IndividualBAL objIndividualBAL = new IndividualBAL();

                        objIndividual = objIndividualBAL.Get_Individual_By_LastNameSSNCodeLicenseNumber(ObjLoginInfo.LastName, ObjLoginInfo.LicenseNumber, ObjLoginInfo.AccessCode);
                        if (objIndividual != null)
                        {
                            Users objUseres = new Users();

                            objUseres = objUsersBAL.Get_Users_byIndividualId(objIndividual.IndividualId);
                            if (objUseres == null)
                            {
                                objUseres = new Users();

                                objUseres.FirstName = "";
                                objUseres.UserName = "";
                                objUseres.LastName = "";
                                objUseres.UserTypeId = 6;
                                objUseres.UserStatusId = 1;
                                objUseres.IsPending = false;
                                objUseres.SourceId = 1;
                                objUseres.Email = "";

                                objUseres.Gender = "";


                                objUseres.PositionTitle = "";

                                objUseres.Phone = "";

                                objUseres.PasswordHash = "123456";
                                objUseres.FailedLogins = 0;

                                objUseres.IsActive = true;
                                objUseres.IsDeleted = false;

                                objUseres.CreatedBy = 1;
                                objUseres.UserGuid = Guid.NewGuid().ToString();
                                objUseres.IndividualGuid = Guid.NewGuid().ToString();
                                objUseres.Authenticator = Guid.NewGuid().ToString();
                                objUseres.CreatedOn = DateTime.Now;
                                objUseres.CreatedBy = 0;
                                objUseres.IndividualId = objIndividual.IndividualId;
                                objUseres.UserId = objUsersBAL.Individual_User_Save(objUseres);

                            }
                            if (objUseres != null && objUseres.UserId > 0)
                            {
                                objResponse.Status = true;
                                objResponse.Message = "Login Successfull.";
                                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                                string Key = TokenHelper.GenrateToken(objUseres.UserId, "");
                                objResponse.IndividualID = objIndividual.IndividualId;
                                objResponse.UserID = objUseres.UserId;

                                var objUserResponse = new { IndividualId = objIndividual.IndividualId, UserID = objUseres.UserId, UserName = objUseres.UserName, UserTypeID = objUseres.UserTypeId, UserTypeName = objUseres.UserTypeName, FirstName = objIndividual.FirstName, LastName = objIndividual.LastName, Email = objUseres.Email };

                                objResponse.Key = Key;
                                objResponse.UserID = objUseres.UserId;
                                objResponse.UserInfo = objUserResponse;
                                LoginHelper.SaveLoginHistory(objUseres);
                            }
                            else
                            {
                                objResponse.Status = false;
                                objResponse.Message = "Login Failed. Invalid Credential.";
                                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("01");
                                string Key = "";// TokenHelper.GenrateToken(0, "");

                                objResponse.Key = Key;
                                objResponse.UserID = 0;
                            }
                        }
                        else
                        {
                            objResponse.Status = false;
                            objResponse.Message = "Login Failed. Invalid Credential.";
                            objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("01");
                            string Key = "";// TokenHelper.GenrateToken(0, "");

                            objResponse.Key = Key;
                            objResponse.UserID = 0;
                        }






                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                    //if (ObjLoginInfo.AccessCode.ToLower() == "1234" && ObjLoginInfo.LastName.ToLower() == "verma" && ObjLoginInfo.LicenseNumber.ToLower() == "1234")
                    //{

                    //    Users objUser = objUsersBAL.Get_Users_byUsersId(20);
                    //    if (objUser != null && objUser.UserId > 0)
                    //    {
                    //        objResponse.Status = true;
                    //        objResponse.Message = "Login Successfull.";
                    //        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    //        string Key = TokenHelper.GenrateToken(1, "");

                    //        var objUserResponse = new { UserID = objUser.UserId, UserName = objUser.UserName, UserTypeID = objUser.UserTypeId, UserTypeName = objUser.UserTypeName, FirstName = objUser.FirstName, LastName = objUser.LastName, Email = objUser.Email };

                    //        objResponse.Key = Key;
                    //        objResponse.UserID = 1;
                    //        objResponse.UserInfo = objUserResponse;
                    //        LoginHelper.SaveLoginHistory(objUser);
                    //    }
                    //    else
                    //    {
                    //        objResponse.Status = false;
                    //        objResponse.Message = "Login Failed. Invalid Credential.";
                    //        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("01");
                    //        string Key = TokenHelper.GenrateToken(1, "");

                    //        objResponse.Key = Key;
                    //        objResponse.UserID = 1;
                    //    }

                    //}

                }
                else
                {

                    Users objUser = objUsersBAL.Get_Users_by_Email_And_Password(ObjLoginInfo.Email, ObjLoginInfo.Password);
                    if (objUser != null && objUser.UserId > 0)
                    {
                        objResponse.Status = true;
                        objResponse.Message = "Login Successfull.";
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                        string Key = TokenHelper.GenrateToken(objUser.UserId, "");

                        var objUserResponse = new { IndividualId = objUser.IndividualId, UserID = objUser.UserId, UserName = objUser.UserName, UserTypeID = objUser.UserTypeId, UserTypeName = objUser.UserTypeName, FirstName = objUser.FirstName, LastName = objUser.LastName, Email = objUser.Email };

                        objResponse.Key = Key;
                        objResponse.UserID = objUser.UserId;
                        objResponse.IndividualID = objUser.IndividualId;
                        objResponse.UserInfo = objUserResponse;
                        LoginHelper.SaveLoginHistory(objUser);
                    }
                    else
                    {
                        objResponse.Status = false;
                        objResponse.Message = "Login Failed. Invalid Credential.";
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("01");
                        string Key = "";// TokenHelper.GenrateToken(0, "");

                        objResponse.Key = Key;
                        objResponse.UserID = 0;
                    }
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo("", ex, "Login", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.Key = "";

            }
            return objResponse;


        }

        /// <summary>
        /// Send the Email to forget password request
        /// </summary>
        /// <param name="Email">Email Address input field.</param>
        [AcceptVerbs("GET")]
        [ActionName("ForgetPassword")]
        public ForgetPasswordResponse ForgetPassword(string Email)
        {
            ForgetPasswordResponse objRsponse = new ForgetPasswordResponse();

            if (EmailHelper.SendMail(Email, "Forget Password", "Forget Password " + Email, true))
            {
                objRsponse.StatusCode = "00";
                objRsponse.Status = true;
                objRsponse.Message = "An email has been sent to your email address. Please follow the instructions in email to reset password.";
            }
            else
            {
                objRsponse.StatusCode = "11";
                objRsponse.Status = false;
                objRsponse.Message = "We are not able to send email due to technical issues.";
            }
            return objRsponse;

        }
        /// <summary>
        ///  Reset Access code by User ID
        /// </summary>

        /// <param name="obj"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("ResetAccessCode")]
        public ResetAccessCodeResponse ResetAccessCode(ResetAccessCodeRequest obj)
        {
            ResetAccessCodeResponse objResponse = new ResetAccessCodeResponse();


            objResponse.StatusCode = "00";
            objResponse.Status = true;
            objResponse.Message = "An email has been sent to your email address with access code and instructions. Please follow the instructions in your email.";

            return objResponse;

        }


        /// <summary>
        ///  Log out by Key
        /// </summary>
        /// <param name="Key"></param>

        /// <returns></returns>
        [AcceptVerbs("GET")]
        [ActionName("LogOut")]
        public BaseEntityServiceResponse LogOut(string Key)
        {
            BaseEntityServiceResponse objResponse = new BaseEntityServiceResponse();
            try
            {

                Token tc = TokenHelper.GetTokenByKey(Key);
                if (tc != null)
                {
                    TokenHelper.DestroyToken(tc.TokenId);
                    LoginHelper.SaveLogOutHistory(tc.UserId, Key);
                    objResponse.StatusCode = "00";
                    objResponse.Status = true;
                    objResponse.Message = "Logout Successfully.";
                }
                return objResponse;
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "LogOut", ENTITY.Enumeration.eSeverity.Error);
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ResponseReason = "";
                return objResponse;
            }
        }

        /// <summary>
        ///  Reset password by Key and ResetByUserIDRequest list
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="objLstRequest"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("ResetPassword")]
        public ResetPasswordResponse ResetPassword(string Key, List<ResetByUserIDRequest> objLstRequest)
        {

            ResetPasswordResponse objRsponse = new ResetPasswordResponse();
            objRsponse.StatusCode = "00";
            objRsponse.Status = true;
            objRsponse.Message = "An email has been sent to your email address. Please follow the instructions in email to reset password.";
            if (objLstRequest.Count > 0)
            {
                Users objUser = new Users();
                UsersBAL objUserBAL = new UsersBAL();
                objUser = objUserBAL.Get_Users_byUsersId(objLstRequest[0].UserId);
                if (objUser != null)
                {

                    if (EmailHelper.SendMail(objUser.Email, "Reset Password", "Reset Password ", true))
                    {
                        objRsponse.StatusCode = "00";
                        objRsponse.Status = true;
                        objRsponse.Message = "An email has been sent to your email address. Please follow the instructions in email to reset password.";
                    }
                    else
                    {
                        objRsponse.StatusCode = "11";
                        objRsponse.Status = false;
                        objRsponse.Message = "We are not able to send email due to technical issues.";
                    }
                }

            }



            return objRsponse;
        }


        /// <summary>
        ///  Change password by Key and ChangePasswordRequest
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="ObjChangePasswordRequest"></param>
        /// <returns>BaseEntityServiceResponse</returns>



        [AcceptVerbs("POST")]
        [ActionName("ChangePassword")]
        public BaseEntityServiceResponse ChangePassword(string Key, ChangePasswordRequest ObjChangePasswordRequest)
        {

            Token objToken = TokenHelper.GetTokenByKey(Key);
            LogingHelper.SaveAuditInfo(Key);
            BaseEntityServiceResponse objResponse = new BaseEntityServiceResponse();
            try
            {
                string ValidationResponse = UserControllerValidation.ValidateChangePassword(ObjChangePasswordRequest);


                if (!string.IsNullOrEmpty(ValidationResponse))
                {
                    objResponse.Message = "Validation Error";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }

                Users objUser = new Users();
                UsersBAL objUserBAL = new UsersBAL();
                objUser = objUserBAL.Get_Users_byUsersId(ObjChangePasswordRequest.UserId);
                if (objUser != null)
                {
                    if (objUser.PasswordHash != ObjChangePasswordRequest.OldPassword)
                    {
                        objResponse.Message = "New password entered does not match the previous password.";
                        objResponse.Status = false;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                        objResponse.ResponseReason = "";
                        return objResponse;

                    }
                    else
                    {
                        objUser.ModifiedOn = DateTime.Now;
                        objUser.ModifiedBy = objToken.UserId;
                        objUser.PasswordHash = ObjChangePasswordRequest.NewPassword;
                        objUserBAL.Save_Users(objUser);
                        LoginHelper.SavePasswordChangedHistory(objUser);

                        objResponse.Message = Messages.PasswordChanged;
                        objResponse.Status = true;
                        objResponse.StatusCode = "00";
                        objResponse.ResponseReason = "";
                        return objResponse;
                    }

                }
                else
                {
                    objResponse.Message = "Invalid Username.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;

                }


            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo("", ex, "ChangePassword", ENTITY.Enumeration.eSeverity.Error);
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ResponseReason = "";
                return objResponse;
            }


        }

    }
}
