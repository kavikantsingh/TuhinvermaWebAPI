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
    public class ApplicationStatusReasonBAL : BaseBAL
    {
        ApplicationStatusReasonDAL objDal = new ApplicationStatusReasonDAL();

        public int Save_ApplicationStatusReason(ApplicationStatusReason objaddress)
        {
            return objDal.Save_ApplicationStatusReason(objaddress);
        }

        public List<ApplicationStatusReason> Get_All_ApplicationStatusReason()
        {
            return objDal.Get_All_ApplicationStatusReason();
        }


        public ApplicationStatusReason Get_address_By_ApplicationStatusReasonId(int ID)
        {
            return objDal.Get_ApplicationStatusReason_byApplicationStatusReasonId(ID);
        }

    }
}
