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
    public class PageModuleTabSubModuleBAL : BaseBAL
    {
        PageModuletabsubmoduleDAL objDal = new PageModuletabsubmoduleDAL();
        public int Save_PageModuletabsubmodule(PageModuleTabSubModule objPageModuletabsubmodule)
        {
            return objDal.Save_PageModuletabsubmodule(objPageModuletabsubmodule);
        }

        public List<PageModuleTabSubModule> Get_All_PageModuletabsubmodule()
        {
            return objDal.Get_All_PageModuletabsubmodule();
        }

        public List<PageModuleTabSubModule> Get_All_PageModuletabsubmoduleByPageModuleId(int PageModuleId)
        {
            return objDal.Get_All_PageModuletabsubmodule_By_PageModuleId(PageModuleId);
        }


    }
}
