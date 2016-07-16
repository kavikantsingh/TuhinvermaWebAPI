using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class TransactionType : BaseEntity
    {
        public int Application_ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Fee { get; set; }
        public decimal Late_Fee { get; set; }
        public bool Is_Active { get; set; }
        public bool Is_Deleted { get; set; }
        public DateTime Created_On { get; set; }
        public int Created_By { get; set; }
        public DateTime Modified_On { get; set; }
        public int Modified_By { get; set; }
        public DateTime DTS { get; set; }
    }


    public class TransactionTypeResponse : BaseEntityServiceResponse
    {
        public List<TransactionType> TransactionTypeList { get; set; }
    }
}
