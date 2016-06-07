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
    public class IndividualRenewalBAL : BaseBAL
    {
        IndividualRenewalDAL objDal = new IndividualRenewalDAL();

        public List<RenewalGet> Get_All_Renewal()
        {
            return objDal.Get_All_Renewal();
        }
        public List<RenewalGet> Search_RenewalWithPager(RenewalApplication obj, int CurrentPage, int PagerSize)
        {
            return objDal.Search_RenewalWithPager(obj, CurrentPage, PagerSize);
        }
        public List<RenewalGet> GetALL_RenewalWithPager(int CurrentPage, int PagerSize)
        {
            return objDal.GetALL_RenewalWithPager(CurrentPage, PagerSize);
        }
    }
}
