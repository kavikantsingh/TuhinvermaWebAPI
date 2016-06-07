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
    public class IndividualLegalCS
    {
        public static IndividualLegalRequestResponce SaveIndividualLegal(Token objToken, IndividualLegalResponse objRequest)
        {
            IndividualLegalRequestResponce objResponse = new IndividualLegalRequestResponce();
            IndividualLegalResponse objIndividualLegalRequest = new IndividualLegalResponse();
            List<IndividualLegalResponse> lstIndividualLegalRequest = new List<IndividualLegalResponse>();
            IndividualLegal objIndCommentLog = new IndividualLegal();
            IndividualLegalBAL objLogBAL = new IndividualLegalBAL();

            try
            {
                int individualId = objRequest.IndividualId;
                int? applicantID = null;
                objIndCommentLog = objLogBAL.Get_address_By_IndividualLegalId(objRequest.IndividualLegalId);
                if (objIndCommentLog != null)
                {
                    objIndCommentLog.IndividualId = individualId;
                    objIndCommentLog.ContentItemLkId = objRequest.ContentItemLkId;
                    objIndCommentLog.ContentItemNumber = objRequest.ContentItemNumber;
                    objIndCommentLog.ContentItemResponse = objRequest.ContentItemResponse;
                    objIndCommentLog.Desc = objRequest.Desc;
                    objIndCommentLog.ContentDescription = objRequest.ContentDescription;
                    objIndCommentLog.IsActive = objRequest.IsActive;
                    objIndCommentLog.IsDeleted = objRequest.IsDeleted;
                    objIndCommentLog.ModifiedBy = objToken.UserId;
                    objIndCommentLog.ModifiedOn = DateTime.Now;

                    objLogBAL.Save_IndividualLegal(objIndCommentLog);


                    //SAVE LOG

                    string logText = "Individual Legal updated successfully. Updated on " + DateTime.Now.ToShortDateString();
                    string logSource = eCommentLogSource.WSAPI.ToString();
                    LogHelper.SaveIndividualLog(individualId, applicantID, logSource, logText, objToken.UserId, null, null, null);

                    //END SAVE LOG

                    objResponse.Message = MessagesClass.UpdateSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }
                else
                {
                    objIndCommentLog = new IndividualLegal();

                    objIndCommentLog.IndividualId = individualId;
                    objIndCommentLog.ContentItemLkId = objRequest.ContentItemLkId;
                    objIndCommentLog.ContentItemNumber = objRequest.ContentItemNumber;
                    objIndCommentLog.ContentItemResponse = objRequest.ContentItemResponse;
                    objIndCommentLog.Desc = objRequest.Desc;
                    objIndCommentLog.ContentDescription = objRequest.ContentDescription;
                    objIndCommentLog.IsActive = objRequest.IsActive;
                    objIndCommentLog.ModifiedBy = null;
                    objIndCommentLog.ModifiedOn = null;
                    objIndCommentLog.CreatedBy = objToken.UserId;
                    objIndCommentLog.CreatedOn = DateTime.Now;
                    objIndCommentLog.IndividualLegalGuid = Guid.NewGuid().ToString();
                    objIndCommentLog.IsActive = objRequest.IsActive;
                    objIndCommentLog.IsDeleted = objRequest.IsDeleted;

                    objIndCommentLog.IndividualLegalId = objLogBAL.Save_IndividualLegal(objIndCommentLog);

                    //SAVE LOG

                    string logText = "Individual Legal saved successfully. Saved on " + DateTime.Now.ToShortDateString();
                    string logSource = eCommentLogSource.WSAPI.ToString();
                    LogHelper.SaveIndividualLog(individualId, applicantID, logSource, logText, objToken.UserId, null, null, null);

                    //END SAVE LOG

                    objResponse.Message = MessagesClass.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }

                lstIndividualLegalRequest.Add(objIndCommentLog);
                objResponse.Status = true;
                objResponse.IndividualLegalResponse = lstIndividualLegalRequest;
            }
            catch (Exception ex)
            {

                LogingHelper.SaveExceptionInfo("", ex, "SaveIndividualLegal", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualLegalResponse = null;
            }
            return objResponse;
        }
    }
}
