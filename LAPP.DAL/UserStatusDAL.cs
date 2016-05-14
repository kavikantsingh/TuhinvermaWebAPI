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
    public class UserStatusDAL : BaseDAL
    {
        public int Save_UserStatus(UserStatus objUserStatus)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("UserStatusId", objUserStatus.UserStatusId));
            lstParameter.Add(new MySqlParameter("Name", objUserStatus.Name));
            lstParameter.Add(new MySqlParameter("SortOrder", objUserStatus.SortOrder));

            lstParameter.Add(new MySqlParameter("IsActive", objUserStatus.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objUserStatus.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objUserStatus.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objUserStatus.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objUserStatus.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objUserStatus.ModifiedOn));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "UserStatus_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        //public int Update_UserStatus(UserStatus objUserStatus)
        //{
        //    DBHelper objDB = new DBHelper();
        //    List<MySqlParameter> lstParameter = new List<MySqlParameter>();

        //    lstParameter.Add(new MySqlParameter("U_UserStatusId", objUserStatus.UserStatusId));
        //    lstParameter.Add(new MySqlParameter("U_Name", objUserStatus.Name));
        //    lstParameter.Add(new MySqlParameter("U_SortOrder", objUserStatus.SortOrder));

        //    lstParameter.Add(new MySqlParameter("U_IsActive", objUserStatus.IsActive));
        //    lstParameter.Add(new MySqlParameter("U_IsDeleted", objUserStatus.IsDeleted));
        //    lstParameter.Add(new MySqlParameter("U_CreatedBy", objUserStatus.CreatedBy));
        //    lstParameter.Add(new MySqlParameter("U_CreatedOn", objUserStatus.CreatedOn));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedBy", objUserStatus.ModifiedBy));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedOn", objUserStatus.ModifiedOn));

        //    int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "UserStatus_Update", lstParameter.ToArray());
        //    return returnValue;
        //}

        public UserStatus Get_UserStatus_byUserStatusId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_UserStatusId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "UserStatus_Get_BY_UserStatusId", lstParameter.ToArray());
            UserStatus objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<UserStatus> Get_All_UserStatus()
        {

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "UserStatus_Get_All");
            List<UserStatus> lstEntity = new List<UserStatus>();
            UserStatus objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private UserStatus FetchEntity(DataRow dr)
        {
            UserStatus objEntity = new UserStatus();

            if (dr.Table.Columns.Contains("UserStatusId") && dr["UserStatusId"] != DBNull.Value)
            {
                objEntity.UserStatusId = Convert.ToInt32(dr["UserStatusId"]);
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
