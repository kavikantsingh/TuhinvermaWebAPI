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
    public class IndividualLegalBAL : BaseBAL
    {
        IndividualLegalDAL objDal = new IndividualLegalDAL();

        public int Save_IndividualLegal(IndividualLegal objaddress)
        {
            return objDal.Save_IndividualLegal(objaddress);
        }

        public List<IndividualLegal> Get_All_IndividualLegal()
        {
            return objDal.Get_All_IndividualLegal();
        }
        public List<IndividualLegal> Get_IndividualLegal_By_IndividualId(int IndividualId)
        {
            return objDal.Get_IndividualLegal_By_IndividualId(IndividualId);
        }
        public IndividualLegal Get_address_By_IndividualLegalId(int ID)
        {
            return objDal.Get_IndividualLegal_By_IndividualLegalId(ID);
        }

    }
}
