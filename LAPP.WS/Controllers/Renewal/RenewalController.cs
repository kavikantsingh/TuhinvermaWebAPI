using LAPP.BAL;
using LAPP.BAL.Renewal;
using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.WS.App_Helper;
using LAPP.WS.App_Helper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LAPP.WS.Controllers.Renewal
{
    /// <summary>
    /// 
    /// </summary>
    public class RenewalController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="IndividualId"></param>

        /// <returns></returns>
        [AcceptVerbs("GET")]
        [ActionName("IndividualRenewalGet")]
        public IndividualRenewalResponse IndividualRenewalGet(string Key, int IndividualId)
        {
            int ApplicationTypeId = 1;

            LogingHelper.SaveAuditInfo(Key);

            IndividualRenewalResponse objResponse = new IndividualRenewalResponse();
            IndividualRenewal objIndividualRenewal = new IndividualRenewal();

            try
            {


                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.IndividualRenewal = null;
                    return objResponse;
                }


                return RenewalProcess.SelectOrCreateResponse(TokenHelper.GetTokenByKey(Key), IndividualId, ApplicationTypeId);

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualRenewalGet", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualRenewal = null;

            }
            return objResponse;


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="objRenewalRequest"></param>
        /// <returns></returns>
        [AcceptVerbs("Post")]
        [ActionName("IndividualRenewalSave")]
        public IndividualRenewalResponse IndividualRenewalSave(string Key, IndividualRenewalResponse objRenewalRequest)
        {

            LogingHelper.SaveAuditInfo(Key);

            IndividualRenewalResponse objResponse = new IndividualRenewalResponse();
            IndividualRenewal objIndividualRenewal = new IndividualRenewal();

            try
            {

                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.IndividualRenewal = null;
                    return objResponse;
                }

                return RenewalProcess.SaveAndValidateRequest(TokenHelper.GetTokenByKey(Key), objRenewalRequest);

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualRenewalSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualRenewal = null;

            }
            return objResponse;


        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        [ActionName("RenewalGetAll")]
        public RenewalGetResponse RenewalGetAll(string Key)
        {
            LogingHelper.SaveAuditInfo(Key);

            RenewalGetResponse objResponse = new RenewalGetResponse();
            IndividualRenewal objIndividualRenewal = new IndividualRenewal();
            List<RenewalGet> lstRenewalGet = new List<RenewalGet>();
            RenewalGet ovjRenewalGet = new RenewalGet();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.RenewalGetList = null;
                    return objResponse;
                }

                objResponse.Status = true;
                objResponse.Message = "";
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                ovjRenewalGet.IndividualId = 10;
                ovjRenewalGet.FirstName = "Nancy";
                ovjRenewalGet.LastName = "Blachly";
                ovjRenewalGet.LicenseNumber = "3124";
                ovjRenewalGet.SubmittedOn = "03/17/2015";
                ovjRenewalGet.FirstName = "Submitted";
                ovjRenewalGet.IsPaid = false;
                lstRenewalGet.Add(ovjRenewalGet);

                ovjRenewalGet = new RenewalGet();
                ovjRenewalGet.FirstName = "David";
                ovjRenewalGet.LastName = "Hook";
                ovjRenewalGet.IndividualId = 14;

                ovjRenewalGet.LicenseNumber = "2346";
                ovjRenewalGet.SubmittedOn = "03/18/2015";
                ovjRenewalGet.FirstName = "Complete";
                ovjRenewalGet.IsPaid = true;

                lstRenewalGet.Add(ovjRenewalGet);

                objResponse.RenewalGetList = lstRenewalGet;
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "RenewalGetAll", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.RenewalGetList = null;

            }
            return objResponse;


        }
    }
}
