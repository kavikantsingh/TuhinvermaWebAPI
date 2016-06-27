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
    public class TemplateTypeBAL : BaseBAL
    {
        TemplateTypeDAL objDal = new TemplateTypeDAL();
        public int Save_TemplateType(TemplateType objTemplateType)
        {
            return objDal.Save_TemplateType(objTemplateType);
        }

        public List<TemplateType> GetAllTemplateType()
        {
            return objDal.GetAllTemplateType();
        }


    }
}
