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
        public string Email { get; set; }

        //public bool IsDeleted { get; set; }
        //public int CreatedBy { get; set; }
        //public DateTime CreatedOn { get; set; }
        //public int ModifiedBy { get; set; }
        //public DateTime ModifiedOn { get; set; }
        //public string IndividualGuid { get; set; }
        //public string Authenticator { get; set; }
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
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string IndividualGuid { get; set; }
        public string Authenticator { get; set; }


        // For Search
        public int StatusId { get; set; }
        public string ApplicationNumber { get; set; }

        public string StatusName { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public bool IsPaid { get; set; }
        public string LicenseNumber { get; set; }
    }


    public class RenewalApplication : BaseEntity
    {
        public string Name { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }

        public string LicenseNumber { get; set; }

        public string SSN { get; set; }

        public int StatusId { get; set; }

        public string StatusName { get; set; }

        public int IndividualId { get; set; }
        public string ApplicationNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime SubmittedDate { get; set; }
        public bool IsPaid { get; set; }
    }

    public class IndividualSearch: BaseEntity
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string LicenseNumber { get; set; }
        public string SSN { get; set; }
        public string MiddleName { get; set; }
        public string Address { get; set; }
        public int IndividualId { get; set; }
        public bool IsActive { get; set; }


    }

    public class IndividualSearchResponse : BaseEntityServiceResponse
    {
        public List<IndividualSearch> IndividualList { get; set; }
    }

    public class RenewalSearchResponse : BaseEntityServiceResponse
    {
        public List<RenewalApplication> RenewalApplicationList { get; set; }
    }

    public class IndividualResponseRequest : BaseEntityServiceResponse
    {
        public List<IndividualResponse> IndividualResponse { get; set; }
    }

}
