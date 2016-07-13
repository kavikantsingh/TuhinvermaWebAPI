using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class ProviderStaff : BaseEntity

    {
        public string ProviderStaffFirstName { get; set; }
        public string ProviderStaffMiddleName { get; set; }
        public string ProviderStaffLastName { get; set; }
        public string ProviderStaffEmail { get; set; }
        public int ProviderStaffId { get; set; }
        public int ProviderIndvNameInfoId { get; set; }
        public int ProviderId { get; set; }
        public int ApplicationId { get; set; }
        public int ProviderContactId { get; set; }
        public bool IsBackgroundCheckReq { get; set; }
        public string CAMTCNumber { get; set; }
        public string ReferenceNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ProviderStaffGuid { get; set; }
        public string ids { get; set; }
        public string titles { get; set; }
    }

    public class ProvIndvNameTitle : ProviderStaff
    {

        //public int IndividualId { get; set; }
        //public int IndividualNameId { get; set; }
        //public string ProvIndvJobTitle { get; set; }
        //public int ProvIndvJobTitleId { get; set; }
        //public bool DoyouSupervise { get; set; }
        //public bool BackgroundCheckRequired { get; set; }
        //public bool IsLicensedorHasBeenLicensed { get; set; }
        //public string ProvIndLicenseNumber { get; set; }
        //public string ProvIndStateLicensed { get; set; }
        //public DateTime ProvIndLicenseExpDate { get; set; }
        //public bool AreyouSupervised { get; set; }
        //public bool NatureofInterest { get; set; }
        //public string ProviderIndvNameInfoGuid { get; set; }
        public int ProvIndvNameTitlePosId { get; set; }
        public string ProvIndvNameTitlePositionId { get; set; }
        public string ProvIndvNameTitlePosition { get; set; }
        public string ProvIndvNameTitlePosGuid { get; set; }
        
    }

    public class ProviderStaffGetResponse : BaseEntityServiceResponse
    {
        public List<ProviderStaff> ProviderStaffList { get; set; }
    }
}
