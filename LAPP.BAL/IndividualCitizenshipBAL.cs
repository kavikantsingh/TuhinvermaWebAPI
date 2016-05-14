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
    public class IndividualCitizenshipBAL : BaseBAL
    {
        IndividualCitizenshipDAL objDal = new IndividualCitizenshipDAL();

        public int Save_IndividualCitizenship(IndividualCitizenship objIndividualCitizenship)
        {
            return objDal.Save_IndividualCitizenship(objIndividualCitizenship);
        }

        public List<IndividualCitizenship> Get_All_IndividualCitizenship()
        {
            return objDal.Get_All_IndividualCitizenship();
        }

        public IndividualCitizenship Get_IndividualCitizenship_By_IndividualCitizenshipId(int ID)
        {
            return objDal.Get_IndividualCitizenship_By_IndividualCitizenshipId(ID);
        }

    }
}
