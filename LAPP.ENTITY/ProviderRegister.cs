using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class ProviderRegister : BaseEntity
    {
        [Display(Description = "Required: Yes, Max Length: 30 (string)")]
        public string FirstName { get; set; }

        [Display(Description = "Required: No, Max Length: 30 (string)")]
        public string MiddleName { get; set; }

        [Display(Description = "Required: Yes, Max Length:30 (string)")]
        public string LastName { get; set; }

        [Display(Description = "Required: No, For example: mm/dd/yyyy")]
        public DateTime? DateofBirth { get; set; }

        [Display(Description = "Required: Yes, Max Length: 128 (string), For example: xyz@example.com, Regular Expression Validation: ^[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,4}$")]
        public string Email { get; set; }

        [Display(Description = "Required: Yes, Max Length:250 (string)")]
        public string SchoolName { get; set; }
        
    }

    
}
