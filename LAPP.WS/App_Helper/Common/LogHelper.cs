using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LAPP.ENTITY;
using LAPP.BAL;
using LAPP.ENTITY.Enumeration;

namespace LAPP.WS.App_Helper.Common
{
    public class LogHelper
    {

        public static void LogComment(int IndividualId, int? ApplicationId, eCommentType Type, string LogSource, string LogText, int CreatedBy, int? PageModuleId = null, int? PageModuleTabSubModuleId = null, int? PageTabSectionId = null)
        {
            try
            {

                IndividualCommentLog objIndCommentLog = new IndividualCommentLog();
                IndividualCommentLogBAL objLogBAL = new IndividualCommentLogBAL();
                objIndCommentLog = new IndividualCommentLog();
                objIndCommentLog.ApplicationId = ApplicationId;
                objIndCommentLog.CommentLogDate = DateTime.Now;
                objIndCommentLog.CommentLogSource = LogSource;
                objIndCommentLog.CommentLogText = LogText;
                objIndCommentLog.CreatedBy = CreatedBy;
                objIndCommentLog.CreatedOn = DateTime.Now;
                objIndCommentLog.EffectiveDate = DateTime.Now;
                objIndCommentLog.EndDate = null;
                objIndCommentLog.IndividualCommentLogGuid = Guid.NewGuid().ToString();


                objIndCommentLog.IndividualId = IndividualId;
                objIndCommentLog.IsActive = true;
                objIndCommentLog.IsDeleted = false;
                objIndCommentLog.IsForInvestigationOnly = false;
                objIndCommentLog.IsForPublic = false;

                objIndCommentLog.IsInternalOnly = true;
                objIndCommentLog.MasterTransactionId = null;
                objIndCommentLog.PageModuleId = PageModuleId;
                objIndCommentLog.PageModuleTabSubModuleId = PageModuleTabSubModuleId;
                objIndCommentLog.PageTabSectionId = PageTabSectionId;
                objIndCommentLog.Type = ((char)Type).ToString();

                objLogBAL.Save_IndividualCommentLog(objIndCommentLog);
            }
            catch (Exception ex)
            {
                LOGING.LogingHelper.SaveExceptionInfo("", ex, "LogHelper - LogComment", ENTITY.Enumeration.eSeverity.Error);
            }

        }

        public static void LogCommunication(int IndividualId, int? ApplicationId, eCommunicationType Type, string Subject, eCommunicationStatus Status, string LogSource, string LogText, string EmailFrom, string EmailTo, int? UserIdFrom, int? UserIdTo, int CreatedBy, int? PageModuleId = null, int? PageModuleTabSubModuleId = null, int? PageTabSectionId = null)
        {
            try
            {

                IndividualCommunicationLog objIndCommunicationLog = new IndividualCommunicationLog();
                IndividualCommunicationLogBAL objLogBAL = new IndividualCommunicationLogBAL();
                objIndCommunicationLog = new IndividualCommunicationLog();
                objIndCommunicationLog.ApplicationId = ApplicationId;
                objIndCommunicationLog.CommunicationLogDate = DateTime.Now;
                objIndCommunicationLog.CommunicationSource = LogSource;
                objIndCommunicationLog.CommunicationText = LogText;
                objIndCommunicationLog.CreatedBy = CreatedBy;
                objIndCommunicationLog.CreatedOn = DateTime.Now;
                objIndCommunicationLog.EffectiveDate = DateTime.Now;
                objIndCommunicationLog.EndDate = null;
                objIndCommunicationLog.IndividualCommunicationLogGuid = Guid.NewGuid().ToString();


                objIndCommunicationLog.IndividualId = IndividualId;
                objIndCommunicationLog.IsActive = true;
                objIndCommunicationLog.IsDeleted = false;
                objIndCommunicationLog.IsForInvestigationOnly = false;
                objIndCommunicationLog.IsForPublic = false;

                objIndCommunicationLog.IsInternalOnly = true;
                objIndCommunicationLog.MasterTransactionId = null;
                objIndCommunicationLog.PageModuleId = PageModuleId;
                objIndCommunicationLog.PageModuleTabSubModuleId = PageModuleTabSubModuleId;
                objIndCommunicationLog.PageTabSectionId = PageTabSectionId;
                objIndCommunicationLog.Type = ((char)Type).ToString();

                objIndCommunicationLog.Subject = Subject;
                objIndCommunicationLog.EmailFrom = EmailFrom;
                objIndCommunicationLog.CommunicationStatus = ((char)Status).ToString();
                objIndCommunicationLog.UserIdFrom = UserIdFrom;


                objIndCommunicationLog.IndividualCommunicationLogId = objLogBAL.Save_IndividualCommunicationLog(objIndCommunicationLog);

                if (objIndCommunicationLog.IndividualCommunicationLogId > 0 && !string.IsNullOrEmpty(EmailTo))
                {

                    IndividualCommunicationToLog objToLog = new IndividualCommunicationToLog();
                    IndividualCommunicationToLogBAL objToLogBAL = new IndividualCommunicationToLogBAL();
                    objToLog.ApplicationId = ApplicationId;
                    objToLog.IndividualId = IndividualId;

                    objToLog.EmailTo = EmailTo;
                    objToLog.IndividualCommunicationLogId = objIndCommunicationLog.IndividualCommunicationLogId;
                    objToLog.IndividualCommunicationToLogGuid = Guid.NewGuid().ToString();
                    objToLog.UserIdTo = UserIdTo;

                    objToLogBAL.Save_IndividualCommunicationToLog(objToLog);
                }
            }
            catch (Exception ex)
            {
                LOGING.LogingHelper.SaveExceptionInfo("", ex, "LogHelper - LogCommunication", ENTITY.Enumeration.eSeverity.Error);
            }

        }
    }
}