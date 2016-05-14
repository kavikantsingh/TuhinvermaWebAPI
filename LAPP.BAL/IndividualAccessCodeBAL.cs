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
    public class IndividualAccessCodeBAL : BaseBAL
    {
        IndividualAccessCodeDAL objDal = new IndividualAccessCodeDAL();
        public int Save_IndividualAccessCode(IndividualAccessCode objIndividualAccessCode)
        {
            return objDal.Save_IndividualAccessCode(objIndividualAccessCode);
        }

        public List<IndividualAccessCode> Get_All_IndividualAccessCode()
        {
            return objDal.Get_All_IndividualAccessCode();
        }


        public IndividualAccessCode Get_IndividualAccessCode_By_IndividualAccessCodeId(int ID)
        {
            return objDal.Get_IndividualAccessCode_By_IndividualAccessCodeId(ID);
        }

    }
}
