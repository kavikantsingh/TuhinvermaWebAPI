using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LAPP.ENTITY.Enumeration;
using System.Net.Mail;
using System.Web;
using LAPP.WS.App_Helper.Common;
using LAPP.GlobalFunctions;
using System.IO;
using LAPP.BAL.ValidateClass;
using LAPP.BAL;

using System.Web.Http.Description;
using LAPP.LOGING;

namespace LAPP.WS.Controllers.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class CommonController : ApiController
    {
        /// <summary>
        /// Get Server date and time
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("ServerDateTimeGet")]
        public DateTime ServerDateTimeGet()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// Get Server date and time
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("ReloadConfiguration")]
        public BaseEntityServiceResponse ReloadConfiguration()
        {
            BaseEntityServiceResponse objResponse = new BaseEntityServiceResponse();

            try
            {
                List<Configuration> lstConfiguration = new List<ENTITY.Configuration>();
                ConfigurationBAL objConfigBAL = new ConfigurationBAL();
                lstConfiguration = objConfigBAL.GetALL_Configuration_WithConfigurationType();
                HttpContext.Current.Application["lstConfiguration"] = lstConfiguration;

                objResponse.Message = "Configuration loaded successfully.";
                objResponse.Status = true;
                objResponse.StatusCode = ((int)ResponseStatusCode.Success).ToString("00");

                 
            }
            catch (Exception ex)
            {
                objResponse.Message = ex.Message;
                objResponse.Status = true;
                objResponse.StatusCode = ((int)ResponseStatusCode.Exception).ToString("00");

            }
            return objResponse;
        }
        //
        /// <summary>
        /// Send Payment Email with Attachment
        /// </summary>
        /// <param name="Key">The Key of the data.</param> 
        /// <param name="objEmailRequest"></param>
        /// <returns></returns>
        /// 

        [HttpPost]
        [ActionName("RenewalEmailWithAttachment")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public BaseEntityServiceResponse RenewalEmailWithAttachment(string Key, RenewalEmailWithAttachment objEmailRequest)
        {


            BaseEntityServiceResponse objResponse = new BaseEntityServiceResponse();
            List<Attachment> lstAttachment = new List<Attachment>();
            string FilePath = ConfigurationHelper.GetConfigurationValueBySetting("RootDocumentPath") + "Renewal\\";
            string PaymentReceiptFileName;
            string LicenseOutputFileName;
            string RenewalApplicationFileName;
            IndividualDocumentBAL objIndividualDocumentBAL = new IndividualDocumentBAL();
            IndividualDocument objIndividualDocument = new IndividualDocument();

            //if (!TokenHelper.ValidateToken(Key))
            //{
            //    objResponse.Status = false;
            //    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
            //    objResponse.Message = "User session has expired.";

            //    return objResponse;
            //}

            string msg = "";
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);

            }


            List<ResponseReason> ObjReasonList = new List<ResponseReason>();
            ObjReasonList = Validations.IsValidEmailProperty(nameof(objEmailRequest.Email), objEmailRequest.Email, ObjReasonList);

            if (!string.IsNullOrEmpty(objEmailRequest.PaymentReceiptBase64))
            {
                try
                {
                    PaymentReceiptFileName = Guid.NewGuid().ToString() + objEmailRequest.PaymentReceiptName; // Guid.NewGuid().ToString() + ".pdf";
                    FileHelper.Base64ToFile(objEmailRequest.PaymentReceiptBase64, FilePath + PaymentReceiptFileName);
                    Attachment item = new Attachment(FilePath + PaymentReceiptFileName);
                    lstAttachment.Add(item);

                    objIndividualDocument = new IndividualDocument();

                    objIndividualDocument.IndividualId = objEmailRequest.IndividualId;
                    objIndividualDocument.ApplicationId = objEmailRequest.ApplicationId;
                    objIndividualDocument.DocumentLkToPageTabSectionId = 1;
                    objIndividualDocument.DocumentLkToPageTabSectionCode = "T";
                    objIndividualDocument.DocumentTypeName = objEmailRequest.PaymentReceiptName;
                    objIndividualDocument.EffectiveDate = DateTime.Now;
                    objIndividualDocument.EndDate = null;
                    objIndividualDocument.IsDocumentUploadedbyIndividual = true;
                    objIndividualDocument.IsDocumentUploadedbyStaff = false;
                    objIndividualDocument.ReferenceNumber = "";
                    objIndividualDocument.IsActive = true;
                    objIndividualDocument.IsDeleted = false;
                    objIndividualDocument.CreatedBy = 1;
                    objIndividualDocument.CreatedOn = DateTime.Now;
                    objIndividualDocument.ModifiedOn = null;
                    objIndividualDocument.ModifiedBy = null;
                    objIndividualDocument.IndividualDocumentGuid = Guid.NewGuid().ToString();

                    if (objIndividualDocument != null)
                    {
                        //objIndividualDocumentBAL.Save_IndividualDocument(objIndividualDocument);
                    }

                }
                catch (Exception ex)
                {

                    ResponseReason objValidationResponse = new ResponseReason();
                    objValidationResponse.Code = "FI";
                    objValidationResponse.PropertyName = nameof(objEmailRequest.PaymentReceiptBase64);
                    objValidationResponse.Message = "Invalid Base64 string supplied.";

                    ObjReasonList.Add(objValidationResponse);

                }
            }

            if (!string.IsNullOrEmpty(objEmailRequest.LicenseOutputBase64))
            {
                try
                {
                    LicenseOutputFileName = Guid.NewGuid().ToString() + objEmailRequest.LicenseOutputName; //  Guid.NewGuid().ToString() + ".pdf";
                    FileHelper.Base64ToFile(objEmailRequest.LicenseOutputBase64, FilePath + LicenseOutputFileName);
                    Attachment item = new Attachment(FilePath + LicenseOutputFileName);
                    lstAttachment.Add(item);


                    objIndividualDocument = new IndividualDocument();

                    objIndividualDocument.IndividualId = objEmailRequest.IndividualId;
                    objIndividualDocument.ApplicationId = objEmailRequest.ApplicationId;
                    objIndividualDocument.DocumentLkToPageTabSectionId = 1;
                    objIndividualDocument.DocumentLkToPageTabSectionCode = "T";
                    objIndividualDocument.DocumentTypeName = objEmailRequest.LicenseOutputName;
                    objIndividualDocument.EffectiveDate = DateTime.Now;
                    objIndividualDocument.EndDate = null;
                    objIndividualDocument.IsDocumentUploadedbyIndividual = true;
                    objIndividualDocument.IsDocumentUploadedbyStaff = false;
                    objIndividualDocument.ReferenceNumber = "";
                    objIndividualDocument.IsActive = true;
                    objIndividualDocument.IsDeleted = false;
                    objIndividualDocument.CreatedBy = 1;
                    objIndividualDocument.CreatedOn = DateTime.Now;
                    objIndividualDocument.ModifiedOn = null;
                    objIndividualDocument.ModifiedBy = null;
                    objIndividualDocument.IndividualDocumentGuid = Guid.NewGuid().ToString();

                    if (objIndividualDocument != null)
                    {
                        //objIndividualDocumentBAL.Save_IndividualDocument(objIndividualDocument);
                    }

                }
                catch (Exception ex)
                {
                    ResponseReason objValidationResponse = new ResponseReason();
                    objValidationResponse.Code = "FI";
                    objValidationResponse.PropertyName = nameof(objEmailRequest.LicenseOutputBase64);
                    objValidationResponse.Message = "Invalid Base64 string supplied.";

                    ObjReasonList.Add(objValidationResponse);
                }
            }

            if (!string.IsNullOrEmpty(objEmailRequest.RenewalApplicationBase64))
            {
                try
                {
                    RenewalApplicationFileName = Guid.NewGuid().ToString() + objEmailRequest.RenewalApplitionName; // + ".pdf";
                    FileHelper.Base64ToFile(objEmailRequest.RenewalApplicationBase64, FilePath + RenewalApplicationFileName);
                    Attachment item = new Attachment(FilePath + RenewalApplicationFileName);
                    lstAttachment.Add(item);

                    objIndividualDocument = new IndividualDocument();

                    objIndividualDocument.IndividualId = objEmailRequest.IndividualId;
                    objIndividualDocument.ApplicationId = objEmailRequest.ApplicationId;
                    objIndividualDocument.DocumentLkToPageTabSectionId = 1;
                    objIndividualDocument.DocumentLkToPageTabSectionCode = "T";
                    objIndividualDocument.DocumentTypeName = objEmailRequest.RenewalApplitionName;
                    objIndividualDocument.EffectiveDate = DateTime.Now;
                    objIndividualDocument.EndDate = null;
                    objIndividualDocument.IsDocumentUploadedbyIndividual = true;
                    objIndividualDocument.IsDocumentUploadedbyStaff = false;
                    objIndividualDocument.ReferenceNumber = "";
                    objIndividualDocument.IsActive = true;
                    objIndividualDocument.IsDeleted = false;
                    objIndividualDocument.CreatedBy = 1;
                    objIndividualDocument.CreatedOn = DateTime.Now;
                    objIndividualDocument.ModifiedOn = null;
                    objIndividualDocument.ModifiedBy = null;
                    objIndividualDocument.IndividualDocumentGuid = Guid.NewGuid().ToString();

                    if (objIndividualDocument != null)
                    {
                        //objIndividualDocumentBAL.Save_IndividualDocument(objIndividualDocument);
                    }

                }
                catch (Exception ex)
                {

                    ResponseReason objValidationResponse = new ResponseReason();
                    objValidationResponse.Code = "FI";
                    objValidationResponse.PropertyName = nameof(objEmailRequest.RenewalApplicationBase64);
                    objValidationResponse.Message = "Invalid Base64 string supplied.";

                    ObjReasonList.Add(objValidationResponse);
                }
            }



            if (!string.IsNullOrEmpty(objEmailRequest.Email) && lstAttachment.Count > 0)
            {

                if (EmailHelper.SendMailWithMultipleAttachment(objEmailRequest.Email, "Renewal Confirmation", "Renewal Attachment", true, lstAttachment))
                {
                    objResponse.Message = "Success";
                    objResponse.ResponseReason = "";
                    objResponse.Status = true;
                    objResponse.StatusCode = ((int)ResponseStatusCode.Success).ToString("00");


                }
                else
                {
                    objResponse.Message = "Email not sent.";
                    objResponse.ResponseReason = "";
                    objResponse.Status = false;
                    objResponse.StatusCode = ((int)ResponseStatusCode.Exception).ToString("00");

                }

            }


            if (ObjReasonList != null && ObjReasonList.Count > 0)
            {
                objResponse.Message = "Validation error";
                objResponse.ResponseReason = GlobalFunctions.GeneralFunctions.GetJsonStringFromList(ObjReasonList);
                objResponse.Status = false;
                objResponse.StatusCode = ((int)ResponseStatusCode.Validation).ToString("00");
                return objResponse;

            }


            return objResponse;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="objEmailRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("EmailWithAttachment")]
        public BaseEntityServiceResponse EmailWithAttachment(string Key, EmailWithAttachment objEmailRequest)
        {
            BaseEntityServiceResponse objResponse = new BaseEntityServiceResponse();
            List<Attachment> lstAttachment = new List<Attachment>();
            string FilePath = ConfigurationHelper.GetConfigurationValueBySetting("RootDocumentPath") + "Renewal\\";
            string DocumentFileName;

            IndividualDocumentBAL objIndividualDocumentBAL = new IndividualDocumentBAL();
            IndividualDocument objIndividualDocument = new IndividualDocument();


            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);

            }
            if (objEmailRequest == null)
            {
                objResponse.Message = "Invalid Object.";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                objResponse.ResponseReason = "";
                return objResponse;

            }

            List<ResponseReason> ObjReasonList = new List<ResponseReason>();
            ObjReasonList = Validations.IsValidEmailProperty(nameof(objEmailRequest.Email), objEmailRequest.Email, ObjReasonList);

            List<EmailAttachment> lstEmailAttachment = objEmailRequest.objAttachmentList;
            if (lstEmailAttachment != null && lstEmailAttachment.Count > 0)
            {

                foreach (EmailAttachment objEmailAttachment in lstEmailAttachment)
                {
                    if (!string.IsNullOrEmpty(objEmailAttachment.DocumentStringBase64))
                    {
                        try
                        {
                            DocumentFileName = Guid.NewGuid().ToString() + objEmailAttachment.DocumentName;
                            FileHelper.Base64ToFile(objEmailAttachment.DocumentStringBase64, FilePath + DocumentFileName);
                            Attachment item = new Attachment(FilePath + DocumentFileName);
                            lstAttachment.Add(item);

                            objIndividualDocument = new IndividualDocument();

                            objIndividualDocument.IndividualId = objEmailRequest.IndividualId;
                            objIndividualDocument.ApplicationId = objEmailRequest.ApplicationId;
                            objIndividualDocument.DocumentLkToPageTabSectionId = 1;
                            objIndividualDocument.DocumentLkToPageTabSectionCode = "T";
                            objIndividualDocument.DocumentTypeName = objEmailAttachment.DocumentName;
                            objIndividualDocument.EffectiveDate = DateTime.Now;
                            objIndividualDocument.EndDate = null;
                            objIndividualDocument.IsDocumentUploadedbyIndividual = true;
                            objIndividualDocument.IsDocumentUploadedbyStaff = false;
                            objIndividualDocument.ReferenceNumber = "";
                            objIndividualDocument.IsActive = true;
                            objIndividualDocument.IsDeleted = false;
                            objIndividualDocument.CreatedBy = 1;
                            objIndividualDocument.CreatedOn = DateTime.Now;
                            objIndividualDocument.ModifiedOn = null;
                            objIndividualDocument.ModifiedBy = null;
                            objIndividualDocument.IndividualDocumentGuid = Guid.NewGuid().ToString();

                            if (objIndividualDocument != null)
                            {
                                // objIndividualDocumentBAL.Save_IndividualDocument(objIndividualDocument);
                            }

                        }
                        catch (Exception ex)
                        {
                            LogingHelper.SaveExceptionInfo(Key, ex, "EmailWithAttachment - " + objEmailAttachment.DocumentName, eSeverity.Critical);
                            //ResponseReason objValidationResponse = new ResponseReason();
                            //objValidationResponse.Code = "FI";
                            //objValidationResponse.PropertyName = nameof(objEmailAttachment.DocumentStringBase64);
                            //objValidationResponse.Message = "Invalid Base64 string supplied.";

                            //ObjReasonList.Add(objValidationResponse);

                        }
                    }
                }
            }


            if (!string.IsNullOrEmpty(objEmailRequest.Email))
            {

                if (EmailHelper.SendMailWithMultipleAttachment(objEmailRequest.Email, objEmailRequest.Subject, "Renewal Attachment", true, lstAttachment))
                {
                    objResponse.Message = "Success";
                    objResponse.ResponseReason = "";
                    objResponse.Status = true;
                    objResponse.StatusCode = ((int)ResponseStatusCode.Success).ToString("00");


                }
                else
                {
                    objResponse.Message = "Email not sent.";
                    objResponse.ResponseReason = "";
                    objResponse.Status = false;
                    objResponse.StatusCode = ((int)ResponseStatusCode.Exception).ToString("00");

                }

            }


            if (ObjReasonList != null && ObjReasonList.Count > 0)
            {
                objResponse.Message = "Validation error";
                objResponse.ResponseReason = GlobalFunctions.GeneralFunctions.GetJsonStringFromList(ObjReasonList);
                objResponse.Status = false;
                objResponse.StatusCode = ((int)ResponseStatusCode.Validation).ToString("00");
                return objResponse;

            }


            return objResponse;
        }
    }
}
