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
    }
}
