using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class ProviderIndividualName : ProviderIndividualNameResponse

    {
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ProviderIndvNameInfoGuid { get; set; }
    }

    public class ProviderIndividualNameResponse : BaseEntity

    {
        public int ProviderIndvNameInfoId { get; set; }
        public int ProviderId { get; set; }
        public int IndividualId { get; set; }
        public int IndividualNameId { get; set; }
        public int ApplicationId { get; set; }
        public string ProvIndvJobTitle { get; set; }
        public int ProvIndvJobTitleId { get; set; }
        public bool DoyouSupervise { get; set; }
        public bool BackgroundCheckRequired { get; set; }
        public bool IsLicensedorHasBeenLicensed { get; set; }
        public string ProvIndLicenseNumber { get; set; }
        public string ProvIndStateLicensed { get; set; }
        public DateTime? ProvIndLicenseExpDate { get; set; }
        public bool AreyouSupervised { get; set; }
        public bool NatureofInterest { get; set; }
        public string ReferenceNumber { get; set; }
        public bool IsActive { get; set; }
    }


    public class ProviderIndividualNameRequest : BaseEntityServiceResponse
    {
        public List<ProviderIndividualNameResponse> ProviderIndividualNameResponseList { get; set; }
    }
}
