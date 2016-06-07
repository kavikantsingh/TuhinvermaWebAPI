using LAPP.BAL;
using LAPP.BAL.Common;
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
    public class ProviderController : ApiController
    {
        /// <summary>
        /// This API used for Provider Registration.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ObjRegisterInfo">Request object for Provider Registration.</param>
        [AcceptVerbs("POST")]
        [ActionName("ProviderRegister")]
        public BaseEntityServiceResponse ProviderRegister(string Key, ProviderRegister ObjRegisterInfo)
        {
            int CreateOrModify = TokenHelper.GetTokenByKey(Key).UserId;

            UsersBAL objUsersBAL = new UsersBAL();
            LogingHelper.SaveAuditInfo();

            BaseEntityServiceResponse objResponse = new BaseEntityServiceResponse();
            
            UsersBAL objBAL = new UsersBAL();
            Users objEntity = new Users();

            Provider objProvider = new Provider();
            ProviderResponse objProviderResponse = new ProviderResponse();
            ProviderResponseRequest objProviderResponseRequest = new ProviderResponseRequest();

            List<UsersRequest> lstEntity = new List<UsersRequest>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Message = "User session has expired.";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.ResponseReason = "";
                return objResponse;
            }
            
            if (ObjRegisterInfo == null)
            {
                objResponse.Message = "Invalid Object.";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                objResponse.ResponseReason = "";
                return objResponse;
            }

            //try
            //{
            //    string ValidationResponse = UsersValidation.ValidateUsersObject(objUsers);

            //    if (!string.IsNullOrEmpty(ValidationResponse))
            //    {
            //        objResponse.Message = "Validation Error";
            //        objResponse.Status = false;
            //        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
            //        objResponse.ResponseReason = ValidationResponse;
            //        return objResponse;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    LogingHelper.SaveExceptionInfo("", ex, "Login", ENTITY.Enumeration.eSeverity.Error);

            //    objResponse.Status = false;
            //    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            //    objResponse.Message = ex.Message;

            //}


            try
            {
                Individual objIndividual = new Individual();
                IndividualBAL objIndividualBAL = new IndividualBAL();

                int PasswordExpirationDays = 0;
                ConfigurationTypeBAL objCfgBAL = new ConfigurationTypeBAL();
                ConfigurationType objCfgEntity = new ConfigurationType();
                ConfigurationType objConfigurationType = new ConfigurationType();
                objConfigurationType = objCfgBAL.Get_Configuration_By_Settings_object("SchoolPasswordExpirationDays".ToLower());
                if (objConfigurationType != null)
                {
                    PasswordExpirationDays = Convert.ToInt32(objConfigurationType.Value);
                }

                string TempPassword = GeneralFunctions.GetTempPassword();
                objEntity = new Users();

                objEntity.FirstName = ObjRegisterInfo.FirstName;
                objEntity.UserName = ObjRegisterInfo.Email;
                objEntity.LastName = ObjRegisterInfo.LastName;
                objEntity.UserTypeId = 3;
                //objEntity.UserSubTypeId = 1;
                objEntity.UserStatusId = 1;
                objEntity.IsPending = false;
                objEntity.SourceId = 1;
                objEntity.Email = ObjRegisterInfo.Email;

                objEntity.Gender = "";
                objEntity.DateOfBirth = ObjRegisterInfo.DateofBirth;

                objEntity.PositionTitle = "";

                objEntity.Phone = "";

                objEntity.PasswordHash = TempPassword;// "123456";
                objEntity.TemporaryPassword = true;
                objEntity.FailedLogins = 0;
                objEntity.PasswordExpirationDate = DateTime.Now.AddDays(PasswordExpirationDays);
                // objUsers.SourceId = 0;

                objEntity.IsActive = true;
                objEntity.IsDeleted = false;

                //   objEntity.PasswordChangedOn = DateTime.Now;
                objEntity.CreatedBy = 1;
                objEntity.UserGuid = Guid.NewGuid().ToString();
                objEntity.IndividualGuid = Guid.NewGuid().ToString();
                objEntity.Authenticator = Guid.NewGuid().ToString();
                objEntity.CreatedOn = DateTime.Now;
                objEntity.CreatedBy = CreateOrModify;
                //objUsers.ModifiedOn = null;
                objEntity.UserId = objBAL.Individual_User_Save(objEntity);

                objResponse.Message = Messages.SaveSuccess;
                objResponse.Status = true;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                int UserId = objEntity.UserId;

                #region User Role Save
                List<UserRolesRequest> lstRoles = new List<UserRolesRequest>();
                //lstRoles = objUsers.UserRoles;

                if (lstRoles != null && lstRoles.Count > 0)
                {
                    foreach (UserRolesRequest objRoleReq in lstRoles)
                    {
                        UserRole objRole = new UserRole();
                        UserRoleBAL objRoleBAL = new UserRoleBAL();
                        if (objRoleReq.UserRoleID > 0)
                        {
                            objRole = objRoleBAL.Get_UserRole_byUserRoleId(objRoleReq.UserRoleID);
                            if (objRole != null)
                            {
                                objRole.IsActive = objRoleReq.Selected;
                                objRole.IsGrantable = objRoleReq.Grantable;
                                objRole.ModifiedBy = 1;
                                objRole.ModifiedOn = DateTime.Now;
                                objRoleBAL.Save_UserRole(objRole);
                            }
                        }
                        else
                        {
                            objRole = new UserRole();
                            objRole.RoleId = objRoleReq.RoleId;
                            objRole.UserId = UserId;
                            objRole.IsActive = objRoleReq.Selected;
                            objRole.IsGrantable = objRoleReq.Grantable;
                            objRole.ModifiedBy = 1;
                            objRole.ModifiedOn = DateTime.Now;
                            objRole.CreatedBy = 1;
                            objRole.CreatedOn = DateTime.Now;
                            objRole.IsDeleted = false;
                            objRoleBAL.Save_UserRole(objRole);
                        }

                    }
                }
                #endregion

                #region User Provider Save

                #endregion

                #region Provider Save

                objProviderResponse.ProviderName = ObjRegisterInfo.SchoolName;

                objProviderResponseRequest =  ProviderCS.ProviderSave(TokenHelper.GetTokenByKey(Key), objProviderResponse);

                //objProviderResponse.ProviderId = objProviderResponseRequest.ProviderResponseList.
                #endregion

                #region Send Email of user creation

                if (EmailHelper.SendMail(ObjRegisterInfo.Email, "Temporary Password", "Email: " + ObjRegisterInfo.Email + " <br/> Temporary Password: " + TempPassword, true))
                {
                    LogHelper.LogCommunication(objIndividual.IndividualId, null, eCommunicationType.Email, "Temporary Password", eCommunicationStatus.Success, (eCommentLogSource.WSAPI).ToString(), "Temporary Password email has been sent", EmailHelper.GetSenderAddress(), ObjRegisterInfo.Email, null, null, UserId, null, null, null);
                }
                else
                {
                    LogHelper.LogCommunication(objIndividual.IndividualId, null, eCommunicationType.Email, "Temporary Password", eCommunicationStatus.Fail, (eCommentLogSource.WSAPI).ToString(), "Temporary Password email sending failed", EmailHelper.GetSenderAddress(), ObjRegisterInfo.Email, null, null, UserId, null, null, null);
                }

                #endregion

                objResponse.Message = Messages.SaveSuccess;
                objResponse.Status = true;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ProviderRegister", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }

            return objResponse;


        }


        /// <summary>
        /// This API used for Provider Login.
        /// </summary>
        /// <param name="ObjLoginInfo">Request object for Provider Login.</param>
        [AcceptVerbs("POST")]
        [ActionName("ProviderLogin")]
        public ProviderLoginResponse ProviderLogin(ProviderLogin ObjLoginInfo)
        {
            UsersBAL objUsersBAL = new UsersBAL();
            LogingHelper.SaveAuditInfo();

            ProviderLoginResponse objResponse = new ProviderLoginResponse();
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
                string ValidationResponse = "";

                if (!string.IsNullOrEmpty(ValidationResponse))
                {
                    objResponse.Message = "Validation Error";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }


            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo("", ex, "Login", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;

            }
            return objResponse;


        }
    }
}