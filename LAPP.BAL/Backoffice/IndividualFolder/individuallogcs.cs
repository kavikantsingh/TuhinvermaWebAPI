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
    public class IndividualLogCS
    {
        public static IndividualCommentLogRequestResponce SaveIndividualLog(Token objToken, IndividualCommentLogRequest objRequest)
        {
            IndividualCommentLogRequestResponce objResponse = new IndividualCommentLogRequestResponce();
            IndividualCommentLogRequest objIndividualCommentLogRequest = new IndividualCommentLogRequest();
            List<IndividualCommentLogRequest> lstIndividualCommentLogRequest = new List<IndividualCommentLogRequest>();
            IndividualCommentLog objIndCommentLog = new IndividualCommentLog();
            IndividualCommentLogBAL objLogBAL = new IndividualCommentLogBAL();

            try
            {
                int individualId = objRequest.IndividualId;
                int? applicantID = objRequest.ApplicationId;
                objIndCommentLog = objLogBAL.Get_IndividualCommentLog_By_IndividualCommentLogId(objRequest.IndividualCommentLogId);
                if (objIndCommentLog != null)
                {
                    objIndCommentLog.ApplicationId = applicantID;
                    objIndCommentLog.CommentLogSource = objRequest.CommentLogSource;
                    objIndCommentLog.CommentLogText = objRequest.CommentLogText;
                    objIndCommentLog.EndDate = objRequest.EndDate;
                    objIndCommentLog.IndividualCommentLogGuid = Guid.NewGuid().ToString();
                    objIndCommentLog.IndividualId = individualId;
                    objIndCommentLog.IsActive = objRequest.IsActive;
                    objIndCommentLog.IsDeleted = objRequest.IsDeleted;
                    objIndCommentLog.IsForInvestigationOnly = false;
                    objIndCommentLog.IsForPublic = false;
                    objIndCommentLog.IsInternalOnly = true;
                    objIndCommentLog.MasterTransactionId = objRequest.MasterTransactionId;
                    objIndCommentLog.PageModuleId = objRequest.PageModuleId;
                    objIndCommentLog.PageModuleTabSubModuleId = objRequest.PageModuleTabSubModuleId;
                    objIndCommentLog.PageTabSectionId = objRequest.PageTabSectionId;
                    objIndCommentLog.ReferenceNumber = objRequest.ReferenceNumber;
                    objIndCommentLog.Type = "L";

                    objLogBAL.Save_IndividualCommentLog(objIndCommentLog);


                    ////SAVE LOG

                    //string logText = "Individual Log updated successfully. Updated on " + DateTime.Now.ToShortDateString();
                    //string logSource = eCommentLogSource.WSAPI.ToString();
                    //LogHelper.SaveIndividualLog(individualId, applicantID, logSource, logText, objToken.UserId, null, null, null);

                    ////END SAVE LOG

                    objResponse.Message = MessagesClass.UpdateSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }
                else
                {
                    objIndCommentLog = new IndividualCommentLog();

                    objIndCommentLog.ApplicationId = applicantID;
                    objIndCommentLog.CommentLogDate = DateTime.Now;
                    objIndCommentLog.CommentLogSource = objRequest.CommentLogSource;
                    objIndCommentLog.CommentLogText = objRequest.CommentLogText;
                    objIndCommentLog.CreatedBy = objToken.UserId;
                    objIndCommentLog.CreatedOn = DateTime.Now;
                    objIndCommentLog.EffectiveDate = DateTime.Now;
                    objIndCommentLog.EndDate = null;
                    objIndCommentLog.IndividualCommentLogGuid = Guid.NewGuid().ToString();
                    objIndCommentLog.ReferenceNumber = objRequest.ReferenceNumber;
                    objIndCommentLog.IndividualId = individualId;
                    objIndCommentLog.IsActive = objRequest.IsActive;
                    objIndCommentLog.IsDeleted = objRequest.IsDeleted;
                    objIndCommentLog.IsForInvestigationOnly = false;
                    objIndCommentLog.IsForPublic = false;
                    objIndCommentLog.IsInternalOnly = true;
                    objIndCommentLog.MasterTransactionId = null;
                    objIndCommentLog.PageModuleId = objRequest.PageModuleId;
                    objIndCommentLog.PageModuleTabSubModuleId = objRequest.PageModuleTabSubModuleId;
                    objIndCommentLog.PageTabSectionId = objRequest.PageTabSectionId;
                    objIndCommentLog.Type = "L";

                    objIndCommentLog.IndividualCommentLogId = objLogBAL.Save_IndividualCommentLog(objIndCommentLog);

                    ////SAVE LOG

                    //string logText = "Individual Log saved successfully. Saved on " + DateTime.Now.ToShortDateString();
                    //string logSource = eCommentLogSource.WSAPI.ToString();
                    //LogHelper.SaveIndividualLog(individualId, applicantID, logSource, logText, objToken.UserId, null, null, null);

                    ////END SAVE LOG

                    objResponse.Message = MessagesClass.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }

                lstIndividualCommentLogRequest.Add(objIndCommentLog);
                objResponse.Status = true;
                objResponse.IndividualCommentLogRequest = lstIndividualCommentLogRequest;
            }
            catch (Exception ex)
            {

                LogingHelper.SaveExceptionInfo("", ex, "SaveIndividualLog", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualCommentLogRequest = null;
            }
            return objResponse;
        }
    }
}
