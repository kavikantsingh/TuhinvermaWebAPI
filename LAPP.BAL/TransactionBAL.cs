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
    public class TransactionBAL : BaseBAL
    {
        TransactionDAL objDal = new TransactionDAL();

        public int Save_Transaction(Transaction objTransaction)
        {
            return objDal.Save_Transaction(objTransaction);
        }

        public List<Transaction> Get_All_Transaction()
        {
            return objDal.Get_All_Transaction();
        }

        public List<Transaction> Get_Transaction_by_IndividualId(int IndividualId)
        {
            return objDal.Get_Transaction_by_IndividualId(IndividualId);
        }

        public List<Transaction> Get_Transaction_by_ApplicationId(int ApplicationId)
        {
            return objDal.Get_Transaction_by_ApplicationId(ApplicationId);
        }

        public Transaction Get_Transaction_By_TransactionId(int ID)
        {
            return objDal.Get_Transaction_By_TransactionId(ID);
        }

    }
}
