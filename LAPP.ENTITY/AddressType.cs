using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class AddressType : BaseEntity

    {
        [Display(Description = "Required:Yes, Max Length:4 (Small Integer)")]
        public int AddressTypeId { get; set; }

        [Display(Description = "Required:Yes, Max Length:10 (string)")]
        public string AddressTypeCode { get; set; }

        [Display(Description = "Required:Yes, Max Length:50 (string)")]
        public string AddressTypeDesc { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1)")]
        public bool IsActive { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1)")]
        public bool IsDeleted { get; set; }

        [Display(Description = "Required:Yes, Max Length:11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int CreatedBy { get; set; }

        [Display(Description = "Required:Yes,  (DateTime), For example: MM/dd/yyyy HH:mm:ss ")]
        public DateTime CreatedOn { get; set; }

        [Display(Description = "Required:No, Max Length:11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int ModifiedBy { get; set; }

        [Display(Description = "Required:No,   (DateTime), For example: MM/dd/yyyy HH:mm:ss ")]
        public DateTime ModifiedOn { get; set; }

    }

    public class AddressTypeGet : BaseEntity
    {
        public int AddressTypeId { get; set; }
        public string AddressTypeCode { get; set; }
        public string AddressTypeDesc { get; set; }
        public bool IsActive { get; set; }
    }



    public class AddressTypeGetResponse : BaseEntityServiceResponse
    {
        public List<AddressTypeGet> AddressTypeGetList { get; set; }
    }

}
