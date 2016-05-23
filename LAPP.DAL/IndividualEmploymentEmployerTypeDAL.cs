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
    public class IndividualEmploymentEmployerTypeDAL : BaseDAL
    {
        public int Save_IndividualEmploymentEmployerType(IndividualEmploymentEmployerType objIndividualEmploymentEmployerType)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualEmploymentEmployerTypeId", objIndividualEmploymentEmployerType.IndividualEmploymentEmployerTypeId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualEmploymentEmployerType.IndividualId));
            lstParameter.Add(new MySqlParameter("IndividualEmploymentId", objIndividualEmploymentEmployerType.IndividualEmploymentId));
            lstParameter.Add(new MySqlParameter("EmployerTypeId", objIndividualEmploymentEmployerType.EmployerTypeId));
            lstParameter.Add(new MySqlParameter("EmployerTypeValue", objIndividualEmploymentEmployerType.EmployerTypeValue));
            lstParameter.Add(new MySqlParameter("IsActive", objIndividualEmploymentEmployerType.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualEmploymentEmployerType.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualEmploymentEmployerType.CreatedBy));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objIndividualEmploymentEmployerType.ModifiedBy));
            lstParameter.Add(new MySqlParameter("IndividualEmploymentEmployerTypeGuid", objIndividualEmploymentEmployerType.IndividualEmploymentEmployerTypeGuid));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "individualemploymentemployertype_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<IndividualEmploymentEmployerType> Get_All_IndividualEmploymentEmployerType()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualemploymentemployertype_Get_All");
            List<IndividualEmploymentEmployerType> lstEntity = new List<IndividualEmploymentEmployerType>();
            IndividualEmploymentEmployerType objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public IndividualEmploymentEmployerType Get_IndividualEmploymentEmployerType_By_IndividualEmploymentEmployerTypeId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualEmploymentEmployerTypeId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualemploymentemployertype_Get_By_IndiEmploymentEmpTyId", lstParameter.ToArray());
            IndividualEmploymentEmployerType objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private IndividualEmploymentEmployerType FetchEntity(DataRow dr)
        {
            IndividualEmploymentEmployerType objEntity = new IndividualEmploymentEmployerType();
            if (dr.Table.Columns.Contains("IndividualEmploymentEmployerTypeId") && dr["IndividualEmploymentEmployerTypeId"] != DBNull.Value)
            {
                objEntity.IndividualEmploymentEmployerTypeId = Convert.ToInt32(dr["IndividualEmploymentEmployerTypeId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("IndividualEmploymentId") && dr["IndividualEmploymentId"] != DBNull.Value)
            {
                objEntity.IndividualEmploymentId = Convert.ToInt32(dr["IndividualEmploymentId"]);
            }
            if (dr.Table.Columns.Contains("EmployerTypeId") && dr["EmployerTypeId"] != DBNull.Value)
            {
                objEntity.EmployerTypeId = Convert.ToInt32(dr["EmployerTypeId"]);
            }
            if (dr.Table.Columns.Contains("EmployerTypeValue") && dr["EmployerTypeValue"] != DBNull.Value)
            {
                objEntity.EmployerTypeValue = Convert.ToBoolean(dr["EmployerTypeValue"]);
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
            if (dr.Table.Columns.Contains("IndividualEmploymentEmployerTypeGuid") && dr["IndividualEmploymentEmployerTypeGuid"] != DBNull.Value)
            {
                objEntity.IndividualEmploymentEmployerTypeGuid = (Guid)dr["IndividualEmploymentEmployerTypeGuid"];
            }
            return objEntity;

        }
    }
}
