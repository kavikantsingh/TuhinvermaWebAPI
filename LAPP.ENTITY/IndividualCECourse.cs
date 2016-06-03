using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class IndividualCECourseResponse : BaseEntity

    {
        public int IndividualCECourseId { get; set; }
        public int IndividualId { get; set; }
        public int? ApplicationId { get; set; }
        public int? CECourseTypeId { get; set; }
        public int? CECourseActivityTypeId { get; set; }
        public DateTime? CECourseStartDate { get; set; }
        public DateTime? CECourseEndDate { get; set; }
        public DateTime? CECourseDueDate { get; set; }
        public DateTime? CECourseDate { get; set; }
        public decimal CECourseHours { get; set; }
        public decimal CECourseUnits { get; set; }
        public string ProgramSponsor { get; set; }
        public string CourseNameTitle { get; set; }
        public string CourseSponsor { get; set; }
        public int? CECourseReportingYear { get; set; }
        public int? CECourseStatusId { get; set; }
        public string InstructorBiography { get; set; }
        public string ActivityDesc { get; set; }
        public string ReferenceNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class IndividualCECourse : IndividualCECourseResponse

    {

        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string IndividualCECourseGuid { get; set; }
    }

    public class IndividualCECourseResponseRequest : BaseEntityServiceResponse
    {
        public List<IndividualCECourseResponse> IndividualCECourseResponseList { get; set; }
    }

}
