using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using LAPP.ENTITY;
namespace LAPP.DAL
{
    public class ProvClinicHoursDAL
    {
        public int Save_ProvClinicHours(ProvClinicHours objProvClinicHours)
        {
            DBHelper objDB = new DBHelper();
            List<MySqlParameter> lstParameter = new List<MySqlParameter>();
            //lstParameter.Add(new MySqlParameter("ProvClinicHoursId", objProvClinicHours.ProvClinicHoursId));
            lstParameter.Add(new MySqlParameter("ProviderId", objProvClinicHours.ProviderId));
            lstParameter.Add(new MySqlParameter("ApplicationId", objProvClinicHours.ApplicationId));
            lstParameter.Add(new MySqlParameter("ClinicHours", objProvClinicHours.ClinicHours));
            lstParameter.Add(new MySqlParameter("ReferenceNumber", objProvClinicHours.ReferenceNumber));
            lstParameter.Add(new MySqlParameter("IsActive", objProvClinicHours.IsActive));
            lstParameter.Add(new MySqlParameter("IsDeleted", objProvClinicHours.IsDeleted));
            lstParameter.Add(new MySqlParameter("CreatedBy", objProvClinicHours.CreatedBy));
            lstParameter.Add(new MySqlParameter("CreatedOn", objProvClinicHours.CreatedOn));

            lstParameter.Add(new MySqlParameter("ModifiedBy", objProvClinicHours.ModifiedBy));
            lstParameter.Add(new MySqlParameter("ModifiedOn", objProvClinicHours.ModifiedOn));
            lstParameter.Add(new MySqlParameter("ProvClinicHoursGuid", objProvClinicHours.ProvClinicHoursGuid));

            //MySqlParameter returnParam = new MySqlParameter("ReturnParam", SqlDbType.Int);
            //returnParam.Direction = ParameterDirection.ReturnValue;
            //lstParameter.Add(returnParam);
            int returnValue = objDB.ExecuteNonQuery(CommandType.StoredProcedure, "ProvClinicHours_Save", lstParameter.ToArray());
            return returnValue;
        }
    }
}
