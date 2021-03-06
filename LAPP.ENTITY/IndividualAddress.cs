using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class IndividualAddressResponse : AddressResponse

    {
        public int IndividualAddressId { get; set; }
        public int IndividualId { get; set; }
        public int AddressId { get; set; }
        public int AddressTypeId { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsMailingSameasPhysical { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int AdressStatusId { get; set; }
        public bool UseUserAddress { get; set; }
        public bool UseVerifiedAddress { get; set; }
    }

    public class IndividualAddress : IndividualAddressResponse

    {

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string IndividualAddressGuid { get; set; }
    }


    public class IndividualAddressResponseRequest : BaseEntityServiceResponse
    {
        public List<IndividualAddressResponse> IndividualAddressResponse { get; set; }
    }
}
