using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;
using LAPP.ENTITY;

namespace LAPP.DAL
{
    public class ProviderIndividualNameDAL : BaseDAL
    {
        public int Save_ProviderIndividualName(ProviderIndividualName objProviderIndName)
        {
            DBHelper objDB = new DBHelper(); List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            lstParameter.Add(new MySqlParameter("ProviderIndvNameInfoId", objProviderIndName.ProviderIndvNameInfoId));
            lstParameter.Add(new MySqlParameter("ProviderId", objProviderIndName.ProviderId));
            lstParameter.Add(new MySqlParameter("IndividualId", objProviderIndName.IndividualId));
            lstParameter.Add(new MySqlParameter("IndividualNameId", objProviderIndName.IndividualNameId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objProviderIndName.ApplicationId));
            lstParameter.Add(new MySqlParameter("ProvIndvJobTitle", objProviderIndName.ProvIndvJobTitle));
            lstParameter.Add(new MySqlParameter("ProvIndvJobTitleId", objProviderIndName.ProvIndvJobTitleId));
            lstParameter.Add(new MySqlParameter("DoyouSupervise", objProviderIndName.DoyouSupervise));
            lstParameter.Add(new MySqlParameter("BackgroundCheckRequired", objProviderIndName.BackgroundCheckRequired));
            lstParameter.Add(new MySqlParameter("IsLicensedorHasBeenLicensed", objProviderIndName.IsLicensedorHasBeenLicensed));
            lstParameter.Add(new MySqlParameter("ProvIndLicenseNumber", objProviderIndName.ProvIndLicenseNumber));
            lstParameter.Add(new MySqlParameter("ProvIndStateLicensed", objProviderIndName.ProvIndStateLicensed));
            lstParameter.Add(new MySqlParameter("ProvIndLicenseExpDate", objProviderIndName.ProvIndLicenseExpDate));
            lstParameter.Add(new MySqlParameter("AreyouSupervised", objProviderIndName.AreyouSupervised));
            lstParameter.Add(new MySqlParameter("NatureofInterest", objProviderIndName.NatureofInterest));
            lstParameter.Add(new MySqlParameter("ReferenceNumber", objProviderIndName.ReferenceNumber));
            lstParameter.Add(new MySqlParameter("IsActive", objProviderIndName.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objProviderIndName.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objProviderIndName.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objProviderIndName.CreatedOn));
            lstParameter.Add(new MySqlParameter("ModifiedBy", objProviderIndName.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objProviderIndName.ModifiedOn));
            lstParameter.Add(new MySqlParameter("ProviderIndvNameInfoGuid", objProviderIndName.ProviderIndvNameInfoGuid));
            lstParameter.Add(new MySqlParameter("EncryptionKey", EncryptionKey.Key));

            MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            lstParameter.Add(returnParam);
            objDB.ExecuteNonQuery(CommandType.StoredProcedure, "ProviderIndividualName_Save", true, lstParameter.ToArray());
            int returnValue = Convert.ToInt32(returnParam.Value);
            return returnValue;
        }

    }
}
