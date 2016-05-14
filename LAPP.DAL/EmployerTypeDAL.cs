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
    public class EmployerTypeDAL : BaseDAL
    {
        public int Save_EmployerType(EmployerType objEmployerType)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("EmployerTypeId", objEmployerType.EmployerTypeId));
            lstParameter.Add(new MySqlParameter("EmployerTypeCode", objEmployerType.EmployerTypeCode));
            lstParameter.Add(new MySqlParameter("EmployerTypeName", objEmployerType.EmployerTypeName));
            lstParameter.Add(new MySqlParameter("EffectiveDate", objEmployerType.EffectiveDate));
            lstParameter.Add(new MySqlParameter("EndDate", objEmployerType.EndDate));
            lstParameter.Add(new MySqlParameter("SortOrder", objEmployerType.SortOrder));
            lstParameter.Add(new MySqlParameter("IsActive", objEmployerType.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objEmployerType.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objEmployerType.CreatedBy));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objEmployerType.ModifiedBy));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "EMPLOYERTYPE_SAVE",true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<EmployerType> Get_All_EmployerType()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "EMPLOYERTYPE_GET_ALL");
            List<EmployerType> lstEntity = new List<EmployerType>();
            EmployerType objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private EmployerType FetchEntity(DataRow dr)
        {
            EmployerType objEntity = new EmployerType();
            if (dr.Table.Columns.Contains("EmployerTypeId") && dr["EmployerTypeId"] != DBNull.Value)
            {
                objEntity.EmployerTypeId = Convert.ToInt32(dr["EmployerTypeId"]);
            }
            if (dr.Table.Columns.Contains("EmployerTypeCode") && dr["EmployerTypeCode"] != DBNull.Value)
            {
                objEntity.EmployerTypeCode = Convert.ToString(dr["EmployerTypeCode"]);
            }
            if (dr.Table.Columns.Contains("EmployerTypeName") && dr["EmployerTypeName"] != DBNull.Value)
            {
                objEntity.EmployerTypeName = Convert.ToString(dr["EmployerTypeName"]);
            }
            if (dr.Table.Columns.Contains("EffectiveDate") && dr["EffectiveDate"] != DBNull.Value)
            {
                objEntity.EffectiveDate = Convert.ToDateTime(dr["EffectiveDate"]);
            }
            if (dr.Table.Columns.Contains("EndDate") && dr["EndDate"] != DBNull.Value)
            {
                objEntity.EndDate = Convert.ToDateTime(dr["EndDate"]);
            }
            if (dr.Table.Columns.Contains("SortOrder") && dr["SortOrder"] != DBNull.Value)
            {
                objEntity.SortOrder = Convert.ToInt32(dr["SortOrder"]);
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
