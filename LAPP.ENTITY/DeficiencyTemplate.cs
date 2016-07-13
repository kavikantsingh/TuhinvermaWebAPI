using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class DeficiencyTemplateSearch : LAPP_DeficiencyTemplate
    {
        public bool IsSearch { get; set; }
        public string MasterTransactionId { get; set; }
        public string DeficiencyTemplateName { get; set; }
        public bool IsActive { get; set; }
    }

    public class LAPP_DeficiencyTemplate: BaseEntity
    {
        public int Created_By { get; set; }
        public DateTime Created_On { get; set; }
        public int Deficiency_Template_ID { get; set; }
        public string Deficiency_Template_Message { get; set; }
        public string Deficiency_Template_Name { get; set; }
        public string Deficiency_Template_Subject { get; set; }
        public DateTime End_Date { get; set; }
        public bool Is_Active { get; set; }
        public bool Is_Deleted { get; set; }
        public int Modified_By { get; set; }
        public DateTime Modified_On { get; set; }
        public int Master_Transaction_Id { get; set; }
        public string Name { get; set; }
        public bool Is_Editable { get; set; }
    }

    public class DeficiencyTemplateResponseGet : BaseEntityServiceResponse
    {
        public List<LAPP_DeficiencyTemplate> DeficiencyTemplateResponseList { get; set; }
    }
    
}
