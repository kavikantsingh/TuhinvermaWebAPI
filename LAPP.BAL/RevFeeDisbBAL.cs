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
    public class RevFeeDisbBAL : BaseBAL
    {
        RevFeeDisbDAL objDal = new RevFeeDisbDAL();

        public int Save_RevFeeDisb(RevFeeDisb objRevFeeDisb)
        {
            return objDal.Save_RevFeeDisb(objRevFeeDisb);
        }

        public List<RevFeeDisb> Get_All_RevFeeDisb()
        {
            return objDal.Get_All_RevFeeDisb();
        }

        public List<RevFeeDisb> Get_RevFeeDisb_by_IndividualId(int IndividualId)
        {
            return objDal.Get_RevFeeDisb_by_IndividualId(IndividualId);
        }

        public RevFeeDisb Get_RevFeeDisb_By_RevFeeDisbId(int ID)
        {
            return objDal.Get_RevFeeDisb_By_RevFeeDisbId(ID);
        }

        public List<RevFeeDisb> Get_RevFeeDisb_by_ApplicationId(int ApplicationId)
        {
            return objDal.Get_RevFeeDisb_by_ApplicationId(ApplicationId);
        }
    }
}
