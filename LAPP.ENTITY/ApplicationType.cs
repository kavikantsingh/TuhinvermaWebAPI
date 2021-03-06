using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class ApplicationType : BaseEntity
    {
        public int ApplicationTypeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

    }

    public class ApplicationTypeGet : BaseEntity
    {
        public int ApplicationTypeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }


    public class ApplicationTypeGetResponse : BaseEntityServiceResponse
    {
        public List<ApplicationTypeGet> ApplicationTypeGetList { get; set; }
    }
}
