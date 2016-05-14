using LAPP.DAL;
using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.BAL
{
    public class RoleMenuBAL
    {
        RoleMenuDAL objDAL = new RoleMenuDAL();

        public int Save_RoleMenu(RoleMenu objRoleMenu)
        {
            return objDAL.Save_RoleMenu(objRoleMenu);
        }

        //public int Update_RoleMenu(RoleMenu objRoleMenu)
        //{
        //    return objDAL.Update_RoleMenu(objRoleMenu);
        //}

        public RoleMenu Get_RoleMenu_byRoleMenuId(int ID)
        {
            return objDAL.Get_RoleMenu_byRoleMenuId(ID);
        }


        public List<RoleMenu> Get_RoleMenu_by_RoleId(int RoleId)
        {
            return objDAL.Get_RoleMenu_by_RoleId(RoleId);
        }

        public List<RoleMenu> Get_All_RoleMenu()
        {
            return objDAL.Get_All_RoleMenu();
        }

    }
}
