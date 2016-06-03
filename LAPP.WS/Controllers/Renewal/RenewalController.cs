﻿using LAPP.BAL;
using LAPP.BAL.Renewal;
using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.LOGING;
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
            IndividualRenewalResponse objResponse = new IndividualRenewalResponse();
            LogingHelper.SaveAuditInfo(Key);

            if (objRenewalRequest == null)
            {
                objResponse.Message = "Invalid Object.";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                objResponse.ResponseReason = "";
                return objResponse;


            }


            LogingHelper.SaveExceptionInfo(objRenewalRequest);



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

            IndividualRenewalBAL objBAL = new IndividualRenewalBAL();
            RenewalGetResponse objResponse = new RenewalGetResponse();
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

                lstRenewalGet = objBAL.Get_All_Renewal();
                if (lstRenewalGet != null && lstRenewalGet.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    List<RenewalGetSelected> lstRenewalGetSelected = lstRenewalGet.Select(RenewalGetSelectedRes => new RenewalGetSelected
                    {
                        IndividualId = RenewalGetSelectedRes.IndividualId,
                        LicenseNumber = RenewalGetSelectedRes.LicenseNumber,
                        ApplicationNumber = RenewalGetSelectedRes.ApplicationNumber,
                        FirstName = RenewalGetSelectedRes.FirstName,
                        LastName = RenewalGetSelectedRes.LastName,
                        SubmittedDate = RenewalGetSelectedRes.SubmittedDate,
                        IsPaid = RenewalGetSelectedRes.IsPaid,
                        IsActive = RenewalGetSelectedRes.IsActive,
                        ApplicationStatus = RenewalGetSelectedRes.ApplicationStatus

                    }).ToList();


                    objResponse.RenewalGetList = lstRenewalGetSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.RenewalGetList = null;
                }
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



        /// <summary>
        /// Method to Search Renewal by key and objSearch.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="objSearch">Record ID.</param>
        [AcceptVerbs("POST")]
        [ActionName("RenewalSearch")]
        public IndividualSearchResponse RenewalSearch(string Key, IndividualSearch objSearch)
        {
            LogingHelper.SaveAuditInfo(Key);

            IndividualSearchResponse objResponse = new IndividualSearchResponse();
            IndividualBAL objBAL = new IndividualBAL();
            Individual objEntity = new Individual();
            List<Individual> lstIndividual = new List<Individual>();
            List<IndividualSearch> lstIndividualSelected = new List<IndividualSearch>();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.IndividualSearch = null;
                    return objResponse;
                }

                lstIndividual = objBAL.Search_Renewal(objSearch);
                if (lstIndividual != null && lstIndividual.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    lstIndividualSelected = lstIndividual.Select(RenewalGetSelectedRes => new IndividualSearch
                    {
                        IndividualId = RenewalGetSelectedRes.IndividualId,
                        LicenseNumber = RenewalGetSelectedRes.LicenseNumber,
                        ApplicationNumber = RenewalGetSelectedRes.ApplicationNumber,
                        FirstName = RenewalGetSelectedRes.FirstName,
                        LastName = RenewalGetSelectedRes.LastName,
                        SubmittedDate = RenewalGetSelectedRes.SubmittedDate,
                        IsPaid = RenewalGetSelectedRes.IsPaid,
                        IsActive = RenewalGetSelectedRes.IsActive,
                        Name = RenewalGetSelectedRes.Name,
                        SSN = RenewalGetSelectedRes.SSN,
                        Phone = RenewalGetSelectedRes.Phone,
                        StatusId = RenewalGetSelectedRes.StatusId,
                        StatusName = RenewalGetSelectedRes.StatusName,
                    }).ToList();

                    objResponse.IndividualSearch = lstIndividualSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.IndividualSearch = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "RenewalSearch", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualSearch = null;

            }
            return objResponse;
        }




        /// <summary>
        /// Method to Search Renewal by key and objSearch with pager.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="objSearch">objSearch.</param>
        /// <param name="CurrentPage">CurrentPage.</param>
        /// <param name="PagerSize">PagerSize.</param>
        [AcceptVerbs("POST")]
        [ActionName("RenewalSearchWithPager")]
        public IndividualSearchResponse RenewalSearchWithPager(string Key, IndividualSearch objSearch, int CurrentPage, int PagerSize)
        {
            LogingHelper.SaveAuditInfo(Key);

            IndividualSearchResponse objResponse = new IndividualSearchResponse();
            IndividualBAL objBAL = new IndividualBAL();
            Individual objEntity = new Individual();
            List<Individual> lstIndividual = new List<Individual>();
            List<IndividualSearch> lstIndividualSelected = new List<IndividualSearch>();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.IndividualSearch = null;
                    return objResponse;
                }

                lstIndividual = objBAL.Search_RenewalWithPager(objSearch, CurrentPage, PagerSize);
                if (lstIndividual != null && lstIndividual.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    lstIndividualSelected = lstIndividual.Select(RenewalGetSelectedRes => new IndividualSearch
                    {
                        IndividualId = RenewalGetSelectedRes.IndividualId,
                        LicenseNumber = RenewalGetSelectedRes.LicenseNumber,
                        ApplicationNumber = RenewalGetSelectedRes.ApplicationNumber,
                        FirstName = RenewalGetSelectedRes.FirstName,
                        LastName = RenewalGetSelectedRes.LastName,
                        SubmittedDate = RenewalGetSelectedRes.SubmittedDate,
                        IsPaid = RenewalGetSelectedRes.IsPaid,
                        IsActive = RenewalGetSelectedRes.IsActive,
                        Name = RenewalGetSelectedRes.Name,
                        SSN = RenewalGetSelectedRes.SSN,
                        Phone = RenewalGetSelectedRes.Phone,
                        StatusId = RenewalGetSelectedRes.StatusId,
                        StatusName = RenewalGetSelectedRes.StatusName,
                    }).ToList();

                    objResponse.IndividualSearch = lstIndividualSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.IndividualSearch = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "RenewalSearchWithPager", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualSearch = null;

            }
            return objResponse;
        }




    }
}
