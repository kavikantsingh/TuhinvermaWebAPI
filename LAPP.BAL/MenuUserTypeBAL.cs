using LAPP.DAL;
using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.BAL
{
    public class MenuUserTypeBAL
    {
        MenuUserTypeDAL objDAL = new MenuUserTypeDAL();

        public int Save_MenuUserType(MenuUserType objMenuUserType)
        {
            return objDAL.Save_MenuUserType(objMenuUserType);
        }

        //public int Update_MenuUserType(MenuUserType objMenuUserType)
        //{
        //    return objDAL.Update_MenuUserType(objMenuUserType);
        //}

        public MenuUserType Get_MenuUserType_byMenuUserTypeId(int ID)
        {
            return objDAL.Get_MenuUserType_byMenuUserTypeId(ID);
        }

        public List<MenuUserType> Get_MenuUserType_by_UserTypeId(int UserTypeId)
        {
            return objDAL.Get_MenuUserType_by_UserTypeId(UserTypeId);
        }

        public List<MenuUserType> Get_All_MenuUserType()
        {
            return objDAL.Get_All_MenuUserType();
        }

    }
}
