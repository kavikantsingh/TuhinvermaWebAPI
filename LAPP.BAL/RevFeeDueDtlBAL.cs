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
    public class RevFeeDueDtlBAL : BaseBAL
    {
        RevFeeDueDtlDAL objDal = new RevFeeDueDtlDAL();

        public int Save_RevFeeDueDtl(RevFeeDueDtl objRevFeeDueDtl)
        {
            return objDal.Save_RevFeeDueDtl(objRevFeeDueDtl);
        }

        public List<RevFeeDueDtl> Get_All_RevFeeDueDtl()
        {
            return objDal.Get_All_RevFeeDueDtl();
        }

        public List<RevFeeDueDtl> Get_RevFeeDueDtl_by_IndividualId(int IndividualId)
        {
            return objDal.Get_RevFeeDueDtl_by_IndividualId(IndividualId);
        }

        public RevFeeDueDtl Get_RevFeeDueDtl_By_RevFeeDueDtlId(int ID)
        {
            return objDal.Get_RevFeeDueDtl_By_RevFeeDueDtlId(ID);
        }

    }
}
