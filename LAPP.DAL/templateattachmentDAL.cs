using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class TemplateAttachmentDAL : BaseDAL
    {
        public int Save_TemplateAttachment(TemplateAttachment objTemplateAttachment)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("TemplateAttachmentId", objTemplateAttachment.TemplateAttachmentId));
            lstParameter.Add(new MySqlParameter("TemplateAttachmentName", objTemplateAttachment.TemplateAttachmentName));
            lstParameter.Add(new MySqlParameter("TemplateAttachmentLink", objTemplateAttachment.TemplateAttachmentLink));
            lstParameter.Add(new MySqlParameter("TemplateId", objTemplateAttachment.TemplateId));
            lstParameter.Add(new MySqlParameter("IsActive", objTemplateAttachment.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objTemplateAttachment.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objTemplateAttachment.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objTemplateAttachment.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objTemplateAttachment.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objTemplateAttachment.ModifiedOn));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "templateattachment_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<TemplateAttachment> Get_All_TemplateAttachment()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "TEMPLATEATTACHMENT_GET_ALL");
            List<TemplateAttachment> lstEntity = new List<TemplateAttachment>();
            TemplateAttachment objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private TemplateAttachment FetchEntity(DataRow dr)
        {
            TemplateAttachment objEntity = new TemplateAttachment();
            if (dr.Table.Columns.Contains("TemplateAttachmentId") && dr["TemplateAttachmentId"] != DBNull.Value)
            {
                objEntity.TemplateAttachmentId = Convert.ToInt32(dr["TemplateAttachmentId"]);
            }
            if (dr.Table.Columns.Contains("TemplateAttachmentName") && dr["TemplateAttachmentName"] != DBNull.Value)
            {
                objEntity.TemplateAttachmentName = Convert.ToString(dr["TemplateAttachmentName"]);
            }
            if (dr.Table.Columns.Contains("TemplateAttachmentLink") && dr["TemplateAttachmentLink"] != DBNull.Value)
            {
                objEntity.TemplateAttachmentLink = Convert.ToString(dr["TemplateAttachmentLink"]);
            }
            if (dr.Table.Columns.Contains("TemplateId") && dr["TemplateId"] != DBNull.Value)
            {
                objEntity.TemplateId = Convert.ToInt32(dr["TemplateId"]);
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
