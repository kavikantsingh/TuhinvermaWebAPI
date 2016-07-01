using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class ProviderGraduatesNumber : BaseEntity
    {
        public int ProviderGraduatesNumberId { get; set; }
        public int ProviderId { get; set; }
        public int ApplicationId { get; set; }
        public int CalendarYear { get; set; }
        public int CalendarYearEstGradCount { get; set; }
        public int CalendarYearActualGradCount { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ProviderGraduatesNumberGuid { get; set; }
        public string Action { get; set; }
    }

    public class ProviderGraduatesNumberGetResponse : BaseEntityServiceResponse
    {
        public List<ProviderGraduatesNumber> ProviderGraduatesNumberList { get; set; }
    }
}
