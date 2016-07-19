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
        ProvReqCourseTitleDAL objDAL = new ProvReqCourseTitleDAL();
        public int Save_ProvReqCourseTitle(ProvReqCourseTitle objProvReqCourseTitle)
        {
            return objDAL.Save_ProvReqCourseTitle(objProvReqCourseTitle);
        }

        public List<ProvReqCourseTitle> Get_All_ProvReqCourseTitle_By_CourseOfStudyId(int CourseOfStudyId, int ProviderId, int appid)
        {
            return objDAL.Get_All_ProvReqCourseTitle_By_CourseOfStudyId(CourseOfStudyId, ProviderId, appid);
        }

        public int Update_ProvReqCourseTitle(ProvReqCourseTitle objProvReqCourseTitle)
        {
            return objDAL.Update_ProvReqCourseTitle(objProvReqCourseTitle);
        }

        public int Delete_ProvReqCourseTitle(ProvReqCourseTitle objProvReqCourseTitle)
        {
            return objDAL.Delete_ProvReqCourseTitle(objProvReqCourseTitle);
        }

        public List<ProvReqCourseTitle> Get_ProvReqCourseTitle_By_ProvReqCourseTitleId(int CourseTitleId, int ProviderId)
        {
            return objDAL.Get_ProvReqCourseTitle_By_ProvReqCourseTitleId(CourseTitleId, ProviderId);
        }
    }
}
