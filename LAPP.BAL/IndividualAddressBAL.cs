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
        public int Update_Individual_Address(IndividualAddressLoadResponse objAddress)
        {
            return objDal.Update_Individual_Address(objAddress);
        }
        public List<IndividualAddress> Get_All_IndividualAddress()
        {
            return objDal.Get_All_IndividualAddress();
        }

        public IndividualAddress Get_Current_IndividualAddress_By_IndividualId(int IndividualId)
        {
            return objDal.Get_Current_IndividualAddress_By_IndividualId(IndividualId);
        }
        public IndividualAddressLoadResponse Get_IndividualAddress_By_IndividualId(int IndividualId, int AddressTypeId)
        {
            return objDal.Get_IndividualAddress_By_IndividualId(IndividualId, AddressTypeId);
        }
        public IndividualAddress Get_IndividualAddress_By_IndividualAddressId(int ID)
        {
            return objDal.Get_IndividualAddress_By_IndividualAddressId(ID);
        }
        public List<IndividualAddress> Get_IndividualAddress_By_IndividualId(int IndividualId)
        {
            return objDal.Get_IndividualAddress_By_IndividualId(IndividualId);
        }
        public List<IndividualAddress> Get_ALL_IndividualAddress_By_IndividualId(int IndividualId)
        {
            return objDal.Get_ALL_IndividualAddress_By_IndividualId(IndividualId);
        }
    }
}
