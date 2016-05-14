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
    public class LookupDAL : BaseDAL
    {
        public int Save_Lookup(Lookup objLookup)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("LookupId", objLookup.LookupId));
            lstParameter.Add(new MySqlParameter("LookupTypeId", objLookup.LookupTypeId));
            lstParameter.Add(new MySqlParameter("LookupCode", objLookup.LookupCode));
            lstParameter.Add(new MySqlParameter("LookupDesc", objLookup.LookupDesc));
            lstParameter.Add(new MySqlParameter("SortOrder", objLookup.SortOrder));
            lstParameter.Add(new MySqlParameter("IsEnabled", objLookup.IsEnabled));

            lstParameter.Add(new MySqlParameter("IsActive", objLookup.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objLookup.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objLookup.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objLookup.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objLookup.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objLookup.ModifiedOn));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "Lookup_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        //public int Update_Lookup(Lookup objLookup)
        //{
        //    DBHelper objDB = new DBHelper();
        //    List<MySqlParameter> lstParameter = new List<MySqlParameter>();

        //    lstParameter.Add(new MySqlParameter("U_LookupId", objLookup.LookupId));
        //    lstParameter.Add(new MySqlParameter("U_LookupTypeId", objLookup.LookupTypeId));
        //    lstParameter.Add(new MySqlParameter("U_LookupCode", objLookup.LookupCode));
        //    lstParameter.Add(new MySqlParameter("U_LookupDesc", objLookup.LookupDesc));
        //    lstParameter.Add(new MySqlParameter("U_SortOrder", objLookup.SortOrder));
        //    lstParameter.Add(new MySqlParameter("U_IsEnabled", objLookup.IsEnabled));

        //    lstParameter.Add(new MySqlParameter("U_IsActive", objLookup.IsActive));
        //    lstParameter.Add(new MySqlParameter("U_IsDeleted", objLookup.IsDeleted));
        //    lstParameter.Add(new MySqlParameter("U_CreatedBy", objLookup.CreatedBy));
        //    lstParameter.Add(new MySqlParameter("U_CreatedOn", objLookup.CreatedOn));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedBy", objLookup.ModifiedBy));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedOn", objLookup.ModifiedOn));

        //    int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "Lookup_Update", lstParameter.ToArray());
        //    return returnValue;
        //}

        public Lookup Get_Lookup_byLookupId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_LookupId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Lookup_Get_BY_LookupId", lstParameter.ToArray());
            Lookup objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public Lookup Get_Lookup_LookupTypeId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_LookupTypeId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Lookup_Get_BY_LookupTypeId", lstParameter.ToArray());
            Lookup objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<Lookup> Get_All_Lookup_LookupTypeId(int ID)
        {

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_LookupTypeId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Lookup_Get_BY_LookupTypeId", lstParameter.ToArray());
            List<Lookup> lstEntity = new List<Lookup>();
            Lookup objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<Lookup> Get_All_Lookup()
        {

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Lookup_Get_All");
            List<Lookup> lstEntity = new List<Lookup>();
            Lookup objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private Lookup FetchEntity(DataRow dr)
        {
            Lookup objEntity = new Lookup();

            if (dr.Table.Columns.Contains("LookupId") && dr["LookupId"] != DBNull.Value)
            {
                objEntity.LookupId = Convert.ToInt32(dr["LookupId"]);
            }

            if (dr.Table.Columns.Contains("LookupTypeId") && dr["LookupTypeId"] != DBNull.Value)
            {
                objEntity.LookupTypeId = Convert.ToInt32(dr["LookupTypeId"]);
            }

            if (dr.Table.Columns.Contains("LookupCode") && dr["LookupCode"] != DBNull.Value)
            {
                objEntity.LookupCode = Convert.ToString(dr["LookupCode"]);
            }

            if (dr.Table.Columns.Contains("LookupDesc") && dr["LookupDesc"] != DBNull.Value)
            {
                objEntity.LookupDesc = Convert.ToString(dr["LookupDesc"]);
            }

            if (dr.Table.Columns.Contains("SortOrder") && dr["SortOrder"] != DBNull.Value)
            {
                objEntity.SortOrder = Convert.ToInt32(dr["SortOrder"]);
            }

            if (dr.Table.Columns.Contains("IsEnabled") && dr["IsEnabled"] != DBNull.Value)
            {
                objEntity.IsEnabled = Convert.ToBoolean(dr["IsEnabled"]);
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
