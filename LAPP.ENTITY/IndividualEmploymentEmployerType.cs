using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class IndividualEmploymentEmployerType : BaseEntity

    {
        public int IndividualEmploymentEmployerTypeId { get; set; }
        public int IndividualId { get; set; }
        public int IndividualEmploymentId { get; set; }
        public int EmployerTypeId { get; set; }
        public bool EmployerTypeValue { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid IndividualEmploymentEmployerTypeGuid { get; set; }
    }


    public class IndividualEmploymentEmployerTypeGet : BaseEntity

    {
        public int IndividualEmploymentEmployerTypeId { get; set; }
        public int IndividualId { get; set; }
        public int IndividualEmploymentId { get; set; }
        public int EmployerTypeId { get; set; }
        public bool EmployerTypeValue { get; set; }
        public bool IsActive { get; set; }
    }

    public class IndividualEmploymentEmployerTypeGetResponse : BaseEntityServiceResponse
    {
        public List<IndividualEmploymentEmployerTypeGet> IndividualEmploymentEmployerTypeGetList { get; set; }
    }
}
