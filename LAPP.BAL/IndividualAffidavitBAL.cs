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
    public class IndividualAffidavitBAL : BaseBAL
    {
        IndividualAffidavitDAL objDal = new IndividualAffidavitDAL();

        public int Save_IndividualAffidavit(IndividualAffidavit objaddress)
        {
            return objDal.Save_IndividualAffidavit(objaddress);
        }

        public List<IndividualAffidavit> Get_All_IndividualAffidavit()
        {
            return objDal.Get_All_IndividualAffidavit();
        }

        public IndividualAffidavit Get_address_By_IndividualAffidavitId(int ID)
        {
            return objDal.Get_IndividualAffidavit_By_IndividualAffidavitId(ID);
        }

        public IndividualAffidavit Get_IndividualAffidavit_By_IndividualId(int IndividualId)
        {
            return objDal.Get_IndividualAffidavit_By_IndividualId(IndividualId);
        }


    }
}
