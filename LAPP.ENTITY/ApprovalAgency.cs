using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class ApprovalAgency : BaseEntity
    {

        [Display(Description = "Required: No, Desc: Auto Generate")]
        public int UserId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 128 (string)")]
        public string UserName { get; set; }

        [Display(Description = "Required: Yes, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int UserTypeId { get; set; }

        [Display(Description = "Required: No, Max Length: 35 (string)")]
        public string FirstName { get; set; }

        [Display(Description = "Required: No, Max Length: 35 (string)")]
        public string LastName { get; set; }

        [Display(Description = "Required: No, Max Length: 50 (string)")]
        public string EntityName { get; set; }

        [Display(Description = "Required: No, Max Length: 25 (string),For example: Format eg:(XXX) XXX-XXXX, Regular Expression Validation: ((\\(\\d{3}\\) ?)|(\\d{3}-))?\\d{3}-\\d{4}")]
        public string Phone { get; set; }

        [Display(Description = "Required: No, Max Length: 25 (string),For example: Format eg:(XXX) XXX-XXXX, Regular Expression Validation: ((\\(\\d{3}\\) ?)|(\\d{3}-))?\\d{3}-\\d{4}")]
        public string CellPhone { get; set; }

        [Display(Description = "Required: No, Max Length: 100 (string)")]
        public string PositionTitle { get; set; }

        [Display(Description = "Required: No, Max Length: 128 (string), For example: xyz@example.com, Regular Expression Validation: ^[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,4}$")]
        public string Email { get; set; }

        [Display(Description = "Required: Yes, Max Length: 128 (string)")]
        public string PasswordHash { get; set; }

        [Display(Description = "Required: No, Max Length: 16 (string)")]
        public string PasswordSalt { get; set; }

        [Display(Description = "Required: No, For example: mm/dd/yyyy  ")]
        public DateTime? PasswordExpirationDate { get; set; }

        [Display(Description = "Required: Yes, For example: mm/dd/yyyy  ")]
        public DateTime? PasswordChangedOn { get; set; }

        [Display(Description = "Required: No, For example: mm/dd/yyyy  ")]
        public DateTime? LastLoginDate { get; set; }

        [Display(Description = "Required: Yes, Max Length: 164 (string)")]
        public string LastLoginIp { get; set; }

        [Display(Description = "Required: Yes, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int FailedLogins { get; set; }

        [Display(Description = "Required: Yes, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int ApprovalAgencytatusId { get; set; }

        [Display(Description = "Required: No, For example: mm/dd/yyyy  ")]
        public DateTime? EulaAcceptedOn { get; set; }

        [Display(Description = "Required: Yes, Max Length: 128 (string)")]
        public string UserExternalId { get; set; }

        [Display(Description = "Required: No, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int IndividualId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int SourceId { get; set; }

        [Display(Description = "Required: No, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int SignatureFileId { get; set; }

        [Display(Description = "Required: No, For example: true or false (0,1) ")]
        public bool? IsPending { get; set; }

        [Display(Description = "Required: Yes, Max Length: 36 (char)")]
        public string UserGuid { get; set; }


        public bool TemporaryPassword { get; set; }


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


        [Display(Description = "Required: Yes, Max Length: 1 (Char)")]
        public string Gender { get; set; }


        [Display(Description = "Required: No, For example: mm/dd/yyyy  ")]
        public DateTime? DateOfBirth { get; set; }



        [Display(Description = "Desc:Used for view only.")]
        public string SourceName { get; set; }

        [Display(Description = "Desc:Used for view only.")]
        public string IndividualName { get; set; }

        [Display(Description = "Desc:Used for view only.")]
        public string UserTypeName { get; set; }

        [Display(Description = "Desc:Used for view only.")]
        public string ApprovalAgencytatusName { get; set; }

        [Display(Description = "Desc:Used for view only.")]
        public string DOB
        {
            get
            {
                if (DateOfBirth != null)
                {
                    return Convert.ToDateTime(DateOfBirth).ToShortDateString();
                }
                else
                {
                    return "NULL";
                }
            }
        }

        public string IndividualGuid { get; set; }
        public string Authenticator { get; set; }
        public int Total_Recard { get; set; }

    }

    public class ApprovalAgencyRequest : BaseEntity
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
        public int ApprovalAgencytatusId { get; set; }

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

        public List<UserRolesRequest> UserRoles { get; set; }

    }

    public class ApprovalAgencySearch : BaseEntity
    {
        public string UserName { get; set; }

        public int? UserTypeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string PositionTitle { get; set; }

        public string Email { get; set; }

        public int? ApprovalAgencytatusId { get; set; }

        public int? SourceId { get; set; }
        public int? RoleId { get; set; }

        public string Gender { get; set; }

        public bool? IsPending { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Display(Description = "Desc:Used for view only.")]
        public string DOB
        {
            get
            {
                if (DateOfBirth != null)
                {
                    return Convert.ToDateTime(DateOfBirth).ToShortDateString();
                }
                else
                {
                    return "NULL";
                }
            }
        }

        public string SourceName { get; set; }

        public int Total_Recard { get; set; }
        public string UserTypeName { get; set; }

        public string ApprovalAgencytatusName { get; set; }
        public string RoleName { get; set; }
    }



    public class ApprovalAgencySearchResponse : BaseEntityServiceResponse
    {
        public int Total_Recard { get; set; }
        public object ApprovalAgency { get; set; }
    }


    public class ApprovalAgencyPostResponse : BaseEntityServiceResponse
    {
        public List<ApprovalAgencyRequest> ApprovalAgency { get; set; }
    }

    public class ApprovalAgencyResponse : BaseEntityServiceResponse
    {
        public object ApprovalAgency { get; set; }
    }
}
