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
    public class IndividualCECourseBAL : BaseBAL
    {
        IndividualCECourseDAL objDal = new IndividualCECourseDAL();
        public int Save_IndividualCECourse(IndividualCECourse objIndividualCECourse)
        {
            return objDal.Save_IndividualCECourse(objIndividualCECourse);
        }

        public List<IndividualCECourse> Get_All_IndividualCECourse()
        {
            return objDal.Get_All_IndividualCECourse();
        }
        public List<IndividualCECourse> Get_IndividualCECourse_By_IndividualLicenseId(int IndividualLicenseId)
        {
            return objDal.Get_IndividualCECourse_By_IndividualLicenseId(IndividualLicenseId);
        }
        public List<IndividualCECourse> Get_IndividualCECourse_By_IndividualId(int IndividualId)
        {
            return objDal.Get_IndividualCECourse_By_IndividualId(IndividualId);
        }
        public void IndividualCECourse_SoftDelete_by_ApplicationId(int _ApplicationId)
        {
            objDal.IndividualCECourse_SoftDelete_by_ApplicationId(_ApplicationId);
        }
        public List<IndividualCECourse> Get_IndividualCECourse_By_ApplicationId(int ApplicationId)
        {
            return objDal.Get_IndividualCECourse_By_ApplicationId(ApplicationId);
        }

        public IndividualCECourse Get_IndividualCECourse_By_IndividualCECourseId(int ID)
        {
            return objDal.Get_IndividualCECourse_By_IndividualCECourseId(ID);
        }

    }
}
