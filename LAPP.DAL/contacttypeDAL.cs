using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class ContacttypeDAL : BaseDAL
    {
        public int Save_Contacttype(ContactType objContacttype)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ContactTypeId", objContacttype.ContactTypeId));
            lstParameter.Add(new MySqlParameter("Code", objContacttype.Code));
            lstParameter.Add(new MySqlParameter("Desc", objContacttype.Desc));
            lstParameter.Add(new MySqlParameter("IsActive", objContacttype.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objContacttype.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objContacttype.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objContacttype.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objContacttype.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objContacttype.ModifiedOn));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "contacttype_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        //public int Update_Contacttype(ContactType objContacttype)
        //{
        //    DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
        //    lstParameter.Add(new MySqlParameter("U_ContactTypeId", objContacttype.ContactTypeId));
        //    lstParameter.Add(new MySqlParameter("U_Code", objContacttype.Code));
        //    lstParameter.Add(new MySqlParameter("U_Desc", objContacttype.Desc));
        //    lstParameter.Add(new MySqlParameter("U_IsActive", objContacttype.IsActive));
        //    lstParameter.Add(new MySqlParameter("U_IsDeleted", objContacttype.IsDeleted));
        //    lstParameter.Add(new MySqlParameter("U_CreatedBy", objContacttype.CreatedBy));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedBy", objContacttype.ModifiedBy));

        //    int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "CONTACTTYPE_Update", lstParameter.ToArray());

        //    return returnValue;
        //}

        public List<ContactType> Get_All_Contacttype()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "CONTACTTYPE_GET_ALL");
            List<ContactType> lstEntity = new List<ContactType>();
            ContactType objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }
        public ContactType Get_Contacttype_byContacttypeId(int ContactTypeId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_ContactTypeId", ContactTypeId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "contacttype_Get_By_ContactTypeId", lstParameter.ToArray());

            ContactType objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);

            }
            return objEntity;
        }
        private ContactType FetchEntity(DataRow dr)
        {
            ContactType objEntity = new ContactType();
            if (dr.Table.Columns.Contains("ContactTypeId") && dr["ContactTypeId"] != DBNull.Value)
            {
                objEntity.ContactTypeId = Convert.ToInt32(dr["ContactTypeId"]);
            }
            if (dr.Table.Columns.Contains("Code") && dr["Code"] != DBNull.Value)
            {
                objEntity.Code = Convert.ToString(dr["Code"]);
            }
            if (dr.Table.Columns.Contains("Desc") && dr["Desc"] != DBNull.Value)
            {
                objEntity.Desc = Convert.ToString(dr["Desc"]);
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
