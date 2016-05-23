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
    public class RevFeeCollectDAL : BaseDAL
    {
        public int Save_RevFeeCollect(RevFeeCollect objRevFeeCollect)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("RevFeeCollectId", objRevFeeCollect.RevFeeCollectId));
            lstParameter.Add(new MySqlParameter("ShoppingCartId", objRevFeeCollect.ShoppingCartId));
            lstParameter.Add(new MySqlParameter("IndividualId", objRevFeeCollect.IndividualId));
            lstParameter.Add(new MySqlParameter("ProviderId", GetNullValue.ByDataType(objRevFeeCollect.ProviderId)));
            lstParameter.Add(new MySqlParameter("IndividualLicenseId", objRevFeeCollect.IndividualLicenseId));
            lstParameter.Add(new MySqlParameter("LicenseTypeId", objRevFeeCollect.LicenseTypeId));
            lstParameter.Add(new MySqlParameter("ReceiptNo", objRevFeeCollect.ReceiptNo));
            lstParameter.Add(new MySqlParameter("AmountDue", objRevFeeCollect.AmountDue));
            lstParameter.Add(new MySqlParameter("PaymentMode", objRevFeeCollect.PaymentMode));
            lstParameter.Add(new MySqlParameter("PaymentModeNumber", objRevFeeCollect.PaymentModeNumber));
            lstParameter.Add(new MySqlParameter("PaidAmount", objRevFeeCollect.PaidAmount));
            lstParameter.Add(new MySqlParameter("PaymentDate", objRevFeeCollect.PaymentDate));
            lstParameter.Add(new MySqlParameter("InvoiceNo", objRevFeeCollect.InvoiceNo));
            lstParameter.Add(new MySqlParameter("UserDefinedRefNo", objRevFeeCollect.UserDefinedRefNo));
            lstParameter.Add(new MySqlParameter("UserDefinedPaymentNo", objRevFeeCollect.UserDefinedPaymentNo));
            lstParameter.Add(new MySqlParameter("RevCollectFeeNum", objRevFeeCollect.RevCollectFeeNum));
            lstParameter.Add(new MySqlParameter("RevFeePaidSource", objRevFeeCollect.RevFeePaidSource));
            lstParameter.Add(new MySqlParameter("CardType", objRevFeeCollect.CardType));
            lstParameter.Add(new MySqlParameter("ConfirmationNo", objRevFeeCollect.ConfirmationNo));
            lstParameter.Add(new MySqlParameter("TransactionRefNo", objRevFeeCollect.TransactionRefNo));
            lstParameter.Add(new MySqlParameter("PaymentBankName", objRevFeeCollect.PaymentBankName));
            lstParameter.Add(new MySqlParameter("ControlNo", objRevFeeCollect.ControlNo));
            lstParameter.Add(new MySqlParameter("PaymentNo", objRevFeeCollect.PaymentNo));
            lstParameter.Add(new MySqlParameter("ReferenceNumber", objRevFeeCollect.ReferenceNumber));

            lstParameter.Add(new MySqlParameter("CreatedBy", objRevFeeCollect.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objRevFeeCollect.CreatedOn));
            lstParameter.Add(new MySqlParameter("RevFeeCollectGuid", objRevFeeCollect.RevFeeCollectGuid));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "RevFeeCollect_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<RevFeeCollect> Get_All_RevFeeCollect()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "RevFeeCollect_GET_ALL");
            List<RevFeeCollect> lstEntity = new List<RevFeeCollect>();
            RevFeeCollect objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<RevFeeCollect> Get_RevFeeCollect_by_IndividualId(int IndividualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "RevFeeCollect_GET_BY_IndividualId", lstParameter.ToArray());
            List<RevFeeCollect> lstEntity = new List<RevFeeCollect>();
            RevFeeCollect objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public RevFeeCollect Get_RevFeeCollect_By_RevFeeCollectId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_RevFeeCollectId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "RevFeeCollect_GET_BY_RevFeeCollectId", lstParameter.ToArray());
            RevFeeCollect objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private RevFeeCollect FetchEntity(DataRow dr)
        {
            RevFeeCollect objEntity = new RevFeeCollect();

            if (dr.Table.Columns.Contains("RevFeeCollectId") && dr["RevFeeCollectId"] != DBNull.Value)
            {
                objEntity.RevFeeCollectId = Convert.ToInt32(dr["RevFeeCollectId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
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

            if (dr.Table.Columns.Contains("ReceiptNo") && dr["ReceiptNo"] != DBNull.Value)
            {
                objEntity.ReceiptNo = Convert.ToString(dr["ReceiptNo"]);
            }
            if (dr.Table.Columns.Contains("AmountDue") && dr["AmountDue"] != DBNull.Value)
            {
                objEntity.AmountDue = Convert.ToDecimal(dr["AmountDue"]);
            }
            if (dr.Table.Columns.Contains("PaymentMode") && dr["PaymentMode"] != DBNull.Value)
            {
                objEntity.PaymentMode = Convert.ToString(dr["PaymentMode"]);
            }
            if (dr.Table.Columns.Contains("PaymentModeNumber") && dr["PaymentModeNumber"] != DBNull.Value)
            {
                objEntity.PaymentModeNumber = Convert.ToString(dr["PaymentModeNumber"]);
            }
            if (dr.Table.Columns.Contains("PaidAmount") && dr["PaidAmount"] != DBNull.Value)
            {
                objEntity.PaidAmount = Convert.ToDecimal(dr["PaidAmount"]);
            }
            if (dr.Table.Columns.Contains("PaymentDate") && dr["PaymentDate"] != DBNull.Value)
            {
                objEntity.PaymentDate = Convert.ToDateTime(dr["PaymentDate"]);
            }


            if (dr.Table.Columns.Contains("InvoiceNo") && dr["InvoiceNo"] != DBNull.Value)
            {
                objEntity.InvoiceNo = Convert.ToString(dr["InvoiceNo"]);
            }

            if (dr.Table.Columns.Contains("UserDefinedRefNo") && dr["UserDefinedRefNo"] != DBNull.Value)
            {
                objEntity.UserDefinedRefNo = Convert.ToString(dr["UserDefinedRefNo"]);
            }

            if (dr.Table.Columns.Contains("UserDefinedPaymentNo") && dr["UserDefinedPaymentNo"] != DBNull.Value)
            {
                objEntity.UserDefinedPaymentNo = Convert.ToString(dr["UserDefinedPaymentNo"]);
            }

            if (dr.Table.Columns.Contains("RevCollectFeeNum") && dr["RevCollectFeeNum"] != DBNull.Value)
            {
                objEntity.RevCollectFeeNum = Convert.ToString(dr["RevCollectFeeNum"]);
            }

            if (dr.Table.Columns.Contains("RevFeePaidSource") && dr["RevFeePaidSource"] != DBNull.Value)
            {
                objEntity.RevFeePaidSource = Convert.ToString(dr["RevFeePaidSource"]);
            }

            if (dr.Table.Columns.Contains("CardType") && dr["CardType"] != DBNull.Value)
            {
                objEntity.CardType = Convert.ToString(dr["CardType"]);
            }

            if (dr.Table.Columns.Contains("ConfirmationNo") && dr["ConfirmationNo"] != DBNull.Value)
            {
                objEntity.ConfirmationNo = Convert.ToString(dr["ConfirmationNo"]);
            }

            if (dr.Table.Columns.Contains("TransactionRefNo") && dr["TransactionRefNo"] != DBNull.Value)
            {
                objEntity.TransactionRefNo = Convert.ToString(dr["TransactionRefNo"]);
            }

            if (dr.Table.Columns.Contains("PaymentBankName") && dr["PaymentBankName"] != DBNull.Value)
            {
                objEntity.PaymentBankName = Convert.ToString(dr["PaymentBankName"]);
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
            if (dr.Table.Columns.Contains("RevFeeCollectGuid") && dr["RevFeeCollectGuid"] != DBNull.Value)
            {
                objEntity.RevFeeCollectGuid = Convert.ToString(dr["RevFeeCollectGuid"]);
            }

            return objEntity;

        }
    }
}
