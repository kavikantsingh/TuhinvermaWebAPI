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
    public class ContentItemLkBAL : BaseBAL
    {
        ContentItemLkDAL objDal = new ContentItemLkDAL();

        public int Save_ContentItemLk(ContentItemLk objaddress)
        {
            return objDal.Save_ContentItemLk(objaddress);
        }

        public List<ContentItemLk> Get_All_ContentItemLk()
        {
            return objDal.Get_All_ContentItemLk();
        }

        public ContentItemLk Get_address_By_ContentItemLkId(int ID)
        {
            return objDal.Get_ContentItemLk_By_ContentItemLkId(ID);
        }

        public ContentItemLk Get_ContentItemLk_By_ContentItemLkId(int ID)
        {
            return objDal.Get_ContentItemLk_By_ContentItemLkId(ID);
        }

        public ContentItemLk Get_ContentItemLk_By_ContentItemLkCode_And_ContentItemLkHash(string Code, int Hash)
        {
            return objDal.Get_ContentItemLk_By_ContentItemLkCode_And_ContentItemLkHash(Code, Hash);
        }

        public List<ContentItemLk> Get_ContentItemLk_By_ContentItemLkCode(string Code)
        {
            return objDal.Get_ContentItemLk_By_ContentItemLkCode(Code);
        }

        public List<ContentItemLk> Get_ContentItemLk_By_ContentLkToPageTabSectionId(int PageTabSectionId)
        {
            return objDal.Get_ContentItemLk_By_ContentLkToPageTabSectionId(PageTabSectionId);
        }

        public List<ContentItemLk> Get_ContentItemLk_By_PageModuleId(int PageModuleId)
        {
            return objDal.Get_ContentItemLk_By_PageModuleId(PageModuleId);
        }

        public int Update_ContentItemLk(ContentItemLkPost objContentItemLkPost)
        {
            return objDal.Update_ContentItemLk(objContentItemLkPost);
        }
        public ContentItemLk Get_ContentItemLk_GET_BY_ContentItemLkId_AND_Code(int ContentItemLkId, String ContentItemLkCode)
        {
            return objDal.Get_ContentItemLk_GET_BY_ContentItemLkId_AND_Code(ContentItemLkId, ContentItemLkCode);
        }
    }
}
