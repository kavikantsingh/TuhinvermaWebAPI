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
    public class LicenseRequirementBAL : BaseBAL
    {
        LicenseRequirementDAL objDal = new LicenseRequirementDAL();

        public int Save_LicenseRequirement(LicenseRequirement objaddress)
        {
            return objDal.Save_LicenseRequirement(objaddress);
        }

        public List<LicenseRequirement> Get_All_LicenseRequirement()
        {
            return objDal.Get_All_LicenseRequirement();
        }


        public LicenseRequirement Get_address_By_LicenseRequirementId(int ID)
        {
            return objDal.Get_LicenseRequirement_byLicenseRequirementId(ID);
        }

    }
}
