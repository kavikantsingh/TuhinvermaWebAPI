using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class ProviderBusinessType :BaseEntity
    {
        public int ProviderBusinessTypeId { get; set; }
        public int ProviderId { get; set; }
        public int ApplicationId { get; set; }
        public int BusinessOrgTypeId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ProviderBusinessTypeGuid { get; set; }
        public string Action { get; set; }
    }

    public class ProviderBusinessTypeGetResponse : BaseEntityServiceResponse
    {
        public List<ProviderBusinessType> ProviderBusinessTypeList { get; set; }
    }
}
