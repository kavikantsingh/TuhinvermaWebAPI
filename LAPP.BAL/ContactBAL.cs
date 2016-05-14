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
    public class ContactBAL : BaseBAL
    {
        ContactDAL objDal = new ContactDAL();
        public int Save_Contact(Contact objContact)
        {
            return objDal.Save_Contact(objContact);
        }
        //public int Update_Contact(Contact objContact)
        //{
        //    return objDal.Update_Contact(objContact);
        //}
        public List<Contact> Get_All_Contact()
        {
            return objDal.Get_All_Contact();
        }


        public Contact Get_Contact_By_ContactId(int ID)
        {
            return objDal.Get_Contact_By_ContactId(ID);
        }

    }
}
