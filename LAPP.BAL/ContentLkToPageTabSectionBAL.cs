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
    public class ContentLkToPageTabSectionBAL : BaseBAL
    {
        ContentLkToPageTabSectionDAL objDal = new ContentLkToPageTabSectionDAL();

        public int Save_ContentLkToPageTabSection(ContentLkToPageTabSection objaddress)
        {
            return objDal.Save_ContentLkToPageTabSection(objaddress);
        }

        public List<ContentLkToPageTabSection> Get_All_ContentLkToPageTabSection()
        {
            return objDal.Get_All_ContentLkToPageTabSection();
        }

        public ContentLkToPageTabSection Get_address_By_ContentLkToPageTabSectionId(int ID)
        {
            return objDal.Get_ContentLkToPageTabSection_By_ContentLkToPageTabSectionId(ID);
        }

        public List<ContentLkToPageTabSection> Get_ContentLkToPageTabSection_By_PageModId_PageTabSecId_PageModTabSubModId(int PageModId, int PageTabSecId, int PageModTabSubModId)
        {
            return objDal.Get_ContentLkToPageTabSection_By_PageModId_PageTabSecId_PageModTabSubModId(PageModId, PageTabSecId, PageModTabSubModId);
        }

    }
}
