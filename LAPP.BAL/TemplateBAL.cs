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
    public class TemplateBAL : BaseBAL
    {
        TemplateDAL objDal = new TemplateDAL();
        public int Save_Template(Template objTemplate)
        {
            return objDal.Save_Template(objTemplate);
        }

        public List<Template> Get_All_Template()
        {
            return objDal.Get_All_Template();
        }


    }
}
