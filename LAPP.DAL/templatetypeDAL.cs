using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class TemplateTypeDAL : BaseDAL
    {
        public int Save_TemplateType(TemplateType objTemplateType)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("TemplateTypeId", objTemplateType.TemplateTypeId));
            lstParameter.Add(new MySqlParameter("Code", objTemplateType.Code));
            lstParameter.Add(new MySqlParameter("IsActive", objTemplateType.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objTemplateType.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objTemplateType.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objTemplateType.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objTemplateType.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objTemplateType.ModifiedOn));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "templatetype_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<TemplateType> Get_All_TemplateType()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "TEMPLATETYPE_GET_ALL");
            List<TemplateType> lstEntity = new List<TemplateType>();
            TemplateType objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private TemplateType FetchEntity(DataRow dr)
        {
            TemplateType objEntity = new TemplateType();
            if (dr.Table.Columns.Contains("TemplateTypeId") && dr["TemplateTypeId"] != DBNull.Value)
            {
                objEntity.TemplateTypeId = Convert.ToInt32(dr["TemplateTypeId"]);
            }
            if (dr.Table.Columns.Contains("Code") && dr["Code"] != DBNull.Value)
            {
                objEntity.Code = Convert.ToString(dr["Code"]);
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
