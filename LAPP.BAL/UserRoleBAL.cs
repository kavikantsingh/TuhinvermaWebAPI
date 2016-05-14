using LAPP.DAL;
using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.BAL
{
    public class UserRoleBAL
    {
        UserRoleDAL objDAL = new UserRoleDAL();

        public int Save_UserRole(UserRole objUserRole)
        {
            return objDAL.Save_UserRole(objUserRole);
        }

        //public int Update_UserRole(UserRole objUserRole)
        //{
        //    return objDAL.Update_UserRole(objUserRole);
        //}

        public UserRole Get_UserRole_byUserRoleId(int ID)
        {
            return objDAL.Get_UserRole_byUserRoleId(ID);
        }

        public List<UserRole> Get_UserRole_by_UserId(int UserId)
        {
            return objDAL.Get_UserRole_by_UserId(UserId);
        }

        public UserRole Get_UserRole_byRoleId(int RoleId)
        {
            return objDAL.Get_UserRole_byRoleId(RoleId);
        }

        public List<UserRole> Get_All_UserRole()
        {
            return objDAL.Get_All_UserRole();
        }

    }
}
