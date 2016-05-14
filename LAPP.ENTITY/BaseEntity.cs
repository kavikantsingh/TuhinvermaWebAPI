using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class BaseEntity
    {

    }

    public class BaseEntityServiceResponse
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public string StatusCode { get; set; }
        public string ResponseReason { get; set; }

    }
}
