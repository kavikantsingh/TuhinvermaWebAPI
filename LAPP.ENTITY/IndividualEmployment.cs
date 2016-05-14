using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class IndividualEmployment : IndividualEmploymentResponse

    {
       
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Guid IndividualEmploymentGuid { get; set; }
    }

    public class IndividualEmploymentResponse : BaseEntity
    {
        public int IndividualEmploymentId { get; set; }
        public int IndividualId { get; set; }
        public int ApplicationId { get; set; }
        public int ProviderId { get; set; }
        public int EmploymentHistoryTypeId { get; set; }
        public DateTime EmploymentStartDate { get; set; }
        public DateTime EmploymentEndDate { get; set; }
        public int EmploymentStatusId { get; set; }
        public int EmploymentTypeId { get; set; }
        public int PositionId { get; set; }
        public bool IsWorkinginFieldofApplication { get; set; }
        public bool EverWorkedinFieldofApplication { get; set; }
        public string ReferenceNumber { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public List<IndividualEmploymentAddressResponse> EmploymentAddress { get; set; }
        public List<IndividualEmploymentContactResponse> EmploymentContact { get; set; }


    }
}
