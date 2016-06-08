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
using LAPP.WS.ValidateController.User;

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

            LogingHelper.SaveAuditInfo();

            BaseEntityServiceResponse objResponse = new BaseEntityServiceResponse();

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

            try
            {
                List<ResponseReason> ObjReasonList = new List<ResponseReason>();

                ObjReasonList = Validations.IsValidEmailFromUser(nameof(ObjRegisterInfo.Email), ObjRegisterInfo.Email, ObjReasonList);

                if (ObjReasonList.Count() > 0)
                {
                    objResponse.Message = ObjReasonList[0].Message;
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";

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


            try
            {

                #region Application

                ApplicationBAL objAppBAL = new ApplicationBAL();
                Application objAppEntity = new Application();

                objAppEntity.ApplicationId = 0;
                objAppEntity.ApplicationTypeId = 3;
                objAppEntity.ApplicationStatusId = 6;
                objAppEntity.ApplicationNumber = SerialsBAL.Get_Application_Number();
                objAppEntity.StartedDate = DateTime.Now;
                objAppEntity.IsFingerprintingNotRequired = false;
                objAppEntity.IsPaymentRequired = false;
                objAppEntity.CanProvisionallyHire = false;
                objAppEntity.GoPaperless = false;
                objAppEntity.IsActive = true;
                objAppEntity.IsDeleted = false;
                objAppEntity.IsArchive = false;
                objAppEntity.CreatedBy = 0;
                objAppEntity.CreatedOn = DateTime.Now;
                objAppEntity.ApplicationGuid = Guid.NewGuid().ToString();


                objAppEntity.ApplicationStatusReasonId = null;
                objAppEntity.ApplicationSubmitMode = "";
                objAppEntity.SubmittedDate = null;
                objAppEntity.ApplicationStatusDate = null;
                objAppEntity.PaymentDeadlineDate = null;
                objAppEntity.PaymentDate = null;
                objAppEntity.ConfirmationNumber = "";
                objAppEntity.ReferenceNumber = "";
                objAppEntity.LicenseRequirementId = null;
                objAppEntity.WithdrawalReasonId = null;
                objAppEntity.LicenseTypeId = null;

                objAppEntity.ApplicationType = "";
                objAppEntity.ApplicationStatus = "";

                int ApplicationId = objAppBAL.Save_Application(objAppEntity);

                #endregion

                #region Users

                UsersBAL objUsersBAL = new UsersBAL();
                Users objEntity = new Users();

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

                objEntity.IndividualId = 0;
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
                objEntity.UserId = objUsersBAL.Individual_User_Save(objEntity);

                int UserId = objEntity.UserId;

                objEntity = objUsersBAL.Get_Users_byUsersId(objEntity.UserId);

                int IndividualId = objEntity.IndividualId;

                objResponse.Message = Messages.SaveSuccess;
                objResponse.Status = true;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");


                #endregion

                #region IndividualName

                IndividualNameBAL objIndNameBAL = new IndividualNameBAL();
                IndividualName objIndName = new IndividualName();

                objIndName.IndividualNameId = 0;
                objIndName.IndividualId = objEntity.IndividualId;
                objIndName.FirstName = ObjRegisterInfo.FirstName;
                objIndName.MiddleName = ObjRegisterInfo.MiddleName;
                objIndName.LastName = ObjRegisterInfo.LastName;
                objIndName.IndividualNameStatusId = 22;
                objIndName.IsActive = true;
                objIndName.IsDeleted = false;
                objIndName.CreatedBy = 0;
                objIndName.CreatedOn = DateTime.Now;
                objIndName.IndividualNameGuid = Guid.NewGuid().ToString();
                objIndName.IndividualNameTypeId = 13;

                int IndividualNameId = objIndNameBAL.Save_IndividualName(objIndName);

                #endregion

                #region Provider Save

                Provider objProvider = new Provider();
                ProviderBAL objProviderBAL = new ProviderBAL();

                objProvider.ProviderId = 0;
                objProvider.ProviderName = ObjRegisterInfo.SchoolName;
                objProvider.DepartmentId = 2;
                objProvider.ProviderTypeId = 1;
                objProvider.ProviderStatusTypeId = 1;
                objProvider.IsActive = true;
                objProvider.IsDeleted = false;
                objProvider.IsEnabled = false;
                objProvider.CreatedBy = 0;
                objProvider.CreatedOn = DateTime.Now;
                objProvider.ProviderGuid = Guid.NewGuid().ToString();

                int ProviderId = objProviderBAL.Save_Provider(objProvider);

                #endregion

                #region Provider Individual Name Info

                // Insert IndividualId as 0 and IndividualNameId from IndividualName table

                ProviderIndividualName objProviderIndName = new ProviderIndividualName();
                ProviderIndividualNameBAL objProviderIndNameBAL = new ProviderIndividualNameBAL();

                objProviderIndName.ProviderIndvNameInfoId = 0;
                objProviderIndName.ProviderId = ProviderId;
                objProviderIndName.IndividualId = 0;
                objProviderIndName.IndividualNameId = IndividualNameId;
                objProviderIndName.ApplicationId = ApplicationId;
                objProviderIndName.IsActive = true;
                objProviderIndName.IsDeleted = false;
                objProviderIndName.CreatedBy = 0;
                objProviderIndName.CreatedOn = DateTime.Now;
                objProviderIndName.ProviderIndvNameInfoGuid = Guid.NewGuid().ToString();

                objProviderIndName.ProviderIndvNameInfoId = objProviderIndNameBAL.Save_ProviderIndividualName(objProviderIndName);

                #endregion

                #region User Role Save

                UserRole objRole = new UserRole();
                UserRoleBAL objRoleBAL = new UserRoleBAL();

                objRole = new UserRole();
                objRole.RoleId = 9;
                objRole.UserId = UserId;
                objRole.IsActive = true;
                objRole.IsGrantable = true;
                objRole.CreatedBy = 1;
                objRole.CreatedOn = DateTime.Now;
                objRole.IsDeleted = false;

                int UserRoleId = objRoleBAL.Save_UserRole(objRole);

                #endregion

                #region User Provider Save

                ProviderUser objProviderUser = new ProviderUser();
                ProviderUserBAL objProviderUserBAL = new ProviderUserBAL();

                objProviderUser.ProviderUserId = 0;
                objProviderUser.ProviderId = ProviderId;
                objProviderUser.ApplicationId = ApplicationId;
                objProviderUser.UserId = UserId;
                objProviderUser.IndividualId = IndividualId;
                objProviderUser.IndividualNameId = IndividualNameId;
                objProviderUser.StartDate = null;
                objProviderUser.EndDate = null;
                objProviderUser.IsActive = true;
                objProviderUser.IsDeleted = false;
                objProviderUser.CreatedBy = 0;
                objProviderUser.CreatedOn = DateTime.Now;
                objProviderUser.ProviderUserGuid = Guid.NewGuid().ToString();

                int ProviderUserId = objProviderUserBAL.Save_ProviderUser(objProviderUser);

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
        /// <param name="ObjProviderLogin">Request object for Provider Login.</param>
        /// <param name="Key">API Security Key</param>
        [AcceptVerbs("POST")]
        [ActionName("ProviderLogin")]
        public ProviderLoginResponse ProviderLogin(string Key, ProviderLogin ObjProviderLogin)
        {
            int CreateOrModify = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo();

            ProviderLoginResponse objResponse = new ProviderLoginResponse();

            UsersBAL objUserBAL = new UsersBAL();
            ProviderUserBAL objProviderUserBAL = new ProviderUserBAL();
            ApplicationStatusBAL objApplicationStatusBAL = new ApplicationStatusBAL();
            Users objEntity = new Users();
            List<UsersRequest> lstEntity = new List<UsersRequest>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Message = "User session has expired.";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.ResponseReason = "";
                return objResponse;
            }

            if (ObjProviderLogin == null)
            {
                objResponse.Message = "Invalid Object.";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                objResponse.ResponseReason = "";
                return objResponse;
            }

            try
            {
                Users user = objUserBAL.Get_Users_by_Email_And_Password(ObjProviderLogin.Email, ObjProviderLogin.Password);

                string TokenKey = TokenHelper.GenrateToken(user.UserId, "");

                if (user != null)
                {
                    if (user.TemporaryPassword) //i.e if the user is login for the first time with the password received on email
                    {
                        ProviderUser ObjEntityProviderUser = objProviderUserBAL.Get_ProviderUser_By_UserId(user.UserId);//user.UserId
                        ApplicationStatus ObjEntityApplicationStatus = objApplicationStatusBAL.Get_ApplicationStatus_byApplicationId(ObjEntityProviderUser.ApplicationId);//ObjEntityProviderUser.ApplicationId
                        int dateCheck = DateTime.Compare((DateTime)user.PasswordExpirationDate, DateTime.Now);
                        if ((dateCheck >= 0)) // checking if the expiration date of the password received is greater than the date when user is trying to login first time
                        {
                            objResponse.IsPasswordTemporary = true;
                            objResponse.ProviderId = ObjEntityProviderUser.ProviderId;
                            objResponse.IndividualId = user.IndividualId;
                            objResponse.UserId = user.UserId;
                            objResponse.IndividualNameId = ObjEntityProviderUser.IndividualNameId;
                            objResponse.ApplicationId = ObjEntityProviderUser.ApplicationId;
                            objResponse.ApplicationStatus = ObjEntityApplicationStatus.Name;
                            objResponse.Key = TokenKey;

                            objResponse.Message = Messages.SaveSuccess;
                            objResponse.Status = true;
                            objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                            objResponse.ResponseReason = "First Time Login.";
                        }
                        else
                        {
                            objResponse.Message = "Password Expired, Change Password.";
                            objResponse.Status = false;
                            objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                            objResponse.ResponseReason = "";
                            return objResponse;
                        }
                    }
                    else
                    {
                        ProviderUser ObjEntityProviderUser = objProviderUserBAL.Get_ProviderUser_By_UserId(user.UserId);//
                        ApplicationStatus ObjEntityApplicationStatus = objApplicationStatusBAL.Get_ApplicationStatus_byApplicationId(ObjEntityProviderUser.ApplicationId);
                        objResponse.IsPasswordTemporary = false;
                        objResponse.ProviderId = ObjEntityProviderUser.ProviderId;
                        objResponse.IndividualId = user.IndividualId;
                        objResponse.UserId = user.UserId;
                        objResponse.IndividualNameId = ObjEntityProviderUser.IndividualNameId;
                        objResponse.ApplicationId = ObjEntityProviderUser.ApplicationId;
                        objResponse.ApplicationStatus = ObjEntityApplicationStatus.Name;
                        objResponse.Key = TokenKey;

                        objResponse.Message = Messages.SaveSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                        objResponse.ResponseReason = "";
                        return objResponse;
                    }
                }
                else
                {
                    objResponse.IsPasswordTemporary = false;
                    objResponse.Message = "Invalid Object.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ProviderLogin", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;


        }
    }
}