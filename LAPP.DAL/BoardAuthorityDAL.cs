using LAPP.ENTITY;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.DAL
{
    public class BoardAuthorityDAL : BaseDAL
    {

        public int Save_BoardAuthority(BoardAuthority objBoardAuthority)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("Acronym", objBoardAuthority.Acronym));
            lstParameter.Add(new MySqlParameter("AlternatePhone", objBoardAuthority.AlternatePhone));
            lstParameter.Add(new MySqlParameter("ApplicationSystemUrl", objBoardAuthority.ApplicationSystemUrl));
            lstParameter.Add(new MySqlParameter("BoardAuthorityGuid", objBoardAuthority.BoardAuthorityGuid));
            lstParameter.Add(new MySqlParameter("BoardAuthorityId", objBoardAuthority.BoardAuthorityId));
            lstParameter.Add(new MySqlParameter("Code", objBoardAuthority.Code));
            lstParameter.Add(new MySqlParameter("ContactEmail", objBoardAuthority.ContactEmail));
            lstParameter.Add(new MySqlParameter("ContactFax", objBoardAuthority.ContactFax));
            lstParameter.Add(new MySqlParameter("ContactPhone", objBoardAuthority.ContactPhone));
            lstParameter.Add(new MySqlParameter("CreatedBy", objBoardAuthority.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objBoardAuthority.CreatedOn));
            lstParameter.Add(new MySqlParameter("IsActive", objBoardAuthority.IsActive));
            lstParameter.Add(new MySqlParameter("IsMailingSameasPhysical", objBoardAuthority.IsMailingSameasPhysical));
            lstParameter.Add(new MySqlParameter("MailingAddressCity", objBoardAuthority.MailingAddressCity));
            lstParameter.Add(new MySqlParameter("MailingAddressLine1", objBoardAuthority.MailingAddressLine1));
            lstParameter.Add(new MySqlParameter("MailingAddressLine2", objBoardAuthority.MailingAddressLine2));
            lstParameter.Add(new MySqlParameter("MailingAddressState", objBoardAuthority.MailingAddressState));
            lstParameter.Add(new MySqlParameter("MailingAddressZip", objBoardAuthority.MailingAddressZip));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objBoardAuthority.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objBoardAuthority.ModifiedOn));
            lstParameter.Add(new MySqlParameter("Name", objBoardAuthority.Name));
            lstParameter.Add(new MySqlParameter("PhysicalAddressCity", objBoardAuthority.PhysicalAddressCity));
            lstParameter.Add(new MySqlParameter("PhysicalAddressLine1", objBoardAuthority.PhysicalAddressLine1));
            lstParameter.Add(new MySqlParameter("PhysicalAddressLine2", objBoardAuthority.PhysicalAddressLine2));
            lstParameter.Add(new MySqlParameter("PhysicalAddressState", objBoardAuthority.PhysicalAddressState));

            lstParameter.Add(new MySqlParameter("PhysicalAddressZip", objBoardAuthority.PhysicalAddressZip));
            lstParameter.Add(new MySqlParameter("StateCode", objBoardAuthority.StateCode));
            lstParameter.Add(new MySqlParameter("SystemAbbreviation", objBoardAuthority.SystemAbbreviation));
            lstParameter.Add(new MySqlParameter("SystemContact", objBoardAuthority.SystemContact));
            lstParameter.Add(new MySqlParameter("SystemName", objBoardAuthority.SystemName));
            lstParameter.Add(new MySqlParameter("SystemUrl", objBoardAuthority.SystemUrl));
            lstParameter.Add(new MySqlParameter("Url", objBoardAuthority.Url));
            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "BoardAuthority_Save", true, lstParameter.ToArray());
            return returnValue;
        }

        public int Save_BoardAuthority(BoardAuthority objBoardAuthority, bool Switch)
        {
            string type = Convert.ToString(ConfigurationManager.AppSettings["DatabaseType"]);
            if (type.ToUpper() == "MSSQL")
            {
                DBHelperMSSQL objDB = new DBHelperMSSQL();
                List<SqlParameter> lstParameter = new List<SqlParameter>();
                lstParameter.Add(new SqlParameter("Acronym", objBoardAuthority.Acronym));
                lstParameter.Add(new SqlParameter("AlternatePhone", objBoardAuthority.AlternatePhone));
                lstParameter.Add(new SqlParameter("ApplicationSystemUrl", objBoardAuthority.ApplicationSystemUrl));
                lstParameter.Add(new SqlParameter("BoardAuthorityGuid", objBoardAuthority.BoardAuthorityGuid));
                lstParameter.Add(new SqlParameter("BoardAuthorityId", objBoardAuthority.BoardAuthorityId));
                lstParameter.Add(new SqlParameter("Code", objBoardAuthority.Code));
                lstParameter.Add(new SqlParameter("ContactEmail", objBoardAuthority.ContactEmail));
                lstParameter.Add(new SqlParameter("ContactFax", objBoardAuthority.ContactFax));
                lstParameter.Add(new SqlParameter("ContactPhone", objBoardAuthority.ContactPhone));
                lstParameter.Add(new SqlParameter("CreatedBy", objBoardAuthority.CreatedBy));
                lstParameter.Add(new SqlParameter("CreatedOn", objBoardAuthority.CreatedOn));
                lstParameter.Add(new SqlParameter("IsActive", objBoardAuthority.IsActive));
                lstParameter.Add(new SqlParameter("IsMailingSameasPhysical", objBoardAuthority.IsMailingSameasPhysical));
                lstParameter.Add(new SqlParameter("MailingAddressCity", objBoardAuthority.MailingAddressCity));
                lstParameter.Add(new SqlParameter("MailingAddressLine1", objBoardAuthority.MailingAddressLine1));
                lstParameter.Add(new SqlParameter("MailingAddressLine2", objBoardAuthority.MailingAddressLine2));
                lstParameter.Add(new SqlParameter("MailingAddressState", objBoardAuthority.MailingAddressState));
                lstParameter.Add(new SqlParameter("MailingAddressZip", objBoardAuthority.MailingAddressZip));
                lstParameter.Add(new SqlParameter("ModifiedBy", objBoardAuthority.ModifiedBy));
                lstParameter.Add(new SqlParameter("ModifiedOn", objBoardAuthority.ModifiedOn));
                lstParameter.Add(new SqlParameter("Name", objBoardAuthority.Name));
                lstParameter.Add(new SqlParameter("PhysicalAddressCity", objBoardAuthority.PhysicalAddressCity));
                lstParameter.Add(new SqlParameter("PhysicalAddressLine1", objBoardAuthority.PhysicalAddressLine1));
                lstParameter.Add(new SqlParameter("PhysicalAddressLine2", objBoardAuthority.PhysicalAddressLine2));
                lstParameter.Add(new SqlParameter("PhysicalAddressState", objBoardAuthority.PhysicalAddressState));

                lstParameter.Add(new SqlParameter("PhysicalAddressZip", objBoardAuthority.PhysicalAddressZip));
                lstParameter.Add(new SqlParameter("StateCode", objBoardAuthority.StateCode));
                lstParameter.Add(new SqlParameter("SystemAbbreviation", objBoardAuthority.SystemAbbreviation));
                lstParameter.Add(new SqlParameter("SystemContact", objBoardAuthority.SystemContact));
                lstParameter.Add(new SqlParameter("SystemName", objBoardAuthority.SystemName));
                lstParameter.Add(new SqlParameter("SystemUrl", objBoardAuthority.SystemUrl));
                lstParameter.Add(new SqlParameter("Url", objBoardAuthority.Url));
                SqlParameter returnParam = new SqlParameter("ReturnParam", SqlDbType.Int);
                returnParam.Direction = ParameterDirection.ReturnValue;
                lstParameter.Add(returnParam);
                objDB.ExecuteNonQuery(CommandType.StoredProcedure, "BoardAuthority_Save", lstParameter.ToArray());
                int returnValue = Convert.ToInt32(returnParam.Value);
                return returnValue;
            }
            else
            {
                DBHelper objDB = new DBHelper();
                List<MySqlParameter> lstParameter = new List<MySqlParameter>();
                lstParameter.Add(new MySqlParameter("Acronym", objBoardAuthority.Acronym));
                lstParameter.Add(new MySqlParameter("AlternatePhone", objBoardAuthority.AlternatePhone));
                lstParameter.Add(new MySqlParameter("ApplicationSystemUrl", objBoardAuthority.ApplicationSystemUrl));
                lstParameter.Add(new MySqlParameter("BoardAuthorityGuid", objBoardAuthority.BoardAuthorityGuid));
                lstParameter.Add(new MySqlParameter("BoardAuthorityId", objBoardAuthority.BoardAuthorityId));
                lstParameter.Add(new MySqlParameter("Code", objBoardAuthority.Code));
                lstParameter.Add(new MySqlParameter("ContactEmail", objBoardAuthority.ContactEmail));
                lstParameter.Add(new MySqlParameter("ContactFax", objBoardAuthority.ContactFax));
                lstParameter.Add(new MySqlParameter("ContactPhone", objBoardAuthority.ContactPhone));
                lstParameter.Add(new MySqlParameter("CreatedBy", objBoardAuthority.CreatedBy));
                lstParameter.Add(new MySqlParameter("CreatedOn", objBoardAuthority.CreatedOn));
                lstParameter.Add(new MySqlParameter("IsActive", objBoardAuthority.IsActive));
                lstParameter.Add(new MySqlParameter("IsMailingSameasPhysical", objBoardAuthority.IsMailingSameasPhysical));
                lstParameter.Add(new MySqlParameter("MailingAddressCity", objBoardAuthority.MailingAddressCity));
                lstParameter.Add(new MySqlParameter("MailingAddressLine1", objBoardAuthority.MailingAddressLine1));
                lstParameter.Add(new MySqlParameter("MailingAddressLine2", objBoardAuthority.MailingAddressLine2));
                lstParameter.Add(new MySqlParameter("MailingAddressState", objBoardAuthority.MailingAddressState));
                lstParameter.Add(new MySqlParameter("MailingAddressZip", objBoardAuthority.MailingAddressZip));
                lstParameter.Add(new MySqlParameter("ModifiedBy", objBoardAuthority.ModifiedBy));
                lstParameter.Add(new MySqlParameter("ModifiedOn", objBoardAuthority.ModifiedOn));
                lstParameter.Add(new MySqlParameter("Name", objBoardAuthority.Name));
                lstParameter.Add(new MySqlParameter("PhysicalAddressCity", objBoardAuthority.PhysicalAddressCity));
                lstParameter.Add(new MySqlParameter("PhysicalAddressLine1", objBoardAuthority.PhysicalAddressLine1));
                lstParameter.Add(new MySqlParameter("PhysicalAddressLine2", objBoardAuthority.PhysicalAddressLine2));
                lstParameter.Add(new MySqlParameter("PhysicalAddressState", objBoardAuthority.PhysicalAddressState));

                lstParameter.Add(new MySqlParameter("PhysicalAddressZip", objBoardAuthority.PhysicalAddressZip));
                lstParameter.Add(new MySqlParameter("StateCode", objBoardAuthority.StateCode));
                lstParameter.Add(new MySqlParameter("SystemAbbreviation", objBoardAuthority.SystemAbbreviation));
                lstParameter.Add(new MySqlParameter("SystemContact", objBoardAuthority.SystemContact));
                lstParameter.Add(new MySqlParameter("SystemName", objBoardAuthority.SystemName));
                lstParameter.Add(new MySqlParameter("SystemUrl", objBoardAuthority.SystemUrl));
                lstParameter.Add(new MySqlParameter("Url", objBoardAuthority.Url));
                MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
                returnParam.Direction = ParameterDirection.ReturnValue;
                lstParameter.Add(returnParam);
                int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "BoardAuthority_Save", true, lstParameter.ToArray());
                return returnValue;
            }
        }

        public int Update_BoardAuthority(BoardAuthority objBoardAuthority)
        {


            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("U_Acronym", objBoardAuthority.Acronym));
            lstParameter.Add(new MySqlParameter("U_AlternatePhone", objBoardAuthority.AlternatePhone));
            lstParameter.Add(new MySqlParameter("U_ApplicationSystemUrl", objBoardAuthority.ApplicationSystemUrl));
            lstParameter.Add(new MySqlParameter("U_BoardAuthorityGuid", objBoardAuthority.BoardAuthorityGuid));
            lstParameter.Add(new MySqlParameter("U_BoardAuthorityId", objBoardAuthority.BoardAuthorityId));
            lstParameter.Add(new MySqlParameter("U_Code", objBoardAuthority.Code));
            lstParameter.Add(new MySqlParameter("U_ContactEmail", objBoardAuthority.ContactEmail));
            lstParameter.Add(new MySqlParameter("U_ContactFax", objBoardAuthority.ContactFax));
            lstParameter.Add(new MySqlParameter("U_ContactPhone", objBoardAuthority.ContactPhone));
            lstParameter.Add(new MySqlParameter("U_CreatedBy", objBoardAuthority.CreatedBy));
            lstParameter.Add(new MySqlParameter("U_CreatedOn", objBoardAuthority.CreatedOn));
            lstParameter.Add(new MySqlParameter("U_IsActive", objBoardAuthority.IsActive));
            lstParameter.Add(new MySqlParameter("U_IsMailingSameasPhysical", objBoardAuthority.IsMailingSameasPhysical));
            lstParameter.Add(new MySqlParameter("U_MailingAddressCity", objBoardAuthority.MailingAddressCity));
            lstParameter.Add(new MySqlParameter("U_MailingAddressLine1", objBoardAuthority.MailingAddressLine1));
            lstParameter.Add(new MySqlParameter("U_MailingAddressLine2", objBoardAuthority.MailingAddressLine2));
            lstParameter.Add(new MySqlParameter("U_MailingAddressState", objBoardAuthority.MailingAddressState));
            lstParameter.Add(new MySqlParameter("U_MailingAddressZip", objBoardAuthority.MailingAddressZip));
            lstParameter.Add(new MySqlParameter("U_ModifiedBy", objBoardAuthority.ModifiedBy));
            lstParameter.Add(new MySqlParameter("U_ModifiedOn", objBoardAuthority.ModifiedOn));
            lstParameter.Add(new MySqlParameter("U_Name", objBoardAuthority.Name));
            lstParameter.Add(new MySqlParameter("U_PhysicalAddressCity", objBoardAuthority.PhysicalAddressCity));
            lstParameter.Add(new MySqlParameter("U_PhysicalAddressLine1", objBoardAuthority.PhysicalAddressLine1));
            lstParameter.Add(new MySqlParameter("U_PhysicalAddressLine2", objBoardAuthority.PhysicalAddressLine2));
            lstParameter.Add(new MySqlParameter("U_PhysicalAddressState", objBoardAuthority.PhysicalAddressState));

            lstParameter.Add(new MySqlParameter("U_PhysicalAddressZip", objBoardAuthority.PhysicalAddressZip));
            lstParameter.Add(new MySqlParameter("U_StateCode", objBoardAuthority.StateCode));
            lstParameter.Add(new MySqlParameter("U_SystemAbbreviation", objBoardAuthority.SystemAbbreviation));
            lstParameter.Add(new MySqlParameter("U_SystemContact", objBoardAuthority.SystemContact));
            lstParameter.Add(new MySqlParameter("U_SystemName", objBoardAuthority.SystemName));
            lstParameter.Add(new MySqlParameter("U_SystemUrl", objBoardAuthority.SystemUrl));
            lstParameter.Add(new MySqlParameter("U_Url", objBoardAuthority.Url));
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "BoardAuthority_Update", lstParameter.ToArray());
            return returnValue;
        }

        public int Update_BoardAuthority(BoardAuthority objBoardAuthority, bool Switch)
        {

            string type = Convert.ToString(ConfigurationManager.AppSettings["DatabaseType"]);
            if (type.ToUpper() == "MSSQL")
            {
                DBHelperMSSQL objDB = new DBHelperMSSQL();
                List<SqlParameter> lstParameter = new List<SqlParameter>();
                lstParameter.Add(new SqlParameter("Acronym", objBoardAuthority.Acronym));
                lstParameter.Add(new SqlParameter("AlternatePhone", objBoardAuthority.AlternatePhone));
                lstParameter.Add(new SqlParameter("ApplicationSystemUrl", objBoardAuthority.ApplicationSystemUrl));
                lstParameter.Add(new SqlParameter("BoardAuthorityGuid", objBoardAuthority.BoardAuthorityGuid));
                lstParameter.Add(new SqlParameter("BoardAuthorityId", objBoardAuthority.BoardAuthorityId));
                lstParameter.Add(new SqlParameter("Code", objBoardAuthority.Code));
                lstParameter.Add(new SqlParameter("ContactEmail", objBoardAuthority.ContactEmail));
                lstParameter.Add(new SqlParameter("ContactFax", objBoardAuthority.ContactFax));
                lstParameter.Add(new SqlParameter("ContactPhone", objBoardAuthority.ContactPhone));
                lstParameter.Add(new SqlParameter("CreatedBy", objBoardAuthority.CreatedBy));
                lstParameter.Add(new SqlParameter("CreatedOn", objBoardAuthority.CreatedOn));
                lstParameter.Add(new SqlParameter("IsActive", objBoardAuthority.IsActive));
                lstParameter.Add(new SqlParameter("IsMailingSameasPhysical", objBoardAuthority.IsMailingSameasPhysical));
                lstParameter.Add(new SqlParameter("MailingAddressCity", objBoardAuthority.MailingAddressCity));
                lstParameter.Add(new SqlParameter("MailingAddressLine1", objBoardAuthority.MailingAddressLine1));
                lstParameter.Add(new SqlParameter("MailingAddressLine2", objBoardAuthority.MailingAddressLine2));
                lstParameter.Add(new SqlParameter("MailingAddressState", objBoardAuthority.MailingAddressState));
                lstParameter.Add(new SqlParameter("MailingAddressZip", objBoardAuthority.MailingAddressZip));
                lstParameter.Add(new SqlParameter("ModifiedBy", objBoardAuthority.ModifiedBy));
                lstParameter.Add(new SqlParameter("ModifiedOn", objBoardAuthority.ModifiedOn));
                lstParameter.Add(new SqlParameter("Name", objBoardAuthority.Name));
                lstParameter.Add(new SqlParameter("PhysicalAddressCity", objBoardAuthority.PhysicalAddressCity));
                lstParameter.Add(new SqlParameter("PhysicalAddressLine1", objBoardAuthority.PhysicalAddressLine1));
                lstParameter.Add(new SqlParameter("PhysicalAddressLine2", objBoardAuthority.PhysicalAddressLine2));
                lstParameter.Add(new SqlParameter("PhysicalAddressState", objBoardAuthority.PhysicalAddressState));

                lstParameter.Add(new SqlParameter("PhysicalAddressZip", objBoardAuthority.PhysicalAddressZip));
                lstParameter.Add(new SqlParameter("StateCode", objBoardAuthority.StateCode));
                lstParameter.Add(new SqlParameter("SystemAbbreviation", objBoardAuthority.SystemAbbreviation));
                lstParameter.Add(new SqlParameter("SystemContact", objBoardAuthority.SystemContact));
                lstParameter.Add(new SqlParameter("SystemName", objBoardAuthority.SystemName));
                lstParameter.Add(new SqlParameter("SystemUrl", objBoardAuthority.SystemUrl));
                lstParameter.Add(new SqlParameter("Url", objBoardAuthority.Url));
                SqlParameter returnParam = new SqlParameter("ReturnParam", SqlDbType.Int);
                returnParam.Direction = ParameterDirection.ReturnValue;
                lstParameter.Add(returnParam);
                objDB.ExecuteNonQuery(CommandType.StoredProcedure, "BoardAuthority_Save", lstParameter.ToArray());
                int returnValue = Convert.ToInt32(returnParam.Value);
                return returnValue;
            }
            else
            {
                DBHelper objDB = new DBHelper();
                List<MySqlParameter> lstParameter = new List<MySqlParameter>();
                lstParameter.Add(new MySqlParameter("U_Acronym", objBoardAuthority.Acronym));
                lstParameter.Add(new MySqlParameter("U_AlternatePhone", objBoardAuthority.AlternatePhone));
                lstParameter.Add(new MySqlParameter("U_ApplicationSystemUrl", objBoardAuthority.ApplicationSystemUrl));
                lstParameter.Add(new MySqlParameter("U_BoardAuthorityGuid", objBoardAuthority.BoardAuthorityGuid));
                lstParameter.Add(new MySqlParameter("U_BoardAuthorityId", objBoardAuthority.BoardAuthorityId));
                lstParameter.Add(new MySqlParameter("U_Code", objBoardAuthority.Code));
                lstParameter.Add(new MySqlParameter("U_ContactEmail", objBoardAuthority.ContactEmail));
                lstParameter.Add(new MySqlParameter("U_ContactFax", objBoardAuthority.ContactFax));
                lstParameter.Add(new MySqlParameter("U_ContactPhone", objBoardAuthority.ContactPhone));
                lstParameter.Add(new MySqlParameter("U_CreatedBy", objBoardAuthority.CreatedBy));
                lstParameter.Add(new MySqlParameter("U_CreatedOn", objBoardAuthority.CreatedOn));
                lstParameter.Add(new MySqlParameter("U_IsActive", objBoardAuthority.IsActive));
                lstParameter.Add(new MySqlParameter("U_IsMailingSameasPhysical", objBoardAuthority.IsMailingSameasPhysical));
                lstParameter.Add(new MySqlParameter("U_MailingAddressCity", objBoardAuthority.MailingAddressCity));
                lstParameter.Add(new MySqlParameter("U_MailingAddressLine1", objBoardAuthority.MailingAddressLine1));
                lstParameter.Add(new MySqlParameter("U_MailingAddressLine2", objBoardAuthority.MailingAddressLine2));
                lstParameter.Add(new MySqlParameter("U_MailingAddressState", objBoardAuthority.MailingAddressState));
                lstParameter.Add(new MySqlParameter("U_MailingAddressZip", objBoardAuthority.MailingAddressZip));
                lstParameter.Add(new MySqlParameter("U_ModifiedBy", objBoardAuthority.ModifiedBy));
                lstParameter.Add(new MySqlParameter("U_ModifiedOn", objBoardAuthority.ModifiedOn));
                lstParameter.Add(new MySqlParameter("U_Name", objBoardAuthority.Name));
                lstParameter.Add(new MySqlParameter("U_PhysicalAddressCity", objBoardAuthority.PhysicalAddressCity));
                lstParameter.Add(new MySqlParameter("U_PhysicalAddressLine1", objBoardAuthority.PhysicalAddressLine1));
                lstParameter.Add(new MySqlParameter("U_PhysicalAddressLine2", objBoardAuthority.PhysicalAddressLine2));
                lstParameter.Add(new MySqlParameter("U_PhysicalAddressState", objBoardAuthority.PhysicalAddressState));

                lstParameter.Add(new MySqlParameter("U_PhysicalAddressZip", objBoardAuthority.PhysicalAddressZip));
                lstParameter.Add(new MySqlParameter("U_StateCode", objBoardAuthority.StateCode));
                lstParameter.Add(new MySqlParameter("U_SystemAbbreviation", objBoardAuthority.SystemAbbreviation));
                lstParameter.Add(new MySqlParameter("U_SystemContact", objBoardAuthority.SystemContact));
                lstParameter.Add(new MySqlParameter("U_SystemName", objBoardAuthority.SystemName));
                lstParameter.Add(new MySqlParameter("U_SystemUrl", objBoardAuthority.SystemUrl));
                lstParameter.Add(new MySqlParameter("U_Url", objBoardAuthority.Url));
                int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "BoardAuthority_Update", lstParameter.ToArray());
                return returnValue;
            }
        }

        public BoardAuthority Get_BoardAuthority_byID(int ID)
        {
            BoardAuthority objEntity = null;

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_BoardAuthorityId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "BoardAuthority_Get_ById", lstParameter.ToArray());

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
            }

            return objEntity;
        }

        public BoardAuthority Get_BoardAuthority_byID(int ID, bool Switch)
        {
            BoardAuthority objEntity = null;
            string type = Convert.ToString(ConfigurationManager.AppSettings["DatabaseType"]);
            if (type.ToUpper() == "MSSQL")
            {
                DBHelperMSSQL objDB = new DBHelperMSSQL();
                List<SqlParameter> lstParameter = new List<SqlParameter>();
                DataSet ds = new DataSet("DS");

                lstParameter.Add(new SqlParameter("BoardAuthorityId", ID));
                ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "BoardAuthority_Get_ById", lstParameter.ToArray());

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objEntity = FetchEntity(dr);
                }

            }
            else
            {
                DataSet ds = new DataSet("DS");
                DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
                lstParameter.Add(new MySqlParameter("G_BoardAuthorityId", ID));
                ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "BoardAuthority_Get_ById", lstParameter.ToArray());

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objEntity = FetchEntity(dr);
                }
            }
            return objEntity;
        }

        public List<BoardAuthority> Get_All_BoardAuthority()
        {

            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();

            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "BoardAuthority_Get_All");
            List<BoardAuthority> lstEntity = new List<BoardAuthority>();
            BoardAuthority objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public List<BoardAuthority> Get_All_BoardAuthority(bool Switch)
        {
            List<BoardAuthority> lstEntity = new List<BoardAuthority>();
            BoardAuthority objEntity = null;
            string type = Convert.ToString(ConfigurationManager.AppSettings["DatabaseType"]);
            if (type.ToUpper() == "MSSQL")
            {
                DataSet ds = new DataSet("DS");
                DBHelperMSSQL objDB = new DBHelperMSSQL();

                ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "BoardAuthority_Get_All");

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objEntity = FetchEntity(dr);
                    if (objEntity != null)
                        lstEntity.Add(objEntity);
                }

            }
            else
            {
                DataSet ds = new DataSet("DS");
                DBHelper objDB = new DBHelper();

                ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "BoardAuthority_Get_All");

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objEntity = FetchEntity(dr);
                    if (objEntity != null)
                        lstEntity.Add(objEntity);
                }
            }
            return lstEntity;
        }

        private BoardAuthority FetchEntity(DataRow dr)
        {
            BoardAuthority objEntity = new BoardAuthority();

            if (dr.Table.Columns.Contains("BoardAuthorityId") && dr["BoardAuthorityId"] != DBNull.Value)
            {
                objEntity.BoardAuthorityId = Convert.ToInt32(dr["BoardAuthorityId"]);
            }
            if (dr.Table.Columns.Contains("Acronym") && dr["Acronym"] != DBNull.Value)
            {
                objEntity.Acronym = Convert.ToString(dr["Acronym"]);
            }
            if (dr.Table.Columns.Contains("AlternatePhone") && dr["AlternatePhone"] != DBNull.Value)
            {
                objEntity.AlternatePhone = Convert.ToString(dr["AlternatePhone"]);
            }


            if (dr.Table.Columns.Contains("ApplicationSystemUrl") && dr["ApplicationSystemUrl"] != DBNull.Value)
            {
                objEntity.ApplicationSystemUrl = Convert.ToString(dr["ApplicationSystemUrl"]);
            }

            if (dr.Table.Columns.Contains("BoardAuthorityGuid") && dr["BoardAuthorityGuid"] != DBNull.Value)
            {
                objEntity.BoardAuthorityGuid = (Guid)(dr["BoardAuthorityGuid"]);
            }

            if (dr.Table.Columns.Contains("Code") && dr["Code"] != DBNull.Value)
            {
                objEntity.Code = Convert.ToString(dr["Code"]);
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
            if (dr.Table.Columns.Contains("CreatedBy") && dr["CreatedBy"] != DBNull.Value)
            {
                objEntity.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
            }
            if (dr.Table.Columns.Contains("CreatedOn") && dr["CreatedOn"] != DBNull.Value)
            {
                objEntity.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
            }
            if (dr.Table.Columns.Contains("IsActive") && dr["IsActive"] != DBNull.Value)
            {
                objEntity.IsActive = Convert.ToBoolean(dr["IsActive"]);
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
            if (dr.Table.Columns.Contains("ModifiedBy") && dr["ModifiedBy"] != DBNull.Value)
            {
                objEntity.ModifiedBy = Convert.ToInt32(dr["ModifiedBy"]);
            }
            if (dr.Table.Columns.Contains("ModifiedOn") && dr["ModifiedOn"] != DBNull.Value)
            {
                objEntity.ModifiedOn = Convert.ToDateTime(dr["ModifiedOn"]);
            }



            if (dr.Table.Columns.Contains("Name") && dr["Name"] != DBNull.Value)
            {
                objEntity.Name = Convert.ToString(dr["Name"]);
            }

            if (dr.Table.Columns.Contains("PhysicalAddressCity") && dr["PhysicalAddressCity"] != DBNull.Value)
            {
                objEntity.PhysicalAddressCity = Convert.ToString(dr["PhysicalAddressCity"]);
            }
            if (dr.Table.Columns.Contains("PhysicalAddressLine1") && dr["PhysicalAddressLine1"] != DBNull.Value)
            {
                objEntity.PhysicalAddressLine1 = Convert.ToString(dr["PhysicalAddressLine1"]);
            }

            if (dr.Table.Columns.Contains("PhysicalAddressLine2") && dr["PhysicalAddressLine2"] != DBNull.Value)
            {
                objEntity.PhysicalAddressLine2 = Convert.ToString(dr["PhysicalAddressLine2"]);
            }
            if (dr.Table.Columns.Contains("PhysicalAddressState") && dr["PhysicalAddressState"] != DBNull.Value)
            {
                objEntity.PhysicalAddressState = Convert.ToString(dr["PhysicalAddressState"]);
            }
            if (dr.Table.Columns.Contains("PhysicalAddressZip") && dr["PhysicalAddressZip"] != DBNull.Value)
            {
                objEntity.PhysicalAddressZip = Convert.ToString(dr["PhysicalAddressZip"]);
            }
            if (dr.Table.Columns.Contains("StateCode") && dr["StateCode"] != DBNull.Value)
            {
                objEntity.StateCode = Convert.ToString(dr["StateCode"]);
            }
            if (dr.Table.Columns.Contains("SystemAbbreviation") && dr["SystemAbbreviation"] != DBNull.Value)
            {
                objEntity.SystemAbbreviation = Convert.ToString(dr["SystemAbbreviation"]);
            }
            if (dr.Table.Columns.Contains("SystemContact") && dr["SystemContact"] != DBNull.Value)
            {
                objEntity.SystemContact = Convert.ToString(dr["SystemContact"]);
            }
            if (dr.Table.Columns.Contains("SystemName") && dr["SystemName"] != DBNull.Value)
            {
                objEntity.SystemName = Convert.ToString(dr["SystemName"]);
            }

            if (dr.Table.Columns.Contains("SystemUrl") && dr["SystemUrl"] != DBNull.Value)
            {
                objEntity.SystemUrl = Convert.ToString(dr["SystemUrl"]);
            }
            if (dr.Table.Columns.Contains("Url") && dr["Url"] != DBNull.Value)
            {
                objEntity.Url = Convert.ToString(dr["Url"]);
            }



            return objEntity;
        }


    }
}
