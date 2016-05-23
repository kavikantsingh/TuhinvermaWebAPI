using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class ShoppingCart : BaseEntity
    {
        public int ShoppingCartId { get; set; }
        public int IndividualId { get; set; }
        public int ProviderId { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ShoppingCartGuid { get; set; }
    }
}
