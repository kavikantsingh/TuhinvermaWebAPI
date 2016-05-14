using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAPP.ENTITY.Enumeration;
using LAPP.LOGING.ENTITY;
using LAPP.LOGING.DAL;
using System.Web;

namespace LAPP.WS.App_Helper
{
    public class LogingHelper
    {
        public static string AppDomainName = "";

        public static AuditvisitInfo SaveAuditInfo(string Key ="")
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
                objAuditInfo.Platform = System.Environment.OSVersion.VersionString;
                objAuditInfo.SessionID = "";
                objAuditInfo.TimeStamp = DateTime.Now;
                objAuditInfo.UserId = 0;
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

                LogDAL objDal = new LogDAL();
                objLog.LogID = objDal.Save_Log(objLog);
            }
            catch (Exception ex)
            {


            }
            return objLog;
        }
    }
}
