using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class IndividualCommunicationToLog
    {

        public int IndividualCommunicationToLogId { get; set; }
        public int? IndividualCommunicationLogId { get; set; }
        public int? IndividualId { get; set; }
        public int? ApplicationId { get; set; }
        public string EmailTo { get; set; }
        public int? UserIdTo { get; set; }
        public string IndividualCommunicationToLogGuid { get; set; }
    }
}
