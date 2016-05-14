using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class IndividualOtherDAL : BaseDAL
    {
        public int Save_IndividualOther(IndividualOther objIndividualOther)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualOtherId", objIndividualOther.IndividualOtherId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualOther.IndividualId));
            lstParameter.Add(new MySqlParameter("IsNameChanged", objIndividualOther.IsNameChanged));
            lstParameter.Add(new MySqlParameter("PlaceofBirthCity", objIndividualOther.PlaceofBirthCity));
            lstParameter.Add(new MySqlParameter("PlaceofBirthState", objIndividualOther.PlaceofBirthState));
            lstParameter.Add(new MySqlParameter("PlaceofBirthCountry", objIndividualOther.PlaceofBirthCountry));
            lstParameter.Add(new MySqlParameter("Picture", objIndividualOther.Picture));

            lstParameter.Add(new MySqlParameter("IsActive", objIndividualOther.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualOther.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualOther.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objIndividualOther.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objIndividualOther.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objIndividualOther.ModifiedOn));

            lstParameter.Add(new MySqlParameter("IndividualOtherGuid", objIndividualOther.IndividualOtherGuid));



            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "individualother_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }


        public List<IndividualOther> Get_All_IndividualOther()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualOther_Get_All");
            List<IndividualOther> lstEntity = new List<IndividualOther>();
            IndividualOther objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public IndividualOther Get_IndividualOther_By_IndividualOtherId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualOtherId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualOther_GET_BY_IndividualOtherId", lstParameter.ToArray());
            IndividualOther objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private IndividualOther FetchEntity(DataRow dr)
        {
            IndividualOther objEntity = new IndividualOther();

            if (dr.Table.Columns.Contains("IndividualOtherId") && dr["IndividualOtherId"] != DBNull.Value)
            {
                objEntity.IndividualOtherId = Convert.ToInt32(dr["IndividualOtherId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("IsNameChanged") && dr["IsNameChanged"] != DBNull.Value)
            {
                objEntity.IsNameChanged = Convert.ToBoolean(dr["IsNameChanged"]);
            }
            if (dr.Table.Columns.Contains("PlaceofBirthCity") && dr["PlaceofBirthCity"] != DBNull.Value)
            {
                objEntity.PlaceofBirthCity = Convert.ToString(dr["PlaceofBirthCity"]);
            }
            if (dr.Table.Columns.Contains("PlaceofBirthState") && dr["PlaceofBirthState"] != DBNull.Value)
            {
                objEntity.PlaceofBirthState = Convert.ToString(dr["PlaceofBirthState"]);
            }
            if (dr.Table.Columns.Contains("PlaceofBirthCountry") && dr["PlaceofBirthCountry"] != DBNull.Value)
            {
                objEntity.PlaceofBirthCountry = Convert.ToInt32(dr["PlaceofBirthCountry"]);
            }
            if (dr.Table.Columns.Contains("Picture") && dr["Picture"] != DBNull.Value)
            {
                objEntity.Picture = Convert.ToString(dr["Picture"]);
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


            if (dr.Table.Columns.Contains("IndividualOtherGuid") && dr["IndividualOtherGuid"] != DBNull.Value)
            {
                objEntity.IndividualOtherGuid = Convert.ToString(dr["IndividualOtherGuid"]);
            }

            return objEntity;

        }
    }
}
