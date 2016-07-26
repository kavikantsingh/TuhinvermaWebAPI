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
        public int Update_Individual(IndividualLoadResponse objIndividual)
        {
            return objDal.Update_Individual(objIndividual);
        }
        public List<Individual> Get_All_Individual()
        {
            return objDal.Get_All_Individual();
        }
        public List<Individual> Search_Individual(IndividualSearch obj)
        {
            return objDal.Search_Individual(obj);
        }
        public List<Individual> Search_Individual_WithPager(IndividualSearch obj, int CurrentPage, int PagerSize)
        {
            return objDal.Search_Individual_WithPager(obj, CurrentPage, PagerSize);
        }

        public List<Individual> Search_Renewal(RenewalApplication obj)
        {
            return objDal.Search_Renewal(obj);
        }
        //public List<Individual> Search_RenewalWithPager(RenewalApplication obj, int CurrentPage, int PagerSize)
        //{
        //    return objDal.Search_RenewalWithPager(obj, CurrentPage, PagerSize);
        //}

        public Individual Get_Individual_By_IndividualId(int ID)
        {
            return objDal.Get_Individual_By_IndividualId(ID);
        }

        public Individual Get_Individual_By_LastNameSSNCodeLicenseNumber(string lastName, string licenseNumber, string SSNCode)
        {
            return objDal.Get_Individual_By_LastNameSSNCodeLicenseNumber(lastName, licenseNumber, SSNCode);
        }

        public int Save_IndividualProvider(IndividualName objIndividual)
        {
            return objDal.Save_IndividualProvider(objIndividual);
        }

        public List<IndividualName> Get_IndividualProvider(int providerid,int induvidualtypeid)
        {
            return objDal.Get_IndividualProvider(providerid, induvidualtypeid);
        }
    }
}
