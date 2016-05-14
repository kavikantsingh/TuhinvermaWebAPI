using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    /// <summary>
    /// 
    /// </summary>
    public class Token : BaseEntity
    {

        public int UserId { get; set; }
        public Int64 TokenId { get; set; }
        public string UserHostIPAdress { get; set; }
        public string RequestBrowsertypeVersion { get; set; }

    }


}
