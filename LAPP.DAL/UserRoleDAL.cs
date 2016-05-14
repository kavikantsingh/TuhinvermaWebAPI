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
    public class UserRoleDAL : BaseDAL
    {
        public int Save_UserRole(UserRole objUserRole)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("UserRoleId", objUserRole.UserRoleId));
            lstParameter.Add(new MySqlParameter("UserId", objUserRole.UserId));
            lstParameter.Add(new MySqlParameter("RoleId", objUserRole.RoleId));
            lstParameter.Add(new MySqlParameter("IsGrantable", objUserRole.IsGrantable));

            lstParameter.Add(new MySqlParameter("IsActive", objUserRole.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objUserRole.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objUserRole.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objUserRole.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objUserRole.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objUserRole.ModifiedOn));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "UserRole_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        //public int Update_UserRole(UserRole objUserRole)
        //{
        //    DBHelper objDB = new DBHelper();
        //    List<MySqlParameter> lstParameter = new List<MySqlParameter>();

        //    lstParameter.Add(new MySqlParameter("U_UserRoleId", objUserRole.UserRoleId));
        //    lstParameter.Add(new MySqlParameter("U_UserId", objUserRole.UserId));
        //    lstParameter.Add(new MySqlParameter("U_RoleId", objUserRole.RoleId));
        //    lstParameter.Add(new MySqlParameter("U_IsGrantable", objUserRole.IsGrantable));

        //    lstParameter.Add(new MySqlParameter("U_IsActive", objUserRole.IsActive));
        //    lstParameter.Add(new MySqlParameter("U_IsDeleted", objUserRole.IsDeleted));
        //    lstParameter.Add(new MySqlParameter("U_CreatedBy", objUserRole.CreatedBy));
        //    lstParameter.Add(new MySqlParameter("U_CreatedOn", objUserRole.CreatedOn));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedBy", objUserRole.ModifiedBy));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedOn", objUserRole.ModifiedOn));

        //    int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "UserRole_Update", lstParameter.ToArray());
        //    return returnValue;
        //}

        public UserRole Get_UserRole_byUserRoleId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_UserRoleId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "UserRole_Get_BY_UserRoleId", lstParameter.ToArray());
            UserRole objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public UserRole Get_UserRole_byRoleId(int RoleId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_RoleId", RoleId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "UserRole_Get_BY_RoleId", lstParameter.ToArray());
            UserRole objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<UserRole> Get_UserRole_by_UserId(int UserId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_UserId", UserId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "UserRole_Get_BY_UserId", lstParameter.ToArray());
            List<UserRole> lstEntity = new List<UserRole>();
            UserRole objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<UserRole> Get_All_UserRole()
        {

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "UserRole_Get_All");
            List<UserRole> lstEntity = new List<UserRole>();
            UserRole objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private UserRole FetchEntity(DataRow dr)
        {
            UserRole objEntity = new UserRole();

            if (dr.Table.Columns.Contains("UserRoleId") && dr["UserRoleId"] != DBNull.Value)
            {
                objEntity.UserRoleId = Convert.ToInt32(dr["UserRoleId"]);
            }

            if (dr.Table.Columns.Contains("UserId") && dr["UserId"] != DBNull.Value)
            {
                objEntity.UserId = Convert.ToInt32(dr["UserId"]);
            }

            if (dr.Table.Columns.Contains("RoleId") && dr["RoleId"] != DBNull.Value)
            {
                objEntity.RoleId = Convert.ToInt32(dr["RoleId"]);
            }
            if (dr.Table.Columns.Contains("IsGrantable") && dr["IsGrantable"] != DBNull.Value)
            {
                objEntity.IsGrantable = Convert.ToBoolean(dr["IsGrantable"]);
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
            if (dr.Table.Columns.Contains("Name") && dr["Name"] != DBNull.Value)
            {
                objEntity.RoleName = Convert.ToString(dr["Name"]);
            }
            return objEntity;
        }


    }
}
