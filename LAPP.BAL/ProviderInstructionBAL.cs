using LAPP.DAL;
using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.BAL
{
    public class ProviderInstructionsBAL
    {
        ProviderInstructionsDAL objDAL = new ProviderInstructionsDAL();

        public int SaveButtonOfInstructions(ProviderInstructions objProviderInstructions)
        {
            return objDAL.SaveButtonOfInstructions(objProviderInstructions);
        }

        public int CheckInitialTabActive(int applicationId, int providerId)
        {
            return objDAL.CheckInitialTabActive(applicationId, providerId);
        }

        ProviderNameDAL objProviderNameDAL = new ProviderNameDAL();

        public int SavePreviousSchoolDetails(ProviderNames objProviderInstructions)
        {
            return objProviderNameDAL.SavePreviousSchoolDetails(objProviderInstructions);
        }

        public int DeletePreviousSchoolDetails(ProviderNames objProviderInstructions)
        {
            return objProviderNameDAL.DeletePreviousSchoolDetails(objProviderInstructions);
        }

        public List<ProviderNames> GetAllPreviousSchools(int applicationId,int providerid)
        {
            return objProviderNameDAL.GetAllPreviousSchools(applicationId, providerid);
        }


    }
}
