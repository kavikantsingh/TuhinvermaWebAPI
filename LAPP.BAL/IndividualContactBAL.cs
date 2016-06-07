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
    public class IndividualContactBAL : BaseBAL
    {
        IndividualContactDAL objDal = new IndividualContactDAL();
        public int Save_IndividualContact(IndividualContact objIndividualContact)
        {
            return objDal.Save_IndividualContact(objIndividualContact);
        }

        public List<IndividualContact> Get_All_IndividualContact()
        {
            return objDal.Get_All_IndividualContact();
        }

        public List<IndividualContact> Get_IndividualContact_By_IndividualId(int IndividualId)
        {
            return objDal.Get_IndividualContact_By_IndividualId(IndividualId);
        }
        public IndividualContact Get_IndividualContact_By_IndividualContactId(int ID)
        {
            return objDal.Get_IndividualContact_By_IndividualContactId(ID);
        }

        public IndividualContact Get_Primary_IndividualContact_By_IndividualId(int IndividualId)
        {
            return objDal.Get_Primary_IndividualContact_By_IndividualId(IndividualId);
        }
    }
}
