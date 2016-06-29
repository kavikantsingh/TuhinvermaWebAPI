using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LAPP.BAL.Reports;
using LAPP.ENTITY;
using LAPP.WS.App_Helper.Common;
using LAPP.ENTITY.Enumeration;
using LAPP.LOGING;

namespace LAPP.WS.Controllers.Reports
{
    public class ReportController : ApiController
    {

        [HttpGet]
        public DailyDepositResponse GetAllDailyDeposit(string Key,string startDate, string enddate)
        {
          // string Key = "2";
            LogingHelper.SaveAuditInfo(Key);

      
            DailyDepositResponse objResponse = new DailyDepositResponse();
            DailyDepositBAL objBAL = new DailyDepositBAL();
         
            try {

                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.DeliveryType = null;
                    return objResponse;
                }
               // List<EmployerType> lstEmployerType = new List<EmployerType>();
                //lstEmployerType = objBAL.Get_All_EmployerType();

                List<DailyDeposit> lstDepositType = new List<DailyDeposit>();
                lstDepositType = objBAL.Get_All_DailyDeposits(startDate, enddate);
                if (lstDepositType != null && lstDepositType.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Message = "";
                    objResponse.DeliveryType = lstDepositType;

                } 
                else
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Message = "No record found.";
                    objResponse.DeliveryType = null;

                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "DailyDepositReport", ENTITY.Enumeration.eSeverity.Error);
                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.DeliveryType = null;

            }           
           
            return objResponse;
        }

        [HttpGet]
        public VerifyDataResponse GetVerifyData(string Key)
        {
            // string Key = "2";
            LogingHelper.SaveAuditInfo(Key);


            VerifyDataResponse objResponse = new VerifyDataResponse();
            VerifyDataBAL objBAL = new VerifyDataBAL();

            try
            {

                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.VerifyDataType = null;
                    return objResponse;
                }
                // List<EmployerType> lstEmployerType = new List<EmployerType>();
                //lstEmployerType = objBAL.Get_All_EmployerType();

                List<VerifyDataEntity> lstVerifyType = new List<VerifyDataEntity>();
                lstVerifyType = objBAL.Get_All_VerifyData();
                if (lstVerifyType != null && lstVerifyType.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Message = "";
                    objResponse.VerifyDataType = lstVerifyType;

                }
                else
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Message = "No record found.";
                    objResponse.VerifyDataType = null;

                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "VerifyDataReport", ENTITY.Enumeration.eSeverity.Error);
                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.VerifyDataType = null;

            }

            return objResponse;
        }

    }
}    
