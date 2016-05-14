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
    public class UserTypeDAL : BaseDAL
    {
        public int Save_UserType(UserType objUserType)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("UserTypeId", objUserType.UserTypeId));
            lstParameter.Add(new MySqlParameter("Code", objUserType.Code));

            lstParameter.Add(new MySqlParameter("IsActive", objUserType.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objUserType.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objUserType.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objUserType.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objUserType.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objUserType.ModifiedOn));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "UserType_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        //public int Update_UserType(UserType objUserType)
        //{
        //    DBHelper objDB = new DBHelper();
        //    List<MySqlParameter> lstParameter = new List<MySqlParameter>();

        //    lstParameter.Add(new MySqlParameter("U_UserTypeId", objUserType.UserTypeId));
        //    lstParameter.Add(new MySqlParameter("U_Code", objUserType.Code));

        //    lstParameter.Add(new MySqlParameter("U_IsActive", objUserType.IsActive));
        //    lstParameter.Add(new MySqlParameter("U_IsDeleted", objUserType.IsDeleted));
        //    lstParameter.Add(new MySqlParameter("U_CreatedBy", objUserType.CreatedBy));
        //    lstParameter.Add(new MySqlParameter("U_CreatedOn", objUserType.CreatedOn));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedBy", objUserType.ModifiedBy));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedOn", objUserType.ModifiedOn));

        //    int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "UserType_Update", lstParameter.ToArray());
        //    return returnValue;
        //}

        public UserType Get_UserType_byUserTypeId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_UserTypeId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "UserType_Get_BY_UserTypeId", lstParameter.ToArray());
            UserType objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<UserType> Get_All_UserType()
        {

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "UserType_Get_All");
            List<UserType> lstEntity = new List<UserType>();
            UserType objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private UserType FetchEntity(DataRow dr)
        {
            UserType objEntity = new UserType();

            if (dr.Table.Columns.Contains("UserTypeId") && dr["UserTypeId"] != DBNull.Value)
            {
                objEntity.UserTypeId = Convert.ToInt32(dr["UserTypeId"]);
            }
            if (dr.Table.Columns.Contains("Code") && dr["Code"] != DBNull.Value)
            {
                objEntity.Code = Convert.ToString(dr["Code"]);
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
