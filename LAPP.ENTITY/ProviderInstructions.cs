using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class ProviderInstructions : BaseEntity
    {

        [Display(Description = "Required: No, Desc: Auto Generate")]
        public int ProviderInstructionsId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 128 (string)")]
        public int ProviderId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 128 (string)")]
        public int ApplicationId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int ContentItemLkId { get; set; }

        [Display(Description = "Required: No, Max Length: 35 (string)")]
        public string ContentItemLkCode { get; set; }

        [Display(Description = "Required: No, Max Length: 35 (string)")]
        public string ReferenceNumber { get; set; }

        [Display(Description = "Required: Yes, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int InstructionsAcceptedBy { get; set; }

        [Display(Description = "Required: No, For example: mm/dd/yyyy  ")]
        public DateTime InstructionsAcceptanceDate { get; set; }

        [Display(Description = "Required: No, For example: true or false (0,1) ")]
        public bool IsActive { get; set; }

        [Display(Description = "Required: No, For example: true or false (0,1) ")]
        public bool IsDeleted { get; set; }

        [Display(Description = "Required: No, For example: true or false (0,1) ")]
        public int CreatedBy { get; set; }

        [Display(Description = "Required: No, For example: mm/dd/yyyy  ")]
        public DateTime CreatedOn { get; set; }

        [Display(Description = "Required: No, For example: true or false (0,1) ")]
        public int ModifiedBy { get; set; }

        [Display(Description = "Required: No, For example: mm/dd/yyyy  ")]
        public DateTime? ModifiedOn { get; set; }

        [Display(Description = "Required: No, Max Length: 35 (string)")]
        public string ProviderInstructionsGuid { get; set; }
        
    }

    public class ProviderInstructionsRequest : BaseEntity
    {

        [Display(Description = "Required: No, Desc: Auto Generate")]
        public int UserId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 128 (string)")]
        public string UserName { get; set; }

        [Display(Description = "Required: Yes, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int UserTypeId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 35 (string)")]
        public string FirstName { get; set; }

        [Display(Description = "Required: Yes, Max Length: 35 (string)")]
        public string LastName { get; set; }


        [Display(Description = "Required: No, Max Length: 25 (string),For example: Format eg:(XXX) XXX-XXXX, Regular Expression Validation: ((\\(\\d{3}\\) ?)|(\\d{3}-))?\\d{3}-\\d{4}")]
        public string Phone { get; set; }

        [Display(Description = "Required: No, Max Length: 100 (string)")]
        public string PositionTitle { get; set; }

        [Display(Description = "Required: Yes, Max Length: 128 (string), For example: xyz@example.com, Regular Expression Validation: ^[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,4}$")]
        public string Email { get; set; }

        [Display(Description = "Required: Yes, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int UserStatusId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int SourceId { get; set; }


        [Display(Description = "Required: Yes, Max Length: 1 (Char)")]
        public string Gender { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsPending { get; set; }

        [Display(Description = "Required: No, For example: mm/dd/yyyy  ")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsDeleted { get; set; }

        //public List<UserRolesRequest> UserRoles { get; set; }

    }


    public class ProviderInstructionsPostResponse : BaseEntityServiceResponse
    {
        public List<ProviderInstructions> ProviderInstructions { get; set; }
    }

    public class ProviderInstructionsResponse : BaseEntityServiceResponse
    {
        public object ProviderInstructions { get; set; }
    }
}
