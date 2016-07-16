using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class Lookup : BaseEntity
    {
        public int ProviderBusinessTypeId { get; set; }

        [Display(Description = "Required: No, Desc: Auto Generate")]
        public int LookupId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int LookupTypeId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 50 (string)")]
        public string LookupCode { get; set; }

        [Display(Description = "Required: No, Max Length: 500 (string)")]
        public string LookupDesc { get; set; }

        [Display(Description = "Required: Yes, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
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
        public int? ModifiedBy { get; set; }

        [Display(Description = "Required: No, For example: mm/dd/yyyy  ")]
        public DateTime? ModifiedOn { get; set; }




    }

    public class LookupPost : BaseEntity
    {
        [Display(Description = "Required: No, Desc: Auto Generate")]
        public int LookupId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int LookupTypeId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 50 (string)")]
        public string LookupCode { get; set; }

        [Display(Description = "Required: No, Max Length: 500 (string)")]
        public string LookupDesc { get; set; }

        [Display(Description = "Required: Yes, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int SortOrder { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsEnabled { get; set; }


        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsActive { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsDeleted { get; set; }
    }
    public class LookupPostResponse : BaseEntityServiceResponse
    {
        public List<LookupPost> Lookup { get; set; }
    }

    public class LookupResponse : BaseEntityServiceResponse
    {
        public object Lookup { get; set; }
    }
}
