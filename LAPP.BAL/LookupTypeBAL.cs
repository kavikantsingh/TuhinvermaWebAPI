using LAPP.DAL;
using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.BAL
{
    public class LookupTypeBAL
    {
        LookupTypeDAL objDAL = new LookupTypeDAL();

        public int Save_LookupType(LookupType objLookupType)
        {
            return objDAL.Save_LookupType(objLookupType);
        }

        //public int Update_LookupType(LookupType objLookupType)
        //{
        //    return objDAL.Update_LookupType(objLookupType);
        //}

        public LookupType Get_LookupType_byLookupTypeId(int ID)
        {
            return objDAL.Get_LookupType_byLookupTypeId(ID);
        }

        public List<LookupType> Get_All_LookupType()
        {
            return objDAL.Get_All_LookupType();
        }

    }
}
