using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class RoleMenu : BaseEntity
    {
        [Display(Description = "Required: No, Desc: Auto Generate")]
        public int RoleMenuId { get; set; }

        [Display(Description = "Required: No, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int RoleId { get; set; }

        [Display(Description = "Required: No, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int MenuId { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool Create { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool Update { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool Delete { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool Read { get; set; }

        [Display(Description = "Desc:Used for view only.")]
        public int ParentMenuId { get; set; }

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


        [Display(Description = "Desc:Used for view only.")]
        public string RoleName { get; set; }

        [Display(Description = "Desc:Used for view only.")]
        public string MenuName { get; set; }

        [Display(Description = "Required: Yes, Max Length: Max (Text)")]
        public string MenuDescription { get; set; }

        [Display(Description = "Required: No, Max Length: 200 (string)")]
        public string MenuURL { get; set; }

        [Display(Description = "Required: No, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int MenuLevel { get; set; }

        [Display(Description = "Required: No, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int MenuSortOrder { get; set; }
    }

    public class RoleMenuRequest : BaseEntity
    {
        [Display(Description = "Required: No, Desc: Auto Generate")]
        public int RoleMenuId { get; set; }

        [Display(Description = "Required: No, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int RoleId { get; set; }

        [Display(Description = "Required: No, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int MenuId { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool Create { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool Update { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool Delete { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool Read { get; set; }



        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsActive { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsDeleted { get; set; }

        //[Display(Description = "Required: Yes, Max Length:11 (Integer), For example: Numeric vlaue (0-9) ")]
        //public int CreatedBy { get; set; }

        //[Display(Description = "Required: Yes, For example: mm/dd/yyyy,  ")]
        //public DateTime? CreatedOn { get; set; }

        //[Display(Description = "Required: No, Max Length:11 (Integer), For example: Numeric vlaue (0-9) ")]
        //public int ModifiedBy { get; set; }

        //[Display(Description = "Required: No, For example: mm/dd/yyyy  ")]
        //public DateTime? ModifiedOn { get; set; }


        //[Display(Description = "Desc:Used for view only.")]
        //public string RoleName { get; set; }

        //[Display(Description = "Desc:Used for view only.")]
        //public string MenuName { get; set; }
    }
    public class RoleMenuResponse : BaseEntityServiceResponse
    {

        public object RoleMenu { get; set; }
    }
}
