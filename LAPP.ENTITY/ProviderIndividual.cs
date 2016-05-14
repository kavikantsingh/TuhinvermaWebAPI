using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class ProviderIndividual : BaseEntity

    {
        public int ProviderId { get; set; }
        public int IndividualId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Guid ProviderIndividualGuid { get; set; }
    }
}
