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
    public class IndividualOtherBAL : BaseBAL
    {
        IndividualOtherDAL objDal = new IndividualOtherDAL();

        public int Save_IndividualOther(IndividualOther objaddress)
        {
            return objDal.Save_IndividualOther(objaddress);
        }

        public List<IndividualOther> Get_All_IndividualOther()
        {
            return objDal.Get_All_IndividualOther();
        }


        public IndividualOther Get_address_By_IndividualOtherId(int ID)
        {
            return objDal.Get_IndividualOther_By_IndividualOtherId(ID);
        }

    }
}
