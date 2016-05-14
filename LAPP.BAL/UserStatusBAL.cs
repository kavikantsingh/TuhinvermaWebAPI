using LAPP.DAL;
using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.BAL
{
    public class UserStatusBAL
    {
        UserStatusDAL objDAL = new UserStatusDAL();

        public int Save_UserStatus(UserStatus objUserStatus)
        {
            return objDAL.Save_UserStatus(objUserStatus);
        }

        //public int Update_UserStatus(UserStatus objUserStatus)
        //{
        //    return objDAL.Update_UserStatus(objUserStatus);
        //}

        public UserStatus Get_UserStatus_byUserStatusId(int ID)
        {
            return objDAL.Get_UserStatus_byUserStatusId(ID);
        }

        public List<UserStatus> Get_All_UserStatus()
        {
            return objDAL.Get_All_UserStatus();
        }

    }
}
