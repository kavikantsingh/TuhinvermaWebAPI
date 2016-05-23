using LAPP.BAL;
using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.LOGING;
using LAPP.WS.App_Helper;
using LAPP.WS.App_Helper.Common;
using LAPP.WS.ValidateController.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LAPP.WS.Controllers.User
{
    public class UsersController : ApiController
    {
        #region Users

        /// <summary>
        /// Looks up all data by Key For Users.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("GET")]
        [ActionName("UsersGetAll")]
        public UsersResponse UsersGetAll(string Key)
        {

            LogingHelper.SaveAuditInfo(Key);

            UsersResponse objResponse = new UsersResponse();
            UsersBAL objBAL = new UsersBAL();
            Users objEntity = new Users();
            List<Users> lstUsers = new List<Users>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.Users = null;
                    return objResponse;

                }

                lstUsers = objBAL.Get_All_Users();
                if (lstUsers != null && lstUsers.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstUsersSelected = lstUsers.Select(menu => new
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
                        UserStatusId = menu.UserStatusId,
                        UserStatusName = menu.UserStatusName,
                        IsPending = menu.IsPending,
                        IsActive = menu.IsActive
                    }).ToList();

                    objResponse.Users = lstUsersSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Users = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UsersGetAll", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.Users = null;
            }
            return objResponse;


        }

        /// <summary>
        /// Get Method to get Users by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ID">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("UsersGetBYID")]
        public UsersResponse UsersGetBYID(string Key, int ID)
        {
            LogingHelper.SaveAuditInfo(Key);

            UsersResponse objResponse = new UsersResponse();
            UsersBAL objBAL = new UsersBAL();
            Users objEntity = new Users();
            List<Users> lstUsers = new List<Users>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.Users = null;
                    return objResponse;
                }

                objEntity = objBAL.Get_Users_byUsersId(ID);
                if (objEntity != null)
                {
                    lstUsers.Add(objEntity);

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstUsersSelected = lstUsers.Select(menu => new
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
                        UserStatusId = menu.UserStatusId,
                        UserStatusName = menu.UserStatusName,
                        IsPending = menu.IsPending,
                        IsActive = menu.IsActive
                    }).ToList();

                    objResponse.Users = lstUsersSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Users = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UsersGetBYID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Users = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Method to Delete Users by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ID">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("UsersDeletebyID")]
        public UsersResponse UsersDeletebyID(string Key, int ID)
        {
            LogingHelper.SaveAuditInfo(Key);

            UsersResponse objResponse = new UsersResponse();
            UsersBAL objBAL = new UsersBAL();
            Users objEntity = new Users();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.Users = null;
                    return objResponse;
                }

                objEntity = objBAL.Get_Users_byUsersId(ID);
                if (objEntity != null)
                {
                    objEntity.IsDeleted = true;
                    objEntity.ModifiedOn = DateTime.Now;
                    objEntity.ModifiedBy = TokenHelper.GetTokenByKey(Key).UserId;
                    objBAL.Save_Users(objEntity);

                    objResponse.Message = Messages.DeleteSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");


                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Users = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UsersDeletebyID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Users = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Method to Search Users by key and objUsersSearch.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="objUsersSearch">Record ID.</param>
        [AcceptVerbs("POST")]
        [ActionName("UsersSearch")]
        public UsersSearchResponse UsersSearch(string Key, UsersSearch objUsersSearch)
        {
            LogingHelper.SaveAuditInfo(Key);

            UsersSearchResponse objResponse = new UsersSearchResponse();
            UsersBAL objBAL = new UsersBAL();
            Users objEntity = new Users();
            List<UsersSearch> lstUsers = new List<UsersSearch>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.Users = null;
                    return objResponse;
                }

                lstUsers = objBAL.Search_Users(objUsersSearch);
                if (lstUsers != null && lstUsers.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstUsersSelected = lstUsers.Select(menu => new
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
                        UserStatusId = menu.UserStatusId,
                        UserStatusName = menu.UserStatusName,

                        IsPending = menu.IsPending,
                    }).ToList();

                    objResponse.Users = lstUsersSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Users = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UsersSearch", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Users = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Save or Update the data For Users
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objUsers">Object of Users</param>
        [AcceptVerbs("POST")]
        [ActionName("UsersSave")]
        public UsersPostResponse UsersSave(string Key, UsersRequest objUsers)
        {


            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo(Key);

            UsersPostResponse objResponse = new UsersPostResponse();
            UsersBAL objBAL = new UsersBAL();
            Users objEntity = new Users();
            List<UsersRequest> lstEntity = new List<UsersRequest>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.Users = null;
                return objResponse;
            }

            string ValidationResponse = UsersValidation.ValidateUsersObject(objUsers);

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

                if (objUsers.UserId > 0)
                {
                    objEntity = objBAL.Get_Users_byUsersId(objUsers.UserId);
                    if (objEntity != null)
                    {
                        objEntity.FirstName = objUsers.FirstName;
                        objEntity.UserName = objUsers.UserName;
                        objEntity.LastName = objUsers.LastName;
                        objEntity.UserTypeId = objUsers.UserTypeId;
                        objEntity.UserStatusId = objUsers.UserStatusId;
                        objEntity.IsPending = objUsers.IsPending;
                        objEntity.SourceId = objUsers.SourceId;
                        objEntity.Phone = objUsers.Phone;
                        objEntity.PositionTitle = objUsers.PositionTitle;

                        objEntity.Gender = objUsers.Gender;
                        objEntity.DateOfBirth = objUsers.DateOfBirth;

                        objEntity.Email = objUsers.Email;

                        if (!string.IsNullOrEmpty(objUsers.Phone))
                        { objEntity.Phone = objUsers.Phone; }

                        #region User Role Save
                        List<UserRolesRequest> lstRoles = new List<UserRolesRequest>();
                        lstRoles = objUsers.UserRoles;

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
                                    objRole.UserId = objUsers.UserId;
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


                        objEntity.Gender = objUsers.Gender;
                        objEntity.DateOfBirth = objUsers.DateOfBirth;

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
                    objEntity = new Users();

                    objEntity.FirstName = objUsers.FirstName;
                    objEntity.UserName = objUsers.UserName;
                    objEntity.LastName = objUsers.LastName;
                    objEntity.UserTypeId = objUsers.UserTypeId;
                    objEntity.UserStatusId = objUsers.UserStatusId;
                    objEntity.IsPending = objUsers.IsPending;
                    objEntity.SourceId = objUsers.SourceId;
                    objEntity.Email = objUsers.Email;

                    objEntity.Gender = objUsers.Gender;
                    objEntity.DateOfBirth = objUsers.DateOfBirth;


                    objEntity.PositionTitle = objUsers.PositionTitle;

                    if (!string.IsNullOrEmpty(objUsers.Phone))
                    { objEntity.Phone = objUsers.Phone; }

                    objEntity.PasswordHash = "123456";
                    objEntity.FailedLogins = 0;
                    // objUsers.SourceId = 0;

                    objEntity.IsActive = true;
                    objEntity.IsDeleted = false;

                    //   objEntity.PasswordChangedOn = DateTime.Now;
                    objEntity.CreatedBy = 1;
                    objEntity.UserGuid = Guid.NewGuid().ToString();
                    objEntity.IndividualGuid = Guid.NewGuid().ToString();
                    objEntity.Authenticator = Guid.NewGuid().ToString();
                    objEntity.CreatedOn = DateTime.Now;
                    objEntity.CreatedBy = CreatedOrMoifiy;
                    //objUsers.ModifiedOn = null;
                    objEntity.UserId = objBAL.Individual_User_Save(objEntity);



                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    objUsers.UserId = objEntity.UserId;

                    #region User Role Save
                    List<UserRolesRequest> lstRoles = new List<UserRolesRequest>();
                    lstRoles = objUsers.UserRoles;

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
                                objRole.UserId = objUsers.UserId;
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

                }

                lstEntity.Add(objUsers);
                objResponse.Status = true;

                objResponse.Users = lstEntity;

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UsersSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.Users = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }


        #endregion

        #region UserType

        /// <summary>
        /// Looks up all data by Key For UserType.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("GET")]
        [ActionName("UserTypeGetAll")]
        public UserTypeResponse UserTypeGetAll(string Key)
        {

            LogingHelper.SaveAuditInfo(Key);

            UserTypeResponse objResponse = new UserTypeResponse();
            UserTypeBAL objBAL = new UserTypeBAL();
            UserType objEntity = new UserType();
            List<UserType> lstBoard = new List<UserType>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.UserType = null;
                    return objResponse;

                }

                lstBoard = objBAL.Get_All_UserType();
                if (lstBoard != null && lstBoard.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserType = lstBoard;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserType = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UserTypeGetAll", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.UserType = null;

            }
            return objResponse;


        }


        /// <summary>
        /// Get Method to get UserType by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ID">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("UserTypeGetBYID")]
        public UserTypeResponse UserTypeGetBYID(string Key, int ID)
        {
            LogingHelper.SaveAuditInfo(Key);

            UserTypeResponse objResponse = new UserTypeResponse();
            UserTypeBAL objBAL = new UserTypeBAL();
            UserType objEntity = new UserType();
            List<UserType> lstBoard = new List<UserType>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.UserType = null;
                    return objResponse;
                }

                objEntity = objBAL.Get_UserType_byUserTypeId(ID);
                if (objEntity != null)
                {
                    lstBoard.Add(objEntity);

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserType = lstBoard;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserType = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UserTypeGetBYID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.UserType = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Save or Update the data For UserType
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objUserType">Object of UserType</param>
        [AcceptVerbs("POST")]
        [ActionName("UserTypeSave")]
        public UserTypeResponse UserTypeSave(string Key, UserType objUserType)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo(Key);

            UserTypeResponse objResponse = new UserTypeResponse();
            UserTypeBAL objBAL = new UserTypeBAL();
            UserType objEntity = new UserType();
            List<UserType> lstEntity = new List<UserType>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.UserType = null;
                return objResponse;
            }

            string ValidationResponse = UsersValidation.ValidateUserTypeObject(objUserType);

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
                if (objUserType.UserTypeId > 0)
                {
                    objEntity = objBAL.Get_UserType_byUserTypeId(objUserType.UserTypeId);
                    if (objEntity != null)
                    {
                        objUserType.ModifiedOn = DateTime.Now;
                        objUserType.ModifiedBy = CreatedOrMoifiy;
                        objBAL.Save_UserType(objUserType);
                        objResponse.Message = Messages.UpdateSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }
                    else
                    {
                        objUserType.CreatedOn = DateTime.Now;
                        //objUserType.ModifiedOn = null;
                        objUserType.UserTypeId = objBAL.Save_UserType(objUserType);
                        objResponse.Message = Messages.SaveSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    }
                }
                else
                {
                    objUserType.CreatedOn = DateTime.Now;
                    objUserType.CreatedBy = CreatedOrMoifiy;

                    //objUserType.ModifiedOn = null;
                    objUserType.UserTypeId = objBAL.Save_UserType(objUserType);
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                }

                lstEntity.Add(objUserType);
                objResponse.Status = true;

                objResponse.UserType = lstEntity;

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UserTypeSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.UserType = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }


        #endregion

        #region UserStatus

        /// <summary>
        /// Looks up all data by Key For UserStatus.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("GET")]
        [ActionName("UserStatusGetAll")]
        public UserStatusResponse UserStatusGetAll(string Key)
        {

            LogingHelper.SaveAuditInfo(Key);

            UserStatusResponse objResponse = new UserStatusResponse();
            UserStatusBAL objBAL = new UserStatusBAL();
            UserStatus objEntity = new UserStatus();
            List<UserStatus> lstBoard = new List<UserStatus>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.UserStatus = null;
                    return objResponse;

                }

                lstBoard = objBAL.Get_All_UserStatus();
                if (lstBoard != null && lstBoard.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserStatus = lstBoard;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserStatus = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UserStatusGetAll", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.UserStatus = null;

            }
            return objResponse;


        }


        /// <summary>
        /// Get Method to get UserStatus by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ID">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("UserStatusGetBYID")]
        public UserStatusResponse UserStatusGetBYID(string Key, int ID)
        {
            LogingHelper.SaveAuditInfo(Key);

            UserStatusResponse objResponse = new UserStatusResponse();
            UserStatusBAL objBAL = new UserStatusBAL();
            UserStatus objEntity = new UserStatus();
            List<UserStatus> lstBoard = new List<UserStatus>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.UserStatus = null;
                    return objResponse;
                }

                objEntity = objBAL.Get_UserStatus_byUserStatusId(ID);
                if (objEntity != null)
                {
                    lstBoard.Add(objEntity);

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserStatus = lstBoard;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserStatus = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UserStatusGetBYID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.UserStatus = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Save or Update the data For UserStatus
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objUserStatus">Object of UserStatus</param>
        [AcceptVerbs("POST")]
        [ActionName("UserStatusSave")]
        public UserStatusResponse UserStatusSave(string Key, UserStatus objUserStatus)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo(Key);

            UserStatusResponse objResponse = new UserStatusResponse();
            UserStatusBAL objBAL = new UserStatusBAL();
            UserStatus objEntity = new UserStatus();
            List<UserStatus> lstEntity = new List<UserStatus>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.UserStatus = null;
                return objResponse;
            }

            string ValidationResponse = UsersValidation.ValidateUserStatusObject(objUserStatus);

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
                if (objUserStatus.UserStatusId > 0)
                {
                    objEntity = objBAL.Get_UserStatus_byUserStatusId(objUserStatus.UserStatusId);
                    if (objEntity != null)
                    {
                        objUserStatus.ModifiedOn = DateTime.Now;
                        objUserStatus.ModifiedBy = CreatedOrMoifiy;
                        objBAL.Save_UserStatus(objUserStatus);
                        objResponse.Message = Messages.UpdateSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }

                    else
                    {
                        objUserStatus.CreatedOn = DateTime.Now;
                        //objUserStatus.ModifiedOn = null;
                        objUserStatus.CreatedBy = CreatedOrMoifiy;

                        objUserStatus.UserStatusId = objBAL.Save_UserStatus(objUserStatus);
                        objResponse.Message = Messages.SaveSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    }
                }
                else
                {
                    objUserStatus.CreatedOn = DateTime.Now;
                    //objUserStatus.ModifiedOn = null;
                    objUserStatus.UserStatusId = objBAL.Save_UserStatus(objUserStatus);
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                }

                lstEntity.Add(objUserStatus);
                objResponse.Status = true;

                objResponse.UserStatus = lstEntity;

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UserStatusSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.UserStatus = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }


        #endregion

        #region UserSecurityQuestion

        /// <summary>
        /// Looks up all data by Key For UserSecurityQuestion.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("GET")]
        [ActionName("UserSecurityQuestionGetAll")]
        public UserSecurityQuestionResponse UserSecurityQuestionGetAll(string Key)
        {

            LogingHelper.SaveAuditInfo(Key);

            UserSecurityQuestionResponse objResponse = new UserSecurityQuestionResponse();
            UserSecurityQuestionBAL objBAL = new UserSecurityQuestionBAL();
            UserSecurityQuestion objEntity = new UserSecurityQuestion();
            List<UserSecurityQuestion> lstBoard = new List<UserSecurityQuestion>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.UserSecurityQuestion = null;
                    return objResponse;

                }

                lstBoard = objBAL.Get_All_UserSecurityQuestion();
                if (lstBoard != null && lstBoard.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserSecurityQuestion = lstBoard;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserSecurityQuestion = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UserSecurityQuestionGetAll", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.UserSecurityQuestion = null;

            }
            return objResponse;


        }


        /// <summary>
        /// Get Method to get UserSecurityQuestion by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ID">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("UserSecurityQuestionGetBYID")]
        public UserSecurityQuestionResponse UserSecurityQuestionGetBYID(string Key, int ID)
        {
            LogingHelper.SaveAuditInfo(Key);

            UserSecurityQuestionResponse objResponse = new UserSecurityQuestionResponse();
            UserSecurityQuestionBAL objBAL = new UserSecurityQuestionBAL();
            UserSecurityQuestion objEntity = new UserSecurityQuestion();
            List<UserSecurityQuestion> lstBoard = new List<UserSecurityQuestion>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.UserSecurityQuestion = null;
                    return objResponse;
                }

                objEntity = objBAL.Get_UserSecurityQuestion_byUserSecurityQuestionId(ID);
                if (objEntity != null)
                {
                    lstBoard.Add(objEntity);

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserSecurityQuestion = lstBoard;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserSecurityQuestion = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UserSecurityQuestionGetBYID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.UserSecurityQuestion = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Get Method to get UserSecurityQuestion by key and UserID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="UserId">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("UserSecurityQuestionGetBYUserID")]
        public UserSecurityQuestionResponse UserSecurityQuestionGetBYUserID(string Key, int UserId)
        {
            LogingHelper.SaveAuditInfo(Key);

            UserSecurityQuestionResponse objResponse = new UserSecurityQuestionResponse();
            UserSecurityQuestionBAL objBAL = new UserSecurityQuestionBAL();
            UserSecurityQuestion objEntity = new UserSecurityQuestion();
            List<UserSecurityQuestion> lstBoard = new List<UserSecurityQuestion>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.UserSecurityQuestion = null;
                    return objResponse;
                }

                objEntity = objBAL.Get_UserSecurityQuestion_byUserId(UserId);
                if (objEntity != null)
                {
                    lstBoard.Add(objEntity);

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserSecurityQuestion = lstBoard;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserSecurityQuestion = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UserSecurityQuestionGetBYID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.UserSecurityQuestion = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Save or Update the data For UserSecurityQuestion
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objUserSecurityQuestion">Object of UserSecurityQuestion</param>
        [AcceptVerbs("POST")]
        [ActionName("UserSecurityQuestionSave")]
        public UserSecurityQuestionResponse UserSecurityQuestionSave(string Key, UserSecurityQuestion objUserSecurityQuestion)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;
            LogingHelper.SaveAuditInfo(Key);

            UserSecurityQuestionResponse objResponse = new UserSecurityQuestionResponse();
            UserSecurityQuestionBAL objBAL = new UserSecurityQuestionBAL();
            UserSecurityQuestion objEntity = new UserSecurityQuestion();
            List<UserSecurityQuestion> lstEntity = new List<UserSecurityQuestion>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.UserSecurityQuestion = null;
                return objResponse;
            }

            string ValidationResponse = UsersValidation.ValidateUserSecurityQuestionObject(objUserSecurityQuestion);

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
                if (objUserSecurityQuestion.UserSecurityQuestionId > 0)
                {
                    objEntity = objBAL.Get_UserSecurityQuestion_byUserSecurityQuestionId(objUserSecurityQuestion.UserSecurityQuestionId);
                    if (objEntity != null)
                    {
                        objUserSecurityQuestion.ModifiedOn = DateTime.Now;
                        objUserSecurityQuestion.ModifiedBy = CreatedOrMoifiy;

                        objBAL.Save_UserSecurityQuestion(objUserSecurityQuestion);
                        objResponse.Message = Messages.UpdateSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }
                }
                else
                {

                    objUserSecurityQuestion.CreatedOn = DateTime.Now;
                    objUserSecurityQuestion.CreatedBy = CreatedOrMoifiy;

                    //objUserSecurityQuestion.ModifiedOn = null;
                    objUserSecurityQuestion.UserSecurityQuestionId = objBAL.Save_UserSecurityQuestion(objUserSecurityQuestion);
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                }

                lstEntity.Add(objUserSecurityQuestion);
                objResponse.Status = true;

                objResponse.UserSecurityQuestion = lstEntity;

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UserSecurityQuestionSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.UserSecurityQuestion = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }


        #endregion

        #region UserRole

        /// <summary>
        /// Looks up all data by Key For UserRole.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        //[AcceptVerbs("GET", "POST")]
        [ActionName("UserRoleGetAll")]
        public UserRoleResponse UserRoleGetAll(string Key)
        {

            LogingHelper.SaveAuditInfo(Key);

            UserRoleResponse objResponse = new UserRoleResponse();
            UserRoleBAL objBAL = new UserRoleBAL();
            UserRole objEntity = new UserRole();
            List<UserRole> lstBoard = new List<UserRole>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.UserRole = null;
                    return objResponse;

                }

                lstBoard = objBAL.Get_All_UserRole();
                if (lstBoard != null && lstBoard.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserRole = lstBoard;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserRole = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UserRoleGetAll", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.UserRole = null;

            }
            return objResponse;


        }


        /// <summary>
        /// Get Method to get UserRole by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ID">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("UserRoleGetBYID")]
        public UserRoleResponse UserRoleGetBYID(string Key, int ID)
        {
            LogingHelper.SaveAuditInfo(Key);

            UserRoleResponse objResponse = new UserRoleResponse();
            UserRoleBAL objBAL = new UserRoleBAL();
            UserRole objEntity = new UserRole();
            List<UserRole> lstBoard = new List<UserRole>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.UserRole = null;
                    return objResponse;
                }

                objEntity = objBAL.Get_UserRole_byUserRoleId(ID);
                if (objEntity != null)
                {
                    lstBoard.Add(objEntity);

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserRole = lstBoard;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserRole = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UserRoleGetBYID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.UserRole = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Get Method to get UserRole by key and UserId.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="UserId">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("UserRoleGetBYUserID")]
        public UserRoleResponse UserRoleGetBYUserID(string Key, int UserId)
        {
            LogingHelper.SaveAuditInfo(Key);

            UserRoleResponse objResponse = new UserRoleResponse();
            UserRoleBAL objBAL = new UserRoleBAL();
            // UserRole objEntity = new UserRole();
            List<UserRole> lstUserRoles = new List<UserRole>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.UserRole = null;
                    return objResponse;
                }

                lstUserRoles = objBAL.Get_UserRole_by_UserId(UserId);
                if (lstUserRoles != null)
                {
                    // lstUserRoles.Add(objEntity);

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var roles = lstUserRoles.Select(role => new { UserRoleID = role.UserRoleId, RoleID = role.RoleId, Role = role.RoleName, Selected = role.IsActive, Grantable = role.IsGrantable }).ToList();

                    objResponse.UserRole = roles;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserRole = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UserRoleGetBYID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.UserRole = null;

            }
            return objResponse;
        }



        /// <summary>
        /// Get Method to get UserRole by key and RoleId.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ID">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("UserRoleGetBYRoleId")]
        public UserRoleResponse UserRoleGetBYRoleId(string Key, int RoleId)
        {
            LogingHelper.SaveAuditInfo(Key);

            UserRoleResponse objResponse = new UserRoleResponse();
            UserRoleBAL objBAL = new UserRoleBAL();
            UserRole objEntity = new UserRole();
            List<UserRole> lstBoard = new List<UserRole>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.UserRole = null;
                    return objResponse;
                }

                objEntity = objBAL.Get_UserRole_byRoleId(RoleId);
                if (objEntity != null)
                {
                    lstBoard.Add(objEntity);

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserRole = lstBoard;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserRole = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UserRoleGetBYID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.UserRole = null;

            }
            return objResponse;
        }



        /// <summary>
        /// Save or Update the data For UserRole
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objUserRole">Object of UserRole</param>
        [AcceptVerbs("POST")]
        [ActionName("UserRoleSave")]
        public UserRoleResponse UserRoleSave(string Key, List<UserRolesRequest> objUserRole)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;
            LogingHelper.SaveAuditInfo(Key);

            UserRoleResponse objResponse = new UserRoleResponse();
            UserRoleBAL objBAL = new UserRoleBAL();
            UserRole objEntity = new UserRole();
            List<UserRole> lstEntity = new List<UserRole>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.UserRole = null;
                return objResponse;
            }

            //string ValidationResponse = UsersValidation.ValidateUserRoleObject(objUserRole);

            //if (!string.IsNullOrEmpty(ValidationResponse))
            //{


            //    objResponse.Message = "Validation Error";
            //    objResponse.Status = false;
            //    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
            //    objResponse.ResponseReason = ValidationResponse;
            //    return objResponse;

            //}

            try
            {
                #region User Role Save
                List<UserRolesRequest> lstRoles = new List<UserRolesRequest>();
                lstRoles = objUserRole;

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
                            objRole.UserId = objRoleReq.UserID;
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

                //if (objUserRole.UserRoleId > 0)
                //{
                //    objEntity = objBAL.Get_UserRole_byUserRoleId(objUserRole.UserRoleId);
                //    if (objEntity != null)
                //    {
                //        objUserRole.ModifiedOn = DateTime.Now;

                //        objBAL.Update_UserRole(objUserRole);
                //        objResponse.Message = Messages.UpdateSuccess;
                //        objResponse.Status = true;
                //        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                //    }
                //}
                //else
                //{
                //    objUserRole.CreatedOn = DateTime.Now;
                //    //objUserRole.ModifiedOn = null;
                //    objUserRole.UserRoleId = objBAL.Save_UserRole(objUserRole);
                //    objResponse.Message = Messages.SaveSuccess;
                //    objResponse.Status = true;
                //    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                //}

                objResponse.Message = Messages.SaveSuccess;
                objResponse.Status = true;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                objResponse.Status = true;

                objResponse.UserRole = lstRoles;

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UserRoleSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.UserRole = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }


        #endregion

        #region UserDivision

        /// <summary>
        /// Looks up all data by Key For UserDivision.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("GET")]
        [ActionName("UserDivisionGetAll")]
        public UserDivisionResponse UserDivisionGetAll(string Key)
        {

            LogingHelper.SaveAuditInfo(Key);

            UserDivisionResponse objResponse = new UserDivisionResponse();
            UserDivisionBAL objBAL = new UserDivisionBAL();
            UserDivision objEntity = new UserDivision();
            List<UserDivision> lstBoard = new List<UserDivision>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.UserDivision = null;
                    return objResponse;

                }

                lstBoard = objBAL.Get_All_UserDivision();
                if (lstBoard != null && lstBoard.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserDivision = lstBoard;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserDivision = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UserDivisionGetAll", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.UserDivision = null;

            }
            return objResponse;


        }


        /// <summary>
        /// Get Method to get UserDivision by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ID">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("UserDivisionGetBYID")]
        public UserDivisionResponse UserDivisionGetBYID(string Key, int ID)
        {
            LogingHelper.SaveAuditInfo(Key);

            UserDivisionResponse objResponse = new UserDivisionResponse();
            UserDivisionBAL objBAL = new UserDivisionBAL();
            UserDivision objEntity = new UserDivision();
            List<UserDivision> lstBoard = new List<UserDivision>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.UserDivision = null;
                    return objResponse;
                }

                objEntity = objBAL.Get_UserDivision_byUserDivisionId(ID);
                if (objEntity != null)
                {
                    lstBoard.Add(objEntity);

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserDivision = lstBoard;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserDivision = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UserDivisionGetBYID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.UserDivision = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Get Method to get UserDivision by key and UserId.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="UserId">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("UserDivisionGetBYUserID")]
        public UserDivisionResponse UserDivisionGetBYUserID(string Key, int UserId)
        {
            LogingHelper.SaveAuditInfo(Key);

            UserDivisionResponse objResponse = new UserDivisionResponse();
            UserDivisionBAL objBAL = new UserDivisionBAL();
            UserDivision objEntity = new UserDivision();
            List<UserDivision> lstBoard = new List<UserDivision>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.UserDivision = null;
                    return objResponse;
                }

                objEntity = objBAL.Get_UserDivision_by_UserId(UserId);
                if (objEntity != null)
                {
                    lstBoard.Add(objEntity);

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserDivision = lstBoard;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserDivision = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UserDivisionGetBYID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.UserDivision = null;

            }
            return objResponse;
        }



        /// <summary>
        /// Get Method to get UserDivision by key and DivisionId.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="DivisionId">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("UserDivisionGetBYDivisionId")]
        public UserDivisionResponse UserDivisionGetBYDivisionId(string Key, int DivisionId)
        {
            LogingHelper.SaveAuditInfo(Key);

            UserDivisionResponse objResponse = new UserDivisionResponse();
            UserDivisionBAL objBAL = new UserDivisionBAL();
            UserDivision objEntity = new UserDivision();
            List<UserDivision> lstBoard = new List<UserDivision>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.UserDivision = null;
                    return objResponse;
                }

                objEntity = objBAL.Get_UserDivision_by_DivisionId(DivisionId);
                if (objEntity != null)
                {
                    lstBoard.Add(objEntity);

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserDivision = lstBoard;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserDivision = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UserDivisionGetBYID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.UserDivision = null;

            }
            return objResponse;
        }



        /// <summary>
        /// Save or Update the data For UserDivision
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objUserDivision">Object of UserDivision</param>
        [AcceptVerbs("POST")]
        [ActionName("UserDivisionSave")]
        public UserDivisionResponse UserDivisionSave(string Key, UserDivision objUserDivision)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;
            LogingHelper.SaveAuditInfo(Key);

            UserDivisionResponse objResponse = new UserDivisionResponse();
            UserDivisionBAL objBAL = new UserDivisionBAL();
            UserDivision objEntity = new UserDivision();
            List<UserDivision> lstEntity = new List<UserDivision>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.UserDivision = null;
                return objResponse;
            }

            string ValidationResponse = UsersValidation.ValidateUserDivisionObject(objUserDivision);

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
                if (objUserDivision.UserDivisionId > 0)
                {
                    objEntity = objBAL.Get_UserDivision_byUserDivisionId(objUserDivision.UserDivisionId);
                    if (objEntity != null)
                    {
                        objUserDivision.ModifiedOn = DateTime.Now;

                        objUserDivision.ModifiedBy = CreatedOrMoifiy;
                        objBAL.Save_UserDivision(objUserDivision);
                        objResponse.Message = Messages.UpdateSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }
                }
                else
                {

                    objUserDivision.CreatedOn = DateTime.Now;
                    objUserDivision.CreatedBy = CreatedOrMoifiy;


                    //objUserDivision.ModifiedOn = null;
                    objUserDivision.UserDivisionId = objBAL.Save_UserDivision(objUserDivision);
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                }

                lstEntity.Add(objUserDivision);
                objResponse.Status = true;

                objResponse.UserDivision = lstEntity;

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UserDivisionSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.UserDivision = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }


        #endregion

        #region UserBoardAuthority

        /// <summary>
        /// Looks up all data by Key For UserBoardAuthority.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("GET")]
        [ActionName("UserBoardAuthorityGetAll")]
        public UserBoardAuthorityResponse UserBoardAuthorityGetAll(string Key)
        {

            LogingHelper.SaveAuditInfo(Key);

            UserBoardAuthorityResponse objResponse = new UserBoardAuthorityResponse();
            UserBoardAuthorityBAL objBAL = new UserBoardAuthorityBAL();
            UserBoardAuthority objEntity = new UserBoardAuthority();
            List<UserBoardAuthority> lstBoard = new List<UserBoardAuthority>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.UserBoardAuthority = null;
                    return objResponse;

                }

                lstBoard = objBAL.Get_All_UserBoardAuthority();
                if (lstBoard != null && lstBoard.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserBoardAuthority = lstBoard;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserBoardAuthority = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UserBoardAuthorityGetAll", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.UserBoardAuthority = null;

            }
            return objResponse;


        }


        /// <summary>
        /// Get Method to get UserBoardAuthority by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ID">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("UserBoardAuthorityGetBYID")]
        public UserBoardAuthorityResponse UserBoardAuthorityGetBYID(string Key, int ID)
        {
            LogingHelper.SaveAuditInfo(Key);

            UserBoardAuthorityResponse objResponse = new UserBoardAuthorityResponse();
            UserBoardAuthorityBAL objBAL = new UserBoardAuthorityBAL();
            UserBoardAuthority objEntity = new UserBoardAuthority();
            List<UserBoardAuthority> lstBoard = new List<UserBoardAuthority>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.UserBoardAuthority = null;
                    return objResponse;
                }

                objEntity = objBAL.Get_UserBoardAuthority_byUserBoardAuthorityId(ID);
                if (objEntity != null)
                {
                    lstBoard.Add(objEntity);

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserBoardAuthority = lstBoard;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserBoardAuthority = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UserBoardAuthorityGetBYID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.UserBoardAuthority = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Get Method to get UserBoardAuthority by key and UserId.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="UserId">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("UserBoardAuthorityGetBYUserID")]
        public UserBoardAuthorityResponse UserBoardAuthorityGetBYUserID(string Key, int UserId)
        {
            LogingHelper.SaveAuditInfo(Key);

            UserBoardAuthorityResponse objResponse = new UserBoardAuthorityResponse();
            UserBoardAuthorityBAL objBAL = new UserBoardAuthorityBAL();
            UserBoardAuthority objEntity = new UserBoardAuthority();
            List<UserBoardAuthority> lstBoard = new List<UserBoardAuthority>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.UserBoardAuthority = null;
                    return objResponse;
                }

                objEntity = objBAL.Get_UserBoardAuthority_by_UserId(UserId);
                if (objEntity != null)
                {
                    lstBoard.Add(objEntity);

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserBoardAuthority = lstBoard;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserBoardAuthority = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UserBoardAuthorityGetBYID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.UserBoardAuthority = null;

            }
            return objResponse;
        }



        /// <summary>
        /// Get Method to get UserBoardAuthority by key and BoardAuthorityId.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="BoardAuthorityId">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("UserBoardAuthorityGetBYBoardAuthorityId")]
        public UserBoardAuthorityResponse UserBoardAuthorityGetBYBoardAuthorityId(string Key, int BoardAuthorityId)
        {
            LogingHelper.SaveAuditInfo(Key);

            UserBoardAuthorityResponse objResponse = new UserBoardAuthorityResponse();
            UserBoardAuthorityBAL objBAL = new UserBoardAuthorityBAL();
            UserBoardAuthority objEntity = new UserBoardAuthority();
            List<UserBoardAuthority> lstBoard = new List<UserBoardAuthority>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.UserBoardAuthority = null;
                    return objResponse;
                }

                objEntity = objBAL.Get_UserBoardAuthority_by_BoardAuthorityId(BoardAuthorityId);
                if (objEntity != null)
                {
                    lstBoard.Add(objEntity);

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserBoardAuthority = lstBoard;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.UserBoardAuthority = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UserBoardAuthorityGetBYID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.UserBoardAuthority = null;

            }
            return objResponse;
        }



        /// <summary>
        /// Save or Update the data For UserBoardAuthority
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objUserBoardAuthority">Object of UserBoardAuthority</param>
        [AcceptVerbs("POST")]
        [ActionName("UserBoardAuthoritySave")]
        public UserBoardAuthorityResponse UserBoardAuthoritySave(string Key, UserBoardAuthority objUserBoardAuthority)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;
            LogingHelper.SaveAuditInfo(Key);

            UserBoardAuthorityResponse objResponse = new UserBoardAuthorityResponse();
            UserBoardAuthorityBAL objBAL = new UserBoardAuthorityBAL();
            UserBoardAuthority objEntity = new UserBoardAuthority();
            List<UserBoardAuthority> lstEntity = new List<UserBoardAuthority>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.UserBoardAuthority = null;
                return objResponse;
            }

            string ValidationResponse = UsersValidation.ValidateUserBoardAuthorityObject(objUserBoardAuthority);

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
                if (objUserBoardAuthority.UserBoardAuthorityId > 0)
                {
                    objEntity = objBAL.Get_UserBoardAuthority_byUserBoardAuthorityId(objUserBoardAuthority.UserBoardAuthorityId);
                    if (objEntity != null)
                    {
                        objUserBoardAuthority.ModifiedOn = DateTime.Now;
                        objUserBoardAuthority.ModifiedBy = CreatedOrMoifiy;
                        objBAL.Save_UserBoardAuthority(objUserBoardAuthority);
                        objResponse.Message = Messages.UpdateSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }
                }
                else
                {

                    objUserBoardAuthority.CreatedOn = DateTime.Now;
                    objUserBoardAuthority.CreatedBy = CreatedOrMoifiy;

                    //objUserBoardAuthority.ModifiedOn = null;
                    objUserBoardAuthority.UserBoardAuthorityId = objBAL.Save_UserBoardAuthority(objUserBoardAuthority);
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                }

                lstEntity.Add(objUserBoardAuthority);
                objResponse.Status = true;

                objResponse.UserBoardAuthority = lstEntity;

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "UserBoardAuthoritySave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.UserBoardAuthority = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }


        #endregion

    }
}
