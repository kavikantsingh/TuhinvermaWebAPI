using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAPP.DAL;
using LAPP.ENTITY;
namespace LAPP.BAL
{
    public class ConfigurationBAL : BaseBAL

    {
        ConfigurationDAL objDAL = new ConfigurationDAL();

        public int Save_Configuration(Configuration objConfiguration)
        {
            return objDAL.Save_Configuration(objConfiguration);
        }

        //public int Update_Configuration(Configuration objConfiguration)
        //{
        //    return objDAL.Update_Configuration(objConfiguration);
        //}

        public List<Configuration> GetAll_Configuration()
        {
            return objDAL.GetAll_Configuration();
        }

        public Configuration Get_Configuration_By_ConfigurationId(int ConfigurationId)
        {
            return objDAL.Get_Configuration_By_ConfigurationId(ConfigurationId);
        }

        public Configuration Get_Configuration_By_ConfigurationTypeId(int ConfigurationTypeId)
        {
            return objDAL.Get_Configuration_By_ConfigurationTypeId(ConfigurationTypeId);
        }

        public List<Configuration> GetALL_Configuration_WithConfigurationType()
        {
            return objDAL.GetALL_Configuration_WithConfigurationType();
        }

        public List<LAPP_DeficiencyTemplate> Get_lapp_application_Deficiency_Template_By_Query_List(string Query)
        {
            return objDAL.Get_lapp_application_Deficiency_Template_By_Query_List(Query);
        }

        public List<MasterTransaction> Get_All_MasterTransaction()
        {
            return objDAL.Get_All_LAPP_MasterTransaction();
        }

        public int SaveDeficiencyTemplate(LAPP_DeficiencyTemplate objlapp_deficiency_template)
        {
            return objDAL.SaveDeficiencyTemplate(objlapp_deficiency_template);
        }
        public int UpdateDeficiencyTemplate(LAPP_DeficiencyTemplate objlapp_deficiency_template)
        {
            return objDAL.UpdateDeficiencyTemplate(objlapp_deficiency_template);
        }
        public LAPP_DeficiencyTemplate GetDeficiencyTemplate(int G_Deficiency_Template_ID)
        {
            return objDAL.GetDeficiencyTemplate(G_Deficiency_Template_ID);
        }
    }
}
