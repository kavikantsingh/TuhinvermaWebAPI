using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class Transaction : BaseEntity

    {
        public int TransactionId { get; set; }
        public int MasterTransactionId { get; set; }
        public int IndividualId { get; set; }
        public int ApplicationId { get; set; }
        public int ProviderId { get; set; }
        public int ShoppingCartId { get; set; }
        public int IndividualLicenseId { get; set; }
        public int LicenseTypeId { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime TransactionStartDatetime { get; set; }
        public DateTime TransactionEndDatetime { get; set; }
        public string TransactionStatus { get; set; }
        public int TransactionInterruptReasonId { get; set; }
        public string TransactionDeviceTy { get; set; }

        public string InvoiceNumber { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string TransactionGuid { get; set; }
    }

    public class TrasactionRes: BaseEntity
    {
        public int TransactionId { get; set; }
        public int MasterTransactionId { get; set; }
        public int IndividualId { get; set; }
        public int ApplicationId { get; set; }
        public int ProviderId { get; set; }
        public int ShoppingCartId { get; set; }
        public int IndividualLicenseId { get; set; }
        public int LicenseTypeId { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime TransactionStartDatetime { get; set; }
        public DateTime? TransactionEndDatetime { get; set; }
        public string TransactionStatus { get; set; }
        public int TransactionInterruptReasonId { get; set; }
        public string TransactionDeviceTy { get; set; }
        public decimal ConvenienceFee { get; set; }

        public string InvoiceNumber { get; set; }
    }

    public class TransactionResponse: BaseEntityServiceResponse
    {
        public TrasactionRes Transaction { get; set; }
    }
}
