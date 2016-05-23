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
    public class IndividualVeteranBAL : BaseBAL
    {
        IndividualVeteranDAL objDal = new IndividualVeteranDAL();

        public int Save_IndividualVeteran(IndividualVeteran objaddress)
        {
            return objDal.Save_IndividualVeteran(objaddress);
        }

        public List<IndividualVeteran> Get_All_IndividualVeteran()
        {
            return objDal.Get_All_IndividualVeteran();
        }

        public IndividualVeteran Get_address_By_IndividualVeteranId(int ID)
        {
            return objDal.Get_IndividualVeteran_By_IndividualVeteranId(ID);
        }
 public IndividualVeteran Get_IndividualVeteran_By_IndividualId(int ID)
        {
            return objDal.Get_IndividualVeteran_By_IndividualId(ID);
        }
    }
}
