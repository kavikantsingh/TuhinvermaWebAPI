using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class PriorPassword : BaseEntity

    {
        public int PriorPasswordId { get; set; }
        public int UserId { get; set; }
        public int IndividualId { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string AccessCodeHash { get; set; }
        public string AccessCodeSalt { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
