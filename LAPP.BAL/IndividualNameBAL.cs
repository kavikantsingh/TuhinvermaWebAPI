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
    public class IndividualNameBAL : BaseBAL
    {
        IndividualNameDAL objDal = new IndividualNameDAL();

        public int Save_IndividualName(IndividualName objaddress)
        {
            return objDal.Save_IndividualName(objaddress);
        }

        public List<IndividualName> Get_All_IndividualName()
        {
            return objDal.Get_All_IndividualName();
        }
        public List<IndividualName> Get_IndividualName_By_IndividualIdANDIndividualNameTypeId(int IndividualId, int IndividualNameTypeId)
        {
            return objDal.Get_IndividualName_By_IndividualIdANDIndividualNameTypeId(IndividualId, IndividualNameTypeId);
        }
        public List<IndividualName> Get_IndividualName_By_IndividualId(int IndividualId)
        {
            return objDal.Get_IndividualName_By_IndividualId(IndividualId);
        }

        public IndividualName Get_IndividualName_By_IndividualNameId(int ID)
        {
            return objDal.Get_IndividualName_By_IndividualNameId(ID);
        }

    }
}
