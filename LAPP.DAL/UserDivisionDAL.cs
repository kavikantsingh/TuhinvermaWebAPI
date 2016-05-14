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
    public class UserDivisionDAL : BaseDAL
    {
        public int Save_UserDivision(UserDivision objUserDivision)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("UserDivisionId", objUserDivision.UserDivisionId));
            lstParameter.Add(new MySqlParameter("UserId", objUserDivision.UserId));
            lstParameter.Add(new MySqlParameter("DivisionId", objUserDivision.DivisionId));

            lstParameter.Add(new MySqlParameter("IsActive", objUserDivision.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objUserDivision.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objUserDivision.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objUserDivision.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objUserDivision.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objUserDivision.ModifiedOn));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "UserDivision_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        //public int Update_UserDivision(UserDivision objUserDivision)
        //{
        //    DBHelper objDB = new DBHelper();
        //    List<MySqlParameter> lstParameter = new List<MySqlParameter>();

        //    lstParameter.Add(new MySqlParameter("U_UserDivisionId", objUserDivision.UserDivisionId));
        //    lstParameter.Add(new MySqlParameter("U_UserId", objUserDivision.UserId));
        //    lstParameter.Add(new MySqlParameter("U_DivisionId", objUserDivision.DivisionId));

        //    lstParameter.Add(new MySqlParameter("U_IsActive", objUserDivision.IsActive));
        //    lstParameter.Add(new MySqlParameter("U_IsDeleted", objUserDivision.IsDeleted));
        //    lstParameter.Add(new MySqlParameter("U_CreatedBy", objUserDivision.CreatedBy));
        //    lstParameter.Add(new MySqlParameter("U_CreatedOn", objUserDivision.CreatedOn));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedBy", objUserDivision.ModifiedBy));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedOn", objUserDivision.ModifiedOn));

        //    int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "UserDivision_Update", lstParameter.ToArray());
        //    return returnValue;
        //}

        public UserDivision Get_UserDivision_byUserDivisionId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_UserDivisionId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "UserDivision_Get_BY_UserDivisionId", lstParameter.ToArray());
            UserDivision objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public UserDivision Get_UserDivision_by_UserId(int UserId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_UserId", UserId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "UserDivision_Get_BY_UserId", lstParameter.ToArray());
            UserDivision objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public UserDivision Get_UserDivision_by_DivisionId(int DivisionId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_DivisionId", DivisionId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "UserDivision_Get_BY_DivisionId", lstParameter.ToArray());
            UserDivision objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }


        public List<UserDivision> Get_All_UserDivision()
        {

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "UserDivision_Get_All");
            List<UserDivision> lstEntity = new List<UserDivision>();
            UserDivision objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private UserDivision FetchEntity(DataRow dr)
        {
            UserDivision objEntity = new UserDivision();

            if (dr.Table.Columns.Contains("UserDivisionId") && dr["UserDivisionId"] != DBNull.Value)
            {
                objEntity.UserDivisionId = Convert.ToInt32(dr["UserDivisionId"]);
            }

            if (dr.Table.Columns.Contains("UserId") && dr["UserId"] != DBNull.Value)
            {
                objEntity.UserId = Convert.ToInt32(dr["UserId"]);
            }

            if (dr.Table.Columns.Contains("DivisionId") && dr["DivisionId"] != DBNull.Value)
            {
                objEntity.DivisionId = Convert.ToInt32(dr["DivisionId"]);
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
