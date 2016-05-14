using LAPP.ENTITY;
using LAPP.GlobalFunctions;
using LAPP.WS.App_Helper;
using LAPP.WS.App_Helper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LAPP.WS.ValidateController.Common
{
    public class MessageValidation
    {


        public static string ValidateMessageObject(MessagePost objMessage)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            objResponseList = Validations.IsValidIntProperty(nameof(objMessage.MessageTypeId), objMessage.MessageTypeId.ToString(), objResponseList);

            objResponseList = Validations.IsRequiredPropertyMaxLength(nameof(objMessage.MessageCode), objMessage.MessageCode, objResponseList, 10);

            objResponseList = Validations.IsValidbool(nameof(objMessage.IsActive), objMessage.IsActive.ToString(), objResponseList);
            objResponseList = Validations.IsValidbool(nameof(objMessage.IsDeleted), objMessage.IsDeleted.ToString(), objResponseList);


            if (objResponseList.Count() > 0)
            {
                return GeneralFunctions.GetJsonStringFromList(objResponseList);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ValidateMessageTypeObject(MessagesTypePost objMessageType)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            objResponseList = Validations.IsValidIntProperty(nameof(objMessageType.MessageTypeId), objMessageType.MessageTypeId.ToString(), objResponseList);
            objResponseList = Validations.IsRequiredProperty(nameof(objMessageType.MessageTypeCode), objMessageType.MessageTypeCode, objResponseList, 75);

            objResponseList = Validations.IsValidbool(nameof(objMessageType.IsActive), objMessageType.IsActive.ToString(), objResponseList);
            objResponseList = Validations.IsValidbool(nameof(objMessageType.IsDeleted), objMessageType.IsDeleted.ToString(), objResponseList);


            if (objResponseList.Count() > 0)
            {
                return GeneralFunctions.GetJsonStringFromList(objResponseList);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}