using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using LAPP.ENTITY;
using MySql.Data.MySqlClient;
namespace LAPP.DAL
{
    public class TransactionDAL : BaseDAL
    {
        public int Save_Transaction(Transaction objTransaction)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("TransactionId", objTransaction.TransactionId));
            lstParameter.Add(new MySqlParameter("MasterTransactionId", GetNullValue.ByDataType(objTransaction.MasterTransactionId)));
            lstParameter.Add(new MySqlParameter("IndividualId", objTransaction.IndividualId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objTransaction.ApplicationId));
            lstParameter.Add(new MySqlParameter("ProviderId", GetNullValue.ByDataType(objTransaction.ProviderId)));
            lstParameter.Add(new MySqlParameter("ShoppingCartId", objTransaction.ShoppingCartId));
            lstParameter.Add(new MySqlParameter("IndividualLicenseId", objTransaction.IndividualLicenseId));
            lstParameter.Add(new MySqlParameter("LicenseTypeId", objTransaction.LicenseTypeId));
            lstParameter.Add(new MySqlParameter("LicenseNumber", objTransaction.LicenseNumber));
            lstParameter.Add(new MySqlParameter("TransactionStartDatetime", objTransaction.TransactionStartDatetime));
            lstParameter.Add(new MySqlParameter("TransactionEndDatetime", objTransaction.TransactionEndDatetime));
            lstParameter.Add(new MySqlParameter("TransactionStatus", objTransaction.TransactionStatus));
            lstParameter.Add(new MySqlParameter("TransactionInterruptReasonId", GetNullValue.ByDataType(objTransaction.TransactionInterruptReasonId)));
            lstParameter.Add(new MySqlParameter("TransactionDeviceTy", objTransaction.TransactionDeviceTy));

            lstParameter.Add(new MySqlParameter("CreatedBy", objTransaction.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objTransaction.CreatedOn));
            lstParameter.Add(new MySqlParameter("TransactionGuid", objTransaction.TransactionGuid));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "Transaction_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<Transaction> Get_All_Transaction()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Transaction_GET_ALL");
            List<Transaction> lstEntity = new List<Transaction>();
            Transaction objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<Transaction> Get_Transaction_by_ApplicationId(int ApplicationId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_ApplicationId", ApplicationId));
         
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Transaction_GET_BY_ApplicationId", lstParameter.ToArray());
            List<Transaction> lstEntity = new List<Transaction>();
            Transaction objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<Transaction> Get_Transaction_by_IndividualId(int IndividualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
           
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Transaction_GET_BY_IndividualId", lstParameter.ToArray());
            List<Transaction> lstEntity = new List<Transaction>();
            Transaction objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }


        public Transaction Get_Transaction_By_TransactionId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_TransactionId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Transaction_GET_BY_TransactionId", lstParameter.ToArray());
            Transaction objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private Transaction FetchEntity(DataRow dr)
        {
            Transaction objEntity = new Transaction();

            if (dr.Table.Columns.Contains("TransactionId") && dr["TransactionId"] != DBNull.Value)
            {
                objEntity.TransactionId = Convert.ToInt32(dr["TransactionId"]);
            }
            if (dr.Table.Columns.Contains("MasterTransactionId") && dr["MasterTransactionId"] != DBNull.Value)
            {
                objEntity.MasterTransactionId = Convert.ToInt32(dr["MasterTransactionId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationId") && dr["ApplicationId"] != DBNull.Value)
            {
                objEntity.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
            }
            if (dr.Table.Columns.Contains("ProviderId") && dr["ProviderId"] != DBNull.Value)
            {
                objEntity.ProviderId = Convert.ToInt32(dr["ProviderId"]);
            }

            if (dr.Table.Columns.Contains("ShoppingCartId") && dr["ShoppingCartId"] != DBNull.Value)
            {
                objEntity.ShoppingCartId = Convert.ToInt32(dr["ShoppingCartId"]);
            }
            if (dr.Table.Columns.Contains("IndividualLicenseId") && dr["IndividualLicenseId"] != DBNull.Value)
            {
                objEntity.IndividualLicenseId = Convert.ToInt32(dr["IndividualLicenseId"]);
            }
            if (dr.Table.Columns.Contains("LicenseTypeId") && dr["LicenseTypeId"] != DBNull.Value)
            {
                objEntity.LicenseTypeId = Convert.ToInt32(dr["LicenseTypeId"]);
            }
            if (dr.Table.Columns.Contains("TransactionStatus") && dr["TransactionStatus"] != DBNull.Value)
            {
                objEntity.TransactionStatus = Convert.ToString(dr["TransactionStatus"]);
            }
            if (dr.Table.Columns.Contains("LicenseNumber") && dr["LicenseNumber"] != DBNull.Value)
            {
                objEntity.LicenseNumber = Convert.ToString(dr["LicenseNumber"]);
            }
            if (dr.Table.Columns.Contains("TransactionStartDatetime") && dr["TransactionStartDatetime"] != DBNull.Value)
            {
                objEntity.TransactionStartDatetime = Convert.ToDateTime(dr["TransactionStartDatetime"]);
            }
            if (dr.Table.Columns.Contains("TransactionEndDatetime") && dr["TransactionEndDatetime"] != DBNull.Value)
            {
                objEntity.TransactionEndDatetime = Convert.ToDateTime(dr["TransactionEndDatetime"]);
            }
            if (dr.Table.Columns.Contains("TransactionInterruptReasonId") && dr["TransactionInterruptReasonId"] != DBNull.Value)
            {
                objEntity.TransactionInterruptReasonId = Convert.ToInt32(dr["TransactionInterruptReasonId"]);
            }
            if (dr.Table.Columns.Contains("TransactionDeviceTy") && dr["TransactionDeviceTy"] != DBNull.Value)
            {
                objEntity.TransactionDeviceTy = Convert.ToString(dr["TransactionDeviceTy"]);
            }

            if (dr.Table.Columns.Contains("CreatedBy") && dr["CreatedBy"] != DBNull.Value)
            {
                objEntity.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
            }
            if (dr.Table.Columns.Contains("CreatedOn") && dr["CreatedOn"] != DBNull.Value)
            {
                objEntity.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
            }

            if (dr.Table.Columns.Contains("TransactionGuid") && dr["TransactionGuid"] != DBNull.Value)
            {
                objEntity.TransactionGuid = dr["TransactionGuid"].ToString();
            }

            return objEntity;

        }
    }
}
