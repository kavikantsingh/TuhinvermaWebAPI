using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class ProviderRelatedSchools : BaseEntity
    {
        public bool Isupdate { get; set; }
        public int ProviderRelatedSchoolId { get; set; }
        public int ProviderId { get; set; }
        public int ProviderNameId { get; set; }
        public int ApplicationId { get; set; }
        public DateTime DateAssociated { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ProviderRelatedSchoolGuid { get; set; }
        public string Action { get; set; }

        public string ContactInfos { get; set; }
        public string ContactIds { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string ProviderName { get; set; }
        public string StreetLine1 { get; set; }
        public string StreetLine2 { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string ZIP { get; set; }
        public int? CountyId { get; set; }
        public int? CountryId { get; set; }

        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public int PhoneId { get; set; }
        public int WebsiteId { get; set; }
        public int EmailId { get; set; }

        public string Phone { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }

        public int AddressId { get; set; }
        public int ContactId { get; set; }
        public int ProviderNameAddressId { get; set; }
        public int ProviderNameContactId { get; set; }


    }

    public class ProviderRelatedSchoolsGetResponse : BaseEntityServiceResponse
    {
        public List<ProviderRelatedSchools> ProviderRelatedSchoolsList { get; set; }
    }

    public class ProviderRelatedSchoolsAddLK
    {
        public int ProviderRelatedSchoolId { get; set; }
        public int ProviderId { get; set; }
        public int ProviderNameId { get; set; }
        public int ApplicationId { get; set; }
        public int ProviderNameAddressId { get; set; }
        public string ProviderRelatedSchoolAddressLkGuid { get; set; }

    }

    public class ProviderRelatedSchoolsConLK
    {
        public int ProviderRelatedSchoolId { get; set; }
        public int ProviderId { get; set; }
        public int ProviderNameId { get; set; }
        public int ApplicationId { get; set; }
        public int ProviderNameContactId { get; set; }
        public string ProviderRelatedSchoolContactLkGuid { get; set; }

    }
}
