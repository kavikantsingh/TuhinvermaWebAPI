using LAPP.DAL;
using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.BAL
{
    public class DivisionBAL
    {
        DivisionDAL objDAL = new DivisionDAL();

        public int Save_Division(Division objDivision)
        {
            return objDAL.Save_Division(objDivision);
        }

        //public int Update_Division(Division objDivision)
        //{
        //    return objDAL.Update_Division(objDivision);
        //}

        public Division Get_Division_byDivisionId(int ID)
        {
            return objDAL.Get_Division_byDivisionId(ID);
        }

        public List<Division> Get_All_Division()
        {
            return objDAL.Get_All_Division();
        }

    }
}
