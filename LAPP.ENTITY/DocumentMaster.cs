using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class DocumentMaster : BaseEntity
    {
        public int DocumentMasterId { get; set; }
        public int DocumentId { get; set; }
        public string DocumentCd { get; set; }
        public string DocumentName { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentTypeIdName { get; set; }
        public string DocumentTypeDesc { get; set; }
        public int Max_size { get; set; }
        public int MasterTransactionId { get; set; }
        public int PageModuleId { get; set; }
        public int PageModuleTabSubModuleId { get; set; }
        public int PageTabSectionId { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsEditable { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class DocumentMasterResponse : BaseEntityServiceResponse
    {
        public List<DocumentMaster> DocumentMaster { get; set; }
    }

    public class DocumentMasterGET : BaseEntity
    {
        public int DocumentTypeId { get; set; }
        public string DocumentTypeIdName { get; set; }
        public string DocumentTypeDesc { get; set; }
        public int Max_size { get; set; }
    }

    public class DocumentMasterGETResponse : BaseEntityServiceResponse
    {
        public List<DocumentMasterGET> DocumentMasterGET { get; set; }
    }
}
