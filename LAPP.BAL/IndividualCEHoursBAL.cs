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
    public class IndividualCEHoursBAL : BaseBAL
    {
        IndividualCEHoursDAL objDal = new IndividualCEHoursDAL();
        public int Save_IndividualCEHours(IndividualCEHours objIndividualCEHours)
        {
            return objDal.Save_IndividualCEHours(objIndividualCEHours);
        }

        public List<IndividualCEHours> Get_All_IndividualCEHours()
        {
            return objDal.Get_All_IndividualCEHours();
        }


        public IndividualCEHours Get_IndividualCEHours_By_IndividualCEHoursId(int ID)
        {
            return objDal.Get_IndividualCEHours_By_IndividualCEHoursId(ID);
        }

        public List<IndividualCEHours> Get_IndividualCEHours_By_IndividualId(int IndividualId)
        {
            return objDal.Get_IndividualCEHours_By_IndividualId(IndividualId);
        }

        public IndividualCEHours Get_Top_IndividualCEHours_By_IndividualId(int individualId)
        {
            return objDal.Get_Top_IndividualCEHours_By_IndividualId(individualId);
        }

        public IndividualCEHours Get_IndividualCEHours_By_IndividualLicenseId(int individualLicenseId)
        {
            return objDal.Get_IndividualCEHours_By_IndividualLicenseId(individualLicenseId);
        }
    }
}
