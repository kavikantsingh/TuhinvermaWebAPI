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
    public class PriorPasswordBAL : BaseBAL
    {
        PriorPasswordDAL objDal = new PriorPasswordDAL();
        public int Save_PriorPassword(PriorPassword objPriorPassword)
        {
            return objDal.Save_PriorPassword(objPriorPassword);
        }

        public List<PriorPassword> Get_All_PriorPassword()
        {
            return objDal.Get_All_PriorPassword();
        }


        public PriorPassword Get_PriorPassword_By_PriorPasswordId(int ID)
        {
            return objDal.Get_PriorPassword_By_PriorPasswordId(ID);
        }

    }
}
