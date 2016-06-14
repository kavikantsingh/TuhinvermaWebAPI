using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class ProvClinicHours: BaseEntity
    {
        public int ProvClinicHoursId { get; set; }
        public int ProviderId { get; set; }
        public int ApplicationId { get; set; }
        public int ClinicHours { get; set; }
        public string ReferenceNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ProvClinicHoursGuid { get; set; }
    }

    public class ProvClinicHoursResponse : BaseEntityServiceResponse
    {
        public object ProvClinicHours { get; set; }
    }
 }
