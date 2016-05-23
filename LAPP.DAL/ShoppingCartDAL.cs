using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using LAPP.ENTITY;
using MySql.Data.MySqlClient;
namespace LAPP.DAL
{
    public class ShoppingCartDAL : BaseDAL
    {
        public int Save_ShoppingCart(ShoppingCart objShoppingCart)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ShoppingCartId", objShoppingCart.ShoppingCartId));
            lstParameter.Add(new MySqlParameter("IndividualId", objShoppingCart.IndividualId));
            lstParameter.Add(new MySqlParameter("ProviderId", GetNullValue.ByDataType(objShoppingCart.ProviderId)));

            lstParameter.Add(new MySqlParameter("CreatedBy", objShoppingCart.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objShoppingCart.CreatedOn));
            lstParameter.Add(new MySqlParameter("ShoppingCartGuid", objShoppingCart.ShoppingCartGuid));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "ShoppingCart_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<ShoppingCart> Get_All_ShoppingCart()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ShoppingCart_GET_ALL");
            List<ShoppingCart> lstEntity = new List<ShoppingCart>();
            ShoppingCart objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<ShoppingCart> Get_ShoppingCart_by_IndividualId(int IndividualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            //lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ShoppingCart_GET_BY_IndividualId", lstParameter.ToArray());
            List<ShoppingCart> lstEntity = new List<ShoppingCart>();
            ShoppingCart objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public ShoppingCart Get_ShoppingCart_By_ShoppingCartId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_ShoppingCartId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ShoppingCart_GET_BY_ShoppingCartId", lstParameter.ToArray());
            ShoppingCart objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        private ShoppingCart FetchEntity(DataRow dr)
        {
            ShoppingCart objEntity = new ShoppingCart();

            if (dr.Table.Columns.Contains("ShoppingCartId") && dr["ShoppingCartId"] != DBNull.Value)
            {
                objEntity.ShoppingCartId = Convert.ToInt32(dr["ShoppingCartId"]);
            }

            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }

            if (dr.Table.Columns.Contains("ProviderId") && dr["ProviderId"] != DBNull.Value)
            {
                objEntity.ProviderId = Convert.ToInt32(dr["ProviderId"]);
            }


            if (dr.Table.Columns.Contains("CreatedBy") && dr["CreatedBy"] != DBNull.Value)
            {
                objEntity.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
            }
            if (dr.Table.Columns.Contains("CreatedOn") && dr["CreatedOn"] != DBNull.Value)
            {
                objEntity.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
            }

            if (dr.Table.Columns.Contains("ShoppingCartGuid") && dr["ShoppingCartGuid"] != DBNull.Value)
            {
                objEntity.ShoppingCartGuid = dr["ShoppingCartGuid"].ToString();
            }

            return objEntity;

        }
    }
}
