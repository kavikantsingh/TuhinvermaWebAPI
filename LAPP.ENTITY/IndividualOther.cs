using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class IndividualOther : BaseEntity
    {
        public int IndividualOtherId { get; set; }
        public int IndividualId { get; set; }
        public bool IsNameChanged { get; set; }
        public string PlaceofBirthCity { get; set; }
        public string PlaceofBirthState { get; set; }
        public int PlaceofBirthCountry { get; set; }
        public string Picture { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public string IndividualOtherGuid { get; set; }

    }
}
