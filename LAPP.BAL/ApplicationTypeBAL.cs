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
    public class ApplicationTypeBAL : BaseBAL
    {
        ApplicationTypeDAL objDal = new ApplicationTypeDAL();

        public int Save_ApplicationType(ApplicationType objaddress)
        {
            return objDal.Save_ApplicationType(objaddress);
        }

        public List<ApplicationType> Get_All_ApplicationType()
        {
            return objDal.Get_All_ApplicationType();
        }


        public ApplicationType Get_address_By_ApplicationTypeId(int ID)
        {
            return objDal.Get_ApplicationType_byApplicationTypeId(ID);
        }

    }
}
