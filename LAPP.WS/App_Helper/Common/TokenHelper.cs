using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAPP.ENTITY.Enumeration;
using LAPP.ENTITY;
using LAPP.LOGING.ENTITY;
using LAPP.LOGING.DAL;
using LAPP.BAL;
using System.Web;
using System.Configuration;
using LAPP.GlobalFunctions;

namespace LAPP.WS.App_Helper.Common
{
    public class TokenHelper
    {
        public static string GenrateToken(int UserID, String AppDomainName = "")
        {
            UserSessionBAL objUserSessionBAL = new UserSessionBAL();

            try
            {
                HttpBrowserCapabilities obj = HttpContext.Current.Request.Browser;

                UserSession objUserSession = new UserSession();
                objUserSession.UserID = UserID;
                objUserSession.SessionGUID = Guid.NewGuid().ToString();
                objUserSession.UserHostIPAddress = HttpContext.Current.Request.UserHostAddress;
                objUserSession.RequestBrowsertypeVersion = (obj != null ? obj.Browser + obj.Version : string.Empty).Trim();
                objUserSession.BrowserUniqueID = "";
                objUserSession.AppDomainName = AppDomainName;
                objUserSession.IssuedOn = DateTime.Now;
                objUserSession.ExpiredOn = DateTime.Now.AddMinutes(20);
                objUserSession.Key = "";
                objUserSession.TokenID = objUserSessionBAL.Save_UserSession(objUserSession);

                string tokenKey = objUserSession.TokenID + "|" + objUserSession.UserID + "|" + objUserSession.UserHostIPAddress + "|" + objUserSession.RequestBrowsertypeVersion;
                objUserSession.Key = Encryption.Base64Encrypt(tokenKey);

                objUserSessionBAL.Save_UserSession(objUserSession);

                return objUserSession.Key;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static void DestroyToken(long TokenId)
        {
            try
            {

                UserSessionBAL objUserSessionBAL = new UserSessionBAL();

                UserSession objUserSession = objUserSessionBAL.Get_UserSession_By_UserSessionId(TokenId);

                if (objUserSession != null)
                {
                    objUserSession.Expired = true;
                    objUserSession.ExpiredOn = DateTime.Now;
                    objUserSessionBAL.Save_UserSession(objUserSession);

                }

            }
            catch (Exception ex)
            {

            }

        }

        public static bool ValidateToken(string Key)
        {
            UserSessionBAL objUserSessionBAL = new UserSessionBAL();
            if (ConfigurationManager.AppSettings["DevelopmentMode"] == "true")
            {
                return true;
            }

            try
            {
                string decryptedStr = Encryption.Base64Decrypt(Key);
                string[] valueArray = decryptedStr.Split('|');

                if (valueArray != null && valueArray.Count() > 0)
                {
                    Int64 KeyTokenID = Convert.ToInt64(valueArray[0].ToString());
                    int KeyUserID = Convert.ToInt32(valueArray[1].ToString());
                    string KeyUserHostIPAddress = valueArray[2];
                    string KeyRequestBrowsertypeVersion = valueArray[3];

                    HttpBrowserCapabilities obj = HttpContext.Current.Request.Browser;
                    string BrowserUserHostIPAddress = HttpContext.Current.Request.UserHostAddress;
                    string BrowserRequestBrowsertypeVersion = (obj != null ? obj.Browser + obj.Version : string.Empty).Trim();

                    if (KeyUserHostIPAddress.Trim() == BrowserUserHostIPAddress.Trim() && KeyRequestBrowsertypeVersion.Trim() == BrowserRequestBrowsertypeVersion)
                    {
                        UserSession objUserSession = objUserSessionBAL.Get_UserSession_By_UserSessionId(KeyTokenID);

                        if (objUserSession != null && objUserSession.Expired == false)
                        {
                            if (DateTime.Now > objUserSession.ExpiredOn)
                            {
                                objUserSession.Expired = true;
                                objUserSessionBAL.Update_UserSession(objUserSession);
                                return false;

                            }
                            else
                            {
                                objUserSession.ExpiredOn = DateTime.Now.AddMinutes(30);
                                objUserSessionBAL.Update_UserSession(objUserSession);
                                return true;
                            }

                        }
                        else
                        {
                            return false;
                        }

                    }
                    else
                    {
                        UserSession objUserSession = objUserSessionBAL.Get_UserSession_By_UserSessionId(KeyTokenID);
                        if (objUserSession != null)
                        {
                            objUserSession.Expired = true;
                            objUserSessionBAL.Update_UserSession(objUserSession);
                            return false;
                        }
                        return false;

                    }

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static Token GetTokenByKey(string Key)
        {
            UserSessionBAL objUserSessionBAL = new UserSessionBAL();
            if (ConfigurationManager.AppSettings["DevelopmentMode"] == "true")
            {
                Token tc = new Token();
                tc.UserHostIPAdress = "27:0:0:1";
                tc.UserId = 0;
                tc.TokenId = 123;
                tc.RequestBrowsertypeVersion = "DevBrowser-4.0";
                return tc;
            }

            try
            {
                string decryptedStr = Encryption.Base64Decrypt(Key);
                string[] valueArray = decryptedStr.Split('|');

                if (valueArray != null && valueArray.Count() > 0)
                {
                    Int64 KeyTokenID = 0; int KeyUserID = 0; string KeyUserHostIPAddress = ""; string KeyRequestBrowsertypeVersion = "";
                    if (valueArray[0] != null)
                    {
                        KeyTokenID = Convert.ToInt64(valueArray[0].ToString());
                    }
                    if (valueArray[1] != null)
                    {
                        KeyUserID = Convert.ToInt32(valueArray[1].ToString());
                    }
                    if (valueArray[2] != null)
                    {
                        KeyUserHostIPAddress = valueArray[2];
                    }
                    if (valueArray[3] != null)
                    {
                        KeyRequestBrowsertypeVersion = valueArray[3];
                    }

                    Token tc = new Token();
                    tc.UserHostIPAdress = KeyUserHostIPAddress;
                    tc.UserId = KeyUserID;
                    tc.TokenId = KeyTokenID;
                    tc.RequestBrowsertypeVersion = KeyRequestBrowsertypeVersion;
                    return tc;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
    }
}

