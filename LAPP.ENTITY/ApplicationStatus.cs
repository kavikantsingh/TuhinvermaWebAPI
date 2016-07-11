using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class ApplicationStatus : BaseEntity

    {
        public int ApplicationStatusId { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class ApplicationStatusGet : BaseEntity
    {
        public int ApplicationStatusId { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
    }


    public class ApplicationStatusGetResponse : BaseEntityServiceResponse
    {
        public List<ApplicationStatusGet> ApplicationStatusList { get; set; }
    }
}
