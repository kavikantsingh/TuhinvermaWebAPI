using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LAPP.WS.App_Helper.Common
{
    public class GenralFuntions
    {
        public static string GetJsonStringFromList<T>(List<T> objList)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(objList);
        }
    }
}
