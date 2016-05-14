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
    public class IndividualOtherNameBAL : BaseBAL
    {
        IndividualOtherNameDAL objDal = new IndividualOtherNameDAL();

        public int Save_IndividualOtherName(IndividualOtherName objaddress)
        {
            return objDal.Save_IndividualOtherName(objaddress);
        }

        public List<IndividualOtherName> Get_All_IndividualOtherName()
        {
            return objDal.Get_All_IndividualOtherName();
        }

        public IndividualOtherName Get_address_By_IndividualOtherNameId(int ID)
        {
            return objDal.Get_IndividualOtherName_By_IndividualOtherNameId(ID);
        }

    }
}
