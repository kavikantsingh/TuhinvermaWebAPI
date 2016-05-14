using LAPP.DAL;
using LAPP.LOGING.ENTITY;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.LOGING.DAL
{
    class CategoryLogDAL
    {
        public int Save_categorylog(CategoryLog objcategorylog)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("CategoryLogID", objcategorylog.CategoryLogID));
            lstParameter.Add(new MySqlParameter("LogID", objcategorylog.LogID));
            lstParameter.Add(new MySqlParameter("CategoryID", objcategorylog.CategoryID));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "categorylog_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        private CategoryLog FetchEntity(DataRow dr)
        {
            CategoryLog objEntity = new CategoryLog();

            if (dr.Table.Columns.Contains("CategoryLogID") && dr["CategoryLogID"] != DBNull.Value)
            {
                objEntity.CategoryLogID = Convert.ToInt32(dr["CategoryLogID"]);
            }
            if (dr.Table.Columns.Contains("CategoryID") && dr["CategoryID"] != DBNull.Value)
            {
                objEntity.CategoryID = Convert.ToInt32(dr["CategoryID"]);
            }
            if (dr.Table.Columns.Contains("LogID") && dr["LogID"] != DBNull.Value)
            {
                objEntity.LogID = Convert.ToInt32(dr["LogID"]);
            }
            return objEntity;
        }

    }
}
