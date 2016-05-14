using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class IndividualAccessCode : BaseEntity

    {
        public int IndividualAccessCodeId { get; set; }
        public int IndividualId { get; set; }
        public string AccessCodeHash { get; set; }
        public string AccessCodeSalt { get; set; }
        public DateTime AccessCodeExpirationDate { get; set; }
        public DateTime AccessCodeChangedOn { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string LastLoginIp { get; set; }
        public DateTime EulaAcceptedOn { get; set; }
        public int SourceId { get; set; }
        public bool IsPending { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string IndividualAccessCodeGuid { get; set; }
    }
}
