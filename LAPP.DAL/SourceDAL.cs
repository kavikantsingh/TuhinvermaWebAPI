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
    public class SourceDAL : BaseDAL
    {
        public int Save_Source(Source objSource)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("SourceId", objSource.SourceId));
            lstParameter.Add(new MySqlParameter("Name", objSource.Name));

            lstParameter.Add(new MySqlParameter("IsActive", objSource.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objSource.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objSource.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objSource.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objSource.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objSource.ModifiedOn));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "Source_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        //public int Update_Source(Source objSource)
        //{
        //    DBHelper objDB = new DBHelper();
        //    List<MySqlParameter> lstParameter = new List<MySqlParameter>();

        //    lstParameter.Add(new MySqlParameter("U_SourceId", objSource.SourceId));
        //    lstParameter.Add(new MySqlParameter("U_Name", objSource.Name));

        //    lstParameter.Add(new MySqlParameter("U_IsActive", objSource.IsActive));
        //    lstParameter.Add(new MySqlParameter("U_IsDeleted", objSource.IsDeleted));
        //    lstParameter.Add(new MySqlParameter("U_CreatedBy", objSource.CreatedBy));
        //    lstParameter.Add(new MySqlParameter("U_CreatedOn", objSource.CreatedOn));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedBy", objSource.ModifiedBy));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedOn", objSource.ModifiedOn));

        //    int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "Source_Update", lstParameter.ToArray());
        //    return returnValue;
        //}

        public Source Get_Source_bySourceId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_SourceId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Source_Get_BY_SourceId", lstParameter.ToArray());
            Source objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<Source> Get_All_Source()
        {

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Source_Get_All");
            List<Source> lstEntity = new List<Source>();
            Source objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private Source FetchEntity(DataRow dr)
        {
            Source objEntity = new Source();

            if (dr.Table.Columns.Contains("SourceId") && dr["SourceId"] != DBNull.Value)
            {
                objEntity.SourceId = Convert.ToInt32(dr["SourceId"]);
            }
            if (dr.Table.Columns.Contains("Name") && dr["Name"] != DBNull.Value)
            {
                objEntity.Name = Convert.ToString(dr["Name"]);
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
