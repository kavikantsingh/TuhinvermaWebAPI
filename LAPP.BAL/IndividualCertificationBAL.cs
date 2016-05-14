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
    public class IndividualCertificationBAL : BaseBAL
    {
        IndividualCertificationDAL objDal = new IndividualCertificationDAL();

        public int Save_IndividualCertification(IndividualCertification objaddress)
        {
            return objDal.Save_IndividualCertification(objaddress);
        }

        public List<IndividualCertification> Get_All_IndividualCertification()
        {
            return objDal.Get_All_IndividualCertification();
        }

        public IndividualCertification Get_IndividualCertification_By_IndividualId(int IndividualId)
        {
            return objDal.Get_IndividualCertification_By_IndividualId(IndividualId);
        }

        public IndividualCertification Get_address_By_IndividualCertificationId(int ID)
        {
            return objDal.Get_IndividualCertification_By_IndividualCertificationId(ID);
        }

    }
}
