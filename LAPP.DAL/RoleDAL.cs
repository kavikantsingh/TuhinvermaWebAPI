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
    public class RoleDAL : BaseDAL
    {
        public int Save_Role(Role objRole)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("RoleId", objRole.RoleId));
            lstParameter.Add(new MySqlParameter("BoardAuthorityId", objRole.BoardAuthorityId));
            lstParameter.Add(new MySqlParameter("Name", objRole.Name));
            lstParameter.Add(new MySqlParameter("Description", objRole.Description));
            lstParameter.Add(new MySqlParameter("DivisionId", objRole.DivisionId));
            lstParameter.Add(new MySqlParameter("UserTypeId", objRole.UserTypeId));
            lstParameter.Add(new MySqlParameter("RoleGuid", objRole.RoleGuid));
            lstParameter.Add(new MySqlParameter("IsEnabled", objRole.IsEnabled));


            lstParameter.Add(new MySqlParameter("IsActive", objRole.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objRole.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objRole.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objRole.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objRole.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objRole.ModifiedOn));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "Role_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        //public int Update_Role(Role objRole)
        //{
        //    DBHelper objDB = new DBHelper();
        //    List<MySqlParameter> lstParameter = new List<MySqlParameter>();

        //    lstParameter.Add(new MySqlParameter("U_RoleId", objRole.RoleId));
        //    lstParameter.Add(new MySqlParameter("U_BoardAuthorityId", objRole.BoardAuthorityId));
        //    lstParameter.Add(new MySqlParameter("U_RoleGuid", objRole.RoleGuid));
        //    lstParameter.Add(new MySqlParameter("U_Description", objRole.Description));
        //    lstParameter.Add(new MySqlParameter("U_Name", objRole.Name));
        //    lstParameter.Add(new MySqlParameter("U_DivisionId", objRole.DivisionId));
        //    lstParameter.Add(new MySqlParameter("U_UserTypeId", objRole.UserTypeId));
        //    lstParameter.Add(new MySqlParameter("U_IsEnabled", objRole.IsEnabled));

        //    lstParameter.Add(new MySqlParameter("U_IsActive", objRole.IsActive));
        //    lstParameter.Add(new MySqlParameter("U_IsDeleted", objRole.IsDeleted));
        //    lstParameter.Add(new MySqlParameter("U_CreatedBy", objRole.CreatedBy));
        //    lstParameter.Add(new MySqlParameter("U_CreatedOn", objRole.CreatedOn));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedBy", objRole.ModifiedBy));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedOn", objRole.ModifiedOn));

        //    int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "Role_Update", lstParameter.ToArray());
        //    return returnValue;
        //}

        public Role Get_Role_byRoleId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_RoleId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Role_Get_BY_RoleId", lstParameter.ToArray());
            Role objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<Role> Get_All_Role()
        {

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Role_Get_All");
            List<Role> lstEntity = new List<Role>();
            Role objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<Role> Get_Role_by_BoardAuthorityId(int BoardAuthorityId)
        {

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_BoardAuthorityId", BoardAuthorityId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Role_Get_BY_BoardAuthorityId", lstParameter.ToArray());

            List<Role> lstEntity = new List<Role>();
            Role objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<Role> Get_Role_by_UserTypeId(int UserTypeId)
        {

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_UserTypeId", UserTypeId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Role_Get_BY_UserTypeId", lstParameter.ToArray());

            List<Role> lstEntity = new List<Role>();
            Role objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }
        private Role FetchEntity(DataRow dr)
        {
            Role objEntity = new Role();

            if (dr.Table.Columns.Contains("RoleId") && dr["RoleId"] != DBNull.Value)
            {
                objEntity.RoleId = Convert.ToInt32(dr["RoleId"]);
            }

            if (dr.Table.Columns.Contains("BoardAuthorityId") && dr["BoardAuthorityId"] != DBNull.Value)
            {
                objEntity.BoardAuthorityId = Convert.ToInt32(dr["BoardAuthorityId"]);
            }
            if (dr.Table.Columns.Contains("RoleGuid") && dr["RoleGuid"] != DBNull.Value)
            {
                objEntity.RoleGuid = Convert.ToString(dr["RoleGuid"]);
            }
            if (dr.Table.Columns.Contains("Description") && dr["Description"] != DBNull.Value)
            {
                objEntity.Description = Convert.ToString(dr["Description"]);
            }

            if (dr.Table.Columns.Contains("Name") && dr["Name"] != DBNull.Value)
            {
                objEntity.Name = Convert.ToString(dr["Name"]);
            }

            if (dr.Table.Columns.Contains("DivisionId") && dr["DivisionId"] != DBNull.Value)
            {
                objEntity.DivisionId = Convert.ToInt32(dr["DivisionId"]);
            }

            if (dr.Table.Columns.Contains("UserTypeId") && dr["UserTypeId"] != DBNull.Value)
            {
                objEntity.UserTypeId = Convert.ToInt32(dr["UserTypeId"]);
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


            // Joined  Field For View Only

            if (dr.Table.Columns.Contains("DivisionName") && dr["DivisionName"] != DBNull.Value)
            {
                objEntity.DivisionName = Convert.ToString(dr["DivisionName"]);
            }
            if (dr.Table.Columns.Contains("BoardAuthorityName") && dr["BoardAuthorityName"] != DBNull.Value)
            {
                objEntity.BoardAuthorityName = Convert.ToString(dr["BoardAuthorityName"]);
            }
            if (dr.Table.Columns.Contains("UserTypeName") && dr["UserTypeName"] != DBNull.Value)
            {
                objEntity.UserTypeName = Convert.ToString(dr["UserTypeName"]);
            }


            return objEntity;
        }

    }
}
