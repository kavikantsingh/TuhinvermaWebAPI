using System;
using System.Collections.Generic;
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
        public int SupervisedIndividualId { get; set; }
        public int SupervisedWorkAddressId { get; set; }
        public int SupervisedMailingAddressId { get; set; }
        public int SupervisedWorkPhoneContactId { get; set; }
        public int SupervisedWorkEmailContactId { get; set; }
        public int SupervisedWorkFaxContactId { get; set; }
        public string SupervisedLicenseNumber { get; set; }
        public string SupervisedStateLicensed { get; set; }
        public DateTime SupervisedLicenseExpirationDate { get; set; }
        public bool Areyousupervised { get; set; }
        public int SupervisorIndividualId { get; set; }
        public int SupervisorWorkAddressId { get; set; }
        public int SupervisorMailingAddressId { get; set; }
        public int SupervisorWorkPhoneContactId { get; set; }
        public int SupervisorWorkEmailContactId { get; set; }
        public int SupervisorWorkFaxContactId { get; set; }
        public string SupervisorLicenseNumber { get; set; }
        public string SupervisorStateLicensed { get; set; }
        public DateTime SupervisorLicenseExpirationDate { get; set; }
        public string ReferenceNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Guid IndividualSupervisoryInfoGuid { get; set; }
    }
}
