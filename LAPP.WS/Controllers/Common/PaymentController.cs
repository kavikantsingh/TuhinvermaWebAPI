using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.WS.App_Helper;
using LAPP.WS.App_Helper.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text.RegularExpressions;
using LAPP.WS.ValidateController.Common;
using System.Transactions;
using LAPP.LOGING;
using System.Web;

namespace LAPP.WS.Controllers.Common
{
    public class PaymentController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="objPaymentRequest"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("ProcessPayment")]
        public PaymentResponse ProcessPayment(string Key, PaymentRequest objPaymentRequest)
        {

            LogingHelper.SaveAuditInfo(Key);

            

            PaymentResponse objResponse = new PaymentResponse();
            PaymentAuthResponse objAuthorization = new PaymentAuthResponse();

            try
            {

                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.PaymentAuthResponse = null;
                    return objResponse;
                }


                #region Validation
                PaymentResponse objValidationResponse = new PaymentResponse();
                objValidationResponse = PaymentValidation.ValidateRequest(objPaymentRequest);
                if (objValidationResponse != null)
                {
                    return objValidationResponse;
                }

                #endregion

                try
                {
                    if (System.Web.HttpContext.Current.IsDebuggingEnabled)
                    {
                        // this is executed only in the debug version
                        string requestStr = Newtonsoft.Json.JsonConvert.SerializeObject(objPaymentRequest);
                        LogingHelper.SaveRequestJson(requestStr, "Process Payment");
                    }
                   
                }
                catch(Exception ex)
                {
                    LogingHelper.SaveExceptionInfo(Key, ex, "ProcessPayment object serialization", ENTITY.Enumeration.eSeverity.Critical);
                }


                //using (TransactionScope transScope = new TransactionScope(new TransactionScopeOption {ti))
                //{
                Token objToken = TokenHelper.GetTokenByKey(Key);

                objResponse = AuthorizeDotNetPayment.ProcessPayment(objPaymentRequest, objToken.UserId, objToken);
                // transScope.Complete();
                //}

                return objResponse;



            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ProcessPayment", ENTITY.Enumeration.eSeverity.Critical);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.PaymentAuthResponse = null;

            }
            return objResponse;


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="objInitiatePaymentRequest"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("InitiatePayment")]
        public LAPP.ENTITY.TransactionResponse InitiatePayment(string Key, InitiatePaymentRequest objInitiatePaymentRequest)
        {

            LogingHelper.SaveAuditInfo(Key);

            TransactionResponse objResponse = new TransactionResponse();
            PaymentAuthResponse objAuthorization = new PaymentAuthResponse();

            try
            {

                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.Transaction = null;
                    return objResponse;
                }

                try
                {
                    if (System.Web.HttpContext.Current.IsDebuggingEnabled)
                    {
                        // this is executed only in the debug version
                        string requestStr = Newtonsoft.Json.JsonConvert.SerializeObject(objInitiatePaymentRequest);
                        LogingHelper.SaveRequestJson(requestStr, "Initiate Payment");
                    }

                }
                catch (Exception ex)
                {
                    LogingHelper.SaveRequestJson(ex.Message, " error in Initiate Payment request");
                }

               
                Token objToken = TokenHelper.GetTokenByKey(Key);

                LAPP.ENTITY.Transaction objTransaction = LAPP.BAL.Payment.InitiatePayment.InitiatePaymentTransaction(objInitiatePaymentRequest, objToken.UserId);

                if (objTransaction != null)
                {
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Message = "Payment Transaction Initiated.";
                    objResponse.Transaction = Newtonsoft.Json.JsonConvert.DeserializeObject<TrasactionRes>(Newtonsoft.Json.JsonConvert.SerializeObject(objTransaction));
                    return objResponse;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.TransactionFailed).ToString("00");
                    objResponse.Message = "Payment Transaction Inititiation failed.";
                    objResponse.Transaction = null;
                    return objResponse;
                }


            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "InitiatePayment", ENTITY.Enumeration.eSeverity.Critical);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Transaction = null;

            }
            return objResponse;


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("MonthListGet")]
        public ListItemResponse MonthListGet(string Key)
        {
            //Audit Request
            LogingHelper.SaveAuditInfo(Key);

            ListItemResponse objResponse = new ListItemResponse();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ListItems = null;
                    return objResponse;
                }

                List<ListItems> objItems = new List<ListItems>();
                for (int i = 1; i < 13; i++)
                {
                    ListItems items = new ListItems();
                    items.Text = CultureInfo.CurrentUICulture.DateTimeFormat.MonthNames[i-1];
                    items.Value = i.ToString();
                    objItems.Add(items);
                }
                objResponse.Status = true;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                objResponse.Message = "";
                objResponse.ListItems = objItems;
                return objResponse;

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "GetAllCountry", ENTITY.Enumeration.eSeverity.Error);
                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.ListItems = null;

            }
            return objResponse;


        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("YearListGet")]
        public ListItemResponse YearListGet(string Key)
        {
            //Audit Request
            LogingHelper.SaveAuditInfo(Key);

            ListItemResponse objResponse = new ListItemResponse();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ListItems = null;
                    return objResponse;
                }

                List<ListItems> objItems = new List<ListItems>();
                int Year = DateTime.Now.Year - 1;

                for (int i = 1; i <= 20; i++)
                {


                    ListItems items = new ListItems();
                    items.Text = (Year + i).ToString();
                    items.Value = ((Year + i).ToString()).GetLast(2);
                    objItems.Add(items);
                }
                objResponse.Status = true;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                objResponse.Message = "";
                objResponse.ListItems = objItems;
                return objResponse;

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "GetAllCountry", ENTITY.Enumeration.eSeverity.Error);
                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.ListItems = null;

            }
            return objResponse;


        }
    }

}
/// <summary>
/// 
/// </summary>
public static class StringExtension
{
    public static string GetLast(this string source, int tail_length)
    {
        if (tail_length >= source.Length)
            return source;
        return source.Substring(source.Length - tail_length);
    }
}