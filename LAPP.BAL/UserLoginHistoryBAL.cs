using LAPP.DAL;
using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.BAL
{
    public class UserLoginHistoryBAL
    {
        UserLoginHistoryDAL objDAL = new UserLoginHistoryDAL();

        public int Save_UserLoginHistory(UserLoginHistory objUserLoginHistory)
        {
            return objDAL.Save_UserLoginHistory(objUserLoginHistory);
        }

        //public int Update_UserLoginHistory(UserLoginHistory objUserLoginHistory)
        //{
        //    return objDAL.Update_UserLoginHistory(objUserLoginHistory);
        //}

        public UserLoginHistory Get_UserLoginHistory_byUserLoginHistoryId(int ID)
        {
            return objDAL.Get_UserLoginHistory_byUserId(ID);
        }

        public List<UserLoginHistory> Get_All_UserLoginHistory()
        {
            return objDAL.Get_All_UserLoginHistory();
        }

    }
}
