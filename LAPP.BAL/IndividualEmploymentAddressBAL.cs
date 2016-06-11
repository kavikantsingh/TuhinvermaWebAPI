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
    public class IndividualEmploymentAddressBAL : BaseBAL
    {
        IndividualEmploymentAddressDAL objDal = new IndividualEmploymentAddressDAL();
        public int Save_IndividualEmploymentAddress(IndividualEmploymentAddress objIndividualEmploymentAddress)
        {
            return objDal.Save_IndividualEmploymentAddress(objIndividualEmploymentAddress);
        }

        public List<IndividualEmploymentAddress> Get_All_IndividualEmploymentAddress()
        {
            return objDal.Get_All_IndividualEmploymentAddress();
        }

        public List<IndividualEmploymentAddress> Get_IndividualEmploymentAddress_By_IndividualEmploymentId(int IndividualEmploymentId)
        {
            return objDal.Get_IndividualEmploymentAddress_By_IndividualEmploymentId(IndividualEmploymentId);
        }
        public List<IndividualEmploymentAddress> Get_individualempaddress_GetTopOne_By_IndividualEmpId(int IndividualEmploymentId)
        {
            return objDal.Get_individualempaddress_GetTopOne_By_IndividualEmpId(IndividualEmploymentId);
        }
        public IndividualEmploymentAddress Get_IndividualEmploymentAddress_By_IndividualEmploymentAddressId(int ID)
        {
            return objDal.Get_IndividualEmploymentAddress_By_IndividualEmploymentAddressId(ID);
        }

    }
}
