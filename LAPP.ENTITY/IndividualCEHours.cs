using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class IndividualCEHours : IndividualCEHResponse

    {

        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Guid IndividualCEHoursGuid { get; set; }
    }

    public class IndividualCEHResponse : BaseEntity
    {
        public int IndividualCEHoursId { get; set; }
        public int IndividualId { get; set; }
        public int ApplicationId { get; set; }
        public int CEHoursTypeId { get; set; }
        public DateTime? CEHoursStartDate { get; set; }
        public DateTime? CEHoursEndDate { get; set; }
        public DateTime? CEHoursDueDate { get; set; }
        public int CEHoursReportingYear { get; set; }
        public int CEHoursStatusId { get; set; }
        public decimal CECarryInHours { get; set; }
        public decimal CERequiredHours { get; set; }
        public decimal CECurrentReportedHours { get; set; }
        public decimal CERolloverHours { get; set; }
        public string ReferenceNumber { get; set; }
        public bool IsActive { get; set; }
        public int? IndividualLicenseId { get; set; }


    }
}
