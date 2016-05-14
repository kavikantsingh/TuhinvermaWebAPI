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
    public class EmployeeTypeBAL : BaseBAL
    {
        EmployeeTypeDAL objDal = new EmployeeTypeDAL();
        public int Save_EmployeeType(EmployeeType objEmployeeType)
        {
            return objDal.Save_EmployeeType(objEmployeeType);
        }

        public List<EmployeeType> Get_All_EmployeeType()
        {
            return objDal.Get_All_EmployeeType();
        }


    }
}
