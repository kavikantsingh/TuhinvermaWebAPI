using LAPP.BAL;
using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.WS.App_Helper;
using LAPP.WS.App_Helper.Common;
using LAPP.WS.ValidateController.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LAPP.WS.Controllers.Common
{
    public class MessageController : ApiController
    {
        #region Messages

        /// <summary>
        /// Looks up all data by Key For Message.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("GET")]
        [ActionName("MessageGetAll")]
        public MessageResponse MessageGetAll(string Key)
        {

            LogingHelper.SaveAuditInfo(Key);

            MessageResponse objResponse = new MessageResponse();
            MessageBAL objBAL = new MessageBAL();
            Message objEntity = new Message();
            List<Message> lstMessage = new List<Message>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.MessageGetList = null;
                    return objResponse;

                }

                lstMessage = objBAL.Get_All_Message();
                if (lstMessage != null && lstMessage.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    List<MessageGet> lstMessageSelected = lstMessage.Select(Message => new MessageGet
                    {
                        MessageId = Message.MessageId,
                        MessageTypeId = Message.MessageTypeId,
                        MessageTypeName = Message.MessageTypeName,
                        MessageCode = Message.MessageCode,
                        MessageDesc = Message.MessageDesc,
                        LabelName = Message.LabelName,
                        IsEnabled = Message.IsEnabled,
                        IsActive = Message.IsActive
                    }).ToList();




                    objResponse.MessageGetList = lstMessageSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.MessageGetList = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "MessageGetAll", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.MessageGetList = null;
            }
            return objResponse;


        }

        /// <summary>
        /// Save or Update the data For Message
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objMessage">Object of Message</param>
        [AcceptVerbs("POST")]
        [ActionName("MessageSave")]
        public MessagePostResponse MessageSave(string Key, MessagePost objMessage)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo(Key);

            MessagePostResponse objResponse = new MessagePostResponse();
            MessageBAL objBAL = new MessageBAL();
            Message objEntity = new Message();
            List<MessagePost> lstEntity = new List<MessagePost>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.Message = null;
                return objResponse;
            }

            string ValidationResponse = MessageValidation.ValidateMessageObject(objMessage);

            if (!string.IsNullOrEmpty(ValidationResponse))
            {
                objResponse.Message = "Validation Error";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                objResponse.ResponseReason = ValidationResponse;
                return objResponse;
            }

            try
            {
                if (objMessage.MessageId > 0)
                {
                    objEntity = objBAL.Get_Message_byMessageId(objMessage.MessageId);
                    if (objEntity != null)
                    {
                        objEntity.MessageTypeId = objMessage.MessageTypeId;
                        objEntity.MessageCode = objMessage.MessageCode;
                        objEntity.MessageDesc = objMessage.MessageDesc;
                        objEntity.LabelName = objMessage.LabelName;
                        objEntity.IsEnabled = objMessage.IsEnabled;
                        objEntity.IsActive = objMessage.IsActive;
                        objEntity.IsDeleted = objMessage.IsDeleted;

                        objEntity.ModifiedOn = DateTime.Now;
                        objEntity.ModifiedBy = CreatedOrMoifiy;
                        objBAL.Save_Message(objEntity);

                        objResponse.Message = Messages.UpdateSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }

                }
                else
                {
                    objEntity = new Message();

                    objEntity.MessageTypeId = objMessage.MessageTypeId;
                    objEntity.MessageCode = objMessage.MessageCode;
                    objEntity.MessageDesc = objMessage.MessageDesc;
                    objEntity.LabelName = objMessage.LabelName;
                    objEntity.IsEnabled = objMessage.IsEnabled;
                    objEntity.IsActive = objMessage.IsActive;
                    objEntity.IsDeleted = objMessage.IsDeleted;

                    objEntity.CreatedBy = CreatedOrMoifiy;
                    objEntity.CreatedOn = DateTime.Now;
                    objEntity.CreatedBy = CreatedOrMoifiy;
                    objEntity.MessageId = objBAL.Save_Message(objEntity);

                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    objMessage.MessageId = objEntity.MessageId;
                }

                lstEntity.Add(objMessage);
                objResponse.Status = true;

                objResponse.MessagesPost = lstEntity;

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "MessageSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.MessagesPost = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }


        #endregion

        #region MessagesTypes

        /// <summary>
        /// Looks up all data by Key For MessagesType.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("GET")]
        [ActionName("MessagesTypeGetAll")]
        public MessagesTypeResponse MessagesTypeGetAll(string Key)
        {

            LogingHelper.SaveAuditInfo(Key);

            MessagesTypeResponse objResponse = new MessagesTypeResponse();
            MessagesTypeBAL objBAL = new MessagesTypeBAL();
            MessagesType objEntity = new MessagesType();
            List<MessagesType> lstMessagesType = new List<MessagesType>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.MessagesTypeList = null;
                    return objResponse;

                }

                lstMessagesType = objBAL.Get_All_MessagesType();
                if (lstMessagesType != null && lstMessagesType.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    List<MessagesTypeGET> lstMessagesTypeSelected = lstMessagesType.Select(MessagesType => new MessagesTypeGET
                    {
                        MessageTypeId = MessagesType.MessageTypeId,
                        MessageTypeCode = MessagesType.MessageTypeCode,
                        IsActive = MessagesType.IsActive

                    }).ToList();

                    objResponse.MessagesTypeList = lstMessagesTypeSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.MessagesTypeList = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "MessagesTypeGetAll", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.MessagesTypeList = null;
            }
            return objResponse;


        }

        /// <summary>
        /// Save or Update the data For MessagesType
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objMessagesType">Object of MessagesType</param>
        [AcceptVerbs("POST")]
        [ActionName("MessagesTypeSave")]
        public MessagesTypePostResponse MessagesTypeSave(string Key, MessagesTypePost objMessagesType)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo(Key);

            MessagesTypePostResponse objResponse = new MessagesTypePostResponse();
            MessagesTypeBAL objBAL = new MessagesTypeBAL();
            MessagesType objEntity = new MessagesType();
            List<MessagesTypePost> lstEntity = new List<MessagesTypePost>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.MessagesTypePost = null;
                return objResponse;
            }

            string ValidationResponse = MessageValidation.ValidateMessageTypeObject(objMessagesType);

            if (!string.IsNullOrEmpty(ValidationResponse))
            {
                objResponse.Message = "Validation Error";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                objResponse.ResponseReason = ValidationResponse;
                return objResponse;
            }

            try
            {
                if (objMessagesType.MessageTypeId > 0)
                {
                    objEntity = objBAL.Get_MessagesType_byMessagesTypeId(objMessagesType.MessageTypeId);
                    if (objEntity != null)
                    {
                        objEntity.MessageTypeId = objMessagesType.MessageTypeId;
                        objEntity.MessageTypeCode = objMessagesType.MessageTypeCode;
                        objEntity.IsActive = objMessagesType.IsActive;
                        objEntity.IsDeleted = objMessagesType.IsDeleted;

                        objEntity.ModifiedOn = DateTime.Now;
                        objEntity.ModifiedBy = CreatedOrMoifiy;
                        objBAL.Save_MessagesType(objEntity);

                        objResponse.Message = Messages.UpdateSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }
                    else
                    {
                        objEntity = new MessagesType();

                        objEntity.MessageTypeId = objMessagesType.MessageTypeId;
                        objEntity.MessageTypeCode = objMessagesType.MessageTypeCode;
                        objEntity.IsActive = objMessagesType.IsActive;
                        objEntity.IsDeleted = objMessagesType.IsDeleted;

                        objEntity.CreatedBy = CreatedOrMoifiy;
                        objEntity.CreatedOn = DateTime.Now;
                        objEntity.MessageTypeId = objBAL.Save_MessagesType(objEntity);

                        objResponse.Message = Messages.SaveSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                        objMessagesType.MessageTypeId = objEntity.MessageTypeId;
                    }

                }
                else
                {
                    objEntity = new MessagesType();

                    objEntity.MessageTypeId = objMessagesType.MessageTypeId;
                    objEntity.MessageTypeCode = objMessagesType.MessageTypeCode;
                    objEntity.IsActive = objMessagesType.IsActive;
                    objEntity.IsDeleted = objMessagesType.IsDeleted;

                    objEntity.CreatedBy = CreatedOrMoifiy;
                    objEntity.CreatedOn = DateTime.Now;
                    objEntity.CreatedBy = CreatedOrMoifiy;
                    objEntity.MessageTypeId = objBAL.Save_MessagesType(objEntity);

                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    objMessagesType.MessageTypeId = objEntity.MessageTypeId;
                }

                lstEntity.Add(objMessagesType);
                objResponse.Status = true;

                objResponse.MessagesTypePost = lstEntity;

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "MessagesTypeSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.MessagesTypePost = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }


        #endregion
    }
}
