using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class UserSecurityQuestion : BaseEntity
    {

        [Display(Description = "Required: No, Desc: Auto Generate")]
        public int UserSecurityQuestionId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int UserId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 128 (string)")]
        public string UserName { get; set; }

        [Display(Description = "Required: Yes, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int SecurityQuestionId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 128 (string)")]
        public string SecurityAnswerHash { get; set; }

        [Display(Description = "Required: No, Max Length: 16 (string)")]
        public string SecurityAnswerSalt { get; set; }





        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsActive { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsDeleted { get; set; }

        [Display(Description = "Required: Yes, Max Length:11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int CreatedBy { get; set; }

        [Display(Description = "Required: Yes, For example: mm/dd/yyyy,  ")]
        public DateTime? CreatedOn { get; set; }

        [Display(Description = "Required: No, Max Length:11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int ModifiedBy { get; set; }

        [Display(Description = "Required: No, For example: mm/dd/yyyy  ")]
        public DateTime? ModifiedOn { get; set; }

    }

    public class UserSecurityQuestionResponse : BaseEntityServiceResponse
    {
        public List<UserSecurityQuestion> UserSecurityQuestion { get; set; }
    }
}
