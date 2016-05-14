using LAPP.DAL;
using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.BAL
{
    public class RoleBAL
    {
        RoleDAL objDAL = new RoleDAL();

        public int Save_Role(Role objRole)
        {
            return objDAL.Save_Role(objRole);
        }

        //public int Update_Role(Role objRole)
        //{
        //    return objDAL.Update_Role(objRole);
        //}

        public Role Get_Role_byRoleId(int ID)
        {
            return objDAL.Get_Role_byRoleId(ID);
        }
        public List<Role> Get_Role_by_BoardAuthorityId(int BoardAuthorityId)
        {
            return objDAL.Get_Role_by_BoardAuthorityId(BoardAuthorityId);
        }
        public List<Role> Get_Role_by_UserTypeId(int UserTypeId)
        {
            return objDAL.Get_Role_by_UserTypeId(UserTypeId);
        }

        public List<Role> Get_All_Role()
        {
            return objDAL.Get_All_Role();
        }

    }
}
