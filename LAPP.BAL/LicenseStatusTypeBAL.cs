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
    public class LicenseStatusTypeBAL : BaseBAL
    {
        LicenseStatusTypeDAL objDal = new LicenseStatusTypeDAL();

        public int Save_LicenseStatusType(LicenseStatusType objLicenseStatusType)
        {
            return objDal.Save_LicenseStatusType(objLicenseStatusType);
        }

        public List<LicenseStatusType> Get_All_LicenseStatusType()
        {
            return objDal.Get_All_LicenseStatusType();
        }


    }
}
