using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class MasterTransaction : BaseEntity

    {
        public int MasterTransactionId { get; set; }
        public string MasterTransactionCode { get; set; }
        public string MasterTransactionName { get; set; }
        public string MasterTransactionDesc { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
    public class MasterTransactionResponse : BaseEntityServiceResponse
    {
        public List<MasterTransaction> MasterTransactionList { get; set; }
    }
}
