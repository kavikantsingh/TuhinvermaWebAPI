using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class ConfigurationType : BaseEntity
    {
        [Display(Description = "Required: No, Desc: Auto Generate")]
        public int ConfigurationTypeId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 100 (string)")]
        public string Setting { get; set; }

        [Display(Description = "Required: No, Max Length: Max (Text)")]
        public string Description { get; set; }

        [Display(Description = "Required: Yes, Max Length: 30 (string)")]
        public string DataType { get; set; }

        [Display(Description = "Required: No, Max Length: 200 (string)")]
        public string Category { get; set; }

        [Display(Description = "Required: NO, Max Length: Max (Text)")]
        public string ValidationRegEx { get; set; }

        [Display(Description = "Required: No, Max Length: Max (Text)")]
        public string ValidationMessage { get; set; }

        [Display(Description = "Required: No, Max Length:250 (string)")]
        public string DefaultValue { get; set; }

        [Display(Description = "Required: No, Max Length: Max (Text)")]
        public bool SupportsDoesNotApply { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsEnabled { get; set; }

        [Display(Description = "Required: No, For example: true or false (0,1) ")]
        public bool IsEditable { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsActive { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsDelete { get; set; }

        [Display(Description = "Required: Yes, Max Length:11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int CreatedBy { get; set; }

        [Display(Description = "Required: Yes, For example: mm/dd/yyyy,  ")]
        public DateTime? CreatedOn { get; set; }

        [Display(Description = "Required: No, Max Length:11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int ModifiedBy { get; set; }

        [Display(Description = "Required: No, For example: mm/dd/yyyy,  ")]
        public DateTime? ModifiedOn { get; set; }

        [Display(Description = "Required: Yes, Max Length: 36 (char)")]
        public string ConfigurationTypeGuid { get; set; }

        [Display(Description = "Desc:Used for view only.")]
        public string Value { get; set; }
    }

    public class ConfigurationTypeGet : BaseEntity
    {
        public int ConfigurationTypeId { get; set; }
        public string Setting { get; set; }
        public string Description { get; set; }
        public string DataType { get; set; }
        public string Category { get; set; }
        public string ValidationRegEx { get; set; }
        public string ValidationMessage { get; set; }
        public string DefaultValue { get; set; }
        public bool SupportsDoesNotApply { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsEditable { get; set; }
        public bool IsActive { get; set; }

        public string Value { get; set; }
    }

    public class ConfigurationTypeRequestResponse : BaseEntityServiceResponse
    {
        public List<ConfigurationType> ConfigurationType { get; set; }
    }

    public class ConfigurationTypeResponse : BaseEntityServiceResponse
    {
        public List<ConfigurationTypeGet> ConfigurationList { get; set; }
    }
}
