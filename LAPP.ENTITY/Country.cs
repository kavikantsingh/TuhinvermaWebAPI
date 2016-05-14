using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace LAPP.ENTITY
{
    public class Country : BaseEntity
    {
        /// <summary> Country Id, Primary Key </summary>
        [Key]
        [Display(Description = "Required: No, Desc: Auto Generate")]
        public int CountryId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 3 (string)")]
        public string Code { get; set; }

        [Display(Description = "Required: Yes, Max Length: 64 (string)")]
        public string Name { get; set; }

        [Display(Description = "Required: No, Max Length: 16 (string)")]
        public string StateLabel { get; set; }

        [Display(Description = "Required: No, Max Length: 16 (string)")]
        public string ZipLabel { get; set; }

        [Display(Description = "Required: No, Max Length: Max (Text)")]
        public string ZipRegex { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsDelete { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsActive { get; set; }

        [Display(Description = "Required:No, Max Length:11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int CreatedBy { get; set; }

        [Display(Description = "Required: Yes, For example: mm/dd/yyyy  ")]
        public DateTime? CreatedOn { get; set; }

        [Display(Description = "Required:No, Max Length:11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int ModifiedBy { get; set; }

        [Display(Description = "Required: No, For example: mm/dd/yyyy  ")]
        public DateTime? ModifiedOn { get; set; }
    }
    public class CountryRequest : BaseEntity
    {
        /// <summary> Country Id, Primary Key </summary>
        [Key]
        [Display(Description = "Required: No, Desc: Auto Generate")]
        public int CountryId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 3 (string)")]
        public string Code { get; set; }

        [Display(Description = "Required: Yes, Max Length: 64 (string)")]
        public string Name { get; set; }

        [Display(Description = "Required: No, Max Length: 16 (string)")]
        public string StateLabel { get; set; }

        [Display(Description = "Required: No, Max Length: 16 (string)")]
        public string ZipLabel { get; set; }

        [Display(Description = "Required: No, Max Length: Max (Text)")]
        public string ZipRegex { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsActive { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsDelete { get; set; }


    }
    public class CountryResponse : BaseEntityServiceResponse
    {
        public object Country { get; set; }
    }
}
