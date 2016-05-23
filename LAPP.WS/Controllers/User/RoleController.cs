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
using System.Web.Http.Description;

namespace LAPP.WS.Controllers.User
{
    public class RoleController : ApiController
    {
        #region Role

        /// <summary>
        /// Looks up all data by Key For Role.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("GET")]
        [ActionName("RoleGetAll")]
        public RoleResponse RoleGetAll(string Key)
        {

            LogingHelper.SaveAuditInfo(Key);

            RoleResponse objResponse = new RoleResponse();
            RoleBAL objBAL = new RoleBAL();
            Role objEntity = new Role();
            List<Role> lstRole = new List<Role>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.Role = null;
                    return objResponse;

                }

                lstRole = objBAL.Get_All_Role();
                if (lstRole != null && lstRole.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstRoleSelected = lstRole.Select(menu => new
                    {
                        RoleId = menu.RoleId,
                        Name = menu.Name,
                        Description = menu.Description,
                        UserTypeId = menu.UserTypeId,
                        UserTypeName = menu.UserTypeName,
                        BoardAuthorityId = menu.BoardAuthorityId,
                        BoardAuthorityName = menu.BoardAuthorityName,
                        DivisionId = menu.DivisionId,
                        DivisionName = menu.DivisionName,
                        IsEnabled = menu.IsEnabled,
                        IsActive = menu.IsActive
                    }).ToList();

                    objResponse.Role = lstRoleSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Role = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "RoleGetAll", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.Role = null;

            }
            return objResponse;


        }

        /// <summary>
        /// Get Method to get Role by key and BoardAuthorityId.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="BoardAuthorityId">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("RoleGetbyBoardAuthorityId")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public RoleResponse RoleGetbyBoardAuthorityId(string Key, int BoardAuthorityId)
        {

            LogingHelper.SaveAuditInfo(Key);

            RoleResponse objResponse = new RoleResponse();
            RoleBAL objBAL = new RoleBAL();
            Role objEntity = new Role();
            List<Role> lstRole = new List<Role>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.Role = null;
                    return objResponse;

                }

                lstRole = objBAL.Get_Role_by_BoardAuthorityId(BoardAuthorityId);
                if (lstRole != null && lstRole.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstRoleSelected = lstRole.Select(menu => new
                    {
                        RoleId = menu.RoleId,
                        Name = menu.Name,
                        Description = menu.Description,
                        UserTypeId = menu.UserTypeId,
                        UserTypeName = menu.UserTypeName,
                        BoardAuthorityId = menu.BoardAuthorityId,
                        BoardAuthorityName = menu.BoardAuthorityName,
                        DivisionId = menu.DivisionId,
                        DivisionName = menu.DivisionName,
                        IsEnabled = menu.IsEnabled,
                        IsActive = menu.IsActive
                    }).ToList();

                    objResponse.Role = lstRoleSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Role = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "RoleGetbyBoardAuthorityId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.Role = null;

            }
            return objResponse;


        }

        /// <summary>
        /// Get Method to get Role by key and BoardAuthorityId.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="UserTypeId">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("RoleGetbyUserTypeId")]
        public RoleResponse RoleGetbyUserTypeId(string Key, int UserTypeId)
        {

            LogingHelper.SaveAuditInfo(Key);

            RoleResponse objResponse = new RoleResponse();
            RoleBAL objBAL = new RoleBAL();
            Role objEntity = new Role();
            List<Role> lstRole = new List<Role>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.Role = null;
                    return objResponse;

                }

                lstRole = objBAL.Get_Role_by_UserTypeId(UserTypeId);
                if (lstRole != null && lstRole.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstRoleSelected = lstRole.Select(menu => new
                    {
                        RoleId = menu.RoleId,
                        Name = menu.Name,
                        Description = menu.Description,
                        UserTypeId = menu.UserTypeId,
                        UserTypeName = menu.UserTypeName,
                        BoardAuthorityId = menu.BoardAuthorityId,
                        BoardAuthorityName = menu.BoardAuthorityName,
                        DivisionId = menu.DivisionId,
                        DivisionName = menu.DivisionName,
                        IsEnabled = menu.IsEnabled,
                        IsActive = menu.IsActive
                    }).ToList();


                    objResponse.Role = lstRoleSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Role = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "RoleGetbyUserTypeId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.Role = null;

            }
            return objResponse;


        }



        /// <summary>
        /// Save or Update the data For Role
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objRole">Object of Role</param>
        [AcceptVerbs("POST")]
        [ActionName("RoleSave")]
        public RoleResponse RoleSave(string Key, Role objRole)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;
            LogingHelper.SaveAuditInfo(Key);

            RoleResponse objResponse = new RoleResponse();
            RoleBAL objBAL = new RoleBAL();
            Role objEntity = new Role();
            List<Role> lstEntity = new List<Role>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.Role = null;
                return objResponse;
            }

            string ValidationResponse = RoleValidation.ValidateRoleObject(objRole);

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
                if (objRole.RoleId > 0)
                {
                    objEntity = objBAL.Get_Role_byRoleId(objRole.RoleId);
                    if (objEntity != null)
                    {
                        objRole.ModifiedOn = DateTime.Now;
                        objRole.ModifiedBy = CreatedOrMoifiy;

                        objBAL.Save_Role(objRole);
                        objResponse.Message = Messages.UpdateSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }
                }
                else
                {
                    objRole.RoleGuid = Guid.NewGuid().ToString();
                    objRole.CreatedOn = DateTime.Now;
                    objRole.CreatedBy = CreatedOrMoifiy;

                    //objRole.ModifiedOn = null;
                    objRole.RoleId = objBAL.Save_Role(objRole);
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                }

                lstEntity.Add(objRole);
                objResponse.Status = true;

                objResponse.Role = lstEntity;

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "RoleSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.Role = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }


        #endregion

        #region RoleMenu

        /// <summary>
        /// Get Method to get RoleMenu by key and RoleId.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="RoleId">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("RoleMenuGetbyRoleId")]
        public RoleMenuResponse RoleMenuGetbyRoleId(string Key, int RoleId)
        {

            LogingHelper.SaveAuditInfo(Key);

            RoleMenuResponse objResponse = new RoleMenuResponse();
            RoleMenuBAL objBAL = new RoleMenuBAL();
            RoleMenu objEntity = new RoleMenu();
            List<RoleMenu> lstRoleMenu = new List<RoleMenu>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.RoleMenu = null;
                    return objResponse;

                }

                lstRoleMenu = objBAL.Get_RoleMenu_by_RoleId(RoleId);
                if (lstRoleMenu != null && lstRoleMenu.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");


                    var lstRoleMenuSelected = lstRoleMenu.Select(menu => new
                    {
                        RoleMenuId = menu.RoleMenuId,
                        ParentMenuId = menu.ParentMenuId,
                        RoleId = menu.RoleId,
                        RoleName = menu.RoleName,
                        MenuId = menu.MenuId,
                        MenuName = menu.MenuName,
                        MenuLevel = menu.MenuLevel,
                        MenuDescription = menu.MenuDescription,
                        MenuURL = menu.MenuURL,
                        MenuSortOrder = menu.MenuSortOrder,
                        Create = menu.Create,
                        Update = menu.Update,
                        Delete = menu.Delete,
                        Read = menu.Read,
                        IsActive = menu.IsActive,
                    }).ToList();


                    objResponse.RoleMenu = lstRoleMenuSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.RoleMenu = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "RoleMenuGetbyRoleId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.RoleMenu = null;

            }
            return objResponse;


        }


        /// <summary>
        /// Save or Update the data For RoleMenu
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objRoleMenu">Object of RoleMenu</param>
        [AcceptVerbs("POST")]
        [ActionName("RoleMenuSave")]
        public RoleMenuResponse RoleMenuSave(string Key, RoleMenuRequest objRoleMenu)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;
            LogingHelper.SaveAuditInfo(Key);

            RoleMenuResponse objResponse = new RoleMenuResponse();
            RoleMenuBAL objBAL = new RoleMenuBAL();
            RoleMenu objEntity = new RoleMenu();
            List<RoleMenu> lstEntity = new List<RoleMenu>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.RoleMenu = null;
                return objResponse;
            }

            string ValidationResponse = RoleValidation.ValidateRoleMenuObject(objRoleMenu);

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
                if (objRoleMenu.RoleMenuId > 0)
                {
                    objEntity = objBAL.Get_RoleMenu_byRoleMenuId(objRoleMenu.RoleMenuId);
                    if (objEntity != null)
                    {
                        objEntity.Create = objRoleMenu.Create;
                        objEntity.Delete = objRoleMenu.Delete;
                        objEntity.IsActive = objRoleMenu.IsActive;
                        objEntity.IsDeleted = objRoleMenu.IsDeleted;
                        objEntity.MenuId = objRoleMenu.MenuId;
                        objEntity.Read = objRoleMenu.Read;
                        objEntity.RoleId = objRoleMenu.RoleId;
                        objEntity.Update = objRoleMenu.Update;

                        objEntity.ModifiedOn = DateTime.Now;
                        objEntity.ModifiedBy = CreatedOrMoifiy;

                        objBAL.Save_RoleMenu(objEntity);
                        objResponse.Message = Messages.UpdateSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }
                }
                else
                {
                    objEntity = new RoleMenu();
                    objEntity.Create = objRoleMenu.Create;
                    objEntity.Delete = objRoleMenu.Delete;
                    objEntity.IsActive = objRoleMenu.IsActive;
                    objEntity.IsDeleted = objRoleMenu.IsDeleted;
                    objEntity.MenuId = objRoleMenu.MenuId;
                    objEntity.Read = objRoleMenu.Read;
                    objEntity.RoleId = objRoleMenu.RoleId;
                    objEntity.Update = objRoleMenu.Update;
                    objEntity.CreatedOn = DateTime.Now;
                    objEntity.CreatedBy = CreatedOrMoifiy;

                    //objRoleMenu.ModifiedOn = null;
                    objEntity.RoleMenuId = objBAL.Save_RoleMenu(objEntity);
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                }

                lstEntity.Add(objEntity);
                objResponse.Status = true;

                var lstRoleMenuSelected = lstEntity.Select(menu => new
                {
                    RoleMenuId = menu.RoleMenuId,
                    ParentMenuId = menu.ParentMenuId,
                    RoleId = menu.RoleId,
                    RoleName = menu.RoleName,
                    MenuId = menu.MenuId,
                    MenuName = menu.MenuName,
                    MenuLevel = menu.MenuLevel,
                    MenuDescription = menu.MenuDescription,
                    MenuURL = menu.MenuURL,
                    MenuSortOrder = menu.MenuSortOrder,
                    Create = menu.Create,
                    Update = menu.Update,
                    Delete = menu.Delete,
                    Read = menu.Read,
                    IsActive = menu.IsActive,
                }).ToList();

                objResponse.RoleMenu = lstRoleMenuSelected;

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "RoleMenuSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.RoleMenu = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }


        #endregion
    }
}
