using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using LAPP.ENTITY;
using LAPP.DAL;

namespace LAPP.BAL
{
    public class ProvReqCourseTitleBAL
    {
        ProvReqCourseTitleDAL objBAL = new ProvReqCourseTitleDAL();
        public int Save_ProvReqCourseTitle(ProvReqCourseTitle objProvReqCourseTitle)
        {
            return objBAL.Save_ProvReqCourseTitle(objProvReqCourseTitle);
        }

        public List<ProvReqCourseTitle> Get_All_ProvReqCourseTitle_By_CourseOfStudyId(int CourseOfStudyId, int ProviderId)
        {
            return objBAL.Get_All_ProvReqCourseTitle_By_CourseOfStudyId(CourseOfStudyId, ProviderId);
        }
    }
}
