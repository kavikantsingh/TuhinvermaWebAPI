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
    public class LookupTypeDAL : BaseDAL
    {
        public int Save_LookupType(LookupType objLookupType)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("LookupTypeId", objLookupType.LookupTypeId));
            lstParameter.Add(new MySqlParameter("DivisionId", objLookupType.DivisionId));
            lstParameter.Add(new MySqlParameter("DepartmentId", objLookupType.DepartmentId));
            lstParameter.Add(new MySqlParameter("StateCode", objLookupType.StateCode));
            lstParameter.Add(new MySqlParameter("Name", objLookupType.Name));
            lstParameter.Add(new MySqlParameter("IsEditable", objLookupType.IsEditable));
            lstParameter.Add(new MySqlParameter("IsEncrypted", objLookupType.IsEncrypted));

            lstParameter.Add(new MySqlParameter("IsActive", objLookupType.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objLookupType.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objLookupType.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objLookupType.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objLookupType.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objLookupType.ModifiedOn));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "LookupType_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        //public int Update_LookupType(LookupType objLookupType)
        //{
        //    DBHelper objDB = new DBHelper();
        //    List<MySqlParameter> lstParameter = new List<MySqlParameter>();

        //    lstParameter.Add(new MySqlParameter("U_LookupTypeId", objLookupType.LookupTypeId));
        //    lstParameter.Add(new MySqlParameter("U_DivisionId", objLookupType.DivisionId));
        //    lstParameter.Add(new MySqlParameter("U_DepartmentId", objLookupType.DepartmentId));
        //    lstParameter.Add(new MySqlParameter("U_StateCode", objLookupType.StateCode));
        //    lstParameter.Add(new MySqlParameter("U_Name", objLookupType.Name));
        //    lstParameter.Add(new MySqlParameter("U_IsEditable", objLookupType.IsEditable));
        //    lstParameter.Add(new MySqlParameter("U_IsEncrypted", objLookupType.IsEncrypted));

        //    lstParameter.Add(new MySqlParameter("U_IsActive", objLookupType.IsActive));
        //    lstParameter.Add(new MySqlParameter("U_IsDeleted", objLookupType.IsDeleted));
        //    lstParameter.Add(new MySqlParameter("U_CreatedBy", objLookupType.CreatedBy));
        //    lstParameter.Add(new MySqlParameter("U_CreatedOn", objLookupType.CreatedOn));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedBy", objLookupType.ModifiedBy));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedOn", objLookupType.ModifiedOn));

        //    int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "LookupType_Update", lstParameter.ToArray());
        //    return returnValue;
        //}

        public LookupType Get_LookupType_byLookupTypeId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_LookupTypeId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "lookuptype_Get_BY_LookupTypeId", lstParameter.ToArray());
            LookupType objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<LookupType> Get_All_LookupType()
        {

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "lookuptype_Get_All");
            List<LookupType> lstEntity = new List<LookupType>();
            LookupType objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private LookupType FetchEntity(DataRow dr)
        {
            LookupType objEntity = new LookupType();

            if (dr.Table.Columns.Contains("LookupTypeId") && dr["LookupTypeId"] != DBNull.Value)
            {
                objEntity.LookupTypeId = Convert.ToInt32(dr["LookupTypeId"]);
            }

            if (dr.Table.Columns.Contains("DivisionId") && dr["DivisionId"] != DBNull.Value)
            {
                objEntity.DivisionId = Convert.ToInt32(dr["DivisionId"]);
            }
            if (dr.Table.Columns.Contains("DepartmentId") && dr["DepartmentId"] != DBNull.Value)
            {
                objEntity.DepartmentId = Convert.ToInt32(dr["DepartmentId"]);
            }
            if (dr.Table.Columns.Contains("StateCode") && dr["StateCode"] != DBNull.Value)
            {
                objEntity.StateCode = Convert.ToString(dr["StateCode"]);
            }

            if (dr.Table.Columns.Contains("Name") && dr["Name"] != DBNull.Value)
            {
                objEntity.Name = Convert.ToString(dr["Name"]);
            }

            if (dr.Table.Columns.Contains("IsEditable") && dr["IsEditable"] != DBNull.Value)
            {
                objEntity.IsEditable = Convert.ToBoolean(dr["IsEditable"]);
            }

            if (dr.Table.Columns.Contains("IsEncrypted") && dr["IsEncrypted"] != DBNull.Value)
            {
                objEntity.IsEncrypted = Convert.ToBoolean(dr["IsEncrypted"]);
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
