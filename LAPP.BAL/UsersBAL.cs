using LAPP.DAL;
using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.BAL
{
    public class UsersBAL
    {
        UsersDAL objDAL = new UsersDAL();

        public int Save_Users(Users objUsers)
        {
            return objDAL.Save_Users(objUsers);
        }

        //public int Update_Users(Users objUsers)
        //{
        //    return objDAL.Update_Users(objUsers);
        //}

        public int Individual_User_Save(Users objUsers)
        {
            return objDAL.Individual_User_Save(objUsers);

        }

        public Users Get_Users_byUsersId(int ID)
        {
            return objDAL.Get_Users_byUserId(ID);
        }

        public Users Get_Users_by_Email(string Email)
        {
            return objDAL.Get_Users_by_Email(Email);
        }

        public Users Get_Users_by_Email_And_Password(string Email, string Password)
        {
            return objDAL.Get_Users_by_Email_And_Password(Email, Password);
        }

        public List<Users> Get_All_Users()
        {
            return objDAL.Get_All_Users();
        }

        public List<UsersSearch> Search_Users(UsersSearch objUsers)
        {
            return objDAL.Search_Users(objUsers);
        }

        public Users Get_Users_byIndividualId(int individualId)
        {
            return objDAL.Get_Users_byIndividualId(individualId);
        }
    }
}
