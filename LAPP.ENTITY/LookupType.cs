using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class LookupType : BaseEntity
    {
        [Display(Description = "Required: No, Desc: Auto Generate")]
        public int LookupTypeId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int DivisionId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int DepartmentId { get; set; }

        [Display(Description = "Required: No, Max Length: 2 (char)")]
        public string StateCode { get; set; }

        [Display(Description = "Required: Yes, Max Length: 75 (string)")]
        public string Name { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsEditable { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsEncrypted { get; set; }




        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsActive { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsDeleted { get; set; }

        [Display(Description = "Required: Yes, Max Length:11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int CreatedBy { get; set; }

        [Display(Description = "Required: Yes, For example: mm/dd/yyyy,  ")]
        public DateTime? CreatedOn { get; set; }

        [Display(Description = "Required: No, Max Length:11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int? ModifiedBy { get; set; }

        [Display(Description = "Required: No, For example: mm/dd/yyyy  ")]
        public DateTime? ModifiedOn { get; set; }


    }


    public class LookupTypePost : BaseEntity
    {
        [Display(Description = "Required: No, Desc: Auto Generate")]
        public int LookupTypeId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int DivisionId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int DepartmentId { get; set; }

        [Display(Description = "Required: No, Max Length: 2 (char)")]
        public string StateCode { get; set; }

        [Display(Description = "Required: Yes, Max Length: 75 (string)")]
        public string Name { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsEditable { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsEncrypted { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsActive { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsDeleted { get; set; }
    }
    public class LookupTypePostResponse : BaseEntityServiceResponse
    {
        public List<LookupTypePost> LookupType { get; set; }
    }

    public class LookupTypeResponse : BaseEntityServiceResponse
    {
        public object LookupType { get; set; }
    }
}
