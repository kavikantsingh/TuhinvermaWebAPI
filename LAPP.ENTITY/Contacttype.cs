using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class ContactType : BaseEntity

    {
        [Display(Description = "Required: No, Max Length:4 (Small Integer)")]
        public int ContactTypeId { get; set; }

        [Display(Description = "Required: Yes, Max Length:10 (string)")]
        public string Code { get; set; }

        [Display(Description = "Required: Yes, Max Length:50 (string)")]
        public string Desc { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1)")]
        public bool IsActive { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1)")]
        public bool IsDeleted { get; set; }

        [Display(Description = "Required:Yes, Max Length:11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int CreatedBy { get; set; }

        [Display(Description = "Required:Yes,  (DateTime), For example: MM/dd/yyyy HH:mm:ss ")]
        public DateTime CreatedOn { get; set; }

        [Display(Description = "Required:No, Max Length:11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int ModifiedBy { get; set; }

        [Display(Description = "Required:No,   (DateTime), For example: MM/dd/yyyy HH:mm:ss ")]
        public DateTime ModifiedOn { get; set; }
    }

    public class ContactTypeGet : BaseEntity
    {
        public int ContactTypeId { get; set; }
        public string Code { get; set; }
        public string Desc { get; set; }
        public bool IsActive { get; set; }
    }

    public class ContactTypeGetResponse : BaseEntityServiceResponse
    {
        public List<ContactTypeGet> ContactTypeGetList { get; set; }
    }
}
