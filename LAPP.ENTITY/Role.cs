using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class Role : BaseEntity
    {
        [Display(Description = "Required: No, Desc: Auto Generate")]
        public int RoleId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 50 (string)")]
        public string Name { get; set; }

        [Display(Description = "Required: Yes, Max Length: Max (Text)")]
        public string Description { get; set; }

        [Display(Description = "Required: No, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int BoardAuthorityId { get; set; }

        [Display(Description = "Required: No, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int DivisionId { get; set; }

        [Display(Description = "Required: No, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int UserTypeId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 36 (char)")]
        public string RoleGuid { get; set; }

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


        [Display(Description = "Desc:Used for view only.")]
        public string BoardAuthorityName { get; set; }

        [Display(Description = "Desc:Used for view only.")]
        public string UserTypeName { get; set; }

        [Display(Description = "Desc:Used for view only.")]
        public string DivisionName { get; set; }

    }

    //public class RoleGet : BaseEntity
    //{
    //    public int RoleId { get; set; }
    //    public string Name { get; set; }
    //    public string Description { get; set; }
    //    public int BoardAuthorityId { get; set; }
    //    public int DivisionId { get; set; }
    //    public int UserTypeId { get; set; }
    //    public string RoleGuid { get; set; }
    //    public bool IsEnabled { get; set; }
    //    public bool IsActive { get; set; }

    //    public string BoardAuthorityName { get; set; }
    //    public string UserTypeName { get; set; }
    //    public string DivisionName { get; set; }

    //}

    //public class RoleResponse : BaseEntityServiceResponse
    //{
    //    public List<RoleGet> RoleList { get; set; }
    //}

    public class RoleResponse : BaseEntityServiceResponse
    {
        public object Role { get; set; }
    }
}
