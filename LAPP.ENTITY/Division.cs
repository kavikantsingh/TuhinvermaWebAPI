using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class Division : BaseEntity
    {
        public int DivisionId { get; set; }
        public int BoardAuthorityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Abbreviation { get; set; }

        public string PhysicalAddressLine1 { get; set; }
        public string PhysicalAddressLine2 { get; set; }
        public string PhysicalAddressCity { get; set; }
        public string PhysicalAddressState { get; set; }
        public string PhysicalAddressZip { get; set; }

        public bool IsMailingSameasPhysical { get; set; }

        public string MailingAddressLine1 { get; set; }
        public string MailingAddressLine2 { get; set; }
        public string MailingAddressCity { get; set; }
        public string MailingAddressState { get; set; }
        public string MailingAddressZip { get; set; }

        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string ContactFax { get; set; }
        public string AlternatePhone { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

    }

    public class DivisionResponse : BaseEntityServiceResponse
    {
        public List<Division> ivision { get; set; }

    }
}
