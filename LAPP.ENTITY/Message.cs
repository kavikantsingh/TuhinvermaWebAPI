using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class Message : BaseEntity
    {

        [Display(Description = "Required: No, Desc: Auto Generate")]
        public int MessageId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int MessageTypeId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 10 (string)")]
        public string MessageCode { get; set; }

        [Display(Description = "Required: No, Max Length: Max (Text)")]
        public string MessageDesc { get; set; }

        [Display(Description = "Required: No, Max Length: 100 (string)")]
        public string LabelName { get; set; }

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

        [Display(Description = "Desc:Used for view only.")]
        public string MessageTypeName { get; set; }

    }

    public class MessagePost : BaseEntity
    {

        [Display(Description = "Required: No, Desc: Auto Generate")]
        public int MessageId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int MessageTypeId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 10 (string)")]
        public string MessageCode { get; set; }

        [Display(Description = "Required: No, Max Length: Max (Text)")]
        public string MessageDesc { get; set; }

        [Display(Description = "Required: No, Max Length: 100 (string)")]
        public string LabelName { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsEnabled { get; set; }


        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsActive { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsDeleted { get; set; }

    }

    public class MessageGet : BaseEntity
    {

        public int MessageId { get; set; }
        public int MessageTypeId { get; set; }
        public string MessageCode { get; set; }
        public string MessageDesc { get; set; }
        public string LabelName { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsActive { get; set; }
        public string MessageTypeName { get; set; }

    }

    public class MessagePostResponse : BaseEntityServiceResponse
    {
        public List<MessagePost> MessagesPost { get; set; }
    }

    public class MessageResponse : BaseEntityServiceResponse
    {
        public List<MessageGet> MessageGetList { get; set; }
    }
}
