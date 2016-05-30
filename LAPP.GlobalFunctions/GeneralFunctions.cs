using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace LAPP.GlobalFunctions
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

        public static string GetRequestIPAddress()
        {

            try
            {
                HttpBrowserCapabilities obj = HttpContext.Current.Request.Browser;
                return HttpContext.Current.Request.UserHostAddress;
            }
            catch (Exception ex) { return ""; }
        }

        public static string GetTempPassword(int size=8)
        {
            const string str = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ@#$&*%";

            var rnd = new Random();
            return new string(Enumerable.Repeat(str, size)
       .Select(s => s[rnd.Next(s.Length)]).ToArray());

        }
    }
}
