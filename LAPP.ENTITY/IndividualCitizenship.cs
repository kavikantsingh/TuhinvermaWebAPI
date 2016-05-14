using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class IndividualCitizenship : BaseEntity

    {
        public int IndividualCitizenshipId { get; set; }
        public int IndividualId { get; set; }
        public bool IsUSCitizen { get; set; }
        public bool IsPermanentResidence { get; set; }
        public bool IsOnVisa { get; set; }
        public int VisaTypeId { get; set; }
        public int CitizenshipCountryId { get; set; }
        public string DHSStatus { get; set; }
        public string A { get; set; }
        public DateTime VisaBeginDate { get; set; }
        public DateTime VisaEndDate { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public string IndividualCitizenshipGuid { get; set; }
    }
}
