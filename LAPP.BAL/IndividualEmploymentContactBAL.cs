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
    public class IndividualEmploymentContactBAL : BaseBAL
    {
        IndividualEmploymentContactDAL objDal = new IndividualEmploymentContactDAL();
        public int Save_IndividualEmploymentContact(IndividualEmploymentContact objIndividualEmploymentContact)
        {
            return objDal.Save_IndividualEmploymentContact(objIndividualEmploymentContact);
        }

        public List<IndividualEmploymentContact> Get_All_IndividualEmploymentContact()
        {
            return objDal.Get_All_IndividualEmploymentContact();
        }
        public List<IndividualEmploymentContact> Get_IndividualEmploymentContact_By_IndividualEmploymentId(int IndividualEmploymentId)
        {
            return objDal.Get_IndividualEmploymentContact_By_IndividualEmploymentId(IndividualEmploymentId);
        }


        public IndividualEmploymentContact Get_IndividualEmploymentContact_By_IndividualEmploymentContactId(int ID)
        {
            return objDal.Get_IndividualEmploymentContact_By_IndividualEmploymentContactId(ID);
        }

    }
}
