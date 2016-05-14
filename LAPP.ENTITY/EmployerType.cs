using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class EmployerTypeRequest : BaseEntity

    {
        public int EmployerTypeId { get; set; }
        public string EmployerTypeCode { get; set; }
        public string EmployerTypeName { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime EndDate { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }

    }
    public class EmployerTypeResponse : BaseEntityServiceResponse

    {
        public List<EmployerTypeRequest> EmployerType { get; set; }


    }
    public class EmployerType : EmployerTypeRequest

    {

        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
