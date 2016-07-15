using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class Provider : ProviderResponse

    {
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ProviderGuid { get; set; }
    }

    public class ProviderResponse : BaseEntity

    {
        public int ProviderId { get; set; }
        public string ProviderNumber { get; set; }
        public int DepartmentId { get; set; }
        public int ProviderTypeId { get; set; }
        public string ProviderName { get; set; }
        public string ProviderDBAName { get; set; }
        public string LicenseNumber { get; set; }
        public int ProviderStatusTypeId { get; set; }
        public string OwnershipCompany { get; set; }
        public string BillingNumber { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string TaxId { get; set; }
        public string ReferenceNumber { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsActive { get; set; }
    }


    public class ProviderResponseRequest : BaseEntityServiceResponse
    {
        public List<ProviderResponse> ProviderResponseList { get; set; }
    }

    public class AllProviderResponseRequest : BaseEntityServiceResponse
    {
        public List<Provider> ProviderResponseList { get; set; }
    }


    public class ProviderInfo : BaseEntityServiceResponse
    {
        public Provider ProviderResponse { get; set; }
    }

}
