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

    public class ProviderApprovalAgencyDAL : BaseDAL
    {
        public int Save_ProviderApprovalAgency(ProviderApprovalAgency objProviderApprovalAgency)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ProviderApprovalAgencyId", objProviderApprovalAgency.ProviderApprovalAgencyId));
            lstParameter.Add(new MySqlParameter("ProviderId", objProviderApprovalAgency.ProviderId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objProviderApprovalAgency.ApplicationId));
            lstParameter.Add(new MySqlParameter("ApprovalAccreditingAgencyId", objProviderApprovalAgency.ApprovalAccreditingAgencyId));
            
            lstParameter.Add(new MySqlParameter("ApprovalAccreditingAgencyName", objProviderApprovalAgency.ApprovalAccreditingAgencyName));
            lstParameter.Add(new MySqlParameter("AgencySchoolCode", objProviderApprovalAgency.AgencySchoolCode));
            lstParameter.Add(new MySqlParameter("IsAdditional", objProviderApprovalAgency.IsAdditional));
            lstParameter.Add(new MySqlParameter("ExpirationDate", objProviderApprovalAgency.ExpirationDate));
            //lstParameter.Add(new MySqlParameter("ExpirationDate", DateTime.Now.Date));
            lstParameter.Add(new MySqlParameter("CreatedBy", objProviderApprovalAgency.CreatedBy));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objProviderApprovalAgency.ModifiedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", DateTime.Now));
            lstParameter.Add(new MySqlParameter("ModifiedOn", DateTime.Now));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            lstParameter.Add(new MySqlParameter("IsDeleted", objProviderApprovalAgency.IsDeleted));
            lstParameter.Add(new MySqlParameter("IsActive", objProviderApprovalAgency.IsActive));
            lstParameter.Add(new MySqlParameter("ProviderApprovalAgencyGuid", objProviderApprovalAgency.ProviderApprovalAgencyGuid));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "ProviderApprovalAgency_save_SchoolInfo", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<ProviderApprovalAgency> Get_All_ProviderApprovalAgency(ProviderApprovalAgency objProviderApprovalAgency)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ProviderId", objProviderApprovalAgency.ProviderId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ProviderApprovalAgency_Get_All_SchoolInfo", lstParameter.ToArray());
            List<ProviderApprovalAgency> lstEntity = new List<ProviderApprovalAgency>();
            ProviderApprovalAgency objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public int DeleteProviderApprovalAgency(ProviderApprovalAgency objProviderApprovalAgency)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ProviderApprovalAgencyId", objProviderApprovalAgency.ProviderApprovalAgencyId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objProviderApprovalAgency.ApplicationId));
            lstParameter.Add(new MySqlParameter("ProviderId", objProviderApprovalAgency.ProviderId));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);

            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "DeleteProviderApprovalAgency_SchoolInfo", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        private ProviderApprovalAgency FetchEntity(DataRow dr)
        {
            ProviderApprovalAgency objEntity = new ProviderApprovalAgency();
            if (dr.Table.Columns.Contains("ProviderId") && dr["ProviderId"] != DBNull.Value)
            {
                objEntity.ProviderId = Convert.ToInt32(dr["ProviderId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationId") && dr["ApplicationId"] != DBNull.Value)
            {
                objEntity.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
            }
            if (dr.Table.Columns.Contains("ApprovalAccreditingAgencyName") && dr["ApprovalAccreditingAgencyName"] != DBNull.Value)
            {
                objEntity.ApprovalAccreditingAgencyName = Convert.ToString(dr["ApprovalAccreditingAgencyName"]);
            }
            if (dr.Table.Columns.Contains("AgencySchoolCode") && dr["AgencySchoolCode"] != DBNull.Value)
            {
                objEntity.AgencySchoolCode = Convert.ToString(dr["AgencySchoolCode"]);
            }
            if (dr.Table.Columns.Contains("ProviderApprovalAgencyId") && dr["ProviderApprovalAgencyId"] != DBNull.Value)
            {
                objEntity.ProviderApprovalAgencyId = Convert.ToInt32(dr["ProviderApprovalAgencyId"]);
            }
            if (dr.Table.Columns.Contains("ApprovalAccreditingAgencyId") && dr["ApprovalAccreditingAgencyId"] != DBNull.Value)
            {
                objEntity.ApprovalAccreditingAgencyId = Convert.ToInt32(dr["ApprovalAccreditingAgencyId"]);
            }
            
            if (dr.Table.Columns.Contains("IsAdditional") && dr["IsAdditional"] != DBNull.Value)
            {
                objEntity.IsAdditional = Convert.ToBoolean(dr["IsAdditional"]);
            }


            return objEntity;

        }

    }
}
