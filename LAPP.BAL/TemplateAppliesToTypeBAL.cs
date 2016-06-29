using System.Collections.Generic;
using LAPP.DAL;
using LAPP.ENTITY;

namespace LAPP.BAL
{
    public class TemplateAppliesToTypeBAL
    {
        TemplateAppliesToTypeDAL objDal = new TemplateAppliesToTypeDAL();

        public List<TemplateAppliesToType> GetAllTemplateAppliesToType()
        {
            return objDal.GetAllTemplateAppliesToType();
        }
    }
}