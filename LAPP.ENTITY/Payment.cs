using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{

    public class PaymentResponse : BaseEntityServiceResponse
    {
        public PaymentAuthResponse PaymentAuthResponse { get; set; }


    }
    public class PaymentAuthResponse : BaseEntity
    {
        [Display(Description = "Code 00: Payment Success")]
        public string ResponseCode { get; set; }
        public string ResponseSubcode { get; set; }
        public string ResponseReasonCode { get; set; }
        public string ResponseReasonText { get; set; }
        public string AuthorizationCode { get; set; }
        public string TransactionID { get; set; }
        public string InvoiceNumber { get; set; }
    }

    public class ListItemResponse : BaseEntityServiceResponse
    {

        public List<ListItems> ListItems { get; set; }

    }
    public class ListItems
    {
        public string Text { get; set; }
        public string Value { get; set; }

    }
    public class AuthorizeDotNetGateWayResponse : BaseEntity
    {
        public string Response_Code { get; set; }
        public string Response_Subcode { get; set; }
        public string Response_Reason_Code { get; set; }
        public string Response_Reason_Text { get; set; }
        public string Authorization_Code { get; set; }
        public string AVS_Response { get; set; }
        public string Transaction_ID { get; set; }
        public string Invoice_Number { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
        public string Method { get; set; }
        public string Transaction_Type { get; set; }
        public string Customer_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZIP_Code { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email_Address { get; set; }
        public string Ship_To_First_Name { get; set; }
        public string Ship_To_Last_Name { get; set; }
        public string Ship_To_Company { get; set; }
        public string Ship_To_Address { get; set; }
        public string Ship_To_City { get; set; }
        public string Ship_To_State { get; set; }
        public string Ship_To_ZIP_Code { get; set; }
        public string Ship_To_Country { get; set; }
        public string Tax { get; set; }
        public string Duty { get; set; }
        public string Freight { get; set; }
        public string Tax_Exempt { get; set; }
        public string Purchase_Order_Number { get; set; }
        public string MD5_Hash { get; set; }
        public string Card_Code_Response { get; set; }
        public string Cardholder_Authentication_Verification_Response { get; set; }
        public string Account_Number { get; set; }
        public string Card_Type { get; set; }
        public string Split_Tender_ID { get; set; }
        public string Requested_Amount { get; set; }
        public string Balance_On_Card { get; set; }
    }

    public class PaymentRequest : BaseEntity
    {
        [Display(Description = "Required: Yes, Max Length:50 (string)")]
        public string ApplicationNumber { get; set; }

        [Display(Description = "Required: Yes,  (Integer)")]
        public int ApplicationId { get; set; }

        [Display(Description = "Required: Yes,  (Integer)")]
        public int IndividualId { get; set; }

        [Display(Description = "Required: Yes,   Max Length:30 (string)")]
        public string CardNumber { get; set; }

        [Display(Description = "Required: Yes,   Max Length:4 (string)")]
        public int CVV { get; set; }

        [Display(Description = "Required: Yes,   Max Length:2 (Integer)")]
        public int ExpirationMonths { get; set; }

        [Display(Description = "Required: Yes, Exclude century number from year,  Max Length:2 (Integer)")]
        public int ExpirationYears { get; set; }

        [Display(Description = "Required: Yes, (decimal)")]
        public decimal Amount { get; set; }

        [Display(Description = "Required: Yes,  Max Length:200 (string)")]
        public string Description { get; set; }

        [Display(Description = "Required: Yes, Max Length:40 (string)")]
        public string InvoiceNumber { get; set; }

        [Display(Description = "Required: Yes, Max Length:40 (string)")]
        public string FirstName { get; set; }

        [Display(Description = "Required: Yes, Max Length:40 (string)")]
        public string LastName { get; set; }

        [Display(Description = "Required: Yes, Max Length:200 (string)")]
        public string Address { get; set; }

        [Display(Description = "Required: Yes, Max Length:40 (string)")]
        public string City { get; set; }

        [Display(Description = "Required: Yes, Use code only eg:NV, Max Length:2 (string)")]
        public string StateCode { get; set; }

        [Display(Description = "Required: Yes, Max Length:11 (string)")]
        public string Zip { get; set; }

        [Display(Description = "Required: Yes, Use state code only eg:USA,  Max Length:3 (string)")]
        public string Country { get; set; }

        [Display(Description = "Required: Yes, Enter valid email address,  Max Length:128 (string)")]
        public string EmailAddress { get; set; }

        public Transaction TransactionObject { get; set; }
        public int RequestedLicenseStatusTypeId { get; set; }
    }
    public class Payment : PaymentResponse
    {

    }

    public class InitiatePaymentRequest
    {
        public int ApplicationId { get; set; }
        public int IndividualId { get; set; }
        public int IndividualLicenseId { get; set; }
        public int LicenseTypeId { get; set; }
        public string LicenseNumber { get; set; }
        public string TransactionDeviceTy { get; set; }
        public List<FeeDetails> FeeDetailsList { get; set; }
        public Transaction TransactionObject { get; set; }

    }

    public class FeeDetails
    {
        public int RevMstFeeId { get; set; }
        public string FeeName { get; set; }
        public decimal FeeAmount { get; set; }
        public int FeeTypeID { get; set; }
        public int LicenseTypeId { get; set; }
        public int IndividualLicenseId { get; set; }
        public string Description { get; set; }
    }


    public class ManualPaymentRequest : BaseEntity
    {
        [Display(Description = "Required: Yes, Max Length:50 (string)")]
        public string ApplicationNumber { get; set; }

        [Display(Description = "Required: Yes,  (Integer)")]
        public int ApplicationId { get; set; }

        [Display(Description = "Required: Yes,  (Integer)")]
        public int IndividualId { get; set; }

        [Display(Description = "Required: Yes,   For example Money Order = MO ")]
        public string PaymentMode { get; set; }

        [Display(Description = "Required: No,   Please enter payment mode number. ")]
        public string PaymentModeNumber { get; set; }

        [Display(Description = "Required: No,   Please enter payment bank name. ")]
        public string PaymentBankName { get; set; }

        [Display(Description = "Required: Yes, (decimal)")]
        public decimal Amount { get; set; }

        [Display(Description = "Required: Yes,  Max Length:200 (string)")]
        public string Description { get; set; }

        [Display(Description = "Required: Yes, Max Length:40 (string)")]
        public string InvoiceNumber { get; set; }

        public Transaction TransactionObject { get; set; }
        public int RequestedLicenseStatusTypeId { get; set; }
    }

    public class ManualPaymentResponse: BaseEntityServiceResponse
    {
        
    }
}
