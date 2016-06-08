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
    public class AddressBAL : BaseBAL
    {
        AddressDAL objDal = new AddressDAL();
        public int Save_address(Address objaddress)
        {
            return objDal.Save_address(objaddress);
        }

        public List<Address> Get_All_address()
        {
            return objDal.Get_All_address();
        }
        
        public Address Get_address_By_AddressId(int ID)
        {
            return objDal.Get_address_By_AddressId(ID);
        }

        public int SaveAddressRequestFromSchoolInformationTab(Address objaddress)
        {
            return objDal.SaveAddressRequestFromSchoolInformationTab(objaddress);
        }

        public List<Address> GetAllPreviousAddress(int addressTypeId, int providerId)
        {
            return objDal.GetAllPreviousAddress(addressTypeId, providerId);
        }

    }
}
