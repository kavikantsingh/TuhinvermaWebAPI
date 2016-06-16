using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class ContentItemLk : BaseEntity
    {
        public int ContentItemLkId { get; set; }
        public int ContentLkToPageTabSectionId { get; set; }
        public string ContentItemLkCode { get; set; }
        public int ContentItemHash { get; set; }
        public string ContentItemLkDesc { get; set; }
        public int SortOrder { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsEditable { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class ContentItemLkResponse : BaseEntityServiceResponse
    {
        public object ContentItemLk { get; set; }
    }
}
