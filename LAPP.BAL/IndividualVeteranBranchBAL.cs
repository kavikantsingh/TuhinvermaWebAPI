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
    public class IndividualVeteranBranchBAL : BaseBAL
    {
        IndividualVeteranBranchDAL objDal = new IndividualVeteranBranchDAL();

        public int Save_IndividualVeteranBranch(IndividualVeteranBranch objaddress)
        {
            return objDal.Save_IndividualVeteranBranch(objaddress);
        }

        public List<IndividualVeteranBranch> Get_All_IndividualVeteranBranch()
        {
            return objDal.Get_All_IndividualVeteranBranch();
        }
        public List<IndividualVeteranBranch> Get_IndividualVeteranBranch_By_IndividualId_VeteranId(int IndividualId, int IndividualVeteranId)
        {
            return objDal.Get_IndividualVeteranBranch_By_IndividualId_VeteranId(IndividualId, IndividualVeteranId);
        }
        public IndividualVeteranBranch Get_address_By_IndividualVeteranBranchId(int ID)
        {
            return objDal.Get_IndividualVeteranBranch_By_IndividualVeteranBranchId(ID);
        }

    }
}
