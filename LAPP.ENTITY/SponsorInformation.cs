using System;
using System.ComponentModel.DataAnnotations;

namespace LAPP.ENTITY
{
    public class SponsorInformationResponse :BaseEntity
    {
        [Display(Description = "Required: Yes, Desc: Auto Generate")]
        public int SponsorId { get; set; }

        [Display(Description = "Required: Yes, Max Length:100 (string)")]
        public string SponsorName { get; set; }

        [Display(Description = "Required: No, Max Length:128 (string)")]
        public string StreetLine1 { get; set; }

        [Display(Description = "Required: No, Max Length:128 (string)")]
        public string StreetLine2 { get; set; }

        [Display(Description = "Required: No, Max Length:128 (string)")]
        public string City { get; set; }

        [Display(Description = "Required: No, Max Length:2 (char)")]
        public string StateCode { get; set; }

        [Display(Description = "Required: No, Max Length:128 (string)")]
        public string Zip { get; set; }

    }
}