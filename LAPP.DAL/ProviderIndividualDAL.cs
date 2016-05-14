using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class ProviderIndividualDAL : BaseDAL
    {
        public int Save_ProviderIndividual(ProviderIndividual objProviderIndividual)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ProviderId", objProviderIndividual.ProviderId));
            lstParameter.Add(new MySqlParameter("IndividualId", objProviderIndividual.IndividualId));
            lstParameter.Add(new MySqlParameter("CreatedBy", objProviderIndividual.CreatedBy));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objProviderIndividual.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ProviderIndividualGuid", objProviderIndividual.ProviderIndividualGuid));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "PROVIDERINDIVIDUAL_SAVE", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<ProviderIndividual> Get_All_ProviderIndividual()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "PROVIDERINDIVIDUAL_GET_ALL");
            List<ProviderIndividual> lstEntity = new List<ProviderIndividual>();
            ProviderIndividual objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private ProviderIndividual FetchEntity(DataRow dr)
        {
            ProviderIndividual objEntity = new ProviderIndividual();
            if (dr.Table.Columns.Contains("ProviderId") && dr["ProviderId"] != DBNull.Value)
            {
                objEntity.ProviderId = Convert.ToInt32(dr["ProviderId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
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
            if (dr.Table.Columns.Contains("ProviderIndividualGuid") && dr["ProviderIndividualGuid"] != DBNull.Value)
            {
                objEntity.ProviderIndividualGuid = (Guid)dr["ProviderIndividualGuid"];
            }
            return objEntity;

        }
    }
}
