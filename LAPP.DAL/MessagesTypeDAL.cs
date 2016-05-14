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
    public class MessagesTypeDAL : BaseDAL
    {
        public int Save_MessagesType(MessagesType objMessagesType)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("MessageTypeId", objMessagesType.MessageTypeId));
            lstParameter.Add(new MySqlParameter("MessageTypeCode", objMessagesType.MessageTypeCode));

            lstParameter.Add(new MySqlParameter("IsActive", objMessagesType.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objMessagesType.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objMessagesType.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objMessagesType.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objMessagesType.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objMessagesType.ModifiedOn));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "MessagesType_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        //public int Update_MessagesType(MessagesType objMessagesType)
        //{
        //    DBHelper objDB = new DBHelper();
        //    List<MySqlParameter> lstParameter = new List<MySqlParameter>();

        //    lstParameter.Add(new MySqlParameter("U_MessageTypeId", objMessagesType.MessageTypeId));
        //    lstParameter.Add(new MySqlParameter("U_MessageTypeCode", objMessagesType.MessageTypeCode));

        //    lstParameter.Add(new MySqlParameter("U_IsActive", objMessagesType.IsActive));
        //    lstParameter.Add(new MySqlParameter("U_IsDeleted", objMessagesType.IsDeleted));
        //    lstParameter.Add(new MySqlParameter("U_CreatedBy", objMessagesType.CreatedBy));
        //    lstParameter.Add(new MySqlParameter("U_CreatedOn", objMessagesType.CreatedOn));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedBy", objMessagesType.ModifiedBy));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedOn", objMessagesType.ModifiedOn));

        //    int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "MessagesType_Update", lstParameter.ToArray());
        //    return returnValue;
        //}

        public MessagesType Get_MessagesType_byMessagesTypeId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_MessageTypeId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "MessagesType_Get_BY_MessageTypeId", lstParameter.ToArray());
            MessagesType objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<MessagesType> Get_All_MessagesType()
        {

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "MessagesType_Get_All");
            List<MessagesType> lstEntity = new List<MessagesType>();
            MessagesType objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private MessagesType FetchEntity(DataRow dr)
        {
            MessagesType objEntity = new MessagesType();

            if (dr.Table.Columns.Contains("MessageTypeId") && dr["MessageTypeId"] != DBNull.Value)
            {
                objEntity.MessageTypeId = Convert.ToInt32(dr["MessageTypeId"]);
            }
            if (dr.Table.Columns.Contains("MessageTypeCode") && dr["MessageTypeCode"] != DBNull.Value)
            {
                objEntity.MessageTypeCode = Convert.ToString(dr["MessageTypeCode"]);
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
