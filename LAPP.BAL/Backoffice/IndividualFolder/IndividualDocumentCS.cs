using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.GlobalFunctions;
using LAPP.LOGING;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LAPP.BAL.Backoffice.IndividualFolder
{
    public class IndividualDocumentCS
    {

        private static IndividualDocumentUploadResponse SaveIndividualDocument(Token objToken, IndividualDocumentUpload objIndividualDocumentResponse)
        {
            IndividualDocumentUploadResponse objResponse = new IndividualDocumentUploadResponse();
            IndividualDocumentBAL objIndividualDocumentBAL = new IndividualDocumentBAL();
            IndividualDocument objIndividualDocument = new IndividualDocument();
            string FilePath = ConfigurationHelper.GetConfigurationValueBySetting("RootDocumentPath") + "Renewal\\";
            int ErrNo = 0;

            int CreatedOrMoifiy = objToken.UserId;

            List<string> SaveErrorList = new List<string>();

            List<IndividualDocumentUpload> lstIndividualDocumentUpload = new List<IndividualDocumentUpload>();
            IndividualDocumentUpload objIndividualDocumentUpload = new IndividualDocumentUpload();
            List<DocumentToUpload> lstDocumentToUpload = new List<DocumentToUpload>();
            List<DocumentToUpload> lstDocumentToUploadNEW = new List<DocumentToUpload>();
            lstDocumentToUpload = objIndividualDocumentResponse.DocumentUploadList;

            try
            {

                int IndividualId = objIndividualDocumentResponse.IndividualId;
                int? ApplicationId = objIndividualDocumentResponse.ApplicationId;

                if (lstDocumentToUpload != null && lstDocumentToUpload.Count > 0)
                {
                    foreach (DocumentToUpload objDtU in lstDocumentToUpload)
                    {
                        try
                        {
                            string DocFileName = Guid.NewGuid().ToString() + objDtU.DocNameWithExtention; // Guid.NewGuid().ToString() + ".pdf";
                            string DocPath = FileHelper.Base64ToFile(objDtU.DocStrBase64, FilePath + DocFileName); // (FilePath + DocFileName);

                            objIndividualDocument = new IndividualDocument();

                            objIndividualDocument.IndividualId = IndividualId;
                            objIndividualDocument.ApplicationId = ApplicationId;
                            objIndividualDocument.DocumentLkToPageTabSectionId = objDtU.DocumentLkToPageTabSectionId;
                            objIndividualDocument.DocumentLkToPageTabSectionCode = objDtU.DocumentLkToPageTabSectionCode;

                            objIndividualDocument.DocumentTypeName = objDtU.DocumentTypeName;
                            objIndividualDocument.DocumentPath = DocPath;
                            objIndividualDocument.EffectiveDate = objDtU.EffectiveDate;
                            objIndividualDocument.EndDate = objDtU.EndDate;
                            objIndividualDocument.IsDocumentUploadedbyIndividual = objDtU.IsDocumentUploadedbyIndividual;
                            objIndividualDocument.IsDocumentUploadedbyStaff = objDtU.IsDocumentUploadedbyStaff;
                            objIndividualDocument.ReferenceNumber = objDtU.ReferenceNumber;
                            objIndividualDocument.IsActive = true;
                            objIndividualDocument.IsDeleted = false;
                            objIndividualDocument.CreatedBy = CreatedOrMoifiy;
                            objIndividualDocument.CreatedOn = DateTime.Now;
                            objIndividualDocument.ModifiedOn = null;
                            objIndividualDocument.ModifiedBy = null;
                            objIndividualDocument.IndividualDocumentGuid = Guid.NewGuid().ToString();

                            if (objIndividualDocument != null)
                            {
                                objIndividualDocument.IndividualDocumentId = objIndividualDocumentBAL.Save_IndividualDocument(objIndividualDocument);
                                objDtU.IndividualDocumentId = objIndividualDocument.IndividualDocumentId;

                                // objIndividualDocumentUpload = new IndividualDocumentUpload();

                                lstDocumentToUploadNEW.Add(objDtU);

                            }

                        }
                        catch (Exception ex)
                        {
                            LogingHelper.SaveExceptionInfo("", ex, "IndividualDocumentSaveForeach", ENTITY.Enumeration.eSeverity.Error);
                            string ErrMes = ex.Message;
                            ErrNo = ErrNo + 1;
                            SaveErrorList.Add(ErrMes);
                        }
                    }
                    // If error occurred
                    if (SaveErrorList.Count > 0)
                    {
                        objResponse.Status = false;
                        if (ErrNo > 0 && ErrNo != lstDocumentToUpload.Count)
                        {
                            objResponse.Message = "Saved with error.";
                            objIndividualDocumentUpload.DocumentUploadList = lstDocumentToUploadNEW;

                        }
                        else
                        {
                            objResponse.Message = "Error occurred while saving.";
                            objIndividualDocumentUpload.DocumentUploadList = null;
                        }
                        objIndividualDocumentUpload.ApplicationId = ApplicationId;
                        objIndividualDocumentUpload.IndividualId = IndividualId;


                        lstIndividualDocumentUpload.Add(objIndividualDocumentUpload);

                        objResponse.IndividualDocumentUploadList = lstIndividualDocumentUpload;
                        objResponse.ResponseReason = GeneralFunctions.GetJsonStringFromList(SaveErrorList);
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                        return objResponse;
                    }

                    //Success

                    else
                    {
                        objResponse.Message = MessagesClass.SaveSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }
                }
                else
                {
                    objResponse.Message = "No data to upload.";
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.IndividualDocumentUploadList = null;
                    return objResponse;
                }

                objIndividualDocumentUpload.ApplicationId = ApplicationId;
                objIndividualDocumentUpload.IndividualId = IndividualId;
                objIndividualDocumentUpload.DocumentUploadList = lstDocumentToUploadNEW;

                lstIndividualDocumentUpload.Add(objIndividualDocumentUpload);

                objResponse.Status = true;
                objResponse.IndividualDocumentUploadList = lstIndividualDocumentUpload;


            }
            catch (Exception ex)
            {

                LogingHelper.SaveExceptionInfo("", ex, "SaveIndividualName", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualDocumentUploadList = null;
            }
            return objResponse;


        }
    }
}
