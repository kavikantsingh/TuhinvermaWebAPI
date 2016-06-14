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
    public class RevFeeDueDAL : BaseDAL
    {
        public int Save_RevFeeDue(RevFeeDue objRevFeeDue)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("RevFeeDueId", objRevFeeDue.RevFeeDueId));
            lstParameter.Add(new MySqlParameter("TransactionId", objRevFeeDue.TransactionId));
            lstParameter.Add(new MySqlParameter("RevFeeMasterId", objRevFeeDue.RevFeeMasterId));
            lstParameter.Add(new MySqlParameter("MasterTransactionId", GetNullValue.ByDataType(objRevFeeDue.MasterTransactionId)));
            lstParameter.Add(new MySqlParameter("IndividualId", objRevFeeDue.IndividualId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objRevFeeDue.ApplicationId));
            lstParameter.Add(new MySqlParameter("ProviderId", GetNullValue.ByDataType(objRevFeeDue.ProviderId)));
            lstParameter.Add(new MySqlParameter("IndividualLicenseId", objRevFeeDue.IndividualLicenseId));
            lstParameter.Add(new MySqlParameter("LicenseTypeId", objRevFeeDue.LicenseTypeId));
            lstParameter.Add(new MySqlParameter("BatchId", GetNullValue.ByDataType(objRevFeeDue.BatchId)));
            lstParameter.Add(new MySqlParameter("TaskId", GetNullValue.ByDataType(objRevFeeDue.TaskId)));
            lstParameter.Add(new MySqlParameter("FeeDueTypeId", GetNullValue.ByDataType(objRevFeeDue.FeeDueTypeId)));
            lstParameter.Add(new MySqlParameter("InvoiceNo", objRevFeeDue.InvoiceNo));
            lstParameter.Add(new MySqlParameter("InvoiceDate", objRevFeeDue.InvoiceDate));
            lstParameter.Add(new MySqlParameter("ReferenceNumber", objRevFeeDue.ReferenceNumber));
            lstParameter.Add(new MySqlParameter("ControlNo", objRevFeeDue.ControlNo));
            lstParameter.Add(new MySqlParameter("FeeAmount", objRevFeeDue.FeeAmount));
            lstParameter.Add(new MySqlParameter("FeeDueDate", objRevFeeDue.FeeDueDate));

            lstParameter.Add(new MySqlParameter("CreatedBy", objRevFeeDue.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objRevFeeDue.CreatedOn));
            lstParameter.Add(new MySqlParameter("RevFeeDueGuid", objRevFeeDue.RevFeeDueGuid));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "RevFeeDue_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<RevFeeDue> Get_Unpaid_RevFeeDue_by_IndividualId(int individualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", individualId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "RevFeeDue_GET_Unpaid_BY_IndividualId", lstParameter.ToArray());
            List<RevFeeDue> lstEntity = new List<RevFeeDue>();
            RevFeeDue objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;

        }

        public List<RevFeeDue> Get_All_RevFeeDue()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "RevFeeDue_GET_ALL");
            List<RevFeeDue> lstEntity = new List<RevFeeDue>();
            RevFeeDue objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        //public List<RevFeeDue> Get_RevFeeDue_by_IndividualIdAnd_ApplicationId(int IndividualId, int ApplicationId)
        //{
        //    DataSet ds = new DataSet("DS");
        //    DBHelper objDB = new DBHelper();
        //    List<MySqlParameter> lstParameter = new List<MySqlParameter>();
        //    lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
        //    lstParameter.Add(new MySqlParameter("G_ApplicationId", ApplicationId));
        //    //lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
        //    ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "RevFeeDue_GET_BY_IndividualId_AND_ApplicationId", lstParameter.ToArray());
        //    List<RevFeeDue> lstEntity = new List<RevFeeDue>();
        //    RevFeeDue objEntity = null;
        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        objEntity = FetchEntity(dr);
        //        if (objEntity != null)
        //            lstEntity.Add(objEntity);
        //    }
        //    return lstEntity;
        //}


        public RevFeeDue Get_RevFeeDue_by_IndividualIdAnd_ApplicationIdAndRevFeeMasterId(int IndividualId, int ApplicationId, int RevFeeMasterId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            lstParameter.Add(new MySqlParameter("G_ApplicationId", ApplicationId));
            lstParameter.Add(new MySqlParameter("G_RevFeeMasterId", RevFeeMasterId));
            //lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "RevFeeDue_GET_BY_IndivId_AND_AppliId_RevFeeMasId", lstParameter.ToArray());
            RevFeeDue objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<RevFeeDue> Get_RevFeeDue_by_IndividualId(int IndividualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            //lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "RevFeeDue_GET_BY_IndividualId", lstParameter.ToArray());
            List<RevFeeDue> lstEntity = new List<RevFeeDue>();
            RevFeeDue objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<RevFeeDue> Get_RevFeeDue_by_TransactionId(int TransactionId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_TransactionId", TransactionId));
            //lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "revfeedue_GET_BY_TransactionId", lstParameter.ToArray());
            List<RevFeeDue> lstEntity = new List<RevFeeDue>();
            RevFeeDue objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public RevFeeDue Get_RevFeeDue_By_RevFeeDueId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_RevFeeDueId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "RevFeeDue_GET_BY_RevFeeDueId", lstParameter.ToArray());
            RevFeeDue objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private RevFeeDue FetchEntity(DataRow dr)
        {
            RevFeeDue objEntity = new RevFeeDue();

            if (dr.Table.Columns.Contains("RevFeeDueId") && dr["RevFeeDueId"] != DBNull.Value)
            {
                objEntity.RevFeeDueId = Convert.ToInt32(dr["RevFeeDueId"]);
            }
            if (dr.Table.Columns.Contains("TransactionId") && dr["TransactionId"] != DBNull.Value)
            {
                objEntity.TransactionId = Convert.ToInt32(dr["TransactionId"]);
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
            if (dr.Table.Columns.Contains("BatchId") && dr["BatchId"] != DBNull.Value)
            {
                objEntity.BatchId = Convert.ToInt32(dr["BatchId"]);
            }

            if (dr.Table.Columns.Contains("TaskId") && dr["TaskId"] != DBNull.Value)
            {
                objEntity.TaskId = Convert.ToInt32(dr["TaskId"]);
            }
            if (dr.Table.Columns.Contains("FeeDueTypeId") && dr["FeeDueTypeId"] != DBNull.Value)
            {
                objEntity.FeeDueTypeId = Convert.ToInt32(dr["FeeDueTypeId"]);
            }
            if (dr.Table.Columns.Contains("InvoiceNo") && dr["InvoiceNo"] != DBNull.Value)
            {
                objEntity.InvoiceNo = Convert.ToString(dr["InvoiceNo"]);
            }
            if (dr.Table.Columns.Contains("InvoiceDate") && dr["InvoiceDate"] != DBNull.Value)
            {
                objEntity.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);
            }
            if (dr.Table.Columns.Contains("ReferenceNumber") && dr["ReferenceNumber"] != DBNull.Value)
            {
                objEntity.ReferenceNumber = Convert.ToString(dr["ReferenceNumber"]);
            }
            if (dr.Table.Columns.Contains("ControlNo") && dr["ControlNo"] != DBNull.Value)
            {
                objEntity.ControlNo = Convert.ToString(dr["ControlNo"]);
            }
            if (dr.Table.Columns.Contains("FeeAmount") && dr["FeeAmount"] != DBNull.Value)
            {
                objEntity.FeeAmount = Convert.ToDecimal(dr["FeeAmount"]);
            }
            if (dr.Table.Columns.Contains("FeeDueDate") && dr["FeeDueDate"] != DBNull.Value)
            {
                objEntity.FeeDueDate = Convert.ToDateTime(dr["FeeDueDate"]);
            }



            if (dr.Table.Columns.Contains("CreatedBy") && dr["CreatedBy"] != DBNull.Value)
            {
                objEntity.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
            }
            if (dr.Table.Columns.Contains("CreatedOn") && dr["CreatedOn"] != DBNull.Value)
            {
                objEntity.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
            }
            if (dr.Table.Columns.Contains("RevFeeDueGuid") && dr["RevFeeDueGuid"] != DBNull.Value)
            {
                objEntity.RevFeeDueGuid = Convert.ToString(dr["RevFeeDueGuid"]);
            }
            if (dr.Table.Columns.Contains("FeeName") && dr["FeeName"] != DBNull.Value)
            {
                objEntity.FeeName = Convert.ToString(dr["FeeName"]);
            }

            if (dr.Table.Columns.Contains("ApplicationName") && dr["ApplicationName"] != DBNull.Value)
            {
                objEntity.ApplicationName = Convert.ToString(dr["ApplicationName"]);
            }
            if (dr.Table.Columns.Contains("PaymentStatus") && dr["PaymentStatus"] != DBNull.Value)
            {
                objEntity.PaymentStatus = Convert.ToString(dr["PaymentStatus"]);
            }

            return objEntity;

        }
    }
}
