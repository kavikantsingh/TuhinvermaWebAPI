using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.WS.App_Helper.Common;
using LAPP.BAL;
using LAPP.WS.App_Helper;
using LAPP.LOGING;

namespace LAPP.WS.Controllers.Common
{
    public class EmployerController : ApiController
    {
        #region Employer Type

        /// <summary>
        /// Employer Type get all
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        [ActionName("EmployerTypeGetAll")]
        public EmployerTypeResponse EmployerTypeGetAll(string Key)
        {

            //Audit Request
            LogingHelper.SaveAuditInfo(Key);

            EmployerTypeResponse objResponse = new EmployerTypeResponse();
            EmployerTypeBAL objBAL = new EmployerTypeBAL();

            try
            {


                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.EmployerType = null;
                    return objResponse;
                }
                List<EmployerType> lstEmployerType = new List<EmployerType>();
                lstEmployerType = objBAL.Get_All_EmployerType();
                if (lstEmployerType != null && lstEmployerType.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Message = "";

                    List<EmployerTypeRequest> lstRequest = lstEmployerType.Select(obj => new EmployerTypeRequest
                    {
                        EmployerTypeCode = obj.EmployerTypeCode,
                        EmployerTypeId = obj.EmployerTypeId,
                        EffectiveDate = obj.EffectiveDate,
                        EmployerTypeName = obj.EmployerTypeName,
                        EndDate = obj.EndDate,
                        IsActive = obj.IsActive,
                        SortOrder = obj.SortOrder


                    }).ToList();

                    objResponse.EmployerType = lstRequest;

                }
                else
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Message = "No record found.";
                    objResponse.EmployerType = null;

                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "EmployerTypeGetAll", ENTITY.Enumeration.eSeverity.Error);
                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.EmployerType = null;

            }
            return objResponse;
        }
        #endregion
    }
}
