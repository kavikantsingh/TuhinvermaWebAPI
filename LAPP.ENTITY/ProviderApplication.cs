using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class ProviderApplication : BaseEntity

    {
        public int ProviderId { get; set; }
        public int ApplicationId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Guid ProviderApplicationGuid { get; set; }
    }
}
