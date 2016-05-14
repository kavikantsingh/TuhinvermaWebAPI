using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class TemplateAttachment : BaseEntity

    {
        public int TemplateAttachmentId { get; set; }
        public string TemplateAttachmentName { get; set; }
        public string TemplateAttachmentLink { get; set; }
        public int TemplateId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
