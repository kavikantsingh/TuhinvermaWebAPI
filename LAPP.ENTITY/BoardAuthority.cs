using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LAPP.ENTITY
{
    /// <summary>
    ///  Board Authority
    /// </summary>
    public class BoardAuthority : BaseEntity
    {
        [Display(Description = "Required: No, Desc: Auto Generate")]
        public int BoardAuthorityId { get; set; }

        [Display(Description = "Required:Yes, Max Length:2 (char), For example: US state code NV for Nevada.  ")]
        public string StateCode { get; set; }

        [Display(Description = "Required:Yes, Max Length:250 (string)")]
        public string Name { get; set; }

        [Display(Description = "Required:Yes, Max Length:10 (string)")]
        public string Code { get; set; }

        [Display(Description = "Required:No, Max Length:10 (string)")]
        public string Acronym { get; set; }

        [Display(Description = "Required:No, Max Length:512 (string), For example: http(s)://www.example.com, Regular Expression Validation: (http|https)://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?")]
        public string Url { get; set; }

        [Display(Description = "Required:Yes, Max Length:250 (string)")]
        public string PhysicalAddressLine1 { get; set; }

        [Display(Description = "Required:Yes, Max Length:250 (string)")]
        public string PhysicalAddressLine2 { get; set; }

        [Display(Description = "Required:Yes, Max Length:50 (string)")]
        public string PhysicalAddressCity { get; set; }

        [Display(Description = "Required:Yes, Max Length:2 (char), For example: US state code NV for Nevada. ")]
        public string PhysicalAddressState { get; set; }

        [Display(Description = "Required:Yes, Max Length:15 (string), For example: XXXXX-XXXX or XXXXX, Regular Expression Validation: (^\\d{5}(-\\d{4})?$)|(^[ABCEGHJKLMNPRSTVXY]{1}\\d{1}[A-Z]{1} *\\d{1}[A-Z]{1}\\d{1}$) ")]
        public string PhysicalAddressZip { get; set; }

        [Display(Description = "Required:Yes, For example, true or false (0,1) ")]
        public bool IsMailingSameasPhysical { get; set; }

        [Display(Description = "Required:No, Max Length:250 (string)")]
        public string MailingAddressLine1 { get; set; }

        [Display(Description = "Required:No, Max Length:250 (string)")]
        public string MailingAddressLine2 { get; set; }

        [Display(Description = "Required:No, Max Length:50 (string)")]
        public string MailingAddressCity { get; set; }

        [Display(Description = "Required:No, Max Length:2 (char), For example: US state code NV for Nevada. ")]
        public string MailingAddressState { get; set; }

        [Display(Description = "Required:No, Max Length:15 (string), For example: XXXXX-XXXX or XXXXX, Regular Expression Validation: (^\\d{5}(-\\d{4})?$)|(^[ABCEGHJKLMNPRSTVXY]{1}\\d{1}[A-Z]{1} *\\d{1}[A-Z]{1}\\d{1}$) ")]
        public string MailingAddressZip { get; set; }

        [Display(Description = "Required:No, Max Length:25 (string),For example: Format eg:(XXX) XXX-XXXX, Regular Expression Validation: ((\\(\\d{3}\\) ?)|(\\d{3}-))?\\d{3}-\\d{4}")]
        public string ContactPhone { get; set; }

        [Display(Description = "Required:No, Max Length:128 (string), For example: xyz@example.com, Regular Expression Validation: ^[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,4}$")]
        public string ContactEmail { get; set; }

        [Display(Description = "Required:No, Max Length:25 (string),For example: Format eg:(XXX) XXX-XXXX, Regular Expression Validation: ((\\(\\d{3}\\) ?)|(\\d{3}-))?\\d{3}-\\d{4}")]
        public string ContactFax { get; set; }

        [Display(Description = "Required:No, Max Length:25 (string),For example: Format eg:(XXX) XXX-XXXX, Regular Expression Validation: ((\\(\\d{3}\\) ?)|(\\d{3}-))?\\d{3}-\\d{4}")]
        public string AlternatePhone { get; set; }

        [Display(Description = "Required:No, Max Length:100 (string)")]
        public string SystemName { get; set; }

        [Display(Description = "Required:No, Max Length:10 (string)")]
        public string SystemAbbreviation { get; set; }

        [Display(Description = "Required:No, Max Length:512 (string), For example: http(s)://www.example.com, Regular Expression Validation: (http|https)://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?")]
        public string SystemUrl { get; set; }

        [Display(Description = "Required:No, Max Length:512 (string), For example: http(s)://www.example.com, Regular Expression Validation: (http|https)://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?")]
        public string ApplicationSystemUrl { get; set; }

        [Display(Description = "Required:No, Max Length:100 (string)")]
        public string SystemContact { get; set; }

        [Display(Description = "Required:Yes, For example: true or false (0,1) ")]
        public bool IsActive { get; set; }

        [Display(Description = "Required:Yes, Max Length:11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int CreatedBy { get; set; }

        [Display(Description = "Required:Yes, For example: mm/dd/yyyy,  ")]
        public DateTime? CreatedOn { get; set; }

        [Display(Description = "Required:No, Max Length:11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int ModifiedBy { get; set; }

        [Display(Description = "Required:No, For example: mm/dd/yyyy  ")]
        public DateTime? ModifiedOn { get; set; }

        [Display(Description = "Required:Yes, Max Length:36 (char)")]
        public Guid BoardAuthorityGuid { get; set; }
    }

    public class BoardAuthorityRequest : BaseEntity
    {
        [Display(Description = "Required: No, Desc: Auto Generate")]
        public int BoardAuthorityId { get; set; }

        [Display(Description = "Required:Yes, Max Length:2 (char), For example: US state code NV for Nevada.  ")]
        public string StateCode { get; set; }

        [Display(Description = "Required:Yes, Max Length:250 (string)")]
        public string Name { get; set; }

        [Display(Description = "Required:Yes, Max Length:10 (string)")]
        public string Code { get; set; }

        [Display(Description = "Required:No, Max Length:10 (string)")]
        public string Acronym { get; set; }

        [Display(Description = "Required:No, Max Length:512 (string), For example: http(s)://www.example.com, Regular Expression Validation: (http|https)://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?")]
        public string Url { get; set; }

        [Display(Description = "Required:Yes, Max Length:250 (string)")]
        public string PhysicalAddressLine1 { get; set; }

        [Display(Description = "Required:Yes, Max Length:250 (string)")]
        public string PhysicalAddressLine2 { get; set; }

        [Display(Description = "Required:Yes, Max Length:50 (string)")]
        public string PhysicalAddressCity { get; set; }

        [Display(Description = "Required:Yes, Max Length:2 (char), For example: US state code NV for Nevada. ")]
        public string PhysicalAddressState { get; set; }

        [Display(Description = "Required:Yes, Max Length:15 (string), For example: XXXXX-XXXX or XXXXX, Regular Expression Validation: (^\\d{5}(-\\d{4})?$)|(^[ABCEGHJKLMNPRSTVXY]{1}\\d{1}[A-Z]{1} *\\d{1}[A-Z]{1}\\d{1}$) ")]
        public string PhysicalAddressZip { get; set; }

        [Display(Description = "Required:Yes, For example, true or false (0,1) ")]
        public bool IsMailingSameasPhysical { get; set; }

        [Display(Description = "Required:No, Max Length:250 (string)")]
        public string MailingAddressLine1 { get; set; }

        [Display(Description = "Required:No, Max Length:250 (string)")]
        public string MailingAddressLine2 { get; set; }

        [Display(Description = "Required:No, Max Length:50 (string)")]
        public string MailingAddressCity { get; set; }

        [Display(Description = "Required:No, Max Length:2 (char), For example: US state code NV for Nevada. ")]
        public string MailingAddressState { get; set; }

        [Display(Description = "Required:No, Max Length:15 (string), For example: XXXXX-XXXX or XXXXX, Regular Expression Validation: (^\\d{5}(-\\d{4})?$)|(^[ABCEGHJKLMNPRSTVXY]{1}\\d{1}[A-Z]{1} *\\d{1}[A-Z]{1}\\d{1}$) ")]
        public string MailingAddressZip { get; set; }

        [Display(Description = "Required:No, Max Length:25 (string),For example: Format eg:(XXX) XXX-XXXX, Regular Expression Validation: ((\\(\\d{3}\\) ?)|(\\d{3}-))?\\d{3}-\\d{4}")]
        public string ContactPhone { get; set; }

        [Display(Description = "Required:No, Max Length:128 (string), For example: xyz@example.com, Regular Expression Validation: ^[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,4}$")]
        public string ContactEmail { get; set; }

        [Display(Description = "Required:No, Max Length:25 (string),For example: Format eg:(XXX) XXX-XXXX, Regular Expression Validation: ((\\(\\d{3}\\) ?)|(\\d{3}-))?\\d{3}-\\d{4}")]
        public string ContactFax { get; set; }

        [Display(Description = "Required:No, Max Length:25 (string),For example: Format eg:(XXX) XXX-XXXX, Regular Expression Validation: ((\\(\\d{3}\\) ?)|(\\d{3}-))?\\d{3}-\\d{4}")]
        public string AlternatePhone { get; set; }

        [Display(Description = "Required:No, Max Length:100 (string)")]
        public string SystemName { get; set; }

        [Display(Description = "Required:No, Max Length:10 (string)")]
        public string SystemAbbreviation { get; set; }

        [Display(Description = "Required:No, Max Length:512 (string), For example: http(s)://www.example.com, Regular Expression Validation: (http|https)://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?")]
        public string SystemUrl { get; set; }

        [Display(Description = "Required:No, Max Length:512 (string), For example: http(s)://www.example.com, Regular Expression Validation: (http|https)://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?")]
        public string ApplicationSystemUrl { get; set; }

        [Display(Description = "Required:No, Max Length:100 (string)")]
        public string SystemContact { get; set; }

        [Display(Description = "Required:Yes, For example: true or false (0,1) ")]
        public bool IsActive { get; set; }
 
    }
    public class BoardAuthorityResponse : BaseEntityServiceResponse
    {
        public object BoardAuthority { get; set; }
    }
}
