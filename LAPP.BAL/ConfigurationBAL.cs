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

    }
}
