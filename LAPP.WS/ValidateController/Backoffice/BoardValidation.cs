using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LAPP.ENTITY;
using LAPP.WS.App_Helper;
using LAPP.WS.App_Helper.Common;
using LAPP.GlobalFunctions;

namespace LAPP.WS.ValidateController.Backoffice
{
    public class BoardValidation
    {
        public static string ValidateBoardAuthorityObject(BoardAuthorityRequest objBoardAuthority)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            objResponseList = Validations.IsRequiredProperty(nameof(objBoardAuthority.Name), objBoardAuthority.Name, objResponseList);

            objResponseList = Validations.IsRequiredProperty(nameof(objBoardAuthority.StateCode), objBoardAuthority.StateCode, objResponseList);

            if (!string.IsNullOrEmpty(objBoardAuthority.Url))
            {
                objResponseList = Validations.IsValidUrlProperty(nameof(objBoardAuthority.Url), objBoardAuthority.Url, objResponseList);
            }

            objResponseList = Validations.IsRequiredProperty(nameof(objBoardAuthority.PhysicalAddressLine1), objBoardAuthority.PhysicalAddressLine1, objResponseList);
            objResponseList = Validations.IsRequiredProperty(nameof(objBoardAuthority.PhysicalAddressLine2), objBoardAuthority.PhysicalAddressLine2, objResponseList);
            objResponseList = Validations.IsRequiredProperty(nameof(objBoardAuthority.PhysicalAddressCity), objBoardAuthority.PhysicalAddressCity, objResponseList);
            objResponseList = Validations.IsRequiredProperty(nameof(objBoardAuthority.PhysicalAddressState), objBoardAuthority.PhysicalAddressState, objResponseList);

            objResponseList = Validations.IsValidUSZIPProperty(nameof(objBoardAuthority.PhysicalAddressZip), objBoardAuthority.PhysicalAddressZip, objResponseList);

            if (!string.IsNullOrEmpty(objBoardAuthority.MailingAddressZip))
            {
                objResponseList = Validations.IsValidUSZIPProperty(nameof(objBoardAuthority.MailingAddressZip), objBoardAuthority.MailingAddressZip, objResponseList);
            }
            if (!string.IsNullOrEmpty(objBoardAuthority.ContactPhone))
            {
                objResponseList = Validations.IsValidUSPhoneNoProperty(nameof(objBoardAuthority.ContactPhone), objBoardAuthority.ContactPhone, objResponseList);
            }

            if (!string.IsNullOrEmpty(objBoardAuthority.ContactEmail))
            {
                objResponseList = Validations.IsValidEmailProperty(nameof(objBoardAuthority.ContactEmail), objBoardAuthority.ContactEmail, objResponseList);
            }
            if (!string.IsNullOrEmpty(objBoardAuthority.AlternatePhone))
            {
                objResponseList = Validations.IsValidUSPhoneNoProperty(nameof(objBoardAuthority.AlternatePhone), objBoardAuthority.AlternatePhone, objResponseList);
            }

            if (!string.IsNullOrEmpty(objBoardAuthority.ContactFax))
            {
                objResponseList = Validations.IsValidUSPhoneNoProperty(nameof(objBoardAuthority.ContactFax), objBoardAuthority.ContactFax, objResponseList);
            }
            if (!string.IsNullOrEmpty(objBoardAuthority.SystemUrl))
            {
                objResponseList = Validations.IsValidUrlProperty(nameof(objBoardAuthority.SystemUrl), objBoardAuthority.SystemUrl, objResponseList);
            }
            if (!string.IsNullOrEmpty(objBoardAuthority.ApplicationSystemUrl))
            {
                objResponseList = Validations.IsValidUrlProperty(nameof(objBoardAuthority.ApplicationSystemUrl), objBoardAuthority.ApplicationSystemUrl, objResponseList);
            }


           // objResponseList = Validations.IsValidDateMMDDYYYYProperty(nameof(objBoardAuthority.CreatedOn), Convert.ToDateTime(objBoardAuthority.CreatedOn).ToShortDateString(), objResponseList, 10);

           // objResponseList = Validations.IsValidIntProperty(nameof(objBoardAuthority.CreatedBy), objBoardAuthority.CreatedBy.ToString(), objResponseList, 10);


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