using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class ProviderUser : ProviderUserResponse

    {
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ProviderUserGuid { get; set; }
    }

    public class ProviderUserResponse : BaseEntity

    {
        public int ProviderUserId { get; set; }
        public int ProviderId { get; set; }
        public int ApplicationId { get; set; }
        public int UserId { get; set; }
        public int IndividualId { get; set; }
        public int IndividualNameId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
    }


    public class ProviderUserRequest : BaseEntityServiceResponse
    {
        public List<ProviderUserResponse> ProviderUserResponseList { get; set; }
    }
}
