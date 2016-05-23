using LAPP.BAL;
using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.LOGING;
using LAPP.WS.App_Helper;
using LAPP.WS.App_Helper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LAPP.WS.Controllers.Common
{
    public class LogController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="objCommentLog"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("CommentLogSave")]
        public BaseEntityServiceResponse CommentLogSave(string Key, IndividualCommentLogRequest objCommentLog)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo(Key);
            BaseEntityServiceResponse objResponse = new BaseEntityServiceResponse();


            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.Message = null;
                return objResponse;
            }



            try
            {
                IndividualCommentLog objIndCommentLog = new IndividualCommentLog();
                  IndividualCommentLogBAL objLogBAL = new IndividualCommentLogBAL();
                if(objCommentLog.IndividualCommentLogId > 0)
                {
                    objIndCommentLog.ApplicationId = objCommentLog.ApplicationId;
                    objIndCommentLog.CommentLogDate = objCommentLog.CommentLogDate;
                    objIndCommentLog.CommentLogSource = objCommentLog.CommentLogSource;
                    objIndCommentLog.CommentLogText = objCommentLog.CommentLogText;
                
                    objIndCommentLog.EffectiveDate = objCommentLog.EffectiveDate;
                    objIndCommentLog.EndDate = objCommentLog.EndDate;
                   
                    objIndCommentLog.IndividualCommentLogId = objCommentLog.IndividualCommentLogId;

                    objIndCommentLog.IndividualId = objCommentLog.IndividualId;
                    objIndCommentLog.IsActive = objCommentLog.IsActive;
                    objIndCommentLog.IsDeleted = objCommentLog.IsDeleted;
                    objIndCommentLog.IsForInvestigationOnly = objCommentLog.IsForInvestigationOnly;
                    objIndCommentLog.IsForPublic = objCommentLog.IsForPublic;

                    objIndCommentLog.IsInternalOnly = objCommentLog.IsInternalOnly;
                    objIndCommentLog.MasterTransactionId = objCommentLog.MasterTransactionId;
                    objIndCommentLog.PageModuleId = objCommentLog.PageModuleId;
                    objIndCommentLog.PageModuleTabSubModuleId = objCommentLog.PageModuleTabSubModuleId;
                    objIndCommentLog.PageTabSectionId = objCommentLog.PageTabSectionId;
                    objIndCommentLog.Type = objCommentLog.Type;

                    objLogBAL.Save_IndividualCommentLog(objIndCommentLog);

                }
                else
                {
                    objIndCommentLog = new IndividualCommentLog();
                    objIndCommentLog.ApplicationId = objCommentLog.ApplicationId;
                    objIndCommentLog.CommentLogDate = objCommentLog.CommentLogDate;
                    objIndCommentLog.CommentLogSource = objCommentLog.CommentLogSource;
                    objIndCommentLog.CommentLogText = objCommentLog.CommentLogText;
                    objIndCommentLog.CreatedBy = CreatedOrMoifiy;
                    objIndCommentLog.CreatedOn = DateTime.Now;
                    objIndCommentLog.EffectiveDate = objCommentLog.EffectiveDate;
                    objIndCommentLog.EndDate = objCommentLog.EndDate;
                    objIndCommentLog.IndividualCommentLogGuid = Guid.NewGuid().ToString();
                    objIndCommentLog.IndividualCommentLogId = objCommentLog.IndividualCommentLogId;

                    objIndCommentLog.IndividualId = objCommentLog.IndividualId;
                    objIndCommentLog.IsActive = objCommentLog.IsActive;
                    objIndCommentLog.IsDeleted = objCommentLog.IsDeleted;
                    objIndCommentLog.IsForInvestigationOnly = objCommentLog.IsForInvestigationOnly;
                    objIndCommentLog.IsForPublic = objCommentLog.IsForPublic;

                    objIndCommentLog.IsInternalOnly = objCommentLog.IsInternalOnly;
                    objIndCommentLog.MasterTransactionId = objCommentLog.MasterTransactionId;
                    objIndCommentLog.PageModuleId = objCommentLog.PageModuleId;
                    objIndCommentLog.PageModuleTabSubModuleId = objCommentLog.PageModuleTabSubModuleId;
                    objIndCommentLog.PageTabSectionId = objCommentLog.PageTabSectionId;
                    objIndCommentLog.Type = objCommentLog.Type;

                    objLogBAL.Save_IndividualCommentLog(objIndCommentLog);

                }

                objResponse.Status = true;
                objResponse.Message = Messages.SaveSuccess;
                objResponse.ResponseReason = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "MessageSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.ResponseReason = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="objCommunicationLog"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("CommunicationLogSave")]
        public BaseEntityServiceResponse CommunicationLogSave(string Key, IndividualCommunicationLogRequest objCommunicationLog)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo(Key);
            BaseEntityServiceResponse objResponse = new BaseEntityServiceResponse();


            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.Message = null;
                return objResponse;
            }



            try
            {
                IndividualCommunicationLog objIndCommunicationLog = new IndividualCommunicationLog();
                IndividualCommunicationLogBAL objLogBAL = new IndividualCommunicationLogBAL();
                if (objCommunicationLog.IndividualCommunicationLogId > 0)
                {
                    objIndCommunicationLog.ApplicationId = objCommunicationLog.ApplicationId;
                    objIndCommunicationLog.CommunicationLogDate = objCommunicationLog.CommunicationLogDate;
                    objIndCommunicationLog.CommunicationSource = objCommunicationLog.CommunicationSource;
                    objIndCommunicationLog.CommunicationText = objCommunicationLog.CommunicationText;

                    objIndCommunicationLog.EffectiveDate = objCommunicationLog.EffectiveDate;
                    objIndCommunicationLog.EndDate = objCommunicationLog.EndDate;

                    objIndCommunicationLog.IndividualCommunicationLogId = objCommunicationLog.IndividualCommunicationLogId;

                    objIndCommunicationLog.IndividualId = objCommunicationLog.IndividualId;
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

                  objIndCommunicationLog.IndividualCommunicationLogId=   objLogBAL.Save_IndividualCommunicationLog(objIndCommunicationLog);

                }
                else
                {
                    objIndCommunicationLog = new IndividualCommunicationLog();
                    objIndCommunicationLog.ApplicationId = objCommunicationLog.ApplicationId;
                    objIndCommunicationLog.CommunicationLogDate = objCommunicationLog.CommunicationLogDate;
                    objIndCommunicationLog.CommunicationSource = objCommunicationLog.CommunicationSource;
                    objIndCommunicationLog.CommunicationText = objCommunicationLog.CommunicationText;
                    objIndCommunicationLog.CreatedBy = CreatedOrMoifiy;
                    objIndCommunicationLog.CreatedOn = DateTime.Now;
                    objIndCommunicationLog.EffectiveDate = objCommunicationLog.EffectiveDate;
                    objIndCommunicationLog.EndDate = objCommunicationLog.EndDate;
                    objIndCommunicationLog.IndividualCommunicationLogGuid = Guid.NewGuid().ToString();
                    objIndCommunicationLog.IndividualCommunicationLogId = objCommunicationLog.IndividualCommunicationLogId;

                    objIndCommunicationLog.IndividualId = objCommunicationLog.IndividualId;
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
                    objIndCommunicationLog.IndividualCommunicationLogId=   objLogBAL.Save_IndividualCommunicationLog(objIndCommunicationLog);

                }
                if( objIndCommunicationLog.IndividualCommunicationLogId > 0 && !string.IsNullOrEmpty(objCommunicationLog.EmailTo ))
                {

                    IndividualCommunicationToLog objToLog = new IndividualCommunicationToLog();
                    IndividualCommunicationToLogBAL objToLogBAL = new IndividualCommunicationToLogBAL();
                    objToLog.ApplicationId = objCommunicationLog.ApplicationId;
                    objToLog.IndividualId = objCommunicationLog.IndividualId;
                    objToLog.EmailTo = objCommunicationLog.EmailTo;
                    objToLog.IndividualCommunicationLogId = objIndCommunicationLog.IndividualCommunicationLogId;
                    objToLog.IndividualCommunicationToLogGuid = Guid.NewGuid().ToString();
                    objToLog.UserIdTo = objCommunicationLog.UserIdTo;

                    objToLogBAL.Save_IndividualCommunicationToLog(objToLog);
                }

                objResponse.Status = true;
                objResponse.Message = Messages.SaveSuccess;
                objResponse.ResponseReason = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "MessageSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.ResponseReason = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }
    }
}
