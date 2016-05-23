using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class RevFeeDisb : BaseEntity

    {
        public int RevFeeDisbId { get; set; }
        public int TransactionId { get; set; }
        public int ShoppingCartId { get; set; }
        public int RevFeeMasterId { get; set; }
        public int MasterTransactionId { get; set; }
        public int IndividualId { get; set; }
        public int ApplicationId { get; set; }
        public int ProviderId { get; set; }
        public int IndividualLicenseId { get; set; }
        public int LicenseTypeId { get; set; }
        public int RevFeeDueId { get; set; }
        public DateTime? FinclTranDate { get; set; }
        public DateTime? PaymentPostDate { get; set; }
        public string InvoiceNo { get; set; }
        public decimal FeePaidAmount { get; set; }
        public decimal OrigFeeAmount { get; set; }
        public string ControlNo { get; set; }
        public string PaymentNo { get; set; }
        public string ReferenceNumber { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string RevFeeDisbGuid { get; set; }
    }
}
