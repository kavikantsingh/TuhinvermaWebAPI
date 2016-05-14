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
    public class UserBoardAuthorityDAL : BaseDAL
    {
        public int Save_UserBoardAuthority(UserBoardAuthority objUserBoardAuthority)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("UserBoardAuthorityId", objUserBoardAuthority.UserBoardAuthorityId));
            lstParameter.Add(new MySqlParameter("UserId", objUserBoardAuthority.UserId));
            lstParameter.Add(new MySqlParameter("BoardAuthorityId", objUserBoardAuthority.BoardAuthorityId));

            lstParameter.Add(new MySqlParameter("IsActive", objUserBoardAuthority.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objUserBoardAuthority.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objUserBoardAuthority.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objUserBoardAuthority.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objUserBoardAuthority.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objUserBoardAuthority.ModifiedOn));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "UserBoardAuthority_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        //public int Update_UserBoardAuthority(UserBoardAuthority objUserBoardAuthority)
        //{
        //    DBHelper objDB = new DBHelper();
        //    List<MySqlParameter> lstParameter = new List<MySqlParameter>();

        //    lstParameter.Add(new MySqlParameter("U_UserBoardAuthorityId", objUserBoardAuthority.UserBoardAuthorityId));
        //    lstParameter.Add(new MySqlParameter("U_UserId", objUserBoardAuthority.UserId));
        //    lstParameter.Add(new MySqlParameter("U_BoardAuthorityId", objUserBoardAuthority.BoardAuthorityId));

        //    lstParameter.Add(new MySqlParameter("U_IsActive", objUserBoardAuthority.IsActive));
        //    lstParameter.Add(new MySqlParameter("U_IsDeleted", objUserBoardAuthority.IsDeleted));
        //    lstParameter.Add(new MySqlParameter("U_CreatedBy", objUserBoardAuthority.CreatedBy));
        //    lstParameter.Add(new MySqlParameter("U_CreatedOn", objUserBoardAuthority.CreatedOn));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedBy", objUserBoardAuthority.ModifiedBy));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedOn", objUserBoardAuthority.ModifiedOn));

        //    int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "UserBoardAuthority_Update", lstParameter.ToArray());
        //    return returnValue;
        //}

        public UserBoardAuthority Get_UserBoardAuthority_byUserBoardAuthorityId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_UserBoardAuthorityId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "UserBoardAuthority_Get_BY_UserBoardAuthorityId", lstParameter.ToArray());
            UserBoardAuthority objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public UserBoardAuthority Get_UserBoardAuthority_by_UserId(int UserId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_UserId", UserId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "UserBoardAuthority_Get_BY_UserId", lstParameter.ToArray());
            UserBoardAuthority objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public UserBoardAuthority Get_UserBoardAuthority_by_BoardAuthorityId(int BoardAuthorityId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_BoardAuthorityId", BoardAuthorityId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "UserBoardAuthority_Get_BY_BoardAuthorityId", lstParameter.ToArray());
            UserBoardAuthority objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<UserBoardAuthority> Get_All_UserBoardAuthority()
        {

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "UserBoardAuthority_Get_All");
            List<UserBoardAuthority> lstEntity = new List<UserBoardAuthority>();
            UserBoardAuthority objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private UserBoardAuthority FetchEntity(DataRow dr)
        {
            UserBoardAuthority objEntity = new UserBoardAuthority();

            if (dr.Table.Columns.Contains("UserBoardAuthorityId") && dr["UserBoardAuthorityId"] != DBNull.Value)
            {
                objEntity.UserBoardAuthorityId = Convert.ToInt32(dr["UserBoardAuthorityId"]);
            }

            if (dr.Table.Columns.Contains("UserId") && dr["UserId"] != DBNull.Value)
            {
                objEntity.UserId = Convert.ToInt32(dr["UserId"]);
            }

            if (dr.Table.Columns.Contains("BoardAuthorityId") && dr["BoardAuthorityId"] != DBNull.Value)
            {
                objEntity.BoardAuthorityId = Convert.ToInt32(dr["BoardAuthorityId"]);
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
