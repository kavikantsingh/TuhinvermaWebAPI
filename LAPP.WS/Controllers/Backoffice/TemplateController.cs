using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LAPP.BAL;
using LAPP.ENTITY;

namespace LAPP.WS.Controllers.Backoffice
{
    /// <summary>
    /// Template Controller
    /// </summary>
    public class TemplateController : ApiController
    {
        readonly TemplateBAL _templateBal = new TemplateBAL();
        /// <summary>
        /// Get All Templates
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        [ActionName("GetAllTemplates")]
        public List<Template> GetAllTemplates()
        {
            return _templateBal.Get_All_Template();
        }

        /// <summary>
        /// Get Template By Id
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        [ActionName("GetTemplateById")]
        public Template GetTemplateById(int id)
        {
            return _templateBal.GetTemplateById(id);
        }

        /// <summary>
        /// Delete Template By Id
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("DeleteTemplateById")]
        public void DeleteTemplateById(int id)
        {
            _templateBal.DeleteTemplateById(id);
        }

        /// <summary>
        /// Update Template
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("UpdateTemplate")]
        public void UpdateTemplate(Template template)
        {
            _templateBal.UpdateTemplate(template);
        }

        /// <summary>
        /// Create Template
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("CreateTemplate")]
        public int CreateTemplate(Template template)
        {
            return _templateBal.CreateTemplate(template);
        }

        /// <summary>
        /// Get All Template Types
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        [ActionName("GetAllTemplateTypes")]
        public List<TemplateType> GetAllTemplateTypes()
        {
            TemplateTypeBAL templateTypeBal = new TemplateTypeBAL();
            return templateTypeBal.GetAllTemplateType();
        }

        /// <summary>
        /// Get All Template Applies To Type
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        [ActionName("GetAllTemplateAppliesToType")]
        public List<TemplateAppliesToType> GetAllTemplateAppliesToType()
        {
            TemplateAppliesToTypeBAL templateAppliesToTypeBal = new TemplateAppliesToTypeBAL();
            return templateAppliesToTypeBal.GetAllTemplateAppliesToType();
        }

        /// <summary>
        /// Get All Templates Get By AppTy Id
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        [ActionName("GetAllTemplatesGetByAppTyId")]
        public List<Template> GetAllTemplatesGetByAppTyId(int applicationTy)
        {
            return _templateBal.Get_All_Template().Where(x=>x.ApplicationTypeId == applicationTy).ToList();
        }

        /// <summary>
        /// Get All Templates Get By Template Name
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        [ActionName("GetAllTemplatesGetByTemplateName")]
        public List<Template> GetAllTemplatesGetByTemplateName(string tempName)
        {
            return _templateBal.Get_All_Template().Where(x=>x.TemplateName.Contains(tempName)).ToList();
        }

        /// <summary>
        /// Get All Templates Get By AppTy Id and Template Name
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        [ActionName("GetAllTemplatesGetByAppTyIdTemplateName")]
        public List<Template> GetAllTemplatesGetByAppTyIdTemplateName(int applicationTy, string tempName)
        {
            return _templateBal.Get_All_Template().Where(x => x.ApplicationTypeId == applicationTy && x.TemplateName.Contains(tempName)).ToList();
        }
    }
}
