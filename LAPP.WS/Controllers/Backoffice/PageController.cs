using LAPP.BAL;
using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.LOGING;
using LAPP.WS.App_Helper;
using LAPP.WS.App_Helper.Common;
using LAPP.WS.ValidateController.Backoffice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace LAPP.WS.Controllers.Backoffice
{
    public class PageController : ApiController
    {
        /// <summary>
        /// Looks up all data by Key For Pages.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("GET")]
        [ActionName("PageGetAllPageNames")]
        public PageModuleResponse PageGetAllPageNames(string Key)
        {
            LogingHelper.SaveAuditInfo(Key);

            PageModuleResponse objResponse = new PageModuleResponse();
            PageModuleBAL objBAL = new PageModuleBAL();
            PageModule objEntity = new PageModule();
            List<PageModule> lstPageModule = new List<PageModule>();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.PageModule = null;
                    return objResponse;
                }
                lstPageModule = objBAL.Get_All_PageModule();
                if (lstPageModule != null && lstPageModule.Count > 0)
                {
                    objResponse.ResponseReason = "To Get All Page Modules";
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstPageModuleSelected = lstPageModule.Select(Page => new PageModule
                    {
                        PageModuleId = Page.PageModuleId,
                        PageModuleCode = Page.PageModuleCode,
                        PageModuleName = Page.PageModuleName,
                        PageModuleDesc = Page.PageModuleDesc,
                        MasterTransactionId = Page.MasterTransactionId,
                        IsEnabled = Page.IsEnabled,
                        IsReadOnly = Page.IsReadOnly,
                        IsActive = Page.IsActive,
                        IsDeleted = Page.IsDeleted,
                        CreatedBy = Page.CreatedBy,
                        CreatedOn = Page.CreatedOn,
                        ModifiedBy = Page.ModifiedBy,
                        ModifiedOn = Page.ModifiedOn
                    }
                    ).ToList();

                    objResponse.PageModule = lstPageModuleSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.PageModule = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "PageGetAllPageNames", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.PageModule = null;
            }

            return objResponse;
        }

        /// <summary>
        /// Looks up all data by Key For Pages.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="PageModuleId">The PageModule .</param>
        [AcceptVerbs("GET")]
        [ActionName("PageGetAllTabsByPageModuleId")]
        public PageModuleTabSubModuleResponse PageGetAllTabsByPageModuleId(string Key, int PageModuleId)
        {
            LogingHelper.SaveAuditInfo(Key);

            PageModuleTabSubModuleResponse objResponse = new PageModuleTabSubModuleResponse();
            PageModuleTabSubModuleBAL objBAL = new PageModuleTabSubModuleBAL();
            PageModuleTabSubModule objEntity = new PageModuleTabSubModule();
            List<PageModuleTabSubModule> lstPageModuleTabSubModule = new List<PageModuleTabSubModule>();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.PageModuleTabSubModule = null;
                    return objResponse;
                }
                lstPageModuleTabSubModule = objBAL.Get_All_PageModuletabsubmoduleByPageModuleId(PageModuleId);
                if (lstPageModuleTabSubModule != null && lstPageModuleTabSubModule.Count > 0)
                {
                    objResponse.ResponseReason = "To Get All Page Modules Tabs Sub Modules";
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstPageModuleTabSubModuleSelected = lstPageModuleTabSubModule.Select(TabSubModule => new
                    {
                        PageModuleTabSubModuleId = TabSubModule.PageModuleTabSubModuleId,
                        PageModuleTabSubModuleCode = TabSubModule.PageModuleTabSubModuleCode,
                        PageModuleTabSubModuleName = TabSubModule.PageModuleTabSubModuleName,
                        PageModuleTabSubModuleDesc = TabSubModule.PageModuleTabSubModuleDesc,
                        MasterTransactionId = TabSubModule.MasterTransactionId,
                        PageModuleId = TabSubModule.PageModuleId,
                        IsEnabled = TabSubModule.IsEnabled,
                        IsReadOnly = TabSubModule.IsReadOnly,
                        IsActive = TabSubModule.IsActive,
                        IsDeleted = TabSubModule.IsDeleted,
                        CreatedBy = TabSubModule.CreatedBy,
                        CreatedOn = TabSubModule.CreatedOn,
                        ModifiedBy = TabSubModule.ModifiedBy,
                        ModifiedOn = TabSubModule.ModifiedOn
                    }
                    ).ToList();

                    objResponse.PageModuleTabSubModule = lstPageModuleTabSubModuleSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.PageModuleTabSubModule = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "PageGetAllTabsByPageModuleId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.PageModuleTabSubModule = null;
            }
            return objResponse;
        }

        /// <summary>
        /// Looks up all data by Key For Pages.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="PageModuleTabSubModuleId">The PageModule .</param>
        [AcceptVerbs("GET")]
        [ActionName("PageGetAllSectionsByTabId")]
        public PageTabSectionResponse PageGetAllSectionsByTabId(string Key, int PageModuleTabSubModuleId)//PageModuleId
        {
            LogingHelper.SaveAuditInfo(Key);

            PageTabSectionResponse objResponse = new PageTabSectionResponse();
            PageTabSectionBAL objBAL = new PageTabSectionBAL();
            PageTabSection objEntity = new PageTabSection();
            List<PageTabSection> lstPageTabSection = new List<PageTabSection>();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.PageTabSection = null;
                    return objResponse;
                }
                lstPageTabSection = objBAL.Get_All_PageTabSection_By_PageModuleTabSubModuleId(PageModuleTabSubModuleId);
                if (lstPageTabSection != null && lstPageTabSection.Count > 0)
                {
                    objResponse.ResponseReason = "To Get All Sections of a Tab";
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstPageTabSectionSelected = lstPageTabSection.Select(Section => new
                    {
                        PageTabSectionId = Section.PageTabSectionId,
                        PageTabSectionCode = Section.PageTabSectionCode,
                        PageTabSectionName = Section.PageTabSectionName,
                        PageTabSectionDesc = Section.PageTabSectionDesc,
                        MasterTransactionId = Section.MasterTransactionId,
                        PageModuleId = Section.PageModuleId,
                        PageModuleTabSubModuleId = Section.PageModuleTabSubModuleId,
                        IsEnabled = Section.IsEnabled,
                        IsReadOnly = Section.IsReadOnly,
                        IsActive = Section.IsActive,
                        IsDeleted = Section.IsDeleted,
                        CreatedBy = Section.CreatedBy,
                        CreatedOn = Section.CreatedOn,
                        ModifiedBy = Section.ModifiedBy,
                        ModifiedOn = Section.ModifiedOn
                    }
                    ).ToList();

                    objResponse.PageTabSection = lstPageTabSectionSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.PageTabSection = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "PageGetAllSectionsByTabId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.PageTabSection = null;
            }
            return objResponse;
        }
    }
}
