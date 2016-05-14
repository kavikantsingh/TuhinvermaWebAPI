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
    public class IndividualApplicationBAL : BaseBAL
    {
        IndividualApplicationDAL objDal = new IndividualApplicationDAL();

        public int Save_IndividualApplication(IndividualApplication objaddress)
        {
            return objDal.Save_IndividualApplication(objaddress);
        }

        public List<IndividualApplication> Get_All_IndividualApplication()
        {
            return objDal.Get_All_IndividualApplication();
        }


        public IndividualApplication Get_IndividualApplication_byIndividualId(int IndividualId)
        {
            return objDal.Get_IndividualApplication_byIndividualId(IndividualId);
        }

    }
}
