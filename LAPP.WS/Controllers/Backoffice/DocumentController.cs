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
    public class DocumentController : ApiController
    {
        /// <summary>
        /// Get Method to get DocumentTypeName by key, DocumentId and DocumentCode.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="DocId">Document ID.</param>
        /// <param name="DocCode">Document Code.</param>
        [AcceptVerbs("GET")]
        [ActionName("DocumentGetDocumentTypeName")]
        public DocumentMasterGETResponse DocumentGetDocumentTypeName(string Key, int DocId, string DocCode)
        {
            LogingHelper.SaveAuditInfo(Key);

            DocumentMasterGETResponse objResponse = new DocumentMasterGETResponse();
            DocumentMasterBAL objBAL = new DocumentMasterBAL();
            DocumentMasterGET objEntity = new DocumentMasterGET();
            List<DocumentMasterGET> lstDocumentMasterGET = new List<DocumentMasterGET>();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.DocumentMasterGET = null;
                    return objResponse;
                }
                lstDocumentMasterGET = objBAL.Get_DocumentMaster_By_DocId_And_DocCode(DocId, DocCode);
                if (lstDocumentMasterGET != null && lstDocumentMasterGET.Count > 0)
                {
                    objResponse.ResponseReason = "To Get All Page Modules";
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstDocumentMasterGETSelected = lstDocumentMasterGET.Select(DocMaster => new DocumentMasterGET
                    {
                        DocumentTypeId = DocMaster.DocumentTypeId,
                        DocumentTypeIdName = DocMaster.DocumentTypeIdName,
                        DocumentTypeDesc = DocMaster.DocumentTypeDesc,
                        Max_size = DocMaster.Max_size
                    }
                    ).ToList();

                    objResponse.DocumentMasterGET = lstDocumentMasterGETSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.DocumentMasterGET = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "DocumentGetDocumentTypeName", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.DocumentMasterGET = null;
            }

            return objResponse;
        }

        /// <summary>
        /// Get Method to get ALL DocumentMaster.
        /// </summary>
        /// <param name="Key">API security key.</param>
        [AcceptVerbs("GET")]
        [ActionName("DocumentMasterGetALL")]
        public DocumentMasterGETResponse DocumentMasterGetALL(string Key)
        {
            LogingHelper.SaveAuditInfo(Key);

            DocumentMasterGETResponse objResponse = new DocumentMasterGETResponse();
            DocumentMasterBAL objBAL = new DocumentMasterBAL();
            DocumentMasterGET objEntity = new DocumentMasterGET();
            List<DocumentMasterGET> lstDocumentMaster = new List<DocumentMasterGET>();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.DocumentMasterGET = null;
                    return objResponse;
                }
                lstDocumentMaster = objBAL.Get_All_DocumentMaster();
                if (lstDocumentMaster != null && lstDocumentMaster.Count > 0)
                {
                    objResponse.ResponseReason = "To Get All Document Master";
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstDocumentMasterGETSelected = lstDocumentMaster.Select(DocMaster => new DocumentMasterGET
                    {
                        DocumentMasterId = DocMaster.DocumentMasterId,
                        DocumentId = DocMaster.DocumentId,
                        DocumentCd = DocMaster.DocumentCd,
                        DocumentName = DocMaster.DocumentName,
                        DocumentTypeId = DocMaster.DocumentTypeId,
                        DocumentTypeIdName = DocMaster.DocumentTypeIdName,
                        DocumentTypeDesc = DocMaster.DocumentTypeDesc,
                        Max_size = DocMaster.Max_size,
                        IsActive = DocMaster.IsActive
                    }
                    ).ToList();

                    objResponse.DocumentMasterGET = lstDocumentMasterGETSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.DocumentMasterGET = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "DocumentMasterGetALL", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.DocumentMasterGET = null;
            }

            return objResponse;
        }

        /// <summary>
        /// To get documents by using PageTabSectionId
        /// </summary>
        /// <param name="Key">API Key</param>
        /// <param name="PageTabSectionId">PageTabSectionId</param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        [ActionName("DocumentMasterGetByPageTabSectionId")]
        public DocumentMasterGETResponse DocumentMasterGetByPageTabSectionId(string Key, int PageTabSectionId)
        {
            LogingHelper.SaveAuditInfo(Key);

            DocumentMasterGETResponse objResponse = new DocumentMasterGETResponse();
            DocumentMasterBAL objBAL = new DocumentMasterBAL();
            DocumentMasterGET objEntity = new DocumentMasterGET();
            List<DocumentMasterGET> lstDocumentMaster = new List<DocumentMasterGET>();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.DocumentMasterGET = null;
                    return objResponse;
                }
                lstDocumentMaster = objBAL.Get_All_DocumentMaster();
                if (lstDocumentMaster != null && lstDocumentMaster.Count > 0)
                {
                    objResponse.ResponseReason = "To Get All Document Master";
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstDocumentMasterGETSelected = lstDocumentMaster.Select(DocMaster => new DocumentMasterGET
                    {
                        DocumentMasterId = DocMaster.DocumentMasterId,
                        DocumentId = DocMaster.DocumentId,
                        DocumentCd = DocMaster.DocumentCd,
                        DocumentName = DocMaster.DocumentName,
                        DocumentTypeId = DocMaster.DocumentTypeId,
                        DocumentTypeIdName = DocMaster.DocumentTypeIdName,
                        DocumentTypeDesc = DocMaster.DocumentTypeDesc,
                        Max_size = DocMaster.Max_size,
                        IsActive = DocMaster.IsActive,
                        PageTabSectionId = DocMaster.PageTabSectionId
                    }
                    ).Where(x => x.PageTabSectionId == PageTabSectionId).ToList();

                    objResponse.DocumentMasterGET = lstDocumentMasterGETSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.DocumentMasterGET = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "DocumentMasterGetByPageTabSectionId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.DocumentMasterGET = null;
            }

            return objResponse;
        }


        /// <summary>
        /// To get document resultset.
        /// </summary>
        /// <param name="Key">API security key.</param>
        [AcceptVerbs("GET")]
        [ActionName("GetDocumentResultSet")]
        public DocumentResultSetResponse GetDocumentResultSet(string Key)
        {
            LogingHelper.SaveAuditInfo(Key);

            var objResponse = new DocumentResultSetResponse();
            var objBAL = new DocumentMasterBAL();
            var objEntity = new DocumentViewModel();
            var lstDocumentMaster = new List<DocumentViewModel>();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.DocumentViewModel = null;
                    return objResponse;
                }
                lstDocumentMaster = objBAL.GetDocumentResultSet();
                if (lstDocumentMaster != null && lstDocumentMaster.Count > 0)
                {
                    objResponse.ResponseReason = "To Get All Document Master";
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstDocumentMasterGETSelected = lstDocumentMaster.Select(DocMaster => new DocumentViewModel
                    {
                        MasterTransactionName = DocMaster.MasterTransactionName,
                        PageModuleName = DocMaster.PageModuleName,
                        PageModuleTabSubModuleName = DocMaster.PageModuleTabSubModuleName,
                        PageTabSectionName = DocMaster.PageTabSectionName,
                        DocumentName = DocMaster.DocumentName,
                        DocumentTypeIdName = DocMaster.DocumentTypeIdName,
                        MaxSize = DocMaster.MaxSize,
                        EndDate = DocMaster.EndDate,
                        DocumentMasterId = DocMaster.DocumentMasterId,
                        IsEditable=DocMaster.IsEditable
                    }
                    ).ToList();

                    objResponse.DocumentViewModel = lstDocumentMasterGETSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.DocumentViewModel = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "GetDocumentResultSet", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.DocumentViewModel = null;
            }

            return objResponse;
        }

        /// <summary>
        /// To get serached docment result by using filter
        /// </summary>
        /// <param name="Key"> application key</param>
        /// <param name="objDocumentMaster">document master object model</param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("Search_GetDocumentResultSet")]
        public DocumentResultSetResponse Search_GetDocumentResultSet(string Key, DocumentMaster objDocumentMaster)
        {
            LogingHelper.SaveAuditInfo(Key);

            var objResponse = new DocumentResultSetResponse();
            var objBAL = new DocumentMasterBAL();
            var objEntity = new DocumentViewModel();
            var lstDocumentMaster = new List<DocumentViewModel>();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.DocumentViewModel = null;
                    return objResponse;
                }
                if(objDocumentMaster== null)
                {
                    lstDocumentMaster = objBAL.GetDocumentResultSet();
                }
                else
                {
                    lstDocumentMaster = objBAL.Search_GetDocumentResultSet(objDocumentMaster);
                }
                
                if (lstDocumentMaster != null && lstDocumentMaster.Count > 0)
                {
                    objResponse.ResponseReason = "To Get All Document Master Result Set using Filters";
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstDocumentMasterGETSelected = lstDocumentMaster.Select(DocMaster => new DocumentViewModel
                    {
                        MasterTransactionName = DocMaster.MasterTransactionName,
                        PageModuleName = DocMaster.PageModuleName,
                        PageModuleTabSubModuleName = DocMaster.PageModuleTabSubModuleName,
                        PageTabSectionName = DocMaster.PageTabSectionName,
                        DocumentName = DocMaster.DocumentName,
                        DocumentTypeIdName = DocMaster.DocumentTypeIdName,
                        MaxSize = DocMaster.MaxSize,
                        EndDate = DocMaster.EndDate,
                        DocumentMasterId = DocMaster.DocumentMasterId,
                        IsEditable = DocMaster.IsEditable
                    }
                    ).ToList();

                    objResponse.DocumentViewModel = lstDocumentMasterGETSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.DocumentViewModel = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "Search_GetDocumentResultSet", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.DocumentViewModel = null;
            }

            return objResponse;
        }

        /// <summary>
        /// To save document master record
        /// </summary>
        /// <param name="Key"> application key</param>
        /// <param name="objDocumentMaster">document master object model</param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("DocumentMasterSave")]
        public DocumentMasterResponse DocumentMasterSave(string Key, DocumentMaster objDocumentMaster)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;
            LogingHelper.SaveAuditInfo(Key);

            var objResponse = new DocumentMasterResponse();
            var objBAL = new DocumentMasterBAL();
            var objEntity = new DocumentViewModel();
            var lstDocumentMaster = new List<DocumentMaster>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.DocumentMaster = null;
                return objResponse;
            }
            //string ValidationResponse = ConfigurationValidation.ValidateConfigurationObject(objDocumentMaster);
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
                objDocumentMaster.DocumentMasterId = objBAL.Save_DocumentMaster(objDocumentMaster);
                objResponse.Message = Messages.SaveSuccess;
                objResponse.Status = true;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                objResponse.DocumentMaster.Add(objDocumentMaster);
                objResponse.Status = true;
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "DocumentMasterSave", ENTITY.Enumeration.eSeverity.Error);
                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.DocumentMaster = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }
    }
}
