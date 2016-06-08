using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class ProviderInformation : BaseEntity

    {
        public string SchoolName { get; set; }
        public string SchoolTelephone { get; set; }
        public bool IsSchoolTelephoneMobile { get; set; }
        public string SchoolWebsite { get; set; }


        public string SchoolAddressStreet1 { get; set; }
        public string SchoolAddressStreet2 { get; set; }
        public string SchoolAddressCity { get; set; }
        public string SchoolAddressState { get; set; }
        public string SchoolAddressZip { get; set; }
        public bool SchoolAddressIsVerifiedClicked { get; set; }
        public bool SchoolAddressIsNotVerifiedClicked { get; set; }

        public string MailingAddressStreet1 { get; set; }
        public string MailingAddressStreet2 { get; set; }
        public string MailingAddressCity { get; set; }
        public string MailingAddressState { get; set; }
        public string MailingAddressZip { get; set; }
        public bool MailingAddressIsVerifiedClicked { get; set; }
        public bool MailingAddressIsNotVerifiedClicked { get; set; }


        public string DirectorFirstName { get; set; }
        public string DirectorLastName { get; set; }
        public string DirectorAdministratorEmail { get; set; }
        public string DirectorJobTitle { get; set; }
        public string DirectorPrimaryNumber { get; set; }
        public bool DirectorPrimaryNumberIsMobile { get; set; }
        public string DirectorSecondaryNumber { get; set; }
        public bool DirectorSecondaryNumberIsMobile { get; set; }

        public string ContactNameFirstName { get; set; }
        public string ContactNameLastName { get; set; }
        public string ContactNameAdministratorEmail { get; set; }
        public string ContactNameJobTitle { get; set; }
        public string ContactNamePrimaryNumber { get; set; }
        public bool ContactNamePrimaryNumberIsMobile { get; set; }
        public string ContactNameSecondaryNumber { get; set; }
        public bool ContactNameSecondaryNumberIsMobile { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public string ContactInfo { get; set; }
        public int ContactTypeId { get; set; }
        public int ProviderId { get; set; }
        public bool IsMobile { get; set; }
        public bool IsPreferredContact { get; set; }


        public int ApplicationId { get; set; }
        public int AddressId { get; set; }
        public DateTime DateValidated { get; set; }
        public bool UseUserAddress { get; set; }
        public bool UseVerifiedAddress { get; set; }
        public string AddressTypeId { get; set; }
        public bool IsMailingSameasPhysical { get; set; }
        public int IndividualId { get; set; }
        
    }
}
