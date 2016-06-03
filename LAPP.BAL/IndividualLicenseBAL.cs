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
    public class IndividualLicenseBAL : BaseBAL
    {
        IndividualLicenseDAL objDal = new IndividualLicenseDAL();
        public int Save_IndividualLicense(IndividualLicense objIndividualLicense)
        {
            return objDal.Save_IndividualLicense(objIndividualLicense);
        }

        public List<IndividualLicense> Get_All_IndividualLicense()
        {
            return objDal.Get_All_IndividualLicense();
        }
        public List<IndividualLicense> Get_IndividualLicense_By_IndividualId(int IndividualId)
        {
            return objDal.Get_IndividualLicense_By_IndividualId(IndividualId);
        }

        public List<IndividualLicense> GetALL_IndividualLicense_By_IndividualId(int IndividualId)
        {
            return objDal.GetALL_IndividualLicense_By_IndividualId(IndividualId);
        }
        public IndividualLicense Get_IndividualLicense_By_IndividualLicenseId(int ID)
        {
            return objDal.Get_IndividualLicense_By_IndividualLicenseId(ID);
        }

        public IndividualLicense Get_Pending_IndividualLicense_By_IndividualId(int individualId)
        {
            return objDal.Get_Pending_IndividualLicense_By_IndividualId(individualId);
        }

        public IndividualLicense Get_Latest_IndividualLicense_By_IndividualId(int individualId)
        {
            return objDal.Get_Latest_IndividualLicense_By_IndividualId(individualId);
        }
    }
}
