using LAPP.ENTITY;
using LAPP.GlobalFunctions;
using LAPP.WS.App_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LAPP.WS.ValidateController.Backoffice
{
    public class IndividualValidations
    {
        public static string ValidateIndividualAddressObject(IndividualAddressResponse objIndiAddr)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            //objResponseList = Validations.IsRequiredProperty(nameof(objUsers.UserName), objUsers.UserName, objResponseList, 128);

            objResponseList = Validations.IsRequiredPropertyMaxLength(nameof(objIndiAddr.StreetLine1), objIndiAddr.StreetLine1, objResponseList, 128);
            objResponseList = Validations.IsRequiredPropertyMaxLength(nameof(objIndiAddr.City), objIndiAddr.City, objResponseList, 128);
            objResponseList = Validations.IsRequiredPropertyMaxLength(nameof(objIndiAddr.StateCode), objIndiAddr.StateCode, objResponseList, 2);
            objResponseList = Validations.IsValidUSZIPProperty(nameof(objIndiAddr.Zip), objIndiAddr.Zip, objResponseList, 15);


            if (objResponseList.Count() > 0)
            {
                return GeneralFunctions.GetJsonStringFromList(objResponseList);
            }
            else
            {
                return string.Empty;
            }
        }



        #region Individual Document
        public static string ValidateIndividualDocument(List<DocumentToUpload> lstDocumentToUpload, int IndividualId)
        {
            List<ResponseReason> objResponseList = new List<ResponseReason>();
            objResponseList = Validations.IsIntGreaterThanZero(nameof(IndividualId), IndividualId, objResponseList);

            if (lstDocumentToUpload != null && lstDocumentToUpload.Count > 0)
            {
                foreach (DocumentToUpload objResponse in lstDocumentToUpload)
                {

                    objResponseList = Validations.IsRequiredPropertyMaxLength(nameof(objResponse.DocumentLkToPageTabSectionId), objResponse.DocumentLkToPageTabSectionId.ToString(), objResponseList, 11);
                    objResponseList = Validations.IsRequiredPropertyMaxLength(nameof(objResponse.DocumentLkToPageTabSectionCode), objResponse.DocumentLkToPageTabSectionCode, objResponseList, 10);

                    objResponseList = Validations.IsRequiredPropertyMaxLength(nameof(objResponse.DocumentTypeName), objResponse.DocumentTypeName, objResponseList, 100);
                }
            }

            if (objResponseList.Count() > 0)
            {
                return GeneralFunctions.GetJsonStringFromList(objResponseList);
            }
            else
            {
                return string.Empty;
            }
        }


        #endregion

    }
}