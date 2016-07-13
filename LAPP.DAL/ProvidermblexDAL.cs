using LAPP.ENTITY;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.DAL
{
    public class ProvidermblexDAL : BaseDAL
    {
        public int Save_Providermblex(ProviderMblex objProvidermblex)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("ProviderMBLExId", objProvidermblex.ProviderMBLExId));
            lstParameter.Add(new MySqlParameter("ProviderId", objProvidermblex.ProviderId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objProvidermblex.ApplicationId));
            lstParameter.Add(new MySqlParameter("MBLExName", objProvidermblex.MBLExName));
            lstParameter.Add(new MySqlParameter("PassingRates", objProvidermblex.PassingRates));
            lstParameter.Add(new MySqlParameter("passingYear", objProvidermblex.PassingYear));
            lstParameter.Add(new MySqlParameter("PassingHalf", objProvidermblex.PassingHalf));

            lstParameter.Add(new MySqlParameter("DateEntered", DateTime.Now.Date));
            lstParameter.Add(new MySqlParameter("CreatedBy", objProvidermblex.CreatedBy));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objProvidermblex.ModifiedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", DateTime.Now));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            lstParameter.Add(new MySqlParameter("IsDeleted", objProvidermblex.IsDeleted));
            lstParameter.Add(new MySqlParameter("IsActive", objProvidermblex.IsActive));
            lstParameter.Add(new MySqlParameter("ProviderMBLExIdGuid", objProvidermblex.ProviderMBLExIdGuid));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "providermblex_save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<ProvidermblexResponse> Get_All_Providermblex(ProviderMblex objProvidermblex)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ProviderId", objProvidermblex.ProviderId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "providermblex_Get_All", lstParameter.ToArray());
            List<ProvidermblexResponse> lstEntity = new List<ProvidermblexResponse>();
            ProvidermblexResponse objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private ProvidermblexResponse FetchEntity(DataRow dr)
        {
            ProvidermblexResponse objEntity = new ProvidermblexResponse();
            if (dr.Table.Columns.Contains("ProviderId") && dr["ProviderId"] != DBNull.Value)
            {
                objEntity.ProviderId = Convert.ToInt32(dr["ProviderId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationId") && dr["ApplicationId"] != DBNull.Value)
            {
                objEntity.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
            }
            if (dr.Table.Columns.Contains("MBLExName") && dr["MBLExName"] != DBNull.Value)
            {
                objEntity.MBLExName = Convert.ToString(dr["MBLExName"]);
            }
            if (dr.Table.Columns.Contains("PassingRates") && dr["PassingRates"] != DBNull.Value)
            {
                objEntity.PassingRates = Convert.ToString(dr["PassingRates"]);
            }
            if (dr.Table.Columns.Contains("PassingYear") && dr["PassingYear"] != DBNull.Value)
            {
                objEntity.PassingYear = Convert.ToInt32(dr["PassingYear"]);
            }
            if (dr.Table.Columns.Contains("DateEntered") && dr["DateEntered"] != DBNull.Value)
            {
                objEntity.DateEntered = Convert.ToDateTime(dr["DateEntered"]).Date;
            }
            if (dr.Table.Columns.Contains("PassingHalf") && dr["PassingHalf"] != DBNull.Value)
            {
                objEntity.PassingHalf = Convert.ToString(dr["PassingHalf"]);
            }
            if (dr.Table.Columns.Contains("ProviderMBLExId") && dr["ProviderMBLExId"] != DBNull.Value)
            {
                objEntity.ProviderMBLExId = Convert.ToInt32(dr["ProviderMBLExId"]);
            }
            
            return objEntity;

        }

    }
}
