using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class MasterTransactionDAL : BaseDAL
    {
        public int Save_MasterTransaction(MasterTransaction objMasterTransaction)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("MasterTransactionId", objMasterTransaction.MasterTransactionId));
            lstParameter.Add(new MySqlParameter("MasterTransactionCode", objMasterTransaction.MasterTransactionCode));
            lstParameter.Add(new MySqlParameter("MasterTransactionName", objMasterTransaction.MasterTransactionName));
            lstParameter.Add(new MySqlParameter("MasterTransactionDesc", objMasterTransaction.MasterTransactionDesc));
            lstParameter.Add(new MySqlParameter("IsEnabled", objMasterTransaction.IsEnabled));
            lstParameter.Add(new MySqlParameter("IsReadOnly", objMasterTransaction.IsReadOnly));
            lstParameter.Add(new MySqlParameter("IsActive", objMasterTransaction.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objMasterTransaction.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objMasterTransaction.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objMasterTransaction.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objMasterTransaction.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objMasterTransaction.ModifiedOn));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "mastertransaction_Save", lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<MasterTransaction> Get_All_MasterTransaction()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "MASTERTRANSACTION_GET_ALL");
            List<MasterTransaction> lstEntity = new List<MasterTransaction>();
            MasterTransaction objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private MasterTransaction FetchEntity(DataRow dr)
        {
            MasterTransaction objEntity = new MasterTransaction();
            if (dr.Table.Columns.Contains("MasterTransactionId") && dr["MasterTransactionId"] != DBNull.Value)
            {
                objEntity.MasterTransactionId = Convert.ToInt32(dr["MasterTransactionId"]);
            }
            if (dr.Table.Columns.Contains("MasterTransactionCode") && dr["MasterTransactionCode"] != DBNull.Value)
            {
                objEntity.MasterTransactionCode = Convert.ToString(dr["MasterTransactionCode"]);
            }
            if (dr.Table.Columns.Contains("MasterTransactionName") && dr["MasterTransactionName"] != DBNull.Value)
            {
                objEntity.MasterTransactionName = Convert.ToString(dr["MasterTransactionName"]);
            }
            if (dr.Table.Columns.Contains("MasterTransactionDesc") && dr["MasterTransactionDesc"] != DBNull.Value)
            {
                objEntity.MasterTransactionDesc = Convert.ToString(dr["MasterTransactionDesc"]);
            }
            if (dr.Table.Columns.Contains("IsEnabled") && dr["IsEnabled"] != DBNull.Value)
            {
                objEntity.IsEnabled = Convert.ToBoolean(dr["IsEnabled"]);
            }
            if (dr.Table.Columns.Contains("IsReadOnly") && dr["IsReadOnly"] != DBNull.Value)
            {
                objEntity.IsReadOnly = Convert.ToBoolean(dr["IsReadOnly"]);
            }
            if (dr.Table.Columns.Contains("IsActive") && dr["IsActive"] != DBNull.Value)
            {
                objEntity.IsActive = Convert.ToBoolean(dr["IsActive"]);
            }
            if (dr.Table.Columns.Contains("IsDeleted") && dr["IsDeleted"] != DBNull.Value)
            {
                objEntity.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);
            }
            if (dr.Table.Columns.Contains("CreatedBy") && dr["CreatedBy"] != DBNull.Value)
            {
                objEntity.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
            }
            if (dr.Table.Columns.Contains("CreatedOn") && dr["CreatedOn"] != DBNull.Value)
            {
                objEntity.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
            }
            if (dr.Table.Columns.Contains("ModifiedBy") && dr["ModifiedBy"] != DBNull.Value)
            {
                objEntity.ModifiedBy = Convert.ToInt32(dr["ModifiedBy"]);
            }
            if (dr.Table.Columns.Contains("ModifiedOn") && dr["ModifiedOn"] != DBNull.Value)
            {
                objEntity.ModifiedOn = Convert.ToDateTime(dr["ModifiedOn"]);
            }
            return objEntity;

        }
    }
}
