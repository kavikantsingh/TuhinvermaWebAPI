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
    public class RoleMenuDAL : BaseDAL
    {
        public int Save_RoleMenu(RoleMenu objRoleMenu)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("RoleMenuId", objRoleMenu.RoleMenuId));
            lstParameter.Add(new MySqlParameter("RoleId", objRoleMenu.RoleId));
            lstParameter.Add(new MySqlParameter("MenuId", objRoleMenu.MenuId));
            lstParameter.Add(new MySqlParameter("Create", objRoleMenu.Create));
            lstParameter.Add(new MySqlParameter("Update", objRoleMenu.Update));
            lstParameter.Add(new MySqlParameter("Delete", objRoleMenu.Delete));
            lstParameter.Add(new MySqlParameter("Read", objRoleMenu.Read));

            lstParameter.Add(new MySqlParameter("IsActive", objRoleMenu.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objRoleMenu.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objRoleMenu.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objRoleMenu.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objRoleMenu.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objRoleMenu.ModifiedOn));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "RoleMenu_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        //public int Update_RoleMenu(RoleMenu objRoleMenu)
        //{
        //    DBHelper objDB = new DBHelper();
        //    List<MySqlParameter> lstParameter = new List<MySqlParameter>();

        //    lstParameter.Add(new MySqlParameter("U_RoleMenuId", objRoleMenu.RoleMenuId));
        //    lstParameter.Add(new MySqlParameter("U_RoleId", objRoleMenu.RoleId));
        //    lstParameter.Add(new MySqlParameter("U_MenuId", objRoleMenu.MenuId));

        //    lstParameter.Add(new MySqlParameter("U_Create", objRoleMenu.Create));
        //    lstParameter.Add(new MySqlParameter("U_Update", objRoleMenu.Update));
        //    lstParameter.Add(new MySqlParameter("U_Delete", objRoleMenu.Delete));
        //    lstParameter.Add(new MySqlParameter("U_Read", objRoleMenu.Read));

        //    lstParameter.Add(new MySqlParameter("U_IsActive", objRoleMenu.IsActive));
        //    lstParameter.Add(new MySqlParameter("U_IsDeleted", objRoleMenu.IsDeleted));
        //    lstParameter.Add(new MySqlParameter("U_CreatedBy", objRoleMenu.CreatedBy));
        //    lstParameter.Add(new MySqlParameter("U_CreatedOn", objRoleMenu.CreatedOn));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedBy", objRoleMenu.ModifiedBy));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedOn", objRoleMenu.ModifiedOn));

        //    int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "RoleMenu_Update", lstParameter.ToArray());
        //    return returnValue;
        //}

        public List<RoleMenu> Get_RoleMenu_by_RoleId(int RoleId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_RoleId", RoleId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "RoleMenu_Get_BY_RoleId", lstParameter.ToArray());

            List<RoleMenu> lstEntity = new List<RoleMenu>();
            RoleMenu objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public RoleMenu Get_RoleMenu_byRoleMenuId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_RoleMenuId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "RoleMenu_Get_BY_RoleMenuId", lstParameter.ToArray());
            RoleMenu objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<RoleMenu> Get_All_RoleMenu()
        {

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "RoleMenu_Get_All");
            List<RoleMenu> lstEntity = new List<RoleMenu>();
            RoleMenu objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private RoleMenu FetchEntity(DataRow dr)
        {
            RoleMenu objEntity = new RoleMenu();

            if (dr.Table.Columns.Contains("RoleMenuId") && dr["RoleMenuId"] != DBNull.Value)
            {
                objEntity.RoleMenuId = Convert.ToInt32(dr["RoleMenuId"]);
            }

          

            if (dr.Table.Columns.Contains("RoleId") && dr["RoleId"] != DBNull.Value)
            {
                objEntity.RoleId = Convert.ToInt32(dr["RoleId"]);
            }

            if (dr.Table.Columns.Contains("MenuId") && dr["MenuId"] != DBNull.Value)
            {
                objEntity.MenuId = Convert.ToInt32(dr["MenuId"]);
            }


            if (dr.Table.Columns.Contains("Create") && dr["Create"] != DBNull.Value)
            {
                objEntity.Create = Convert.ToBoolean(dr["Create"]);
            }
            if (dr.Table.Columns.Contains("Update") && dr["Update"] != DBNull.Value)
            {
                objEntity.Update = Convert.ToBoolean(dr["Update"]);
            }
            if (dr.Table.Columns.Contains("Delete") && dr["Delete"] != DBNull.Value)
            {
                objEntity.Delete = Convert.ToBoolean(dr["Delete"]);
            }
            if (dr.Table.Columns.Contains("Read") && dr["Read"] != DBNull.Value)
            {
                objEntity.Read = Convert.ToBoolean(dr["Read"]);
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


            // Joined  Field For View Only

            if (dr.Table.Columns.Contains("MenuName") && dr["MenuName"] != DBNull.Value)
            {
                objEntity.MenuName = Convert.ToString(dr["MenuName"]);
            }
            if (dr.Table.Columns.Contains("RoleName") && dr["RoleName"] != DBNull.Value)
            {
                objEntity.RoleName = Convert.ToString(dr["RoleName"]);
            }
            if (dr.Table.Columns.Contains("ParentMenuId") && dr["ParentMenuId"] != DBNull.Value)
            {
                objEntity.ParentMenuId = Convert.ToInt32(dr["ParentMenuId"]);
            }

            if (dr.Table.Columns.Contains("ParentMenuId") && dr["ParentMenuId"] != DBNull.Value)
            {
                objEntity.ParentMenuId = Convert.ToInt32(dr["ParentMenuId"]);
            }

            if (dr.Table.Columns.Contains("ParentMenuId") && dr["ParentMenuId"] != DBNull.Value)
            {
                objEntity.ParentMenuId = Convert.ToInt32(dr["ParentMenuId"]);
            }

            if (dr.Table.Columns.Contains("MenuSortOrder") && dr["MenuSortOrder"] != DBNull.Value)
            {
                objEntity.MenuSortOrder = Convert.ToInt32(dr["MenuSortOrder"]);
            }
            if (dr.Table.Columns.Contains("MenuLevel") && dr["MenuLevel"] != DBNull.Value)
            {
                objEntity.MenuLevel= Convert.ToInt32(dr["MenuLevel"]);
            }

            if (dr.Table.Columns.Contains("MenuDescription") && dr["MenuDescription"] != DBNull.Value)
            {
                objEntity.MenuDescription = Convert.ToString(dr["MenuDescription"]);
            }

            if (dr.Table.Columns.Contains("MenuURL") && dr["MenuURL"] != DBNull.Value)
            {
                objEntity.MenuURL = Convert.ToString(dr["MenuURL"]);
            }
            return objEntity;
        }


    }
}
