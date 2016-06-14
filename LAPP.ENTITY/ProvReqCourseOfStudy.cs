using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class ProvReqCourseOfStudy:BaseEntity
    {
        public int ProvReqCourseofStudyId { get; set; }
        public int ProviderId { get; set; }
        public int ApplicationId { get; set; }
        public int ReqCourseofStudyNameId { get; set; }
        public string ReqCourseofStudyName { get; set; }
        public int MinimumReqCourseHours { get; set; }
        public string ReferenceNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ProvReqCourseofStudyGuid { get; set; }
    }

    public class ProvReqCourseOfStudyResponse: BaseEntityServiceResponse
    {
        public object ProvReqCourseOfStudy { get; set; }
    }
}
