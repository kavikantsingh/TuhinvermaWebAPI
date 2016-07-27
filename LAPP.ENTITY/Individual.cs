using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{


    public class IndividualResponse : BaseEntity
    {
        public int IndividualId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int? SuffixId { get; set; }
        public string SSN { get; set; }
        public bool IsItin { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? RaceId { get; set; }
        public string Gender { get; set; }
        public int? HairColorId { get; set; }
        public int? EyeColorId { get; set; }
        public int? Weight { get; set; }
        public int? Height { get; set; }
        public string PlaceOfBirth { get; set; }
        public int? CitizenshipId { get; set; }
        public string ExternalId { get; set; }
        public string ExternalId2 { get; set; }
        public bool IsArchived { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public string Email { get; set; }

        // IndividualOther
        public bool IsNameChanged { get; set; }
        public string PlaceofBirthCity { get; set; }
        public string PlaceofBirthState { get; set; }
        public int PlaceofBirthCountry { get; set; }
        public string Picture { get; set; }

        // 
        public string LicenseStatusTypeName { get; set; }
        public DateTime OriginalLicenseDate { get; set; }
        public string StatusColorCode { get; set; }
        public string LicenseNumber { get; set; }
        public int LicenseTypeId { get; set; }
        public string LicenseTypeName { get; set; }
        public int LicenseStatusTypeId { get; set; }
        public string LicenseStatusTypeCode { get; set; }
        public string Name { get; set; }

        //public string FullName
        //{
        //    get
        //    {
        //        if (FirstName != null && LastName != null && MiddleName != null)
        //        {
        //            return FirstName + " " + LastName + " " + MiddleName;
        //        }
        //        else
        //        {

        //            return FirstName + " " + LastName;
        //        }
        //    }
        //}


        public IndividualAddress objIndividualAddress { get; set; }
        public IndividualContact objIndividualContact { get; set; }
    }

    public class Individual : IndividualResponse

    {
        //public int IndividualId { get; set; }
        //public byte FirstName { get; set; }
        //public byte MiddleName { get; set; }
        //public byte LastName { get; set; }
        //public int SuffixId { get; set; }
        //public string SSN { get; set; }
        //public bool IsItin { get; set; }
        //public DateTime DateOfBirth { get; set; }
        //public int RaceId { get; set; }
        //public string Gender { get; set; }
        //public int HairColorId { get; set; }
        //public int EyeColorId { get; set; }
        //public int Weight { get; set; }
        //public int Height { get; set; }
        //public string PlaceOfBirth { get; set; }
        //public int CitizenshipId { get; set; }
        //public string ExternalId { get; set; }
        //public string ExternalId2 { get; set; }
        //public bool IsArchived { get; set; }
        //public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string IndividualGuid { get; set; }
        public string Authenticator { get; set; }

        public int Total_Recard { get; set; }


        // For Search
        public int StatusId { get; set; }
        public string ApplicationNumber { get; set; }

        public string StatusName { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string Phone { get; set; }
        public bool IsPaid { get; set; }
    }

    public class IndividualSearch : BaseEntity
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string LicenseNumber { get; set; }
        public string SSN { get; set; }
        public string MiddleName { get; set; }
        public string Address { get; set; }
        public int IndividualId { get; set; }
        public bool IsActive { get; set; }
        public string StatusColorCode { get; set; }
        public string LicenseStatusTypeName { get; set; }

    }

    public class IndividualSearchResponse : BaseEntityServiceResponse
    {
        public int Total_Recard { get; set; }
        public List<IndividualSearch> IndividualList { get; set; }
    }

    public class IndividualResponseRequest : BaseEntityServiceResponse
    {
        public List<IndividualResponse> IndividualResponse { get; set; }
    }

    public class CertificateHolderLoadResponse
    {
        public int IndividualId { get; set; }
        public int IndividualLicenseId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string CAMTCIdNumber { get; set; }
        public string CAMTCCertificateNumber { get; set; }
        public int HomeAddressId { get; set; }
        public int HomeAddressTypeId { get; set; }
        public string HomeStreetLine1 { get; set; }
        public string HomeStreetLine2 { get; set; }
        public string HomeCity { get; set; }
        public string HomeStateCode { get; set; }
        public string HomeZip { get; set; }
        public int HomeCountryId { get; set; }
        public bool HomeUseUserAddress { get; set; }
        public bool HomeUseVerifiedAddress { get; set; }
        public int MailingAddressId { get; set; }
        public int MailingAddressTypeId { get; set; }
        public string MailingStreetLine1 { get; set; }
        public string MailingStreetLine2 { get; set; }
        public string MailingCity { get; set; }
        public string MailingStateCode { get; set; }
        public string MailingZip { get; set; }
        public int MailingCountryId { get; set; }
        public bool MailingUseUserAddress { get; set; }
        public bool MailingUseVerifiedAddress { get; set; }
        public bool IsMailingSameAsPhysical { get; set; }
        public int PrimaryPhoneContactId { get; set; }
        public int PrimaryPhoneContactTypeId { get; set; }
        public string PrimaryPhone { get; set; }
        public bool PrimaryPhoneIsMobile { get; set; }
        public int SecondaryPhoneContactId { get; set; }
        public int SecondaryPhoneContactTypeId { get; set; }
        public string SecondaryPhone { get; set; }
        public bool SecondaryPhoneIsMobile { get; set; }
        public int WebsiteContactId { get; set; }
        public int WebsiteContactTypeId { get; set; }
        public string Website { get; set; }
        public int PrimaryEmailContactId { get; set; }
        public int PrimaryEmailContactTypeId { get; set; }
        public string PrimaryEmail { get; set; }
        public int SecondaryEmailContactId { get; set; }
        public int SecondaryEmailContactTypeId { get; set; }
        public string SecondaryEmail { get; set; }
    }
    public class IndividualLoadResponse
    {
        public int IndividualId { get; set; }
        public int ApplicationId { get; set; }
        public int IndividualLicenseId { get; set; }
        public string MiddleName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CAMTCIdNumber { get; set; }
        public string CAMTCCertificateNumber { get; set; }
        public List<IndividualAddressLoadResponse> IndividualAddress { get; set; }
        public List<IndividualContactLoadResponse> IndividualContacts { get; set; }
    }

    public class IndividualAddressLoadResponse
    {
        public int IndividualId { get; set; }
        public int AddressId { get; set; }
        public int AddressTypeId { get; set; }
        public string StreetLine1 { get; set; }
        public string StreetLine2 { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string Zip { get; set; }
        public int CountryId { get; set; }
        public bool UseUserAddress { get; set; }
        public bool UseVerifiedAddress { get; set; }
        public bool IsMailingSameAsPhysical { get; set; }

    }
    public class IndividualContactLoadResponse
    {
        public int IndividualId { get; set; }
        public int ContactId { get; set; }
        public int ContactTypeId { get; set; }
        public bool IsMobile { get; set; }
        public string ContactInfo { get; set; }
    }
}
