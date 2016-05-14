using LAPP.DAL;
using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.BAL
{
    public class MenuBAL
    {
        MenuDAL objDAL = new MenuDAL();

        public int Save_Menu(Menu objMenu)
        {
            return objDAL.Save_Menu(objMenu);
        }

        //public int Update_Menu(Menu objMenu)
        //{
        //    return objDAL.Update_Menu(objMenu);
        //}

        public Menu Get_Menu_byMenuId(int ID)
        {
            return objDAL.Get_Menu_byMenuId(ID);
        }

        public List<Menu> Get_All_Menu()
        {
            return objDAL.Get_All_Menu();
        }

    }
}
