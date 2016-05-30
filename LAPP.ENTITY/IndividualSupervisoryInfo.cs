using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class IndividualSupervisoryInfo : BaseEntity

    {
        public int IndividualSupervisoryInfoId { get; set; }
        public int IndividualId { get; set; }
        public int ApplicationId { get; set; }
        public bool Doyousupervise { get; set; }
        public int? SupervisedIndividualId { get; set; }
        public int? SupervisedWorkAddressId { get; set; }
        public int? SupervisedMailingAddressId { get; set; }
        public int? SupervisedWorkPhoneContactId { get; set; }
        public int? SupervisedWorkEmailContactId { get; set; }
        public int? SupervisedWorkFaxContactId { get; set; }
        public string SupervisedLicenseNumber { get; set; }
        public string SupervisedStateLicensed { get; set; }
        public DateTime? SupervisedLicenseExpirationDate { get; set; }
        public bool Areyousupervised { get; set; }
        public int? SupervisorIndividualId { get; set; }
        public int? SupervisorWorkAddressId { get; set; }
        public int? SupervisorMailingAddressId { get; set; }
        public int? SupervisorWorkPhoneContactId { get; set; }
        public int? SupervisorWorkEmailContactId { get; set; }
        public int? SupervisorWorkFaxContactId { get; set; }
        public string SupervisorLicenseNumber { get; set; }
        public string SupervisorStateLicensed { get; set; }
        public DateTime? SupervisorLicenseExpirationDate { get; set; }

        public int IndividualNameId { get; set; }

        public string ReferenceNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string IndividualSupervisoryInfoGuid { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string SupervisorType { get; set; }
    }

    public class SponsorInformationResponse : BaseEntity

    {
        public int IndividualSupervisoryInfoId { get; set; }
        public int IndividualId { get; set; }
        public int ApplicationId { get; set; }
        // public bool Doyousupervise { get; set; }
        // public int? SupervisedIndividualId { get; set; }
        // public int? SupervisedWorkAddressId { get; set; }
        // public int? SupervisedMailingAddressId { get; set; }
        // public int? SupervisedWorkPhoneContactId { get; set; }
        //  public int? SupervisedWorkEmailContactId { get; set; }
        //  public int? SupervisedWorkFaxContactId { get; set; }
        // public string SupervisedLicenseNumber { get; set; }
        //  public string SupervisedStateLicensed { get; set; }
        //  public DateTime? SupervisedLicenseExpirationDate { get; set; }
        //   public bool Areyousupervised { get; set; }
        //   public int? SupervisorIndividualId { get; set; }
        //  public int? SupervisorWorkAddressId { get; set; }
        //   public int? SupervisorMailingAddressId { get; set; }
        //   public int? SupervisorWorkPhoneContactId { get; set; }
        //   public int? SupervisorWorkEmailContactId { get; set; }
        //   public int? SupervisorWorkFaxContactId { get; set; }
        public string SupervisorLicenseNumber { get; set; }
        // public string SupervisorStateLicensed { get; set; }
        // public DateTime? SupervisorLicenseExpirationDate { get; set; }
        // public string ReferenceNumber { get; set; }
        public bool IsActive { get; set; }
        public int IndividualNameId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int? SupervisorWorkAddressId { get; set; }
        public string SupervisorType { get; set; }
        public bool IsDeleted { get; set; }
        public List<SponsorAddressResponse> SponsorAddress { get; set; }

    }

    public class SponsorAddressResponse : BaseEntity
    {
        [Display(Description = "Required: Yes, Desc: Auto Generate")]
        public int AddressId { get; set; }


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

    }
}
