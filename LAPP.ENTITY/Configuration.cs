using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class Configuration : BaseEntity
    {
        [Display(Description = "Required: No, Desc: Auto Generate")]
        public int ConfigurationId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 11 (Integer)")]
        public int ConfigurationTypeId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 11 (Integer)")]
        public int DepartmentId { get; set; }

        [Display(Description = "Required: No, Max Length: Max (Text)")]
        public string Value { get; set; }

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
        public string ConfigurationGuid { get; set; }



        [Display(Description = "Desc:Used for view only.")]
        public string ConfigurationType { get; set; }

        [Display(Description = "Desc:Used for view only.")]
        public string DepartmentName { get; set; }

    }

    public class ConfigurationSearch : BaseEntity
    {
        public int ConfigurationTypeId { get; set; }
        public string Name { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }

        public string ConfigurationType { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string Setting { get; set; }
    }

    public class ConfigurationGet : BaseEntity
    {
        public int ConfigurationTypeId { get; set; }
        public int ConfigurationId { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
        public string ConfigurationType { get; set; }
    }


    public class ConfigurationSearchResponse : BaseEntityServiceResponse
    {
        public object Configuration { get; set; }
    }

    public class ConfigurationRequestResponse : BaseEntityServiceResponse
    {
        public List<Configuration> Configuration { get; set; }
    }

    public class ConfigurationResponse : BaseEntityServiceResponse
    {
        public List<ConfigurationGet> ConfigurationList { get; set; }
    }
}
