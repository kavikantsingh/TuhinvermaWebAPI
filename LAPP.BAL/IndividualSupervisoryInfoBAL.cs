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
    public class IndividualSupervisoryInfoBAL : BaseBAL
    {
        IndividualSupervisoryInfoDAL objDal = new IndividualSupervisoryInfoDAL();
        public int Save_IndividualSupervisoryInfo(IndividualSupervisoryInfo objIndividualSupervisoryInfo)
        {
            return objDal.Save_IndividualSupervisoryInfo(objIndividualSupervisoryInfo);
        }

        public List<IndividualSupervisoryInfo> Get_All_IndividualSupervisoryInfo()
        {
            return objDal.Get_All_IndividualSupervisoryInfo();
        }
        public List<IndividualSupervisoryInfo> Get_IndividualSupervisoryInfo_By_IndividualId(int IndividualId)
        {
            return objDal.Get_IndividualSupervisoryInfo_By_IndividualId(IndividualId);
        }

        public IndividualSupervisoryInfo Get_IndividualSupervisoryInfo_By_IndividualSupervisoryInfoId(int ID)
        {
            return objDal.Get_IndividualSupervisoryInfo_By_IndividualSupervisoryInfoId(ID);
        }

        public List<IndividualSupervisoryInfo> Get_IndividualSupervisoryInfo_By_ApplicationId(int applicationId)
        {
            return objDal.Get_IndividualSupervisoryInfo_By_ApplicationId(applicationId);
        }
    }
}
