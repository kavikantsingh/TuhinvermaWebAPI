using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class Users : BaseEntity
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
        public int UserStatusId { get; set; }

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
        public string UserStatusName { get; set; }

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

    public class UsersRequest : BaseEntity
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

        public List<UserRolesRequest> UserRoles { get; set; }

        public string IndividualGuid { get; set; }

    }

    public class UserRolesRequest
    {
        public int UserID { get; set; }
        public int UserRoleID { get; set; }
        public int RoleId { get; set; }
        public bool Selected { get; set; }
        public bool Grantable { get; set; }
    }


    public class UsersSearch : BaseEntity
    {
        public string UserName { get; set; }

        public int? UserTypeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string PositionTitle { get; set; }

        public string Email { get; set; }

        public int? UserStatusId { get; set; }

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

        public string UserStatusName { get; set; }
        public string RoleName { get; set; }
    }



    public class UsersSearchResponse : BaseEntityServiceResponse
    {
        public int Total_Recard { get; set; }
        //public object Users { get; set; }
        public List<Users> Users { get; set; }
     }


    public class UsersPostResponse : BaseEntityServiceResponse
    {
        public List<UsersRequest> Users { get; set; }
    }

    public class UsersResponse : BaseEntityServiceResponse
    {
        //public object Users { get; set; }
        public   Users Users { get; set; }

    }
}
