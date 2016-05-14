using LAPP.DAL;
using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.BAL
{
    public class UserDivisionBAL
    {
        UserDivisionDAL objDAL = new UserDivisionDAL();

        public int Save_UserDivision(UserDivision objUserDivision)
        {
            return objDAL.Save_UserDivision(objUserDivision);
        }

        //public int Update_UserDivision(UserDivision objUserDivision)
        //{
        //    return objDAL.Update_UserDivision(objUserDivision);
        //}

        public UserDivision Get_UserDivision_byUserDivisionId(int ID)
        {
            return objDAL.Get_UserDivision_byUserDivisionId(ID);
        }

        public UserDivision Get_UserDivision_by_UserId(int UserId)
        {
            return objDAL.Get_UserDivision_by_UserId(UserId);
        }

        public UserDivision Get_UserDivision_by_DivisionId(int DivisionId)
        {
            return objDAL.Get_UserDivision_by_DivisionId(DivisionId);
        }

        public List<UserDivision> Get_All_UserDivision()
        {
            return objDAL.Get_All_UserDivision();
        }

    }
}
