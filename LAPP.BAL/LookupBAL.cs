using LAPP.DAL;
using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.BAL
{
    public class LookupBAL
    {
        LookupDAL objDAL = new LookupDAL();

        public int Save_Lookup(Lookup objLookup)
        {
            return objDAL.Save_Lookup(objLookup);
        }

        //public int Update_Lookup(Lookup objLookup)
        //{
        //    return objDAL.Update_Lookup(objLookup);
        //}

        public Lookup Get_Lookup_byLookupId(int ID)
        {
            return objDAL.Get_Lookup_byLookupId(ID);
        }
        public Lookup Get_Lookup_LookupTypeId(int ID)
        {
            return objDAL.Get_Lookup_LookupTypeId(ID);
        }
        public List<Lookup> Get_All_Lookup_LookupTypeId(int ID)
        {
            return objDAL.Get_All_Lookup_LookupTypeId(ID);
        }

        public List<Lookup> Get_All_Lookup()
        {
            return objDAL.Get_All_Lookup();
        }

    }
}
