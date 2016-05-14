using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using LAPP.ENTITY;
using LAPP.DAL;
namespace LAPP.BAL
{
    public class MasterTransactionBAL : BaseBAL
    {
        MasterTransactionDAL objDal = new MasterTransactionDAL();
        public int Save_MasterTransaction(MasterTransaction objMasterTransaction)
        {
            return objDal.Save_MasterTransaction(objMasterTransaction);
        }

        public List<MasterTransaction> Get_All_MasterTransaction()
        {
            return objDal.Get_All_MasterTransaction();
        }


    }
}
