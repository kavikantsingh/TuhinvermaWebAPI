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
    public class RevFeeCollectBAL : BaseBAL
    {
        RevFeeCollectDAL objDal = new RevFeeCollectDAL();

        public int Save_RevFeeCollect(RevFeeCollect objRevFeeCollect)
        {
            return objDal.Save_RevFeeCollect(objRevFeeCollect);
        }

        public List<RevFeeCollect> Get_All_RevFeeCollect()
        {
            return objDal.Get_All_RevFeeCollect();
        }

        public List<RevFeeCollect> Get_RevFeeCollect_by_IndividualId(int IndividualId)
        {
            return objDal.Get_RevFeeCollect_by_IndividualId(IndividualId);
        }

        public RevFeeCollect Get_RevFeeCollect_By_RevFeeCollectId(int ID)
        {
            return objDal.Get_RevFeeCollect_By_RevFeeCollectId(ID);
        }

    }
}
