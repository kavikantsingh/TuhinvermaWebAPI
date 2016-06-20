using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class RevFeeDisbResponse : BaseEntity

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
        public string FeeName { get; set; }

        public string ApplicationName { get; set; }
        public string PaymentStatus { get; set; }
        public string ApplicationNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public string PaymentMode { get; set; }

        public List<FeeDetail> FeeDetailList { get; set; }
    }

    public class FeeDetail
    {
        public int RevFeeDisbId { get; set; }
        public string FeeName { get; set; }
        public decimal FeePaidAmount { get; set; }
        public decimal OrigFeeAmount { get; set; }
        public string Description { get; set; }

    }

    public class RevFeeDisb : RevFeeDisbResponse

    {
        public string ReferenceNumber { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string RevFeeDisbGuid { get; set; }
    }

    public class RevFeeDisbAPIResponse : BaseEntityServiceResponse

    {

        public List<RevFeeDisbResponse> RevFeeDisbResponseList { get; set; }
    }
}
