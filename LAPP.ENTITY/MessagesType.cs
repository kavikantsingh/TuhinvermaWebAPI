﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class MessagesType : BaseEntity
    {
        [Display(Description = "Required: Yes, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int MessageTypeId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 25 (string)")]
        public string MessageTypeCode { get; set; }


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

    public class MessagesTypePost : BaseEntity
    {
        [Display(Description = "Required: Yes, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int MessageTypeId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 25 (string)")]
        public string MessageTypeCode { get; set; }


        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsActive { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsDeleted { get; set; }

    }

    public class MessagesTypeGET : BaseEntity
    {
        public int MessageTypeId { get; set; }
        public string MessageTypeCode { get; set; }
        public bool IsActive { get; set; }

    }

    public class MessagesTypePostResponse : BaseEntityServiceResponse
    {
        public List<MessagesTypePost> MessagesTypePost { get; set; }
    }

    public class MessagesTypeResponse : BaseEntityServiceResponse
    {
        public List<MessagesTypeGET> MessagesTypeList { get; set; }
    }
}
