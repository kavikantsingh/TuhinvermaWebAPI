using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace LAPP.GlobalFunctions
{
    public class Encryption
    {
        private static string EncryptionKey
        {
            get
            {
                
                return "La(%#^*1@#&*_+w$rasdfs";
            }
        }
        private class Encryption64
        {
            static private byte[] key = {

    };
            static private byte[] IV = {
        0x12,
        0x34,
        0x56,
        0x78,
        0x90,
        0xab,
        0xcd,
        0xef

    };
            public static string DecryptString(string stringToDecrypt, string sEncryptionKey)
            {
                byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
                try
                {
                    //key = System.Text.Encoding.UTF8.GetBytes(Strings.Left(sEncryptionKey, 8));
                    key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    inputByteArray = Convert.FromBase64String(stringToDecrypt);
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                    return encoding.GetString(ms.ToArray());



                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
            public static string EncryptString(string stringToEncrypt, string SEncryptionKey)
            {
                try
                {
                    //key = System.Text.Encoding.UTF8.GetBytes(Strings.Left(SEncryptionKey, 8));
                    key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey.Substring(0, 8));
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }


        }
        private static string EncryptString(string input)
        {
            return Encryption64.EncryptString(input, EncryptionKey);

        }
        private static string DecryptString(string input)
        {
            return Encryption64.DecryptString(input, EncryptionKey);
        }



        ///<summary>
        /// Base 64 Encoding with URL and Filename Safe Alphabet using UTF-8 character set.
        ///</summary>
        ///<param name="str">The origianl string</param>
        ///<returns>The Base64 encoded string</returns>
        private static string Base64ForUrlEncode(string str)
        {
            str = EncryptString(str);
            byte[] encbuff = Encoding.UTF8.GetBytes(str);
            return HttpServerUtility.UrlTokenEncode(encbuff);
        }
        ///<summary>
        /// Decode Base64 encoded string with URL and Filename Safe Alphabet using UTF-8.
        ///</summary>
        ///<param name="str">Base64 code</param>
        ///<returns>The decoded string.</returns>
        private static string Base64ForUrlDecode(string str)
        {
            byte[] decbuff = HttpServerUtility.UrlTokenDecode(str);
            str = Encoding.UTF8.GetString(decbuff);
            str = DecryptString(str);

            return str;
        }


        ///<summary>
        /// Base 64 Encoding with URL and Filename Safe Alphabet using UTF-8 character set.
        ///</summary>
        ///<param name="str">The origianl string</param>
        ///<returns>The Base64 encoded string</returns>
        public static string Base64Encrypt(string str)
        {
            str = EncryptString(str);
            byte[] encbuff = Encoding.UTF8.GetBytes(str);
            return HttpServerUtility.UrlTokenEncode(encbuff);
        }
        ///<summary>
        /// Decode Base64 encoded string with URL and Filename Safe Alphabet using UTF-8.
        ///</summary>
        ///<param name="str">Base64 code</param>
        ///<returns>The decoded string.</returns>
        public static string Base64Decrypt(string str)
        {
            byte[] decbuff = HttpServerUtility.UrlTokenDecode(str);
            str = Encoding.UTF8.GetString(decbuff);
            str = DecryptString(str);

            return str;
        }

    }
}