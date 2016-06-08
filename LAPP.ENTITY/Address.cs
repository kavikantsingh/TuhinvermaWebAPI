using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{

    public class AddressResponse : BaseEntity

    {
        [Display(Description = "Required: Yes, Desc: Auto Generate")]
        public int AddressId { get; set; }

        [Display(Description = "Required: No, Max Length:100 (string)")]
        public string Addressee { get; set; }

        [Display(Description = "Required: No, Max Length:128 (string)")]
        public string StreetLine1 { get; set; }

        [Display(Description = "Required: No, Max Length:128 (string)")]
        public string StreetLine2 { get; set; }

        [Display(Description = "Required: No, Max Length:128 (string)")]
        public string City { get; set; }

        [Display(Description = "Required: No, Max Length:2 (char)")]
        public string StateCode { get; set; }

        [Display(Description = "Required: No, Max Length:128 (string)")]
        public string Zip { get; set; }

        [Display(Description = "Required:No, Max Length:11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int? CountyId { get; set; }

        [Display(Description = "Required:No, Max Length:11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int? CountryId { get; set; }



        [Display(Description = "Required: Yes, For example: true or false (0,1)")]
        public bool IsActive { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1)")]
        public bool BadAddress { get; set; }


    }
    public class Address : AddressResponse

    {

        [Display(Description = "Required: No, (DateTime)")]
        public DateTime? DateValidated { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1)")]
        public bool UseUserAddress { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1)")]
        public bool UseVerifiedAddress { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1)")]
        public bool IsDeleted { get; set; }

        [Display(Description = "Required:Yes, Max Length:11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int CreatedBy { get; set; }

        [Display(Description = "Required:Yes,  (DateTime), For example: MM/dd/yyyy HH:mm:ss ")]
        public DateTime CreatedOn { get; set; }

        [Display(Description = "Required:No, Max Length:11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int ModifiedBy { get; set; }

        [Display(Description = "Required:No,   (DateTime), For example: MM/dd/yyyy HH:mm:ss ")]
        public DateTime ModifiedOn { get; set; }

        [Display(Description = "Required: Yes, Max Length:36 (char)")]
        public string AddressGuid { get; set; }

        [Display(Description = "Required: Yes, Max Length:36 (char)")]
        public string Authenticator { get; set; }

        [Display(Description = "Required: Yes, Max Length:11")]
        public int ProviderId { get; set; }

        [Display(Description = "Required: Yes, Max Length:11")]
        public int AddressTypeId { get; set; }

        [Display(Description = "Required: No, DateTime, For example: MM/dd/yyyy HH:mm:ss ")]
        public DateTime BeginDate { get; set; }

        [Display(Description = "Required: No, DateTime, For example: MM/dd/yyyy HH:mm:ss ")]
        public DateTime EndDate { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1)")]
        public bool IsMailingSameasPhysical { get; set; }

        [Display(Description = "Required: Yes, Max Length:36 (char)")]
        public string ProviderAddressGuid { get; set; }

    }

    public class AddressResponseRequest : BaseEntityServiceResponse
    {
        public List<AddressResponse> AddressResponse { get; set; }
    }

}
