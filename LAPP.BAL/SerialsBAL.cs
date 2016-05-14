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
    public class SerialsBAL : BaseBAL
    {
       

        public static string Get_Receipt_No()
        {
            SerialsDAL objDal = new SerialsDAL();
            return objDal.Get_Receipt_No();
        }
        public static string Get_License_Number()
        {
            SerialsDAL objDal = new SerialsDAL();
            return objDal.serial_get_for_License_Number();
        }


        public static string  Get_Application_Number()
        {
            SerialsDAL objDal = new SerialsDAL();
            return objDal.serial_get_for_ApplicationNumber();
        }



    }
}
