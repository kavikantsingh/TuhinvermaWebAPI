using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class ProviderLogin : BaseEntity
    {
        [Display(Description = "Required: No, Max Length: 128 (string), For example: xyz@example.com, Regular Expression Validation: ^[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,4}$")]
        public string Email { get; set; }

        [Display(Description = "Required: No, Max Length: 30 (string)")]
        public string Password { get; set; }

    }

    public class ProviderLoginResponse : BaseEntityServiceResponse
    {
        public bool IsPasswordChange { get; set; }

    }
}
