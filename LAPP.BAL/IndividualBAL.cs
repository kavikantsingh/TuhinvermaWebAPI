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
    public class IndividualBAL : BaseBAL
    {
        IndividualDAL objDal = new IndividualDAL();
        public int Save_Individual(Individual objIndividual)
        {
            return objDal.Save_Individual(objIndividual);
        }

        public List<Individual> Get_All_Individual()
        {
            return objDal.Get_All_Individual();
        }


        public Individual Get_Individual_By_IndividualId(int ID)
        {
            return objDal.Get_Individual_By_IndividualId(ID);
        }

    }
}
