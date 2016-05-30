using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.LOGING;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.BAL.Backoffice.IndividualFolder
{
    public class IndividualCorrespondenceCS
    {
        public static IndividualCommunicationLogRequestResponce SaveIndividualCorrespondence(Token objToken, IndividualCommunicationLogRequest objCommunicationLog)
        {
            IndividualCommunicationLogRequestResponce objResponse = new IndividualCommunicationLogRequestResponce();
            IndividualCommunicationLogRequest objIndividualCommunicationLogRequest = new IndividualCommunicationLogRequest();

            List<IndividualCommunicationLogRequest> lstIndividualCommunicationLogRequest = new List<IndividualCommunicationLogRequest>();
            List<IndividualCommunicationLog> lstIndividualCommunicationLog = new List<IndividualCommunicationLog>();
            IndividualCommunicationLog objIndCommunicationLog = new IndividualCommunicationLog();
            IndividualCommunicationLogBAL objLogBAL = new IndividualCommunicationLogBAL();

            try
            {
                int individualId = objCommunicationLog.IndividualId;
                int? ApplicationId = objCommunicationLog.ApplicationId;
                objIndCommunicationLog = objLogBAL.Get_IndividualCommunicationLog_By_IndividualCommunicationLogId(objCommunicationLog.IndividualCommunicationLogId);
                if (objIndCommunicationLog != null && objIndCommunicationLog.IndividualCommunicationLogId > 0)
                {
                    objIndCommunicationLog.ApplicationId = ApplicationId;
                    objIndCommunicationLog.CommunicationLogDate = objCommunicationLog.CommunicationLogDate;
                    objIndCommunicationLog.CommunicationSource = objCommunicationLog.CommunicationSource;
                    objIndCommunicationLog.CommunicationText = objCommunicationLog.CommunicationText;
                    objIndCommunicationLog.EffectiveDate = objCommunicationLog.EffectiveDate;
                    objIndCommunicationLog.EndDate = objCommunicationLog.EndDate;
                    objIndCommunicationLog.IndividualCommunicationLogId = objCommunicationLog.IndividualCommunicationLogId;
                    objIndCommunicationLog.IndividualId = individualId;
                    objIndCommunicationLog.IsActive = objCommunicationLog.IsActive;
                    objIndCommunicationLog.IsDeleted = objCommunicationLog.IsDeleted;
                    objIndCommunicationLog.IsForInvestigationOnly = objCommunicationLog.IsForInvestigationOnly;
                    objIndCommunicationLog.IsForPublic = objCommunicationLog.IsForPublic;
                    objIndCommunicationLog.IsInternalOnly = objCommunicationLog.IsInternalOnly;
                    objIndCommunicationLog.MasterTransactionId = objCommunicationLog.MasterTransactionId;
                    objIndCommunicationLog.PageModuleId = objCommunicationLog.PageModuleId;
                    objIndCommunicationLog.PageModuleTabSubModuleId = objCommunicationLog.PageModuleTabSubModuleId;
                    objIndCommunicationLog.PageTabSectionId = objCommunicationLog.PageTabSectionId;
                    objIndCommunicationLog.Type = objCommunicationLog.Type;
                    objIndCommunicationLog.Subject = objCommunicationLog.Subject;
                    objIndCommunicationLog.EmailFrom = objCommunicationLog.EmailFrom;
                    objIndCommunicationLog.CommunicationStatus = objCommunicationLog.CommunicationStatus;
                    objIndCommunicationLog.UserIdFrom = objCommunicationLog.UserIdFrom;

                    objIndCommunicationLog.IndividualCommunicationLogId = objLogBAL.Save_IndividualCommunicationLog(objIndCommunicationLog);

                    //SAVE LOG

                    string logText = "Individual Correspondence updated successfully. Updated on " + DateTime.Now.ToShortDateString();
                    string logSource = eCommentLogSource.WSAPI.ToString();
                    LogHelper.SaveIndividualLog(individualId, ApplicationId, logSource, logText, objToken.UserId, null, null, null);

                    //END SAVE LOG

                    objResponse.Message = MessagesClass.UpdateSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }
                else
                {

                    objIndCommunicationLog = new IndividualCommunicationLog();

                    objIndCommunicationLog.ApplicationId = ApplicationId;
                    objIndCommunicationLog.CommunicationLogDate = objCommunicationLog.CommunicationLogDate;
                    objIndCommunicationLog.CommunicationSource = objCommunicationLog.CommunicationSource;
                    objIndCommunicationLog.CommunicationText = objCommunicationLog.CommunicationText;
                    objIndCommunicationLog.CreatedBy = objToken.UserId;
                    objIndCommunicationLog.CreatedOn = DateTime.Now;
                    objIndCommunicationLog.EffectiveDate = objCommunicationLog.EffectiveDate;
                    objIndCommunicationLog.EndDate = objCommunicationLog.EndDate;
                    objIndCommunicationLog.IndividualCommunicationLogGuid = Guid.NewGuid().ToString();
                    objIndCommunicationLog.IndividualCommunicationLogId = objCommunicationLog.IndividualCommunicationLogId;
                    objIndCommunicationLog.IndividualId = individualId;
                    objIndCommunicationLog.IsActive = objCommunicationLog.IsActive;
                    objIndCommunicationLog.IsDeleted = objCommunicationLog.IsDeleted;
                    objIndCommunicationLog.IsForInvestigationOnly = objCommunicationLog.IsForInvestigationOnly;
                    objIndCommunicationLog.IsForPublic = objCommunicationLog.IsForPublic;
                    objIndCommunicationLog.IsInternalOnly = objCommunicationLog.IsInternalOnly;
                    objIndCommunicationLog.MasterTransactionId = objCommunicationLog.MasterTransactionId;
                    objIndCommunicationLog.PageModuleId = objCommunicationLog.PageModuleId;
                    objIndCommunicationLog.PageModuleTabSubModuleId = objCommunicationLog.PageModuleTabSubModuleId;
                    objIndCommunicationLog.PageTabSectionId = objCommunicationLog.PageTabSectionId;
                    objIndCommunicationLog.Type = objCommunicationLog.Type;
                    objIndCommunicationLog.Subject = objCommunicationLog.Subject;
                    objIndCommunicationLog.EmailFrom = objCommunicationLog.EmailFrom;
                    objIndCommunicationLog.CommunicationStatus = objCommunicationLog.CommunicationStatus;
                    objIndCommunicationLog.UserIdFrom = objCommunicationLog.UserIdFrom;

                    objIndCommunicationLog.IndividualCommunicationLogId = objLogBAL.Save_IndividualCommunicationLog(objIndCommunicationLog);


                    /// save IndividualCommunicationToLog

                    if (objIndCommunicationLog.IndividualCommunicationLogId > 0 && !string.IsNullOrEmpty(objCommunicationLog.EmailTo))
                    {

                        IndividualCommunicationToLog objToLog = new IndividualCommunicationToLog();
                        IndividualCommunicationToLogBAL objToLogBAL = new IndividualCommunicationToLogBAL();
                        objToLog.ApplicationId = ApplicationId;
                        objToLog.IndividualId = individualId;
                        objToLog.EmailTo = objCommunicationLog.EmailTo;
                        objToLog.IndividualCommunicationLogId = objIndCommunicationLog.IndividualCommunicationLogId;
                        objToLog.IndividualCommunicationToLogGuid = Guid.NewGuid().ToString();
                        objToLog.UserIdTo = objCommunicationLog.UserIdTo;

                        objToLogBAL.Save_IndividualCommunicationToLog(objToLog);
                    }

                    //SAVE LOG

                    string logText = "Individual Correspondence saved successfully. Saved on " + DateTime.Now.ToShortDateString();
                    string logSource = eCommentLogSource.WSAPI.ToString();
                    LogHelper.SaveIndividualLog(individualId, ApplicationId, logSource, logText, objToken.UserId, null, null, null);

                    //END SAVE LOG

                    objResponse.Message = MessagesClass.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }

                lstIndividualCommunicationLogRequest.Add(objIndCommunicationLog);
                objResponse.Status = true;
                objResponse.IndividualCommunicationLogRequest = lstIndividualCommunicationLogRequest;
            }
            catch (Exception ex)
            {

                LogingHelper.SaveExceptionInfo("", ex, "SaveIndividualCorrespondence", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualCommunicationLogRequest = null;
            }
            return objResponse;
        }
    }
}
