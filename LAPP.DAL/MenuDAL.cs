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
    public class MenuDAL : BaseDAL
    {
        public int Save_Menu(Menu objMenu)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("MenuId", objMenu.MenuId));
            lstParameter.Add(new MySqlParameter("ParentMenuId", objMenu.ParentMenuId));
            lstParameter.Add(new MySqlParameter("Name", objMenu.Name));
            lstParameter.Add(new MySqlParameter("Description", objMenu.Description));
            lstParameter.Add(new MySqlParameter("MenuURL", objMenu.MenuURL));
            lstParameter.Add(new MySqlParameter("MenuLevel", objMenu.MenuLevel));
            lstParameter.Add(new MySqlParameter("SortOrder", objMenu.SortOrder));
            lstParameter.Add(new MySqlParameter("IsEnabled", objMenu.IsEnabled));



            lstParameter.Add(new MySqlParameter("IsActive", objMenu.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objMenu.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objMenu.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objMenu.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objMenu.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objMenu.ModifiedOn));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "Menu_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        //public int Update_Menu(Menu objMenu)
        //{
        //    DBHelper objDB = new DBHelper();
        //    List<MySqlParameter> lstParameter = new List<MySqlParameter>();

        //    lstParameter.Add(new MySqlParameter("U_MenuId", objMenu.MenuId));
        //    lstParameter.Add(new MySqlParameter("U_ParentMenuId", objMenu.ParentMenuId));
        //    lstParameter.Add(new MySqlParameter("U_SortOrder", objMenu.SortOrder));
        //    lstParameter.Add(new MySqlParameter("U_Description", objMenu.Description));
        //    lstParameter.Add(new MySqlParameter("U_Name", objMenu.Name));
        //    lstParameter.Add(new MySqlParameter("U_MenuURL", objMenu.MenuURL));
        //    lstParameter.Add(new MySqlParameter("U_MenuLevel", objMenu.MenuLevel));
        //    lstParameter.Add(new MySqlParameter("U_IsEnabled", objMenu.IsEnabled));

        //    lstParameter.Add(new MySqlParameter("U_IsActive", objMenu.IsActive));
        //    lstParameter.Add(new MySqlParameter("U_IsDeleted", objMenu.IsDeleted));
        //    lstParameter.Add(new MySqlParameter("U_CreatedBy", objMenu.CreatedBy));
        //    lstParameter.Add(new MySqlParameter("U_CreatedOn", objMenu.CreatedOn));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedBy", objMenu.ModifiedBy));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedOn", objMenu.ModifiedOn));

        //    int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "Menu_Update", lstParameter.ToArray());
        //    return returnValue;
        //}

        public Menu Get_Menu_byMenuId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_MenuId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Menu_Get_BY_MenuId", lstParameter.ToArray());
            Menu objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<Menu> Get_All_Menu()
        {

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Menu_Get_All");
            List<Menu> lstEntity = new List<Menu>();
            Menu objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private Menu FetchEntity(DataRow dr)
        {
            Menu objEntity = new Menu();

            if (dr.Table.Columns.Contains("MenuId") && dr["MenuId"] != DBNull.Value)
            {
                objEntity.MenuId = Convert.ToInt32(dr["MenuId"]);
            }

            if (dr.Table.Columns.Contains("ParentMenuId") && dr["ParentMenuId"] != DBNull.Value)
            {
                objEntity.ParentMenuId = Convert.ToInt32(dr["ParentMenuId"]);
            }
            if (dr.Table.Columns.Contains("SortOrder") && dr["SortOrder"] != DBNull.Value)
            {
                objEntity.SortOrder = Convert.ToInt32(dr["SortOrder"]);
            }
            if (dr.Table.Columns.Contains("Description") && dr["Description"] != DBNull.Value)
            {
                objEntity.Description = Convert.ToString(dr["Description"]);
            }

            if (dr.Table.Columns.Contains("Name") && dr["Name"] != DBNull.Value)
            {
                objEntity.Name = Convert.ToString(dr["Name"]);
            }

            if (dr.Table.Columns.Contains("MenuURL") && dr["MenuURL"] != DBNull.Value)
            {
                objEntity.MenuURL = Convert.ToString(dr["MenuURL"]);
            }

            if (dr.Table.Columns.Contains("MenuLevel") && dr["MenuLevel"] != DBNull.Value)
            {
                objEntity.MenuLevel = Convert.ToInt32(dr["MenuLevel"]);
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
