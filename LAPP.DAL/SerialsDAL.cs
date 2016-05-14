using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class SerialsDAL : BaseDAL
    {
        public string Get_Receipt_No()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Serial_get_by_receipt_no");
            return ds.Tables[0].Rows[0][0].ToString();
        }


        public string serial_get_for_License_Number()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "serial_get_for_License_Number");
            return ds.Tables[0].Rows[0][0].ToString();
        }


       public string serial_get_for_ApplicationNumber()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "serial_get_for_ApplicationNumber");
            return ds.Tables[0].Rows[0][0].ToString();
        }

        private Serials FetchEntity(DataRow dr)
        {
            Serials objEntity = new Serials();
            if (dr.Table.Columns.Contains("Serial_Id") && dr["Serial_Id"] != DBNull.Value)
            {
                objEntity.Serial_Id = Convert.ToInt32(dr["Serial_Id"]);
            }
            if (dr.Table.Columns.Contains("SerialName") && dr["SerialName"] != DBNull.Value)
            {
                objEntity.SerialName = Convert.ToString(dr["SerialName"]);
            }
            if (dr.Table.Columns.Contains("SerialPrefix") && dr["SerialPrefix"] != DBNull.Value)
            {
                objEntity.SerialPrefix = Convert.ToString(dr["SerialPrefix"]);
            }
            if (dr.Table.Columns.Contains("SerialCounter") && dr["SerialCounter"] != DBNull.Value)
            {
                objEntity.SerialCounter = Convert.ToInt64(dr["SerialCounter"]);
            }
            if (dr.Table.Columns.Contains("DTS") && dr["DTS"] != DBNull.Value)
            {
                objEntity.DTS = Convert.ToDateTime(dr["DTS"]);
            }
            return objEntity;

        }


       
    }
}
