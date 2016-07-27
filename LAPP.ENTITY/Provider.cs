using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class ProviderNames : ProviderResponse

    {
        public int ProviderNameId { get; set; }
        public int ApplicationId { get; set; }
        public int IndividualId { get; set; }
        public string ProviderName { get; set; }
        public DateTime DateofNameChange { get; set; }
        public int ProviderNameStatusId { get; set; }
        public int ProviderNameTypeId { get; set; }
        public string ReferenceNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ProviderNameGuid { get; set; }
    }

    public class ProviderNameResponse : BaseEntity

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

    public class MyTest
    {
        public int ApplicationId { get; set; }
        public DateTime DateofNameChange { get; set; }
        public string ProviderName { get; set; }

        public static implicit operator MyTest(List<object> v)
        {
            throw new NotImplementedException();
        }
    }

    public class ProviderNameResponseRequest : BaseEntityServiceResponse
    {
        public List<ProviderNameResponse> ProviderNameResponseList { get; set; }
    }

    public class ProviderTabStatus
    {
        public int ApplicationTabStatusId { get; set; }
        public int ApplicationId { get; set; }
        public int PageModuleId { get; set; }
        public int PageModuleTabSubModuleId { get; set; }
        public int PageTabSectionId { get; set; }
        public int IndividualId { get; set; }
        public int ProviderId { get; set; }
        public string TabName { get; set; }
        public bool ApplicationTabStatus { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }

    public class ProviderTabStatusGet
    {
        public string TabName { get; set; }
        public bool ApplicationTabStatus { get; set; }
    }

    public class ProviderTabStatusGetResponse : BaseEntityServiceResponse
    {
        public string TabName { get; set; }
        public bool ApplicationTabStatus { get; set; }
    }

    public class ProviderTabStatusGetResponseRequest : ProviderTabStatusGetResponse
    {
        public List<ProviderTabStatusGetResponse> ProviderTabStatusList { get; set; }
    }


    public class ProviderNameAddress 

    {
        public int ProviderNameAddressId { get; set; }
        public int ProviderId { get; set; }
        public int ProviderNameId { get; set; }
        public int ApplicationId { get; set; }
        public int AddressId { get; set; }
        public int AddressTypeId { get; set; }
        public DateTime AddressChangeDate { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsMailingSameasPhysical { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ProviderNameAddressGuid { get; set; }
    }

    public class ProviderNameContact

    {
        public int ProviderNameContactId { get; set; }
        public int ProviderId { get; set; }
        public int ProviderNameId { get; set; }
        public int ApplicationId { get; set; }
        public int ContactId { get; set; }
        public int ContactTypeId { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPreferredContact { get; set; }
        public bool IsMobile { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ProviderNameContactGuid { get; set; }
    }
}
