using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class IndividualName : IndividualNameRequest
    {
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public string IndividualNameGuid { get; set; }
    }

    public class IndividualNameRequest : BaseEntity
    {
        public int IndividualNameId { get; set; }
        public int IndividualId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int? SuffixId { get; set; }
        public int IndividualNameStatusId { get; set; }
        public int IndividualNameTypeId { get; set; }
        public bool IsActive { get; set; }
    }


    public class IndividualNameRequestResponse : BaseEntityServiceResponse
    {
        public List<IndividualNameRequest> IndividualNameResponse { get; set; }
    }
}
