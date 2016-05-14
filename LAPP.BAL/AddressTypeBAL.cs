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
    public class AddressTypeBAL : BaseBAL
    {
        AddressTypeDAL objDal = new AddressTypeDAL();
        public int Save_AddressType(AddressType objAddressType)
        {
            return objDal.Save_AddressType(objAddressType);
        }

        //public int Update_AddressType(AddressType objAddressType)
        //{
        //    return objDal.Update_AddressType(objAddressType);
        //}

        public AddressType Get_AddressType_byAddressTypeId(int AddressTypeId)
        {
            
            return objDal.Get_AddressType_byAddressTypeId(AddressTypeId);
        }

        public List<AddressType> Get_All_AddressType()
        {
            return objDal.Get_All_AddressType();
        }


    }
}
