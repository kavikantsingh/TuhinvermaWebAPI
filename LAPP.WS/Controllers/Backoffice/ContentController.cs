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
    public class ContentController : ApiController
    {
        #region ContentItemLk

        /// <summary>
        /// Looks up all data by Key For Content.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("GET")]
        [ActionName("ContentItemLkGetAll")]
        public ContentItemLkResponse ContentItemLkGetAll(string Key)
        {
            LogingHelper.SaveAuditInfo(Key);

            ContentItemLkResponse objResponse = new ContentItemLkResponse();
            ContentItemLkBAL objBAL = new ContentItemLkBAL();
            ContentItemLk objEntity = new ContentItemLk();
            List<ContentItemLk> lstContent = new List<ContentItemLk>();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ContentItemLk = null;
                    return objResponse;
                }

                lstContent = objBAL.Get_All_ContentItemLk();
                if (lstContent != null && lstContent.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstContentItemLkSelected = lstContent.Select(Cont => new ContentItemLk
                    {
                        ContentItemLkId = Cont.ContentItemLkId,
                        ContentLkToPageTabSectionId = Cont.ContentLkToPageTabSectionId,
                        ContentItemLkCode = Cont.ContentItemLkCode,
                        ContentItemHash = Cont.ContentItemHash,
                        ContentItemLkDesc = Cont.ContentItemLkDesc,
                        SortOrder = Cont.SortOrder,
                        EffectiveDate = Cont.EffectiveDate,
                        EndDate = Cont.EndDate,
                        IsEnabled = Cont.IsEnabled,
                        IsEditable = Cont.IsEditable,
                        IsActive = Cont.IsActive,
                        IsDeleted = Cont.IsDeleted,
                        CreatedBy = Cont.CreatedBy,
                        CreatedOn = Cont.CreatedOn,
                        ModifiedBy = Cont.ModifiedBy,
                        ModifiedOn = Cont.ModifiedOn
                    }
                    ).ToList();

                    objResponse.ContentItemLk = lstContentItemLkSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ContentItemLk = null;
                }

            }

            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ContentItemLk_Get_All", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ContentItemLk = null;
            }

            return objResponse;
        }

        /// <summary>
        /// Get Method to get Content by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ID">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("LookupContentGetBYContentItemLkID")]
        public ContentItemLkResponse LookupContentGetBYContentItemLkID(string Key, int ID)
        {
            LogingHelper.SaveAuditInfo(Key);

            ContentItemLkResponse objResponse = new ContentItemLkResponse();
            ContentItemLkBAL objBAL = new ContentItemLkBAL();
            ContentItemLk objEntity = new ContentItemLk();
            List<ContentItemLk> lstContent = new List<ContentItemLk>();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ContentItemLk = null;
                    return objResponse;
                }

                objEntity = objBAL.Get_ContentItemLk_By_ContentItemLkId(ID);
                if (objEntity != null)
                {
                    lstContent.Add(objEntity);

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstContentItemLkSelected = lstContent.Select(Cont => new ContentItemLk
                    {
                        ContentItemLkId = Cont.ContentItemLkId,
                        ContentLkToPageTabSectionId = Cont.ContentLkToPageTabSectionId,
                        ContentItemLkCode = Cont.ContentItemLkCode,
                        ContentItemHash = Cont.ContentItemHash,
                        ContentItemLkDesc = Cont.ContentItemLkDesc,
                        SortOrder = Cont.SortOrder,
                        EffectiveDate = Cont.EffectiveDate,
                        EndDate = Cont.EndDate,
                        IsEnabled = Cont.IsEnabled,
                        IsEditable = Cont.IsEditable,
                        IsActive = Cont.IsActive,
                        IsDeleted = Cont.IsDeleted,
                        CreatedBy = Cont.CreatedBy,
                        CreatedOn = Cont.CreatedOn,
                        ModifiedBy = Cont.ModifiedBy,
                        ModifiedOn = Cont.ModifiedOn
                    }
                    ).ToList();

                    objResponse.ContentItemLk = lstContentItemLkSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ContentItemLk = null;
                }
            }


            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ContentItemLk_Get_All", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ContentItemLk = null;
            }

            return objResponse;
        }

        /// <summary>
        /// Get Method to get Content by key, ContentItemLkCode and ContentItemHash.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ContentItemLkCode">Content Item Link Code.</param>
        /// <param name="ContentItemHash">Content Item Link Hash.</param>
        [AcceptVerbs("GET")]
        [ActionName("ContentGetBYContentItemLkCodeAndItemHash")]
        public ContentItemLkResponse ContentGetBYContentItemLkCodeAndItemHash(string Key, string ContentItemLkCode, int ContentItemHash)
        {
            LogingHelper.SaveAuditInfo(Key);

            ContentItemLkResponse objResponse = new ContentItemLkResponse();
            ContentItemLkBAL objBAL = new ContentItemLkBAL();
            ContentItemLk objEntity = new ContentItemLk();
            List<ContentItemLk> lstContent = new List<ContentItemLk>();
            try
            {

                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ContentItemLk = null;
                    return objResponse;
                }
                objEntity = objBAL.Get_ContentItemLk_By_ContentItemLkCode_And_ContentItemLkHash(ContentItemLkCode, ContentItemHash);
                if (objEntity != null)
                {
                    lstContent.Add(objEntity);

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstContentItemLkSelected = lstContent.Select(Cont => new ContentItemLk
                    {
                        ContentItemLkDesc = Cont.ContentItemLkDesc
                    }
                    ).ToList();
                    objResponse.ContentItemLk = lstContentItemLkSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ContentItemLk = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ContentGetBYContentItemLkCodeAndItemHash", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ContentItemLk = null;
            }
            return objResponse;
        }

        /// <summary>
        /// Get Method to get Content by key and ContentItemLkCode.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ContentItemLkCode">Content Item Link Code.</param>
        [AcceptVerbs("GET")]
        [ActionName("ContentGetBYContentItemLkCode")]
        public ContentItemLkResponse ContentGetBYContentItemLkCode(string Key, string ContentItemLkCode)
        {
            LogingHelper.SaveAuditInfo(Key);

            ContentItemLkResponse objResponse = new ContentItemLkResponse();
            ContentItemLkBAL objBAL = new ContentItemLkBAL();
            ContentItemLk objEntity = new ContentItemLk();
            List<ContentItemLk> lstContent = new List<ContentItemLk>();
            try
            {

                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ContentItemLk = null;
                    return objResponse;
                }
                lstContent = objBAL.Get_ContentItemLk_By_ContentItemLkCode(ContentItemLkCode);
                if (lstContent != null && lstContent.Count > 0)
                {
                    //lstContent.Add(objEntity);

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstContentItemLkSelected = lstContent.Select(Cont => new ContentItemLk
                    {
                        ContentItemHash = Cont.ContentItemHash,
                        ContentItemLkDesc = Cont.ContentItemLkDesc
                    }
                    ).ToList();
                    objResponse.ContentItemLk = lstContentItemLkSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ContentItemLk = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ContentGetBYContentItemLkCode", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ContentItemLk = null;
            }
            return objResponse;
        }

        /// <summary>
        /// Get Method to get Content by key and ContentLkToPageTabSectionId.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ContentLkToPageTabSectionId">Content Link To PageTabSectionId.</param>
        [AcceptVerbs("GET")]
        [ActionName("ContentGetBYContentLkToPageTabSectionId")]
        public ContentItemLkResponse ContentGetBYContentLkToPageTabSectionId(string Key, int ContentLkToPageTabSectionId)
        {
            LogingHelper.SaveAuditInfo(Key);

            ContentItemLkResponse objResponse = new ContentItemLkResponse();
            ContentItemLkBAL objBAL = new ContentItemLkBAL();
            ContentItemLk objEntity = new ContentItemLk();
            List<ContentItemLk> lstContent = new List<ContentItemLk>();
            try
            {

                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ContentItemLk = null;
                    return objResponse;
                }
                lstContent = objBAL.Get_ContentItemLk_By_ContentLkToPageTabSectionId(ContentLkToPageTabSectionId);
                if (lstContent != null && lstContent.Count > 0)
                {
                    //lstContent.Add(objEntity);

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstContentItemLkSelected = lstContent.Select(Cont => new ContentItemLk
                    {
                        ContentItemHash = Cont.ContentItemHash,
                        ContentItemLkDesc = Cont.ContentItemLkDesc
                    }
                    ).ToList();
                    objResponse.ContentItemLk = lstContentItemLkSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ContentItemLk = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ContentGetBYContentLkToPageTabSectionId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ContentItemLk = null;
            }
            return objResponse;
        }

        /// <summary>
        /// Get Method to get Content by key and ContentItemLkId.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ContentItemLkId">Content Link Id.</param>
        [AcceptVerbs("GET")]
        [ActionName("ContentGetBYContentItemLkId")]
        public ContentItemLkResponse ContentGetBYContentItemLkId(string Key, int ContentItemLkId)
        {
            LogingHelper.SaveAuditInfo(Key);

            ContentItemLkResponse objResponse = new ContentItemLkResponse();
            ContentItemLkBAL objBAL = new ContentItemLkBAL();
            ContentItemLk objEntity = new ContentItemLk();
            List<ContentItemLk> lstContent = new List<ContentItemLk>();
            try
            {

                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ContentItemLk = null;
                    return objResponse;
                }
                objEntity = objBAL.Get_ContentItemLk_By_ContentItemLkId(ContentItemLkId);
                if (objEntity != null)
                {
                    lstContent.Add(objEntity);

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstContentItemLkSelected = lstContent.Select(Cont => new ContentItemLk
                    {
                        ContentItemHash = Cont.ContentItemHash,
                        ContentItemLkDesc = Cont.ContentItemLkDesc
                    }
                    ).ToList();
                    objResponse.ContentItemLk = lstContentItemLkSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ContentItemLk = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ContentGetBYContentItemLkId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ContentItemLk = null;
            }
            return objResponse;
        }
        /// <summary>
        /// Get Content Type Name
        /// </summary>
        /// <param name="Key">API security key</param>
        /// <returns>ContentLkToPageTabSection</returns>
        [AcceptVerbs("GET")]
        [ActionName("ContentGetContentTypeName")]
        public ContentLkToPageTabSectionResponse ContentGetContentTypeName(string Key)
        {
            LogingHelper.SaveAuditInfo(Key);

            ContentLkToPageTabSectionResponse objResponse = new ContentLkToPageTabSectionResponse();
            ContentLkToPageTabSectionBAL objBAL = new ContentLkToPageTabSectionBAL();
            ContentLkToPageTabSection objEntity = new ContentLkToPageTabSection();
            List<ContentLkToPageTabSection> lstContent = new List<ContentLkToPageTabSection>();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ContentLkToPageTabSection = null;
                    return objResponse;
                }

                lstContent = objBAL.Get_All_ContentLkToPageTabSection();
                if (lstContent != null && lstContent.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstContentItemLkSelected = lstContent.Select(Cont => new ContentLkToPageTabSection
                    {
                        ContentLkToPageTabSectionId = Cont.ContentLkToPageTabSectionId,
                        ContentTypeName = Cont.ContentTypeName,
                        MasterTransactionId = Cont.MasterTransactionId,
                        PageModuleId = Cont.PageModuleId,
                        PageModuleTabSubModuleId = Cont.PageModuleTabSubModuleId,
                        PageTabSectionId = Cont.PageTabSectionId,
                        EffectiveDate = Cont.EffectiveDate,
                        EndDate = Cont.EndDate,
                        IsEnabled = Cont.IsEnabled,
                        IsEditable = Cont.IsEditable,
                        IsActive = Cont.IsActive,
                        IsDeleted = Cont.IsDeleted,
                        CreatedBy = Cont.CreatedBy,
                        CreatedOn = Cont.CreatedOn,
                        ModifiedBy = Cont.ModifiedBy,
                        ModifiedOn = Cont.ModifiedOn
                    }
                    ).ToList();

                    objResponse.ContentLkToPageTabSection = lstContentItemLkSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ContentLkToPageTabSection = null;
                }

            }

            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ContentGetContentTypeName", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ContentLkToPageTabSection = null;
            }

            return objResponse;
        }

        /// <summary>
        /// Get Content Type Name By Page Tab Section Id
        /// </summary>
        /// <param name="Key">API security key</param>
        /// <param name="PageModId">Page Module Id</param>
        ///<param name="PageTabSecId">Page Tab Section Id</param>
        ///<param name="PageModTabSubModId">Page Module Tab Sub Module Id</param>
        /// <returns>ContentLkToPageTabSection</returns>
        [AcceptVerbs("GET")]
        [ActionName("ContentGetContentTypeNameByPageTabSectionId")]
        public ContentLkToPageTabSectionResponse ContentGetContentTypeNameByPageTabSectionId(string Key, int PageModId,  int PageTabSecId, int PageModTabSubModId)
        {
            LogingHelper.SaveAuditInfo(Key);

            ContentLkToPageTabSectionResponse objResponse = new ContentLkToPageTabSectionResponse();
            ContentLkToPageTabSectionBAL objBAL = new ContentLkToPageTabSectionBAL();
            ContentLkToPageTabSection objEntity = new ContentLkToPageTabSection();
            List<ContentLkToPageTabSection> lstContent = new List<ContentLkToPageTabSection>();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ContentLkToPageTabSection = null;
                    return objResponse;
                }

                lstContent = objBAL.Get_ContentLkToPageTabSection_By_PageModId_PageTabSecId_PageModTabSubModId(PageModId, PageTabSecId, PageModTabSubModId);
                if (lstContent != null && lstContent.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstContentItemLkSelected = lstContent.Select(Cont => new ContentLkToPageTabSection
                    {
                        ContentLkToPageTabSectionId = Cont.ContentLkToPageTabSectionId,
                        ContentTypeName = Cont.ContentTypeName,
                        MasterTransactionId = Cont.MasterTransactionId,
                        PageModuleId = Cont.PageModuleId,
                        PageModuleTabSubModuleId = Cont.PageModuleTabSubModuleId,
                        PageTabSectionId = Cont.PageTabSectionId,
                        EffectiveDate = Cont.EffectiveDate,
                        EndDate = Cont.EndDate,
                        IsEnabled = Cont.IsEnabled,
                        IsEditable = Cont.IsEditable,
                        IsActive = Cont.IsActive,
                        IsDeleted = Cont.IsDeleted,
                        CreatedBy = Cont.CreatedBy,
                        CreatedOn = Cont.CreatedOn,
                        ModifiedBy = Cont.ModifiedBy,
                        ModifiedOn = Cont.ModifiedOn
                    }
                    ).ToList();

                    objResponse.ContentLkToPageTabSection = lstContentItemLkSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ContentLkToPageTabSection = null;
                }

            }

            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ContentGetContentTypeName", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ContentLkToPageTabSection = null;
            }

            return objResponse;
        }

        /// <summary>
        /// Get Content Type Name By Page Tab Section Id
        /// </summary>
        /// <param name="Key">API security key</param>
        /// <param name="PageModuleId">Page Module Id</param>
        /// <returns>ContentItemLkResponse</returns>
        [AcceptVerbs("GET")]
        [ActionName("ContentGetContentInformation")]
        public ContentItemLkResponse ContentGetContentInformation(string Key, int PageModuleId)
        {
            LogingHelper.SaveAuditInfo(Key);

            ContentItemLkResponse objResponse = new ContentItemLkResponse();
            ContentItemLkBAL objBAL = new ContentItemLkBAL();
            ContentItemLk objEntity = new ContentItemLk();
            List<ContentItemLk> lstContent = new List<ContentItemLk>();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ContentItemLk = null;
                    return objResponse;
                }

                lstContent = objBAL.Get_ContentItemLk_By_PageModuleId(PageModuleId);
                if (lstContent != null && lstContent.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstContentItemLkSelected = lstContent.Select(Cont => new ContentItemLk
                    {
                        ContentItemLkId = Cont.ContentItemLkId,
                        ContentLkToPageTabSectionId = Cont.ContentLkToPageTabSectionId,
                        ContentItemLkCode = Cont.ContentItemLkCode,
                        ContentItemHash = Cont.ContentItemHash,
                        ContentItemLkDesc = Cont.ContentItemLkDesc,
                        SortOrder = Cont.SortOrder,
                        EffectiveDate = Cont.EffectiveDate,
                        EndDate = Cont.EndDate,
                        IsEnabled = Cont.IsEnabled,
                        IsEditable = Cont.IsEditable,
                        IsActive = Cont.IsActive,
                        IsDeleted = Cont.IsDeleted,
                        CreatedBy = Cont.CreatedBy,
                        CreatedOn = Cont.CreatedOn,
                        ModifiedBy = Cont.ModifiedBy,
                        ModifiedOn = Cont.ModifiedOn
                    }
                    ).ToList();

                    objResponse.ContentItemLk = lstContentItemLkSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ContentItemLk = null;
                }

            }

            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ContentItemLk_Get_All", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ContentItemLk = null;
            }

            return objResponse;
        }

        /// <summary>
        /// Updates ContentItemLk Table
        /// </summary>
        /// <param name="Key">API security key</param>
        /// <param name="objContentItemLkPost">ContentItemLkPost</param>
        /// <returns>ContentItemLkResponse</returns>
        [AcceptVerbs("POST")]
        [ActionName("ContentUpdateContentInfo")]
        public ContentItemLkResponse ContentUpdateContentInfo(string Key, ContentItemLkPost objContentItemLkPost)
        {
            int CreateOrModify = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo();
            ContentItemLkResponse objResponse = new ContentItemLkResponse();
            ContentItemLkPost objEntity = new ContentItemLkPost();
            ContentItemLkBAL objBAL = new ContentItemLkBAL();
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
                if (objContentItemLkPost != null)
                {
                    objEntity.ContentItemLkId = objContentItemLkPost.ContentItemLkId;
                    objEntity.ContentItemHash = objContentItemLkPost.ContentItemHash;
                    objEntity.ContentItemLkDesc = objContentItemLkPost.ContentItemLkDesc;
                    objEntity.EffectiveDate = objContentItemLkPost.EffectiveDate;
                    objEntity.EndDate = objContentItemLkPost.EndDate;

                    int ReturnContentItemLkId = objBAL.Update_ContentItemLk(objEntity);
                    List<ContentItemLk> lstTemp = new List<ContentItemLk>();
                    ContentItemLk objTempEntity = new ContentItemLk();
                    objTempEntity.ContentItemLkId = ReturnContentItemLkId;
                    lstTemp.Add(objTempEntity);
                    objResponse.ContentItemLk = lstTemp;
                    objResponse.Message = "Successful";
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";



                }
                else
                {
                    objResponse.Message = "ContentItemLk object cannot be null.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo("", ex, "ContentUpdateContentInfo", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
            }
            return objResponse;
        }

        /// <summary>
        /// Get affidavit Content
        /// </summary>
        /// <param name="Key">API security key</param>
        /// <returns>ContentItemLkResponse</returns>
        [AcceptVerbs("GET")]
        [ActionName("ContentGetAffidavitContent")]
        public ContentItemLkAffidavitResponse ContentGetAffidavitContent(string Key)
        {
            LogingHelper.SaveAuditInfo(Key);

            ContentItemLkAffidavitResponse objResponse = new ContentItemLkAffidavitResponse();
            ContentItemLkBAL objBAL = new ContentItemLkBAL();
            ContentItemLk objEntity = new ContentItemLk();
            List<ContentItemLkAffidavitItem> lstContent = new List<ContentItemLkAffidavitItem>();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ContentItems = null;
                    return objResponse;
                }
                var items = new Dictionary<int, ContentItemLk>();
                items.Add(1, objBAL.Get_ContentItemLk_GET_BY_ContentItemLkId_AND_Code(21, "SchoolEligibility"));
                items.Add(2, objBAL.Get_ContentItemLk_GET_BY_ContentItemLkId_AND_Code(21, "SchoolEligibility"));
                items.Add(3, objBAL.Get_ContentItemLk_GET_BY_ContentItemLkId_AND_Code(21, "SchoolEligibility"));
                items.Add(4, objBAL.Get_ContentItemLk_GET_BY_ContentItemLkId_AND_Code(21, "SchoolEligibility"));
                items.Add(5, objBAL.Get_ContentItemLk_GET_BY_ContentItemLkId_AND_Code(21, "SchoolEligibility"));
                items.Add(6, objBAL.Get_ContentItemLk_GET_BY_ContentItemLkId_AND_Code(21, "SchoolEligibility"));
                items.Add(7, objBAL.Get_ContentItemLk_GET_BY_ContentItemLkId_AND_Code(21, "SchoolEligibility"));


                if (items != null && items.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    lstContent = items.Select(i => new ContentItemLkAffidavitItem
                    {
                        ContentItemLkId = i.Value.ContentItemLkId,
                        ContentItemLkCode = i.Value.ContentItemLkCode,
                        ContentItemLkDesc = i.Value.ContentItemLkDesc,
                        ContentItemNo = i.Key,
                        IsChecked = false
                    }).ToList();

                    objResponse.ContentItems = lstContent;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ContentItems = null;
                }

            }

            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ContentGetAffidavitContent", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ContentItems = null;
            }

            return objResponse;
        }
        #endregion
    }
}
        
