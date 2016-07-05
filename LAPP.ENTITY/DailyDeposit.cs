using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
    public class DailyDeposit : BaseEntity
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string license { get; set; }
        public string paymentmethod { get; set; }
        public string amount { get; set; }
        public string date { get; set; }
        public string Confirmation { get; set; }
        public string transactiontype { get; set; }
    }


    public class DailyDepositResponse : BaseEntityServiceResponse
    {
        public List<DailyDeposit> DeliveryType { get; set; }
    }


}