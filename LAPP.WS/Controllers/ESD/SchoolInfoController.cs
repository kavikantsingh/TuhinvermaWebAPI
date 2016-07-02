using LAPP.BAL;
using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.GlobalFunctions;
using LAPP.LOGING;
using LAPP.WS.App_Helper;
using LAPP.WS.App_Helper.Common;
using LAPP.WS.ValidateController.SchoolInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LAPP.WS.Controllers.ESD
{
    /// <summary>
    /// SchooolInfo tab CRUD Operations
    /// </summary>
    public class SchoolInfoController : ApiController
    {
        #region ProviderInfo

        [AcceptVerbs("POST")]
        [ActionName("ProviderInfoSave")]
        public ProviderAddressCommonResponseRequest ProviderInfoSave(string Key, ProviderAddressCommonResponse objIndividualAddress)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo(Key);

            ProviderAddressCommonResponseRequest objResponse = new ProviderAddressCommonResponseRequest();
            IndividualAddressBAL objBAL = new IndividualAddressBAL();
            IndividualAddress objEntity = new IndividualAddress();
            List<ProviderAddressCommonResponse> lstEntity = new List<ProviderAddressCommonResponse>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.ProviderAddressCommonResponse = null;
                return objResponse;
            }
            try
            {

                try
                {
                    if (System.Web.HttpContext.Current.IsDebuggingEnabled)
                    {
                        // this is executed only in the debug version
                        string requestStr = Newtonsoft.Json.JsonConvert.SerializeObject(objIndividualAddress);
                        LogingHelper.SaveRequestJson(requestStr, "Save Individual Address Request");
                    }

                }
                catch (Exception ex)
                {
                    LogingHelper.SaveExceptionInfo(Key, ex, "ProviderInfoSave object serialization", ENTITY.Enumeration.eSeverity.Critical);
                }

                string ValidationResponse = "todo";//IndividualValidations.ValidateIndividualAddressObject(objIndividualAddress);

                if (!string.IsNullOrEmpty(ValidationResponse))
                {
                    objResponse.Message = "Validation Error";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }
                if (objIndividualAddress != null)
                {
                    //  return IndividualAddressCS.SaveIndividualAddress(TokenHelper.GetTokenByKey(Key), objIndividualAddress);
                }
                else
                {
                    objResponse.Message = "Provider Address object cannot be null.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ProviderInfoSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.ProviderAddressCommonResponse = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }


        [AcceptVerbs("GET")]
        [ActionName("ProviderInfoGet")]
        public ProviderAddressCommonResponseRequest IndividualContactBYIndividualId(string Key, int ProviderId)
        {
            ProviderAddressCommonResponseRequest response = new ProviderAddressCommonResponseRequest();

            return response;
        }

        #region CRUD Operations for Address by ProviderId and AddressTypeID

        #endregion



        #endregion

        #region Eligibility

        /// <summary>
        /// Looks up all data by Key For ApprovalAgency.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("GET")]
        [ActionName("ApprovalAgencyGetAll")]
        public ApprovalAgencyResponse ApprovalAgencyGetAll(string Key)
        {

            LogingHelper.SaveAuditInfo(Key);

            ApprovalAgencyResponse objResponse = new ApprovalAgencyResponse();
            ApprovalAgencyBAL objBAL = new ApprovalAgencyBAL();
            ApprovalAgency objEntity = new ApprovalAgency();
            List<ApprovalAgency> lstApprovalAgency = new List<ApprovalAgency>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ApprovalAgency = null;
                    return objResponse;

                }

                lstApprovalAgency = objBAL.Get_All_ApprovalAgency();
                if (lstApprovalAgency != null && lstApprovalAgency.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstApprovalAgencySelected = lstApprovalAgency.Select(menu => new
                    {
                        UserId = menu.UserId,
                        UserName = menu.UserName,
                        UserTypeId = menu.UserTypeId,
                        UserTypeName = menu.UserTypeName,
                        FirstName = menu.FirstName,
                        LastName = menu.LastName,
                        DateOfBirth = menu.DOB,
                        Gender = menu.Gender,
                        Phone = menu.Phone,
                        PositionTitle = menu.PositionTitle,
                        Email = menu.Email,
                        EulaAcceptedOn = menu.EulaAcceptedOn,
                        IndividualId = menu.IndividualId,
                        IndividualName = menu.IndividualName,
                        SourceId = menu.SourceId,
                        SourceName = menu.SourceName,
                        ApprovalAgencytatusId = menu.ApprovalAgencytatusId,
                        ApprovalAgencytatusName = menu.ApprovalAgencytatusName,
                        IsPending = menu.IsPending,
                        IsActive = menu.IsActive
                    }).ToList();

                    objResponse.ApprovalAgency = lstApprovalAgencySelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ApprovalAgency = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ApprovalAgencyGetAll", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ApprovalAgency = null;
            }
            return objResponse;


        }

        /// <summary>
        /// Get Method to get ApprovalAgency by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ID">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("ApprovalAgencyGetBYID")]
        public ApprovalAgencyResponse ApprovalAgencyGetBYID(string Key, int ID)
        {
            LogingHelper.SaveAuditInfo(Key);

            ApprovalAgencyResponse objResponse = new ApprovalAgencyResponse();
            ApprovalAgencyBAL objBAL = new ApprovalAgencyBAL();
            ApprovalAgency objEntity = new ApprovalAgency();
            List<ApprovalAgency> lstApprovalAgency = new List<ApprovalAgency>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ApprovalAgency = null;
                    return objResponse;
                }

                objEntity = objBAL.Get_ApprovalAgency_byApprovalAgencyId(ID);
                if (objEntity != null)
                {
                    lstApprovalAgency.Add(objEntity);

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstApprovalAgencySelected = lstApprovalAgency.Select(menu => new
                    {
                        UserId = menu.UserId,
                        UserName = menu.UserName,
                        UserTypeId = menu.UserTypeId,
                        UserTypeName = menu.UserTypeName,
                        FirstName = menu.FirstName,
                        LastName = menu.LastName,
                        DateOfBirth = menu.DOB,
                        Gender = menu.Gender,
                        Phone = menu.Phone,
                        PositionTitle = menu.PositionTitle,
                        Email = menu.Email,
                        EulaAcceptedOn = menu.EulaAcceptedOn,
                        IndividualId = menu.IndividualId,
                        IndividualName = menu.IndividualName,
                        SourceId = menu.SourceId,
                        SourceName = menu.SourceName,
                        ApprovalAgencytatusId = menu.ApprovalAgencytatusId,
                        ApprovalAgencytatusName = menu.ApprovalAgencytatusName,
                        IsPending = menu.IsPending,
                        IsActive = menu.IsActive
                    }).ToList();

                    objResponse.ApprovalAgency = lstApprovalAgencySelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ApprovalAgency = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ApprovalAgencyGetBYID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.ApprovalAgency = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Method to Delete ApprovalAgency by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ID">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("ApprovalAgencyDeletebyID")]
        public ApprovalAgencyResponse ApprovalAgencyDeletebyID(string Key, int ID)
        {
            LogingHelper.SaveAuditInfo(Key);

            ApprovalAgencyResponse objResponse = new ApprovalAgencyResponse();
            ApprovalAgencyBAL objBAL = new ApprovalAgencyBAL();
            ApprovalAgency objEntity = new ApprovalAgency();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ApprovalAgency = null;
                    return objResponse;
                }

                objEntity = objBAL.Get_ApprovalAgency_byApprovalAgencyId(ID);
                if (objEntity != null)
                {
                    objEntity.IsDeleted = true;
                    objEntity.ModifiedOn = DateTime.Now;
                    objEntity.ModifiedBy = TokenHelper.GetTokenByKey(Key).UserId;
                    objBAL.Save_ApprovalAgency(objEntity);

                    objResponse.Message = Messages.DeleteSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");


                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ApprovalAgency = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ApprovalAgencyDeletebyID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.ApprovalAgency = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Method to Search ApprovalAgency by key and objApprovalAgencySearch.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="objApprovalAgencySearch">Record ID.</param>
        /// <param name="PageNumber">Record ID.</param>
        /// <param name="NoOfRecords">Record ID.</param>
        [AcceptVerbs("POST")]
        [ActionName("ApprovalAgencySearch")]
        public ApprovalAgencySearchResponse ApprovalAgencySearch(string Key, ApprovalAgencySearch objApprovalAgencySearch, int PageNumber, int NoOfRecords)
        {
            LogingHelper.SaveAuditInfo(Key);

            ApprovalAgencySearchResponse objResponse = new ApprovalAgencySearchResponse();
            ApprovalAgencyBAL objBAL = new ApprovalAgencyBAL();
            ApprovalAgency objEntity = new ApprovalAgency();
            List<ApprovalAgencySearch> lstApprovalAgencySearch = new List<ApprovalAgencySearch>();
            List<ApprovalAgency> lstApprovalAgency = new List<ApprovalAgency>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ApprovalAgency = null;
                    return objResponse;
                }

                lstApprovalAgency = objBAL.Search_ApprovalAgency_WithPager(objApprovalAgencySearch, PageNumber, NoOfRecords);
                if (lstApprovalAgency != null && lstApprovalAgency.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstApprovalAgencySelected = lstApprovalAgency.Select(menu => new
                    {
                        UserName = menu.UserName,
                        UserTypeId = menu.UserTypeId,
                        UserTypeName = menu.UserTypeName,
                        FirstName = menu.FirstName,
                        LastName = menu.LastName,
                        DateOfBirth = menu.DOB,
                        Gender = menu.Gender,
                        Phone = menu.Phone,
                        PositionTitle = menu.PositionTitle,
                        Email = menu.Email,
                        SourceId = menu.SourceId,
                        SourceName = menu.SourceName,
                        ApprovalAgencytatusId = menu.ApprovalAgencytatusId,
                        ApprovalAgencytatusName = menu.ApprovalAgencytatusName,

                        IsPending = menu.IsPending,
                    }).ToList();
                    objResponse.Total_Recard = lstApprovalAgency[0].Total_Recard;
                    objResponse.ApprovalAgency = lstApprovalAgencySelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ApprovalAgency = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ApprovalAgencySearch", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.ApprovalAgency = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Save or Update the data For ApprovalAgency
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objApprovalAgency">Object of ApprovalAgency</param>
        [AcceptVerbs("POST")]
        [ActionName("ApprovalAgencySave")]
        public ApprovalAgencyPostResponse ApprovalAgencySave(string Key, ApprovalAgencyRequest objApprovalAgency)
        {


            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo(Key);

            ApprovalAgencyPostResponse objResponse = new ApprovalAgencyPostResponse();
            ApprovalAgencyBAL objBAL = new ApprovalAgencyBAL();
            ApprovalAgency objEntity = new ApprovalAgency();
            List<ApprovalAgencyRequest> lstEntity = new List<ApprovalAgencyRequest>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.ApprovalAgency = null;
                return objResponse;
            }

            string ValidationResponse = ApprovalAgencyValidation.ValidateApprovalAgencyObject(objApprovalAgency);

            if (!string.IsNullOrEmpty(ValidationResponse))
            {


                objResponse.Message = "Validation Error";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                objResponse.ResponseReason = ValidationResponse;
                return objResponse;

            }

            try
            {
                Individual objIndividual = new Individual();
                IndividualBAL objIndividualBAL = new IndividualBAL();

                if (objApprovalAgency.UserId > 0)
                {
                    objEntity = objBAL.Get_ApprovalAgency_byApprovalAgencyId(objApprovalAgency.UserId);
                    if (objEntity != null)
                    {
                        objEntity.FirstName = objApprovalAgency.FirstName;
                        objEntity.UserName = objApprovalAgency.UserName;
                        objEntity.LastName = objApprovalAgency.LastName;
                        objEntity.UserTypeId = objApprovalAgency.UserTypeId;
                        objEntity.ApprovalAgencytatusId = objApprovalAgency.ApprovalAgencytatusId;
                        objEntity.IsPending = objApprovalAgency.IsPending;
                        objEntity.SourceId = objApprovalAgency.SourceId;
                        objEntity.Phone = objApprovalAgency.Phone;
                        objEntity.PositionTitle = objApprovalAgency.PositionTitle;

                        objEntity.Gender = objApprovalAgency.Gender;
                        objEntity.DateOfBirth = objApprovalAgency.DateOfBirth;

                        objEntity.Email = objApprovalAgency.Email;

                        if (!string.IsNullOrEmpty(objApprovalAgency.Phone))
                        { objEntity.Phone = objApprovalAgency.Phone; }

                        #region User Role Save
                        List<UserRolesRequest> lstRoles = new List<UserRolesRequest>();
                        lstRoles = objApprovalAgency.UserRoles;

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
                                        objRole.ModifiedBy = CreatedOrMoifiy;
                                        objRole.ModifiedOn = DateTime.Now;
                                        objRoleBAL.Save_UserRole(objRole);
                                    }
                                }
                                else
                                {
                                    objRole = new UserRole();
                                    objRole.RoleId = objRoleReq.RoleId;
                                    objRole.UserId = objApprovalAgency.UserId;
                                    objRole.IsActive = objRoleReq.Selected;
                                    objRole.IsGrantable = objRoleReq.Grantable;
                                    objRole.ModifiedBy = 0;
                                    objRole.ModifiedOn = DateTime.Now;
                                    objRole.CreatedBy = CreatedOrMoifiy;
                                    objRole.CreatedOn = DateTime.Now;
                                    objRole.IsDeleted = false;
                                    objRoleBAL.Save_UserRole(objRole);
                                }

                            }
                        }
                        #endregion


                        objEntity.Gender = objApprovalAgency.Gender;
                        objEntity.DateOfBirth = objApprovalAgency.DateOfBirth;

                        objEntity.ModifiedOn = DateTime.Now;
                        objEntity.ModifiedBy = CreatedOrMoifiy;
                        objBAL.Individual_User_Save(objEntity);
                        objResponse.Message = Messages.UpdateSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }

                }
                else
                {
                    string TempPassword = GeneralFunctions.GetTempPassword();
                    objEntity = new ApprovalAgency();

                    objEntity.FirstName = objApprovalAgency.FirstName;
                    objEntity.UserName = objApprovalAgency.UserName;
                    objEntity.LastName = objApprovalAgency.LastName;
                    objEntity.UserTypeId = objApprovalAgency.UserTypeId;
                    objEntity.ApprovalAgencytatusId = objApprovalAgency.ApprovalAgencytatusId;
                    objEntity.IsPending = objApprovalAgency.IsPending;
                    objEntity.SourceId = objApprovalAgency.SourceId;
                    objEntity.Email = objApprovalAgency.Email;

                    objEntity.Gender = objApprovalAgency.Gender;
                    objEntity.DateOfBirth = objApprovalAgency.DateOfBirth;


                    objEntity.PositionTitle = objApprovalAgency.PositionTitle;

                    if (!string.IsNullOrEmpty(objApprovalAgency.Phone))
                    { objEntity.Phone = objApprovalAgency.Phone; }

                    objEntity.PasswordHash = TempPassword;// "123456";
                    objEntity.TemporaryPassword = true;
                    objEntity.FailedLogins = 0;
                    // objApprovalAgency.SourceId = 0;

                    objEntity.IsActive = true;
                    objEntity.IsDeleted = false;

                    //   objEntity.PasswordChangedOn = DateTime.Now;
                    objEntity.CreatedBy = 1;
                    objEntity.UserGuid = Guid.NewGuid().ToString();
                    objEntity.IndividualGuid = Guid.NewGuid().ToString();
                    objEntity.Authenticator = Guid.NewGuid().ToString();
                    objEntity.CreatedOn = DateTime.Now;
                    objEntity.CreatedBy = CreatedOrMoifiy;
                    //objApprovalAgency.ModifiedOn = null;
                    objEntity.UserId = objBAL.Individual_User_Save(objEntity);



                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    objApprovalAgency.UserId = objEntity.UserId;

                    #region User Role Save
                    List<UserRolesRequest> lstRoles = new List<UserRolesRequest>();
                    lstRoles = objApprovalAgency.UserRoles;

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
                                objRole.UserId = objApprovalAgency.UserId;
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

                    #region Send Email of user creation

                    if (EmailHelper.SendMail(objApprovalAgency.Email, "Temporary Password", "Email: " + objApprovalAgency.Email + " <br/> Temporary Password: " + TempPassword, true))
                    {
                        LogHelper.LogCommunication(objIndividual.IndividualId, null, eCommunicationType.Email, "Temporary Password", eCommunicationStatus.Success, (eCommentLogSource.WSAPI).ToString(), "Temporary Password email has been sent", EmailHelper.GetSenderAddress(), objApprovalAgency.Email, null, null, objApprovalAgency.UserId, null, null, null);
                    }
                    else
                    {
                        LogHelper.LogCommunication(objIndividual.IndividualId, null, eCommunicationType.Email, "Temporary Password", eCommunicationStatus.Fail, (eCommentLogSource.WSAPI).ToString(), "Temporary Password email sending failed", EmailHelper.GetSenderAddress(), objApprovalAgency.Email, null, null, objApprovalAgency.UserId, null, null, null);
                    }

                    #endregion

                }

                lstEntity.Add(objApprovalAgency);
                objResponse.Status = true;

                objResponse.ApprovalAgency = lstEntity;

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ApprovalAgencySave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.ApprovalAgency = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
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

    }
}