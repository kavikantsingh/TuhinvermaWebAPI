using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.DAL
{
    public class EncryptionKey : BaseDAL
    {
        public static string Key = "test%%##abc@123";
        public static string GetSalt(int size=10)
        {
            const string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";

            var rnd = new Random();
            return new string(Enumerable.Repeat(str, size)
       .Select(s => s[rnd.Next(s.Length)]).ToArray());

        }
    }
}
