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
    public class IndividualAddressBAL : BaseBAL
    {
        IndividualAddressDAL objDal = new IndividualAddressDAL();
        public int Save_IndividualAddress(IndividualAddress objIndividualAddress)
        {
            return objDal.Save_IndividualAddress(objIndividualAddress);
        }

        public List<IndividualAddress> Get_All_IndividualAddress()
        {
            return objDal.Get_All_IndividualAddress();
        }


        public IndividualAddress Get_IndividualAddress_By_IndividualAddressId(int ID)
        {
            return objDal.Get_IndividualAddress_By_IndividualAddressId(ID);
        }

        public List<IndividualAddress> Get_IndividualAddress_By_IndividualId(int IndividualId)
        {
            return objDal.Get_IndividualAddress_By_IndividualId(IndividualId);
        }
    }
}
