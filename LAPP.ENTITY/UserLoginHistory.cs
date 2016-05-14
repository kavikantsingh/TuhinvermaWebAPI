using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class UserLoginHistory : BaseEntity
    {

        [Display(Description = "Required: Yes, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int UserId { get; set; }

        [Display(Description = "Required: No, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int IndividualId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 128 (string)")]
        public string UserName { get; set; }

        [Display(Description = "Required: No, Max Length: 128 (string), For example: xyz@example.com, Regular Expression Validation: ^[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,4}$")]
        public string Email { get; set; }

        [Display(Description = "Required: Yes, For example: mm/dd/yyyy  ")]
        public DateTime? PasswordChangedOn { get; set; }

        [Display(Description = "Required: No, For example: mm/dd/yyyy  ")]
        public DateTime? LoginDate { get; set; }

        [Display(Description = "Required: No, For example: mm/dd/yyyy  ")]
        public DateTime? LogoutDate { get; set; }

        [Display(Description = "Required: No, Max Length: 164 (string)")]
        public string LoginIp { get; set; }

        [Display(Description = "Required: Yes, Max Length: 50 (string)")]
        public string MachineName { get; set; }

        [Display(Description = "Required: No, Max Length: 256 (string)")]
        public string UserAgent { get; set; }

        [Display(Description = "Required: No, Max Length: 256 (string)")]
        public string UserHostAddress { get; set; }

        [Display(Description = "Required: No, Max Length: 256 (string)")]
        public string UserHostName { get; set; }

        [Display(Description = "Required: No, Max Length: 36 (char)")]
        public string UserLoginHistoryGuid { get; set; }
    }

    public class UserLoginHistoryResponse : BaseEntityServiceResponse
    {
        public List<UserLoginHistory> UserLoginHistory { get; set; }
    }
}
