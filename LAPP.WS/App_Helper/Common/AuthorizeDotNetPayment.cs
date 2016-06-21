using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.GlobalFunctions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using LAPP.BAL;
using LAPP.DAL;
using LAPP.BAL.Payment;

namespace LAPP.WS.App_Helper.Common
{
    public class AuthorizeDotNetPayment
    {

        public static PaymentResponse ProcessPayment(PaymentRequest objPaymentRequest, int CreatedBy, string AffirmativeAction, Token objToken)
        {
            if (objPaymentRequest == null)
                throw new Exception("Invalid request object. please check with API request signature.");

            if (objPaymentRequest.TransactionObject == null)
                throw new Exception("Invalid TransactionObject in payment request. please check with API request signature.");

            PaymentResponse objPaymentResponse = new PaymentResponse();
            try
            {
                #region Payment Process

                TransactionBAL objTranBal = new TransactionBAL();
                Transaction objTrans = objTranBal.Get_Transaction_By_TransactionId(objPaymentRequest.TransactionObject.TransactionId);

                if (objTrans == null)
                    throw new Exception("Invalid Transaction id sent. please send the transaction object which you received on payment initiate call.");

                ApplicationBAL objApplicationBAL = new ApplicationBAL();
                Application objApplication = objApplicationBAL.Get_Application_By_ApplicationId(objPaymentRequest.ApplicationId);

                if (objApplication == null)
                    throw new Exception("Invalid ApplicationId sent to API. ");

                if(objApplication != null && objApplication.IsPaid == true)
                    throw new Exception("There is no pending amount to process payment for this application.");

                string DescriptionText = "";
                IndividualLicenseBAL objIndLicenseBAL = new IndividualLicenseBAL();
                IndividualLicense objIndLicense = objIndLicenseBAL.Get_IndividualLicense_By_ApplicationId(objApplication.ApplicationId);
                if(objIndLicense != null)
                {
                    DescriptionText = "License number - " + objIndLicense.LicenseNumber;
                }


                  

                RevFeeDueBAL objFeeDueBAL = new RevFeeDueBAL();

                decimal Amount = 0;
                List<RevFeeDue> lstFeeDue = new List<RevFeeDue>();
                lstFeeDue = objFeeDueBAL.Get_RevFeeDue_by_TransactionId(objPaymentRequest.TransactionObject.TransactionId);

                if (lstFeeDue != null && lstFeeDue.Count() <= 0)
                {
                    throw new Exception("Invalid Transaction id sent. please sent the transaction object which you received on payment initiate call.");
                }

                if (lstFeeDue != null && lstFeeDue.Count() > 0 )
                {

                    foreach (RevFeeDue objRevFeeDue in lstFeeDue)
                    {
                        Amount += objRevFeeDue.FeeAmount;

                    }
                }

                if (Amount <= 0)
                {
                    throw new Exception("Invalid Transaction id sent. please sent the transaction object which you received on payment initiate call.");
                }

                string InvoiceNumber = lstFeeDue[0].InvoiceNo;

                if(!string.IsNullOrEmpty(InvoiceNumber))
                {
                    DescriptionText += " and Invoice Number - " + InvoiceNumber;
                }


                //string obj = JsonConvert.SerializeObject(lstOrder);
                //GNF.LogTransaction(obj);

                // Authoroze dot net ebsoft sandbox detail 
                string loginID =  LOGING.ConfigurationHelper.GetConfigurationValueBySetting("AuthorizeDotNetApiLoginId");//;
                string transactionKey =  LOGING.ConfigurationHelper.GetConfigurationValueBySetting("AuthorizeDotNetApiKey");;

                //string loginID = Lapp_Configuration.AuthorizeDotNetAPILoginID().Value.Trim();
                // string transactionKey = Lapp_Configuration.AuthorizeDotNetTransactionKey().Value.Trim();

                // By default, this sample code is designed to post to our test server for
                // developer accounts: https://test.authorize.net/gateway/transact.dll
                //String post_url = "https://test.authorize.net/gateway/transact.dll";

                // for real accounts (even in test mode), please make sure that you are
                // posting to: https://secure.authorize.net/gateway/transact.dll
                String post_url = LOGING.ConfigurationHelper.GetConfigurationValueBySetting("AuthorizeDotNetPostUrl");// "https://test.authorize.net/gateway/transact.dll";
                //String post_url =  Lapp_Configuration.GetAuthorizeNetPostUrl();


                Dictionary<string, string> post_values = new Dictionary<string, string>();
                //the API Login ID and Transaction Key must be replaced with valid values

                post_values.Add("x_login", loginID);
                post_values.Add("x_tran_key", transactionKey);
                post_values.Add("x_delim_data", "TRUE");
                post_values.Add("x_delim_char", "|");
                post_values.Add("x_relay_response", "FALSE");

                if (System.Web.HttpContext.Current.IsDebuggingEnabled)
                {
                    post_values.Add("x_test_request", "TRUE");
                }
                else
                {
                    post_values.Add("x_test_request", "FALSE");
                }
                
               
                post_values.Add("x_type", "AUTH_CAPTURE");
                post_values.Add("x_method", "CC");

                post_values.Add("x_card_num", objPaymentRequest.CardNumber);
                post_values.Add("x_exp_date", objPaymentRequest.ExpirationMonths.ToString() + objPaymentRequest.ExpirationYears.ToString()); // ddlExpirationMonths.SelectedValue + ddlExpirationYears.SelectedValue);
                                                                                                                                             //post_values.Add("x_exp_date", ddlExpirationMonths.SelectedItem.Text/ddlExpirationYears.SelectedItem.Text);

                //post_values.Add("x_amount", objPaymentRequest.Amount.ToString()); //this.TotalAmount.ToString()
                post_values.Add("x_amount", Amount.ToString()); //this.TotalAmount.ToString()

                post_values.Add("x_description", DescriptionText);
                post_values.Add("x_invoice_num", InvoiceNumber);

                // Billing Address

                post_values.Add("x_first_name", objPaymentRequest.FirstName.Trim());
                post_values.Add("x_last_name", objPaymentRequest.LastName.Trim());
                //post_values.Add("x_company", x_company.Text.Trim());
                post_values.Add("x_address", objPaymentRequest.Address.Trim());
                post_values.Add("x_city", objPaymentRequest.City);
                post_values.Add("x_state", objPaymentRequest.StateCode);
                post_values.Add("x_zip", objPaymentRequest.Zip);
                post_values.Add("x_country", objPaymentRequest.Country); // x_country.Text.Trim());

                post_values.Add("x_email", objPaymentRequest.EmailAddress.Trim());
                // post_values.Add("x_phone", x_phone.Text.Trim());
                //  post_values.Add("x_fax", x_fax.Text.Trim());

                //Shipping Address

                //post_values.Add("x_ship_to_first_name", x_ship_to_first_name.Text.Trim());
                //post_values.Add("x_ship_to_last_name", x_ship_to_last_name.Text.Trim());
                //post_values.Add("x_ship_to_company", x_ship_to_company.Text.Trim());
                //post_values.Add("x_ship_to_address", x_ship_to_address.Text.Trim());
                //post_values.Add("x_ship_to_city", x_ship_to_city.Text.Trim());
                //post_values.Add("x_ship_to_state", x_ship_to_state.Text.Trim());
                //post_values.Add("x_ship_to_zip", x_ship_to_zip.Text.Trim());
                //post_values.Add("x_ship_to_country", x_ship_to_country.Text.Trim());

                // Additional fields can be added here as outlined in the AIM integration
                // guide at: http://developer.authorize.net

                // This section takes the input fields and converts them to the proper format
                // for an http post.  For example: "x_login=username&x_tran_key=a1B2c3D4"
                String post_string = "";

                foreach (KeyValuePair<string, string> post_value in post_values)
                {
                    post_string += post_value.Key + "=" + HttpUtility.UrlEncode(post_value.Value) + "&";
                }
                post_string = post_string.TrimEnd('&');

                // The following section provides an example of how to add line item details to
                // the post string.  Because line items may consist of multiple values with the
                // same key/name, they cannot be simply added into the above array.
                //
                // This section is commented out by default.
                /*
                string[] line_items = {
                    "item1<|>golf balls<|><|>2<|>18.95<|>Y",
                    "item2<|>golf bag<|>Wilson golf carry bag, red<|>1<|>39.99<|>Y",
                    "item3<|>book<|>Golf for Dummies<|>1<|>21.99<|>Y"};

                foreach( string value in line_items )
                {
                    post_string += "&x_line_item=" + HttpUtility.UrlEncode(value);
                }
                */


                // create an HttpWebRequest object to communicate with Authorize.net
                HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(post_url);
                objRequest.Method = "POST";
                objRequest.ContentLength = post_string.Length;
                objRequest.ContentType = "application/x-www-form-urlencoded";

                // post data is sent as a stream
                StreamWriter myWriter = null;
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(post_string);
                myWriter.Close();

                // returned values are returned as a stream, then read into a string
                String post_response;
                HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
                using (StreamReader responseStream = new StreamReader(objResponse.GetResponseStream()))
                {
                    post_response = responseStream.ReadToEnd();
                    responseStream.Close();
                }

                // the response string is broken into an array
                // The split character specified here must match the delimiting character specified above
                Array response_array = post_response.Split('|');


                AuthorizeDotNetGateWayResponse objAuthResponse = FetchAuthorize_Net_Response(response_array);

                string ResponseString = JsonConvert.SerializeObject(objAuthResponse);


                if (objAuthResponse != null && objAuthResponse.Response_Code.ToLower() == "1")
                {
                    objPaymentResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objPaymentResponse.Message = "Payment Succesful.";
                    objPaymentResponse.Status = true;

                    objPaymentResponse.PaymentAuthResponse = new PaymentAuthResponse();

                    objPaymentResponse.PaymentAuthResponse.ResponseCode = objAuthResponse.Response_Code;
                    objPaymentResponse.PaymentAuthResponse.AuthorizationCode = objAuthResponse.Authorization_Code;
                    objPaymentResponse.PaymentAuthResponse.ResponseSubcode = objAuthResponse.Response_Subcode;
                    objPaymentResponse.PaymentAuthResponse.ResponseReasonCode = objAuthResponse.Response_Reason_Code;
                    objPaymentResponse.PaymentAuthResponse.ResponseReasonText = objAuthResponse.Response_Reason_Text;
                    objPaymentResponse.PaymentAuthResponse.TransactionID = objAuthResponse.Transaction_ID;
                    objPaymentResponse.PaymentAuthResponse.InvoiceNumber = objAuthResponse.Invoice_Number;

                    string emailText = "Payment Successfull.";


                    // bool emailSent = GlobalFunctions.EmailHelper.SendMail(objPaymentRequest.EmailAddress, "Payment Successfull.", emailText, true);

                    //Save_Payment(objAuthResponse.Transaction_ID, objAuthResponse.Response_Code, "Authorized");

                    if (objTrans != null)
                    {
                        InitiatePayment.ProcessApprovedPayment(objTrans, objAuthResponse, objToken, objPaymentRequest.RequestedLicenseStatusTypeId, AffirmativeAction);

                        //string ReceiptNumber = SerialsBAL.Get_Receipt_No();
                        //IndividualBAL objIndividualBAL = new IndividualBAL();
                        //Individual objIndividual = objIndividualBAL.Get_Individual_By_IndividualId(objTrans.IndividualId);
                        //if (objIndividual != null)
                        //{

                            
                        //    List<RevFeeDue> objFeedueList = objFeeDueBAL.Get_RevFeeDue_by_TransactionId(objTrans.TransactionId);

                        //    decimal AmountDue = objFeedueList.Sum(x => x.FeeAmount);

                        //    RevFeeCollectBAL objFeeCollectBAL = new RevFeeCollectBAL();
                        //    RevFeeCollect objFeeCollect = new RevFeeCollect();
                        //    objFeeCollect.ShoppingCartId = objTrans.ShoppingCartId;
                        //    objFeeCollect.IndividualId = objTrans.IndividualId;
                        //    objFeeCollect.ProviderId = 0;

                        //    //IndividualLicenseBAL objIndividualLicenseBAL = new IndividualLicenseBAL();
                        //    //IndividualLicense objIndiLicense = objIndividualLicenseBAL.get

                        //    objFeeCollect.IndividualLicenseId = objTrans.IndividualLicenseId;
                        //    objFeeCollect.LicenseTypeId = objTrans.LicenseTypeId;
                        //    objFeeCollect.ReceiptNo = ReceiptNumber;
                        //    objFeeCollect.AmountDue = AmountDue;
                        //    objFeeCollect.PaymentMode = "OL";
                        //    objFeeCollect.PaymentModeNumber = "";
                        //    objFeeCollect.PaidAmount = Convert.ToDecimal(objAuthResponse.Amount);
                        //    objFeeCollect.PaymentDate = DateTime.Now;
                        //    objFeeCollect.InvoiceNo = objTrans.InvoiceNumber;
                        //    objFeeCollect.UserDefinedRefNo = objTrans.InvoiceNumber;
                        //    objFeeCollect.UserDefinedPaymentNo = ReceiptNumber;
                        //    objFeeCollect.RevCollectFeeNum = "";
                        //    objFeeCollect.RevFeePaidSource = "AuthDot";
                        //    objFeeCollect.CardType = "";
                        //    objFeeCollect.ConfirmationNo = objAuthResponse.Authorization_Code;
                        //    objFeeCollect.TransactionRefNo = objAuthResponse.Transaction_ID;
                        //    objFeeCollect.PaymentBankName = "";
                        //    objFeeCollect.ControlNo = "";
                        //    objFeeCollect.PaymentNo = "";
                        //    objFeeCollect.ReferenceNumber = objAuthResponse.Transaction_ID;
                        //    objFeeCollect.CreatedBy = CreatedBy;
                        //    objFeeCollect.CreatedOn = DateTime.Now;
                        //    objFeeCollect.RevFeeCollectGuid = Guid.NewGuid().ToString();

                        //    objFeeCollect.RevFeeCollectId = objFeeCollectBAL.Save_RevFeeCollect(objFeeCollect);


                        //    RevFeeDisbBAL objFeeDisbBAL = new RevFeeDisbBAL();

                        //    foreach (RevFeeDue FeeDue in objFeedueList)
                        //    {

                        //        RevFeeDisb objFeeDisb = new RevFeeDisb();
                        //        objFeeDisb.TransactionId = objTrans.TransactionId;
                        //        objFeeDisb.ShoppingCartId = objTrans.ShoppingCartId;
                        //        objFeeDisb.RevFeeMasterId = FeeDue.RevFeeMasterId;
                        //        objFeeDisb.IndividualId = FeeDue.IndividualId;
                        //        objFeeDisb.ApplicationId = FeeDue.ApplicationId;
                        //        objFeeDisb.ProviderId = 0;
                        //        objFeeDisb.IndividualLicenseId = FeeDue.IndividualLicenseId;
                        //        objFeeDisb.LicenseTypeId = FeeDue.LicenseTypeId;
                        //        objFeeDisb.RevFeeDueId = FeeDue.RevFeeDueId;
                        //        objFeeDisb.FinclTranDate = DateTime.Now;
                        //        objFeeDisb.PaymentPostDate = DateTime.Now;
                        //        objFeeDisb.InvoiceNo = FeeDue.InvoiceNo;
                        //        objFeeDisb.FeePaidAmount = FeeDue.FeeAmount;
                        //        objFeeDisb.OrigFeeAmount = FeeDue.FeeAmount;
                        //        objFeeDisb.ControlNo = FeeDue.ControlNo;
                        //        objFeeDisb.PaymentNo = ReceiptNumber;
                        //        objFeeDisb.ReferenceNumber = objAuthResponse.Authorization_Code;
                        //        objFeeDisb.CreatedBy = CreatedBy;
                        //        objFeeDisb.CreatedOn = DateTime.Now;
                        //        objFeeDisb.MasterTransactionId = 0;
                        //        objFeeDisb.RevFeeDisbGuid = Guid.NewGuid().ToString();

                        //        objFeeDisb.RevFeeDisbId = objFeeDisbBAL.Save_RevFeeDisb(objFeeDisb);


                        //        RevFeeDueDtlBAL objFeeDueDetailBAL = new RevFeeDueDtlBAL();
                        //        RevFeeDueDtl objFeeDueDetail = new RevFeeDueDtl();
                        //        objFeeDueDetail.RevFeeDueId = FeeDue.RevFeeDueId;
                        //        objFeeDueDetail.IndividualId = FeeDue.IndividualId;
                        //        objFeeDueDetail.ProviderId = 0;
                        //        objFeeDueDetail.IndividualLicenseId = FeeDue.IndividualLicenseId;
                        //        objFeeDueDetail.LicenseTypeId = FeeDue.LicenseTypeId;
                        //        objFeeDueDetail.InvoiceNo = FeeDue.InvoiceNo;
                        //        objFeeDueDetail.ReferenceNumber = FeeDue.ReferenceNumber;
                        //        objFeeDueDetail.PaymentNo = objFeeDisb.PaymentNo;
                        //        objFeeDueDetail.ReceiptNo = ReceiptNumber;
                        //        objFeeDueDetail.ControlNo = "";
                        //        objFeeDueDetail.FeePaidAmount = FeeDue.FeeAmount;
                        //        objFeeDueDetail.FeeDuePaymentDate = null;
                        //        objFeeDueDetail.CreatedBy = CreatedBy;
                        //        objFeeDueDetail.CreatedOn = DateTime.Now;
                        //        objFeeDueDetail.RevFeeDueDtlGuid = Guid.NewGuid().ToString();

                        //        objFeeDueDetail.RevFeeDueDtlId = objFeeDueDetailBAL.Save_RevFeeDueDtl(objFeeDueDetail);





                        //    }

                        //    objTrans.TransactionEndDatetime = DateTime.Now;
                        //    objTrans.TransactionStatus = "C";
                        //    objTrans.TransactionInterruptReasonId = 0;
                        //    objTranBal.Save_Transaction(objTrans);


                        //    LAPP.BAL.Renewal.RenewalProcess.ChangeLicenseStatus(objTrans.IndividualLicenseId, objTrans.ApplicationId, objPaymentRequest.RequestedLicenseStatusTypeId, objToken);


                        //}

                    }

                    return objPaymentResponse;
                }
                else if (objAuthResponse != null)
                {
                    //Record detail


                    string errormsg = "Error Code: " + objAuthResponse.Response_Reason_Code + " <br /> " + objAuthResponse.Response_Reason_Text;

                    //ltrMsg.Text = MessageBox.BuildValidationMessage(errormsg, 2);//Its seems to be an error in you current your request. If your card is charged then please contact to Board Office.

                    objPaymentResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.PaymentGatewayPaymentFail).ToString("00");
                    objPaymentResponse.Message = errormsg;
                    objPaymentResponse.Status = false;

                    objPaymentResponse.PaymentAuthResponse = new PaymentAuthResponse();
                    objPaymentResponse.PaymentAuthResponse.ResponseCode = objAuthResponse.Response_Code;
                    objPaymentResponse.PaymentAuthResponse.AuthorizationCode = objAuthResponse.Authorization_Code;
                    objPaymentResponse.PaymentAuthResponse.ResponseSubcode = objAuthResponse.Response_Subcode;
                    objPaymentResponse.PaymentAuthResponse.ResponseReasonCode = objAuthResponse.Response_Reason_Code;
                    objPaymentResponse.PaymentAuthResponse.ResponseReasonText = objAuthResponse.Response_Reason_Text;
                    objPaymentResponse.PaymentAuthResponse.TransactionID = objAuthResponse.Transaction_ID;
                    objPaymentResponse.PaymentAuthResponse.InvoiceNumber = objAuthResponse.Invoice_Number;

                    return objPaymentResponse;

                }
                else
                {
                    //Record detail
                    string errormsg = "Payment failed from gateway.";

                    //ltrMsg.Text = MessageBox.BuildValidationMessage(errormsg, 2);//Its seems to be an error in you current your request. If your card is charged then please contact to Board Office.

                    objPaymentResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.PaymentGatewayPaymentFail).ToString("00");
                    objPaymentResponse.Message = errormsg;
                    objPaymentResponse.Status = false;

                    objPaymentResponse.PaymentAuthResponse = new PaymentAuthResponse();
                    objPaymentResponse.PaymentAuthResponse.ResponseCode = "";
                    objPaymentResponse.PaymentAuthResponse.AuthorizationCode = "";
                    objPaymentResponse.PaymentAuthResponse.ResponseSubcode = "";
                    objPaymentResponse.PaymentAuthResponse.ResponseReasonCode = "";
                    objPaymentResponse.PaymentAuthResponse.ResponseReasonText = "";
                    objPaymentResponse.PaymentAuthResponse.TransactionID = "";
                    objPaymentResponse.PaymentAuthResponse.InvoiceNumber = "";

                    return objPaymentResponse;
                }
                #endregion
            }
            catch (Exception ex)
            {

                LAPP.LOGING.LogingHelper.SaveExceptionInfo("", ex, "ProcessPayment AuthorizeDotNet", eSeverity.Critical, 1);

                throw ex;

                //objPaymentResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.PaymentGatewayPaymentFail).ToString("00");
                //objPaymentResponse.Message = ex.Message;
                //objPaymentResponse.Status = false;

                //objPaymentResponse.PaymentAuthResponse = new PaymentAuthResponse();
                //objPaymentResponse.PaymentAuthResponse.ResponseCode = "";
                //objPaymentResponse.PaymentAuthResponse.AuthorizationCode = "";
                //objPaymentResponse.PaymentAuthResponse.ResponseSubcode = "";
                //objPaymentResponse.PaymentAuthResponse.ResponseReasonCode = "";
                //objPaymentResponse.PaymentAuthResponse.ResponseReasonText = "";
                //objPaymentResponse.PaymentAuthResponse.TransactionID = "";
                //objPaymentResponse.PaymentAuthResponse.InvoiceNumber = "";

                //return objPaymentResponse;
            }


        }


        private static AuthorizeDotNetGateWayResponse FetchAuthorize_Net_Response(Array objArray)
        {
            string ErrorMessage = "";
            AuthorizeDotNetGateWayResponse objAuthResponce = new AuthorizeDotNetGateWayResponse();
            try
            {
                int Count = objArray.Length;

                if (Count >= 4)
                {
                    objAuthResponce.Response_Code = objArray.GetValue(0).ToString();

                    objAuthResponce.Response_Subcode = objArray.GetValue(1).ToString();
                    objAuthResponce.Response_Reason_Code = objArray.GetValue(2).ToString();
                    objAuthResponce.Response_Reason_Text = objArray.GetValue(3).ToString();
                    objAuthResponce.Authorization_Code = objArray.GetValue(4).ToString();
                    ErrorMessage = objAuthResponce.Response_Reason_Text;
                }
                if (Count >= 38)
                {
                    objAuthResponce.AVS_Response = objArray.GetValue(5).ToString();
                    objAuthResponce.Transaction_ID = objArray.GetValue(6).ToString();
                    objAuthResponce.Invoice_Number = objArray.GetValue(7).ToString();
                    objAuthResponce.Description = objArray.GetValue(8).ToString();
                    objAuthResponce.Amount = objArray.GetValue(9).ToString();
                    objAuthResponce.Method = objArray.GetValue(10).ToString();
                    objAuthResponce.Transaction_Type = objArray.GetValue(11).ToString();
                    objAuthResponce.Customer_ID = objArray.GetValue(12).ToString();
                    objAuthResponce.First_Name = objArray.GetValue(13).ToString();
                    objAuthResponce.Last_Name = objArray.GetValue(14).ToString();
                    objAuthResponce.Company = objArray.GetValue(15).ToString();
                    objAuthResponce.Address = objArray.GetValue(16).ToString();
                    objAuthResponce.City = objArray.GetValue(17).ToString();
                    objAuthResponce.State = objArray.GetValue(18).ToString();
                    objAuthResponce.ZIP_Code = objArray.GetValue(19).ToString();
                    objAuthResponce.Country = objArray.GetValue(20).ToString();
                    objAuthResponce.Phone = objArray.GetValue(21).ToString();
                    objAuthResponce.Fax = objArray.GetValue(22).ToString();
                    objAuthResponce.Email_Address = objArray.GetValue(23).ToString();
                    objAuthResponce.Ship_To_First_Name = objArray.GetValue(24).ToString();
                    objAuthResponce.Ship_To_Last_Name = objArray.GetValue(25).ToString();
                    objAuthResponce.Ship_To_Company = objArray.GetValue(26).ToString();
                    objAuthResponce.Ship_To_Address = objArray.GetValue(27).ToString();
                    objAuthResponce.Ship_To_City = objArray.GetValue(28).ToString();
                    objAuthResponce.Ship_To_State = objArray.GetValue(29).ToString();
                    objAuthResponce.Ship_To_ZIP_Code = objArray.GetValue(30).ToString();
                    objAuthResponce.Ship_To_Country = objArray.GetValue(31).ToString();
                    objAuthResponce.Tax = objArray.GetValue(32).ToString();
                    objAuthResponce.Duty = objArray.GetValue(33).ToString();
                    objAuthResponce.Freight = objArray.GetValue(34).ToString();
                    objAuthResponce.Tax_Exempt = objArray.GetValue(35).ToString();
                    objAuthResponce.Purchase_Order_Number = objArray.GetValue(36).ToString();
                    objAuthResponce.MD5_Hash = objArray.GetValue(37).ToString();
                    objAuthResponce.Card_Code_Response = objArray.GetValue(38).ToString();
                }
                if (Count >= 44)
                {
                    objAuthResponce.Cardholder_Authentication_Verification_Response = objArray.GetValue(39).ToString();
                    objAuthResponce.Account_Number = objArray.GetValue(40).ToString();
                    objAuthResponce.Card_Type = objArray.GetValue(41).ToString();
                    objAuthResponce.Split_Tender_ID = objArray.GetValue(42).ToString();
                    objAuthResponce.Requested_Amount = objArray.GetValue(43).ToString();
                    objAuthResponce.Balance_On_Card = objArray.GetValue(44).ToString();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return objAuthResponce;
        }

    }
}

