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
    public class ProvReqCourseOfStudyBAL
    {
        ProvReqCourseOfStudyDAL objDal = new ProvReqCourseOfStudyDAL();
        public List<ProvReqCourseOfStudy> Get_All_ProvReqCourseOfStudy()
        {
            return objDal.Get_All_ProvReqCourseOfStudy();
        }
    }
}
