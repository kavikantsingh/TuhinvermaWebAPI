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
    public class PageTabSectionBAL : BaseBAL
    {
        PageTabSectionDAL objDal = new PageTabSectionDAL();
        public int Save_PageTabSection(PageTabSection objPageTabSection)
        {
            return objDal.Save_PageTabSection(objPageTabSection);
        }

        public List<PageTabSection> Get_All_PageTabSection()
        {
            return objDal.Get_All_PageTabSection();
        }

        public List<PageTabSection> Get_All_PageTabSection_By_PageModuleTabSubModuleId(int PageModuleTabSubModuleId)
        {
            return objDal.Get_All_PageTabSection_By_PageModuleTabSubModuleId(PageModuleTabSubModuleId);
        }

    }
}
