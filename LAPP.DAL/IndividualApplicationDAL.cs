using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class IndividualApplicationDAL : BaseDAL
    {
        public int Save_IndividualApplication(IndividualApplication objIndividualApplication)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualApplication.IndividualId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objIndividualApplication.ApplicationId));

            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualApplication.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objIndividualApplication.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objIndividualApplication.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objIndividualApplication.ModifiedOn));
            lstParameter.Add(new MySqlParameter("IndividualApplicationGuid", objIndividualApplication.IndividualApplicationGuid));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "individualapplication_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<IndividualApplication> Get_All_IndividualApplication()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualapplication_Get_All");
            List<IndividualApplication> lstEntity = new List<IndividualApplication>();
            IndividualApplication objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public IndividualApplication Get_IndividualApplication_byIndividualId(int IndividualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParam = new List<MySqlParameter>();
            lstParam.Add(new MySqlParameter("G_IndividualId", IndividualId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "individualapplication_Get_By_IndividualId", lstParam.ToArray());

            IndividualApplication objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);

            }
            return objEntity;
        }

        private IndividualApplication FetchEntity(DataRow dr)
        {
            IndividualApplication objEntity = new IndividualApplication();


            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("ApplicationId") && dr["ApplicationId"] != DBNull.Value)
            {
                objEntity.ApplicationId = Convert.ToInt32(dr["ApplicationId"]);
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
