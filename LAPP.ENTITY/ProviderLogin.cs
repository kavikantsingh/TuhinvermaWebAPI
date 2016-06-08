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
        public bool IsPasswordTemporary { get; set; }
        public int ProviderId { get; set; }
        public int UserId { get; set; }
        public int IndividualNameId { get; set; }
        public int IndividualId { get; set; }
        public int ApplicationId { get; set; }
        public string ApplicationStatus { get; set; }
        public string Key { get; set; }
    }
}
