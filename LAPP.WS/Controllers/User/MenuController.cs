using LAPP.BAL;
using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
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
    public class MenuController : ApiController
    {
        #region Menu

        /// <summary>
        /// Looks up all data by Key For Menu.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("GET")]
        [ActionName("MenuGetAll")]
        public MenuResponse MenuGetAll(string Key)
        {

            LogingHelper.SaveAuditInfo(Key);

            MenuResponse objResponse = new MenuResponse();
            MenuBAL objBAL = new MenuBAL();
            Menu objEntity = new Menu();
            List<Menu> lstMenu = new List<Menu>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.Menu = null;
                    return objResponse;

                }

                lstMenu = objBAL.Get_All_Menu();
                if (lstMenu != null && lstMenu.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstMenuSelected = lstMenu.Select(menu => new { MenuID = menu.MenuId, Name = menu.Name, Description = menu.Description, MenuURL = menu.MenuURL, ParentMenuId = menu.ParentMenuId, MenuLevel = menu.MenuLevel, SortOrder = menu.SortOrder, IsActive = menu.IsActive }).ToList();

                    objResponse.Menu = lstMenuSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Menu = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "MenuGetAll", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.Menu = null;

            }
            return objResponse;


        }


        /// <summary>
        /// Get Method to get Menu by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ID">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("MenuGetbyId")]
        public MenuResponse MenuGetBYID(string Key, int ID)
        {
            LogingHelper.SaveAuditInfo(Key);

            MenuResponse objResponse = new MenuResponse();
            MenuBAL objBAL = new MenuBAL();
            Menu objEntity = new Menu();
            List<Menu> lstMenu = new List<Menu>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.Menu = null;
                    return objResponse;
                }

                objEntity = objBAL.Get_Menu_byMenuId(ID);
                if (objEntity != null)
                {
                    lstMenu.Add(objEntity);
                    var lstMenuSelected = lstMenu.Select(menu => new { MenuID = menu.MenuId, Name = menu.Name, Description = menu.Description, MenuURL = menu.MenuURL, ParentMenuId = menu.ParentMenuId, MenuLevel = menu.MenuLevel, SortOrder = menu.SortOrder, IsActive = menu.IsActive }).ToList();

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Menu = lstMenuSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Menu = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "MenuGetBYID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Menu = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Save or Update the data For Menu
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objMenu">Object of Menu</param>
        [AcceptVerbs("POST")]
        [ActionName("MenuSave")]
        public MenuResponse MenuSave(string Key, Menu objMenu)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;
            LogingHelper.SaveAuditInfo(Key);

            MenuResponse objResponse = new MenuResponse();
            MenuBAL objBAL = new MenuBAL();
            Menu objEntity = new Menu();
            List<Menu> lstEntity = new List<Menu>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.Menu = null;
                return objResponse;
            }

            string ValidationResponse = MenuValidation.ValidateMenuObject(objMenu);

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
                if (objMenu.MenuId > 0)
                {
                    objEntity = objBAL.Get_Menu_byMenuId(objMenu.MenuId);
                    if (objEntity != null)
                    {
                        objMenu.ModifiedOn = DateTime.Now;
                        objMenu.ModifiedBy = CreatedOrMoifiy;
                        objBAL.Save_Menu(objMenu);
                        objResponse.Message = Messages.UpdateSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }

                    else
                    {

                        objMenu.CreatedOn = DateTime.Now;
                        objMenu.CreatedBy = CreatedOrMoifiy;

                        //objMenu.ModifiedOn = null;
                        objMenu.MenuId = objBAL.Save_Menu(objMenu);
                        objResponse.Message = Messages.SaveSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    }
                }
                else
                {

                    objMenu.CreatedOn = DateTime.Now;
                    //objMenu.ModifiedOn = null;
                    objMenu.MenuId = objBAL.Save_Menu(objMenu);
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                }

                lstEntity.Add(objMenu);
                objResponse.Status = true;

                objResponse.Menu = lstEntity;

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "MenuSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.Menu = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }


        #endregion

        #region MenuUserType

        /// <summary>
        /// Get Method to get MenuUserType by key and UserTypeId.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="UserTypeId">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("MenuUserTypeGetbyUserTypeId")]
        public MenuUserTypeResponse MenuUserTypeGetbyUserTypeId(string Key, int UserTypeId)
        {

            LogingHelper.SaveAuditInfo(Key);

            MenuUserTypeResponse objResponse = new MenuUserTypeResponse();
            MenuUserTypeBAL objBAL = new MenuUserTypeBAL();
            MenuUserType objEntity = new MenuUserType();
            List<MenuUserType> lstMenuUserType = new List<MenuUserType>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.MenuUserType = null;
                    return objResponse;

                }

                lstMenuUserType = objBAL.Get_MenuUserType_by_UserTypeId(UserTypeId);
                if (lstMenuUserType != null && lstMenuUserType.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstMenuSelected = lstMenuUserType.Select(menu => new { MenuUserTypeId = menu.MenuUserTypeId, MenuId = menu.MenuId, UserTypeId = menu.UserTypeId, BoardAuthorityId = menu.BoardAuthorityId, IsActive = menu.IsActive }).ToList();


                    objResponse.MenuUserType = lstMenuSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.MenuUserType = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "MenuUserTypeGetAll", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.MenuUserType = null;

            }
            return objResponse;


        }


        /// <summary>
        /// Save or Update the data For MenuUserType
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objMenuUserType">Object of MenuUserType</param>
        [AcceptVerbs("POST")]
        [ActionName("MenuUserTypeSave")]
        public MenuUserTypeResponse MenuUserTypeSave(string Key, MenuUserType objMenuUserType)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;
            LogingHelper.SaveAuditInfo(Key);

            MenuUserTypeResponse objResponse = new MenuUserTypeResponse();
            MenuUserTypeBAL objBAL = new MenuUserTypeBAL();
            MenuUserType objEntity = new MenuUserType();
            List<MenuUserType> lstEntity = new List<MenuUserType>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.MenuUserType = null;
                return objResponse;
            }

            string ValidationResponse = MenuValidation.ValidateMenuUserTypeObject(objMenuUserType);

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
                if (objMenuUserType.MenuUserTypeId > 0)
                {
                    objEntity = objBAL.Get_MenuUserType_byMenuUserTypeId(objMenuUserType.MenuUserTypeId);
                    if (objEntity != null)
                    {
                        objMenuUserType.ModifiedOn = DateTime.Now;
                        objMenuUserType.ModifiedBy = CreatedOrMoifiy;

                        objBAL.Save_MenuUserType(objMenuUserType);
                        objResponse.Message = Messages.UpdateSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }
                }
                else
                {

                    objMenuUserType.CreatedOn = DateTime.Now;
                    objMenuUserType.CreatedBy = CreatedOrMoifiy;

                    //objMenuUserType.ModifiedOn = null;
                    objMenuUserType.MenuUserTypeId = objBAL.Save_MenuUserType(objMenuUserType);
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                }

                lstEntity.Add(objMenuUserType);
                objResponse.Status = true;

                objResponse.MenuUserType = lstEntity;

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "MenuUserTypeSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.MenuUserType = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }


        #endregion
    }
}
