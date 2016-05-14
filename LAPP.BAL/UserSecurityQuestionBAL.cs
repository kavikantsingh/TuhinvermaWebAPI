using LAPP.DAL;
using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.BAL
{
    public class UserSecurityQuestionBAL
    {
        UserSecurityQuestionDAL objDAL = new UserSecurityQuestionDAL();

        public int Save_UserSecurityQuestion(UserSecurityQuestion objUserSecurityQuestion)
        {
            return objDAL.Save_UserSecurityQuestion(objUserSecurityQuestion);
        }

        //public int Update_UserSecurityQuestion(UserSecurityQuestion objUserSecurityQuestion)
        //{
        //    return objDAL.Update_UserSecurityQuestion(objUserSecurityQuestion);
        //}

        public UserSecurityQuestion Get_UserSecurityQuestion_byUserSecurityQuestionId(int ID)
        {
            return objDAL.Get_UserSecurityQuestion_byUserSecurityQuestionId(ID);
        }

        public UserSecurityQuestion Get_UserSecurityQuestion_byUserId(int UserId)
        {
            return objDAL.Get_UserSecurityQuestion_byUserId(UserId);
        }
        public List<UserSecurityQuestion> GetAll_UserSecurityQuestion_byUserId(int UserId)
        {
            return objDAL.GetAll_UserSecurityQuestion_byUserId(UserId);

        }
        public List<UserSecurityQuestion> Get_All_UserSecurityQuestion()
        {
            return objDAL.Get_All_UserSecurityQuestion();
        }

    }
}
