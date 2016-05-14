using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{

    public class ContactResponse : BaseEntity

    {
        [Display(Description = "Required: Yes, Desc: Auto Generate")]
        public int ContactId { get; set; }

        [Display(Description = "Required: No, Max Length:128 (string)")]
        public string ContactFirstName { get; set; }

        [Display(Description = "Required: No, Max Length:128 (string)")]
        public string ContactLastName { get; set; }

        [Display(Description = "Required: No, Max Length:128 (string)")]
        public string ContactMiddleName { get; set; }

        [Display(Description = "Required: Yes, Max Length:4 (Small Integer)")]
        public int ContactTypeId { get; set; }

        [Display(Description = "Required: No, Max Length:10 (string)")]
        public string Code { get; set; }

        [Display(Description = "Required: Yes, Max Length:128 (string)")]
        public string ContactInfo { get; set; }

        [Display(Description = "Required:No,  (DateTime), For example: MM/dd/yyyy HH:mm:ss ")]
        public DateTime? DateContactValidated { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1)")]
        public bool IsActive { get; set; }

        
    }
    public class Contact : ContactResponse

    {
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
        [Display(Description = "Required: Yes, Max Length:36 (char)")]
        public string ContactGuid { get; set; }

        [Display(Description = "Required: Yes, Max Length:36 (char)")]
        public string Authenticator { get; set; }
    }
}
