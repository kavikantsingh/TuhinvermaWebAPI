using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class Providermblex : ProvidermblexResponse
    {

        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ProviderMBLExIdGuid { get; set; }

    }
    public class ProvidermblexResponse : BaseEntity
    {
        public int ProviderMBLExId { get; set; }
        public int ProviderId { get; set; }
        public int ApplicationId { get; set; }
        public string MBLExName { get; set; }
        public string PassingRates { get; set; }
        public int PassingYear { get; set; }
        public DateTime DateEntered { get; set; }
        public string PassingHalf { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string MblexGuid { get; set; }
    }



    public class ProvidermblexResponseRequest : BaseEntityServiceResponse
    {
        public List<ProvidermblexResponse> ProvidermblexResponseList { get; set; }
    }


}