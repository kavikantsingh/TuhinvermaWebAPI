using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    class CommonResponse
    {
    }

    public class ForgetPasswordResponse : BaseEntityServiceResponse
    {

    }

    public class ResetAccessCodeResponse : BaseEntityServiceResponse
    {

    }

    public class ResetAccessCodeRequest
    {
        public string LastName { get; set; }
        public string SSN { get; set; }
        public string LicenseNumber { get; set; }

    }


    public class ResetPasswordResponse : BaseEntityServiceResponse
    {

    }

    public class SendEmailResponse : BaseEntityServiceResponse
    {

    }

    public class ResetByUserIDRequest : BaseEntity
    {
        public int UserId { get; set; }
    }


    public class ChangePasswordRequest : BaseEntity
    {
        [Display(Description = "Required: Yes")]
        public int UserId { get; set; }
        [Display(Description = "Required: Yes, Max Length: 128")]
        public string OldPassword { get; set; }
        [Display(Description = "Required: Yes, Max Length: 128")]
        public string NewPassword { get; set; }
        [Display(Description = "Required: Yes, Confirm Password should be equals to New Password, Max Length: 128")]
        public string ConfirmPassword { get; set; }
    }

    public class RenewalEmailWithAttachment
    {

        public string Email { get; set; }
        public int IndividualId { get; set; }
        public int ApplicationId { get; set; }

        public string PaymentReceiptBase64 { get; set; }
        public string PaymentReceiptName { get; set; }

        public string LicenseOutputBase64 { get; set; }
        public string LicenseOutputName { get; set; }

        public string RenewalApplicationBase64 { get; set; }
        public string RenewalApplitionName { get; set; }
        

    }

    public class  EmailWithAttachment
    {

        public string Email { get; set; }
        public int IndividualId { get; set; }
        public int ApplicationId { get; set; }

        public string Subject { get; set; }
        public int TemplateId { get; set; }
        public int TemplateTypeId { get; set; }

        public List<EmailAttachment> objAttachmentList { get; set; }

    }


    public class EmailAttachment
    {
        public string DocumentStringBase64 { get; set; }
        public string DocumentName { get; set; }

    }
}
