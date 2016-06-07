using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class IndividualCertificationDAL : BaseDAL
    {
        public int Save_IndividualCertification(IndividualCertification objIndividualCertification)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("IndividualCertificationId", objIndividualCertification.IndividualCertificationId));
            lstParameter.Add(new MySqlParameter("IndividualId", objIndividualCertification.IndividualId));
            lstParameter.Add(new MySqlParameter("CertificationTypeId", objIndividualCertification.CertificationTypeId));
            lstParameter.Add(new MySqlParameter("ClinicalComptence", objIndividualCertification.ClinicalComptence));
            lstParameter.Add(new MySqlParameter("IsClinicalComptence", objIndividualCertification.IsClinicalComptence));
            lstParameter.Add(new MySqlParameter("DateIssued", objIndividualCertification.DateIssued));
            lstParameter.Add(new MySqlParameter("ABAMember", objIndividualCertification.ABAMember));
            lstParameter.Add(new MySqlParameter("PraxisExam", objIndividualCertification.PraxisExam));
            lstParameter.Add(new MySqlParameter("IsNBCHIS", objIndividualCertification.IsNBCHIS));
            lstParameter.Add(new MySqlParameter("NBCHISAccount", objIndividualCertification.NBCHISAccount));
            lstParameter.Add(new MySqlParameter("NBCHISCertificate", objIndividualCertification.NBCHISCertificate));

            lstParameter.Add(new MySqlParameter("DatePassed", objIndividualCertification.DatePassed));
            lstParameter.Add(new MySqlParameter("ABA", objIndividualCertification.ABA));
            lstParameter.Add(new MySqlParameter("ASHA", objIndividualCertification.ASHA));
            lstParameter.Add(new MySqlParameter("IsNBCOTCertified", objIndividualCertification.IsNBCOTCertified));
            lstParameter.Add(new MySqlParameter("IsNBCOTAppliedforRenewal", objIndividualCertification.IsNBCOTAppliedforRenewal));
            lstParameter.Add(new MySqlParameter("IsNBCOTExamScheduled", objIndividualCertification.IsNBCOTExamScheduled));
            lstParameter.Add(new MySqlParameter("NBCOTDateTaken", objIndividualCertification.NBCOTDateTaken));
            lstParameter.Add(new MySqlParameter("NBCOTDatePassed", objIndividualCertification.NBCOTDatePassed));
            lstParameter.Add(new MySqlParameter("NBCOTDateScheduled", objIndividualCertification.NBCOTDateScheduled));
            lstParameter.Add(new MySqlParameter("NoChanges", objIndividualCertification.NoChanges));


            lstParameter.Add(new MySqlParameter("IsActive", objIndividualCertification.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objIndividualCertification.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objIndividualCertification.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objIndividualCertification.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objIndividualCertification.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objIndividualCertification.ModifiedOn));

            lstParameter.Add(new MySqlParameter("IndividualCertificationGuid", objIndividualCertification.IndividualCertificationGuid));



            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "individualcertification_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }


        public List<IndividualCertification> Get_All_IndividualCertification()
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualCertification_Get_All");
            List<IndividualCertification> lstEntity = new List<IndividualCertification>();
            IndividualCertification objEntity = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objEntity = FetchEntity(dr);
                if (objEntity != null)
                    lstEntity.Add(objEntity);
            }
            return lstEntity;
        }

        public IndividualCertification Get_IndividualCertification_By_IndividualCertificationId(int ID)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualCertificationId", ID));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualCertification_GET_BY_IndividualCertificationId", lstParameter.ToArray());
            IndividualCertification objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }

        public IndividualCertification Get_IndividualCertification_By_IndividualId(int IndividualId)
        {
            DataSet ds = new DataSet("DS");
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("G_IndividualId", IndividualId));
            ds = objDB.ExecuteDataSet(CommandType.StoredProcedure, "IndividualCertification_GET_BY_IndividualId", lstParameter.ToArray());
            IndividualCertification objEntity = null;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                objEntity = FetchEntity(dr);
            }
            return objEntity;
        }


        private IndividualCertification FetchEntity(DataRow dr)
        {
            IndividualCertification objEntity = new IndividualCertification();

            if (dr.Table.Columns.Contains("IndividualCertificationId") && dr["IndividualCertificationId"] != DBNull.Value)
            {
                objEntity.IndividualCertificationId = Convert.ToInt32(dr["IndividualCertificationId"]);
            }
            if (dr.Table.Columns.Contains("IndividualId") && dr["IndividualId"] != DBNull.Value)
            {
                objEntity.IndividualId = Convert.ToInt32(dr["IndividualId"]);
            }
            if (dr.Table.Columns.Contains("CertificationTypeId") && dr["CertificationTypeId"] != DBNull.Value)
            {
                objEntity.CertificationTypeId = Convert.ToInt32(dr["CertificationTypeId"]);
            }
            if (dr.Table.Columns.Contains("ClinicalComptence") && dr["ClinicalComptence"] != DBNull.Value)
            {
                objEntity.ClinicalComptence = Convert.ToString(dr["ClinicalComptence"]);
            }
            if (dr.Table.Columns.Contains("IsClinicalComptence") && dr["IsClinicalComptence"] != DBNull.Value)
            {
                objEntity.IsClinicalComptence = Convert.ToBoolean(dr["IsClinicalComptence"]);
            }
            if (dr.Table.Columns.Contains("DateIssued") && dr["DateIssued"] != DBNull.Value)
            {
                objEntity.DateIssued = Convert.ToDateTime(dr["DateIssued"]);
            }
            if (dr.Table.Columns.Contains("ABAMember") && dr["ABAMember"] != DBNull.Value)
            {
                objEntity.ABAMember = Convert.ToString(dr["ABAMember"]);
            }
            if (dr.Table.Columns.Contains("PraxisExam") && dr["PraxisExam"] != DBNull.Value)
            {
                objEntity.PraxisExam = Convert.ToString(dr["PraxisExam"]);
            }
            if (dr.Table.Columns.Contains("IsNBCHIS") && dr["IsNBCHIS"] != DBNull.Value)
            {
                objEntity.IsNBCHIS = Convert.ToBoolean(dr["IsNBCHIS"]);
            }
            if (dr.Table.Columns.Contains("NBCHISAccount") && dr["NBCHISAccount"] != DBNull.Value)
            {
                objEntity.NBCHISAccount = Convert.ToString(dr["NBCHISAccount"]);
            }
            if (dr.Table.Columns.Contains("NBCHISCertificate") && dr["NBCHISCertificate"] != DBNull.Value)
            {
                objEntity.NBCHISCertificate = Convert.ToString(dr["NBCHISCertificate"]);
            }
            if (dr.Table.Columns.Contains("NoChanges") && dr["NoChanges"] != DBNull.Value)
            {
                objEntity.NoChanges = Convert.ToBoolean(dr["NoChanges"]);
            }

            if (dr.Table.Columns.Contains("DatePassed") && dr["DatePassed"] != DBNull.Value)
            {
                objEntity.DatePassed = Convert.ToDateTime(dr["DatePassed"]);
            }
            if (dr.Table.Columns.Contains("ABA") && dr["ABA"] != DBNull.Value)
            {
                objEntity.ABA = Convert.ToString(dr["ABA"]);
            }
            if (dr.Table.Columns.Contains("ASHA") && dr["ASHA"] != DBNull.Value)
            {
                objEntity.ASHA = Convert.ToString(dr["ASHA"]);
            }
            if (dr.Table.Columns.Contains("IsNBCOTCertified") && dr["IsNBCOTCertified"] != DBNull.Value)
            {
                objEntity.IsNBCOTCertified = Convert.ToBoolean(dr["IsNBCOTCertified"]);
            }
            if (dr.Table.Columns.Contains("IsNBCOTAppliedforRenewal") && dr["IsNBCOTAppliedforRenewal"] != DBNull.Value)
            {
                objEntity.IsNBCOTAppliedforRenewal = Convert.ToBoolean(dr["IsNBCOTAppliedforRenewal"]);
            }
            if (dr.Table.Columns.Contains("IsNBCOTExamScheduled") && dr["IsNBCOTExamScheduled"] != DBNull.Value)
            {
                objEntity.IsNBCOTExamScheduled = Convert.ToBoolean(dr["IsNBCOTExamScheduled"]);
            }
            if (dr.Table.Columns.Contains("NBCOTDateTaken") && dr["NBCOTDateTaken"] != DBNull.Value)
            {
                objEntity.NBCOTDateTaken = Convert.ToDateTime(dr["NBCOTDateTaken"]);
            }
            if (dr.Table.Columns.Contains("NBCOTDatePassed") && dr["NBCOTDatePassed"] != DBNull.Value)
            {
                objEntity.NBCOTDatePassed = Convert.ToDateTime(dr["NBCOTDatePassed"]);
            }
            if (dr.Table.Columns.Contains("NBCOTDateScheduled") && dr["NBCOTDateScheduled"] != DBNull.Value)
            {
                objEntity.NBCOTDateScheduled = Convert.ToDateTime(dr["NBCOTDateScheduled"]);
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


            if (dr.Table.Columns.Contains("IndividualCertificationGuid") && dr["IndividualCertificationGuid"] != DBNull.Value)
            {
                objEntity.IndividualCertificationGuid = Convert.ToString(dr["IndividualCertificationGuid"]);
            }

            return objEntity;

        }
    }
}
