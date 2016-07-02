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

        public Template GetTemplateById(int id)
        {
            return objDal.GetTemplateById(id);
        }

        public void DeleteTemplateById(int id)
        {
            objDal.DeleteTemplateById(id);
        }

        public void UpdateTemplate(Template template)
        {
            objDal.UpdateTemplate(template);
        }

        public int CreateTemplate(Template template)
        {
            return objDal.CreateTemplate(template);
        }

    }
}
