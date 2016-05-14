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
    public class DivisionDAL : BaseDAL
    {

        public int Save_Division(Division objDivision)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("DivisionId", objDivision.DivisionId));
            lstParameter.Add(new MySqlParameter("BoardAuthorityId", objDivision.BoardAuthorityId));
            lstParameter.Add(new MySqlParameter("Name", objDivision.Name));
            lstParameter.Add(new MySqlParameter("Description", objDivision.Description));
            lstParameter.Add(new MySqlParameter("Abbreviation", objDivision.Abbreviation));

            lstParameter.Add(new MySqlParameter("PhysicalAddressLine1", objDivision.PhysicalAddressLine1));
            lstParameter.Add(new MySqlParameter("PhysicalAddressLine2", objDivision.PhysicalAddressLine2));
            lstParameter.Add(new MySqlParameter("PhysicalAddressCity", objDivision.PhysicalAddressCity));
            lstParameter.Add(new MySqlParameter("PhysicalAddressState", objDivision.PhysicalAddressState));
            lstParameter.Add(new MySqlParameter("PhysicalAddressZip", objDivision.PhysicalAddressZip));

            lstParameter.Add(new MySqlParameter("IsMailingSameasPhysical", objDivision.IsMailingSameasPhysical));

            lstParameter.Add(new MySqlParameter("MailingAddressCity", objDivision.MailingAddressCity));
            lstParameter.Add(new MySqlParameter("MailingAddressLine1", objDivision.MailingAddressLine1));
            lstParameter.Add(new MySqlParameter("MailingAddressLine2", objDivision.MailingAddressLine2));
            lstParameter.Add(new MySqlParameter("MailingAddressState", objDivision.MailingAddressState));
            lstParameter.Add(new MySqlParameter("MailingAddressZip", objDivision.MailingAddressZip));

            lstParameter.Add(new MySqlParameter("ContactPhone", objDivision.ContactPhone));
            lstParameter.Add(new MySqlParameter("ContactEmail", objDivision.ContactEmail));
            lstParameter.Add(new MySqlParameter("ContactFax", objDivision.ContactFax));
            lstParameter.Add(new MySqlParameter("AlternatePhone", objDivision.AlternatePhone));

            lstParameter.Add(new MySqlParameter("IsActive", objDivision.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objDivision.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objDivision.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objDivision.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objDivision.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objDivision.ModifiedOn));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "Division_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        //public int Update_Division(Division objDivision)
        //{
        //    DBHelper objDB = new DBHelper();
        //    List<MySqlParameter> lstParameter = new List<MySqlParameter>();

        //    lstParameter.Add(new MySqlParameter("U_DivisionId", objDivision.DivisionId));
        //    lstParameter.Add(new MySqlParameter("U_BoardAuthorityId", objDivision.BoardAuthorityId));
        //    lstParameter.Add(new MySqlParameter("U_Name", objDivision.Name));
        //    lstParameter.Add(new MySqlParameter("U_Description", objDivision.Description));
        //    lstParameter.Add(new MySqlParameter("U_Abbreviation", objDivision.Abbreviation));

        //    lstParameter.Add(new MySqlParameter("U_PhysicalAddressLine1", objDivision.PhysicalAddressLine1));
        //    lstParameter.Add(new MySqlParameter("U_PhysicalAddressLine2", objDivision.PhysicalAddressLine2));
        //    lstParameter.Add(new MySqlParameter("U_PhysicalAddressCity", objDivision.PhysicalAddressCity));
        //    lstParameter.Add(new MySqlParameter("U_PhysicalAddressState", objDivision.PhysicalAddressState));
        //    lstParameter.Add(new MySqlParameter("U_PhysicalAddressZip", objDivision.PhysicalAddressZip));

        //    lstParameter.Add(new MySqlParameter("U_IsMailingSameasPhysical", objDivision.IsMailingSameasPhysical));

        //    lstParameter.Add(new MySqlParameter("U_MailingAddressCity", objDivision.MailingAddressCity));
        //    lstParameter.Add(new MySqlParameter("U_MailingAddressLine1", objDivision.MailingAddressLine1));
        //    lstParameter.Add(new MySqlParameter("U_MailingAddressLine2", objDivision.MailingAddressLine2));
        //    lstParameter.Add(new MySqlParameter("U_MailingAddressState", objDivision.MailingAddressState));
        //    lstParameter.Add(new MySqlParameter("U_MailingAddressZip", objDivision.MailingAddressZip));

        //    lstParameter.Add(new MySqlParameter("U_ContactPhone", objDivision.ContactPhone));
        //    lstParameter.Add(new MySqlParameter("U_ContactEmail", objDivision.ContactEmail));
        //    lstParameter.Add(new MySqlParameter("U_ContactFax", objDivision.ContactFax));
        //    lstParameter.Add(new MySqlParameter("U_AlternatePhone", objDivision.AlternatePhone));

        //    lstParameter.Add(new MySqlParameter("U_IsActive", objDivision.IsActive));
        //    lstParameter.Add(new MySqlParameter("U_IsDeleted", objDivision.IsDeleted));
        //    lstParameter.Add(new MySqlParameter("U_CreatedBy", objDivision.CreatedBy));
        //    lstParameter.Add(new MySqlParameter("U_CreatedOn", objDivision.CreatedOn));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedBy", objDivision.ModifiedBy));
        //    lstParameter.Add(new MySqlParameter("U_ModifiedOn", objDivision.ModifiedOn));

        //    int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "Division_Update", lstParameter.ToArray());
        //    return returnValue;
        //}

        public Division Get_Division_byDivisionId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_DivisionId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Division_Get_BY_DivisionId", lstParameter.ToArray());
            Division objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public List<Division> Get_All_Division()
        {

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "Division_Get_All");
            List<Division> lstEntity = new List<Division>();
            Division objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        private Division FetchEntity(DataRow dr)
        {
            Division objEntity = new Division();

            if (dr.Table.Columns.Contains("DivisionId") && dr["DivisionId"] != DBNull.Value)
            {
                objEntity.DivisionId = Convert.ToInt32(dr["DivisionId"]);
            }

            if (dr.Table.Columns.Contains("BoardAuthorityId") && dr["BoardAuthorityId"] != DBNull.Value)
            {
                objEntity.BoardAuthorityId = Convert.ToInt32(dr["BoardAuthorityId"]);
            }

            if (dr.Table.Columns.Contains("Name") && dr["Name"] != DBNull.Value)
            {
                objEntity.Name = Convert.ToString(dr["Name"]);
            }

            if (dr.Table.Columns.Contains("Description") && dr["Description"] != DBNull.Value)
            {
                objEntity.Description = Convert.ToString(dr["Description"]);
            }

            if (dr.Table.Columns.Contains("Abbreviation") && dr["Abbreviation"] != DBNull.Value)
            {
                objEntity.Abbreviation = Convert.ToString(dr["Abbreviation"]);
            }



            if (dr.Table.Columns.Contains("PhysicalAddressLine1") && dr["PhysicalAddressLine1"] != DBNull.Value)
            {
                objEntity.PhysicalAddressLine1 = Convert.ToString(dr["PhysicalAddressLine1"]);
            }

            if (dr.Table.Columns.Contains("PhysicalAddressLine2") && dr["PhysicalAddressLine2"] != DBNull.Value)
            {
                objEntity.PhysicalAddressLine2 = Convert.ToString(dr["PhysicalAddressLine2"]);
            }
            if (dr.Table.Columns.Contains("PhysicalAddressCity") && dr["PhysicalAddressCity"] != DBNull.Value)
            {
                objEntity.PhysicalAddressCity = Convert.ToString(dr["PhysicalAddressCity"]);
            }
            if (dr.Table.Columns.Contains("PhysicalAddressState") && dr["PhysicalAddressState"] != DBNull.Value)
            {
                objEntity.PhysicalAddressState = Convert.ToString(dr["PhysicalAddressState"]);
            }
            if (dr.Table.Columns.Contains("PhysicalAddressZip") && dr["PhysicalAddressZip"] != DBNull.Value)
            {
                objEntity.PhysicalAddressZip = Convert.ToString(dr["PhysicalAddressZip"]);
            }


            if (dr.Table.Columns.Contains("IsMailingSameasPhysical") && dr["IsMailingSameasPhysical"] != DBNull.Value)
            {
                objEntity.IsMailingSameasPhysical = Convert.ToBoolean(dr["IsMailingSameasPhysical"]);
            }


            if (dr.Table.Columns.Contains("MailingAddressCity") && dr["MailingAddressCity"] != DBNull.Value)
            {
                objEntity.MailingAddressCity = Convert.ToString(dr["MailingAddressCity"]);
            }
            if (dr.Table.Columns.Contains("MailingAddressLine1") && dr["MailingAddressLine1"] != DBNull.Value)
            {
                objEntity.MailingAddressLine1 = Convert.ToString(dr["MailingAddressLine1"]);
            }
            if (dr.Table.Columns.Contains("MailingAddressLine2") && dr["MailingAddressLine2"] != DBNull.Value)
            {
                objEntity.MailingAddressLine2 = Convert.ToString(dr["MailingAddressLine2"]);
            }
            if (dr.Table.Columns.Contains("MailingAddressState") && dr["MailingAddressState"] != DBNull.Value)
            {
                objEntity.MailingAddressState = Convert.ToString(dr["MailingAddressState"]);
            }
            if (dr.Table.Columns.Contains("MailingAddressZip") && dr["MailingAddressZip"] != DBNull.Value)
            {
                objEntity.MailingAddressZip = Convert.ToString(dr["MailingAddressZip"]);
            }


            if (dr.Table.Columns.Contains("ContactEmail") && dr["ContactEmail"] != DBNull.Value)
            {
                objEntity.ContactEmail = Convert.ToString(dr["ContactEmail"]);
            }
            if (dr.Table.Columns.Contains("ContactFax") && dr["ContactFax"] != DBNull.Value)
            {
                objEntity.ContactFax = Convert.ToString(dr["ContactFax"]);
            }
            if (dr.Table.Columns.Contains("ContactPhone") && dr["ContactPhone"] != DBNull.Value)
            {
                objEntity.ContactPhone = Convert.ToString(dr["ContactPhone"]);
            }
            if (dr.Table.Columns.Contains("AlternatePhone") && dr["AlternatePhone"] != DBNull.Value)
            {
                objEntity.AlternatePhone = Convert.ToString(dr["AlternatePhone"]);
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
