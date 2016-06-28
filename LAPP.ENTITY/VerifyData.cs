using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class VerifyDataEntity : BaseEntity
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string middlename { get; set; }
        public string license { get; set; }
        public string status { get; set; }
        public string ExpirationDate { get; set; }
        public string CurrentLicenseDate { get; set; }
        public string OriginalLicenseDate { get; set; }
       
    }


    public class VerifyDataResponse : BaseEntityServiceResponse
    {
        public List<VerifyDataEntity> VerifyDataType { get; set; }
    }


}
