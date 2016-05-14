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
    public class ContactTypeBAL : BaseBAL
    {
        ContacttypeDAL objDal = new ContacttypeDAL();
        public int Save_Contacttype(ContactType objContacttype)
        {
            return objDal.Save_Contacttype(objContacttype);
        }
        //public int Update_Contacttype(ContactType objContacttype)
        //{
        //    return objDal.Update_Contacttype(objContacttype);
        //}
        public ContactType Get_Contacttype_byContacttypeId(int ContactTypeId)
        {
            return objDal.Get_Contacttype_byContacttypeId(ContactTypeId);
        }
        public List<ContactType> Get_All_Contacttype()
        {
            return objDal.Get_All_Contacttype();
        }


    }
}
