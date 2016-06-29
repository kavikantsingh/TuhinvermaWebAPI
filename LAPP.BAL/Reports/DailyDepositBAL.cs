using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using LAPP.ENTITY;
using LAPP.DAL;

namespace LAPP.BAL.Reports
{
    public class DailyDepositBAL : BaseBAL
    {
        DailyDepositDAL objDal = new DailyDepositDAL();
        public List<DailyDeposit> Get_All_DailyDeposits(string startDate, string endDate)
        {
            return objDal.Get_All_DailyDeposits(startDate, endDate);
        }
    }
}
