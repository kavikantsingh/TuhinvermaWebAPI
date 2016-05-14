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
    public class MenuUserTypeDAL : BaseDAL
    {
        public int Save_MenuUserType(MenuUserType objMenuUserType)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("MenuUserTypeId", objMenuUserType.MenuUserTypeId));
            lstParameter.Add(new MySqlParameter("MenuId", objMenuUserType.MenuId));
            lstParameter.Add(new MySqlParameter("UserTypeId", objMenuUserType.UserTypeId));
            lstParameter.Add(new MySqlParameter("BoardAuthorityId", objMenuUserType.BoardAuthorityId));


            lstParameter.Add(new MySqlParameter("IsActive", objMenuUserType.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objMenuUserType.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objMenuUserType.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objMenuUserType.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objMenuUserType.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objMenuUserType.ModifiedOn));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "MenuUserType_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        //public int Update_MenuUserType(MenuUserType objMenuUserType)
        //{
        //    DBHelper objDB = new DBHelper();
        //    List<MySqlParameter> lstParameter = new List<MySqlParameter>();

        //    lstParameter.Add(new MySqlParameter("U_MenuUserTypeId", objMenuUserType.MenuUserTypeId));
        //    lstParameter.Add(new MySqlParameter("U_MenuId", objMenuUserType.MenuId));
        //    lstParameter.Add(new MySqlParameter("U_BoardAuthorityId", objMenuUserType.BoardAuthorityId));
        //    lstParameter.Add(new MySqlParameter("U_UserTypeId", objMenuUserType.UserTypeId));

        //    lstParameter.Add(new MySqlParameter("U_IsActive", objMenuUserType.IsActive));
        //    lstParameter.Add(new MySqlParameter("U_IsDeleted", objMenuUserType.IsDeleted));
        //    lstParameter.Add(new MySqlParameter("U_CreatedBy", objMenuUserType.CreatedBy));
        //    lstParameter.Add(new MySqlParameter("U_CreatedOn", objMenuUserType.CreatedOn));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedBy", objMenuUserType.ModifiedBy));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedOn", objMenuUserType.ModifiedOn));

        //    int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "MenuUserType_Update", lstParameter.ToArray());
        //    return returnValue;
        //}

        public MenuUserType Get_MenuUserType_byMenuUserTypeId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_MenuUserTypeId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "MenuUserType_Get_BY_MenuUserTypeId", lstParameter.ToArray());
            MenuUserType objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<MenuUserType> Get_All_MenuUserType()
        {

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "MenuUserType_Get_All");
            List<MenuUserType> lstEntity = new List<MenuUserType>();
            MenuUserType objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<MenuUserType> Get_MenuUserType_by_UserTypeId(int UserTypeId)
        {

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_UserTypeId", UserTypeId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "MenuUserType_Get_BY_UserTypeId", lstParameter.ToArray());

            List<MenuUserType> lstEntity = new List<MenuUserType>();
            MenuUserType objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private MenuUserType FetchEntity(DataRow dr)
        {
            MenuUserType objEntity = new MenuUserType();

            if (dr.Table.Columns.Contains("MenuUserTypeId") && dr["MenuUserTypeId"] != DBNull.Value)
            {
                objEntity.MenuUserTypeId = Convert.ToInt32(dr["MenuUserTypeId"]);
            }

            if (dr.Table.Columns.Contains("MenuId") && dr["MenuId"] != DBNull.Value)
            {
                objEntity.MenuId = Convert.ToInt32(dr["MenuId"]);
            }
            if (dr.Table.Columns.Contains("BoardAuthorityId") && dr["BoardAuthorityId"] != DBNull.Value)
            {
                objEntity.BoardAuthorityId = Convert.ToInt32(dr["BoardAuthorityId"]);
            }
            if (dr.Table.Columns.Contains("UserTypeId") && dr["UserTypeId"] != DBNull.Value)
            {
                objEntity.UserTypeId = Convert.ToInt32(dr["UserTypeId"]);
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
