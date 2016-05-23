using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class RevFeeDue : BaseEntity

    {
        public int RevFeeDueId { get; set; }
        public int TransactionId { get; set; }
        public int RevFeeMasterId { get; set; }
        public int MasterTransactionId { get; set; }
        public int IndividualId { get; set; }
        public int ApplicationId { get; set; }
        public int ProviderId { get; set; }
        public int IndividualLicenseId { get; set; }
        public int LicenseTypeId { get; set; }
        public int BatchId { get; set; }
        public int TaskId { get; set; }
        public int FeeDueTypeId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string ReferenceNumber { get; set; }
        public string ControlNo { get; set; }
        public decimal FeeAmount { get; set; }
        public DateTime? FeeDueDate { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string RevFeeDueGuid { get; set; }
    }
}
