using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class RevFeeCollect : BaseEntity

    {
        public int RevFeeCollectId { get; set; }
        public int ShoppingCartId { get; set; }
        public int IndividualId { get; set; }
        public int ProviderId { get; set; }
        public int IndividualLicenseId { get; set; }
        public int LicenseTypeId { get; set; }
        public string ReceiptNo { get; set; }
        public decimal AmountDue { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentModeNumber { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string InvoiceNo { get; set; }
        public string UserDefinedRefNo { get; set; }
        public string UserDefinedPaymentNo { get; set; }
        public string RevCollectFeeNum { get; set; }
        public string RevFeePaidSource { get; set; }
        public string CardType { get; set; }
        public string ConfirmationNo { get; set; }
        public string TransactionRefNo { get; set; }
        public string PaymentBankName { get; set; }
        public string ControlNo { get; set; }
        public string PaymentNo { get; set; }
        public string ReferenceNumber { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string RevFeeCollectGuid { get; set; }
    }
}
