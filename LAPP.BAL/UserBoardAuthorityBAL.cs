using LAPP.DAL;
using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.BAL
{
    public class UserBoardAuthorityBAL
    {
        UserBoardAuthorityDAL objDAL = new UserBoardAuthorityDAL();

        public int Save_UserBoardAuthority(UserBoardAuthority objUserBoardAuthority)
        {
            return objDAL.Save_UserBoardAuthority(objUserBoardAuthority);
        }

        //public int Update_UserBoardAuthority(UserBoardAuthority objUserBoardAuthority)
        //{
        //    return objDAL.Update_UserBoardAuthority(objUserBoardAuthority);
        //}

        public UserBoardAuthority Get_UserBoardAuthority_byUserBoardAuthorityId(int ID)
        {
            return objDAL.Get_UserBoardAuthority_byUserBoardAuthorityId(ID);
        }

        public UserBoardAuthority Get_UserBoardAuthority_by_UserId(int UserId)
        {
            return objDAL.Get_UserBoardAuthority_by_UserId(UserId);
        }

        public UserBoardAuthority Get_UserBoardAuthority_by_BoardAuthorityId(int BoardAuthorityId)
        {
            return objDAL.Get_UserBoardAuthority_by_BoardAuthorityId(BoardAuthorityId);
        }

        public List<UserBoardAuthority> Get_All_UserBoardAuthority()
        {
            return objDAL.Get_All_UserBoardAuthority();
        }

    }
}
