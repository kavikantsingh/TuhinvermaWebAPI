using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class LoginInfo : BaseEntity
    {
        [Display(Description = "Required: No, Max Length: 128 (string), For example: xyz@example.com, Regular Expression Validation: ^[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,4}$")]
        public string Email { get; set; }

        [Display(Description = "Required: No, Max Length: 30 (string)")]
        public string Password { get; set; }

        [Display(Description = "Required: No, Max Length:100 (string)")]
        public string LastName { get; set; }

        [Display(Description = "Required: No, Max Length:4 (char)")]
        public string AccessCode { get; set; }

        [Display(Description = "Required: No, Max Length:250 (string)")]
        public string LicenseNumber { get; set; }

        [Display(Description = "Required: No, Max Length:250 (string)")]
        public string AppDomain { get; set; }

        [Display(Description = "Required: No, For example: true or false (0,1) ")]
        public bool LoginWithoutEmail { get; set; }

    }

    public class LoginInfoResponse : BaseEntityServiceResponse
    {
        public string Key { get; set; }
        public int UserID { get; set; }
        public int IndividualID { get; set; }
        public object UserInfo { get; set; }

    }
}
