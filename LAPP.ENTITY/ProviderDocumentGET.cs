using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class ProviderDocument:BaseEntity
    {
        public int ProviderDocumentId { get; set; }
        public int ProviderId { get; set; }
        public int ApplicationId { get; set; }
        public int DocumentId { get; set; }
        public string DocumentCd { get; set; }
        public int DocumentTypeId { get; set; }
        public int DocumentLkToPageTabSectionId { get; set; }
        public string DocumentLkToPageTabSectionCode { get; set; }
        public string DocumentName { get; set; }
        public string OtherDocumentTypeName { get; set; }
        public string DocumentPath { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsDocumentUploadedbyProvider { get; set; }
        public bool IsDocumentUploadedbyStaff { get; set; }
        public string ReferenceNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ProviderDocumentGuid { get; set; }
    }

    public class ProviderDocumentResponse : BaseEntityServiceResponse
    {
        public ProviderDocument ProviderDocument { get; set; }
    }

        public class ProviderDocumentGET : BaseEntity
    {
        public int ProviderId { get; set; }
        public int DocumentId { get; set; }
        public int ProviderDocumentId { get; set;}
        public string DocumentTypeIdName { get; set; }
        public string DocumentTypeDesc { get; set; }
    }

    public class ProviderDocumentGETResponse: BaseEntityServiceResponse
    {
        public List<ProviderDocumentGET> ProviderDocumentGET { get; set; }
    }
}
