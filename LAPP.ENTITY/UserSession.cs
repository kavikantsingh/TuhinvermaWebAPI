using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class UserSession : BaseEntity
    {
        public Int64 TokenID { get; set; }
        public int UserID { get; set; }
        public string SessionGUID { get; set; }
        public string UserHostIPAddress { get; set; }
        public string RequestBrowsertypeVersion { get; set; }
        public string BrowserUniqueID { get; set; }
        public string AppDomainName { get; set; }
        public string Key { get; set; }
        public DateTime? IssuedOn { get; set; }
        public bool Expired { get; set; }
        public DateTime? ExpiredOn { get; set; }
    }

    public class UserSessionResponse : BaseEntityServiceResponse
    {
        public List<UserSession> UserSession { get; set; }
    }
}
