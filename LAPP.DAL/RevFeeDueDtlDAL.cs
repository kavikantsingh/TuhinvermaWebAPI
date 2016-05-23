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
    public class RevFeeDueDtlDAL : BaseDAL
    {
        public int Save_RevFeeDueDtl(RevFeeDueDtl objRevFeeDueDtl)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("RevFeeDueDtlId", objRevFeeDueDtl.RevFeeDueDtlId));
            lstParameter.Add(new MySqlParameter("RevFeeDueId", objRevFeeDueDtl.RevFeeDueId));
            lstParameter.Add(new MySqlParameter("IndividualId", objRevFeeDueDtl.IndividualId));
            lstParameter.Add(new MySqlParameter("ProviderId", GetNullValue.ByDataType(objRevFeeDueDtl.ProviderId)));
            lstParameter.Add(new MySqlParameter("IndividualLicenseId", objRevFeeDueDtl.IndividualLicenseId));
            lstParameter.Add(new MySqlParameter("LicenseTypeId", objRevFeeDueDtl.LicenseTypeId));
            lstParameter.Add(new MySqlParameter("InvoiceNo", objRevFeeDueDtl.InvoiceNo));
            lstParameter.Add(new MySqlParameter("ReferenceNumber", objRevFeeDueDtl.ReferenceNumber));
            lstParameter.Add(new MySqlParameter("PaymentNo", objRevFeeDueDtl.PaymentNo));
            lstParameter.Add(new MySqlParameter("ReceiptNo", objRevFeeDueDtl.ReceiptNo));
            lstParameter.Add(new MySqlParameter("ControlNo", objRevFeeDueDtl.ControlNo));
            lstParameter.Add(new MySqlParameter("FeePaidAmount", objRevFeeDueDtl.FeePaidAmount));
            lstParameter.Add(new MySqlParameter("FeeDuePaymentDate", objRevFeeDueDtl.FeeDuePaymentDate));

            lstParameter.Add(new MySqlParameter("CreatedBy", objRevFeeDueDtl.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objRevFeeDueDtl.CreatedOn));
            lstParameter.Add(new MySqlParameter("RevFeeDueDtlGuid", objRevFeeDueDtl.RevFeeDueDtlGuid));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "RevFeeDueDtl_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<RevFeeDueDtl> Get_All_RevFeeDueDtl()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "RevFeeDueDtl_GET_ALL");
            List<RevFeeDueDtl> lstEntity = new List<RevFeeDueDtl>();
            RevFeeDueDtl objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<RevFeeDueDtl> Get_RevFeeDueDtl_by_IndividualId(int IndividualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            //lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "RevFeeDueDtl_GET_BY_IndividualId", lstParameter.ToArray());
            List<RevFeeDueDtl> lstEntity = new List<RevFeeDueDtl>();
            RevFeeDueDtl objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public RevFeeDueDtl Get_RevFeeDueDtl_By_RevFeeDueDtlId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_RevFeeDueDtlId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "RevFeeDueDtl_GET_BY_RevFeeDueDtlId", lstParameter.ToArray());
            RevFeeDueDtl objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private RevFeeDueDtl FetchEntity(DataRow dr)
        {
            RevFeeDueDtl objEntity = new RevFeeDueDtl();

            if (dr.Table.Columns.Contains("RevFeeDueDtlId") && dr["RevFeeDueDtlId"] != DBNull.Value)
            {
                objEntity.RevFeeDueDtlId = Convert.ToInt32(dr["RevFeeDueDtlId"]);
            }
            if (dr.Table.Columns.Contains("RevFeeDueId") && dr["RevFeeDueId"] != DBNull.Value)
            {
                objEntity.RevFeeDueId = Convert.ToInt32(dr["RevFeeDueId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
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


            if (dr.Table.Columns.Contains("InvoiceNo") && dr["InvoiceNo"] != DBNull.Value)
            {
                objEntity.InvoiceNo = Convert.ToString(dr["InvoiceNo"]);
            }
            if (dr.Table.Columns.Contains("ReferenceNumber") && dr["ReferenceNumber"] != DBNull.Value)
            {
                objEntity.ReferenceNumber = Convert.ToString(dr["ReferenceNumber"]);
            }
            if (dr.Table.Columns.Contains("PaymentNo") && dr["PaymentNo"] != DBNull.Value)
            {
                objEntity.PaymentNo = Convert.ToString(dr["PaymentNo"]);
            }
            if (dr.Table.Columns.Contains("ReceiptNo") && dr["ReceiptNo"] != DBNull.Value)
            {
                objEntity.ReceiptNo = Convert.ToString(dr["ReceiptNo"]);
            }
            if (dr.Table.Columns.Contains("ControlNo") && dr["ControlNo"] != DBNull.Value)
            {
                objEntity.ControlNo = Convert.ToString(dr["ControlNo"]);
            }
            if (dr.Table.Columns.Contains("FeePaidAmount") && dr["FeePaidAmount"] != DBNull.Value)
            {
                objEntity.FeePaidAmount = Convert.ToDecimal(dr["FeePaidAmount"]);
            }
            if (dr.Table.Columns.Contains("FeeDuePaymentDate") && dr["FeeDuePaymentDate"] != DBNull.Value)
            {
                objEntity.FeeDuePaymentDate = Convert.ToDateTime(dr["FeeDuePaymentDate"]);
            }



            if (dr.Table.Columns.Contains("CreatedBy") && dr["CreatedBy"] != DBNull.Value)
            {
                objEntity.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
            }
            if (dr.Table.Columns.Contains("CreatedOn") && dr["CreatedOn"] != DBNull.Value)
            {
                objEntity.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
            }
            if (dr.Table.Columns.Contains("RevFeeDueDtlGuid") && dr["RevFeeDueDtlGuid"] != DBNull.Value)
            {
                objEntity.RevFeeDueDtlGuid = Convert.ToString(dr["RevFeeDueDtlGuid"]);
            }

            return objEntity;

        }
    }
}
