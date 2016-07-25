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
    public class ProviderBAL : BaseBAL
    {
        ProviderDAL objDal = new ProviderDAL();
        public int Save_Provider(Provider objProvider)
        {
            return objDal.Save_Provider(objProvider);
        }

        public List<Provider> Get_All_Provider()
        {
            return objDal.Get_All_Provider();
        }


        public Provider Get_Provider_By_ProviderId(int ID)
        {
            return objDal.Get_Provider_By_ProviderId(ID);
        }


        public int SaveSchoolInformation(ProviderInformation objProvider)
        {
            return objDal.SaveSchoolInformation(objProvider);
        }

        #region Shekhar 

        public int SaveProviderStaff(ProviderStaff objProviderStaff)
        {
            return objDal.SaveProviderStaff(objProviderStaff);
        }

        public int SaveProvIndvNameTitle(ProvIndvNameTitle objProviderIndName)
        {
            return objDal.SaveProvIndvNameTitle(objProviderIndName);
        }

        public List<ProviderStaff> GetAllProviderStaffDetails(int ApplicationId, int ProviderId)
        {
            return objDal.GetAllProviderStaffDetails(ApplicationId, ProviderId);
        }

        public int SaveProviderOtherProgram(ProviderOtherProgramName objProviderOtherProgram)
        {
            return objDal.SaveProviderOtherProgram(objProviderOtherProgram);
        }

        public List<ProviderOtherProgramName> GetAllProviderOtherProgram(int ApplicationId, int ProviderId)
        {
            return objDal.GetAllProviderOtherProgram(ApplicationId, ProviderId);
        }


        public int SaveProviderGraduatesNumber(ProviderGraduatesNumber objProviderGraduatesNumber)
        {
            return objDal.SaveProviderGraduatesNumber(objProviderGraduatesNumber);
        }

        public List<ProviderGraduatesNumber> GetAllProviderGraduatesNumber(int ApplicationId, int ProviderId)
        {
            return objDal.GetAllProviderGraduatesNumber(ApplicationId, ProviderId);
        }

        public int SaveProviderTabStatus(ProviderTabStatus ObjProviderTabStatus)
        {
            return objDal.SaveProviderTabStatus(ObjProviderTabStatus);
        }

        public List<ProviderTabStatusGetResponse> GetAllProviderTabStatus(int ApplicationId, int ProviderId)
        {
            return objDal.GetAllProviderTabStatus(ApplicationId, ProviderId);
        }

        public int SaveProviderBusinessType(ProviderBusinessType objProviderBusinessType)
        {
            return objDal.SaveProviderBusinessType(objProviderBusinessType);
        }

        public List<ProviderBusinessType> GetProviderBusinessTypeByProviderId(int ApplicationId, int ProviderId)
        {
            return objDal.GetProviderBusinessTypeByProviderId(ApplicationId, ProviderId);
        }

        public int SaveProviderNames(ProviderNames objProvider)
        {
            return objDal.SaveProviderNames(objProvider);
        }

        #endregion

    }
}

