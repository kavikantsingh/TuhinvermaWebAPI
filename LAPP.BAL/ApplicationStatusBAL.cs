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
    public class ApplicationStatusBAL : BaseBAL
    {
        ApplicationStatusDAL objDal = new ApplicationStatusDAL();

        public int Save_ApplicationStatus(ApplicationStatus objaddress)
        {
            return objDal.Save_ApplicationStatus(objaddress);
        }

        public List<ApplicationStatus> Get_All_ApplicationStatus()
        {
            return objDal.Get_All_ApplicationStatus();
        }


        public ApplicationStatus Get_address_By_ApplicationStatusId(int ID)
        {
            return objDal.Get_ApplicationStatus_byApplicationStatusId(ID);
        }

    }
}
