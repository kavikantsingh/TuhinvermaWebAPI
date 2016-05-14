using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class Menu : BaseEntity
    {
        [Display(Description = "Required: Yes, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int MenuId { get; set; }

        [Display(Description = "Required: No, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int ParentMenuId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 30 (string)")]
        public string Name { get; set; }

        [Display(Description = "Required: Yes, Max Length: Max (Text)")]
        public string Description { get; set; }

        [Display(Description = "Required: No, Max Length: 200 (string)")]
        public string MenuURL { get; set; }

        [Display(Description = "Required: No, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int MenuLevel { get; set; }

        [Display(Description = "Required: No, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int SortOrder { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsEnabled { get; set; }




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

    public class MenuResponse : BaseEntityServiceResponse
    {
        public object Menu { get; set; }
    }
}
