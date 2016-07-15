using LAPP.BAL.ValidateClass;
using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.GlobalFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LAPP.WS.ValidateController.Common
{
    public class PaymentValidation
    {
        public static PaymentResponse ValidateRequest(PaymentRequest objPaymentRequest)
        {
            PaymentResponse objResponse = new PaymentResponse();
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            objResponseList = Validations.IsValidCreditCardNumber(nameof(objPaymentRequest.CardNumber), objPaymentRequest.CardNumber, objResponseList);
            objResponseList = Validations.IsValidIntDecimalProperty(nameof(objPaymentRequest.CVV), objPaymentRequest.CVV.ToString(), objResponseList);
            objResponseList = Validations.IsValidIntDecimalProperty(nameof(objPaymentRequest.ExpirationMonths), objPaymentRequest.ExpirationMonths.ToString(), objResponseList);
            objResponseList = Validations.IsValidIntDecimalProperty(nameof(objPaymentRequest.ExpirationYears), objPaymentRequest.ExpirationYears.ToString(), objResponseList);


            objResponseList = Validations.IsRequiredProperty(nameof(objPaymentRequest.FirstName), objPaymentRequest.FirstName.ToString(), objResponseList);
            objResponseList = Validations.IsRequiredProperty(nameof(objPaymentRequest.LastName), objPaymentRequest.LastName.ToString(), objResponseList);
            objResponseList = Validations.IsRequiredProperty(nameof(objPaymentRequest.Address), objPaymentRequest.Address.ToString(), objResponseList);
            objResponseList = Validations.IsValidEmailProperty(nameof(objPaymentRequest.EmailAddress), objPaymentRequest.EmailAddress.ToString(), objResponseList);
            objResponseList = Validations.IsRequiredProperty(nameof(objPaymentRequest.City), objPaymentRequest.City.ToString(), objResponseList);
            objResponseList = Validations.IsRequiredProperty(nameof(objPaymentRequest.StateCode), objPaymentRequest.StateCode.ToString(), objResponseList, 2);
            objResponseList = Validations.IsValidUSZIPProperty(nameof(objPaymentRequest.Zip), objPaymentRequest.Zip.ToString(), objResponseList);

            if (objResponseList.Count > 0)
            {
                objResponse.Message = "Validation Error";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                objResponse.ResponseReason = GeneralFunctions.GetJsonStringFromList(objResponseList);
                return objResponse;
            }
            else
            {
                return null;
            }
        }

        public static ManualPaymentResponse ValidateProcessManualPayment(ManualPaymentRequest objManPayReq)
        {
            ManualPaymentResponse objResponse = new ManualPaymentResponse();
            List<ResponseReason> objResponseList = new List<ResponseReason>();

            objResponseList = Validations.IsRequiredPropertyMaxLength(nameof(objManPayReq.ApplicationNumber), objManPayReq.ApplicationNumber, objResponseList, 50);
            objResponseList = Validations.IsIntGreaterThanZero(nameof(objManPayReq.ApplicationId), objManPayReq.ApplicationId, objResponseList);
            objResponseList = Validations.IsIntGreaterThanZero(nameof(objManPayReq.IndividualId), objManPayReq.IndividualId, objResponseList);
            objResponseList = Validations.IsRequiredPropertyMaxLength(nameof(objManPayReq.PaymentMode), objManPayReq.PaymentMode, objResponseList, 2);
            //objResponseList = Validations.IsRequiredPropertyMaxLength(nameof(objManPayReq.Description), objManPayReq.Description, objResponseList, 200);
            //objResponseList = Validations.IsRequiredPropertyMaxLength(nameof(objManPayReq.InvoiceNumber), objManPayReq.InvoiceNumber, objResponseList, 40);
            objResponseList = Validations.IsValidIntDecimalGraterThenZero(nameof(objManPayReq.Amount), objManPayReq.Amount.ToString(), objResponseList);

            if (objResponseList.Count > 0)
            {
                objResponse.Message = "Validation Error";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                objResponse.ResponseReason = GeneralFunctions.GetJsonStringFromList(objResponseList);
                return objResponse;
            }
            else
            {
                return null;
            }
        }

    }
}