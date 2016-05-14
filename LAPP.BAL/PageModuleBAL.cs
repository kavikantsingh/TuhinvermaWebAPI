using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using LAPP.ENTITY;
using LAPP.DAL;
namespace LAPP.BAL
{
    public class PageModuleBAL : BaseBAL
    {
        PageModuleDAL objDal = new PageModuleDAL();
        public int Save_PageModule(PageModule objPageModule)
        {
            return objDal.Save_PageModule(objPageModule);
        }

        public List<PageModule> Get_All_PageModule()
        {
            return objDal.Get_All_PageModule();
        }


    }
}
