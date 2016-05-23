using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using LAPP.BAL;

namespace LAPP.WS
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected List<Configuration> lstConfiguration;
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
           // List<Configuration> lstConfiguration = new List<Configuration>();
            ConfigurationBAL objConfigBAL = new ConfigurationBAL();
            lstConfiguration = objConfigBAL.GetALL_Configuration_WithConfigurationType();
            Application["lstConfiguration"] = lstConfiguration;
            
            //if(lstConfiguration != null && lstConfiguration.Count > 0)
            //{
            //    foreach(Configuration obj in lstConfiguration)
            //    {
            //        Application[obj.Setting.ToLower()] = obj.Value;
            //    }
            //}
        }
    }
}
