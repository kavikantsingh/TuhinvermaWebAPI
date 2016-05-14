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
    public class TemplateAttachmentBAL : BaseBAL
    {
        TemplateAttachmentDAL objDal = new TemplateAttachmentDAL();
        public int Save_TemplateAttachment(TemplateAttachment objTemplateAttachment)
        {
            return objDal.Save_TemplateAttachment(objTemplateAttachment);
        }

        public List<TemplateAttachment> Get_All_TemplateAttachment()
        {
            return objDal.Get_All_TemplateAttachment();
        }


    }
}
