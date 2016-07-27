using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAPP.DAL;
using LAPP.ENTITY;
namespace LAPP.BAL
{
    public class ConfigurationTypeBAL : BaseBAL

    {
        ConfigurationTypeDAL objDAL = new ConfigurationTypeDAL();

        public int Save_ConfigurationType(ConfigurationType objConfigurationType)
        {
            return objDAL.Save_ConfigurationType(objConfigurationType);
        }

        //public int Update_ConfigurationType(ConfigurationType objConfigurationType)
        //{
        //    return objDAL.Update_ConfigurationType(objConfigurationType);
        //}
        public List<ConfigurationType> Get_Configuration_By_Settings(string Setting)
        {
            return objDAL.Get_Configuration_By_Settings(Setting);
        }
        public ConfigurationType Get_ConfigurationType_Value_By_Setting(string Setting)
        {
            return objDAL.Get_ConfigurationType_Value_By_Setting(Setting);
        }
        public  ConfigurationType  Get_Configuration_By_Settings_object(string Setting)
        {
            return objDAL.Get_Configuration_By_Settings_object(Setting);
        }
        public List<ConfigurationType> GetAll_ConfigurationType()
        {
            return objDAL.GetAll_ConfigurationType();
        }

        public ConfigurationType Get_ConfigurationType_By_ConfigurationTypeId(int ConfigurationTypeId)
        {
            return objDAL.Get_ConfigurationType_By_ConfigurationTypeId(ConfigurationTypeId);
        }

    }
}
