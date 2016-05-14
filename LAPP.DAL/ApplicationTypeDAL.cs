using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class ApplicationTypeDAL : BaseDAL
    {
        public int Save_ApplicationType(ApplicationType objApplicationType)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("ApplicationTypeId", objApplicationType.ApplicationTypeId));
            lstParameter.Add(new MySqlParameter("Name", objApplicationType.Name));
            lstParameter.Add(new MySqlParameter("Code", objApplicationType.Code));

            lstParameter.Add(new MySqlParameter("IsActive", objApplicationType.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objApplicationType.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objApplicationType.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objApplicationType.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objApplicationType.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objApplicationType.ModifiedOn));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "applicationtype_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<ApplicationType> Get_All_ApplicationType()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ADDRESSTYPE_GET_ALL");
            List<ApplicationType> lstEntity = new List<ApplicationType>();
            ApplicationType objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public ApplicationType Get_ApplicationType_byApplicationTypeId(int AddressId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParam = new List<MySqlParameter>();
            lstParam.Add(new MySqlParameter("G_ApplicationTypeId", AddressId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "addresstype_Get_By_ApplicationTypeId");

            ApplicationType objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);

            }
            return objEntity;
        }

        private ApplicationType FetchEntity(DataRow dr)
        {
            ApplicationType objEntity = new ApplicationType();


            if (dr.Table.Columns.Contains("ApplicationTypeId") && dr["ApplicationTypeId"] != DBNull.Value)
            {
                objEntity.ApplicationTypeId = Convert.ToInt32(dr["ApplicationTypeId"]);
            }
            if (dr.Table.Columns.Contains("Name") && dr["Name"] != DBNull.Value)
            {
                objEntity.Name = Convert.ToString(dr["Name"]);
            }
            if (dr.Table.Columns.Contains("Code") && dr["Code"] != DBNull.Value)
            {
                objEntity.Code = Convert.ToString(dr["Code"]);
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
