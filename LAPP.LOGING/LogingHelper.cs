using System;
using System.Web;
using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.LOGING.DAL;
using LAPP.LOGING.ENTITY;
using Newtonsoft.Json;
using System.Configuration;
using LAPP.GlobalFunctions;
using System.Linq;

namespace LAPP.LOGING
{
    public class LogingHelper
    {
        public static string AppDomainName = "";

        public static AuditvisitInfo SaveAuditInfo(string Key = "")
        {
            AuditvisitInfo objAuditInfo = new AuditvisitInfo();
            try
            {
                HttpBrowserCapabilities obj = HttpContext.Current.Request.Browser;
                objAuditInfo.HostIPAddress = HttpContext.Current.Request.UserHostAddress;
                objAuditInfo.PageName = HttpContext.Current.Request.Path;
                objAuditInfo.MachineDeviceName = HttpContext.Current.Server.MachineName;
                objAuditInfo.UserHostName = HttpContext.Current.Request.Url.Authority;
                objAuditInfo.RequestBrowserTypeVersion = obj != null ? obj.Browser + " " + obj.Version : string.Empty;
                objAuditInfo.UserHostAddress = HttpContext.Current.Request.Url.Host;
                objAuditInfo.IsActiveXControlEnabled = obj.ActiveXControls;
                objAuditInfo.IsCookieEnabled = obj.Cookies;
                objAuditInfo.IsCrawler = obj.Crawler;
                objAuditInfo.IsJavascriptEnabled = obj.JavaScript;
                objAuditInfo.RequestUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                objAuditInfo.Platform = Environment.OSVersion.VersionString;
                objAuditInfo.SessionID = "";
                objAuditInfo.TimeStamp = DateTime.Now;

                if (!string.IsNullOrEmpty(Key))
                {
                    objAuditInfo.UserId = GetTokenByKey(Key).UserId; 
                }
                else
                {
                    objAuditInfo.UserId = 0;
                }

                objAuditInfo.UserName = "";
                objAuditInfo.IndividualId = 0;
                objAuditInfo.EntityId = 0;
                objAuditInfo.DeviceId = "";
                objAuditInfo.AppDomainName = AppDomainName;// HttpContext.Current.Request.Url.OriginalString;
                objAuditInfo.RequestUrlReferrer = HttpContext.Current.Request.UrlReferrer != null ? HttpContext.Current.Request.UrlReferrer.AbsoluteUri : "";
                AuditVisitInfoDAL objDal = new AuditVisitInfoDAL();
                objAuditInfo.ID = objDal.Save_AuditvisitInfo(objAuditInfo);
            }
            catch (Exception ex) { }
            return objAuditInfo;
        }

        public static Log SaveExceptionInfo(string Key, Exception exception, string Title, eSeverity Severity, int Priority = -1)
        {
            Log objLog = new Log();
            LogDAL objDal = new LogDAL();
            try
            {

                HttpBrowserCapabilities obj = HttpContext.Current.Request.Browser;
                objLog.MachineName = HttpContext.Current.Server.MachineName;
                objLog.UserHostName = HttpContext.Current.Request.UserHostName;
                objLog.RequestBrowserTypeVersion = obj != null ? obj.Browser + " " + obj.Version : string.Empty;
                objLog.UserHostAddress = HttpContext.Current.Request.UserHostAddress;
                objLog.RequestUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                objLog.UserId = 0;
                objLog.UserName = "";
                objLog.IndividualId = 0;
                objLog.EntityId = 0;
                objLog.AppDomainName = HttpContext.Current.Request.Url.OriginalString;
                objLog.RequestUrlReferrer = HttpContext.Current.Request.UrlReferrer != null ? HttpContext.Current.Request.UrlReferrer.AbsoluteUri : "";
                objLog.Timestamp = DateTime.Now;
                objLog.Exception = exception.ToString();
                objLog.Application = "LAPP";
                objLog.AppDomainName = AppDomainName;
                objLog.CreatedOn = DateTime.Now;
                objLog.ElapsedMs = 0;
                objLog.UserAgent = HttpContext.Current.Request.UserAgent;
                objLog.Win32ThreadId = "";
                objLog.StackTrace = exception.StackTrace;
                objLog.Severity = Severity.ToString();
                objLog.Title = Title;
                objLog.ThreadName = "";
                objLog.Source = (eSource.WS).ToString();
                objLog.ProcessName = "";
                objLog.ProcessID = "";
                objLog.Priority = Priority;
                objLog.Message = exception.Message;
                objLog.IsDebug = false;
                objLog.FormattedMessage = "";
                objLog.EventID = 0;

                objLog.LogID = objDal.Save_Log(objLog);
            }
            catch (Exception ex)
            {
                try
                {
                    objLog = new Log();
                    objLog.MachineName = "";
                    objLog.UserHostName = "";
                    objLog.RequestBrowserTypeVersion = "";
                    objLog.UserHostAddress = "";
                    objLog.RequestUrl = "";
                    objLog.UserId = 0;
                    objLog.UserName = "";
                    objLog.IndividualId = 0;
                    objLog.EntityId = 0;
                    objLog.AppDomainName = "";
                    objLog.RequestUrlReferrer = "";
                    objLog.Timestamp = DateTime.Now;
                    objLog.Exception = ex.ToString();
                    objLog.Application = "LAPP";
                    objLog.AppDomainName = "";
                    objLog.CreatedOn = DateTime.Now;
                    objLog.ElapsedMs = 0;
                    objLog.UserAgent = "";
                    objLog.Win32ThreadId = "";
                    objLog.StackTrace = ex.StackTrace;
                    objLog.Severity = Severity.ToString();
                    objLog.Title = Title;
                    objLog.ThreadName = "";
                    objLog.Source = (eSource.WS).ToString();
                    objLog.ProcessName = "";
                    objLog.ProcessID = "";
                    objLog.Priority = Priority;
                    objLog.Message = ex.Message;
                    objLog.IsDebug = false;
                    objLog.FormattedMessage = "";
                    objLog.EventID = 0;

                    objLog.LogID = objDal.Save_Log(objLog);
                }
                catch(Exception ex1)
                {

                }
            }
            return objLog;
        }

        public static void SaveExceptionInfo(IndividualRenewalResponse objRenewalRequest)
        {
            Log objLog = new Log();
            try
            {

                HttpBrowserCapabilities obj = HttpContext.Current.Request.Browser;
                objLog.MachineName = HttpContext.Current.Server.MachineName;
                objLog.UserHostName = HttpContext.Current.Request.UserHostName;
                objLog.RequestBrowserTypeVersion = obj != null ? obj.Browser + " " + obj.Version : string.Empty;
                objLog.UserHostAddress = HttpContext.Current.Request.UserHostAddress;
                objLog.RequestUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                objLog.UserId = 0;
                objLog.UserName = "";
                objLog.IndividualId = objRenewalRequest.IndividualRenewal.Individual.IndividualId;
                objLog.EntityId = 0;
                objLog.AppDomainName = HttpContext.Current.Request.Url.OriginalString;
                objLog.RequestUrlReferrer = HttpContext.Current.Request.UrlReferrer != null ? HttpContext.Current.Request.UrlReferrer.AbsoluteUri : "";
                objLog.Timestamp = DateTime.Now;
                objLog.Exception = JsonConvert.SerializeObject(objRenewalRequest);
                objLog.Application = "LAPP";
                objLog.AppDomainName = AppDomainName;
                objLog.CreatedOn = DateTime.Now;
                objLog.ElapsedMs = 0;
                objLog.UserAgent = HttpContext.Current.Request.UserAgent;
                objLog.Win32ThreadId = "";
                objLog.StackTrace = "Renewal Request";
                objLog.Severity = "";
                objLog.Title = "Renewal Request";
                objLog.ThreadName = "";
                objLog.Source = (eSource.WS).ToString();
                objLog.ProcessName = "";
                objLog.ProcessID = "";
                objLog.Priority = 0;
                objLog.Message = "";
                objLog.IsDebug = false;
                objLog.FormattedMessage = "";
                objLog.EventID = 0;

                LogDAL objDal = new LogDAL();
                objLog.LogID = objDal.Save_Log(objLog);
            }
            catch (Exception ex)
            {


            }

        }

        public static void SaveRequestJson(string jsonText, string Title)
        {
            Log objLog = new Log();
            try
            {

                HttpBrowserCapabilities obj = HttpContext.Current.Request.Browser;
                objLog.MachineName = HttpContext.Current.Server.MachineName;
                objLog.UserHostName = HttpContext.Current.Request.UserHostName;
                objLog.RequestBrowserTypeVersion = obj != null ? obj.Browser + " " + obj.Version : string.Empty;
                objLog.UserHostAddress = HttpContext.Current.Request.UserHostAddress;
                objLog.RequestUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                objLog.UserId = 0;
                objLog.UserName = "";
                objLog.IndividualId = 0;
                objLog.EntityId = 0;
                objLog.AppDomainName = HttpContext.Current.Request.Url.OriginalString;
                objLog.RequestUrlReferrer = HttpContext.Current.Request.UrlReferrer != null ? HttpContext.Current.Request.UrlReferrer.AbsoluteUri : "";
                objLog.Timestamp = DateTime.Now;
                objLog.Exception = jsonText; // Newtonsoft.Json.JsonConvert.SerializeObject(objRenewalRequest);
                objLog.Application = "LAPP";
                objLog.AppDomainName = AppDomainName;
                objLog.CreatedOn = DateTime.Now;
                objLog.ElapsedMs = 0;
                objLog.UserAgent = HttpContext.Current.Request.UserAgent;
                objLog.Win32ThreadId = "";
                objLog.StackTrace = Title ;
                objLog.Severity = "";
                objLog.Title = Title ;
                objLog.ThreadName = "";
                objLog.Source = (eSource.WS).ToString();
                objLog.ProcessName = "";
                objLog.ProcessID = "";
                objLog.Priority = 0;
                objLog.Message = "";
                objLog.IsDebug = false;
                objLog.FormattedMessage = "";
                objLog.EventID = 0;

                LogDAL objDal = new LogDAL();
                objLog.LogID = objDal.Save_Log(objLog);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        private static Token GetTokenByKey(string Key)
        {
            
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
