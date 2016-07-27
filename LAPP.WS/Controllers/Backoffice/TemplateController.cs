using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LAPP.BAL;
using LAPP.ENTITY;
using LAPP.WS.App_Helper.Common;

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
        public void DeleteTemplateById(string Key,int id)
        {
            _templateBal.DeleteTemplateById(id);
        }

        /// <summary>
        /// Update Template
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("UpdateTemplate")]
        public void UpdateTemplate(string Key, Template template)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

            var temp= _templateBal.GetTemplateById(template.TemplateId);
            temp.TemplateName = template.TemplateName;
            temp.ApplicationTypeId = template.ApplicationTypeId;
            temp.TemplateSubject = template.TemplateSubject;
            temp.TemplateMessage = template.TemplateMessage;
            temp.ModifiedOn = System.DateTime.Now;
            temp.ModifiedBy = CreatedOrMoifiy;
            _templateBal.UpdateTemplate(temp);
        }

        /// <summary>
        /// Create Template
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("CreateTemplate")]
        public int CreateTemplate(string Key, Template template)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;
            template.TemplateCode = "";
            template.MasterTransactionId = 1;
            template.PageModuleId = 1;
            template.PageTabSectionId = 1;
            template.PageModuleTabSubModuleId = 1;
            template.TemplateAppliesToTypeId = 1;
            template.TemplateTypeId = 1;

            template.IsActive = true;
            template.CreatedOn = System.DateTime.Now;
            template.CreatedBy = CreatedOrMoifiy;
            template.ModifiedOn = System.DateTime.Now;
            template.ModifiedBy = CreatedOrMoifiy;
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
        [AcceptVerbs("POST")]
        [ActionName("GetAllTemplatesGetByAppTyId")]
        public List<Template> GetAllTemplatesGetByAppTyId(string Key, TemplateSearch tempSearch)
        {
            return _templateBal.Get_All_Template().Where(x=>x.ApplicationTypeId == tempSearch.applicationTy).ToList();
        }

        /// <summary>
        /// Get All Templates Get By Template Name
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("GetAllTemplatesGetByTemplateName")]
        public List<Template> GetAllTemplatesGetByTemplateName(string Key, TemplateSearch tempSearch)
        {
            return _templateBal.Get_All_Template().Where(x=>x.TemplateName.Contains(tempSearch.tempName)).ToList();
        }

        /// <summary>
        /// Get All Templates Get By AppTy Id and Template Name
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("GetAllTemplatesGetByAppTyIdTemplateName")]
        public List<Template> GetAllTemplatesGetByAppTyIdTemplateName(string Key, TemplateSearch tempSearch)
        {
            return _templateBal.Get_All_Template().Where(x => x.ApplicationTypeId == tempSearch.applicationTy && x.TemplateName.Contains(tempSearch.tempName)).ToList();
        }
    }
}
