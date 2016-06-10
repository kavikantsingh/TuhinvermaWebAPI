using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class IndividualDocument : BaseEntity
    {
        public int IndividualDocumentId { get; set; }
        public int IndividualId { get; set; }
        public int? ApplicationId { get; set; }
        public int? DocumentLkToPageTabSectionId { get; set; }
        public string DocumentLkToPageTabSectionCode { get; set; }
        public string DocumentTypeName { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsDocumentUploadedbyIndividual { get; set; }
        public bool IsDocumentUploadedbyStaff { get; set; }
        public string ReferenceNumber { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string DocumentPath { get; set; }
        public string IndividualDocumentGuid { get; set; }
        public int? DocumentId { get; set; }
        public string DocumentCd { get; set; }
        public int? DocumentTypeId { get; set; }
        public string DocumentName { get; set; }
        public string OtherDocumentTypeName { get; set; }
    }

    public class IndividualDocumentGet : BaseEntity
    {
        public int IndividualDocumentId { get; set; }
        public int IndividualId { get; set; }
        public int? ApplicationId { get; set; }
        public int? DocumentLkToPageTabSectionId { get; set; }
        public string DocumentLkToPageTabSectionCode { get; set; }
        public string DocumentTypeName { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsDocumentUploadedbyIndividual { get; set; }
        public bool IsDocumentUploadedbyStaff { get; set; }
        public string ReferenceNumber { get; set; }

        public bool IsActive { get; set; }

        public int? DocumentId { get; set; }
        public string DocumentCd { get; set; }
        public int? DocumentTypeId { get; set; }
        public string DocumentName { get; set; }
        public string OtherDocumentTypeName { get; set; }
    }


    public class IndividualDocumentUpload : BaseEntity
    {
        public int IndividualId { get; set; }
        public int? ApplicationId { get; set; }
        public string Email { get; set; }
        public bool SendEmail { get; set; }
        public int TransactionId { get; set; }

        public List<DocumentToUpload> DocumentUploadList { get; set; }
    }
    public class IndividualDocumentByHTML : BaseEntity
    {
        public int IndividualId { get; set; }
        public int? ApplicationId { get; set; }
        public string Email { get; set; }
        public bool SendEmail { get; set; }
        public int TransactionId { get; set; }
        public string AffirmativeAction { get; set; }

        public List<DocumentToUploadByHTML> DocumentUploadList { get; set; }
    }
    public class DocumentToUpload : BaseEntity
    {
        public int IndividualDocumentId { get; set; }
        public int? DocumentLkToPageTabSectionId { get; set; }
        public string DocumentLkToPageTabSectionCode { get; set; }
        public string DocumentTypeName { get; set; }
        public string DocumentPath { get; set; }
        public string DocNameWithExtention { get; set; }
        public string DocStrBase64 { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsDocumentUploadedbyIndividual { get; set; }
        public bool IsDocumentUploadedbyStaff { get; set; }
        public string ReferenceNumber { get; set; }

        public int? DocumentId { get; set; }
        public string DocumentCd { get; set; }
        public int? DocumentTypeId { get; set; }
        public string OtherDocumentTypeName { get; set; }

    }
    public class DocumentToUploadByHTML : BaseEntity
    {
        public int IndividualDocumentId { get; set; }
        public int DocumentLkToPageTabSectionId { get; set; }
        public string DocumentLkToPageTabSectionCode { get; set; }
        public string DocumentTypeName { get; set; }
       // public string DocumentPath { get; set; }
        public string DocNameWithExtention { get; set; }
        public string HtmlString { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsDocumentUploadedbyIndividual { get; set; }
        public bool IsDocumentUploadedbyStaff { get; set; }
        // public string ReferenceNumber { get; set; }
        //   public System.Net.Http.HttpResponseMessage FileContent { get; set; }
        public int? DocumentId { get; set; }
        public string DocumentCd { get; set; }
        public int? DocumentTypeId { get; set; }
        public string OtherDocumentTypeName { get; set; }

    }
    public class HtmlToPdfDocument : BaseEntity
    {


        public string DocNameWithExtention { get; set; }
        public string HtmlString { get; set; }

    }

    public class IndividualDocumentUploadResponse : BaseEntityServiceResponse
    {
        public List<IndividualDocumentUpload> IndividualDocumentUploadList { get; set; }
    }
    public class IndividualDocumentByHtmlResponse : BaseEntityServiceResponse
    {
        public List<IndividualDocumentByHTML> IndividualDocumentUploadList { get; set; }
    }
    public class IndividualDocumentGetResponse : BaseEntityServiceResponse
    {
        public List<IndividualDocumentGet> IndividualDocumentGetList { get; set; }
    }
}
