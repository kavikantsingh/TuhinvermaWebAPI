using LAPP.DAL;
using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.BAL
{
    public class ProviderProgramBAL : BaseBAL
    {

        ProviderProgramDAL objDal = new ProviderProgramDAL();
        public int Save_ProviderProgram(ProviderProgram objProviderProgram)
        {
            return objDal.Save_ProviderProgram(objProviderProgram);
        }

        public List<ProviderProgram> Get_All_ProviderProgram(ProviderProgram objProviderProgram)
        {
            return objDal.Get_All_ProviderProgram(objProviderProgram);
        }


        public int DeleteProviderProgram(ProviderProgram objProviderProgram)
        {
            return objDal.DeleteProviderProgram(objProviderProgram);
        }


    }
}
