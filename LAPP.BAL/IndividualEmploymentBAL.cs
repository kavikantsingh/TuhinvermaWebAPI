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
    public class IndividualEmploymentBAL : BaseBAL
    {
        IndividualEmploymentDAL objDal = new IndividualEmploymentDAL();
        public int Save_IndividualEmployment(IndividualEmployment objIndividualEmployment)
        {
            return objDal.Save_IndividualEmployment(objIndividualEmployment);
        }

        public List<IndividualEmployment> Get_All_IndividualEmployment()
        {
            return objDal.Get_All_IndividualEmployment();
        }
        public List<IndividualEmployment> Get_IndividualEmployment_by_IndividualId(int IndividualId)
        {
            return objDal.Get_IndividualEmployment_by_IndividualId(IndividualId);
        }
        public List<IndividualEmployment> Get_IndividualEmployment_by_ApplicationId(int ApplicationId)
        {
            return objDal.Get_IndividualEmployment_by_ApplicationId(ApplicationId);
        }
        public IndividualEmployment Get_IndividualEmployment_By_IndividualEmploymentId(int ID)
        {
            return objDal.Get_IndividualEmployment_By_IndividualEmploymentId(ID);
        }
        public void IndividualEmployment_SoftDelete_by_ApplicationId(int _ApplicationId)
        {
            objDal.IndividualEmployment_SoftDelete_by_ApplicationId(_ApplicationId);
        }
    }
}
