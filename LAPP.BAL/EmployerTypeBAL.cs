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
    public class EmployerTypeBAL : BaseBAL
    {
        EmployerTypeDAL objDal = new EmployerTypeDAL();
        public int Save_EmployerType(EmployerType objEmployerType)
        {
            return objDal.Save_EmployerType(objEmployerType);
        }

        public List<EmployerType> Get_All_EmployerType()
        {
            return objDal.Get_All_EmployerType();
        }


    }
}
