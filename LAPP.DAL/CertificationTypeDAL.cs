using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class CertificationTypeDAL : BaseDAL
    {
        public int Save_CertificationType(CertificationType objCertificationType)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();

            lstParameter.Add(new MySqlParameter("CertificationTypeId", objCertificationType.CertificationTypeId));
            lstParameter.Add(new MySqlParameter("Name", objCertificationType.Name));
            lstParameter.Add(new MySqlParameter("Code", objCertificationType.Code));

            lstParameter.Add(new MySqlParameter("IsActive", objCertificationType.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objCertificationType.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objCertificationType.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objCertificationType.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objCertificationType.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objCertificationType.ModifiedOn));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "certificationtype_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

        public List<CertificationType> Get_All_CertificationType()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "ADDRESSTYPE_GET_ALL");
            List<CertificationType> lstEntity = new List<CertificationType>();
            CertificationType objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public CertificationType Get_CertificationType_byCertificationTypeId(int AddressId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParam = new List<MySqlParameter>();
            lstParam.Add(new MySqlParameter("G_CertificationTypeId", AddressId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "addresstype_Get_By_CertificationTypeId");

            CertificationType objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);

            }
            return objEntity;
        }

        private CertificationType FetchEntity(DataRow dr)
        {
            CertificationType objEntity = new CertificationType();


            if (dr.Table.Columns.Contains("CertificationTypeId") && dr["CertificationTypeId"] != DBNull.Value)
            {
                objEntity.CertificationTypeId = Convert.ToInt32(dr["CertificationTypeId"]);
            }
            if (dr.Table.Columns.Contains("Name") && dr["Name"] != DBNull.Value)
            {
                objEntity.Name = Convert.ToString(dr["Name"]);
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
