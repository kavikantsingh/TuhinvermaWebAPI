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
    public class RevFeeDisbDAL : BaseDAL
    {
        public int Save_RevFeeDisb(RevFeeDisb objRevFeeDisb)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("RevFeeDisbId", objRevFeeDisb.RevFeeDisbId));
            lstParameter.Add(new MySqlParameter("TransactionId", objRevFeeDisb.TransactionId));
            lstParameter.Add(new MySqlParameter("ShoppingCartId", objRevFeeDisb.ShoppingCartId));
            lstParameter.Add(new MySqlParameter("RevFeeMasterId", objRevFeeDisb.RevFeeMasterId));
            lstParameter.Add(new MySqlParameter("MasterTransactionId", GetNullValue.ByDataType(objRevFeeDisb.MasterTransactionId)));
            lstParameter.Add(new MySqlParameter("IndividualId", objRevFeeDisb.IndividualId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objRevFeeDisb.ApplicationId));
            lstParameter.Add(new MySqlParameter("ProviderId",  GetNullValue.ByDataType(objRevFeeDisb.ProviderId)));
            lstParameter.Add(new MySqlParameter("IndividualLicenseId", objRevFeeDisb.IndividualLicenseId));
            lstParameter.Add(new MySqlParameter("LicenseTypeId", objRevFeeDisb.LicenseTypeId));
            lstParameter.Add(new MySqlParameter("RevFeeDueId", objRevFeeDisb.RevFeeDueId));
            lstParameter.Add(new MySqlParameter("FinclTranDate", objRevFeeDisb.FinclTranDate));
            lstParameter.Add(new MySqlParameter("PaymentPostDate", objRevFeeDisb.PaymentPostDate));
            lstParameter.Add(new MySqlParameter("InvoiceNo", objRevFeeDisb.InvoiceNo));
            lstParameter.Add(new MySqlParameter("FeePaidAmount", objRevFeeDisb.FeePaidAmount));
            lstParameter.Add(new MySqlParameter("OrigFeeAmount", objRevFeeDisb.OrigFeeAmount));
            lstParameter.Add(new MySqlParameter("ControlNo", objRevFeeDisb.ControlNo));
            lstParameter.Add(new MySqlParameter("PaymentNo", objRevFeeDisb.PaymentNo));
            lstParameter.Add(new MySqlParameter("ReferenceNumber", objRevFeeDisb.ReferenceNumber));

            lstParameter.Add(new MySqlParameter("CreatedBy", objRevFeeDisb.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objRevFeeDisb.CreatedOn));
            lstParameter.Add(new MySqlParameter("RevFeeDisbGuid", objRevFeeDisb.RevFeeDisbGuid));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "RevFeeDisb_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<RevFeeDisb> Get_RevFeeDisb_by_ApplicationId(int ApplicationId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_ApplicationId", ApplicationId));
         
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "RevFeeDisb_GET_BY_ApplicationId", lstParameter.ToArray());
            List<RevFeeDisb> lstEntity = new List<RevFeeDisb>();
            RevFeeDisb objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<RevFeeDisb> Get_All_RevFeeDisb()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "RevFeeDisb_GET_ALL");
            List<RevFeeDisb> lstEntity = new List<RevFeeDisb>();
            RevFeeDisb objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<RevFeeDisb> Get_RevFeeDisb_by_IndividualId(int IndividualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            //lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "RevFeeDisb_GET_BY_IndividualId", lstParameter.ToArray());
            List<RevFeeDisb> lstEntity = new List<RevFeeDisb>();
            RevFeeDisb objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public RevFeeDisb Get_RevFeeDisb_By_RevFeeDisbId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_RevFeeDisbId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "RevFeeDisb_GET_BY_RevFeeDisbId", lstParameter.ToArray());
            RevFeeDisb objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private RevFeeDisb FetchEntity(DataRow dr)
        {
            RevFeeDisb objEntity = new RevFeeDisb();

            if (dr.Table.Columns.Contains("RevFeeDisbId") && dr["RevFeeDisbId"] != DBNull.Value)
            {
                objEntity.RevFeeDisbId = Convert.ToInt32(dr["RevFeeDisbId"]);
            }
            if (dr.Table.Columns.Contains("TransactionId") && dr["TransactionId"] != DBNull.Value)
            {
                objEntity.TransactionId = Convert.ToInt32(dr["TransactionId"]);
            }
            if (dr.Table.Columns.Contains("ShoppingCartId") && dr["ShoppingCartId"] != DBNull.Value)
            {
                objEntity.ShoppingCartId = Convert.ToInt32(dr["ShoppingCartId"]);
            }
            if (dr.Table.Columns.Contains("RevFeeMasterId") && dr["RevFeeMasterId"] != DBNull.Value)
            {
                objEntity.RevFeeMasterId = Convert.ToInt32(dr["RevFeeMasterId"]);
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
            if (dr.Table.Columns.Contains("IndividualLicenseId") && dr["IndividualLicenseId"] != DBNull.Value)
            {
                objEntity.IndividualLicenseId = Convert.ToInt32(dr["IndividualLicenseId"]);
            }
            if (dr.Table.Columns.Contains("LicenseTypeId") && dr["LicenseTypeId"] != DBNull.Value)
            {
                objEntity.LicenseTypeId = Convert.ToInt32(dr["LicenseTypeId"]);
            }
            if (dr.Table.Columns.Contains("RevFeeDueId") && dr["RevFeeDueId"] != DBNull.Value)
            {
                objEntity.RevFeeDueId = Convert.ToInt32(dr["RevFeeDueId"]);
            }

            if (dr.Table.Columns.Contains("FinclTranDate") && dr["FinclTranDate"] != DBNull.Value)
            {
                objEntity.FinclTranDate = Convert.ToDateTime(dr["FinclTranDate"]);
            }
            if (dr.Table.Columns.Contains("PaymentPostDate") && dr["PaymentPostDate"] != DBNull.Value)
            {
                objEntity.PaymentPostDate = Convert.ToDateTime(dr["PaymentPostDate"]);
            }

            if (dr.Table.Columns.Contains("InvoiceNo") && dr["InvoiceNo"] != DBNull.Value)
            {
                objEntity.InvoiceNo = Convert.ToString(dr["InvoiceNo"]);
            }

            if (dr.Table.Columns.Contains("FeePaidAmount") && dr["FeePaidAmount"] != DBNull.Value)
            {
                objEntity.FeePaidAmount = Convert.ToDecimal(dr["FeePaidAmount"]);
            }
            if (dr.Table.Columns.Contains("OrigFeeAmount") && dr["OrigFeeAmount"] != DBNull.Value)
            {
                objEntity.OrigFeeAmount = Convert.ToDecimal(dr["OrigFeeAmount"]);
            }

            if (dr.Table.Columns.Contains("ControlNo") && dr["ControlNo"] != DBNull.Value)
            {
                objEntity.ControlNo = Convert.ToString(dr["ControlNo"]);
            }
            if (dr.Table.Columns.Contains("PaymentNo") && dr["PaymentNo"] != DBNull.Value)
            {
                objEntity.PaymentNo = Convert.ToString(dr["PaymentNo"]);
            }
            if (dr.Table.Columns.Contains("ReferenceNumber") && dr["ReferenceNumber"] != DBNull.Value)
            {
                objEntity.ReferenceNumber = Convert.ToString(dr["ReferenceNumber"]);
            }


            if (dr.Table.Columns.Contains("CreatedBy") && dr["CreatedBy"] != DBNull.Value)
            {
                objEntity.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
            }
            if (dr.Table.Columns.Contains("CreatedOn") && dr["CreatedOn"] != DBNull.Value)
            {
                objEntity.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
            }
            if (dr.Table.Columns.Contains("RevFeeDisbGuid") && dr["RevFeeDisbGuid"] != DBNull.Value)
            {
                objEntity.RevFeeDisbGuid = Convert.ToString(dr["RevFeeDisbGuid"]);
            }

            return objEntity;

        }
    }
}
