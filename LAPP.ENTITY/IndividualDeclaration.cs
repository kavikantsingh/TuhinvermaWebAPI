using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class IndividualDeclaration : BaseEntity

    {
        public int IndividualDeclarationId { get; set; }
        public int IndividualId { get; set; }
        public int ApplicationId { get; set; }
        public int ContentItemLkId { get; set; }
        public int ContentItem { get; set; }
        public bool ContentItemResponse { get; set; }
        public string Desc { get; set; }
        public DateTime DeclarationDate { get; set; }
        public DateTime DateofApplication { get; set; }
        public bool NoticeofMandatoryReporter { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Guid IndividualDeclarationGuid { get; set; }
    }
}
