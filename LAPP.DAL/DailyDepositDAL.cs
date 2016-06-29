using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;

namespace LAPP.DAL
{
    public class DailyDepositDAL : BaseDAL
    {
        public List<DailyDeposit> Get_All_DailyDeposits(String startDate, string endDate)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            lstParameter.Add(new MySqlParameter("startDate", Convert.ToDateTime(startDate)));//   DateTime.Now.AddDays(-45) ));        
            lstParameter.Add(new MySqlParameter("endDate", Convert.ToDateTime(endDate))); // DateTime.Now.AddDays(1)));

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Get_All_DataDeposit", lstParameter.ToArray());

            List<DailyDeposit> lstEntity = new List<DailyDeposit>();
            DailyDeposit objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }
        private DailyDeposit FetchEntity(DataRow dr)
        {
            DailyDeposit objEntity = new DailyDeposit();
            if (dr.Table.Columns.Contains("FirstName") && dr["FirstName"] != DBNull.Value)
            {
                objEntity.firstname = Convert.ToString(dr["FirstName"]);
            }
            if (dr.Table.Columns.Contains("LastName") && dr["LastName"] != DBNull.Value)
            {
                objEntity.lastname = Convert.ToString(dr["LastName"]);
            }
            if (dr.Table.Columns.Contains("LicenseNumber") && dr["LicenseNumber"] != DBNull.Value)
            {
                objEntity.license = Convert.ToString(dr["LicenseNumber"]);
            }
            if (dr.Table.Columns.Contains("ReceiptNo/InvoiceNo") && dr["ReceiptNo/InvoiceNo"] != DBNull.Value)
            {
                objEntity.paymentmethod = Convert.ToString(dr["ReceiptNo/InvoiceNo"]);
            }
            if (dr.Table.Columns.Contains("AmountDue") && dr["AmountDue"] != DBNull.Value)
            {
                objEntity.amount = Convert.ToString(dr["AmountDue"]);
            }
            if (dr.Table.Columns.Contains("PaymentDate") && dr["PaymentDate"] != DBNull.Value)
            {
                objEntity.date = Convert.ToString(dr["PaymentDate"]);
            }
            if (dr.Table.Columns.Contains("ConfirmationNo") && dr["ConfirmationNo"] != DBNull.Value)
            {
                objEntity.Confirmation = Convert.ToString(dr["ConfirmationNo"]);
            }
            if (dr.Table.Columns.Contains("TransactionRefNo") && dr["TransactionRefNo"] != DBNull.Value)
            {
                objEntity.transactiontype = Convert.ToString(dr["TransactionRefNo"]);
            }
           
            return objEntity;

        }
    }
}
