using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class Provider : BaseEntity

    {
        public int ProviderId { get; set; }
        public string ProviderNumber { get; set; }
        public int DepartmentId { get; set; }
        public int ProviderTypeId { get; set; }
        public byte ProviderName { get; set; }
        public byte ProviderDBAName { get; set; }
        public string LicenseNumber { get; set; }
        public int ProviderStatusTypeId { get; set; }
        public string OwnershipCompany { get; set; }
        public string BillingNumber { get; set; }
        public DateTime ClosedDate { get; set; }
        public byte TaxId { get; set; }
        public string ReferenceNumber { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Guid ProviderGuid { get; set; }
    }
}
