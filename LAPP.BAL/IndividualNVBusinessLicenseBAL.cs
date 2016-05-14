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
    public class IndividualNVBusinessLicenseBAL : BaseBAL
    {
        IndividualNVBusinessLicenseDAL objDal = new IndividualNVBusinessLicenseDAL();

        public int Save_IndividualNVBusinessLicense(IndividualNVBusinessLicense objaddress)
        {
            return objDal.Save_IndividualNVBusinessLicense(objaddress);
        }

        public List<IndividualNVBusinessLicense> Get_All_IndividualNVBusinessLicense()
        {
            return objDal.Get_All_IndividualNVBusinessLicense();
        }

        public IndividualNVBusinessLicense Get_address_By_IndividualNVBusinessLicenseId(int ID)
        {
            return objDal.Get_IndividualNVBusinessLicense_By_IndividualNVBusinessLicenseId(ID);
        }

    }
}
