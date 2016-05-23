using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class RevFeeMaster : BaseEntity

    {
        public int RevFeeMasterId { get; set; }
        public string FeeAccountCode { get; set; }
        public string FeeName { get; set; }
        public decimal FeeAmount { get; set; }
        public int FeeTypeId { get; set; }
        public int LicenseTypeId { get; set; }
        public int FiscalYear { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int FeeAcctId { get; set; }
        public int FeeFundId { get; set; }
        public int FeeGrantId { get; set; }
        public int FeeProjectId { get; set; }
        public int FeeOrder { get; set; }
        public bool IsFeeOverrideAllowed { get; set; }
        public bool IsFeeExemptionAllowed { get; set; }
        public bool IsRecurringFee { get; set; }
        public bool IsFeeRefundable { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
