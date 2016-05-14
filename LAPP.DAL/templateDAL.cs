using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class TemplateDAL : BaseDAL
    {
        public int Save_Template(Template objTemplate)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("TemplateId", objTemplate.TemplateId));
            lstParameter.Add(new MySqlParameter("TemplateCode", objTemplate.TemplateCode));
            lstParameter.Add(new MySqlParameter("TemplateName", objTemplate.TemplateName));
            lstParameter.Add(new MySqlParameter("TemplateSubject", objTemplate.TemplateSubject));
            lstParameter.Add(new MySqlParameter("TemplateMessage", objTemplate.TemplateMessage));
            lstParameter.Add(new MySqlParameter("TemplateTypeId", objTemplate.TemplateTypeId));
            lstParameter.Add(new MySqlParameter("MasterTransactionId", objTemplate.MasterTransactionId));
            lstParameter.Add(new MySqlParameter("PageModuleId", objTemplate.PageModuleId));
            lstParameter.Add(new MySqlParameter("PageModuleTabSubModuleId", objTemplate.PageModuleTabSubModuleId));
            lstParameter.Add(new MySqlParameter("PageTabSectionId", objTemplate.PageTabSectionId));
            lstParameter.Add(new MySqlParameter("EffectiveDate", objTemplate.EffectiveDate));
            lstParameter.Add(new MySqlParameter("EndDate", objTemplate.EndDate));
            lstParameter.Add(new MySqlParameter("IsEnabled", objTemplate.IsEnabled));
            lstParameter.Add(new MySqlParameter("IsEditable", objTemplate.IsEditable));
            lstParameter.Add(new MySqlParameter("IsActive", objTemplate.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objTemplate.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objTemplate.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objTemplate.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objTemplate.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objTemplate.ModifiedOn));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "template_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<Template> Get_All_Template()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "TEMPLATE_GET_ALL");
            List<Template> lstEntity = new List<Template>();
            Template objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private Template FetchEntity(DataRow dr)
        {
            Template objEntity = new Template();
            if (dr.Table.Columns.Contains("TemplateId") && dr["TemplateId"] != DBNull.Value)
            {
                objEntity.TemplateId = Convert.ToInt32(dr["TemplateId"]);
            }
            if (dr.Table.Columns.Contains("TemplateCode") && dr["TemplateCode"] != DBNull.Value)
            {
                objEntity.TemplateCode = Convert.ToString(dr["TemplateCode"]);
            }
            if (dr.Table.Columns.Contains("TemplateName") && dr["TemplateName"] != DBNull.Value)
            {
                objEntity.TemplateName = Convert.ToString(dr["TemplateName"]);
            }
            if (dr.Table.Columns.Contains("TemplateSubject") && dr["TemplateSubject"] != DBNull.Value)
            {
                objEntity.TemplateSubject = Convert.ToString(dr["TemplateSubject"]);
            }
            if (dr.Table.Columns.Contains("TemplateMessage") && dr["TemplateMessage"] != DBNull.Value)
            {
                objEntity.TemplateMessage = Convert.ToString(dr["TemplateMessage"]);
            }
            if (dr.Table.Columns.Contains("TemplateTypeId") && dr["TemplateTypeId"] != DBNull.Value)
            {
                objEntity.TemplateTypeId = Convert.ToInt32(dr["TemplateTypeId"]);
            }
            if (dr.Table.Columns.Contains("MasterTransactionId") && dr["MasterTransactionId"] != DBNull.Value)
            {
                objEntity.MasterTransactionId = Convert.ToInt32(dr["MasterTransactionId"]);
            }
            if (dr.Table.Columns.Contains("PageModuleId") && dr["PageModuleId"] != DBNull.Value)
            {
                objEntity.PageModuleId = Convert.ToInt32(dr["PageModuleId"]);
            }
            if (dr.Table.Columns.Contains("PageModuleTabSubModuleId") && dr["PageModuleTabSubModuleId"] != DBNull.Value)
            {
                objEntity.PageModuleTabSubModuleId = Convert.ToInt32(dr["PageModuleTabSubModuleId"]);
            }
            if (dr.Table.Columns.Contains("PageTabSectionId") && dr["PageTabSectionId"] != DBNull.Value)
            {
                objEntity.PageTabSectionId = Convert.ToInt32(dr["PageTabSectionId"]);
            }
            if (dr.Table.Columns.Contains("EffectiveDate") && dr["EffectiveDate"] != DBNull.Value)
            {
                objEntity.EffectiveDate = Convert.ToDateTime(dr["EffectiveDate"]);
            }
            if (dr.Table.Columns.Contains("EndDate") && dr["EndDate"] != DBNull.Value)
            {
                objEntity.EndDate = Convert.ToDateTime(dr["EndDate"]);
            }
            if (dr.Table.Columns.Contains("IsEnabled") && dr["IsEnabled"] != DBNull.Value)
            {
                objEntity.IsEnabled = Convert.ToBoolean(dr["IsEnabled"]);
            }
            if (dr.Table.Columns.Contains("IsEditable") && dr["IsEditable"] != DBNull.Value)
            {
                objEntity.IsEditable = Convert.ToBoolean(dr["IsEditable"]);
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
