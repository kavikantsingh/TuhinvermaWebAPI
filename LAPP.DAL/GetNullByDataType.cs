using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.DAL
{
    public class GetNullValue
    {
        public static string ByDataType(string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                return "";
            }
            else
            {
                return value;
            }
        }

        public static int? ByDataType(int? value)
        {
            if(value == 0)
            {
                return null;
            }
            else if (value == null)
            {
                return null;
            }
            else
            {
                return value;
            }
        }
    }
}
