﻿using LAPP.BAL;
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

                    var lstContentItemLkSelected = lstContent.Select(Cont => new
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

                    var lstContentItemLkSelected = lstContent.Select(Cont => new
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
        }
        }

        #endregion