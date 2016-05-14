using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAPP.DAL;
using LAPP.ENTITY;
namespace LAPP.BAL
{
    public class CountryBAL : BaseBAL

    {
        CountryDAL objDAL = new CountryDAL();

        public int Save_Country(Country objCountry)
        {
            return objDAL.Save_Country(objCountry);
        }

        //public int Update_Country(Country objCountry)
        //{
        //    return objDAL.Update_Country(objCountry);
        //}
        public List<Country> GetAll_Country()
        {
            return objDAL.GetAll_Country();
        }

        public Country Get_Country_byID(int countryId)
        {
            return objDAL.Get_Country_byID(countryId);
        }
        public void Delete_Country_byID(int countryId)
        {
            objDAL.Delete_Country_byID(countryId);
        }
    }
}
