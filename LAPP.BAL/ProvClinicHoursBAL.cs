using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using LAPP.ENTITY;
using LAPP.DAL;

namespace LAPP.BAL
{
    public class ProvClinicHoursBAL
    {
        ProvClinicHoursDAL objDAL = new ProvClinicHoursDAL();
        public int Save_ProvClinicHours(ProvClinicHours objProvClinicHours)
        {
            return objDAL.Save_ProvClinicHours(objProvClinicHours);
        }

        public ProvClinicHours Get_ProvClinicHours(int ProviderId)
        {
            return objDAL.Get_ProvClinicHours(ProviderId);
        }
    }

}
