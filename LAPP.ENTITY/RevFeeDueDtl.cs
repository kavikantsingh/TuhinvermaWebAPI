using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class RevFeeDueDtl : BaseEntity

    {
        public int RevFeeDueDtlId { get; set; }
        public int RevFeeDueId { get; set; }
        public int IndividualId { get; set; }
        public int ProviderId { get; set; }
        public int IndividualLicenseId { get; set; }
        public int LicenseTypeId { get; set; }
        public string InvoiceNo { get; set; }
        public string ReferenceNumber { get; set; }
        public string PaymentNo { get; set; }
        public string ReceiptNo { get; set; }
        public string ControlNo { get; set; }
        public decimal FeePaidAmount { get; set; }
        public DateTime? FeeDuePaymentDate { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string RevFeeDueDtlGuid { get; set; }
    }
}
