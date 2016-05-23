//using LAPP.ENTITY;
using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LAPP.WS.App_Helper.Common
{
    public class ConfigurationHelper
    {

        public static Configuration GetConfigurationObjectBySetting(string Setting)
        {
            Configuration objConfig = new Configuration();
            System.Web.HttpApplication objApplication = new HttpApplication();
            List<Configuration> lstConfiguration = (List<Configuration>)HttpContext.Current.Application["lstConfiguration"];
            if (lstConfiguration != null && lstConfiguration.Count > 0)
            {
                lstConfiguration = lstConfiguration.Where(x => x.Setting.ToLower() == Setting.ToLower()).ToList();
                if (lstConfiguration.Count > 0)
                {
                    objConfig = lstConfiguration[0];
                }

            }
            return objConfig;
        }
        public static string GetConfigurationValueBySetting(string Setting)
        {
            string value = "";// = new Configuration();

            List<Configuration> lstConfiguration = (List<Configuration>)HttpContext.Current.Application["lstConfiguration"];
            if (lstConfiguration != null && lstConfiguration.Count > 0)
            {
                lstConfiguration = lstConfiguration.Where(x => x.Setting.ToLower() == Setting.ToLower()).ToList();
                if (lstConfiguration.Count > 0)
                {
                    value = lstConfiguration[0].Value;
                }

            }
            return value;
        }

        public static List<Configuration> GetConfigurationListBySetting(string Setting)
        {
            Configuration objConfig = new Configuration();
         
            List<Configuration> lstConfiguration = (List<Configuration>)HttpContext.Current.Application["lstConfiguration"];
            if (lstConfiguration != null && lstConfiguration.Count > 0)
            {
                lstConfiguration = lstConfiguration.Where(x => x.Setting.ToLower() == Setting.ToLower()).ToList();


            }
            return lstConfiguration;
        }
    }
}