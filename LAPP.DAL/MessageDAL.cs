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
    public class MessageDAL : BaseDAL
    {
        public int Save_Message(Message objMessage)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("MessageId", objMessage.MessageId));
            lstParameter.Add(new MySqlParameter("MessageTypeId", objMessage.MessageTypeId));
            lstParameter.Add(new MySqlParameter("MessageCode", objMessage.MessageCode));
            lstParameter.Add(new MySqlParameter("MessageDesc", objMessage.MessageDesc));
            lstParameter.Add(new MySqlParameter("LabelName", objMessage.LabelName));
            lstParameter.Add(new MySqlParameter("IsEnabled", objMessage.IsEnabled));

            lstParameter.Add(new MySqlParameter("IsActive", objMessage.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objMessage.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objMessage.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objMessage.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objMessage.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objMessage.ModifiedOn));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "Messages_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        //public int Update_Message(Message objMessage)
        //{
        //    DBHelper objDB = new DBHelper();
        //    List<MySqlParameter> lstParameter = new List<MySqlParameter>();

        //    lstParameter.Add(new MySqlParameter("U_MessageId", objMessage.MessageId));
        //    lstParameter.Add(new MySqlParameter("U_MessageTypeId", objMessage.MessageTypeId));
        //    lstParameter.Add(new MySqlParameter("U_MessageCode", objMessage.MessageCode));
        //    lstParameter.Add(new MySqlParameter("U_MessageDesc", objMessage.MessageDesc));
        //    lstParameter.Add(new MySqlParameter("U_LabelName", objMessage.LabelName));
        //    lstParameter.Add(new MySqlParameter("U_IsEnabled", objMessage.IsEnabled));

        //    lstParameter.Add(new MySqlParameter("U_IsActive", objMessage.IsActive));
        //    lstParameter.Add(new MySqlParameter("U_IsDeleted", objMessage.IsDeleted));
        //    lstParameter.Add(new MySqlParameter("U_CreatedBy", objMessage.CreatedBy));
        //    lstParameter.Add(new MySqlParameter("U_CreatedOn", objMessage.CreatedOn));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedBy", objMessage.ModifiedBy));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedOn", objMessage.ModifiedOn));

        //    int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "Messages_Update", lstParameter.ToArray());
        //    return returnValue;
        //}

        public Message Get_Message_byMessageId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_MessageId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Messages_Get_BY_MessageId", lstParameter.ToArray());
            Message objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<Message> Get_All_Message()
        {

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Messages_Get_All");
            List<Message> lstEntity = new List<Message>();
            Message objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private Message FetchEntity(DataRow dr)
        {
            Message objEntity = new Message();

            if (dr.Table.Columns.Contains("MessageId") && dr["MessageId"] != DBNull.Value)
            {
                objEntity.MessageId = Convert.ToInt32(dr["MessageId"]);
            }

            if (dr.Table.Columns.Contains("MessageTypeId") && dr["MessageTypeId"] != DBNull.Value)
            {
                objEntity.MessageTypeId = Convert.ToInt32(dr["MessageTypeId"]);
            }

            if (dr.Table.Columns.Contains("MessageCode") && dr["MessageCode"] != DBNull.Value)
            {
                objEntity.MessageCode = Convert.ToString(dr["MessageCode"]);
            }

            if (dr.Table.Columns.Contains("MessageDesc") && dr["MessageDesc"] != DBNull.Value)
            {
                objEntity.MessageDesc = Convert.ToString(dr["MessageDesc"]);
            }

            if (dr.Table.Columns.Contains("LabelName") && dr["LabelName"] != DBNull.Value)
            {
                objEntity.LabelName = Convert.ToString(dr["LabelName"]);
            }

            if (dr.Table.Columns.Contains("IsEnabled") && dr["IsEnabled"] != DBNull.Value)
            {
                objEntity.IsEnabled = Convert.ToBoolean(dr["IsEnabled"]);
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


            // Used only for view

            if (dr.Table.Columns.Contains("MessageTypeName") && dr["MessageTypeName"] != DBNull.Value)
            {
                objEntity.MessageTypeName = Convert.ToString(dr["MessageTypeName"]);
            }

            return objEntity;
        }


    }
}
