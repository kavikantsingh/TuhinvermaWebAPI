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
    public class LicenseTypeBAL : BaseBAL
    {
        LicenseTypeDAL objDal = new LicenseTypeDAL();

        public int Save_LicenseType(LicenseType objLicenseType)
        {
            return objDal.Save_LicenseType(objLicenseType);
        }

        public List<LicenseType> Get_All_LicenseType()
        {
            return objDal.Get_All_LicenseType();
        }


    }
}
