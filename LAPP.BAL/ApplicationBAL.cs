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
    public class ApplicationBAL : BaseBAL
    {
        ApplicationDAL objDal = new ApplicationDAL();

        public int Save_Application(Application objaddress)
        {
            return objDal.Save_Application(objaddress);
        }

        public List<Application> Get_All_Application()
        {
            return objDal.Get_All_Application();
        }

        public List<Application> Get_Application_By_IndividualId(int IndividualId)
        {
            return objDal.Get_Application_By_IndividualId(IndividualId);
        }

        public Application Get_Application_By_ApplicationId(int applicationId)
        {
            return objDal.Get_Application_By_ApplicationId(applicationId);
        }
        public ApplicationCount Get_DashboardApplicationCountRenewal()
        {
            return objDal.Get_DashboardApplicationCountRenewal();
        }
        public ApplicationCount Get_DashboardApplicationCountApplications()
        {
            return objDal.Get_DashboardApplicationCountApplications();
        }
    }
}
