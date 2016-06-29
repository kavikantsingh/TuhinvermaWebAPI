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
    public class VerifyDataBAL : BaseBAL
    {
        VerifyDataDAL objDal = new VerifyDataDAL();
        public List<VerifyDataEntity> Get_All_VerifyData()
        {
            return objDal.Get_All_VerifyData();
        }
    }
}
