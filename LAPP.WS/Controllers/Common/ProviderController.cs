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
    /// <summary>
    /// 
    /// </summary>
    public class ProviderController : ApiController
    {

        #region ProviderInfo
        /// <summary>
        /// This API used for Provider Registration.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ObjRegisterInfo">Request object for Provider Registration.</param>
        [AcceptVerbs("POST")]
        [ActionName("ProviderRegister")]
        public BaseEntityServiceResponse ProviderRegister(string Key, ProviderRegister ObjRegisterInfo)
        {
            int CreateOrModify = 0; // TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo();

            BaseEntityServiceResponse objResponse = new BaseEntityServiceResponse();

            List<UsersRequest> lstEntity = new List<UsersRequest>();

            //if (!TokenHelper.ValidateToken(Key))
            //{
            //    objResponse.Message = "User session has expired.";
            //    objResponse.Status = false;
            //    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
            //    objResponse.ResponseReason = "";
            //    return objResponse;
            //}

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
            int CreateOrModify = 0; //  TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo();

            ProviderLoginResponse objResponse = new ProviderLoginResponse();

            UsersBAL objUserBAL = new UsersBAL();
            ProviderUserBAL objProviderUserBAL = new ProviderUserBAL();
            ApplicationStatusBAL objApplicationStatusBAL = new ApplicationStatusBAL();
            Users objEntity = new Users();
            List<UsersRequest> lstEntity = new List<UsersRequest>();

            //if (!TokenHelper.ValidateToken(Key))
            //{
            //    objResponse.Message = "User session has expired.";
            //    objResponse.Status = false;
            //    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
            //    objResponse.ResponseReason = "";
            //    return objResponse;
            //}

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
                    objEntity.DocumentId = objProviderDocument.DocumentId;
                    objEntity.DocumentCd = objProviderDocument.DocumentCd;
                    objEntity.DocumentTypeId = objProviderDocument.DocumentTypeId;
                    objEntity.DocumentLkToPageTabSectionId = objProviderDocument.DocumentLkToPageTabSectionId;
                    objEntity.DocumentLkToPageTabSectionCode = objProviderDocument.DocumentLkToPageTabSectionCode;
                    objEntity.DocumentName = objProviderDocument.DocumentName;
                    objEntity.OtherDocumentTypeName = objProviderDocument.OtherDocumentTypeName;
                    objEntity.DocumentPath = objProviderDocument.DocumentPath;
                    objEntity.EffectiveDate = objProviderDocument.EffectiveDate;
                    objEntity.EndDate = objProviderDocument.EndDate;
                    objEntity.IsDocumentUploadedbyProvider = objProviderDocument.IsDocumentUploadedbyProvider;
                    objEntity.IsDocumentUploadedbyStaff = objProviderDocument.IsDocumentUploadedbyStaff;
                    objEntity.ReferenceNumber = objProviderDocument.ReferenceNumber;
                    objEntity.IsActive = objProviderDocument.IsActive;
                    objEntity.IsDeleted = objProviderDocument.IsDeleted;
                    objEntity.CreatedBy = objProviderDocument.CreatedBy;
                    objEntity.CreatedOn = objProviderDocument.CreatedOn;
                    objEntity.ModifiedBy = objProviderDocument.ModifiedBy;
                    objEntity.ModifiedOn = objProviderDocument.ModifiedOn;
                    objEntity.ProviderDocumentGuid = Guid.NewGuid().ToString();
                    objEntity.Base64Str = objProviderDocument.Base64Str;
                    objEntity.Extension = objProviderDocument.Extension;
                    //--file save process--//
                    string FilePath = ProviderDocumentSaveProcess(objEntity.Base64Str, objEntity.DocumentName, objEntity.Extension);
                    //--file save process--//
                    if (FilePath != null)
                    {
                        objEntity.DocumentPath = FilePath;

                        int ReturnProviderId = objBAL.Save_ProviderDocument(objEntity);
                        List<ProviderDocumentGET> lstTempProviderDoccumentGET = new List<ProviderDocumentGET>();
                        ProviderDocumentGET objTempEntity = new ProviderDocumentGET();
                        objTempEntity.ProviderDocumentId = ReturnProviderId;
                        lstTempProviderDoccumentGET.Add(objTempEntity);
                        objResponse.ProviderDocumentGET = lstTempProviderDoccumentGET;

                        objResponse.Message = Messages.SaveSuccess;
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
            catch (Exception ex)
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
                if (Base64Str != null && filename != null && extension != null)
                {
                    string FilePath = ConfigurationHelper.GetConfigurationValueBySetting("RootDocumentPath") + "SchoolApplication\\";
                    //FilePath = @"D:\sample workspace\stuffstestfolder";
                    Byte[] bytes = Convert.FromBase64String(Base64Str);
                    File.WriteAllBytes(FilePath + filename + extension, bytes);
                    return FilePath + filename + extension;
                }
            }
            catch (Exception ex)
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
                if (ProviderDocId != null && UserId != null && ProviderId != null)// && CourseTitleName!=null && CourseHours!=null)
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
                if (ProviderDocId != null && UserId != null && ProviderId != null && ApplicationId != null)// && CourseTitleName!=null && CourseHours!=null)
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

        #region Provider Document Download

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="ProviderDocumentId"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        [ActionName("ProviderDocumentByProviderDocumentId")]
        public HttpResponseMessage PdfDocumentByIndividualDocumentId(string Key, int ProviderDocumentId)
        {
            try
            {
                //IndividualDocumentByHtmlResponse objResponse = new IndividualDocumentByHtmlResponse();

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                ProviderDocumentGET objDocument = new ProviderDocumentGET();
                ProviderDocumentBAL objDocumentBAL = new ProviderDocumentBAL();
                if (!TokenHelper.ValidateToken(Key))
                {
                    HttpError myCustomError = new HttpError("User session has expired.") { { "CustomErrorCode", Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00") } };
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, myCustomError);
                }

                objDocument = objDocumentBAL.Get_ProviderDocument_By_ProviderDocumentId(ProviderDocumentId);
                if (objDocument != null)
                {

                    var stream = new FileStream(objDocument.DocumentPath, FileMode.Open);
                    result.Content = new StreamContent(stream);
                    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    result.Content.Headers.ContentDisposition.FileName = Path.GetFileName(objDocument.DocumentPath);
                    //   result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                    result.Content.Headers.ContentLength = stream.Length;

                }
                return result;
            }
            catch (Exception ex)
            { return null; }
        }

        #endregion


        /// <summary>
        /// This method is to return the current tab that is active and show tick to all other tabs
        /// </summary>
        /// <param name="objProviderInstruction">Request object for Provider Instruction.</param>
        [AcceptVerbs("POST")]
        [ActionName("CheckInitialTabActive")]
        public ProviderLoginResponse CheckInitialTabActive(string Key, ProviderInstructions objProviderInstruction)
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

                    objResponse.Message = Messages.SaveSuccess;
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
        public ProviderLoginResponse SaveButtonOfInstructions(string Key, ProviderInstructions objProviderInstruction)
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

                    objResponse.Message = Messages.SaveSuccess;
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
        /// <param name="Key"></param>
        /// <param name="objProviderNames">Request object for Provider Name.</param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("AddPreviousSchoolInSchoolInformation")]
        public ProviderPreviousSchoolResponse AddPreviousSchoolInSchoolInformation(string Key, ProviderNames objProviderNames)
        {
            LogingHelper.SaveAuditInfo(Key);

            ProviderPreviousSchoolResponse objResponse = new ProviderPreviousSchoolResponse();
            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.ListOfPreviousSchool = null;
                return objResponse;
            }

            try
            {

                if (objProviderNames == null)
                {
                    objResponse.Message = "Invalid Object.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
                //Method to Save click information on Provider Instructor Table
                objProviderNames.ProviderNameStatusId = 2;
                objProviderNames.ProviderNameTypeId = 1;
                objProviderNames.ProviderNameGuid = Guid.NewGuid().ToString();
                ProviderInstructionsBAL objProviderInstructionBAL = new ProviderInstructionsBAL();
                int output = objProviderInstructionBAL.SavePreviousSchoolDetails(objProviderNames);

                string ValidationResponse = "";

                if (output > 0)
                {
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";

                    List<ProviderNames> lstPrevSchools = objProviderInstructionBAL.GetAllPreviousSchools(objProviderNames.ApplicationId, objProviderNames.ProviderId);
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
                LogingHelper.SaveExceptionInfo("", ex, "AddPreviousSchoolInSchoolInformation", ENTITY.Enumeration.eSeverity.Error);
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ListOfPreviousSchool = null;
            }


            return objResponse;


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="objProviderNames"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("DeletePreviousSchoolInSchoolInformation")]
        public ProviderPreviousSchoolResponse DeletePreviousSchoolInSchoolInformation(string Key, ProviderNames objProviderNames)
        {
            LogingHelper.SaveAuditInfo(Key);

            ProviderPreviousSchoolResponse objResponse = new ProviderPreviousSchoolResponse();
            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.ListOfPreviousSchool = null;
                return objResponse;
            }

            try
            {

                if (objProviderNames == null)
                {
                    objResponse.Message = "Invalid Object.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
                //Method to Save click information on Provider Instructor Table
                objProviderNames.ProviderNameStatusId = 2;
                objProviderNames.ProviderNameTypeId = 1;

                if (objProviderNames.ProviderNameId == 0)
                    objProviderNames.ProviderNameGuid = Guid.NewGuid().ToString();
                ProviderInstructionsBAL objProviderInstructionBAL = new ProviderInstructionsBAL();
                int output = objProviderInstructionBAL.DeletePreviousSchoolDetails(objProviderNames);

                string ValidationResponse = "";

                if (output > 0)
                {
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";

                    List<ProviderNames> lstPrevSchools = objProviderInstructionBAL.GetAllPreviousSchools(objProviderNames.ProviderNameTypeId, objProviderNames.ProviderId);
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
                LogingHelper.SaveExceptionInfo("", ex, "AddPreviousSchoolInSchoolInformation", ENTITY.Enumeration.eSeverity.Error);
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ListOfPreviousSchool = null;
            }


            return objResponse;


        }

        //GET Address information for different tabs based on Address Type iD
        /// <summary>
        /// This method is to Save the Address data comes in School Information 
        /// </summary>
        /// <param name="objAddress">Request object for Provider Instruction.</param>
        /// <param name="Key"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("SaveAddressRequestFromSchoolInformationTab")]
        public ProviderAddressResponse SaveAddressRequestFromSchoolInformationTab(string Key, Address objAddress)
        {
            LogingHelper.SaveAuditInfo(Key);

            ProviderAddressResponse objResponse = new ProviderAddressResponse();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.ListOfPreviousAddress = null;
                return objResponse;
            }

            try
            {
                if (objAddress == null)
                {
                    objResponse.Message = "Invalid Object.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                    objResponse.ResponseReason = "";
                    objResponse.ListOfPreviousAddress = null;
                    return objResponse;
                }
                //Method to Save Address based on different address Types
                AddressBAL objAddressBAL = new AddressBAL();
                int output = objAddressBAL.SaveAddressRequestFromSchoolInformationTab(objAddress);

                string ValidationResponse = "";

                if (output > 0)
                {
                    objResponse.Message = Messages.SaveSuccess;
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
        /// <param name="objSchoolInformation">Request object for Provider Instruction.</param>/// <param name="Key"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("SaveSchoolInformation")]
        public ProviderLoginResponse SaveSchoolInformation(string Key, ProviderInformation objSchoolInformation)
        {
            LogingHelper.SaveAuditInfo(Key);
            ProviderLoginResponse objResponse = new ProviderLoginResponse();
            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.ResponseReason = null;
                return objResponse;
            }



            try
            {
                if (objSchoolInformation == null)
                {
                    objResponse.Message = "Invalid Object.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }

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
                objSchoolTelephone.IsMobile = objSchoolInformation.IsSchoolTelephoneMobile;
                objContactBAL.Save_ContactAndProviderContact(objSchoolTelephone);

                ProviderInformation objSchoolWebSite = new ProviderInformation();
                objSchoolWebSite.ContactTypeId = 10; //Change it
                objSchoolWebSite.ContactInfo = objSchoolInformation.SchoolWebsite;
                objSchoolWebSite.CreatedBy = objSchoolInformation.CreatedBy;
                objSchoolWebSite.ProviderId = objSchoolInformation.ProviderId;
                objSchoolWebSite.IsPreferredContact = objSchoolInformation.IsPreferredContact;
                objSchoolWebSite.IsMobile = objSchoolInformation.IsMobile;
                objContactBAL.Save_ContactAndProviderContact(objSchoolWebSite);


                //Update School Address, Mailing Address to Address Table and provideraddress table
                Address objSchoolAddress = new Address();
                objSchoolAddress.AddressId = objSchoolInformation.SchoolAddressId;
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
                objMailingAddress.AddressId = objSchoolInformation.MailingAddressId;
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
                objDirectorName.IndividualNameTypeId = 16;
                objDirectorName.IndividualNameStatusId = 11;
                objDirectorName.ProviderId = objSchoolInformation.ProviderId;
                objDirectorName.ApplicationId = objSchoolInformation.ApplicationId;
                objDirectorName.IndividualId = objSchoolInformation.DirectorIndividualId;
                objIndividualBAL.Save_IndividualProvider(objDirectorName);

                IndividualName objContactName = new IndividualName();
                objContactName.FirstName = objSchoolInformation.ContactNameFirstName;
                objContactName.LastName = objSchoolInformation.ContactNameLastName;
                objContactName.CreatedBy = objSchoolInformation.CreatedBy;
                objContactName.ProvIndvJobTitle = objSchoolInformation.ContactNameJobTitle;
                objContactName.IndividualNameTypeId = 18;
                objContactName.IndividualNameStatusId = 11;
                objContactName.ProviderId = objSchoolInformation.ProviderId;
                objContactName.ApplicationId = objSchoolInformation.ApplicationId;
                objDirectorName.IndividualId = objSchoolInformation.ContactNameIndividualId;
                objIndividualBAL.Save_IndividualProvider(objContactName);

                //Save Phone number for Primary Number and Director
                ProviderInformation objDirectorPrimaryTelephone = new ProviderInformation();
                objSchoolTelephone.ContactTypeId = 11; //11 chnage
                objSchoolTelephone.ContactInfo = objSchoolInformation.DirectorPrimaryNumber;
                objSchoolTelephone.CreatedBy = objSchoolInformation.CreatedBy;
                objSchoolTelephone.ProviderId = objSchoolInformation.ProviderId;
                objSchoolTelephone.IsPreferredContact = objSchoolInformation.IsPreferredContact;
                objSchoolTelephone.IsMobile = objSchoolInformation.DirectorPrimaryNumberIsMobile;
                objContactBAL.Save_ContactAndProviderContact(objSchoolTelephone);

                //Save Phone number for Secondary Number and Director
                ProviderInformation objDirectorSecondaryTelephone = new ProviderInformation();
                objDirectorSecondaryTelephone.ContactTypeId = 12; //12
                objDirectorSecondaryTelephone.ContactInfo = objSchoolInformation.DirectorSecondaryNumber;
                objDirectorSecondaryTelephone.CreatedBy = objSchoolInformation.CreatedBy;
                objDirectorSecondaryTelephone.ProviderId = objSchoolInformation.ProviderId;
                objDirectorSecondaryTelephone.IsPreferredContact = objSchoolInformation.IsPreferredContact;
                objDirectorSecondaryTelephone.IsMobile = objSchoolInformation.DirectorSecondaryNumberIsMobile;
                objContactBAL.Save_ContactAndProviderContact(objDirectorSecondaryTelephone);

                //Save Administrator Email for Director
                ProviderInformation objDirectorEmail = new ProviderInformation();
                objDirectorEmail.ContactTypeId = 13; //13
                objDirectorEmail.ContactInfo = objSchoolInformation.DirectorAdministratorEmail;
                objDirectorEmail.CreatedBy = objSchoolInformation.CreatedBy;
                objDirectorEmail.ProviderId = objSchoolInformation.ProviderId;
                objDirectorEmail.IsPreferredContact = objSchoolInformation.IsPreferredContact;
                objDirectorEmail.IsMobile = objSchoolInformation.IsMobile;
                objContactBAL.Save_ContactAndProviderContact(objDirectorEmail);

                //Save Phone number for Primary Number of Contact
                ProviderInformation objContactPrimaryTelephone = new ProviderInformation();
                objContactPrimaryTelephone.ContactTypeId = 14; //14
                objContactPrimaryTelephone.ContactInfo = objSchoolInformation.ContactNamePrimaryNumber;
                objContactPrimaryTelephone.CreatedBy = objSchoolInformation.CreatedBy;
                objContactPrimaryTelephone.ProviderId = objSchoolInformation.ProviderId;
                objContactPrimaryTelephone.IsPreferredContact = objSchoolInformation.IsPreferredContact;
                objContactPrimaryTelephone.IsMobile = objSchoolInformation.ContactNamePrimaryNumberIsMobile;
                objContactBAL.Save_ContactAndProviderContact(objContactPrimaryTelephone);

                //Save Phone number for Secondary Number of Contact
                ProviderInformation objContactSecondaryTelephone = new ProviderInformation();
                objContactSecondaryTelephone.ContactTypeId = 15; //15
                objContactSecondaryTelephone.ContactInfo = objSchoolInformation.ContactNameSecondaryNumber;
                objContactSecondaryTelephone.CreatedBy = objSchoolInformation.CreatedBy;
                objContactSecondaryTelephone.ProviderId = objSchoolInformation.ProviderId;
                objContactSecondaryTelephone.IsPreferredContact = objSchoolInformation.IsPreferredContact;
                objContactSecondaryTelephone.IsMobile = objSchoolInformation.ContactNameSecondaryNumberIsMobile;
                objContactBAL.Save_ContactAndProviderContact(objContactSecondaryTelephone);

                //Save Administrator Email of Contact
                ProviderInformation objContactEmail = new ProviderInformation();
                objContactEmail.ContactTypeId = 16; //16
                objContactEmail.ContactInfo = objSchoolInformation.ContactNameAdministratorEmail;
                objContactEmail.CreatedBy = objSchoolInformation.CreatedBy;
                objContactEmail.ProviderId = objSchoolInformation.ProviderId;
                objContactEmail.IsPreferredContact = objSchoolInformation.IsPreferredContact;
                objContactEmail.IsMobile = objSchoolInformation.IsMobile;
                objContactBAL.Save_ContactAndProviderContact(objContactEmail);


                int output = 5;
                string ValidationResponse = "";


                if (output > 0)
                {
                    objResponse.Message = Messages.SaveSuccess;
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
        /// <param name="Key"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("GetAllSchoolInformationDetails")]
        public ProviderOnLoadResponse GetAllSchoolInformationDetails(string Key, ProviderInformation objSchoolInformation)
        {
            LogingHelper.SaveAuditInfo(Key);

            ProviderOnLoadResponse objResponse = new ProviderOnLoadResponse();
            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.ResponseReason = null;
                return objResponse;
            }

            try
            {
                if (objSchoolInformation == null)
                {
                    objResponse.Message = "Invalid Object.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }

                ProviderInformation objOutPutProviderInformation = new ProviderInformation();
                AddressBAL objAddressBAL = new AddressBAL();
                ProviderInstructionsBAL objProviderInstructionBAL = new ProviderInstructionsBAL();
                ContactBAL objContactBAL = new ContactBAL();
                IndividualBAL objIndividualBAL = new IndividualBAL();
                //Get School Telephone
                ProviderInformation objSchoolTelephone = objContactBAL.Get_ContactAndProviderContactByProviderId(objSchoolInformation.ProviderId, 9);
                if (objSchoolTelephone != null)
                {
                    objOutPutProviderInformation.SchoolTelephone = objSchoolTelephone.ContactInfo;
                    objOutPutProviderInformation.IsSchoolTelephoneMobile = objSchoolTelephone.IsMobile;
                }
                else
                {
                    objOutPutProviderInformation.SchoolTelephone = "";
                    objOutPutProviderInformation.IsSchoolTelephoneMobile = false;
                }

                //Get School Website
                ProviderInformation objSchoolWebsite = objContactBAL.Get_ContactAndProviderContactByProviderId(objSchoolInformation.ProviderId, 10);
                if (objSchoolWebsite != null)
                {
                    objOutPutProviderInformation.SchoolWebsite = objSchoolWebsite.ContactInfo;

                }
                else
                {
                    objOutPutProviderInformation.SchoolWebsite = "";
                }


                //school address 6
                List<Address> SchoolAddress = objAddressBAL.GetAllPreviousAddress(6, objSchoolInformation.ProviderId);
                if (SchoolAddress != null)
                {
                    objResponse.SchoolAddress = SchoolAddress;
                }

                //mail address 1
                List<Address> objMailAddress = objAddressBAL.GetAllPreviousAddress(1, objSchoolInformation.ProviderId);
                if (objMailAddress != null)
                    objResponse.MailingAddress = objMailAddress;

                //job title
                List<IndividualName> lstprimarytitle = objIndividualBAL.Get_IndividualProvider(objSchoolInformation.ProviderId, 16);
                if (lstprimarytitle != null)
                    objResponse.ListOfDirectorJobTitle = lstprimarytitle;

                //job title
                List<IndividualName> lstcontacttitle = objIndividualBAL.Get_IndividualProvider(objSchoolInformation.ProviderId, 18);
                if (lstcontacttitle != null)
                    objResponse.ListOfContactJobTitle = lstcontacttitle;
                ////Get Director Primary Number
                ProviderInformation objDirectorPrimaryTelephone = objContactBAL.Get_ContactAndProviderContactByProviderId(objSchoolInformation.ProviderId, 11);
                if (objDirectorPrimaryTelephone != null)
                {
                    objOutPutProviderInformation.DirectorPrimaryNumber = objDirectorPrimaryTelephone.ContactInfo;
                    objOutPutProviderInformation.DirectorPrimaryNumberIsMobile = objDirectorPrimaryTelephone.IsMobile;
                }
                else
                {
                    objOutPutProviderInformation.DirectorPrimaryNumber = "";
                    objOutPutProviderInformation.DirectorPrimaryNumberIsMobile = false;
                }
                ////Get Director Secondary Number
                ProviderInformation objDirectorSecondaryTelephone = objContactBAL.Get_ContactAndProviderContactByProviderId(objSchoolInformation.ProviderId, 12);
                if (objDirectorSecondaryTelephone != null)
                {
                    objOutPutProviderInformation.DirectorSecondaryNumber = objDirectorSecondaryTelephone.ContactInfo;
                    objOutPutProviderInformation.DirectorSecondaryNumberIsMobile = objDirectorSecondaryTelephone.IsMobile;
                }
                else
                {
                    objOutPutProviderInformation.DirectorSecondaryNumber = "";
                    objOutPutProviderInformation.DirectorSecondaryNumberIsMobile = false;
                }
                ////Get Director Email Address
                ProviderInformation objDirectorEmail = objContactBAL.Get_ContactAndProviderContactByProviderId(objSchoolInformation.ProviderId, 13);
                if (objDirectorEmail != null)
                {
                    objOutPutProviderInformation.DirectorAdministratorEmail = objDirectorEmail.ContactInfo;
                }
                else
                {
                    objOutPutProviderInformation.DirectorAdministratorEmail = "";
                }
                ////Get Contact Primary Telephone
                ProviderInformation objContactPrimaryTelephone = objContactBAL.Get_ContactAndProviderContactByProviderId(objSchoolInformation.ProviderId, 14);
                if (objContactPrimaryTelephone != null)
                {
                    objOutPutProviderInformation.ContactNamePrimaryNumber = objContactPrimaryTelephone.ContactInfo;
                    objOutPutProviderInformation.ContactNamePrimaryNumberIsMobile = objContactPrimaryTelephone.IsMobile;
                }
                else
                {
                    objOutPutProviderInformation.ContactNamePrimaryNumber = "";
                    objOutPutProviderInformation.ContactNamePrimaryNumberIsMobile = false;
                }
                ////Get Contact Secondary Telephone
                ProviderInformation objContactSecondaryTelephone = objContactBAL.Get_ContactAndProviderContactByProviderId(objSchoolInformation.ProviderId, 15);
                if (objContactSecondaryTelephone != null)
                {
                    objOutPutProviderInformation.ContactNameSecondaryNumber = objContactSecondaryTelephone.ContactInfo;
                    objOutPutProviderInformation.ContactNameSecondaryNumberIsMobile = objContactSecondaryTelephone.IsMobile;
                }
                else
                {
                    objOutPutProviderInformation.ContactNameSecondaryNumber = "";
                    objOutPutProviderInformation.ContactNameSecondaryNumberIsMobile = false;
                }
                ////Get Contact Website
                ProviderInformation objContactEmail = objContactBAL.Get_ContactAndProviderContactByProviderId(objSchoolInformation.ProviderId, 16);
                if (objContactEmail != null)
                {
                    objOutPutProviderInformation.ContactNameAdministratorEmail = objContactEmail.ContactInfo;
                }
                else

                {
                    objOutPutProviderInformation.ContactNameAdministratorEmail = "";
                }

                //Method to get previous school Names grid
                List<ProviderNames> lstPrevSchools = objProviderInstructionBAL.GetAllPreviousSchools(objSchoolInformation.ApplicationId, objSchoolInformation.ProviderId);
                if (lstPrevSchools != null)
                {
                    objResponse.ListOfPreviousSchool = lstPrevSchools;
                }
                else
                {
                    objResponse.ListOfPreviousSchool = new List<ProviderNames>();
                }

                //Method to Get all the previous schools  grid
                List<Address> lstPrevAddress = objAddressBAL.GetAllPreviousAddress(3, objSchoolInformation.ProviderId);
                if (lstPrevAddress != null)
                {
                    objResponse.ListOfPreviousAddress = lstPrevAddress;
                }
                else
                {
                    objResponse.ListOfPreviousAddress = new List<Address>();
                }
                //Method to Get all the previous schools  grid
                List<Address> lstSateliteAddress = objAddressBAL.GetAllPreviousAddress(7, objSchoolInformation.ProviderId);
                if (lstSateliteAddress != null)
                {
                    objResponse.ListOfSatliteSchool = lstSateliteAddress;
                }
                else
                {
                    objResponse.ListOfSatliteSchool = new List<Address>();
                }
                //Fill All fields to Response
                objResponse.ProviderInformationDetails = objOutPutProviderInformation;

                //Current Thing


                int output = 5;
                string ValidationResponse = "";

                if (output > 0)
                {
                    objResponse.Message = Messages.SaveSuccess;
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
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        [ActionName("GetAllProvider")]
        public AllProviderResponseRequest Get_All_Provider(string Key)
        {
            LogingHelper.SaveAuditInfo(Key);

            AllProviderResponseRequest objResponse = new AllProviderResponseRequest();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.ResponseReason = null;
                return objResponse;
            }

            ProviderBAL objProviderBAL = new ProviderBAL();
            ProviderResponseRequest objProviderResponseRequest = new ProviderResponseRequest();
            Individual objIndividual = new Individual();
            List<IndividualResponse> lstIndividualResponse = new List<IndividualResponse>();
            List<Individual> lstIndividual = new List<Individual>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ProviderResponseList = null;
                    return objResponse;
                }

                try
                {
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";

                    //Method to Get all the previous schools 
                    List<Provider> lstProvider = objProviderBAL.Get_All_Provider();
                    objResponse.ProviderResponseList = lstProvider;

                    return objResponse;

                }
                catch (Exception ex)
                {
                    LogingHelper.SaveExceptionInfo(Key, ex, "ValidateIndividual", ENTITY.Enumeration.eSeverity.Error);

                    objResponse.Status = false;
                    objResponse.Message = ex.Message;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                    objResponse.ProviderResponseList = null;

                }


            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ValidateIndividual", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.ProviderResponseList = null;

            }
            return objResponse;
        }

        /// <summary>
        /// Get_ProviderDetailsByID
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="providerid"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        [ActionName("GetProviderById")]
        public ProviderInfo Get_ProviderDetailsByID(string Key, int providerid)
        {
            LogingHelper.SaveAuditInfo(Key);

            ProviderInfo objResponse = new ProviderInfo();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.ResponseReason = null;
                return objResponse;
            }

            ProviderBAL objProviderBAL = new ProviderBAL();
            ProviderResponseRequest objProviderResponseRequest = new ProviderResponseRequest();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ProviderResponse = null;
                    return objResponse;
                }

                try
                {
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";

                    //Method to Get all the previous schools 
                    Provider lstProvider = objProviderBAL.Get_Provider_By_ProviderId(providerid);
                    objResponse.ProviderResponse = lstProvider;

                    return objResponse;

                }
                catch (Exception ex)
                {
                    LogingHelper.SaveExceptionInfo(Key, ex, "ValidateIndividual", ENTITY.Enumeration.eSeverity.Error);

                    objResponse.Status = false;
                    objResponse.Message = ex.Message;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                    objResponse.ProviderResponse = null;

                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ValidateIndividual", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.ProviderResponse = null;

            }
            return objResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="objProvidermblexResponse"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("GetAllProvidermblex")]
        public ProvidermblexResponseRequest GetAllProvidermblex(string Key, ProviderMblex objProvidermblexResponse)
        {
            LogingHelper.SaveAuditInfo(Key);
            ProvidermblexResponseRequest objResponse = new ProvidermblexResponseRequest();
            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.ResponseReason = null;
                return objResponse;
            }

            providermblexBAL objprovidermblexBAL = new providermblexBAL();

            ProviderMblex objIndividual = new ProviderMblex();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ProvidermblexResponseList = null;
                    return objResponse;
                }

                try
                {
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";

                    //Method to Get all the previous schools 
                    List<ProvidermblexResponse> lstProvider = objprovidermblexBAL.Get_All_Providermblex(objProvidermblexResponse);
                    objResponse.ProvidermblexResponseList = lstProvider;

                    return objResponse;

                }
                catch (Exception ex)
                {
                    LogingHelper.SaveExceptionInfo(Key, ex, "ValidateIndividual", ENTITY.Enumeration.eSeverity.Error);

                    objResponse.Status = false;
                    objResponse.Message = ex.Message;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                    objResponse.ProvidermblexResponseList = null;

                }


            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ValidateIndividual", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.ProvidermblexResponseList = null;

            }
            return objResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="objAprovidermblex"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("SaveProvidermblex")]
        public ProvidermblexResponseRequest SaveProvidermblex(string Key, ProviderMblex objAprovidermblex)
        {
            LogingHelper.SaveAuditInfo(Key);

            ProvidermblexResponseRequest objProvidermblexResponse = new ProvidermblexResponseRequest();

            if (!TokenHelper.ValidateToken(Key))
            {
                objProvidermblexResponse.Status = false;
                objProvidermblexResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objProvidermblexResponse.Message = "User session has expired.";
                objProvidermblexResponse.ProvidermblexResponseList = null;
                return objProvidermblexResponse;
            }

            try
            {
                if (objAprovidermblex == null)
                {
                    objProvidermblexResponse.Message = "Invalid Object.";
                    objProvidermblexResponse.Status = false;
                    objProvidermblexResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                    objProvidermblexResponse.ResponseReason = "";
                    objProvidermblexResponse.ProvidermblexResponseList = null;
                    return objProvidermblexResponse;
                }
                if (objAprovidermblex.ProviderMBLExId == 0)
                    objAprovidermblex.ProviderMBLExIdGuid = Guid.NewGuid().ToString();

                providermblexBAL objprovidermblexBAL = new providermblexBAL();
                int output = objprovidermblexBAL.Save_Providermblex(objAprovidermblex);

                string ValidationResponse = "";

                if (output > 0)
                {
                    objProvidermblexResponse.Message = Messages.SaveSuccess;
                    objProvidermblexResponse.Status = true;
                    objProvidermblexResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objProvidermblexResponse.ResponseReason = "";

                    //Method to Get all the previous schools 
                    List<ProvidermblexResponse> lstprovidermblexBAL = objprovidermblexBAL.Get_All_Providermblex(objAprovidermblex);
                    objProvidermblexResponse.ProvidermblexResponseList = lstprovidermblexBAL;

                    return objProvidermblexResponse;
                }
                else
                {
                    objProvidermblexResponse.Message = output.ToString();
                    objProvidermblexResponse.Status = false;
                    objProvidermblexResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objProvidermblexResponse.ResponseReason = ValidationResponse;
                    objProvidermblexResponse.ProvidermblexResponseList = null;
                    return objProvidermblexResponse;
                }
            }
            catch (Exception ex)
            {
                // LogingHelper.SaveExceptionInfo("", ex, "Login", ENTITY.Enumeration.eSeverity.Error);
                objProvidermblexResponse.Status = false;
                objProvidermblexResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objProvidermblexResponse.Message = ex.Message;
                objProvidermblexResponse.ProvidermblexResponseList = null;

            }
            return objProvidermblexResponse;


        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        [ActionName("Get_All_Providersitevisittype")]
        public ProvidersitevisittypeRequestResponse Get_All_Providersitevisittype(string Key)
        {
            LogingHelper.SaveAuditInfo(Key);
            ProvidersitevisittypeRequestResponse objResponse = new ProvidersitevisittypeRequestResponse();
            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.ResponseReason = null;
                return objResponse;
            }

            ProvidersitevisittypeBAL objprovidermblexBAL = new ProvidersitevisittypeBAL();

            Providersitevisittype objIndividual = new Providersitevisittype();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ProvidersitevisittypeGetList = null;
                    return objResponse;
                }

                try
                {
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";

                    //Method to Get all the previous schools 
                    List<Providersitevisittype> lstProvider = objprovidermblexBAL.Get_All_Providersitevisittype();
                    objResponse.ProvidersitevisittypeGetList = lstProvider;

                    return objResponse;

                }
                catch (Exception ex)
                {
                    LogingHelper.SaveExceptionInfo(Key, ex, "ValidateIndividual", ENTITY.Enumeration.eSeverity.Error);

                    objResponse.Status = false;
                    objResponse.Message = ex.Message;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                    objResponse.ProvidersitevisittypeGetList = null;

                }


            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ValidateIndividual", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.ProvidersitevisittypeGetList = null;

            }
            return objResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="objProvidersitevisittype"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("Save_Providersitevisittype")]
        public ProvidersitevisittypeRequestResponse Save_Providersitevisittype(string Key, Providersitevisittype objProvidersitevisittype)
        {
            LogingHelper.SaveAuditInfo(Key);

            ProvidersitevisittypeRequestResponse objProvidermblexResponse = new ProvidersitevisittypeRequestResponse();

            if (!TokenHelper.ValidateToken(Key))
            {
                objProvidermblexResponse.Status = false;
                objProvidermblexResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objProvidermblexResponse.Message = "User session has expired.";
                objProvidermblexResponse.ProvidersitevisittypeGetList = null;
                return objProvidermblexResponse;
            }

            try
            {
                if (objProvidersitevisittype == null)
                {
                    objProvidermblexResponse.Message = "Invalid Object.";
                    objProvidermblexResponse.Status = false;
                    objProvidermblexResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                    objProvidermblexResponse.ResponseReason = "";
                    objProvidermblexResponse.ProvidersitevisittypeGetList = null;
                    return objProvidermblexResponse;
                }
                //Method to Save Address based on different address Types
                ProvidersitevisittypeBAL objprovidermblexBAL = new ProvidersitevisittypeBAL();

                int output = objprovidermblexBAL.Save_Providersitevisittype(objProvidersitevisittype);

                string ValidationResponse = "";

                if (output > 0)
                {
                    objProvidermblexResponse.Message = Messages.SaveSuccess;
                    objProvidermblexResponse.Status = true;
                    objProvidermblexResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objProvidermblexResponse.ResponseReason = "";

                    //Method to Get all the previous schools 
                    List<Providersitevisittype> lstProvidersitevisittype = objprovidermblexBAL.Get_All_Providersitevisittype();
                    objProvidermblexResponse.ProvidersitevisittypeGetList = lstProvidersitevisittype;

                    return objProvidermblexResponse;
                }
                else
                {
                    objProvidermblexResponse.Message = output.ToString();
                    objProvidermblexResponse.Status = false;
                    objProvidermblexResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objProvidermblexResponse.ResponseReason = ValidationResponse;
                    objProvidermblexResponse.ProvidersitevisittypeGetList = null;
                    return objProvidermblexResponse;
                }
            }
            catch (Exception ex)
            {
                // LogingHelper.SaveExceptionInfo("", ex, "Login", ENTITY.Enumeration.eSeverity.Error);
                objProvidermblexResponse.Status = false;
                objProvidermblexResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objProvidermblexResponse.Message = ex.Message;
                objProvidermblexResponse.ProvidersitevisittypeGetList = null;

            }
            return objProvidermblexResponse;


        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="objAddress"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("DeleteaddressRequestFromSchoolInformationTab")]
        public ProviderAddressResponse DeleteaddressRequestFromSchoolInformationTab(string Key, Address objAddress)
        {
            LogingHelper.SaveAuditInfo(Key);
            ProviderAddressResponse objResponse = new ProviderAddressResponse();
            AddressBAL objAddressBAL = new AddressBAL();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.ListOfPreviousAddress = null;
                return objResponse;
            }

            try
            {
                if (objAddress == null)
                {
                    objResponse.Message = "Invalid Object.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                    objResponse.ResponseReason = "";
                    objResponse.ListOfPreviousAddress = null;
                    return objResponse;
                }
                //job title
                int output = objAddressBAL.DeleteaddressRequestFromSchoolInformationTab(objAddress);
                string ValidationResponse = "";

                if (output > 0)
                {
                    objResponse.Message = Messages.SaveSuccess;
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

        #endregion  

        #region Eligibility


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="objProviderProgram"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("GetAllProviderProgram")]
        public ProviderProgramResponse GetAllProviderProgram(string Key, ProviderProgram objProviderProgram)
        {
            LogingHelper.SaveAuditInfo(Key);
            ProviderProgramResponse objResponse = new ProviderProgramResponse();
            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.ResponseReason = null;
                return objResponse;
            }

            ProviderProgramBAL objProviderProgramBAL = new ProviderProgramBAL();

            ProviderProgram objIndividual = new ProviderProgram();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ProviderProgramResponseList = null;
                    return objResponse;
                }

                try
                {
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";

                    //Method to Get all the previous schools 
                    List<ProviderProgram> lstProvider = objProviderProgramBAL.Get_All_ProviderProgram(objProviderProgram);
                    objResponse.ProviderProgramResponseList = lstProvider;

                    return objResponse;

                }
                catch (Exception ex)
                {
                    LogingHelper.SaveExceptionInfo(Key, ex, "ValidateIndividual", ENTITY.Enumeration.eSeverity.Error);

                    objResponse.Status = false;
                    objResponse.Message = ex.Message;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                    objResponse.ProviderProgramResponseList = null;

                }


            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ValidateIndividual", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.ProviderProgramResponseList = null;

            }
            return objResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="objProviderProgram"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("SaveProviderProgram")]
        public ProviderProgramResponse SaveProviderProgram(string Key, ProviderProgram objProviderProgram)
        {
            LogingHelper.SaveAuditInfo(Key);

            ProviderProgramResponse objProviderProgramResponse = new ProviderProgramResponse();

            if (!TokenHelper.ValidateToken(Key))
            {
                objProviderProgramResponse.Status = false;
                objProviderProgramResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objProviderProgramResponse.Message = "User session has expired.";
                objProviderProgramResponse.ProviderProgramResponseList = null;
                return objProviderProgramResponse;
            }

            try
            {
                if (objProviderProgram == null)
                {
                    objProviderProgramResponse.Message = "Invalid Object.";
                    objProviderProgramResponse.Status = false;
                    objProviderProgramResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                    objProviderProgramResponse.ResponseReason = "";
                    objProviderProgramResponse.ProviderProgramResponseList = null;
                    return objProviderProgramResponse;
                }
                if (objProviderProgram.ProviderProgramId == 0)
                    objProviderProgram.ProviderProgramGuid = Guid.NewGuid().ToString();

                ProviderProgramBAL objProviderProgramBAL = new ProviderProgramBAL();
                int output = objProviderProgramBAL.Save_ProviderProgram(objProviderProgram);

                string ValidationResponse = "";

                if (output > 0)
                {
                    objProviderProgramResponse.Message = Messages.SaveSuccess;
                    objProviderProgramResponse.Status = true;
                    objProviderProgramResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objProviderProgramResponse.ResponseReason = "";

                    //Method to Get all the previous schools 
                    List<ProviderProgram> lstProviderProgramBAL = objProviderProgramBAL.Get_All_ProviderProgram(objProviderProgram);
                    objProviderProgramResponse.ProviderProgramResponseList = lstProviderProgramBAL;

                    return objProviderProgramResponse;
                }
                else
                {
                    objProviderProgramResponse.Message = output.ToString();
                    objProviderProgramResponse.Status = false;
                    objProviderProgramResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objProviderProgramResponse.ResponseReason = ValidationResponse;
                    objProviderProgramResponse.ProviderProgramResponseList = null;
                    return objProviderProgramResponse;
                }
            }
            catch (Exception ex)
            {
                // LogingHelper.SaveExceptionInfo("", ex, "Login", ENTITY.Enumeration.eSeverity.Error);
                objProviderProgramResponse.Status = false;
                objProviderProgramResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objProviderProgramResponse.Message = ex.Message;
                objProviderProgramResponse.ProviderProgramResponseList = null;

            }
            return objProviderProgramResponse;


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="objProviderProgram"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("DeleteProviderProgram")]
        public ProviderProgramResponse DeleteaddressProviderProgram(string Key, ProviderProgram objProviderProgram)
        {
            LogingHelper.SaveAuditInfo(Key);
            ProviderProgramResponse objResponse = new ProviderProgramResponse();
            ProviderProgramBAL objProviderProgramBAL = new ProviderProgramBAL();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.ProviderProgramResponseList = null;
                return objResponse;
            }

            try
            {
                if (objProviderProgram == null)
                {
                    objResponse.Message = "Invalid Object.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                    objResponse.ResponseReason = "";
                    objResponse.ProviderProgramResponseList = null;
                    return objResponse;
                }
                //job title
                int output = objProviderProgramBAL.DeleteProviderProgram(objProviderProgram);
                string ValidationResponse = "";

                if (output > 0)
                {
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";

                    //Method to Get all the previous schools 
                    List<ProviderProgram> lstPrevAddress = objProviderProgramBAL.Get_All_ProviderProgram(objProviderProgram);
                    objResponse.ProviderProgramResponseList = lstPrevAddress;

                    return objResponse;
                }
                else
                {
                    objResponse.Message = output.ToString();
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    objResponse.ProviderProgramResponseList = null;
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                // LogingHelper.SaveExceptionInfo("", ex, "Login", ENTITY.Enumeration.eSeverity.Error);
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ProviderProgramResponseList = null;

            }
            return objResponse;


        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="objProviderApprovalAgency"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("GetAllProviderApprovalAgency")]
        public ProviderApprovalAgencyResponse GetAllProviderApprovalAgency(string Key, ProviderApprovalAgency objProviderApprovalAgency)
        {
            LogingHelper.SaveAuditInfo(Key);
            ProviderApprovalAgencyResponse objResponse = new ProviderApprovalAgencyResponse();
            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.ResponseReason = null;
                return objResponse;
            }

            ProviderApprovalAgencyBAL objProviderApprovalAgencyBAL = new ProviderApprovalAgencyBAL();

            ProviderApprovalAgency objIndividual = new ProviderApprovalAgency();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ProviderApprovalAgencyResponseList = null;
                    return objResponse;
                }

                try
                {
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";

                    //Method to Get all the previous schools 
                    List<ProviderApprovalAgency> lstProvider = objProviderApprovalAgencyBAL.Get_All_ProviderApprovalAgency(objProviderApprovalAgency);
                    objResponse.ProviderApprovalAgencyResponseList = lstProvider;

                    return objResponse;

                }
                catch (Exception ex)
                {
                    LogingHelper.SaveExceptionInfo(Key, ex, "ValidateIndividual", ENTITY.Enumeration.eSeverity.Error);

                    objResponse.Status = false;
                    objResponse.Message = ex.Message;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                    objResponse.ProviderApprovalAgencyResponseList = null;

                }


            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ValidateIndividual", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.ProviderApprovalAgencyResponseList = null;

            }
            return objResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="objProviderApprovalAgency"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("SaveProviderApprovalAgency")]
        public ProviderApprovalAgencyResponse SaveProviderApprovalAgency(string Key, ProviderApprovalAgency objProviderApprovalAgency)
        {
            LogingHelper.SaveAuditInfo(Key);

            ProviderApprovalAgencyResponse objProviderApprovalAgencyResponse = new ProviderApprovalAgencyResponse();

            if (!TokenHelper.ValidateToken(Key))
            {
                objProviderApprovalAgencyResponse.Status = false;
                objProviderApprovalAgencyResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objProviderApprovalAgencyResponse.Message = "User session has expired.";
                objProviderApprovalAgencyResponse.ProviderApprovalAgencyResponseList = null;
                return objProviderApprovalAgencyResponse;
            }

            try
            {
                if (objProviderApprovalAgency == null)
                {
                    objProviderApprovalAgencyResponse.Message = "Invalid Object.";
                    objProviderApprovalAgencyResponse.Status = false;
                    objProviderApprovalAgencyResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                    objProviderApprovalAgencyResponse.ResponseReason = "";
                    objProviderApprovalAgencyResponse.ProviderApprovalAgencyResponseList = null;
                    return objProviderApprovalAgencyResponse;
                }
                if (objProviderApprovalAgency.ProviderApprovalAgencyId == 0)
                    objProviderApprovalAgency.ProviderApprovalAgencyGuid = Guid.NewGuid().ToString();

                ProviderApprovalAgencyBAL objProviderApprovalAgencyBAL = new ProviderApprovalAgencyBAL();
                int output = objProviderApprovalAgencyBAL.Save_ProviderApprovalAgency(objProviderApprovalAgency);

                string ValidationResponse = "";

                if (output > 0)
                {
                    objProviderApprovalAgencyResponse.Message = Messages.SaveSuccess;
                    objProviderApprovalAgencyResponse.Status = true;
                    objProviderApprovalAgencyResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objProviderApprovalAgencyResponse.ResponseReason = "";

                    //Method to Get all the previous schools 
                    List<ProviderApprovalAgency> lstProviderApprovalAgencyBAL = objProviderApprovalAgencyBAL.Get_All_ProviderApprovalAgency(objProviderApprovalAgency);
                    objProviderApprovalAgencyResponse.ProviderApprovalAgencyResponseList = lstProviderApprovalAgencyBAL;

                    return objProviderApprovalAgencyResponse;
                }
                else
                {
                    objProviderApprovalAgencyResponse.Message = output.ToString();
                    objProviderApprovalAgencyResponse.Status = false;
                    objProviderApprovalAgencyResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objProviderApprovalAgencyResponse.ResponseReason = ValidationResponse;
                    objProviderApprovalAgencyResponse.ProviderApprovalAgencyResponseList = null;
                    return objProviderApprovalAgencyResponse;
                }
            }
            catch (Exception ex)
            {
                // LogingHelper.SaveExceptionInfo("", ex, "Login", ENTITY.Enumeration.eSeverity.Error);
                objProviderApprovalAgencyResponse.Status = false;
                objProviderApprovalAgencyResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objProviderApprovalAgencyResponse.Message = ex.Message;
                objProviderApprovalAgencyResponse.ProviderApprovalAgencyResponseList = null;

            }
            return objProviderApprovalAgencyResponse;


        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="objProviderApprovalAgency"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("DeleteProviderApprovalAgency")]
        public ProviderApprovalAgencyResponse DeleteProviderApprovalAgencyResponse(string Key, ProviderApprovalAgency objProviderApprovalAgency)
        {
            LogingHelper.SaveAuditInfo(Key);
            ProviderApprovalAgencyResponse objResponse = new ProviderApprovalAgencyResponse();
            ProviderApprovalAgencyBAL objProviderProgramBAL = new ProviderApprovalAgencyBAL();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.ProviderApprovalAgencyResponseList = null;
                return objResponse;
            }

            try
            {
                if (objProviderApprovalAgency == null)
                {
                    objResponse.Message = "Invalid Object.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                    objResponse.ResponseReason = "";
                    objResponse.ProviderApprovalAgencyResponseList = null;
                    return objResponse;
                }
                //job title
                int output = objProviderProgramBAL.DeleteProviderApprovalAgency(objProviderApprovalAgency);
                string ValidationResponse = "";

                if (output > 0)
                {
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";

                    //Method to Get all the previous schools 
                    List<ProviderApprovalAgency> lstProviderApprovalAgency = objProviderProgramBAL.Get_All_ProviderApprovalAgency(objProviderApprovalAgency);
                    objResponse.ProviderApprovalAgencyResponseList = lstProviderApprovalAgency;

                    return objResponse;
                }
                else
                {
                    objResponse.Message = output.ToString();
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    objResponse.ProviderApprovalAgencyResponseList = null;
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                // LogingHelper.SaveExceptionInfo("", ex, "Login", ENTITY.Enumeration.eSeverity.Error);
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ProviderApprovalAgencyResponseList = null;

            }
            return objResponse;


        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="objProviderEntityInformation"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("SaveProviderEntityInformation")]
        public ProviderLoginResponse SaveProviderEntityInformation(string Key, ProviderEntityInformation objProviderEntityInformation)
        {
            LogingHelper.SaveAuditInfo(Key);
            ProviderLoginResponse objResponse = new ProviderLoginResponse();
            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.ResponseReason = null;
                return objResponse;
            }

            try
            {
                if (objProviderEntityInformation == null)
                {
                    objResponse.Message = "Invalid Object.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }

                //Method to Save Address based on different address Types
                //Update school Telephone and School Website in Contact and ProviderContact Table
                ProviderEligibilityBAL objProviderEligibilityBAL = new ProviderEligibilityBAL();


                ProviderEligibility objProviderEligibility1 = new ProviderEligibility();
                objProviderEligibility1.ProviderId = objProviderEntityInformation.ProviderId;
                objProviderEligibility1.ProviderEligibilityId = objProviderEntityInformation.ProviderEligibilityId1;
                if (objProviderEntityInformation.ProviderEligibilityId1 == 0)
                    objProviderEligibility1.ProviderEligibilityIdGuid = Guid.NewGuid().ToString(); ;
                objProviderEligibility1.ContentItemLkId = objProviderEntityInformation.ContentItemLkId1;
                objProviderEligibility1.IsChecked = objProviderEntityInformation.IsChecked1;
                objProviderEligibilityBAL.Save_ProviderEligibility(objProviderEligibility1);

                ProviderEligibility objProviderEligibility2 = new ProviderEligibility();
                objProviderEligibility2.ProviderId = objProviderEntityInformation.ProviderId;
                objProviderEligibility2.ProviderEligibilityId = objProviderEntityInformation.ProviderEligibilityId2;
                if (objProviderEntityInformation.ProviderEligibilityId2 == 0)
                    objProviderEligibility2.ProviderEligibilityIdGuid = Guid.NewGuid().ToString(); ;
                objProviderEligibility2.ContentItemLkId = objProviderEntityInformation.ContentItemLkId2;
                objProviderEligibility2.IsChecked = objProviderEntityInformation.IsChecked2;
                objProviderEligibilityBAL.Save_ProviderEligibility(objProviderEligibility2);


                ProviderEligibility objProviderEligibility3 = new ProviderEligibility();
                objProviderEligibility3.ProviderId = objProviderEntityInformation.ProviderId;
                objProviderEligibility3.ProviderEligibilityId = objProviderEntityInformation.ProviderEligibilityId3;
                if (objProviderEntityInformation.ProviderEligibilityId3 == 0)
                    objProviderEligibility3.ProviderEligibilityIdGuid = Guid.NewGuid().ToString(); ;
                objProviderEligibility3.ContentItemLkId = objProviderEntityInformation.ContentItemLkId3;
                objProviderEligibility3.IsChecked = objProviderEntityInformation.IsChecked3;

                objProviderEligibilityBAL.Save_ProviderEligibility(objProviderEligibility3);

                ProviderEligibility objProviderEligibility4 = new ProviderEligibility();
                objProviderEligibility4.ProviderId = objProviderEntityInformation.ProviderId;
                objProviderEligibility4.ProviderEligibilityId = objProviderEntityInformation.ProviderEligibilityId4;
                if (objProviderEntityInformation.ProviderEligibilityId4 == 0)
                    objProviderEligibility4.ProviderEligibilityIdGuid = Guid.NewGuid().ToString(); ;
                objProviderEligibility4.ContentItemLkId = objProviderEntityInformation.ContentItemLkId4;
                objProviderEligibility4.IsChecked = objProviderEntityInformation.IsChecked4;

                objProviderEligibilityBAL.Save_ProviderEligibility(objProviderEligibility4);


                int output = 5;
                string ValidationResponse = "";


                if (output > 0)
                {
                    objResponse.Message = Messages.SaveSuccess;
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
        /// <param name="objProviderEntityInformation">Request object for Provider Instruction.</param>
        /// <param name="Key"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("GetAllEligibilityDetails")]
        public ProviderEntityInformation GetAllEligibilityDetails(string Key, ProviderEntityInformation objProviderEntityInformation)
        {
            LogingHelper.SaveAuditInfo(Key);

            ProviderEntityInformation objResponse = new ProviderEntityInformation();
            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.ResponseReason = null;
                return objResponse;
            }

            try
            {
                if (objProviderEntityInformation == null)
                {
                    objResponse.Message = "Invalid Object.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }

                ProviderEntityInformation objOutPutProviderInformation = new ProviderEntityInformation();
                ProviderEligibilityBAL objProviderEligibilityBAL = new ProviderEligibilityBAL();


                List<ProviderEligibility> objProviderEligibility1 = objProviderEligibilityBAL.Get_All_ProviderEligibility(objProviderEntityInformation.ProviderId, objProviderEntityInformation.ContentItemLkId1);
                if (objProviderEligibility1 != null && objProviderEligibility1.Count > 0)
                {
                    objResponse.IsChecked1 = objProviderEligibility1[0].IsChecked;
                    objResponse.ProviderEligibilityId1 = objProviderEligibility1[0].ProviderEligibilityId;
                }


                //Get School Website
                List<ProviderEligibility> objProviderEligibility2 = objProviderEligibilityBAL.Get_All_ProviderEligibility(objProviderEntityInformation.ProviderId, objProviderEntityInformation.ContentItemLkId2);
                if (objProviderEligibility2 != null && objProviderEligibility2.Count > 0)
                {
                    objResponse.ProviderEligibilityId2 = objProviderEligibility2[0].ProviderEligibilityId;
                    objResponse.IsChecked2 = objProviderEligibility2[0].IsChecked;
                }


                List<ProviderEligibility> objProviderEligibility3 = objProviderEligibilityBAL.Get_All_ProviderEligibility(objProviderEntityInformation.ProviderId, objProviderEntityInformation.ContentItemLkId3);
                if (objProviderEligibility3 != null && objProviderEligibility3.Count > 0)
                {
                    objResponse.ProviderEligibilityId3 = objProviderEligibility3[0].ProviderEligibilityId;
                    objResponse.IsChecked3 = objProviderEligibility3[0].IsChecked;
                }

                List<ProviderEligibility> objProviderEligibility4 = objProviderEligibilityBAL.Get_All_ProviderEligibility(objProviderEntityInformation.ProviderId, objProviderEntityInformation.ContentItemLkId4);
                if (objProviderEligibility4 != null && objProviderEligibility4.Count > 0)
                {
                    objResponse.ProviderEligibilityId4 = objProviderEligibility4[0].ProviderEligibilityId;
                    objResponse.IsChecked4 = objProviderEligibility4[0].IsChecked;
                }

                int output = 5;
                string ValidationResponse = "";

                if (output > 0)
                {
                    objResponse.Message = Messages.SaveSuccess;
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



        #endregion

        #region About


        #endregion

        #region Staff


        #endregion

        #region School Xref


        #endregion

        #region Staff Xref


        #endregion

        #region Transcript


        #endregion

        #region Enrollment Agreement


        #endregion

        #region Course Catalog


        #endregion

        #region Curriculum


        #endregion

        #region Fees


        #endregion

        #region Notes


        #endregion

        #region Investigative Notes

        #endregion

        #region Documents


        #endregion

        #region Shekhar 

        /// <summary>
        /// This method is to Save the School Informations
        /// </summary>
        /// <param name="Key">Security Key for API.</param>
        /// <param name="ObjProviderStaff">Request object for Provider Instruction.</param>
        [AcceptVerbs("POST")]
        [ActionName("SaveProviderStaff")]
        public BaseEntityServiceResponse SaveProviderStaff(string Key, ProvIndvNameTitle ObjProviderStaff)
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

            if (ObjProviderStaff == null)
            {
                objResponse.Message = "Invalid Object.";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                objResponse.ResponseReason = "";
                return objResponse;
            }

            try
            {

                if (ObjProviderStaff.ProviderStaffId == 0)
                {
                    #region IndividualName

                    IndividualNameBAL objIndNameBAL = new IndividualNameBAL();
                    IndividualName objIndName = new IndividualName();

                    objIndName.IndividualNameId = 0;
                    objIndName.IndividualId = 0;
                    objIndName.FirstName = ObjProviderStaff.ProviderStaffFirstName;
                    objIndName.MiddleName = "";
                    objIndName.LastName = ObjProviderStaff.ProviderStaffLastName;
                    objIndName.IndividualNameStatusId = 22;
                    objIndName.IsActive = true;
                    objIndName.IsDeleted = false;
                    objIndName.CreatedBy = 0;
                    objIndName.CreatedOn = DateTime.Now;
                    objIndName.IndividualNameGuid = Guid.NewGuid().ToString();
                    objIndName.IndividualNameTypeId = 13;

                    int IndividualNameId = objIndNameBAL.Save_IndividualName(objIndName);

                    #endregion

                    #region Provider Individual Name Info

                    // Insert IndividualId as 0 and IndividualNameId from IndividualName table

                    ProviderIndividualName objProviderIndName = new ProviderIndividualName();
                    ProviderIndividualNameBAL objProviderIndNameBAL = new ProviderIndividualNameBAL();

                    objProviderIndName.ProviderIndvNameInfoId = 0;
                    objProviderIndName.ProviderId = ObjProviderStaff.ProviderId;
                    objProviderIndName.IndividualId = 0;
                    objProviderIndName.IndividualNameId = IndividualNameId;
                    objProviderIndName.ApplicationId = ObjProviderStaff.ApplicationId;
                    objProviderIndName.IsActive = true;
                    objProviderIndName.IsDeleted = false;
                    objProviderIndName.CreatedBy = 0;
                    objProviderIndName.CreatedOn = DateTime.Now;
                    objProviderIndName.ProviderIndvNameInfoGuid = Guid.NewGuid().ToString();

                    int ProviderIndvNameInfoId = objProviderIndNameBAL.Save_ProviderIndividualName(objProviderIndName);

                    #endregion

                    #region Provider Staff

                    ProviderStaff objProvStaff = new ProviderStaff();
                    ProviderBAL objProviderStaffBAL = new ProviderBAL();

                    objProvStaff.ProviderStaffId = 0;
                    objProvStaff.ProviderIndvNameInfoId = ProviderIndvNameInfoId;
                    objProvStaff.ProviderId = ObjProviderStaff.ProviderId;
                    objProvStaff.ApplicationId = ObjProviderStaff.ApplicationId;
                    objProvStaff.ProviderContactId = 0;
                    objProvStaff.IsBackgroundCheckReq = ObjProviderStaff.IsBackgroundCheckReq;
                    objProvStaff.CAMTCNumber = ObjProviderStaff.CAMTCNumber;
                    objProvStaff.ReferenceNumber = ObjProviderStaff.ReferenceNumber;
                    objProvStaff.IsActive = ObjProviderStaff.IsActive;
                    objProvStaff.IsDeleted = ObjProviderStaff.IsDeleted;
                    objProvStaff.CreatedBy = ObjProviderStaff.CreatedBy;
                    objProvStaff.CreatedOn = ObjProviderStaff.CreatedOn;
                    objProvStaff.ModifiedBy = ObjProviderStaff.ModifiedBy;
                    objProvStaff.ModifiedOn = ObjProviderStaff.ModifiedOn;
                    objProvStaff.ProviderStaffGuid = ObjProviderStaff.ProviderStaffGuid;

                    int ProviderStaffId = objProviderStaffBAL.SaveProviderStaff(objProvStaff);

                    #endregion

                    #region Provider Individual Name Title/Position

                    ProvIndvNameTitle objProvIndvNameTitle = new ProvIndvNameTitle();
                    ProviderBAL objProviderBAL = new ProviderBAL();

                    string conId = ObjProviderStaff.ProvIndvNameTitlePositionId; ;
                    string conTitle = ObjProviderStaff.ProvIndvNameTitlePosition;

                    string[] Id = conId.Split(',');
                    string[] Title = conTitle.Split(',');

                    for (int i = 0; i < Id.Count(); i++)
                    {
                        objProvIndvNameTitle.ProvIndvNameTitlePosId = 0;
                        objProvIndvNameTitle.ProviderIndvNameInfoId = ProviderIndvNameInfoId;
                        objProvIndvNameTitle.ProviderId = ObjProviderStaff.ProviderId;
                        objProvIndvNameTitle.ApplicationId = ObjProviderStaff.ApplicationId;
                        objProvIndvNameTitle.ProviderStaffId = ProviderStaffId;
                        objProvIndvNameTitle.ProvIndvNameTitlePositionId = Id[i];
                        objProvIndvNameTitle.ProvIndvNameTitlePosition = Title[i];

                        objProvIndvNameTitle.ReferenceNumber = "";
                        objProvIndvNameTitle.IsActive = true;
                        objProvIndvNameTitle.IsDeleted = false;
                        objProvIndvNameTitle.CreatedBy = 0;
                        objProvIndvNameTitle.CreatedOn = DateTime.Now;
                        objProvIndvNameTitle.ModifiedBy = null;
                        objProvIndvNameTitle.ModifiedOn = null;
                        objProvIndvNameTitle.ProvIndvNameTitlePosGuid = Guid.NewGuid().ToString();

                        int ProvIndvNameTitleId = objProviderBAL.SaveProvIndvNameTitle(objProvIndvNameTitle);

                    }

                    #endregion
                }

                else
                {
                    #region Provider Staff

                    ProviderStaff objProvStaff = new ProviderStaff();
                    ProviderBAL objProviderStaffBAL = new ProviderBAL();

                    objProvStaff.ProviderStaffId = ObjProviderStaff.ProviderStaffId;
                    objProvStaff.ProviderIndvNameInfoId = ObjProviderStaff.ProviderIndvNameInfoId;
                    objProvStaff.ProviderId = ObjProviderStaff.ProviderId;
                    objProvStaff.ApplicationId = ObjProviderStaff.ApplicationId;
                    objProvStaff.ProviderContactId = 0;
                    objProvStaff.IsBackgroundCheckReq = ObjProviderStaff.IsBackgroundCheckReq;
                    objProvStaff.CAMTCNumber = ObjProviderStaff.CAMTCNumber;
                    objProvStaff.ReferenceNumber = ObjProviderStaff.ReferenceNumber;
                    objProvStaff.IsActive = ObjProviderStaff.IsActive;
                    objProvStaff.IsDeleted = ObjProviderStaff.IsDeleted;
                    objProvStaff.CreatedBy = ObjProviderStaff.CreatedBy;
                    objProvStaff.CreatedOn = ObjProviderStaff.CreatedOn;
                    objProvStaff.ModifiedBy = ObjProviderStaff.ModifiedBy;
                    objProvStaff.ModifiedOn = ObjProviderStaff.ModifiedOn;
                    objProvStaff.ProviderStaffGuid = ObjProviderStaff.ProviderStaffGuid;

                    int ProviderStaffId = objProviderStaffBAL.SaveProviderStaff(objProvStaff);

                    #endregion
                }

                objResponse.Message = Messages.SaveSuccess;
                objResponse.Status = true;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ProviderStaffSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }

            return objResponse;


        }

        /// <summary>
        /// This method is to Get the grid values of Provider Staff
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ApplicationId">Application Id</param>
        /// <param name="ProviderId">Provider Id</param>

        [AcceptVerbs("GET")]
        [ActionName("GetAllProviderStaffDetails")]
        public ProviderOnLoadResponse GetAllProviderStaffDetails(string Key, int ApplicationId, int ProviderId)
        {
            ProviderOnLoadResponse objResponse = new ProviderOnLoadResponse();

            LogingHelper.SaveAuditInfo(Key);

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    return objResponse;
                }

                ProviderBAL objProviderBAL = new ProviderBAL();

                //Method to get provider staff details grid
                List<ProviderStaff> lstProviderStaff = objProviderBAL.GetAllProviderStaffDetails(ApplicationId, ProviderId);
                objResponse.ListOfProviderStaffDetails = lstProviderStaff;

                if (objResponse != null)
                {
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
                else
                {
                    objResponse.Message = "Fail";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo("", ex, "GetProviderStaffDetails", ENTITY.Enumeration.eSeverity.Error);
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;

            }
            return objResponse;


        }

        /// <summary>
        /// This method is to Save the Other Program in "About the School" Tab
        /// </summary>
        /// <param name="Key">Security Key for API.</param>
        /// <param name="ObjProviderOtherProgram">Request object for Provider Instruction.</param>
        [AcceptVerbs("POST")]
        [ActionName("SaveProviderOtherProgram")]
        public BaseEntityServiceResponse SaveProviderOtherProgramName(string Key, ProviderOtherProgramName ObjProviderOtherProgram)
        {
            int CreateOrModify = 0;
            try
            {
                CreateOrModify = TokenHelper.GetTokenByKey(Key).UserId;
            }
            catch { }

            LogingHelper.SaveAuditInfo();

            BaseEntityServiceResponse objResponse = new BaseEntityServiceResponse();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Message = "User session has expired.";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.ResponseReason = "";
                return objResponse;
            }

            if (ObjProviderOtherProgram == null)
            {
                objResponse.Message = "Invalid Object.";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                objResponse.ResponseReason = "";
                return objResponse;
            }

            try
            {
                if (ObjProviderOtherProgram.ProviderOtherProgramId == 0)
                    ObjProviderOtherProgram.ProviderOtherProgramGuid = Guid.NewGuid().ToString();

                ProviderBAL objProviderOtherProgramBAL = new ProviderBAL();
                int ProviderOtherProgramId = objProviderOtherProgramBAL.SaveProviderOtherProgram(ObjProviderOtherProgram);

                objResponse.Message = Messages.SaveSuccess;
                objResponse.Status = true;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ProviderOtherProgramNameSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }

            return objResponse;


        }

        /// <summary>
        /// This method is to Get the grid values of Provider Other Program
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ApplicationId">Application Id</param>
        /// <param name="ProviderId">Provider Id</param>
        [AcceptVerbs("GET")]
        [ActionName("GetAllProviderOtherProgram")]
        public ProviderOtherProgramNameGetResponse GetAllProviderOtherProgram(string Key, int ApplicationId, int ProviderId)
        {
            ProviderOtherProgramNameGetResponse objResponse = new ProviderOtherProgramNameGetResponse();

            LogingHelper.SaveAuditInfo(Key);

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    return objResponse;
                }

                ProviderBAL objProviderBAL = new ProviderBAL();

                //Method to get provider staff details grid
                List<ProviderOtherProgramName> lstProviderOtherProgram = objProviderBAL.GetAllProviderOtherProgram(ApplicationId, ProviderId);
                objResponse.ProviderOtherProgramList = lstProviderOtherProgram;

                if (objResponse != null)
                {
                    objResponse.Message = "Success";
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
                else
                {
                    objResponse.Message = "Fail";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo("", ex, "GetProviderOtherProgram", ENTITY.Enumeration.eSeverity.Error);
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;

            }
            return objResponse;


        }


        /// <summary>
        /// This method is to Save the Graduates Number in "About the School" Tab
        /// </summary>
        /// <param name="Key">Security Key for API.</param>
        /// <param name="ObjProviderGraduatesNumber">Request object for Provider Instruction.</param>
        [AcceptVerbs("POST")]
        [ActionName("SaveProviderGraduatesNumber")]
        public BaseEntityServiceResponse SaveProviderGraduatesNumber(string Key, ProviderGraduatesNumber ObjProviderGraduatesNumber)
        {
            int CreateOrModify = 0;
            try
            {
                CreateOrModify = TokenHelper.GetTokenByKey(Key).UserId;
            }
            catch { }

            LogingHelper.SaveAuditInfo();

            BaseEntityServiceResponse objResponse = new BaseEntityServiceResponse();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Message = "User session has expired.";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.ResponseReason = "";
                return objResponse;
            }

            if (ObjProviderGraduatesNumber == null)
            {
                objResponse.Message = "Invalid Object.";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                objResponse.ResponseReason = "";
                return objResponse;
            }

            try
            {
                if (ObjProviderGraduatesNumber.ProviderGraduatesNumberId == 0)
                    ObjProviderGraduatesNumber.ProviderGraduatesNumberGuid = Guid.NewGuid().ToString();
                ProviderBAL objProviderGraduatesNumberBAL = new ProviderBAL();
                int ProviderGraduatesNumberId = objProviderGraduatesNumberBAL.SaveProviderGraduatesNumber(ObjProviderGraduatesNumber);

                objResponse.Message = Messages.SaveSuccess;
                objResponse.Status = true;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ProviderOtherProgramNameSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }

            return objResponse;


        }

        /// <summary>
        /// This method is to Get the grid values of Provider Graduates Number in "About the School" Tab
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ApplicationId">Application Id</param>
        /// <param name="ProviderId">Provider Id</param>
        [AcceptVerbs("GET")]
        [ActionName("GetAllProviderGraduatesNumber")]
        public ProviderGraduatesNumberGetResponse GetAllProviderGraduatesNumber(string Key, int ApplicationId, int ProviderId)
        {
            ProviderGraduatesNumberGetResponse objResponse = new ProviderGraduatesNumberGetResponse();

            LogingHelper.SaveAuditInfo(Key);

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    return objResponse;
                }

                ProviderBAL objProviderBAL = new ProviderBAL();

                //Method to get provider staff details grid
                List<ProviderGraduatesNumber> lstProviderGraduatesNumber = objProviderBAL.GetAllProviderGraduatesNumber(ApplicationId, ProviderId);
                objResponse.ProviderGraduatesNumberList = lstProviderGraduatesNumber;

                if (objResponse != null)
                {
                    objResponse.Message = "Success";
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
                else
                {
                    objResponse.Message = "Fail";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo("", ex, "GetProviderGraduatesNumber", ENTITY.Enumeration.eSeverity.Error);
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;

            }
            return objResponse;


        }


        /// <summary>
        /// This method is to Get the Tab Status
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ApplicationId">Application Id</param>
        /// <param name="ProviderId">Provider Id</param>
        [AcceptVerbs("GET")]
        [ActionName("GetAllProviderTabStatus")]
        public ProviderTabStatusGetResponse GetAllProviderTabStatus(string Key, int ApplicationId, int ProviderId)
        {
            ProviderTabStatusGetResponseRequest objResponse = new ProviderTabStatusGetResponseRequest();

            LogingHelper.SaveAuditInfo(Key);

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    return objResponse;
                }

                ProviderBAL objProviderBAL = new ProviderBAL();

                //Method to get provider staff details grid
                List<ProviderTabStatusGetResponse> lstProviderOtherProgram = objProviderBAL.GetAllProviderTabStatus(ApplicationId, ProviderId);
                objResponse.ProviderTabStatusList = lstProviderOtherProgram;

                if (objResponse != null)
                {
                    objResponse.Message = "Success";
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
                else
                {
                    objResponse.Message = "Fail";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo("", ex, "GetProviderTabStatus", ENTITY.Enumeration.eSeverity.Error);
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;

            }
            return objResponse;
        }

        /// <summary>
        /// This method is to Get the checked values of Type of Business Organisation in "About the School" Tab
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ApplicationId">Application Id</param>
        /// <param name="ProviderId">Provider Id</param>
        [AcceptVerbs("GET")]
        [ActionName("GetProviderBusinessTypeByProviderId")]
        public ProviderBusinessTypeGetResponse GetProviderBusinessTypeByProviderId(string Key, int ApplicationId, int ProviderId)
        {
            ProviderBusinessTypeGetResponse objResponse = new ProviderBusinessTypeGetResponse();

            LogingHelper.SaveAuditInfo(Key);

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    return objResponse;
                }

                ProviderBAL objProviderBAL = new ProviderBAL();

                List<ProviderBusinessType> lstProviderBusinessType = objProviderBAL.GetProviderBusinessTypeByProviderId(ApplicationId, ProviderId);
                objResponse.ProviderBusinessTypeList = lstProviderBusinessType;

                if (objResponse != null)
                {
                    objResponse.Message = "Success";
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
                else
                {
                    objResponse.Message = "Fail";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo("", ex, "GetProviderBusinessType", ENTITY.Enumeration.eSeverity.Error);
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;

            }
            return objResponse;


        }


        /// <summary>
        /// This method is to Save the Business Type in "About the School" Tab
        /// </summary>
        /// <param name="Key">Security Key for API.</param>
        /// <param name="objProviderBusinessType">Request object for Provider Business Type.</param>
        [AcceptVerbs("POST")]
        [ActionName("SaveProviderBusinessType")]
        public BaseEntityServiceResponse SaveProviderBusinessType(string Key, List<ProviderBusinessType> objProviderBusinessType)
        {
            int CreateOrModify = 0;
            try
            {
                CreateOrModify = TokenHelper.GetTokenByKey(Key).UserId;
            }
            catch { }

            LogingHelper.SaveAuditInfo();

            BaseEntityServiceResponse objResponse = new BaseEntityServiceResponse();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Message = "User session has expired.";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.ResponseReason = "";
                return objResponse;
            }

            if (objProviderBusinessType == null)
            {
                objResponse.Message = "Invalid Object.";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                objResponse.ResponseReason = "";
                return objResponse;
            }

            try
            {
                ProviderBAL objProviderBAL = new ProviderBAL();
                int ProviderBusinessTypeId = 0;

                if (objProviderBusinessType.Count > 0)
                {
                    for (int i = 0; i < objProviderBusinessType.Count; i++)
                    {
                        ProviderBusinessTypeId = objProviderBAL.SaveProviderBusinessType(objProviderBusinessType[i]);
                    }
                }

                objResponse.Message = Messages.SaveSuccess;
                objResponse.Status = true;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ProviderBusinessTypeSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }

            return objResponse;


        }


        /// <summary>
        /// This method is to Save the Related Schools in "About the School" Tab
        /// </summary>
        /// <param name="Key">Security Key for API.</param>
        /// <param name="ObjProviderRelatedSchools">Request object for Provider Instruction.</param>
        [AcceptVerbs("POST")]
        [ActionName("SaveProviderRelatedSchools")]
        public BaseEntityServiceResponse SaveProviderRelatedSchools(string Key, ProviderRelatedSchools ObjProviderRelatedSchools)
        {
            int CreateOrModify = 0;
            try
            {
                CreateOrModify = TokenHelper.GetTokenByKey(Key).UserId;
            }
            catch { }

            LogingHelper.SaveAuditInfo();

            BaseEntityServiceResponse objResponse = new BaseEntityServiceResponse();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Message = "User session has expired.";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.ResponseReason = "";
                return objResponse;
            }

            if (ObjProviderRelatedSchools == null)
            {
                objResponse.Message = "Invalid Object.";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                objResponse.ResponseReason = "";
                return objResponse;
            }

            try
            {

                // Save Provider Name -> ProviderDAL.SaveProviderNames
                // Save ProviderRelatedSchools
                // Save Address -> AddressDAL.Save_address
                // Save Contact -> ContactDAL.Save_Contact
                // Save ProviderNameAddress with insert in LK table
                // Save ProviderNameContact with insert in LK table


                ProviderNames objProviderNames = new ProviderNames();
                objProviderNames.ProviderNameId = ObjProviderRelatedSchools.ProviderNameId;
                objProviderNames.ApplicationId = ObjProviderRelatedSchools.ApplicationId;
                objProviderNames.ProviderId = ObjProviderRelatedSchools.ProviderId;
                objProviderNames.IndividualId = 0;
                objProviderNames.ProviderName = ObjProviderRelatedSchools.ProviderName;
                objProviderNames.ProviderNameStatusId = 2;
                objProviderNames.ProviderNameTypeId = 1;
                objProviderNames.ProviderNameGuid = Guid.NewGuid().ToString();
                ProviderBAL objProviderBAL = new ProviderBAL();
                int ProviderNameId = objProviderBAL.SaveProviderNames(objProviderNames);


                ProviderRelatedSchools objProviderRelatedSchools = new ProviderRelatedSchools();
                objProviderRelatedSchools.ProviderRelatedSchoolId = ObjProviderRelatedSchools.ProviderRelatedSchoolId;
                objProviderRelatedSchools.ProviderId = ObjProviderRelatedSchools.ProviderId;
                objProviderRelatedSchools.ProviderNameId = ProviderNameId;
                objProviderRelatedSchools.ApplicationId = ObjProviderRelatedSchools.ApplicationId;
                objProviderRelatedSchools.DateAssociated = DateTime.Now;
                objProviderRelatedSchools.IsActive = true;
                objProviderRelatedSchools.IsDeleted = false;
                objProviderRelatedSchools.CreatedBy = ObjProviderRelatedSchools.ProviderId;
                objProviderRelatedSchools.CreatedOn = DateTime.Now;
                objProviderRelatedSchools.ProviderRelatedSchoolGuid = Guid.NewGuid().ToString();
                ProviderRelatedSchoolsBAL objProviderRSBAL = new ProviderRelatedSchoolsBAL();
                int ProviderRelatedSchoolId = objProviderRSBAL.SaveProviderRelatedSchools(objProviderRelatedSchools);


                Address objAddress = new Address();
                objAddress.AddressId = ObjProviderRelatedSchools.AddressId;
                objAddress.Addressee = "";
                objAddress.StreetLine1 = ObjProviderRelatedSchools.StreetLine1;
                objAddress.StreetLine2 = ObjProviderRelatedSchools.StreetLine2;
                objAddress.City = ObjProviderRelatedSchools.City;
                objAddress.StateCode = ObjProviderRelatedSchools.StateCode;
                objAddress.Zip = ObjProviderRelatedSchools.ZIP;
                objAddress.CountryId = ObjProviderRelatedSchools.CountryId;
                objAddress.CountyId = ObjProviderRelatedSchools.CountyId;
                objAddress.IsActive = true;
                objAddress.IsDeleted = false;
                objAddress.AddressGuid = Guid.NewGuid().ToString();
                objAddress.Authenticator = Guid.NewGuid().ToString();
                AddressBAL objAddressBAL = new AddressBAL();
                int AddressId = objAddressBAL.Save_address(objAddress);

                Contact objContact = new Contact();
                objContact.ContactId = objProviderRelatedSchools.PhoneId;
                objContact.ContactFirstName = ObjProviderRelatedSchools.ContactFirstName;
                objContact.ContactLastName = ObjProviderRelatedSchools.ContactLastName;
                objContact.ContactTypeId = 6;
                objContact.Code = "P";
                objContact.ContactInfo = ObjProviderRelatedSchools.Phone;
                objContact.Authenticator = "";

                objContact.IsActive = true;
                objContact.IsDeleted = false;
                objContact.ContactGuid = Guid.NewGuid().ToString();
                objContact.Authenticator = Guid.NewGuid().ToString();
                ContactBAL objContactBAL = new ContactBAL();
                int ContactId_Phone = objContactBAL.Save_Contact(objContact);

                objContact = new Contact();
                objContact.ContactId = ObjProviderRelatedSchools.EmailId;
                objContact.ContactFirstName = ObjProviderRelatedSchools.ContactFirstName;
                objContact.ContactLastName = ObjProviderRelatedSchools.ContactLastName;
                objContact.ContactTypeId = 8;
                objContact.Code = "E";
                objContact.ContactInfo = ObjProviderRelatedSchools.Email;
                objContact.Authenticator = "";
                objContact.IsActive = true;
                objContact.IsDeleted = false;
                objContact.ContactGuid = Guid.NewGuid().ToString();
                objContactBAL = new ContactBAL();
                int ContactId_Email = objContactBAL.Save_Contact(objContact);

                objContact = new Contact();
                objContact.ContactId = ObjProviderRelatedSchools.WebsiteId;
                objContact.ContactFirstName = ObjProviderRelatedSchools.ContactFirstName;
                objContact.ContactLastName = ObjProviderRelatedSchools.ContactLastName;
                objContact.ContactTypeId = 10;
                objContact.Code = "L";
                objContact.ContactInfo = ObjProviderRelatedSchools.Website;
                objContact.Authenticator = "";
                objContact.IsActive = true;
                objContact.IsDeleted = false;
                objContact.ContactGuid = Guid.NewGuid().ToString();
                objContactBAL = new ContactBAL();
                int ContactId_Web = objContactBAL.Save_Contact(objContact);
                if (!ObjProviderRelatedSchools.Isupdate)
                {

                    ProviderNameAddress objProviderNameAddress = new ProviderNameAddress();
                    objProviderNameAddress.ProviderNameAddressId = ObjProviderRelatedSchools.ProviderNameAddressId;
                    objProviderNameAddress.ProviderId = ObjProviderRelatedSchools.ProviderId;
                    objProviderNameAddress.ProviderNameId = ProviderNameId;
                    objProviderNameAddress.ApplicationId = ObjProviderRelatedSchools.ApplicationId;
                    objProviderNameAddress.AddressId = AddressId;
                    objProviderNameAddress.AddressTypeId = 6;
                    objProviderNameAddress.BeginDate = DateTime.Now;
                    objProviderNameAddress.IsMailingSameasPhysical = false;
                    objProviderNameAddress.IsActive = true;
                    objProviderNameAddress.IsDeleted = false;
                    objProviderNameAddress.CreatedBy = 0;
                    objProviderNameAddress.CreatedOn = DateTime.Now;
                    objProviderNameAddress.ProviderNameAddressGuid = Guid.NewGuid().ToString();
                    ProviderNameBAL objProvNameBAL = new ProviderNameBAL();
                    int ProviderNameAddressId = objProvNameBAL.SaveProviderNameAddress(objProviderNameAddress);


                    ProviderNameContact objProviderNameContact = new ProviderNameContact();
                    objProviderNameContact.ProviderNameContactId = ObjProviderRelatedSchools.ProviderNameContactId;
                    objProviderNameContact.ProviderId = ObjProviderRelatedSchools.ProviderId;
                    objProviderNameContact.ProviderNameId = ProviderNameId;
                    objProviderNameContact.ApplicationId = ObjProviderRelatedSchools.ApplicationId;
                    objProviderNameContact.ContactId = ContactId_Phone;
                    objProviderNameContact.ContactTypeId = 6;
                    objProviderNameContact.BeginDate = DateTime.Now;
                    objProviderNameContact.IsPreferredContact = false;
                    objProviderNameContact.IsMobile = false;
                    objProviderNameContact.IsActive = true;
                    objProviderNameContact.IsDeleted = false;
                    objProviderNameContact.CreatedBy = 0;
                    objProviderNameContact.CreatedOn = DateTime.Now;
                    objProviderNameContact.ProviderNameContactGuid = Guid.NewGuid().ToString();
                    objProvNameBAL = new ProviderNameBAL();
                    int ProviderNameContactId_Phone = objProvNameBAL.SaveProviderNameContact(objProviderNameContact);

                    objProviderNameContact = new ProviderNameContact();
                    objProviderNameContact.ProviderNameContactId = ObjProviderRelatedSchools.ProviderNameContactId;
                    objProviderNameContact.ProviderId = ObjProviderRelatedSchools.ProviderId;
                    objProviderNameContact.ProviderNameId = ProviderNameId;
                    objProviderNameContact.ApplicationId = ObjProviderRelatedSchools.ApplicationId;
                    objProviderNameContact.ContactId = ContactId_Email;
                    objProviderNameContact.ContactTypeId = 8;
                    objProviderNameContact.BeginDate = DateTime.Now;
                    objProviderNameContact.IsPreferredContact = false;
                    objProviderNameContact.IsMobile = false;
                    objProviderNameContact.IsActive = true;
                    objProviderNameContact.IsDeleted = false;
                    objProviderNameContact.CreatedBy = 0;
                    objProviderNameContact.CreatedOn = DateTime.Now;
                    objProviderNameContact.ProviderNameContactGuid = Guid.NewGuid().ToString();
                    objProvNameBAL = new ProviderNameBAL();
                    int ProviderNameContactId_Email = objProvNameBAL.SaveProviderNameContact(objProviderNameContact);

                    objProviderNameContact = new ProviderNameContact();
                    objProviderNameContact.ProviderNameContactId = ObjProviderRelatedSchools.ProviderNameContactId;
                    objProviderNameContact.ProviderId = ObjProviderRelatedSchools.ProviderId;
                    objProviderNameContact.ProviderNameId = ProviderNameId;
                    objProviderNameContact.ApplicationId = ObjProviderRelatedSchools.ApplicationId;
                    objProviderNameContact.ContactId = ContactId_Web;
                    objProviderNameContact.ContactTypeId = 10;
                    objProviderNameContact.BeginDate = DateTime.Now;
                    objProviderNameContact.IsPreferredContact = false;
                    objProviderNameContact.IsMobile = false;
                    objProviderNameContact.IsActive = true;
                    objProviderNameContact.IsDeleted = false;
                    objProviderNameContact.CreatedBy = 0;
                    objProviderNameContact.CreatedOn = DateTime.Now;
                    objProviderNameContact.ProviderNameContactGuid = Guid.NewGuid().ToString();
                    objProvNameBAL = new ProviderNameBAL();
                    int ProviderNameContactId_Web = objProvNameBAL.SaveProviderNameContact(objProviderNameContact);


                    ProviderRelatedSchoolsAddLK objProviderNameAddressLK = new ProviderRelatedSchoolsAddLK();
                    objProviderNameAddressLK.ProviderRelatedSchoolId = ProviderRelatedSchoolId;
                    objProviderNameAddressLK.ProviderId = ObjProviderRelatedSchools.ProviderId;
                    objProviderNameAddressLK.ProviderNameId = ProviderNameId;
                    objProviderNameAddressLK.ApplicationId = ObjProviderRelatedSchools.ApplicationId;
                    objProviderNameAddressLK.ProviderNameAddressId = ProviderNameAddressId;
                    objProviderNameAddressLK.ProviderRelatedSchoolAddressLkGuid = Guid.NewGuid().ToString();
                    objProviderRSBAL = new ProviderRelatedSchoolsBAL();
                    int ProviderRSLK = objProviderRSBAL.SaveProviderRelatedSchoolAddressLK(objProviderNameAddressLK);


                    ProviderRelatedSchoolsConLK objProviderNameContactLK = new ProviderRelatedSchoolsConLK();
                    objProviderNameContactLK.ProviderRelatedSchoolId = ProviderRelatedSchoolId;
                    objProviderNameContactLK.ProviderId = ObjProviderRelatedSchools.ProviderId;
                    objProviderNameContactLK.ProviderNameId = ProviderNameId;
                    objProviderNameContactLK.ApplicationId = ObjProviderRelatedSchools.ApplicationId;
                    objProviderNameContactLK.ProviderNameContactId = ProviderNameContactId_Phone;
                    objProviderNameContactLK.ProviderRelatedSchoolContactLkGuid = Guid.NewGuid().ToString();
                    objProviderRSBAL = new ProviderRelatedSchoolsBAL();
                    ProviderRSLK = objProviderRSBAL.SaveProviderRelatedSchoolContactLK(objProviderNameContactLK);

                    objProviderNameContactLK = new ProviderRelatedSchoolsConLK();
                    objProviderNameContactLK.ProviderRelatedSchoolId = ProviderRelatedSchoolId;
                    objProviderNameContactLK.ProviderId = ObjProviderRelatedSchools.ProviderId;
                    objProviderNameContactLK.ProviderNameId = ProviderNameId;
                    objProviderNameContactLK.ApplicationId = ObjProviderRelatedSchools.ApplicationId;
                    objProviderNameContactLK.ProviderNameContactId = ProviderNameContactId_Email;
                    objProviderNameContactLK.ProviderRelatedSchoolContactLkGuid = Guid.NewGuid().ToString();
                    objProviderRSBAL = new ProviderRelatedSchoolsBAL();
                    ProviderRSLK = objProviderRSBAL.SaveProviderRelatedSchoolContactLK(objProviderNameContactLK);

                    objProviderNameContactLK = new ProviderRelatedSchoolsConLK();
                    objProviderNameContactLK.ProviderRelatedSchoolId = ProviderRelatedSchoolId;
                    objProviderNameContactLK.ProviderId = ObjProviderRelatedSchools.ProviderId;
                    objProviderNameContactLK.ProviderNameId = ProviderNameId;
                    objProviderNameContactLK.ApplicationId = ObjProviderRelatedSchools.ApplicationId;
                    objProviderNameContactLK.ProviderNameContactId = ProviderNameContactId_Web;
                    objProviderNameContactLK.ProviderRelatedSchoolContactLkGuid = Guid.NewGuid().ToString();
                    objProviderRSBAL = new ProviderRelatedSchoolsBAL();
                    ProviderRSLK = objProviderRSBAL.SaveProviderRelatedSchoolContactLK(objProviderNameContactLK);

                }
                objResponse.Message = Messages.SaveSuccess;
                objResponse.Status = true;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ProviderRelatedSchoolSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }

            return objResponse;


        }

        /// <summary>
        /// This method is to Get the grid values of Provider Related Schools
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ApplicationId">Application Id</param>
        /// <param name="ProviderId">Provider Id</param>
        [AcceptVerbs("GET")]
        [ActionName("GetAllProviderRelatedSchools")]
        public ProviderRelatedSchoolsGetResponse GetAllProviderRelatedSchools(string Key, int ApplicationId, int ProviderId)
        {
            ProviderRelatedSchoolsGetResponse objResponse = new ProviderRelatedSchoolsGetResponse();

            LogingHelper.SaveAuditInfo(Key);

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    return objResponse;
                }

                ProviderRelatedSchoolsBAL objProviderBAL = new ProviderRelatedSchoolsBAL();

                List<ProviderRelatedSchools> lstProviderOtherProgram = objProviderBAL.Get_ProviderRelatedSchool_By_ProviderId(ApplicationId, ProviderId);
                objResponse.ProviderRelatedSchoolsList = lstProviderOtherProgram;

                if (objResponse != null)
                {
                    objResponse.Message = "Success";
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
                else
                {
                    objResponse.Message = "Fail";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo("", ex, "GetProviderRelatedSchool", ENTITY.Enumeration.eSeverity.Error);
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;

            }
            return objResponse;


        }

        #endregion

    }
}