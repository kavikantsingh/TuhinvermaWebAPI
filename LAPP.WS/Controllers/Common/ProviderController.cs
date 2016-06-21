using LAPP.BAL;
using LAPP.BAL.Backoffice.IndividualFolder;
using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.GlobalFunctions;
using LAPP.LOGING;
using LAPP.WS.App_Helper;
using LAPP.WS.App_Helper.Common;
using LAPP.WS.ValidateController.Backoffice;
using LAPP.WS.ValidateController.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using EO.Pdf;
using System.Drawing;
using System.Net.Http.Headers;
using System.Web.Http.Description;
using System.Data.OleDb;
using System.Data;
using LAPP.WS.Controllers.Common;

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

                string mailContent = "The email address has been registered. The temporary password has been sent to the email address used for Registration. ";
                mailContent += "Please check your email address.";
                mailContent += "<br/> <br/>";
                mailContent += "Temporary Password: " + TempPassword;
                mailContent += "<br/> <br/>";
                mailContent += "If you are missing emails from California Massage Therapy Council, please check email accounts <u>Spam</u> or <u>Junk</u> folder to ensure ";
                mailContent += "email message was not filtered.If the message is in Spam or Junk folder, click Not Spam or Not Junk after selecting the message, ";
                mailContent += "which will allow future email messages from California Massage Therapy Council to be delivered to your Inbox.";
                mailContent += "<br/><br/><br/>";
                mailContent += "Thank you.";
                mailContent += "<br/><br/>";
                mailContent += "California Massage Therapy Council";

                //if (EmailHelper.SendMail(ObjRegisterInfo.Email, "Temporary Password", "Email: " + ObjRegisterInfo.Email + " <br/> Temporary Password: " + TempPassword, true))
                if (EmailHelper.SendMail(ObjRegisterInfo.Email, "Temporary Password", mailContent, true))
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

                
                if (user != null)
                {
                    string TokenKey = TokenHelper.GenrateToken(user.UserId, "");

                    if (user.TemporaryPassword) //i.e if the user login for the first time with the password received on email
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
                    objResponse.Message = "Login Failed. Invalid Credential.";
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

        #region ProviderDocument
        /// <summary>
        /// This API is used to GET Provider Document based on Provider Id and Document Id
        /// </summary>
        /// <param name="Key">API Security Key</param>
        /// <param name="ProviderId">Provider Id.</param>
        /// <param name="DocumentId">Document Id</param>
        [AcceptVerbs("GET")]
        [ActionName("ProviderGetProviderDocumentByProviderIdAndDocumentId")]
        public ProviderDocumentGETResponse ProviderGetProviderDocumentByProviderIdAndDocumentId(string Key, int ProviderId, int DocumentId)
        {
            LogingHelper.SaveAuditInfo(Key);

            ProviderDocumentGETResponse objResponse = new ProviderDocumentGETResponse();
            ProviderDocumentBAL objBAL = new ProviderDocumentBAL();
            ProviderDocumentGET objEntity = new ProviderDocumentGET();
            List<ProviderDocumentGET> lstProviderDocumentGET = new List<ProviderDocumentGET>();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ProviderDocumentGET = null;
                    return objResponse;
                }
                lstProviderDocumentGET = objBAL.Get_ProviderDocument_By_ProviderId_DocumentId(ProviderId, DocumentId);
                if (lstProviderDocumentGET != null && lstProviderDocumentGET.Count > 0)
                {
                    objResponse.ResponseReason = "To Get ProviderDocument+DocumentMaster Records";
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstProviderDocumentGETSelected = lstProviderDocumentGET.Select(ProvDoc => new ProviderDocumentGET
                    {
                        ProviderId = ProvDoc.ProviderId,
                        DocumentId = ProvDoc.DocumentId,
                        ProviderDocumentId = ProvDoc.ProviderDocumentId,
                        DocumentTypeIdName = ProvDoc.DocumentTypeIdName,
                        DocumentTypeDesc = ProvDoc.DocumentTypeDesc,
                        DocumentName = ProvDoc.DocumentName,
                        OtherDocumentTypeName = ProvDoc.OtherDocumentTypeName,
                        DocumentTypeId = ProvDoc.DocumentTypeId,
                        DocumentPath = ProvDoc.DocumentPath
                    }
                    ).ToList();

                    objResponse.ProviderDocumentGET = lstProviderDocumentGETSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ProviderDocumentGET = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ProviderGetProviderDocumentByProviderIdAndDocumentId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ProviderDocumentGET = null;
            }

            return objResponse;
        }

        /// <summary>
        /// This API is used to GET Provider Document based on Provider Id and Document Id
        /// </summary>
        /// <param name="Key">API Security Key</param>
        /// <param name="ProviderId">Provider Id.</param>
        /// <param name="DocumentId">Document Id</param>
        /// <param name="ApplicationId">Application Id</param>
        [AcceptVerbs("GET")]
        [ActionName("ProviderGetProviderDocumentByProviderIdAndDocumentId")]
        public ProviderDocumentGETResponse ProviderGetProviderDocumentByProviderIdDocumentIdAndApplcationId(string Key, int ProviderId, int DocumentId, int ApplicationId)
        {
            LogingHelper.SaveAuditInfo(Key);

            ProviderDocumentGETResponse objResponse = new ProviderDocumentGETResponse();
            ProviderDocumentBAL objBAL = new ProviderDocumentBAL();
            ProviderDocumentGET objEntity = new ProviderDocumentGET();
            List<ProviderDocumentGET> lstProviderDocumentGET = new List<ProviderDocumentGET>();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ProviderDocumentGET = null;
                    return objResponse;
                }
                lstProviderDocumentGET = objBAL.Get_ProviderDocument_By_ProviderId_DocumentId_ApplicationId(ProviderId, DocumentId, ApplicationId);
                if (lstProviderDocumentGET != null && lstProviderDocumentGET.Count > 0)
                {
                    objResponse.ResponseReason = "To Get ProviderDocument+DocumentMaster Records";
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstProviderDocumentGETSelected = lstProviderDocumentGET.Select(ProvDoc => new ProviderDocumentGET
                    {
                        ProviderId = ProvDoc.ProviderId,
                        DocumentId = ProvDoc.DocumentId,
                        ProviderDocumentId = ProvDoc.ProviderDocumentId,
                        DocumentTypeIdName = ProvDoc.DocumentTypeIdName,
                        DocumentTypeDesc = ProvDoc.DocumentTypeDesc,
                        DocumentName = ProvDoc.DocumentName,
                        OtherDocumentTypeName = ProvDoc.OtherDocumentTypeName,
                        DocumentTypeId = ProvDoc.DocumentTypeId,
                        DocumentPath = ProvDoc.DocumentPath
                    }
                    ).ToList();

                    objResponse.ProviderDocumentGET = lstProviderDocumentGETSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ProviderDocumentGET = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ProviderGetProviderDocumentByProviderIdAndDocumentId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ProviderDocumentGET = null;
            }

            return objResponse;
        }

        /// <summary>
        /// This API is used to GET Provider Document based on Provider Id and Document Id
        /// </summary>
        /// <param name="Key">API Security Key</param>
        /// <param name="objProviderDocument">object ProviderDocument</param>
        /// <param name="Base64Str">File Base64 String</param>
        /// <param name="ext">File extension</param>
        [AcceptVerbs("POST")]
        [ActionName("ProviderDocumentSave")]
        public ProviderDocumentGETResponse ProviderDocumentSave(string Key, ProviderDocument objProviderDocument)
        {
            int CreateOrModify = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo();
            ProviderDocumentGETResponse objResponse = new ProviderDocumentGETResponse();
            ProviderDocument objEntity = new ProviderDocument();
            ProviderDocumentBAL objBAL = new ProviderDocumentBAL();
            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Message = "User session has expired.";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.ResponseReason = "";
                return objResponse;
            }

            try
            {
                if (objProviderDocument != null)
                {
                    objEntity.ProviderDocumentId = objProviderDocument.ProviderDocumentId;
                    objEntity.ProviderId = objProviderDocument.ProviderId;
                    objEntity.ApplicationId = objProviderDocument.ApplicationId;
                    objEntity.DocumentId=objProviderDocument.DocumentId;
                    objEntity.DocumentCd= objProviderDocument.DocumentCd;
                    objEntity.DocumentTypeId= objProviderDocument.DocumentTypeId;
                    objEntity.DocumentLkToPageTabSectionId= objProviderDocument.DocumentLkToPageTabSectionId;
                    objEntity.DocumentLkToPageTabSectionCode= objProviderDocument.DocumentLkToPageTabSectionCode;
                    objEntity.DocumentName= objProviderDocument.DocumentName;
                    objEntity.OtherDocumentTypeName= objProviderDocument.OtherDocumentTypeName;
                    objEntity.DocumentPath= objProviderDocument.DocumentPath;
                    objEntity.EffectiveDate= objProviderDocument.EffectiveDate;
                    objEntity.EndDate= objProviderDocument.EndDate;
                    objEntity.IsDocumentUploadedbyProvider= objProviderDocument.IsDocumentUploadedbyProvider;
                    objEntity.IsDocumentUploadedbyStaff= objProviderDocument.IsDocumentUploadedbyStaff;
                    objEntity.ReferenceNumber= objProviderDocument.ReferenceNumber;
                    objEntity.IsActive= objProviderDocument.IsActive;
                    objEntity.IsDeleted= objProviderDocument.IsDeleted;
                    objEntity.CreatedBy= objProviderDocument.CreatedBy;
                    objEntity.CreatedOn= objProviderDocument.CreatedOn;
                    objEntity.ModifiedBy= objProviderDocument.ModifiedBy;
                    objEntity.ModifiedOn= objProviderDocument.ModifiedOn;
                    objEntity.ProviderDocumentGuid= Guid.NewGuid().ToString();
                    objEntity.Base64Str = objProviderDocument.Base64Str;
                    objEntity.Extension = objProviderDocument.Extension;
                    //--file save process--//
                    string FilePath = ProviderDocumentSaveProcess(objEntity.Base64Str, objEntity.DocumentName, objEntity.Extension);
                    //--file save process--//
                    if(FilePath!=null)
                    { 
                        int ReturnProviderId=objBAL.Save_ProviderDocument(objEntity);
                        List<ProviderDocumentGET> lstTempProviderDoccumentGET = new List<ProviderDocumentGET>();
                        ProviderDocumentGET objTempEntity = new ProviderDocumentGET();
                        objTempEntity.ProviderDocumentId = ReturnProviderId;
                        lstTempProviderDoccumentGET.Add(objTempEntity);
                        objResponse.ProviderDocumentGET = lstTempProviderDoccumentGET;
                        objResponse.Message = "Successful";
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                        objResponse.ResponseReason = "";
                    }


                }
                else
                {
                    objResponse.Message = "ProviderDocument object cannot be null.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
            }
            catch(Exception ex)
            {
                LogingHelper.SaveExceptionInfo("", ex, "Login", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
            }
            return objResponse;
        }

        private string ProviderDocumentSaveProcess(string Base64Str, string filename, string extension)
        {
            try
            { 
                if (Base64Str!=null && filename!=null && extension!=null)
                {
                    string FilePath= ConfigurationHelper.GetConfigurationValueBySetting("RootDocumentPath") + "SchoolApplication\\";
                    //FilePath = @"D:\sample workspace\stuffstestfolder";
                    Byte[] bytes = Convert.FromBase64String(Base64Str);
                    File.WriteAllBytes(FilePath + filename + extension, bytes);
                    return FilePath + filename + extension;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
            return null;
        }

        /// <summary>
        /// This API is used to GET Provider Document based on Provider Id and Document Id
        /// </summary>
        /// <param name="Key">API Security Key</param>
        /// <param name="ProviderDocId">ProviderDocument Id.</param>
        /// <param name="UserId">User Id</param>
        /// <param name="ProviderId">Provider Id</param>
        [AcceptVerbs("POST")]
        [ActionName("ProviderDocumentDelete")]
        public ProviderDocumentResponse ProviderDocumentDelete(string Key, int? ProviderDocId, int? UserId, int? ProviderId)
        {
            int CreateOrModify = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo();

            ProviderDocumentResponse objResponse = new ProviderDocumentResponse();

            ProviderDocumentBAL objProviderDocumentBAL = new ProviderDocumentBAL();
            ProviderDocument objEntity = new ProviderDocument();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Message = "User session has expired.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
                if (ProviderDocId != null&&UserId!=null&&ProviderId!=null)// && CourseTitleName!=null && CourseHours!=null)
                {
                    objEntity.IsActive = false;
                    objEntity.IsDeleted = true;
                    objEntity.ModifiedBy = UserId;
                    objEntity.ModifiedOn = DateTime.Now;
                    objProviderDocumentBAL.Delete_ProviderDocument_By_ProviderDocId_And_ProviderId(ProviderDocId, ProviderId, UserId);
                    objResponse.Message = Messages.DeleteSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }
                else
                {
                    objResponse.Message = "Invalid Object.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo("", ex, "ProviderDocumentDelete", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
            }
            return objResponse;
        }

        /// <summary>
        /// This API is used to GET Provider Document based on Provider Id and Document Id
        /// </summary>
        /// <param name="Key">API Security Key</param>
        /// <param name="ProviderDocId">ProviderDocument Id.</param>
        /// <param name="UserId">User Id</param>
        /// <param name="ProviderId">Provider Id</param>
        /// <param name="ApplicationId">Application Id</param>
        [AcceptVerbs("POST")]
        [ActionName("ProviderDocumentDelete")]
        public ProviderDocumentResponse ProviderDocumentDelete(string Key, int? ProviderDocId, int? UserId, int? ProviderId, int? ApplicationId)
        {
            int CreateOrModify = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo();

            ProviderDocumentResponse objResponse = new ProviderDocumentResponse();

            ProviderDocumentBAL objProviderDocumentBAL = new ProviderDocumentBAL();
            ProviderDocument objEntity = new ProviderDocument();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Message = "User session has expired.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
                if (ProviderDocId != null && UserId != null && ProviderId != null && ApplicationId!=null)// && CourseTitleName!=null && CourseHours!=null)
                {
                    objEntity.IsActive = false;
                    objEntity.IsDeleted = true;
                    objEntity.ModifiedBy = UserId;
                    objEntity.ModifiedOn = DateTime.Now;
                    objProviderDocumentBAL.Delete_ProviderDocument_By_ProviderDocId_ProviderId_And_ApplicationId(ProviderDocId, UserId, ProviderId, ApplicationId);//Delete_ProviderDocument_By_ProviderDocId_And_ProviderId(ProviderDocId, ProviderId, UserId);

                    ProviderDocument tempEntity = new ProviderDocument();
                    tempEntity.ProviderId = (int)ProviderId;
                    objResponse.ProviderDocument = tempEntity;
                    objResponse.Message = Messages.DeleteSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }
                else
                {
                    objResponse.Message = "Invalid Object.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo("", ex, "ProviderDocumentDelete", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
            }
            return objResponse;
        }


        #endregion ProviderDocument

        /// <summary>
        /// This method is to return the current tab that is active and show tick to all other tabs
        /// </summary>
        /// <param name="objProviderInstruction">Request object for Provider Instruction.</param>
        [AcceptVerbs("POST")]
        [ActionName("CheckInitialTabActive")]
        public ProviderLoginResponse CheckInitialTabActive(ProviderInstructions objProviderInstruction)
        {
            ProviderLoginResponse objResponse = new ProviderLoginResponse();
            if (objProviderInstruction == null)
            {
                objResponse.Message = "Invalid Object.";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                objResponse.ResponseReason = "";
                return objResponse;
            }

            try
            {
                //Method to Save click information on Provider Instructor Table
                ProviderInstructionsBAL objProviderInstructionBAL = new ProviderInstructionsBAL();
                int output = objProviderInstructionBAL.CheckInitialTabActive(objProviderInstruction.ApplicationId, objProviderInstruction.ProviderId);

                string ValidationResponse = "";

                if (output > 0)
                {
                    objResponse.Message = "Success";
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
                else
                {
                    objResponse.Message = output.ToString();
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                // LogingHelper.SaveExceptionInfo("", ex, "Login", ENTITY.Enumeration.eSeverity.Error);
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;

            }
            return objResponse;


        }

        /// <summary>
        /// Save button Click that get invoked from Save button click of Instructor tab of Application 
        /// </summary>
        /// <param name="objProviderInstruction">Request object for Provider Instruction.</param>
        [AcceptVerbs("POST")]
        [ActionName("SaveButtonOfInstructions")]
        public ProviderLoginResponse SaveButtonOfInstructions(ProviderInstructions objProviderInstruction)
        {

            ProviderLoginResponse objResponse = new ProviderLoginResponse();
            if (objProviderInstruction == null)
            {
                objResponse.Message = "Invalid Object.";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                objResponse.ResponseReason = "";
                return objResponse;
            }

            try
            {
                //Method to Save click information on Provider Instructor Table
                ProviderInstructionsBAL objProviderInstructionBAL = new ProviderInstructionsBAL();
                int output = objProviderInstructionBAL.SaveButtonOfInstructions(objProviderInstruction);

                string ValidationResponse = "";

                if (output > 0)
                {
                    objResponse.Message = "Success";
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
                else
                {
                    objResponse.Message = output.ToString();
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                // LogingHelper.SaveExceptionInfo("", ex, "Login", ENTITY.Enumeration.eSeverity.Error);
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;

            }
            return objResponse;


        }

        /// <summary>
        /// This method is to Add Previous Schools to the table
        /// </summary>
        /// <param name="objProviderNames">Request object for Provider Name.</param>
        [AcceptVerbs("POST")]
        [ActionName("AddPreviousSchoolInSchoolInformation")]
        public ProviderPreviousSchoolResponse AddPreviousSchoolInSchoolInformation(ProviderNames objProviderNames)
        {
            ProviderPreviousSchoolResponse objResponse = new ProviderPreviousSchoolResponse();
            if (objProviderNames == null)
            {
                objResponse.Message = "Invalid Object.";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                objResponse.ResponseReason = "";
                return objResponse;
            }

            try
            {
                //Method to Save click information on Provider Instructor Table
                ProviderInstructionsBAL objProviderInstructionBAL = new ProviderInstructionsBAL();
                int output = objProviderInstructionBAL.SavePreviousSchoolDetails(objProviderNames);

                string ValidationResponse = "";

                if (output > 0)
                {
                    objResponse.Message = "Success";
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";

                    List<ProviderNames> lstPrevSchools = objProviderInstructionBAL.GetAllPreviousSchools(objProviderNames.ApplicationId);
                    objResponse.ListOfPreviousSchool = lstPrevSchools;

                    return objResponse;
                }
                else
                {
                    objResponse.Message = output.ToString();
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    objResponse.ListOfPreviousSchool = null;
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                // LogingHelper.SaveExceptionInfo("", ex, "Login", ENTITY.Enumeration.eSeverity.Error);
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ListOfPreviousSchool = null;
            }
            return objResponse;


        }

        /// <summary>
        /// This method is to Save the Address data comes in School Information 
        /// </summary>
        /// <param name="objAddress">Request object for Provider Instruction.</param>
        [AcceptVerbs("POST")]
        [ActionName("SaveAddressRequestFromSchoolInformationTab")]
        public ProviderAddressResponse SaveAddressRequestFromSchoolInformationTab(Address objAddress)
        {
            ProviderAddressResponse objResponse = new ProviderAddressResponse();
            if (objAddress == null)
            {
                objResponse.Message = "Invalid Object.";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                objResponse.ResponseReason = "";
                objResponse.ListOfPreviousAddress = null;
                return objResponse;
            }

            try
            {
                //Method to Save Address based on different address Types
                AddressBAL objAddressBAL = new AddressBAL();
                int output = objAddressBAL.SaveAddressRequestFromSchoolInformationTab(objAddress);

                string ValidationResponse = "";

                if (output > 0)
                {
                    objResponse.Message = "Success";
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";

                    //Method to Get all the previous schools 
                    List<Address> lstPrevAddress = objAddressBAL.GetAllPreviousAddress(objAddress.AddressTypeId, objAddress.ProviderId);
                    objResponse.ListOfPreviousAddress = lstPrevAddress;

                    return objResponse;
                }
                else
                {
                    objResponse.Message = output.ToString();
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    objResponse.ListOfPreviousAddress = null;
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                // LogingHelper.SaveExceptionInfo("", ex, "Login", ENTITY.Enumeration.eSeverity.Error);
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ListOfPreviousAddress = null;

            }
            return objResponse;


        }

        /// <summary>
        /// This method is to Save the School Informations
        /// </summary>
        /// <param name="objSchoolInformation">Request object for Provider Instruction.</param>
        [AcceptVerbs("POST")]
        [ActionName("SaveSchoolInformation")]
        public ProviderLoginResponse SaveSchoolInformation(ProviderInformation objSchoolInformation)
        {
            ProviderLoginResponse objResponse = new ProviderLoginResponse();
            if (objSchoolInformation == null)
            {
                objResponse.Message = "Invalid Object.";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                objResponse.ResponseReason = "";
                return objResponse;
            }

            try
            {
                //Method to Save Address based on different address Types
                //Update school Telephone and School Website in Contact and ProviderContact Table
                ContactBAL objContactBAL = new ContactBAL();
                AddressBAL objAddressBAL = new AddressBAL();
                IndividualBAL objIndividualBAL = new IndividualBAL();

                ProviderInformation objSchoolTelephone = new ProviderInformation();
                objSchoolTelephone.ContactTypeId = 9;
                objSchoolTelephone.ContactInfo = objSchoolInformation.SchoolTelephone;
                objSchoolTelephone.CreatedBy = objSchoolInformation.CreatedBy;
                objSchoolTelephone.ProviderId = objSchoolInformation.ProviderId;
                objSchoolTelephone.IsPreferredContact = objSchoolInformation.IsPreferredContact;
                objSchoolTelephone.IsMobile = objSchoolInformation.IsMobile;
                objContactBAL.Save_ContactAndProviderContact(objSchoolTelephone);

                ProviderInformation objSchoolWebSite = new ProviderInformation();
                objSchoolWebSite.ContactTypeId = 9; //Change it
                objSchoolWebSite.ContactInfo = objSchoolInformation.SchoolWebsite;
                objSchoolWebSite.CreatedBy = objSchoolInformation.CreatedBy;
                objSchoolWebSite.ProviderId = objSchoolInformation.ProviderId;
                objSchoolWebSite.IsPreferredContact = objSchoolInformation.IsPreferredContact;
                objSchoolWebSite.IsMobile = objSchoolInformation.IsMobile;
                objContactBAL.Save_ContactAndProviderContact(objSchoolWebSite);

                //Update School Address, Mailing Address to Address Table and provideraddress table
                Address objSchoolAddress = new Address();
                objSchoolAddress.AddressId = objSchoolInformation.AddressId;
                objSchoolAddress.StreetLine1 = objSchoolInformation.SchoolAddressStreet1;
                objSchoolAddress.StreetLine2 = objSchoolInformation.SchoolAddressStreet2;
                objSchoolAddress.City = objSchoolInformation.SchoolAddressCity;
                objSchoolAddress.StateCode = objSchoolInformation.SchoolAddressState;
                objSchoolAddress.Zip = objSchoolInformation.SchoolAddressZip;
                objSchoolAddress.DateValidated = objSchoolInformation.DateValidated;
                objSchoolAddress.UseUserAddress = objSchoolInformation.UseUserAddress;
                objSchoolAddress.UseVerifiedAddress = objSchoolInformation.UseVerifiedAddress;
                objSchoolAddress.CreatedBy = objSchoolInformation.CreatedBy;
                objSchoolAddress.ProviderId = objSchoolInformation.ProviderId;
                objSchoolAddress.AddressTypeId = 6; //Need to set correct value
                objSchoolAddress.IsMailingSameasPhysical = objSchoolInformation.IsMailingSameasPhysical;
                objAddressBAL.SaveAddressRequestFromSchoolInformationTab(objSchoolAddress);

                Address objMailingAddress = new Address();
                objMailingAddress.AddressId = objSchoolInformation.AddressId;
                objMailingAddress.StreetLine1 = objSchoolInformation.MailingAddressStreet1;
                objMailingAddress.StreetLine2 = objSchoolInformation.MailingAddressStreet2;
                objMailingAddress.City = objSchoolInformation.MailingAddressCity;
                objMailingAddress.StateCode = objSchoolInformation.MailingAddressState;
                objMailingAddress.Zip = objSchoolInformation.MailingAddressZip;
                objMailingAddress.DateValidated = objSchoolInformation.DateValidated;
                objMailingAddress.UseUserAddress = objSchoolInformation.UseUserAddress;
                objMailingAddress.UseVerifiedAddress = objSchoolInformation.UseVerifiedAddress;
                objMailingAddress.CreatedBy = objSchoolInformation.CreatedBy;
                objMailingAddress.ProviderId = objSchoolInformation.ProviderId;
                objMailingAddress.AddressTypeId = 1;
                objMailingAddress.IsMailingSameasPhysical = objSchoolInformation.IsMailingSameasPhysical;
                objAddressBAL.SaveAddressRequestFromSchoolInformationTab(objMailingAddress);

                //Director Name, Job Title & Contact Name for this Application
                IndividualName objDirectorName = new IndividualName();
                objDirectorName.FirstName = objSchoolInformation.DirectorFirstName;
                objDirectorName.LastName = objSchoolInformation.DirectorLastName;
                objDirectorName.CreatedBy = objSchoolInformation.CreatedBy;
                objDirectorName.ProvIndvJobTitle = objSchoolInformation.DirectorJobTitle;
                objDirectorName.IndividualNameTypeId = 1;
                objDirectorName.ProviderId = objSchoolInformation.ProviderId;
                objDirectorName.ApplicationId = objSchoolInformation.ApplicationId;
                objDirectorName.IndividualId = objSchoolInformation.IndividualId;
                objIndividualBAL.Save_IndividualProvider(objDirectorName);

                IndividualName objContactName = new IndividualName();
                objContactName.FirstName = objSchoolInformation.ContactNameFirstName;
                objContactName.LastName = objSchoolInformation.ContactNameLastName;
                objContactName.CreatedBy = objSchoolInformation.CreatedBy;
                objDirectorName.ProvIndvJobTitle = objSchoolInformation.ContactNameJobTitle;
                objContactName.IndividualNameTypeId = 1;
                objContactName.ProviderId = objSchoolInformation.ProviderId;
                objContactName.ApplicationId = objSchoolInformation.ApplicationId;
                objDirectorName.IndividualId = objSchoolInformation.IndividualId;
                objIndividualBAL.Save_IndividualProvider(objContactName);

                //Save Phone number for Primary Number and Director
                ProviderInformation objDirectorPrimaryTelephone = new ProviderInformation();
                objSchoolTelephone.ContactTypeId = 8; //11 chnage
                objSchoolTelephone.ContactInfo = objSchoolInformation.DirectorPrimaryNumber;
                objSchoolTelephone.CreatedBy = objSchoolInformation.CreatedBy;
                objSchoolTelephone.ProviderId = objSchoolInformation.ProviderId;
                objSchoolTelephone.IsPreferredContact = objSchoolInformation.IsPreferredContact;
                objSchoolTelephone.IsMobile = objSchoolInformation.DirectorPrimaryNumberIsMobile;
                objContactBAL.Save_ContactAndProviderContact(objSchoolTelephone);

                //Save Phone number for Secondary Number and Director
                ProviderInformation objDirectorSecondaryTelephone = new ProviderInformation();
                objDirectorSecondaryTelephone.ContactTypeId = 8; //12
                objDirectorSecondaryTelephone.ContactInfo = objSchoolInformation.DirectorSecondaryNumber;
                objDirectorSecondaryTelephone.CreatedBy = objSchoolInformation.CreatedBy;
                objDirectorSecondaryTelephone.ProviderId = objSchoolInformation.ProviderId;
                objDirectorSecondaryTelephone.IsPreferredContact = objSchoolInformation.IsPreferredContact;
                objDirectorSecondaryTelephone.IsMobile = objSchoolInformation.DirectorSecondaryNumberIsMobile;
                objContactBAL.Save_ContactAndProviderContact(objDirectorSecondaryTelephone);

                //Save Administrator Email for Director
                ProviderInformation objDirectorEmail = new ProviderInformation();
                objDirectorEmail.ContactTypeId = 8; //13
                objDirectorEmail.ContactInfo = objSchoolInformation.DirectorAdministratorEmail;
                objDirectorEmail.CreatedBy = objSchoolInformation.CreatedBy;
                objDirectorEmail.ProviderId = objSchoolInformation.ProviderId;
                objDirectorEmail.IsPreferredContact = objSchoolInformation.IsPreferredContact;
                objDirectorEmail.IsMobile = objSchoolInformation.IsMobile;
                objContactBAL.Save_ContactAndProviderContact(objDirectorEmail);

                //Save Phone number for Primary Number of Contact
                ProviderInformation objContactPrimaryTelephone = new ProviderInformation();
                objContactPrimaryTelephone.ContactTypeId = 8; //14
                objContactPrimaryTelephone.ContactInfo = objSchoolInformation.DirectorPrimaryNumber;
                objContactPrimaryTelephone.CreatedBy = objSchoolInformation.CreatedBy;
                objContactPrimaryTelephone.ProviderId = objSchoolInformation.ProviderId;
                objContactPrimaryTelephone.IsPreferredContact = objSchoolInformation.IsPreferredContact;
                objContactPrimaryTelephone.IsMobile = objSchoolInformation.DirectorPrimaryNumberIsMobile;
                objContactBAL.Save_ContactAndProviderContact(objContactPrimaryTelephone);

                //Save Phone number for Secondary Number of Contact
                ProviderInformation objContactSecondaryTelephone = new ProviderInformation();
                objContactSecondaryTelephone.ContactTypeId = 8; //15
                objContactSecondaryTelephone.ContactInfo = objSchoolInformation.DirectorSecondaryNumber;
                objContactSecondaryTelephone.CreatedBy = objSchoolInformation.CreatedBy;
                objContactSecondaryTelephone.ProviderId = objSchoolInformation.ProviderId;
                objContactSecondaryTelephone.IsPreferredContact = objSchoolInformation.IsPreferredContact;
                objContactSecondaryTelephone.IsMobile = objSchoolInformation.DirectorSecondaryNumberIsMobile;
                objContactBAL.Save_ContactAndProviderContact(objContactSecondaryTelephone);

                //Save Administrator Email of Contact
                ProviderInformation objContactEmail = new ProviderInformation();
                objContactEmail.ContactTypeId = 8; //16
                objContactEmail.ContactInfo = objSchoolInformation.DirectorAdministratorEmail;
                objContactEmail.CreatedBy = objSchoolInformation.CreatedBy;
                objContactEmail.ProviderId = objSchoolInformation.ProviderId;
                objContactEmail.IsPreferredContact = objSchoolInformation.IsPreferredContact;
                objContactEmail.IsMobile = objSchoolInformation.IsMobile;
                objContactBAL.Save_ContactAndProviderContact(objContactEmail);


                int output = 5;
                string ValidationResponse = "";

                if (output > 0)
                {
                    objResponse.Message = "Success";
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
                else
                {
                    objResponse.Message = output.ToString();
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                // LogingHelper.SaveExceptionInfo("", ex, "Login", ENTITY.Enumeration.eSeverity.Error);
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;

            }
            return objResponse;


        }

        /// <summary>
        /// This method is to Save the School Informations
        /// </summary>
        /// <param name="objSchoolInformation">Request object for Provider Instruction.</param>
        [AcceptVerbs("POST")]
        [ActionName("GetAllSchoolInformationDetails")]
        public ProviderOnLoadResponse GetAllSchoolInformationDetails(ProviderInformation objSchoolInformation)
        {
            ProviderOnLoadResponse objResponse = new ProviderOnLoadResponse();
            if (objSchoolInformation == null)
            {
                objResponse.Message = "Invalid Object.";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                objResponse.ResponseReason = "";
                return objResponse;
            }
            try
            {
                ProviderInformation objOutPutProviderInformation = new ProviderInformation();
                AddressBAL objAddressBAL = new AddressBAL();
                ProviderInstructionsBAL objProviderInstructionBAL = new ProviderInstructionsBAL();
                ContactBAL objContactBAL = new ContactBAL();

                //Get School Telephone
                ProviderInformation objSchoolTelephone = objContactBAL.Get_ContactAndProviderContactByProviderId(objSchoolInformation.ProviderId, 9);
                objOutPutProviderInformation.SchoolTelephone = objSchoolTelephone.SchoolTelephone;
                objOutPutProviderInformation.IsSchoolTelephoneMobile = objSchoolTelephone.IsMobile;

                //Get School Website
                // ProviderInformation objSchoolWebsite = objContactBAL.Get_ContactAndProviderContactByProviderId(objSchoolInformation.ProviderId, 10);
                // objOutPutProviderInformation.SchoolWebsite = objSchoolWebsite.SchoolTelephone;

                ////Get Director Primary Number
                //ProviderInformation objDirectorPrimaryTelephone = objContactBAL.Get_ContactAndProviderContactByProviderId(objSchoolInformation.ProviderId, 11);
                //objOutPutProviderInformation.DirectorPrimaryNumber = objDirectorPrimaryTelephone.SchoolTelephone;
                //objOutPutProviderInformation.DirectorPrimaryNumberIsMobile = objDirectorPrimaryTelephone.IsMobile;

                ////Get Director Secondary Number
                //ProviderInformation objDirectorSecondaryTelephone = objContactBAL.Get_ContactAndProviderContactByProviderId(objSchoolInformation.ProviderId, 12);
                //objOutPutProviderInformation.DirectorSecondaryNumber = objDirectorSecondaryTelephone.DirectorSecondaryNumber;
                //objOutPutProviderInformation.DirectorSecondaryNumberIsMobile = objDirectorSecondaryTelephone.DirectorSecondaryNumberIsMobile;

                ////Get Director Email Address
                //ProviderInformation objDirectorEmail = objContactBAL.Get_ContactAndProviderContactByProviderId(objSchoolInformation.ProviderId, 13);
                //objOutPutProviderInformation.DirectorAdministratorEmail = objDirectorEmail.DirectorAdministratorEmail;

                ////Get Contact Primary Telephone
                //ProviderInformation objContactPrimaryTelephone = objContactBAL.Get_ContactAndProviderContactByProviderId(objSchoolInformation.ProviderId, 14);
                //objOutPutProviderInformation.ContactNamePrimaryNumber = objContactPrimaryTelephone.ContactNamePrimaryNumber;
                //objOutPutProviderInformation.ContactNamePrimaryNumberIsMobile = objContactPrimaryTelephone.ContactNamePrimaryNumberIsMobile;

                ////Get Contact Secondary Telephone
                //ProviderInformation objContactSecondaryTelephone = objContactBAL.Get_ContactAndProviderContactByProviderId(objSchoolInformation.ProviderId, 15);
                //objOutPutProviderInformation.ContactNameSecondaryNumber = objContactSecondaryTelephone.ContactNameSecondaryNumber;
                //objOutPutProviderInformation.ContactNameSecondaryNumberIsMobile = objContactSecondaryTelephone.ContactNameSecondaryNumberIsMobile;

                ////Get Contact Website
                //ProviderInformation objContactEmail = objContactBAL.Get_ContactAndProviderContactByProviderId(objSchoolInformation.ProviderId, 16);
                //objOutPutProviderInformation.ContactNameAdministratorEmail = objContactEmail.ContactNameAdministratorEmail;


                //Method to get previous school Names grid
                List<ProviderNames> lstPrevSchools = objProviderInstructionBAL.GetAllPreviousSchools(objSchoolInformation.ApplicationId);
                objResponse.ListOfPreviousSchool = lstPrevSchools;


                //Method to Get all the previous schools  grid
                List<Address> lstPrevAddress = objAddressBAL.GetAllPreviousAddress(6, objSchoolInformation.ProviderId);
                objResponse.ListOfPreviousAddress = lstPrevAddress;

                //Method to Get all the previous schools  grid
                List<Address> lstSateliteAddress = objAddressBAL.GetAllPreviousAddress(4, objSchoolInformation.ProviderId);
                objResponse.ListOfSatliteSchool = lstSateliteAddress;

                //Fill All fields to Response
                objResponse.ProviderInformationDetails = objOutPutProviderInformation;

                //Current Thing



                int output = 5;
                string ValidationResponse = "";

                if (output > 0)
                {
                    objResponse.Message = "Success";
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
                else
                {
                    objResponse.Message = output.ToString();
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                // LogingHelper.SaveExceptionInfo("", ex, "Login", ENTITY.Enumeration.eSeverity.Error);
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;

            }
            return objResponse;


        }

    }
}