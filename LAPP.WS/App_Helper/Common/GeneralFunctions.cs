using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace LAPP.WS.App_Helper.Common
{
    public class GeneralFunctions
    {
        /// <summary>
        ///  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static string GetJsonStringFromList<T>(List<T> objList)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(objList);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string GetSplitedCamelCase(string Value)
        {
            string result;
            result = Regex.Replace(Value, "([A-Z])", " $1").Trim();
            return result;
        }


    }
}
