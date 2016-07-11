using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace LAPP.DAL
{
    public class ProviderProgramDAL : BaseDAL
    {
        public int Save_ProviderProgram(ProviderProgram objProviderProgram)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ProviderProgramId", objProviderProgram.ProviderProgramId));
            lstParameter.Add(new MySqlParameter("ProviderId", objProviderProgram.ProviderId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objProviderProgram.ApplicationId));
            lstParameter.Add(new MySqlParameter("ProgramName", objProviderProgram.ProgramName));
            lstParameter.Add(new MySqlParameter("TotalNoofPgmHours", (decimal)objProviderProgram.TotalNoofPgmHours));
            lstParameter.Add(new MySqlParameter("CreatedOn", DateTime.Now));
            lstParameter.Add(new MySqlParameter("DateEntered", DateTime.Now.Date));
            lstParameter.Add(new MySqlParameter("CreatedBy", objProviderProgram.CreatedBy));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objProviderProgram.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", DateTime.Now));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            lstParameter.Add(new MySqlParameter("IsDeleted", objProviderProgram.IsDeleted));
            lstParameter.Add(new MySqlParameter("IsActive", objProviderProgram.IsActive));
            lstParameter.Add(new MySqlParameter("ProviderProgramGuid", objProviderProgram.ProviderProgramGuid));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "ProviderProgram_save_SchoolInfo", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<ProviderProgram> Get_All_ProviderProgram(ProviderProgram objProviderProgram)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ProviderId", objProviderProgram.ProviderId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ProviderProgram_Get_All", lstParameter.ToArray());
            List<ProviderProgram> lstEntity = new List<ProviderProgram>();
            ProviderProgram objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public int DeleteProviderProgram(ProviderProgram objaddress)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ProviderProgramId", objaddress.ProviderProgramId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objaddress.ApplicationId));
            lstParameter.Add(new MySqlParameter("ProviderId", objaddress.ProviderId));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);

            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "DeleteProviderProgram_SchoolInfo", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        private ProviderProgram FetchEntity(DataRow dr)
        {
            ProviderProgram objEntity = new ProviderProgram();
            if (dr.Table.Columns.Contains("ProviderId") && dr["ProviderId"] != DBNull.Value)
            {
                objEntity.ProviderId = Convert.ToInt32(dr["ProviderId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationId") && dr["ApplicationId"] != DBNull.Value)
            {
                objEntity.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
            }
            if (dr.Table.Columns.Contains("ProgramName") && dr["ProgramName"] != DBNull.Value)
            {
                objEntity.ProgramName = Convert.ToString(dr["ProgramName"]);
            }
            if (dr.Table.Columns.Contains("TotalNoofPgmHours") && dr["TotalNoofPgmHours"] != DBNull.Value)
            {
                objEntity.TotalNoofPgmHours = Convert.ToDecimal(dr["TotalNoofPgmHours"]);
            }
            if (dr.Table.Columns.Contains("ProviderProgramId") && dr["ProviderProgramId"] != DBNull.Value)
            {
                objEntity.ProviderProgramId = Convert.ToInt32(dr["ProviderProgramId"]);
            }

            if (dr.Table.Columns.Contains("IsProgramApproved") && dr["IsProgramApproved"] != DBNull.Value)
            {
                objEntity.IsProgramApproved = Convert.ToBoolean(dr["IsProgramApproved"]);
            }

            if (dr.Table.Columns.Contains("ProgramApprovalStartDate") && dr["ProgramApprovalStartDate"] != DBNull.Value)
            {
                objEntity.ProgramApprovalStartDate = Convert.ToDateTime(dr["ProgramApprovalStartDate"]);
            }

            if (dr.Table.Columns.Contains("ProgramApprovalEndDate") && dr["ProgramApprovalEndDate"] != DBNull.Value)
            {
                objEntity.ProgramApprovalEndDate = Convert.ToDateTime(dr["ProgramApprovalEndDate"]);
            }




            return objEntity;

        }

    }
}
