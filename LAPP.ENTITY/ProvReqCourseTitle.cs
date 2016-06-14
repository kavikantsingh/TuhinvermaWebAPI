using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class ProvReqCourseTitle : BaseEntity
    {
        public int ProvReqCourseTitleId { get; set; }
        public int ProvReqCourseofStudyId { get; set; }
        public int ProviderId { get; set; }
        public int ApplicationId { get; set; }
        public string CourseTitleName { get; set; }
        public int CourseHours { get; set; }
        public string ReferenceNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ProviderOtherProgramGuid { get; set; }
    }

    public class ProvReqCourseTitleResponse : BaseEntityServiceResponse
    {
        public object ProvReqCourseTitle { get; set; }
    }
    }
