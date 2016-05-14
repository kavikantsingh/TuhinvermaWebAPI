using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class EmployeeTypeDAL : BaseDAL
    {
        public int Save_EmployeeType(EmployeeType objEmployeeType)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("EmployeeTypeId", objEmployeeType.EmployeeTypeId));
            lstParameter.Add(new MySqlParameter("Name", objEmployeeType.Name));
            lstParameter.Add(new MySqlParameter("SortOrder", objEmployeeType.SortOrder));

            lstParameter.Add(new MySqlParameter("IsActive", objEmployeeType.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objEmployeeType.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objEmployeeType.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objEmployeeType.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objEmployeeType.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objEmployeeType.ModifiedOn));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "employeetype_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<EmployeeType> Get_All_EmployeeType()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "EMPLOYEETYPE_GET_ALL");
            List<EmployeeType> lstEntity = new List<EmployeeType>();
            EmployeeType objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private EmployeeType FetchEntity(DataRow dr)
        {
            EmployeeType objEntity = new EmployeeType();
            if (dr.Table.Columns.Contains("EmployeeTypeId") && dr["EmployeeTypeId"] != DBNull.Value)
            {
                objEntity.EmployeeTypeId = Convert.ToInt32(dr["EmployeeTypeId"]);
            }
            if (dr.Table.Columns.Contains("Name") && dr["Name"] != DBNull.Value)
            {
                objEntity.Name = Convert.ToString(dr["Name"]);
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
