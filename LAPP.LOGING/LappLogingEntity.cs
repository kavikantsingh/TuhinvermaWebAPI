using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LAPP.LOGING.ENTITY
{
    class LappLogingEntity
    {
    }


    #region CategoryEntity

    public class Category
    {
        [Display(Description = "Required: No, Desc: Auto Generate")]
        public int CategoryID { get; set; }

        [Display(Description = "Required: Yes, Max Length: 64 (string)")]
        public string CategoryName { get; set; }
    }

    public class CategoryResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string StatusCode { get; set; }
        public string ResponseReason { get; set; }
        public List<Category> Category { get; set; }
    }

    #endregion

    #region CategoryLogEntity

    public class CategoryLog
    {
        [Display(Description = "Required: No, Desc: Auto Generate")]
        public int CategoryLogID { get; set; }

        [Display(Description = "Required:Yes, Max Length:11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int CategoryID { get; set; }

        [Display(Description = "Required:Yes, Max Length:11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int LogID { get; set; }
    }

    public class CategoryLogResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string StatusCode { get; set; }
        public string ResponseReason { get; set; }
        public List<CategoryLog> categorylog { get; set; }
    }

    #endregion

    #region DataLogEntity

    public class Datalog
    {
        [Display(Description = "Required: No, Desc: Auto Generate")]
        public int DataLogId { get; set; }

        [Display(Description = "Required: Yes, Max Length: 50 (string)")]
        public string TableName { get; set; }

        [Display(Description = "Required: Yes,  Max Length: 11 (Integer), For example: Numeric vlaue (0-9)")]
        public int RowIdValue { get; set; }

        [Display(Description = "Required: Yes, Max Length: 6 (string)")]
        public string Action { get; set; }

        [Display(Description = "Required: No, Max Length: 50 (string)")]
        public string ColumnName { get; set; }

        [Display(Description = "Required: No, Max Length: Max (Text)")]
        public string BeforeValue { get; set; }

        [Display(Description = "Required: No, Max Length: Max (Text)")]
        public string AfterValue { get; set; }

        [Display(Description = "Required:Yes, For example: true or false (0,1) ")]
        public bool IsSystem { get; set; }

        [Display(Description = "Required: No, Max Length: 128 (string)")]
        public string UserName { get; set; }

        [Display(Description = "Required: Yes, For example: mm/dd/yyyy  ")]
        public DateTime LogDateTime { get; set; }
    }

    public class DatalogResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string StatusCode { get; set; }
        public string ResponseReason { get; set; }
        public List<Datalog> Datalog { get; set; }
    }

    #endregion

    #region LogEntity

    public class Log
    {
        [Display(Description = "Required: No, Desc: Auto Generate")]
        public int LogID { get; set; }

        [Display(Description = "Required: Yes, Max Length: 2 (Char)")]
        public string Source { get; set; }

        [Display(Description = "Required: No, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int EventID { get; set; }

        [Display(Description = "Required: No, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int IndividualId { get; set; }

        [Display(Description = "Required: No, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int EntityId { get; set; }

        [Display(Description = "Required: No, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int UserId { get; set; }

        [Display(Description = "Required: No, Max Length: 128 (String)")]
        public string UserName { get; set; }

        [Display(Description = "Required: Yes, Max Length: 11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int Priority { get; set; }

        [Display(Description = "Required: Yes, Max Length: 32 (String)")]
        public string Severity { get; set; }

        [Display(Description = "Required: Yes, Max Length: 256 (String)")]
        public string Title { get; set; }

        [Display(Description = "Required: Yes, For example: mm/dd/yyyy,  ")]
        public DateTime Timestamp { get; set; }

        [Display(Description = "Required: Yes, Max Length: 32 (String)")]
        public string MachineName { get; set; }

        [Display(Description = "Required: Yes, Max Length: 512 (String)")]
        public string AppDomainName { get; set; }

        [Display(Description = "Required: No, Max Length: 32 (String)")]
        public string Application { get; set; }

        [Display(Description = "Required: Yes, Max Length: 256 (String)")]
        public string ProcessID { get; set; }

        [Display(Description = "Required: Yes, Max Length: 512 (String)")]
        public string ProcessName { get; set; }

        [Display(Description = "Required: No, Max Length: 512 (String)")]
        public string ThreadName { get; set; }

        [Display(Description = "Required: No, Max Length: 128 (String)")]
        public string Win32ThreadId { get; set; }

        [Display(Description = "Required: No, Max Length: 128 (String)")]
        public string RequestUrl { get; set; }

        [Display(Description = "Required: No, Max Length: 128 (String)")]
        public string RequestUrlReferrer { get; set; }

        [Display(Description = "Required: No, Max Length: 256 (String)")]
        public string RequestBrowserTypeVersion { get; set; }

        [Display(Description = "Required: No, Max Length: 256 (String)")]
        public string UserAgent { get; set; }

        [Display(Description = "Required: No, Max Length: 256 (String)")]
        public string UserHostAddress { get; set; }

        [Display(Description = "Required: No, Max Length: 256 (String)")]
        public string UserHostName { get; set; }

        [Display(Description = "Required: No, Max Length: Max (Text)")]
        public string Message { get; set; }

        [Display(Description = "Required: No, Max Length: Max (Text)")]
        public string FormattedMessage { get; set; }

        [Display(Description = "Required: No, Max Length: Max (Text)")]
        public string StackTrace { get; set; }

        [Display(Description = "Required: No, Max Length: Max (Text)")]
        public string Exception { get; set; }

        [Display(Description = "Required: No, For example: Integer (float)")]
        public float ElapsedMs { get; set; }

        [Display(Description = "Required: No, Max Length: 40 (String)")]
        public string SessionId { get; set; }

        [Display(Description = "Required: Yes, For example: true or false (0,1) ")]
        public bool IsDebug { get; set; }

        [Display(Description = "Required: Yes, For example: mm/dd/yyyy,  ")]
        public DateTime CreatedOn { get; set; }
    }

    public class LogResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string StatusCode { get; set; }
        public string ResponseReason { get; set; }
        public List<Log> Log { get; set; }
    }

    #endregion

    #region AuditvisitInfoEntity

    public class AuditvisitInfo
    {
        [Display(Description = "Required: No, Desc: Auto Generate")]
        public int ID { get; set; }

        [Display(Description = "Required: No, Max Length: 50 (string)")]
        public string HostIPAddress { get; set; }

        [Display(Description = "Required: No, Max Length: 500 (string)")]
        public string PageName { get; set; }

        [Display(Description = "Required: No, Max Length: 200 (string)")]
        public string RequestUrl { get; set; }

        [Display(Description = "Required: No, Max Length: 200 (string)")]
        public string RequestUrlReferrer { get; set; }

        [Display(Description = "Required: No, Max Length: 256 (string)")]
        public string RequestBrowserTypeVersion { get; set; }

        [Display(Description = "Required: No, Max Length: 100 (string)")]
        public string SessionID { get; set; }

        [Display(Description = "Required: No, For example: mm/dd/yyyy  ")]
        public DateTime TimeStamp { get; set; }

        [Display(Description = "Required: No, Max Length:11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int IndividualId { get; set; }

        [Display(Description = "Required: No, Max Length:11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int EntityId { get; set; }

        [Display(Description = "Required: No, Max Length:11 (Integer), For example: Numeric vlaue (0-9) ")]
        public int UserId { get; set; }

        [Display(Description = "Required: No, Max Length: 128 (string)")]
        public string UserName { get; set; }

        [Display(Description = "Required: No, Max Length: 50 (string)")]
        public string Platform { get; set; }

        [Display(Description = "Required: No, Max Length: 32 (string)")]
        public string MachineDeviceName { get; set; }

        [Display(Description = "Required: No, Max Length: 100 (string)")]
        public string DeviceId { get; set; }

        [Display(Description = "Required: No, Max Length: 512 (string)")]
        public string AppDomainName { get; set; }

        [Display(Description = "Required: No, Max Length: 256 (string)")]
        public string UserHostAddress { get; set; }

        [Display(Description = "Required: No, Max Length: 256 (string)")]
        public string UserHostName { get; set; }

        [Display(Description = "Required: No, For example: true or false (0,1) ")]
        public bool IsJavascriptEnabled { get; set; }

        [Display(Description = "Required: No, For example: true or false (0,1) ")]
        public bool IsCookieEnabled { get; set; }

        [Display(Description = "Required: No, For example: true or false (0,1) ")]
        public bool IsCrawler { get; set; }

        [Display(Description = "Required: No, For example: true or false (0,1) ")]
        public bool IsActiveXControlEnabled { get; set; }
    }

    public class AuditvisitInfoResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string StatusCode { get; set; }
        public string ResponseReason { get; set; }
        public List<AuditvisitInfo> AuditvisitInfo { get; set; }
    }

    #endregion
}
