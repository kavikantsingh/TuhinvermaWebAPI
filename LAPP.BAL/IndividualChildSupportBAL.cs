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
    public class IndividualChildSupportBAL : BaseBAL
    {
        IndividualChildSupportDAL objDal = new IndividualChildSupportDAL();

        public int Save_IndividualChildSupport(IndividualChildSupport objaddress)
        {
            return objDal.Save_IndividualChildSupport(objaddress);
        }

        public List<IndividualChildSupport> Get_All_IndividualChildSupport()
        {
            return objDal.Get_All_IndividualChildSupport();
        }

        public IndividualChildSupport Get_address_By_IndividualChildSupportId(int ID)
        {
            return objDal.Get_IndividualChildSupport_By_IndividualChildSupportId(ID);
        }

        public List<IndividualChildSupport> Get_IndividualChildSupport_By_IndividualId(int IndividualId)
        {
            return objDal.Get_IndividualChildSupport_By_IndividualId(IndividualId);
        }
    }
}
