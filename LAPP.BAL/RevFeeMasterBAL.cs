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
    public class RevFeeMasterBAL : BaseBAL
    {
        RevFeeMasterDAL objDal = new RevFeeMasterDAL();

        public int Save_RevFeeMaster(RevFeeMaster objRevFeeMaster)
        {
            return objDal.Save_RevFeeMaster(objRevFeeMaster);
        }

        public List<RevFeeMaster> Get_All_RevFeeMaster()
        {
            return objDal.Get_All_RevFeeMaster();
        }

        public RevFeeMaster Get_RevFeeMaster_By_RevFeeMasterId(int ID)
        {
            return objDal.Get_RevFeeMaster_By_RevFeeMasterId(ID);
        }

        public List<RevFeeMaster> Get_RevFeeMaster_By_LicenseTypeId(int? licenseTypeId)
        {
            return objDal.Get_RevFeeMaster_By_LicenseTypeId(licenseTypeId);
        }
    }
}
