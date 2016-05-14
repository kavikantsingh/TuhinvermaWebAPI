using LAPP.DAL;
using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.BAL
{
    public class SourceBAL
    {
        SourceDAL objDAL = new SourceDAL();

        public int Save_Source(Source objSource)
        {
            return objDAL.Save_Source(objSource);
        }

        //public int Update_Source(Source objSource)
        //{
        //    return objDAL.Update_Source(objSource);
        //}

        public Source Get_Source_bySourceId(int ID)
        {
            return objDAL.Get_Source_bySourceId(ID);
        }

        public List<Source> Get_All_Source()
        {
            return objDAL.Get_All_Source();
        }

    }
}
