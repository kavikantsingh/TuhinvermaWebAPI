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
    public class StateDAL : BaseDAL
    {
        public int Save_State(State objState)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();


            lstParameter.Add(new MySqlParameter("Name", objState.Name));
            lstParameter.Add(new MySqlParameter("StateId", objState.StateId));
            lstParameter.Add(new MySqlParameter("CountryId", objState.CountryId));

            lstParameter.Add(new MySqlParameter("CreatedBy", objState.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objState.CreatedOn));
            lstParameter.Add(new MySqlParameter("IsActive", objState.IsActive));
            lstParameter.Add(new MySqlParameter("IsDelete", objState.IsDelete));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objState.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objState.ModifiedOn));

            lstParameter.Add(new MySqlParameter("StateCode", objState.StateCode));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "State_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        //public int Update_State(State objState)
        //{
        //    DBHelper objDB = new DBHelper();
        //    List<MySqlParameter> lstParameter = new List<MySqlParameter>();

        //    lstParameter.Add(new MySqlParameter("U_StateId", objState.StateId));
        //    lstParameter.Add(new MySqlParameter("U_CountryId", objState.CountryId));

        //    lstParameter.Add(new MySqlParameter("U_CreatedBy", objState.CreatedBy));
        //    lstParameter.Add(new MySqlParameter("U_CreatedOn", objState.CreatedOn));
        //    lstParameter.Add(new MySqlParameter("U_IsActive", objState.IsActive));
        //    lstParameter.Add(new MySqlParameter("U_IsDelete", objState.IsDelete));


        //    lstParameter.Add(new MySqlParameter("U_ModifiedBy", objState.ModifiedBy));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedOn", objState.ModifiedOn));
        //    lstParameter.Add(new MySqlParameter("U_Name", objState.Name));

        //    lstParameter.Add(new MySqlParameter("U_StateCode", objState.StateCode));

        //    int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "State_Update", lstParameter.ToArray());
        //    return returnValue;
        //}
        public void Delete_State_byID(int StateId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("U_StateId", StateId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "State_Delete_byID", lstParameter.ToArray());


        }
        public List<State> Get_State_ByCountryID(int CountryId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_CountryId", CountryId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "state_getBy_CountryId", lstParameter.ToArray());
            List<State> lstEntity = new List<State>();

            State objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                {
                    lstEntity.Add(objEntity);

                }
            }
            return lstEntity;
        }
        public State Get_State_ByStateID(int StateID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_StateID", StateID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "state_get_ByID", lstParameter.ToArray());


            State objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);

            }
            return objEntity;
        }
        private State FetchEntity(DataRow dr)
        {
            State objEntity = new State();

            if (dr.Table.Columns.Contains("CountryId") && dr["CountryId"] != DBNull.Value)
            {
                objEntity.CountryId = Convert.ToInt32(dr["CountryId"]);
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
                objEntity.Name = Convert.ToString(dr["Name"]);
            }
            if (dr.Table.Columns.Contains("StateCode") && dr["StateCode"] != DBNull.Value)
            {
                objEntity.StateCode = Convert.ToString(dr["StateCode"]);
            }
            if (dr.Table.Columns.Contains("StateId") && dr["StateId"] != DBNull.Value)
            {
                objEntity.StateId = Convert.ToInt32(dr["StateId"]);
            }

            if (dr.Table.Columns.Contains("IsActive") && dr["IsActive"] != DBNull.Value)
            {
                objEntity.IsActive = Convert.ToBoolean(dr["IsActive"]);
            }




            return objEntity;
        }
    }
}
