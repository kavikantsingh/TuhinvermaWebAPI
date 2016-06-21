using LAPP.BAL;
using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.LOGING;
using LAPP.WS.App_Helper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LAPP.WS.Controllers.Backoffice
{
    public class DashboardController : ApiController
    {

        /// <summary>
        /// Get Method to get DashboardApplicationCount key.
        /// </summary>
        /// <param name="Key">API security key.</param>
        [AcceptVerbs("GET")]
        [ActionName("DashboardApplicationCount")]
        public ApplicationCountResponse DashboardApplicationCount(string Key)
        {
            LogingHelper.SaveAuditInfo(Key);

            ApplicationCountResponse objResponse = new ApplicationCountResponse();
            ApplicationCount objEntity = new ApplicationCount();
            List<ApplicationCount> lstApplicationCount = new List<ApplicationCount>();
            ApplicationBAL objAppBAL = new ApplicationBAL();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ApplicationCount = null;
                    return objResponse;
                }



                objEntity = objAppBAL.Get_DashboardApplicationCountRenewal();
                if (objEntity != null)
                {
                    ApplicationCount objEntityForRenewal = new ApplicationCount();

                    objEntityForRenewal.ApplicationType = "Licensee";
                    objEntityForRenewal.ApplicationsApproved = objEntity.ApplicationsApproved;
                    objEntityForRenewal.ApplicationsDenied = objEntity.ApplicationsDenied;
                    objEntityForRenewal.ApplicationsUnderReview = objEntity.ApplicationsUnderReview;
                    objEntityForRenewal.ApplicationsSubmittedCount = objEntity.ApplicationsSubmittedCount;

                    lstApplicationCount.Add(objEntityForRenewal);
                }

                objEntity = new ApplicationCount();

                objEntity = objAppBAL.Get_DashboardApplicationCountApplications();
                if (objEntity != null)
                {
                    ApplicationCount objEntityForRenewal = new ApplicationCount();

                    objEntityForRenewal.ApplicationType = "Licensee";
                    objEntityForRenewal.ApplicationsApproved = objEntity.ApplicationsApproved;
                    objEntityForRenewal.ApplicationsDenied = objEntity.ApplicationsDenied;
                    objEntityForRenewal.ApplicationsUnderReview = objEntity.ApplicationsUnderReview;
                    objEntityForRenewal.ApplicationsSubmittedCount = objEntity.ApplicationsSubmittedCount;

                    lstApplicationCount.Add(objEntityForRenewal);
                }

                if (lstApplicationCount != null && lstApplicationCount.Count > 0)
                {
                    objResponse.ApplicationCount = lstApplicationCount;
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ApplicationCount = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "DashboardApplicationCount", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.ApplicationCount = null;

            }
            return objResponse;
        }


    }
}
