using LAPP.BAL;
using LAPP.BAL.Backoffice.IndividualFolder;
using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.GlobalFunctions;
using LAPP.LOGING;
using LAPP.WS.App_Helper;
using LAPP.WS.App_Helper.Common;
using LAPP.WS.ValidateController.Backoffice;
using LAPP.WS.ValidateController.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using EO.Pdf;
using System.Drawing;
using System.Net.Http.Headers;
using System.Web.Http.Description;
using System.Data.OleDb;
using System.Data;

namespace LAPP.WS.Controllers.Backoffice
{
    public class IndividualController : ApiController
    {

        #region IndividualAddress_Save


        /// <summary>
        /// Save or Update the data For IndividualAddress
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objIndividualAddress">Object of IndividualAddress</param>
        [AcceptVerbs("POST")]
        [ActionName("IndividualAddressSave")]
        public IndividualAddressResponseRequest IndividualAddressSave(string Key, IndividualAddressResponse objIndividualAddress)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo(Key);

            IndividualAddressResponseRequest objResponse = new IndividualAddressResponseRequest();
            IndividualAddressBAL objBAL = new IndividualAddressBAL();
            IndividualAddress objEntity = new IndividualAddress();
            List<IndividualAddressResponse> lstEntity = new List<IndividualAddressResponse>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.IndividualAddressResponse = null;
                return objResponse;
            }
            try
            {

                try
                {
                    if (System.Web.HttpContext.Current.IsDebuggingEnabled)
                    {
                        // this is executed only in the debug version
                        string requestStr = Newtonsoft.Json.JsonConvert.SerializeObject(objIndividualAddress);
                        LogingHelper.SaveRequestJson(requestStr, "Save Individual Address Request");
                    }

                }
                catch (Exception ex)
                {
                    LogingHelper.SaveExceptionInfo(Key, ex, "SaveIndividualAddress object serialization", ENTITY.Enumeration.eSeverity.Critical);
                }

                string ValidationResponse = IndividualValidations.ValidateIndividualAddressObject(objIndividualAddress);

                if (!string.IsNullOrEmpty(ValidationResponse))
                {
                    objResponse.Message = "Validation Error";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }
                if (objIndividualAddress != null)
                {
                    return IndividualAddressCS.SaveIndividualAddress(TokenHelper.GetTokenByKey(Key), objIndividualAddress);
                }
                else
                {
                    objResponse.Message = "Individual Address object cannot be null.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualAddressSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.IndividualAddressResponse = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }


        /// <summary>
        /// Get Method to get IndividualAddress by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="IndividualId">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("IndividualAddressBYIndividualId")]

        public IndividualAddressResponseRequest IndividualAddressBYIndividualId(string Key, int IndividualId)
        {
            LogingHelper.SaveAuditInfo(Key);

            IndividualAddressResponseRequest objResponse = new IndividualAddressResponseRequest();
            IndividualAddressBAL objBAL = new IndividualAddressBAL();
            IndividualAddress objEntity = new IndividualAddress();
            List<IndividualAddressResponse> lstEntity = new List<IndividualAddressResponse>();
            List<IndividualAddress> lstIndividualAddress = new List<IndividualAddress>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.IndividualAddressResponse = null;
                    return objResponse;
                }

                lstIndividualAddress = objBAL.Get_IndividualAddress_By_IndividualId(IndividualId);
                if (lstIndividualAddress != null)
                {
                    List<IndividualAddressResponse> lstAddressResponse = lstIndividualAddress
                        .Select(obj => new IndividualAddressResponse
                        {
                            Addressee = obj.Addressee,
                            AddressTypeId = obj.AddressTypeId,
                            AddressId = obj.AddressId,
                            BeginDate = obj.BeginDate,
                            City = obj.City,
                            CountryId = obj.CountryId,
                            CountyId = obj.CountyId,
                            EndDate = obj.EndDate,
                            IndividualAddressId = obj.IndividualAddressId,
                            IndividualId = obj.IndividualId,
                            IsActive = obj.IsActive,
                            IsMailingSameasPhysical = obj.IsMailingSameasPhysical,
                            StateCode = obj.StateCode,
                            StreetLine1 = obj.StreetLine1,
                            StreetLine2 = obj.StreetLine2,
                            BadAddress = obj.BadAddress,
                            Zip = obj.Zip


                        }).ToList();


                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.IndividualAddressResponse = lstAddressResponse;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.IndividualAddressResponse = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualAddressBYIndividualId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualAddressResponse = null;

            }
            return objResponse;
        }


        #endregion

        #region IndividualSave



        /// <summary>
        /// Method to Search Individual by key and objSearch.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="objSearch">Record ID.</param>
        [AcceptVerbs("POST")]
        [ActionName("IndividualSearch")]
        public IndividualSearchResponse IndividualSearch(string Key, IndividualSearch objSearch)
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
                    objResponse.IndividualList = null;
                    return objResponse;
                }

                lstIndividual = objBAL.Search_Individual(objSearch);
                if (lstIndividual != null && lstIndividual.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    lstIndividualSelected = lstIndividual.Select(RenewalGetSelectedRes => new IndividualSearch
                    {
                        IndividualId = RenewalGetSelectedRes.IndividualId,
                        FirstName = RenewalGetSelectedRes.FirstName,
                        LastName = RenewalGetSelectedRes.LastName,
                        Name = RenewalGetSelectedRes.Name,
                        LicenseNumber = RenewalGetSelectedRes.LicenseNumber,
                        Email = RenewalGetSelectedRes.Email,
                        SSN = RenewalGetSelectedRes.SSN,
                        Phone = RenewalGetSelectedRes.Phone,

                    }).ToList();

                    objResponse.IndividualList = lstIndividualSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.IndividualList = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualSearch", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualList = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Method to Search Individual by key and objSearch with Pager.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="objSearch">objSearch.</param>
        /// <param name="PageNumber">Record ID.</param>
        /// <param name="NoOfRecords">Record ID.</param>
        [AcceptVerbs("POST")]
        [ActionName("IndividualSearchWithPage")]
        public IndividualSearchResponse IndividualSearchWithPage(string Key, IndividualSearch objSearch, int PageNumber, int NoOfRecords)
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
                    objResponse.IndividualList = null;
                    return objResponse;
                }

                lstIndividual = objBAL.Search_Individual_WithPager(objSearch, PageNumber, NoOfRecords);
                if (lstIndividual != null && lstIndividual.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    lstIndividualSelected = lstIndividual.Select(RenewalGetSelectedRes => new IndividualSearch
                    {
                        IndividualId = RenewalGetSelectedRes.IndividualId,
                        FirstName = RenewalGetSelectedRes.FirstName,
                        LastName = RenewalGetSelectedRes.LastName,
                        LicenseNumber = RenewalGetSelectedRes.LicenseNumber,
                        Name = RenewalGetSelectedRes.Name,
                        Email = RenewalGetSelectedRes.Email,
                        SSN = RenewalGetSelectedRes.SSN,
                        Phone = RenewalGetSelectedRes.Phone,

                    }).ToList();

                    objResponse.IndividualList = lstIndividualSelected;
                    objResponse.Total_Recard = lstIndividual[0].Total_Recard;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.IndividualList = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualSearchWithPage", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualList = null;

            }
            return objResponse;
        }



        /// <summary>
        /// Save or Update the data For IndividualAddress
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objIndividual">Object of IndividualAddress</param>
        [AcceptVerbs("POST")]
        [ActionName("IndividualSave")]
        public IndividualResponseRequest IndividualSave(string Key, IndividualResponse objIndividual)
        {
            if (objIndividual == null)
                throw new Exception("Request object is not matching with API signature. Please compare with API signature.");

            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo(Key);

            IndividualResponseRequest objResponse = new IndividualResponseRequest();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.IndividualResponse = null;
                return objResponse;
            }
            try
            {
                string ValidationResponse = "";// IndividualAddressValidate.ValidateIndividualAddressObject(objIndividualAddress);

                try
                {
                    if (System.Web.HttpContext.Current.IsDebuggingEnabled)
                    {
                        // this is executed only in the debug version
                        string requestStr = Newtonsoft.Json.JsonConvert.SerializeObject(objIndividual);
                        LogingHelper.SaveRequestJson(requestStr, "Save Individual Request");
                    }

                }
                catch (Exception ex)
                {
                    LogingHelper.SaveExceptionInfo(Key, ex, "ProcessPayment object serialization", ENTITY.Enumeration.eSeverity.Critical);
                }

                if (!string.IsNullOrEmpty(ValidationResponse))
                {
                    objResponse.Message = "Validation Error";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }
                if (objIndividual != null)
                {
                    return IndividualCS.SaveIndividual(TokenHelper.GetTokenByKey(Key), objIndividual);
                }
                else
                {
                    objResponse.Message = "Individual object cannot be null.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.IndividualResponse = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }


        /// <summary>
        /// Get Method to get Individual by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="IndividualId">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("IndividualBYIndividualId")]
        public IndividualResponseRequest IndividualBYIndividualId(string Key, int IndividualId)
        {
            LogingHelper.SaveAuditInfo(Key);

            IndividualResponseRequest objResponse = new IndividualResponseRequest();
            IndividualBAL objIndividualBAL = new IndividualBAL();
            IndividualResponse objIndividualResponse = new IndividualResponse();
            Individual objIndividual = new Individual();
            List<IndividualResponse> lstIndividualResponse = new List<IndividualResponse>();
            List<Individual> lstIndividual = new List<Individual>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.IndividualResponse = null;
                    return objResponse;
                }

                objIndividual = objIndividualBAL.Get_Individual_By_IndividualId(IndividualId);
                if (objIndividual != null)
                {
                    lstIndividual.Add(objIndividual);

                    lstIndividualResponse = lstIndividual.Select(obj => new IndividualResponse
                    {
                        IndividualId = obj.IndividualId,
                        FirstName = obj.FirstName,
                        LastName = obj.LastName,
                        MiddleName = obj.MiddleName,
                        SuffixId = obj.SuffixId,
                        Email = obj.Email,
                        SSN = obj.SSN,
                        IsItin = obj.IsItin,
                        DateOfBirth = obj.DateOfBirth,
                        RaceId = obj.RaceId,
                        Gender = obj.Gender,
                        HairColorId = obj.HairColorId,
                        EyeColorId = obj.EyeColorId,
                        Weight = obj.Weight,
                        Height = obj.Height,
                        PlaceOfBirth = obj.PlaceOfBirth,
                        CitizenshipId = obj.CitizenshipId,
                        ExternalId = obj.ExternalId,
                        ExternalId2 = obj.ExternalId2,
                        IsArchived = obj.IsArchived,
                        Name = obj.Name,

                        StatusColorCode = obj.StatusColorCode,
                        LicenseStatusTypeName = obj.LicenseStatusTypeName,
                        LicenseNumber = obj.LicenseNumber,
                        LicenseTypeId = obj.LicenseTypeId,
                        LicenseTypeName = obj.LicenseTypeName,
                        LicenseStatusTypeId = obj.LicenseStatusTypeId,
                        OriginalLicenseDate = obj.OriginalLicenseDate,
                        IsNameChanged = obj.IsNameChanged,
                        PlaceofBirthCity = obj.PlaceofBirthCity,
                        PlaceofBirthState = obj.PlaceofBirthState,
                        PlaceofBirthCountry = obj.PlaceofBirthCountry,
                        objIndividualAddress = obj.objIndividualAddress,
                        Picture = obj.Picture,
                        objIndividualContact = obj.objIndividualContact,

                        IsActive = obj.IsActive

                    }).ToList();

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    objResponse.IndividualResponse = lstIndividualResponse;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.IndividualResponse = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualBYIndividualId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualResponse = null;

            }
            return objResponse;
        }



        /// <summary>
        /// Validate Individual by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="IndividualId">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("ValidateIndividual")]
        public IndividualRenewalResponse ValidateIndividual(string Key, int IndividualId)
        {
            LogingHelper.SaveAuditInfo(Key);

            IndividualRenewalResponse objResponse = new IndividualRenewalResponse();
            IndividualBAL objIndividualBAL = new IndividualBAL();
            IndividualResponse objIndividualResponse = new IndividualResponse();
            Individual objIndividual = new Individual();
            List<IndividualResponse> lstIndividualResponse = new List<IndividualResponse>();
            List<Individual> lstIndividual = new List<Individual>();
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

                try
                {
                    objIndividualResponse = objIndividualBAL.Get_Individual_By_IndividualId(IndividualId);
                    if (objIndividualResponse != null)
                    {
                        Users objUseres = new Users();
                        UsersBAL objUsersBAL = new UsersBAL();
                        objUseres = objUsersBAL.Get_Users_byIndividualId(objIndividualResponse.IndividualId);
                        if (objUseres != null)
                        {
                            objIndividualResponse.Email = objUseres.Email;
                        }

                        IndividualRenewal objIndividualRenewal = new IndividualRenewal();
                        objResponse.IndividualRenewal = objIndividualRenewal;



                        #region Validate License and Application 

                        IndividualLicense objLatestLicense = new IndividualLicense();
                        IndividualLicenseBAL objIndividualLicenseBAL = new IndividualLicenseBAL();

                        objLatestLicense = objIndividualLicenseBAL.Get_Latest_IndividualLicense_By_IndividualId(IndividualId);
                        if (objLatestLicense != null)
                        {
                            if (objLatestLicense.LicenseStatusTypeId == 6)
                            {
                                objResponse.ResponseReason = "";
                                objResponse.Message = "Either you are not allowed to renew or you have submitted your renewal. Please contact to the board.";
                                objResponse.Status = false;
                                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.RenewalDenied).ToString("00");
                                objResponse.IndividualRenewal = null;
                                return objResponse;

                            }
                            bool NotValidLicenseRequestMax = false;
                            bool NotValidLicenseRequestMin = false;

                            int AllowedDaysMax = 0;
                            int AllowedDaysMin = 0;
                            ConfigurationTypeBAL objBAL = new ConfigurationTypeBAL();
                            ConfigurationType objEntity = new ConfigurationType();
                            ConfigurationType objConfigurationType = new ConfigurationType();
                            objConfigurationType = objBAL.Get_Configuration_By_Settings_object("Noofdaysallowedforrenewalfromlicenseexpdate".ToLower());
                            if (objConfigurationType != null)
                            {
                                AllowedDaysMax = Convert.ToInt32(objConfigurationType.Value);
                            }
                            objConfigurationType = new ConfigurationType();
                            objConfigurationType = objBAL.Get_Configuration_By_Settings_object("Noofdaysallowedforrenewalbeforelicenseexp".ToLower());
                            if (objConfigurationType != null)
                            {
                                AllowedDaysMin = Convert.ToInt32(objConfigurationType.Value);
                            }

                            List<IndividualLicense> lstIndividualLicense = new List<IndividualLicense>();
                            try
                            {
                                lstIndividualLicense = objIndividualLicenseBAL.Get_IndividualLicense_By_IndividualId(IndividualId);
                                if (lstIndividualLicense != null && lstIndividualLicense.Count > 0)
                                {

                                    IndividualLicense objLicense = lstIndividualLicense[0];
                                    if (objLicense.LicenseExpirationDate.Date.AddDays(-AllowedDaysMin) >= DateTime.Now)
                                    {
                                        NotValidLicenseRequestMin = true;
                                    }

                                    if (objLicense.LicenseExpirationDate.Date.AddDays(AllowedDaysMax) <= DateTime.Now)
                                    {
                                        NotValidLicenseRequestMax = true;
                                    }

                                    #region Check Renewal Denieal Status

                                    if (NotValidLicenseRequestMin)
                                    {
                                        objResponse.ResponseReason = "";// GlobalFunctions.GeneralFunctions.GetJsonStringFromList(lstResponseReason);
                                        objResponse.Message = "License renewal is not open yet. License can only be renewed starting  " + AllowedDaysMin + " days before the expiration date. ";
                                        objResponse.Status = false;
                                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.RenewalDenied).ToString("00");
                                        objResponse.IndividualRenewal = null;
                                        return objResponse;

                                    }
                                    if (NotValidLicenseRequestMax)
                                    {
                                        objResponse.ResponseReason = "";// GlobalFunctions.GeneralFunctions.GetJsonStringFromList(lstResponseReason);
                                        objResponse.Message = "License can only be renewed within " + AllowedDaysMax + " days of the license expiration date. Please contact office.";
                                        objResponse.Status = false;
                                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.RenewalDenied).ToString("00");
                                        objResponse.IndividualRenewal = null;
                                        return objResponse;

                                    }

                                    #endregion

                                }
                                else
                                {
                                    objResponse.ResponseReason = "";
                                    objResponse.Message = "Either you are not allowed to renew or you have submitted your renewal. Please contact to the board.";
                                    objResponse.Status = false;
                                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.RenewalDenied).ToString("00");
                                    objResponse.IndividualRenewal = null;
                                    return objResponse;

                                }

                                objResponse.ResponseReason = "";
                                objResponse.Message = "You are eligible for renewal.";
                                objResponse.Status = true;
                                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                                objResponse.IndividualRenewal = null;
                                return objResponse;
                            }
                            catch (Exception ex)
                            {
                                throw ex;

                            }
                        }
                        else
                        {
                            objResponse.Status = false;
                            objResponse.Message = "No license found.";
                            objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                            objResponse.IndividualRenewal = null;
                        }
                        #endregion
                    }
                    else
                    {
                        objResponse.Status = false;
                        objResponse.Message = "No record found.";
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                        objResponse.IndividualRenewal = null;
                    }

                }
                catch (Exception ex)
                {
                    LogingHelper.SaveExceptionInfo(Key, ex, "ValidateIndividual", ENTITY.Enumeration.eSeverity.Error);

                    objResponse.Status = false;
                    objResponse.Message = ex.Message;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                    objResponse.IndividualRenewal = null;

                }


            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ValidateIndividual", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualRenewal = null;

            }
            return objResponse;
        }


        #endregion

        #region IndividualNameSave


        /// <summary>
        /// Save or Update the data For IndividualNameAddress
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objIndividualName">Object of IndividualNameAddress</param>
        [AcceptVerbs("POST")]
        [ActionName("IndividualNameSave")]
        public IndividualNameRequestResponse IndividualNameSave(string Key, IndividualNameRequest objIndividualName)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo(Key);

            IndividualNameRequestResponse objResponse = new IndividualNameRequestResponse();
            IndividualNameBAL objBAL = new IndividualNameBAL();
            IndividualName objEntity = new IndividualName();
            List<IndividualNameRequest> lstEntity = new List<IndividualNameRequest>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.IndividualNameResponse = null;
                return objResponse;
            }

            try
            {
                if (System.Web.HttpContext.Current.IsDebuggingEnabled)
                {
                    // this is executed only in the debug version
                    string requestStr = Newtonsoft.Json.JsonConvert.SerializeObject(objIndividualName);
                    LogingHelper.SaveRequestJson(requestStr, "Save Individual Name Request");
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "SaveIndividualName object serialization", ENTITY.Enumeration.eSeverity.Critical);
            }

            try
            {
                string ValidationResponse = "";// IndividualNameAddressValidate.ValidateIndividualNameAddressObject(objIndividualNameAddress);

                if (!string.IsNullOrEmpty(ValidationResponse))
                {
                    objResponse.Message = "Validation Error";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }
                if (objIndividualName != null)
                {
                    return IndividualNameCS.SaveIndividualName(TokenHelper.GetTokenByKey(Key), objIndividualName);

                }
                else
                {
                    objResponse.Message = "Individual Name object cannot be null.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualNameSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.IndividualNameResponse = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }



        /// <summary>
        /// Get Method to get IndividualName by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="IndividualId">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("IndividualNameBYIndividualId")]
        public IndividualNameRequestResponse IndividualNameBYIndividualId(string Key, int IndividualId)
        {
            LogingHelper.SaveAuditInfo(Key);

            IndividualNameRequestResponse objResponse = new IndividualNameRequestResponse();
            IndividualNameBAL objBAL = new IndividualNameBAL();
            IndividualName objEntity = new IndividualName();
            List<IndividualNameRequest> lstEntity = new List<IndividualNameRequest>();
            List<IndividualName> lstIndividualName = new List<IndividualName>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.IndividualNameResponse = null;
                    return objResponse;
                }

                lstIndividualName = objBAL.Get_IndividualName_By_IndividualIdANDIndividualNameTypeId(IndividualId, Convert.ToInt32(eIndividualNameType.Individual));
                if (lstIndividualName != null)
                {
                    lstEntity = lstIndividualName
                        .Select(obj => new IndividualNameRequest
                        {
                            IndividualNameId = obj.IndividualNameId,
                            IndividualId = obj.IndividualId,
                            FirstName = obj.FirstName,
                            MiddleName = obj.MiddleName,
                            LastName = obj.LastName,
                            SuffixId = obj.SuffixId,
                            IndividualNameStatusId = obj.IndividualNameStatusId,
                            IndividualNameTypeId = obj.IndividualNameTypeId,
                            IsActive = obj.IsActive,
                            BeginDate = obj.BeginDate,
                            EndDate = obj.EndDate,

                        }).ToList();


                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.IndividualNameResponse = lstEntity;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.IndividualNameResponse = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualNameBYIndividualId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualNameResponse = null;

            }
            return objResponse;
        }


        #endregion

        #region IndividualEducation_Save


        /// <summary>
        /// Save or Update the data For IndividualCECourse
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objIndividualCECourse">Object of IndividualCECourse</param>
        [AcceptVerbs("POST")]
        [ActionName("IndividualEducationSave")]
        public IndividualCECourseResponseRequest IndividualEducationSave(string Key, IndividualCECourseResponse objIndividualCECourse)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo(Key);

            IndividualCECourseResponseRequest objResponse = new IndividualCECourseResponseRequest();
            IndividualCECourseBAL objBAL = new IndividualCECourseBAL();
            IndividualCECourse objEntity = new IndividualCECourse();
            List<IndividualCECourseResponse> lstEntity = new List<IndividualCECourseResponse>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.IndividualCECourseResponseList = null;
                return objResponse;
            }
            try
            {

                try
                {
                    if (System.Web.HttpContext.Current.IsDebuggingEnabled)
                    {
                        // this is executed only in the debug version
                        string requestStr = Newtonsoft.Json.JsonConvert.SerializeObject(objIndividualCECourse);
                        LogingHelper.SaveRequestJson(requestStr, "Save Individual Education Request");
                    }

                }
                catch (Exception ex)
                {
                    LogingHelper.SaveExceptionInfo(Key, ex, "SaveIndividualEducation object serialization", ENTITY.Enumeration.eSeverity.Critical);
                }
                string ValidationResponse = "";// IndividualCECourseValidate.ValidateIndividualCECourseObject(objIndividualCECourse);

                if (!string.IsNullOrEmpty(ValidationResponse))
                {
                    objResponse.Message = "Validation Error";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }

                if (objIndividualCECourse != null)
                {
                    return IndividualEducationCS.SaveIndividualEducation(TokenHelper.GetTokenByKey(Key), objIndividualCECourse);
                }
                else
                {
                    objResponse.Message = "Individual Education object cannot be null.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualEducationSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.IndividualCECourseResponseList = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }

        /// <summary>
        /// Get Method to get IndividualEducation by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="IndividualId">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("IndividualEducationBYIndividualId")]
        public IndividualCECourseResponseRequest IndividualEducationBYIndividualId(string Key, int IndividualId)
        {
            LogingHelper.SaveAuditInfo(Key);

            IndividualCECourseResponseRequest objResponse = new IndividualCECourseResponseRequest();
            IndividualCECourseBAL objBAL = new IndividualCECourseBAL();
            IndividualCECourseResponse objEntity = new IndividualCECourseResponse();
            List<IndividualCECourseResponse> lstEntity = new List<IndividualCECourseResponse>();
            List<IndividualCECourse> lstIndividualCECourse = new List<IndividualCECourse>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.IndividualCECourseResponseList = null;
                    return objResponse;
                }

                lstIndividualCECourse = objBAL.Get_IndividualCECourse_By_IndividualId(IndividualId);
                if (lstIndividualCECourse != null && lstIndividualCECourse.Count > 0)
                {

                    List<IndividualCECourseResponse> lstCeCourseResponse = lstIndividualCECourse.Select(obj => new IndividualCECourseResponse
                    {
                        ActivityDesc = obj.ActivityDesc,
                        ApplicationId = obj.ApplicationId,
                        CECourseActivityTypeId = obj.CECourseActivityTypeId,
                        CECourseDate = obj.CECourseDate,
                        CECourseDueDate = obj.CECourseDueDate,
                        CECourseEndDate = obj.CECourseEndDate,
                        CECourseHours = obj.CECourseHours,
                        CECourseReportingYear = obj.CECourseReportingYear,
                        CECourseStartDate = obj.CECourseStartDate,
                        CECourseStatusId = obj.CECourseStatusId,
                        CECourseTypeId = obj.CECourseTypeId,
                        CECourseUnits = obj.CECourseUnits,
                        CourseNameTitle = obj.CourseNameTitle,
                        CourseSponsor = obj.CourseSponsor,
                        IndividualCECourseId = obj.IndividualCECourseId,
                        IndividualLicenseId = obj.IndividualLicenseId,
                        IndividualId = obj.IndividualId,
                        InstructorBiography = obj.InstructorBiography,
                        IsActive = obj.IsActive,
                        ProgramSponsor = obj.ProgramSponsor,
                        ReferenceNumber = obj.ReferenceNumber
                    }).ToList();


                    objResponse.IndividualCECourseResponseList = lstCeCourseResponse;

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.IndividualCECourseResponseList = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualEducationBYIndividualId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualCECourseResponseList = null;

            }
            return objResponse;
        }

        #endregion

        #region IndividualEmployment_Save


        /// <summary>
        /// Save or Update the data For IndividualEmployment
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objIndividualEmployment">Object of IndividualEmployment</param>
        [AcceptVerbs("POST")]
        [ActionName("IndividualEmploymentSave")]
        public IndividualEmploymentResponseRequest IndividualEmploymentSave(string Key, IndividualEmploymentResponse objIndividualEmployment)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo(Key);

            IndividualEmploymentResponseRequest objResponse = new IndividualEmploymentResponseRequest();
            IndividualEmploymentBAL objBAL = new IndividualEmploymentBAL();
            IndividualEmployment objEntity = new IndividualEmployment();
            List<IndividualEmploymentResponse> lstEntity = new List<IndividualEmploymentResponse>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.IndividualEmploymentResponseList = null;
                return objResponse;
            }
            try
            {

                try
                {
                    if (System.Web.HttpContext.Current.IsDebuggingEnabled)
                    {
                        // this is executed only in the debug version
                        string requestStr = Newtonsoft.Json.JsonConvert.SerializeObject(objIndividualEmployment);
                        LogingHelper.SaveRequestJson(requestStr, "Save Individual Employment Request");
                    }

                }
                catch (Exception ex)
                {
                    LogingHelper.SaveExceptionInfo(Key, ex, "SaveIndividualEmployment object serialization", ENTITY.Enumeration.eSeverity.Critical);
                }
                string ValidationResponse = "";// IndividualEmploymentValidate.ValidateIndividualEmploymentObject(objIndividualEmployment);

                if (!string.IsNullOrEmpty(ValidationResponse))
                {
                    objResponse.Message = "Validation Error";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }

                if (objIndividualEmployment != null)
                {
                    return IndividualEmploymentCS.SaveIndividualEmployment(TokenHelper.GetTokenByKey(Key), objIndividualEmployment);
                }
                else
                {
                    objResponse.Message = "Individual Employment object cannot be null.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualEmploymentSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.IndividualEmploymentResponseList = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }



        /// <summary>
        /// Get Method to get IndividualEmployment by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="IndividualId">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("IndividualEmploymentByIndividualId")]
        public IndividualEmploymentResponseRequest IndividualEmploymentByIndividualId(string Key, int IndividualId)
        {
            LogingHelper.SaveAuditInfo(Key);

            IndividualEmploymentResponseRequest objResponse = new IndividualEmploymentResponseRequest();
            IndividualEmploymentBAL objBAL = new IndividualEmploymentBAL();
            IndividualCECourseResponse objEntity = new IndividualCECourseResponse();
            List<IndividualEmploymentResponse> lstEmploymentResponse = new List<IndividualEmploymentResponse>();
            List<IndividualEmployment> lstIndividualEmployment = new List<IndividualEmployment>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.IndividualEmploymentResponseList = null;
                    return objResponse;
                }

                lstIndividualEmployment = objBAL.Get_IndividualEmployment_by_IndividualId(IndividualId);
                if (lstIndividualEmployment != null && lstIndividualEmployment.Count > 0)
                {


                    lstEmploymentResponse = lstIndividualEmployment.Select(obj => new IndividualEmploymentResponse
                    {
                        ApplicationId = obj.ApplicationId,
                        EmploymentAddress = obj.EmploymentAddress,
                        EmploymentContact = obj.EmploymentContact,
                        EmploymentEndDate = obj.EmploymentEndDate,
                        EmploymentHistoryTypeId = obj.EmploymentHistoryTypeId,
                        EmploymentStartDate = obj.EmploymentStartDate,
                        EmploymentStatusId = obj.EmploymentStatusId,
                        EmploymentTypeId = obj.EmploymentTypeId,
                        EverWorkedinFieldofApplication = obj.EverWorkedinFieldofApplication,
                        IndividualEmploymentId = obj.IndividualEmploymentId,
                        IndividualId = obj.IndividualId,
                        IsActive = obj.IsActive,
                        IsWorkinginFieldofApplication = obj.IsWorkinginFieldofApplication,
                        PositionId = obj.PositionId,
                        ProviderId = obj.ProviderId,
                        ReferenceNumber = obj.ReferenceNumber,
                        Role = obj.Role,
                        EmployerName = obj.EmployerName

                    }).ToList();

                    objResponse.IndividualEmploymentResponseList = lstEmploymentResponse;

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");


                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.IndividualEmploymentResponseList = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualEmploymentByIndividualId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualEmploymentResponseList = null;

            }
            return objResponse;
        }


        #endregion

        #region IndividualLicenseSave

        /// <summary>
        /// Get Method to get IndividualLicense by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="IndividualId">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("IndividualLicenseBYIndividualId")]
        public IndividualLicenseResponseRequest IndividualLicenseBYIndividualId(string Key, int IndividualId)
        {
            LogingHelper.SaveAuditInfo(Key);

            IndividualLicenseResponseRequest objResponse = new IndividualLicenseResponseRequest();
            IndividualLicenseBAL objIndividualLicenseBAL = new IndividualLicenseBAL();
            IndividualLicenseResponse objIndividualLicenseResponse = new IndividualLicenseResponse();
            IndividualLicense objIndividualLicense = new IndividualLicense();
            List<IndividualLicenseResponse> lstIndividualLicenseResponse = new List<IndividualLicenseResponse>();
            List<IndividualLicense> lstIndividualLicense = new List<IndividualLicense>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.IndividualLicenseList = null;
                    return objResponse;
                }

                lstIndividualLicense = objIndividualLicenseBAL.Get_IndividualLicense_By_IndividualId(IndividualId);
                if (lstIndividualLicense != null && lstIndividualLicense.Count > 0)
                {
                    List<IndividualLicenseResponse> lstLicenseResponse = lstIndividualLicense.Select(obj => new IndividualLicenseResponse
                    {
                        ApplicationId = obj.ApplicationId,
                        ApplicationTypeId = obj.ApplicationTypeId,
                        IndividualId = obj.IndividualId,
                        IndividualLicenseId = obj.IndividualLicenseId,
                        IsActive = obj.IsActive,
                        IsLicenseActive = obj.IsLicenseActive,
                        IsLicenseTemporary = obj.IsLicenseTemporary,
                        LicenseEffectiveDate = obj.LicenseEffectiveDate,
                        LicenseExpirationDate = obj.LicenseExpirationDate,
                        LicenseNumber = obj.LicenseNumber,
                        LicenseStatusTypeId = obj.LicenseStatusTypeId,
                        LicenseTypeId = obj.LicenseTypeId,
                        OriginalLicenseDate = obj.OriginalLicenseDate,
                        LicenseStatusTypeCode = obj.LicenseStatusTypeCode,
                        LicenseTypeName = obj.LicenseTypeName,
                        LicenseStatusTypeName = obj.LicenseStatusTypeName,
                        LicenseStatusColorCode = obj.LicenseStatusColorCode

                    }).ToList();

                    objResponse.IndividualLicenseList = lstLicenseResponse;

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.IndividualLicenseList = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualLicenseBYIndividualId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualLicenseList = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Get Method to get IndividualLicenseDetail by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="IndividualId">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("IndividualLicenseDetailBYIndividualId")]
        public IndividualLicenseResponseRequest IndividualLicenseDetailBYIndividualId(string Key, int IndividualId)
        {
            LogingHelper.SaveAuditInfo(Key);

            IndividualLicenseResponseRequest objResponse = new IndividualLicenseResponseRequest();
            IndividualLicenseBAL objIndividualLicenseBAL = new IndividualLicenseBAL();
            IndividualLicenseResponse objIndividualLicenseResponse = new IndividualLicenseResponse();
            IndividualLicense objIndividualLicense = new IndividualLicense();
            List<IndividualLicenseResponse> lstIndividualLicenseResponse = new List<IndividualLicenseResponse>();
            List<IndividualLicense> lstIndividualLicense = new List<IndividualLicense>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.IndividualLicenseList = null;
                    return objResponse;
                }

                lstIndividualLicense = objIndividualLicenseBAL.GetALL_IndividualLicense_By_IndividualId(IndividualId);
                if (lstIndividualLicense != null && lstIndividualLicense.Count > 0)
                {
                    List<IndividualLicenseResponse> lstLicenseResponse = lstIndividualLicense.Select(obj => new IndividualLicenseResponse
                    {
                        ApplicationId = obj.ApplicationId,
                        ApplicationTypeId = obj.ApplicationTypeId,
                        IndividualId = obj.IndividualId,
                        IndividualLicenseId = obj.IndividualLicenseId,
                        IsActive = obj.IsActive,
                        IsLicenseActive = obj.IsLicenseActive,
                        IsLicenseTemporary = obj.IsLicenseTemporary,
                        LicenseEffectiveDate = obj.LicenseEffectiveDate,
                        LicenseExpirationDate = obj.LicenseExpirationDate,
                        LicenseNumber = obj.LicenseNumber,
                        LicenseStatusTypeId = obj.LicenseStatusTypeId,
                        LicenseTypeId = obj.LicenseTypeId,
                        OriginalLicenseDate = obj.OriginalLicenseDate,
                        LicenseStatusTypeCode = obj.LicenseStatusTypeCode,
                        LicenseTypeName = obj.LicenseTypeName,
                        LicenseStatusTypeName = obj.LicenseStatusTypeName,
                        LicenseStatusColorCode = obj.LicenseStatusColorCode,
                        LicenseDetail = obj.LicenseDetail

                    }).ToList();

                    objResponse.IndividualLicenseList = lstLicenseResponse;

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.IndividualLicenseList = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualLicenseDetailBYIndividualId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualLicenseList = null;

            }
            return objResponse;
        }



        /// <summary>
        /// Save or Update the data For IndividualLicense
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objIndividualLicense">Object of IndividualLicense</param>
        [AcceptVerbs("POST")]
        [ActionName("IndividualLicenseSave")]
        public IndividualLicenseResponseRequest IndividualLicenseSave(string Key, IndividualLicenseResponse objIndividualLicense)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo(Key);

            IndividualLicenseResponseRequest objResponse = new IndividualLicenseResponseRequest();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.IndividualLicenseList = null;
                return objResponse;
            }
            try
            {
                try
                {
                    if (System.Web.HttpContext.Current.IsDebuggingEnabled)
                    {
                        // this is executed only in the debug version
                        string requestStr = Newtonsoft.Json.JsonConvert.SerializeObject(objIndividualLicense);
                        LogingHelper.SaveRequestJson(requestStr, "IndividualLicenseSave Request");
                    }

                }
                catch (Exception ex)
                {
                    LogingHelper.SaveExceptionInfo(Key, ex, "IndividualLicenseSave object serialization", ENTITY.Enumeration.eSeverity.Critical);
                }

                string ValidationResponse = "";// IndividualLicenseAddressValidate.ValidateIndividualLicenseAddressObject(objIndividualLicenseAddress);

                if (!string.IsNullOrEmpty(ValidationResponse))
                {
                    objResponse.Message = "Validation Error";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }

                if (objIndividualLicense != null)
                {
                    return IndividualLicenseCS.SaveIndividualLicense(TokenHelper.GetTokenByKey(Key), objIndividualLicense);
                }
                else
                {
                    objResponse.Message = "Individual License object cannot be null.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualLicenseSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.IndividualLicenseList = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }


        #endregion

        #region IndividualDocumentSave


        /// <summary>
        /// Get Method to get IndividualDocument by key and IndividualId.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="IndividualId">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("IndividualDocumentGetByIndividualId")]
        public IndividualDocumentGetResponse IndividualDocumentGetByIndividualId(string Key, int IndividualId)
        {
            LogingHelper.SaveAuditInfo(Key);

            IndividualDocumentGetResponse objResponse = new IndividualDocumentGetResponse();

            IndividualDocumentBAL objIndividualDocumentBAL = new IndividualDocumentBAL();
            IndividualDocumentGet objIndividualDocumentGet = new IndividualDocumentGet();
            IndividualDocument objIndividualDocument = new IndividualDocument();
            List<IndividualDocumentGet> lstIndividualDocumentGet = new List<IndividualDocumentGet>();
            List<IndividualDocument> lstIndividualDocument = new List<IndividualDocument>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.IndividualDocumentGetList = null;
                    return objResponse;
                }

                lstIndividualDocument = objIndividualDocumentBAL.Get_IndividualDocument_by_IndividualId(IndividualId);
                if (lstIndividualDocument != null && lstIndividualDocument.Count > 0)
                {
                    lstIndividualDocumentGet = lstIndividualDocument.Select(obj => new IndividualDocumentGet
                    {
                        IndividualDocumentId = obj.IndividualDocumentId,
                        IndividualId = obj.IndividualId,
                        ApplicationId = obj.ApplicationId,
                        DocumentLkToPageTabSectionId = obj.DocumentLkToPageTabSectionId,
                        DocumentLkToPageTabSectionCode = obj.DocumentLkToPageTabSectionCode,
                        DocumentTypeName = obj.DocumentTypeName,
                        EffectiveDate = obj.EffectiveDate,
                        EndDate = obj.EndDate,
                        IsDocumentUploadedbyIndividual = obj.IsDocumentUploadedbyIndividual,
                        IsDocumentUploadedbyStaff = obj.IsDocumentUploadedbyStaff,
                        ReferenceNumber = "",
                        IsActive = obj.IsActive,
                        DocumentId = obj.DocumentId,
                        DocumentCd = obj.DocumentCd,
                        DocumentTypeId = obj.DocumentTypeId,
                        DocumentName = obj.DocumentName,
                        OtherDocumentTypeName = obj.OtherDocumentTypeName

                    }).ToList();

                    objResponse.IndividualDocumentGetList = lstIndividualDocumentGet;

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.IndividualDocumentGetList = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualDocumentGetByIndividualId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualDocumentGetList = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Get Method to get IndividualDocument by key IndividualId and ApplicationId.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="IndividualId">Record ID.</param>
        /// <param name="ApplicationId">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("IndividualDocumentGetByIndividualIdAndApplicationId")]
        public IndividualDocumentGetResponse IndividualDocumentGetByIndividualIdAndApplicationId(string Key, int IndividualId, int ApplicationId)
        {
            LogingHelper.SaveAuditInfo(Key);

            IndividualDocumentGetResponse objResponse = new IndividualDocumentGetResponse();

            IndividualDocumentBAL objIndividualDocumentBAL = new IndividualDocumentBAL();
            IndividualDocumentGet objIndividualDocumentGet = new IndividualDocumentGet();
            IndividualDocument objIndividualDocument = new IndividualDocument();
            List<IndividualDocumentGet> lstIndividualDocumentGet = new List<IndividualDocumentGet>();
            List<IndividualDocument> lstIndividualDocument = new List<IndividualDocument>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.IndividualDocumentGetList = null;
                    return objResponse;
                }

                lstIndividualDocument = objIndividualDocumentBAL.Get_IndividualDocument_by_IndividualIdAndApplicationId(IndividualId, ApplicationId);
                if (lstIndividualDocument != null && lstIndividualDocument.Count > 0)
                {
                    lstIndividualDocumentGet = lstIndividualDocument.Select(obj => new IndividualDocumentGet
                    {
                        IndividualDocumentId = obj.IndividualDocumentId,
                        IndividualId = obj.IndividualId,
                        ApplicationId = obj.ApplicationId,
                        DocumentLkToPageTabSectionId = obj.DocumentLkToPageTabSectionId,
                        DocumentLkToPageTabSectionCode = obj.DocumentLkToPageTabSectionCode,
                        DocumentTypeName = obj.DocumentTypeName,
                        EffectiveDate = obj.EffectiveDate,
                        EndDate = obj.EndDate,
                        IsDocumentUploadedbyIndividual = obj.IsDocumentUploadedbyIndividual,
                        IsDocumentUploadedbyStaff = obj.IsDocumentUploadedbyStaff,
                        ReferenceNumber = obj.ReferenceNumber,
                        IsActive = obj.IsActive,
                        DocumentId = obj.DocumentId,
                        DocumentCd = obj.DocumentCd,
                        DocumentTypeId = obj.DocumentTypeId,
                        DocumentName = obj.DocumentName,
                        OtherDocumentTypeName = obj.OtherDocumentTypeName

                    }).ToList();

                    objResponse.IndividualDocumentGetList = lstIndividualDocumentGet;

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.IndividualDocumentGetList = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualDocumentGetByIndividualIdAndApplicationId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualDocumentGetList = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Delete Method to get IndividualDocument by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ID">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("IndividualDocumentDeleteByID")]
        public IndividualDocumentGetResponse IndividualDocumentDeleteByID(string Key, int ID)
        {
            LogingHelper.SaveAuditInfo(Key);
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

            IndividualDocumentGetResponse objResponse = new IndividualDocumentGetResponse();

            IndividualDocumentBAL objIndividualDocumentBAL = new IndividualDocumentBAL();
            IndividualDocumentGet objIndividualDocumentGet = new IndividualDocumentGet();
            IndividualDocument objIndividualDocument = new IndividualDocument();
            List<IndividualDocumentGet> lstIndividualDocumentGet = new List<IndividualDocumentGet>();
            List<IndividualDocument> lstIndividualDocument = new List<IndividualDocument>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.IndividualDocumentGetList = null;
                    return objResponse;
                }

                objIndividualDocument = objIndividualDocumentBAL.Get_IndividualDocument_By_IndividualDocumentId(ID);
                if (objIndividualDocument != null)
                {
                    objIndividualDocument.IsDeleted = true;
                    objIndividualDocument.ModifiedBy = CreatedOrMoifiy;
                    objIndividualDocument.ModifiedOn = DateTime.Now;
                    //objIndividualDocument.DocumentPath = "";
                    objIndividualDocumentBAL.Save_IndividualDocument(objIndividualDocument);

                    objResponse.Message = Messages.DeleteSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }
                else
                {
                    objResponse.Message = "No record found to delete.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                }
                objResponse.IndividualDocumentGetList = null;

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualDocumentDeleteByID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualDocumentGetList = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Save or Update the data For IndividualDocument
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objIndividualDocumentResponse">Object of IndividualDocument</param>
        [AcceptVerbs("POST")]
        [ActionName("IndividualDocumentSave")]
        public IndividualDocumentUploadResponse IndividualDocumentSave(string Key, IndividualDocumentUpload objIndividualDocumentResponse)
        {
            IndividualDocumentUploadResponse objResponse = new IndividualDocumentUploadResponse();
            IndividualDocumentBAL objIndividualDocumentBAL = new IndividualDocumentBAL();
            IndividualDocument objIndividualDocument = new IndividualDocument();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.IndividualDocumentUploadList = null;
                return objResponse;
            }


            try
            {
                if (System.Web.HttpContext.Current.IsDebuggingEnabled)
                {
                    // this is executed only in the debug version
                    string requestStr = Newtonsoft.Json.JsonConvert.SerializeObject(objIndividualDocumentResponse);
                    LogingHelper.SaveRequestJson("none only capture Transaction Id", ("Document upload object-" + objIndividualDocumentResponse.TransactionId.ToString()));
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, ("Document upload object failed- " + objIndividualDocumentResponse.TransactionId.ToString()), ENTITY.Enumeration.eSeverity.Critical);
            }


            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

            List<IndividualDocumentUpload> lstIndividualDocumentUpload = new List<IndividualDocumentUpload>();
            IndividualDocumentUpload objIndividualDocumentUpload = new IndividualDocumentUpload();
            List<DocumentToUpload> lstDocumentToUpload = new List<DocumentToUpload>();
            List<DocumentToUpload> lstDocumentToUploadNEW = new List<DocumentToUpload>();
            lstDocumentToUpload = objIndividualDocumentResponse.DocumentUploadList;

            LogingHelper.SaveAuditInfo(Key);

            try
            {
                string ValidationResponse = IndividualValidations.ValidateIndividualDocument(lstDocumentToUpload, objIndividualDocumentResponse.IndividualId);

                if (!string.IsNullOrEmpty(ValidationResponse))
                {
                    objResponse.Message = "Validation Error";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }

                if (objIndividualDocumentResponse != null)
                {
                    return SaveIndividualDocument(TokenHelper.GetTokenByKey(Key), objIndividualDocumentResponse);
                }
                else
                {
                    objResponse.Message = "Individual Document object cannot be null.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }
            }

            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualDocumentSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.IndividualDocumentUploadList = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }
        #region  Individual Document Process

        private static IndividualDocumentUploadResponse SaveIndividualDocument(Token objToken, IndividualDocumentUpload objIndividualDocumentResponse)
        {
            IndividualDocumentUploadResponse objResponse = new IndividualDocumentUploadResponse();
            IndividualDocumentBAL objIndividualDocumentBAL = new IndividualDocumentBAL();
            IndividualDocument objIndividualDocument = new IndividualDocument();
            string FilePath = ConfigurationHelper.GetConfigurationValueBySetting("RootDocumentPath") + "Renewal\\";
            int ErrNo = 0;

            int CreatedOrMoifiy = objToken.UserId;

            List<string> SaveErrorList = new List<string>();

            List<Attachment> lstAttachment = new List<Attachment>();

            List<IndividualDocumentUpload> lstIndividualDocumentUpload = new List<IndividualDocumentUpload>();
            IndividualDocumentUpload objIndividualDocumentUpload = new IndividualDocumentUpload();
            List<DocumentToUpload> lstDocumentToUpload = new List<DocumentToUpload>();
            List<DocumentToUpload> lstDocumentToUploadNEW = new List<DocumentToUpload>();
            lstDocumentToUpload = objIndividualDocumentResponse.DocumentUploadList;

            try
            {

                int IndividualId = objIndividualDocumentResponse.IndividualId;
                int? ApplicationId = objIndividualDocumentResponse.ApplicationId;

                if (lstDocumentToUpload != null && lstDocumentToUpload.Count > 0)
                {
                    foreach (DocumentToUpload objDtU in lstDocumentToUpload)
                    {

                        try
                        {
                            int individualId = objIndividualDocumentResponse.IndividualId;
                            int? applicationId = objIndividualDocumentResponse.ApplicationId;


                            objIndividualDocument = new IndividualDocument();

                            objIndividualDocument.IndividualId = IndividualId;
                            objIndividualDocument.ApplicationId = ApplicationId;
                            objIndividualDocument.DocumentLkToPageTabSectionId = objDtU.DocumentLkToPageTabSectionId;
                            objIndividualDocument.DocumentLkToPageTabSectionCode = objDtU.DocumentLkToPageTabSectionCode;

                            objIndividualDocument.DocumentTypeName = objDtU.DocumentTypeName;
                            objIndividualDocument.DocumentPath = "";
                            objIndividualDocument.EffectiveDate = objDtU.EffectiveDate;
                            objIndividualDocument.EndDate = objDtU.EndDate;
                            objIndividualDocument.IsDocumentUploadedbyIndividual = objDtU.IsDocumentUploadedbyIndividual;
                            objIndividualDocument.IsDocumentUploadedbyStaff = objDtU.IsDocumentUploadedbyStaff;
                            objIndividualDocument.ReferenceNumber = objDtU.ReferenceNumber;
                            objIndividualDocument.IsActive = true;
                            objIndividualDocument.IsDeleted = true;
                            objIndividualDocument.CreatedBy = CreatedOrMoifiy;
                            objIndividualDocument.CreatedOn = DateTime.Now;
                            objIndividualDocument.ModifiedOn = null;
                            objIndividualDocument.ModifiedBy = null;
                            objIndividualDocument.IndividualDocumentGuid = Guid.NewGuid().ToString();

                            objIndividualDocument.DocumentId = objDtU.DocumentId;
                            objIndividualDocument.DocumentCd = objDtU.DocumentCd;
                            objIndividualDocument.DocumentTypeId = objDtU.DocumentTypeId;
                            objIndividualDocument.DocumentName = objDtU.DocNameWithExtention;
                            objIndividualDocument.OtherDocumentTypeName = objDtU.OtherDocumentTypeName;

                            if (objIndividualDocument != null)
                            {
                                objIndividualDocument.IndividualDocumentId = objIndividualDocumentBAL.Save_IndividualDocument(objIndividualDocument);
                                objDtU.IndividualDocumentId = objIndividualDocument.IndividualDocumentId;


                                string DocFileName = objIndividualDocument.IndividualDocumentId + "-" + objDtU.DocNameWithExtention; // Guid.NewGuid().ToString() + ".pdf";
                                string DocPath = FileHelper.Base64ToFile(objDtU.DocStrBase64, FilePath + DocFileName); // (FilePath + DocFileName);

                                objDtU.DocNameWithExtention = DocFileName;
                                objIndividualDocument.DocumentPath = DocPath;
                                objIndividualDocument.IsDeleted = false;
                                objIndividualDocumentBAL.Save_IndividualDocument(objIndividualDocument);
                                // objIndividualDocumentUpload = new IndividualDocumentUpload();

                                lstDocumentToUploadNEW.Add(objDtU);

                                Attachment objAttachment = new Attachment(DocPath);
                                lstAttachment.Add(objAttachment);


                                //SAVE LOG

                                string logText = "Individual Document uploaded successfully. Document Type Name " + objDtU.DocumentTypeName + ". Uploaded on " + DateTime.Now.ToShortDateString();
                                string logSource = eCommentLogSource.WSAPI.ToString();
                                LogHelper.SaveIndividualLog(individualId, applicationId, logSource, logText, objToken.UserId, null, null, null);

                                //END SAVE LOG
                            }

                        }
                        catch (Exception ex)
                        {
                            LogingHelper.SaveExceptionInfo("", ex, "IndividualDocumentSaveForeach", ENTITY.Enumeration.eSeverity.Error);
                            string ErrMes = ex.Message;
                            ErrNo = ErrNo + 1;
                            SaveErrorList.Add(ErrMes);
                        }
                    }
                    // If error occurred
                    if (SaveErrorList.Count > 0)
                    {
                        objResponse.Status = false;
                        if (ErrNo > 0 && ErrNo != lstDocumentToUpload.Count)
                        {
                            objResponse.Message = "Saved with error.";
                            objIndividualDocumentUpload.DocumentUploadList = lstDocumentToUploadNEW;

                        }
                        else
                        {
                            objResponse.Message = "Error occurred while saving.";
                            objIndividualDocumentUpload.DocumentUploadList = null;
                        }
                        objIndividualDocumentUpload.ApplicationId = ApplicationId;
                        objIndividualDocumentUpload.IndividualId = IndividualId;


                        lstIndividualDocumentUpload.Add(objIndividualDocumentUpload);

                        objResponse.IndividualDocumentUploadList = lstIndividualDocumentUpload;
                        objResponse.ResponseReason = GeneralFunctions.GetJsonStringFromList(SaveErrorList);
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                        return objResponse;
                    }

                    //Success

                    else
                    {
                        objResponse.Message = MessagesClass.SaveSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }
                }
                else
                {
                    objResponse.Message = "No data to upload.";
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.IndividualDocumentUploadList = null;
                    return objResponse;
                }

                objIndividualDocumentUpload.ApplicationId = ApplicationId;
                objIndividualDocumentUpload.IndividualId = IndividualId;
                objIndividualDocumentUpload.DocumentUploadList = lstDocumentToUploadNEW;
                objIndividualDocumentUpload.Email = objIndividualDocumentResponse.Email;
                objIndividualDocumentUpload.SendEmail = objIndividualDocumentResponse.SendEmail;
                lstIndividualDocumentUpload.Add(objIndividualDocumentUpload);

                objResponse.Status = true;
                objResponse.IndividualDocumentUploadList = lstIndividualDocumentUpload;

                if (objIndividualDocumentResponse.SendEmail)
                {
                    if (lstAttachment.Count == 0)
                    {
                        objResponse.Status = false;
                        objResponse.Message = "No file to send.";
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                        objResponse.IndividualDocumentUploadList = null;
                        return objResponse;
                    }
                    List<ResponseReason> lstResponseReason = new List<ResponseReason>();
                    lstResponseReason = Validations.IsValidEmailProperty(nameof(objIndividualDocumentResponse.Email), objIndividualDocumentResponse.Email, lstResponseReason);
                    if (lstResponseReason != null && lstResponseReason.Count > 0)
                    {
                        objResponse.Status = false;
                        objResponse.Message = "Validation error";
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                        objResponse.IndividualDocumentUploadList = null;
                        objResponse.ResponseReason = GeneralFunctions.GetJsonStringFromList(lstResponseReason);
                        return objResponse;

                    }

                    if (objIndividualDocumentResponse.SendEmail)
                    {
                        #region Process Email Content

                        string EmailTemplate = File.ReadAllText(HttpContext.Current.Server.MapPath("~/EmailTemplate/RenewalConfirmation.html"));
                        Application objApplication = new Application();
                        ApplicationBAL objApplicationBAL = new ApplicationBAL();
                        Individual objIndividual = new Individual();
                        IndividualBAL objIndividualBAL = new IndividualBAL();
                        objIndividual = objIndividualBAL.Get_Individual_By_IndividualId(IndividualId);
                        if (objIndividual != null)
                        {
                            EmailTemplate = EmailTemplate.Replace("#FullName#", objIndividual.FirstName + " " + objIndividual.LastName);
                            EmailTemplate = EmailTemplate.Replace("#Date#", DateTime.Now.ToShortDateString());



                            objApplication = objApplicationBAL.Get_Application_By_ApplicationId(Convert.ToInt32(ApplicationId));
                            if (objApplication != null)
                            {
                                decimal Amount = 0;
                                List<RevFeeDisb> lstFeeDisb = new List<RevFeeDisb>();
                                RevFeeDisbBAL objFeeDisbBAL = new RevFeeDisbBAL();
                                lstFeeDisb = objFeeDisbBAL.Get_RevFeeDisb_by_TransactionId(objIndividualDocumentResponse.TransactionId);
                                if (lstFeeDisb != null)
                                {

                                    foreach (RevFeeDisb objFeedisb in lstFeeDisb)
                                    {
                                        Amount += objFeedisb.FeePaidAmount;

                                    }
                                }

                                EmailTemplate = EmailTemplate.Replace("#AmountPaid#", String.Format("{0:C}", Amount));
                                EmailTemplate = EmailTemplate.Replace("#ApplicationNumber#", objApplication.ApplicationNumber);


                                IndividualLicense objLatestLicense = new IndividualLicense();
                                IndividualLicenseBAL objIndividualLicenseBAL = new IndividualLicenseBAL();

                                objLatestLicense = objIndividualLicenseBAL.Get_Latest_IndividualLicense_By_IndividualId(IndividualId);
                                if (objLatestLicense != null)
                                {
                                    EmailTemplate = EmailTemplate.Replace("#LicenseNumber#", objLatestLicense.LicenseNumber);


                                }
                                if (EmailHelper.SendMailWithMultipleAttachment(objIndividualDocumentResponse.Email, "Online License Renewal and Payment", EmailTemplate, true, lstAttachment))
                                {
                                    LogHelper.LogCommunication(objIndividual.IndividualId, objApplication.ApplicationId, eCommunicationType.Email, "Renewal Confirmation", eCommunicationStatus.Success, "Public", "Renewal Confirmation email has been sent", EmailHelper.GetSenderAddress(), objIndividualDocumentResponse.Email, null, null, objToken.UserId, null, null, null);
                                }
                                else
                                {
                                    LogHelper.LogCommunication(objIndividual.IndividualId, objApplication.ApplicationId, eCommunicationType.Email, "Online License Renewal and Payment", eCommunicationStatus.Fail, "Public", "Renewal Confirmation email sending failed.", EmailHelper.GetSenderAddress(), objIndividualDocumentResponse.Email, null, null, objToken.UserId, null, null, null);
                                }

                            }
                        }
                        #endregion

                    }

                }
            }
            catch (Exception ex)
            {

                LogingHelper.SaveExceptionInfo("", ex, "SaveIndividualDocument", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualDocumentUploadList = null;
            }
            return objResponse;


        }

        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="objIndividualDocumentResponse"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("IndividualDocumentSaveByHTML")]
        public IndividualDocumentByHtmlResponse IndividualDocumentSaveByHTML(string Key, IndividualDocumentByHTML objIndividualDocumentResponse)
        {
            IndividualDocumentByHtmlResponse objResponse = new IndividualDocumentByHtmlResponse();
            IndividualDocumentBAL objIndividualDocumentBAL = new IndividualDocumentBAL();
            IndividualDocument objIndividualDocument = new IndividualDocument();



            try
            {
                if (objIndividualDocumentResponse == null)
                    throw new Exception("Request object does not have valid JSON. Please compare with API signature");


                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.IndividualDocumentUploadList = null;
                    return objResponse;
                }

                int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

                List<IndividualDocumentByHTML> lstIndividualDocumentUpload = new List<IndividualDocumentByHTML>();
                IndividualDocumentByHTML objIndividualDocumentUpload = new IndividualDocumentByHTML();
                List<DocumentToUploadByHTML> lstDocumentToUpload = new List<DocumentToUploadByHTML>();
                List<DocumentToUploadByHTML> lstDocumentToUploadNEW = new List<DocumentToUploadByHTML>();
                lstDocumentToUpload = objIndividualDocumentResponse.DocumentUploadList;

                LogingHelper.SaveAuditInfo(Key);

                try
                {
                    if (System.Web.HttpContext.Current.IsDebuggingEnabled)
                    {
                        // this is executed only in the debug version
                        string requestStr = Newtonsoft.Json.JsonConvert.SerializeObject(objIndividualDocumentResponse);
                        LogingHelper.SaveRequestJson(requestStr, ("HTML Document upload object  -" + objIndividualDocumentResponse.TransactionId.ToString()));
                    }

                }
                catch (Exception ex)
                {
                    LogingHelper.SaveExceptionInfo(Key, ex, (" HTML Document upload object failed- " + objIndividualDocumentResponse.TransactionId.ToString()), ENTITY.Enumeration.eSeverity.Critical);
                }


                //string ValidationResponse = IndividualValidations.ValidateIndividualDocument(lstDocumentToUpload, objIndividualDocumentResponse.IndividualId);

                //if (!string.IsNullOrEmpty(ValidationResponse))
                //{
                //    objResponse.Message = "Validation Error";
                //    objResponse.Status = false;
                //    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                //    objResponse.ResponseReason = ValidationResponse;
                //    return objResponse;
                //}

                if (objIndividualDocumentResponse != null)
                {
                    return SaveIndividualDocumentHTML(TokenHelper.GetTokenByKey(Key), objIndividualDocumentResponse);
                }
                else
                {
                    objResponse.Message = "Individual Document object cannot be null.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = null;
                    return objResponse;
                }
            }

            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualDocumentGetByHTML", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.IndividualDocumentUploadList = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }
        #region  Individual Document Process

        private static IndividualDocumentByHtmlResponse SaveIndividualDocumentHTML(Token objToken, IndividualDocumentByHTML objIndividualDocumentResponse)
        {
            IndividualDocumentByHtmlResponse objResponse = new IndividualDocumentByHtmlResponse();
            IndividualDocumentBAL objIndividualDocumentBAL = new IndividualDocumentBAL();
            IndividualDocument objIndividualDocument = new IndividualDocument();
            string FilePath = ConfigurationHelper.GetConfigurationValueBySetting("RootDocumentPath") + "Renewal\\";
            int ErrNo = 0;

            int CreatedOrMoifiy = objToken.UserId;

            List<string> SaveErrorList = new List<string>();

            List<Attachment> lstAttachment = new List<Attachment>();

            List<IndividualDocumentByHTML> lstIndividualDocumentUpload = new List<IndividualDocumentByHTML>();
            IndividualDocumentByHTML objIndividualDocumentUpload = new IndividualDocumentByHTML();
            List<DocumentToUploadByHTML> lstDocumentToUpload = new List<DocumentToUploadByHTML>();
            List<DocumentToUploadByHTML> lstDocumentToUploadNEW = new List<DocumentToUploadByHTML>();
            lstDocumentToUpload = objIndividualDocumentResponse.DocumentUploadList;

            try
            {

                int IndividualId = objIndividualDocumentResponse.IndividualId;
                int? ApplicationId = objIndividualDocumentResponse.ApplicationId;

                if (lstDocumentToUpload != null && lstDocumentToUpload.Count > 0)
                {
                    foreach (DocumentToUploadByHTML objDtU in lstDocumentToUpload)
                    {

                        try
                        {
                            int individualId = objIndividualDocumentResponse.IndividualId;
                            int? applicationId = objIndividualDocumentResponse.ApplicationId;

                            objIndividualDocument = new IndividualDocument();

                            objIndividualDocument.IndividualId = IndividualId;
                            objIndividualDocument.ApplicationId = ApplicationId;
                            objIndividualDocument.DocumentLkToPageTabSectionId = objDtU.DocumentLkToPageTabSectionId;
                            objIndividualDocument.DocumentLkToPageTabSectionCode = objDtU.DocumentLkToPageTabSectionCode;

                            objIndividualDocument.DocumentTypeName = objDtU.DocumentTypeName;
                            objIndividualDocument.DocumentPath = "";
                            objIndividualDocument.EffectiveDate = objDtU.EffectiveDate;
                            objIndividualDocument.EndDate = objDtU.EndDate;
                            objIndividualDocument.IsDocumentUploadedbyIndividual = objDtU.IsDocumentUploadedbyIndividual;
                            objIndividualDocument.IsDocumentUploadedbyStaff = objDtU.IsDocumentUploadedbyStaff;
                            objIndividualDocument.ReferenceNumber = "";// objDtU.ReferenceNumber;
                            objIndividualDocument.IsActive = true;
                            objIndividualDocument.IsDeleted = true;
                            objIndividualDocument.CreatedBy = CreatedOrMoifiy;
                            objIndividualDocument.CreatedOn = DateTime.Now;
                            objIndividualDocument.ModifiedOn = null;
                            objIndividualDocument.ModifiedBy = null;
                            objIndividualDocument.IndividualDocumentGuid = Guid.NewGuid().ToString();

                            objIndividualDocument.DocumentId = objDtU.DocumentId;
                            objIndividualDocument.DocumentCd = objDtU.DocumentCd;
                            objIndividualDocument.DocumentTypeId = objDtU.DocumentTypeId;
                            objIndividualDocument.DocumentName = objDtU.DocNameWithExtention;
                            objIndividualDocument.OtherDocumentTypeName = objDtU.OtherDocumentTypeName;

                            if (objIndividualDocument != null)
                            {
                                objIndividualDocument.IndividualDocumentId = objIndividualDocumentBAL.Save_IndividualDocument(objIndividualDocument);
                                objDtU.IndividualDocumentId = objIndividualDocument.IndividualDocumentId;


                                string DocFileName = objIndividualDocument.IndividualDocumentId + "-" + objDtU.DocNameWithExtention;
                                string DocPath = FilePath + DocFileName;
                                EO.Pdf.Runtime.AddLicense("f6yywc2faLWRm8ufdabl/RfusLWRm8ufdeb29RDxguXqAMvjmuvpzs22aKi1wN2vaqqmsSHkq+rtABm8W6ymsdq9RoGksefyot7y8h/0q9zC6gPqnNHvxg34aav32PjOhuHZBPXWerTBzdryot7y8h/0q9zCnrWfWbOz/RTinuX39umMQ3Xj7fQQ7azcwp61n1mz8PoO5Kfq6doPvXCoucfbtWutucPnrqXg5/YZ8p7A6M+4iVmXwP0U4p7l9/YQn6fY8fbooZrh5QrNvXWm8PoO5Kfq6fbpjEOXpM0M66Xm+8+4iVmXwPIP41nr/QEQvFu807/745+ZpLEh5Kvq7QAZvFs=");


                                HtmlToPdf.Options.AutoFitX = HtmlToPdfAutoFitMode.ShrinkToFit;
                                HtmlToPdf.Options.PageSize = EO.Pdf.PdfPageSizes.A4;
                                HtmlToPdf.Options.OutputArea = new RectangleF(0.1f, 0.1f, 8.0f, 11.5f);
                                FileInfo fi = new FileInfo(DocPath);
                                if (fi.Exists)
                                {
                                    fi.Delete();
                                }
                                HtmlToPdf.ConvertHtml(objDtU.HtmlString, DocPath);
                                objDtU.DocNameWithExtention = DocFileName;
                                objDtU.HtmlString = "";

                                objIndividualDocument.DocumentPath = DocPath;
                                objIndividualDocument.IsDeleted = false;
                                objIndividualDocumentBAL.Save_IndividualDocument(objIndividualDocument);

                                lstDocumentToUploadNEW.Add(objDtU);

                                Attachment objAttachment = new Attachment(DocPath);
                                lstAttachment.Add(objAttachment);



                                //SAVE LOG

                                string logText = "Individual Document uploaded successfully. Document Type Name " + objDtU.DocumentTypeName + ". Uploaded on " + DateTime.Now.ToShortDateString();
                                string logSource = eCommentLogSource.WSAPI.ToString();
                                LogHelper.SaveIndividualLog(individualId, applicationId, logSource, logText, objToken.UserId, null, null, null);

                                //END SAVE LOG
                            }

                        }
                        catch (Exception ex)
                        {
                            LogingHelper.SaveExceptionInfo("", ex, "SaveIndividualDocumentHTMLSaveForeach", ENTITY.Enumeration.eSeverity.Error);
                            string ErrMes = ex.Message;
                            ErrNo = ErrNo + 1;
                            SaveErrorList.Add(ErrMes);
                        }
                    }
                    // If error occurred
                    if (SaveErrorList.Count > 0)
                    {
                        objResponse.Status = false;
                        if (ErrNo > 0 && ErrNo != lstDocumentToUpload.Count)
                        {
                            objResponse.Message = "Saved with error.";
                            objIndividualDocumentUpload.DocumentUploadList = lstDocumentToUploadNEW;

                        }
                        else
                        {
                            objResponse.Message = "Error occurred while saving.";
                            objIndividualDocumentUpload.DocumentUploadList = null;
                        }
                        objIndividualDocumentUpload.ApplicationId = ApplicationId;
                        objIndividualDocumentUpload.IndividualId = IndividualId;


                        lstIndividualDocumentUpload.Add(objIndividualDocumentUpload);

                        objResponse.IndividualDocumentUploadList = lstIndividualDocumentUpload;
                        objResponse.ResponseReason = GeneralFunctions.GetJsonStringFromList(SaveErrorList);
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                        return objResponse;
                    }

                    //Success

                    else
                    {
                        objResponse.Message = MessagesClass.SaveSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }
                }
                else
                {
                    objResponse.Message = "No data to upload.";
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.IndividualDocumentUploadList = null;
                    return objResponse;
                }

                objIndividualDocumentUpload.ApplicationId = ApplicationId;
                objIndividualDocumentUpload.IndividualId = IndividualId;
                objIndividualDocumentUpload.DocumentUploadList = lstDocumentToUploadNEW;
                objIndividualDocumentUpload.Email = objIndividualDocumentResponse.Email;
                objIndividualDocumentUpload.SendEmail = objIndividualDocumentResponse.SendEmail;
                lstIndividualDocumentUpload.Add(objIndividualDocumentUpload);

                objResponse.Status = true;
                objResponse.IndividualDocumentUploadList = lstIndividualDocumentUpload;

                if (objIndividualDocumentResponse.SendEmail)
                {
                    if (lstAttachment.Count == 0)
                    {
                        objResponse.Status = false;
                        objResponse.Message = "No file to send.";
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                        objResponse.IndividualDocumentUploadList = null;
                        return objResponse;
                    }
                    List<ResponseReason> lstResponseReason = new List<ResponseReason>();
                    lstResponseReason = Validations.IsValidEmailProperty(nameof(objIndividualDocumentResponse.Email), objIndividualDocumentResponse.Email, lstResponseReason);
                    if (lstResponseReason != null && lstResponseReason.Count > 0)
                    {
                        objResponse.Status = false;
                        objResponse.Message = "Validation error";
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                        objResponse.IndividualDocumentUploadList = null;
                        objResponse.ResponseReason = GeneralFunctions.GetJsonStringFromList(lstResponseReason);
                        return objResponse;

                    }


                    #region Process Email Content
                    if (objIndividualDocumentResponse.SendEmail)
                    {
                        string EmailTemplate = ""; // File.ReadAllText(HttpContext.Current.Server.MapPath("~/EmailTemplate/RenewalConfirmation.html"));
                        if (!string.IsNullOrEmpty(objIndividualDocumentResponse.AffirmativeAction) && objIndividualDocumentResponse.AffirmativeAction.ToUpper() == "Y")
                        {
                            EmailTemplate = File.ReadAllText(HttpContext.Current.Server.MapPath("~/EmailTemplate/ConfirmationEmailUponSuccessfulPaymentAffirmativeActionisY.html"));
                        }
                        else if (!string.IsNullOrEmpty(objIndividualDocumentResponse.AffirmativeAction) && objIndividualDocumentResponse.AffirmativeAction.ToUpper() == "N")
                        {
                            EmailTemplate = File.ReadAllText(HttpContext.Current.Server.MapPath("~/EmailTemplate/ConfirmationEmailUponSuccessfulPaymentAffirmativeActionisN.html"));
                        }
                        else
                        {
                            EmailTemplate = File.ReadAllText(HttpContext.Current.Server.MapPath("~/EmailTemplate/RenewalConfirmation.html"));
                        }

                        Application objApplication = new Application();
                        ApplicationBAL objApplicationBAL = new ApplicationBAL();
                        Individual objIndividual = new Individual();
                        IndividualBAL objIndividualBAL = new IndividualBAL();
                        objIndividual = objIndividualBAL.Get_Individual_By_IndividualId(IndividualId);
                        if (objIndividual != null)
                        {
                            EmailTemplate = EmailTemplate.Replace("#FullName#", objIndividual.FirstName + " " + objIndividual.LastName);
                            EmailTemplate = EmailTemplate.Replace("#Date#", DateTime.Now.ToShortDateString());



                            objApplication = objApplicationBAL.Get_Application_By_ApplicationId(Convert.ToInt32(ApplicationId));
                            if (objApplication != null)
                            {
                                decimal Amount = 0;
                                List<RevFeeDisb> lstFeeDisb = new List<RevFeeDisb>();
                                RevFeeDisbBAL objFeeDisbBAL = new RevFeeDisbBAL();
                                lstFeeDisb = objFeeDisbBAL.Get_RevFeeDisb_by_TransactionId(objIndividualDocumentResponse.TransactionId);
                                if (lstFeeDisb != null)
                                {

                                    foreach (RevFeeDisb objFeedisb in lstFeeDisb)
                                    {
                                        Amount += objFeedisb.FeePaidAmount;

                                    }
                                }

                                EmailTemplate = EmailTemplate.Replace("#AmountPaid#", String.Format("{0:C}", Amount));
                                EmailTemplate = EmailTemplate.Replace("#ApplicationNumber#", objApplication.ApplicationNumber);


                                IndividualLicense objLatestLicense = new IndividualLicense();
                                IndividualLicenseBAL objIndividualLicenseBAL = new IndividualLicenseBAL();

                                objLatestLicense = objIndividualLicenseBAL.Get_Latest_IndividualLicense_By_IndividualId(IndividualId);
                                if (objLatestLicense != null)
                                {
                                    EmailTemplate = EmailTemplate.Replace("#LicenseNumber#", objLatestLicense.LicenseNumber);


                                }
                                if (EmailHelper.SendMailWithMultipleAttachment(objIndividualDocumentResponse.Email, "Online License Renewal and Payment", EmailTemplate, true, lstAttachment))
                                {
                                    LogHelper.SaveIndividualLog(objIndividual.IndividualId, null, "Backoffice", ("Online License Renewal and Payment sent by email. Email Address: " + objIndividualDocumentResponse.Email + ", Sent On: " + DateTime.Now.ToString("MM/dd/yyyy")), 0, null, null, null);
                                    LogHelper.LogCommunication(objIndividual.IndividualId, null, eCommunicationType.Email, "Online License Renewal and Payment", eCommunicationStatus.Success, "Backoffice", EmailTemplate, EmailHelper.GetSenderAddress(), objIndividualDocumentResponse.Email, null, null, 0, null, null, null);

                                    //LogHelper.LogCommunication(objIndividual.IndividualId, objApplication.ApplicationId, eCommunicationType.Email, "Renewal Confirmation", eCommunicationStatus.Success, "Public", "Renewal Confirmation email has been sent", EmailHelper.GetSenderAddress(), objIndividualDocumentResponse.Email, null, null, objToken.UserId, null, null, null);
                                }
                                else
                                {
                                    LogHelper.SaveIndividualLog(objIndividual.IndividualId, null, "Backoffice", ("Online License Renewal and Payment sent by email failed. Email Address: " + objIndividualDocumentResponse.Email + ", Sent On: " + DateTime.Now.ToString("MM/dd/yyyy")), 0, null, null, null);
                                    LogHelper.LogCommunication(objIndividual.IndividualId, null, eCommunicationType.Email, "Online License Renewal and Payment", eCommunicationStatus.Fail, "Backoffice", EmailTemplate, EmailHelper.GetSenderAddress(), objIndividualDocumentResponse.Email, null, null, 0, null, null, null);

                                    //LogHelper.LogCommunication(objIndividual.IndividualId, objApplication.ApplicationId, eCommunicationType.Email, "Online License Renewal and Payment", eCommunicationStatus.Fail, "Public", "Renewal Confirmation email sending failed.", EmailHelper.GetSenderAddress(), objIndividualDocumentResponse.Email, null, null, objToken.UserId, null, null, null);
                                }

                            }
                        }
                    }
                    #endregion



                }
            }
            catch (Exception ex)
            {

                LogingHelper.SaveExceptionInfo("", ex, "SaveIndividualDocumentHTML", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualDocumentUploadList = null;
            }
            return objResponse;


        }

        #endregion



        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        // [AcceptVerbs("GET")]
        // [ActionName("IndividualSendRenewalNotice")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        //public IndividualDocumentByHtmlResponse IndividualSendRenewalNotice(string Key)
        //{
        //    if (Key != "8004746986")
        //        return null;

        //    IndividualDocumentByHtmlResponse objResponse = new IndividualDocumentByHtmlResponse();
        //    IndividualDocumentBAL objIndividualDocumentBAL = new IndividualDocumentBAL();
        //    IndividualDocument objIndividualDocument = new IndividualDocument();



        //    try
        //    {
        //        //string EmailTemplate = ""; // File.ReadAllText(HttpContext.Current.Server.MapPath("~/EmailTemplate/RenewalConfirmation.html"));
        //        string FilePath = HttpContext.Current.Server.MapPath("~/EmailTemplate/DataForSendingEmail-ActiveLicenseOnly.xlsx");
        //        OleDbConnection conn = new OleDbConnection();
        //        OleDbCommand cmd = new OleDbCommand();
        //        OleDbDataAdapter da = new OleDbDataAdapter();
        //        DataSet ds = new DataSet();
        //        string query = null;
        //        string connString = string.Empty;
        //        //Connection String to Excel Workbook
        //        //FileStream stream = File.Open(strNewPath, FileMode.Open, FileAccess.Read);


        //        ///---------------- Using EPPlus--------------------------------------
        //        var pck = new OfficeOpenXml.ExcelPackage();
        //        pck.Load(new System.IO.FileInfo(FilePath).OpenRead());
        //        var ws = pck.Workbook.Worksheets.First();  // or Worksheets[1] instead(1-based!)
        //        DataTable tbl = new DataTable();
        //        bool hasHeader = true; // change it if required
        //        foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
        //        {
        //            tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
        //        }
        //        var startRow = hasHeader ? 2 : 1;
        //        for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
        //        {
        //            var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
        //            var row = tbl.NewRow();
        //            foreach (var cell in wsRow)
        //            {
        //                row[cell.Start.Column - 1] = cell.Text;
        //            }
        //            tbl.Rows.Add(row);
        //        }
        //        pck.Dispose();

        //        int Sent = 0;
        //        int failed = 0;

        //        if (tbl.Rows.Count > 0)
        //        {


        //            foreach (DataRow dr in tbl.Rows)
        //            {
        //                try
        //                {
        //                    string EmailTemplate = File.ReadAllText(HttpContext.Current.Server.MapPath("~/EmailTemplate/SpeechandHearingRenewalNotice.html"));

        //                    int IndividualID = Convert.ToInt32(dr["IndividualId"].ToString());
        //                    string LicenseType = dr["LicenseType"].ToString();
        //                    string LicenseNumber = dr["LicenseNumber"].ToString();
        //                    string LicenseExpirationDate = Convert.ToDateTime(dr["LicenseExpirationDate"].ToString()).ToString("MM/dd/yyyy");
        //                    string EmailAddress = dr["EmailAddress"].ToString();
        //                    string Email = EmailAddress; //  "test@ebsoftsolutions.com";


        //                    EmailTemplate = EmailTemplate.Replace("#LicenseType#", LicenseType );
        //                    EmailTemplate = EmailTemplate.Replace("#LicenseNumber#", LicenseNumber);
        //                    EmailTemplate = EmailTemplate.Replace("#LicenseExpirationDate#", LicenseExpirationDate);

        //                    if (!string.IsNullOrEmpty(EmailAddress))
        //                    {


        //                        if (EmailHelper.SendMailWithMultipleAttachment(Email, "License Renewal ONLINE is now open", EmailTemplate, true, new List<Attachment>() ))
        //                        {
        //                            LogHelper.SaveIndividualLog(IndividualID, null, "Backoffice", ("Renewal notice has been sent by email. Email Address: " + EmailAddress + ", Sent On: " + DateTime.Now.ToString("MM/dd/yyyy")), 0, null, null, null);
        //                            LogHelper.LogCommunication(IndividualID, null, eCommunicationType.Email, "License Renewal ONLINE is now open", eCommunicationStatus.Success, "Backoffice", EmailTemplate, EmailHelper.GetSenderAddress(), Email, null, null, 0, null, null, null);
        //                            Sent++;
        //                        }
        //                        else
        //                        {
        //                            LogHelper.LogCommunication(IndividualID, null, eCommunicationType.Email, "License Renewal ONLINE is now open", eCommunicationStatus.Fail, "Backoffice", EmailTemplate, EmailHelper.GetSenderAddress(), Email, null, null, 0, null, null, null);
        //                            failed++;

        //                        }
        //                    }

        //                }
        //                catch (Exception ex)
        //                {

        //                }


        //            }
        //        }


        //        objResponse.Message = "Email Sent:" + Sent + ", Email Failed:" + failed;
        //        objResponse.Status = false;
        //        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
        //        objResponse.ResponseReason = null;
        //        return objResponse;

        //    }

        //    catch (Exception ex)
        //    {
        //        LogingHelper.SaveExceptionInfo(Key, ex, "IndividualDocumentGetByHTML", ENTITY.Enumeration.eSeverity.Error);

        //        objResponse.Status = false;
        //        objResponse.Message = ex.Message;
        //        objResponse.IndividualDocumentUploadList = null;
        //        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
        //    }
        //    return objResponse;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="objHtmlToPdfDocument"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [ActionName("HtmlToPdfDocument")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public HttpResponseMessage HtmlToPdfDocument(string Key, HtmlToPdfDocument objHtmlToPdfDocument)
        {

            try
            {
                string FilePath = ConfigurationHelper.GetConfigurationValueBySetting("RootDocumentPath") + "Renewal\\";
                string DocFileName = objHtmlToPdfDocument.DocNameWithExtention;
                string DocPath = FilePath + DocFileName;
                EO.Pdf.Runtime.AddLicense("f6yywc2faLWRm8ufdabl/RfusLWRm8ufdeb29RDxguXqAMvjmuvpzs22aKi1wN2vaqqmsSHkq+rtABm8W6ymsdq9RoGksefyot7y8h/0q9zC6gPqnNHvxg34aav32PjOhuHZBPXWerTBzdryot7y8h/0q9zCnrWfWbOz/RTinuX39umMQ3Xj7fQQ7azcwp61n1mz8PoO5Kfq6doPvXCoucfbtWutucPnrqXg5/YZ8p7A6M+4iVmXwP0U4p7l9/YQn6fY8fbooZrh5QrNvXWm8PoO5Kfq6fbpjEOXpM0M66Xm+8+4iVmXwPIP41nr/QEQvFu807/745+ZpLEh5Kvq7QAZvFs=");


                HtmlToPdf.Options.AutoFitX = HtmlToPdfAutoFitMode.ShrinkToFit;
                HtmlToPdf.Options.PageSize = EO.Pdf.PdfPageSizes.A4;
                HtmlToPdf.Options.OutputArea = new RectangleF(0.1f, 0.1f, 8.0f, 11.5f);
                FileInfo fi = new FileInfo(DocPath);
                if (fi.Exists)
                {

                    fi.Delete();
                }
                HtmlToPdf.ConvertHtml(objHtmlToPdfDocument.HtmlString, DocPath);
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                var stream = new FileStream(DocPath, FileMode.Open);
                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = Path.GetFileName(DocPath);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentLength = stream.Length;

                return result;
            }
            catch (Exception ex)
            { return null; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="IndividualDocumentId"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        [ActionName("PdfDocumentByIndividualDocumentId")]
        public HttpResponseMessage PdfDocumentByIndividualDocumentId(string Key, int IndividualDocumentId)
        {

            try
            {
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                IndividualDocument objDocument = new IndividualDocument();
                IndividualDocumentBAL objDocumentBAL = new IndividualDocumentBAL();
                objDocument = objDocumentBAL.Get_IndividualDocument_By_IndividualDocumentId(IndividualDocumentId);
                if (objDocument != null)
                {

                    var stream = new FileStream(objDocument.DocumentPath, FileMode.Open);
                    result.Content = new StreamContent(stream);
                    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    result.Content.Headers.ContentDisposition.FileName = Path.GetFileName(objDocument.DocumentPath);
                    //   result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                    result.Content.Headers.ContentLength = stream.Length;

                }
                return result;
            }
            catch (Exception ex)
            { return null; }
        }
        #endregion

        #region IndividualContact_Save


        /// <summary>
        /// Save or Update the data For IndividualContact
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objIndividualContact">Object of IndividualContact</param>
        [AcceptVerbs("POST")]
        [ActionName("IndividualContactSave")]
        public IndividualContactResponseRequest IndividualContactSave(string Key, IndividualContactResponse objIndividualContact)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo(Key);

            IndividualContactResponseRequest objResponse = new IndividualContactResponseRequest();
            IndividualContactBAL objBAL = new IndividualContactBAL();
            IndividualContact objEntity = new IndividualContact();
            List<IndividualContactResponse> lstEntity = new List<IndividualContactResponse>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.IndividualContactResponse = null;
                return objResponse;
            }
            try
            {

                try
                {
                    if (System.Web.HttpContext.Current.IsDebuggingEnabled)
                    {
                        // this is executed only in the debug version
                        string requestStr = Newtonsoft.Json.JsonConvert.SerializeObject(objIndividualContact);
                        LogingHelper.SaveRequestJson(requestStr, "IndividualContactSave Request");
                    }

                }
                catch (Exception ex)
                {
                    LogingHelper.SaveExceptionInfo(Key, ex, "IndividualContactSave object serialization", ENTITY.Enumeration.eSeverity.Critical);
                }

                string ValidationResponse = "";//IndividualValidations.ValidateIndividualContactObject(objIndividualContact);

                if (!string.IsNullOrEmpty(ValidationResponse))
                {
                    objResponse.Message = "Validation Error";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }

                if (objIndividualContact != null)
                {
                    return IndividualContactCS.SaveIndividualContact(TokenHelper.GetTokenByKey(Key), objIndividualContact);
                }
                else
                {
                    objResponse.Message = "Individual Contact object cannot be null.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualContactSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.IndividualContactResponse = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }


        /// <summary>
        /// Get Method to get IndividualContact by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="IndividualId">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("IndividualContactBYIndividualId")]

        public IndividualContactResponseRequest IndividualContactBYIndividualId(string Key, int IndividualId)
        {
            LogingHelper.SaveAuditInfo(Key);

            IndividualContactResponseRequest objResponse = new IndividualContactResponseRequest();
            IndividualContactBAL objBAL = new IndividualContactBAL();
            IndividualContact objEntity = new IndividualContact();
            List<IndividualContactResponse> lstEntity = new List<IndividualContactResponse>();
            List<IndividualContact> lstIndividualContact = new List<IndividualContact>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.IndividualContactResponse = null;
                    return objResponse;
                }

                IndividualContactBAL objIndividualContactBAL = new IndividualContactBAL();
                lstIndividualContact = objIndividualContactBAL.Get_IndividualContact_By_IndividualId(IndividualId);
                if (lstIndividualContact != null)
                {
                    List<IndividualContactResponse> lstContactResponse = lstIndividualContact
                        .Select(obj => new IndividualContactResponse
                        {
                            BeginDate = obj.BeginDate,
                            EndDate = obj.EndDate,
                            IndividualId = obj.IndividualId,
                            IsActive = obj.IsActive,
                            Code = obj.Code,
                            ContactFirstName = obj.ContactFirstName,
                            ContactId = obj.ContactId,
                            ContactInfo = obj.ContactInfo,
                            ContactLastName = obj.ContactLastName,
                            ContactMiddleName = obj.ContactMiddleName,
                            ContactTypeId = obj.ContactTypeId,
                            DateContactValidated = obj.DateContactValidated,
                            IndividualContactId = obj.IndividualContactId,
                            IsMobile = obj.IsMobile,
                            IsPreferredContact = obj.IsPreferredContact

                        }).ToList();

                    objResponse.IndividualContactResponse = lstContactResponse;


                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.IndividualContactResponse = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualContactBYIndividualId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualContactResponse = null;

            }
            return objResponse;
        }


        #endregion

        #region ApplicationSave


        /// <summary>
        /// Get Method to get Application by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="IndividualId">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("ApplicationBYIndividualId")]
        public ApplicationResponseGet ApplicationBYIndividualId(string Key, int IndividualId)
        {
            LogingHelper.SaveAuditInfo(Key);

            ApplicationResponseGet objResponse = new ApplicationResponseGet();
            ApplicationBAL objBAL = new ApplicationBAL();
            Application objEntity = new Application();
            List<ApplicationResponse> lstEntity = new List<ApplicationResponse>();
            List<Application> lstApplication = new List<Application>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.ApplicationResponseList = null;
                    return objResponse;
                }

                lstApplication = objBAL.Get_Application_By_IndividualId(IndividualId);
                if (lstApplication != null)
                {
                    lstEntity = lstApplication
                        .Select(obj => new ApplicationResponse
                        {
                            ApplicationId = obj.ApplicationId,
                            ApplicationTypeId = obj.ApplicationTypeId,
                            ApplicationStatusId = obj.ApplicationStatusId,
                            ApplicationType = obj.ApplicationType,
                            ApplicationStatusReasonId = obj.ApplicationStatusReasonId,
                            ApplicationNumber = obj.ApplicationNumber,
                            ApplicationSubmitMode = obj.ApplicationSubmitMode,
                            StartedDate = obj.StartedDate,
                            SubmittedDate = obj.SubmittedDate,
                            ApplicationStatusDate = obj.ApplicationStatusDate,
                            PaymentDeadlineDate = obj.PaymentDeadlineDate,
                            PaymentDate = obj.PaymentDate,
                            ConfirmationNumber = obj.ConfirmationNumber,
                            ReferenceNumber = obj.ReferenceNumber,
                            IsFingerprintingNotRequired = obj.IsFingerprintingNotRequired,
                            IsPaymentRequired = obj.IsPaymentRequired,
                            CanProvisionallyHire = obj.CanProvisionallyHire,
                            GoPaperless = obj.GoPaperless,
                            LicenseRequirementId = obj.LicenseRequirementId,
                            WithdrawalReasonId = obj.WithdrawalReasonId,
                            LicenseTypeId = obj.LicenseTypeId,
                            ApplicationStatus = obj.ApplicationStatus,
                            IsActive = obj.IsActive,

                        }).ToList();

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ApplicationResponseList = lstEntity;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ApplicationResponseList = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ApplicationBYIndividualId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.ApplicationResponseList = null;

            }
            return objResponse;
        }


        #endregion

        #region IndividualCommentLogSave


        /// <summary>
        /// Get Method to get IndividualCommentLog by key and IndividualId.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="IndividualId">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("IndividualCommentLogGetByIndividualId")]
        public IndividualCommentLogRequestResponce IndividualCommentLogGetByIndividualId(string Key, int IndividualId)
        {
            LogingHelper.SaveAuditInfo(Key);

            IndividualCommentLogRequestResponce objResponse = new IndividualCommentLogRequestResponce();

            IndividualCommentLogBAL objIndividualCommentLogBAL = new IndividualCommentLogBAL();
            IndividualCommentLogRequest objIndividualCommentLogRequest = new IndividualCommentLogRequest();
            IndividualCommentLog objIndividualCommentLog = new IndividualCommentLog();
            List<IndividualCommentLogRequest> lstIndividualCommentLogGet = new List<IndividualCommentLogRequest>();
            List<IndividualCommentLog> lstIndividualCommentLog = new List<IndividualCommentLog>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.IndividualCommentLogRequest = null;
                    return objResponse;
                }

                lstIndividualCommentLog = objIndividualCommentLogBAL.Get_IndividualCommentLog_by_IndividualIdANDTYPE(IndividualId, "C");
                if (lstIndividualCommentLog != null && lstIndividualCommentLog.Count > 0)
                {
                    lstIndividualCommentLogGet = lstIndividualCommentLog.Select(obj => new IndividualCommentLogRequest
                    {
                        IndividualCommentLogId = obj.IndividualCommentLogId,
                        IndividualId = obj.IndividualId,
                        ApplicationId = obj.ApplicationId,
                        MasterTransactionId = obj.MasterTransactionId,
                        PageModuleId = obj.PageModuleId,
                        PageModuleTabSubModuleId = obj.PageModuleTabSubModuleId,
                        EffectiveDate = obj.EffectiveDate,
                        EndDate = obj.EndDate,
                        PageTabSectionId = obj.PageTabSectionId,
                        CommentLogDate = obj.CommentLogDate,
                        ReferenceNumber = obj.ReferenceNumber,
                        Type = obj.Type,
                        CommentLogSource = obj.CommentLogSource,
                        CommentLogText = obj.CommentLogText,
                        IsInternalOnly = obj.IsInternalOnly,
                        IsForInvestigationOnly = obj.IsForInvestigationOnly,
                        IsForPublic = obj.IsForPublic,
                        IsActive = obj.IsActive,

                    }).ToList();

                    objResponse.IndividualCommentLogRequest = lstIndividualCommentLogGet;

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.IndividualCommentLogRequest = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualCommentLogGetByIndividualId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualCommentLogRequest = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Save or Update the data For IndividualCommentLog
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objIndividualCommentLogRequest">Object of IndividualCommentLog</param>
        [AcceptVerbs("POST")]
        [ActionName("IndividualCommentLogSave")]
        public IndividualCommentLogRequestResponce IndividualCommentLogSave(string Key, IndividualCommentLogRequest objIndividualCommentLogRequest)
        {
            IndividualCommentLogRequestResponce objResponse = new IndividualCommentLogRequestResponce();
            IndividualCommentLogBAL objIndividualCommentLogBAL = new IndividualCommentLogBAL();
            IndividualCommentLog objIndividualCommentLog = new IndividualCommentLog();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.IndividualCommentLogRequest = null;
                return objResponse;
            }

            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo(Key);

            try
            {

                try
                {
                    if (System.Web.HttpContext.Current.IsDebuggingEnabled)
                    {
                        // this is executed only in the debug version
                        string requestStr = Newtonsoft.Json.JsonConvert.SerializeObject(objIndividualCommentLogRequest);
                        LogingHelper.SaveRequestJson(requestStr, "IndividualCommentLogSave Request");
                    }

                }
                catch (Exception ex)
                {
                    LogingHelper.SaveExceptionInfo(Key, ex, "IndividualCommentLogSave object serialization", ENTITY.Enumeration.eSeverity.Critical);
                }

                string ValidationResponse = "";//IndividualValidations.ValidateIndividualCommentLog(lstDocumentToUpload, objIndividualCommentLogResponse.IndividualId);

                if (!string.IsNullOrEmpty(ValidationResponse))
                {
                    objResponse.Message = "Validation Error";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }

                if (objIndividualCommentLogRequest != null)
                {
                    return IndividualNoteCS.SaveIndividualNote(TokenHelper.GetTokenByKey(Key), objIndividualCommentLogRequest);
                }
                else
                {
                    objResponse.Message = "Individual Note object cannot be null.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }
            }

            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualCommentLogSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.IndividualCommentLogRequest = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }

        #endregion

        #region INDIVIDUALLOG

        /// <summary>
        /// Get Method to get IndividualCommentLog by key and IndividualId.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="IndividualId">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("IndividualLogGetByIndividualId")]
        public IndividualCommentLogRequestResponce IndividualLogGetByIndividualId(string Key, int IndividualId)
        {
            LogingHelper.SaveAuditInfo(Key);

            IndividualCommentLogRequestResponce objResponse = new IndividualCommentLogRequestResponce();

            IndividualCommentLogBAL objIndividualCommentLogBAL = new IndividualCommentLogBAL();
            IndividualCommentLogRequest objIndividualCommentLogRequest = new IndividualCommentLogRequest();
            IndividualCommentLog objIndividualCommentLog = new IndividualCommentLog();
            List<IndividualCommentLogRequest> lstIndividualCommentLogGet = new List<IndividualCommentLogRequest>();
            List<IndividualCommentLog> lstIndividualCommentLog = new List<IndividualCommentLog>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.IndividualCommentLogRequest = null;
                    return objResponse;
                }

                lstIndividualCommentLog = objIndividualCommentLogBAL.Get_IndividualCommentLog_by_IndividualIdANDTYPE(IndividualId, "L");
                if (lstIndividualCommentLog != null && lstIndividualCommentLog.Count > 0)
                {
                    lstIndividualCommentLogGet = lstIndividualCommentLog.Select(obj => new IndividualCommentLogRequest
                    {
                        IndividualCommentLogId = obj.IndividualCommentLogId,
                        IndividualId = obj.IndividualId,
                        ApplicationId = obj.ApplicationId,
                        MasterTransactionId = obj.MasterTransactionId,
                        PageModuleId = obj.PageModuleId,
                        PageModuleTabSubModuleId = obj.PageModuleTabSubModuleId,
                        EffectiveDate = obj.EffectiveDate,
                        EndDate = obj.EndDate,
                        PageTabSectionId = obj.PageTabSectionId,
                        CommentLogDate = obj.CommentLogDate,
                        ReferenceNumber = obj.ReferenceNumber,
                        Type = obj.Type,
                        CommentLogSource = obj.CommentLogSource,
                        CommentLogText = obj.CommentLogText,
                        IsInternalOnly = obj.IsInternalOnly,
                        IsForInvestigationOnly = obj.IsForInvestigationOnly,
                        IsForPublic = obj.IsForPublic,
                        IsActive = obj.IsActive,

                    }).ToList();

                    objResponse.IndividualCommentLogRequest = lstIndividualCommentLogGet;

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.IndividualCommentLogRequest = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualLogGetByIndividualId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualCommentLogRequest = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Save or Update the data For IndividualLog
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objIndividualCommentLogRequest">Object of IndividualCommentLog</param>
        [AcceptVerbs("POST")]
        [ActionName("IndividualLogSave")]
        public IndividualCommentLogRequestResponce IndividualLogSave(string Key, IndividualCommentLogRequest objIndividualCommentLogRequest)
        {
            IndividualCommentLogRequestResponce objResponse = new IndividualCommentLogRequestResponce();
            IndividualCommentLogBAL objIndividualCommentLogBAL = new IndividualCommentLogBAL();
            IndividualCommentLog objIndividualCommentLog = new IndividualCommentLog();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.IndividualCommentLogRequest = null;
                return objResponse;
            }

            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo(Key);

            try
            {
                try
                {
                    if (System.Web.HttpContext.Current.IsDebuggingEnabled)
                    {
                        // this is executed only in the debug version
                        string requestStr = Newtonsoft.Json.JsonConvert.SerializeObject(objIndividualCommentLogRequest);
                        LogingHelper.SaveRequestJson(requestStr, "IndividualLogSave Request");
                    }

                }
                catch (Exception ex)
                {
                    LogingHelper.SaveExceptionInfo(Key, ex, "IndividualLogSave object serialization", ENTITY.Enumeration.eSeverity.Critical);
                }

                string ValidationResponse = "";//IndividualValidations.ValidateIndividualCommentLog(lstDocumentToUpload, objIndividualCommentLogResponse.IndividualId);

                if (!string.IsNullOrEmpty(ValidationResponse))
                {
                    objResponse.Message = "Validation Error";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }

                if (objIndividualCommentLogRequest != null)
                {
                    return IndividualLogCS.SaveIndividualLog(TokenHelper.GetTokenByKey(Key), objIndividualCommentLogRequest);
                }
                else
                {
                    objResponse.Message = "Individual Log object cannot be null.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }
            }

            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualLogSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.IndividualCommentLogRequest = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }


        #endregion

        #region Communication



        /// <summary>
        /// Get Method to get IndividualCommunicationLog by key and IndividualId.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="IndividualId">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("IndividualCommunicationGetByIndividualId")]
        public IndividualCommunicationLogRequestResponce IndividualCommunicationGetByIndividualId(string Key, int IndividualId)
        {
            LogingHelper.SaveAuditInfo(Key);

            IndividualCommunicationLogRequestResponce objResponse = new IndividualCommunicationLogRequestResponce();

            IndividualCommunicationLogBAL objIndividualCommunicationLogBAL = new IndividualCommunicationLogBAL();
            IndividualCommunicationLogRequest objIndividualCommunicationLogRequest = new IndividualCommunicationLogRequest();
            IndividualCommunicationLog objIndividualCommunicationLog = new IndividualCommunicationLog();
            List<IndividualCommunicationLogRequest> lstIndividualCommunicationLogGet = new List<IndividualCommunicationLogRequest>();
            List<IndividualCommunicationLog> lstIndividualCommunicationLog = new List<IndividualCommunicationLog>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.IndividualCommunicationLogRequest = null;
                    return objResponse;
                }

                lstIndividualCommunicationLog = objIndividualCommunicationLogBAL.Get_IndividualCommunicationLog_by_IndividualId(IndividualId);
                if (lstIndividualCommunicationLog != null && lstIndividualCommunicationLog.Count > 0)
                {
                    lstIndividualCommunicationLogGet = lstIndividualCommunicationLog.Select(obj => new IndividualCommunicationLogRequest
                    {
                        IndividualCommunicationLogId = obj.IndividualCommunicationLogId,
                        IndividualId = obj.IndividualId,
                        ApplicationId = obj.ApplicationId,
                        MasterTransactionId = obj.MasterTransactionId,
                        PageModuleId = obj.PageModuleId,
                        PageModuleTabSubModuleId = obj.PageModuleTabSubModuleId,
                        EffectiveDate = obj.EffectiveDate,
                        EndDate = obj.EndDate,
                        PageTabSectionId = obj.PageTabSectionId,
                        CommunicationLogDate = obj.CommunicationLogDate,
                        ReferenceNumber = obj.ReferenceNumber,
                        Type = obj.Type,
                        Subject = obj.Subject,
                        CommunicationSource = obj.CommunicationSource,
                        CommunicationText = obj.CommunicationText,
                        CommunicationStatus = obj.CommunicationStatus,
                        UserIdFrom = obj.UserIdFrom,
                        EmailTo = obj.EmailTo,
                        UserIdTo = obj.UserIdTo,
                        IsInternalOnly = obj.IsInternalOnly,
                        IsForInvestigationOnly = obj.IsForInvestigationOnly,
                        IsForPublic = obj.IsForPublic,
                        IsActive = obj.IsActive,

                    }).ToList();

                    objResponse.IndividualCommunicationLogRequest = lstIndividualCommunicationLogGet;

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.IndividualCommunicationLogRequest = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualCommunicationGetByIndividualId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualCommunicationLogRequest = null;

            }
            return objResponse;
        }



        /// <summary>
        /// Save or Update the data For IndividualCommunication
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objCommunicationLog">Object of objCommunicationLog</param>
        [AcceptVerbs("POST")]
        [ActionName("IndividualCommunicationSave")]
        public IndividualCommunicationLogRequestResponce IndividualCommunicationSave(string Key, IndividualCommunicationLogRequest objCommunicationLog)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo(Key);
            IndividualCommunicationLogRequestResponce objResponse = new IndividualCommunicationLogRequestResponce();


            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.Message = null;
                objResponse.IndividualCommunicationLogRequest = null;
                return objResponse;
            }
            try
            {

                try
                {
                    if (System.Web.HttpContext.Current.IsDebuggingEnabled)
                    {
                        // this is executed only in the debug version
                        string requestStr = Newtonsoft.Json.JsonConvert.SerializeObject(objCommunicationLog);
                        LogingHelper.SaveRequestJson(requestStr, "Individual Communication Save Request");
                    }

                }
                catch (Exception ex)
                {
                    LogingHelper.SaveExceptionInfo(Key, ex, "IndividualCommunicationSave object serialization", ENTITY.Enumeration.eSeverity.Critical);
                }
                string ValidationResponse = "";//IndividualValidations.ValidateIndividualCommentLog(lstDocumentToUpload, objIndividualCommentLogResponse.IndividualId);

                if (!string.IsNullOrEmpty(ValidationResponse))
                {
                    objResponse.Message = "Validation Error";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    objResponse.IndividualCommunicationLogRequest = null;
                    return objResponse;
                }

                if (objCommunicationLog != null)
                {
                    return IndividualCorrespondenceCS.SaveIndividualCorrespondence(TokenHelper.GetTokenByKey(Key), objCommunicationLog);
                }
                else
                {
                    objResponse.Message = "Individual Communication object cannot be null.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualCommunicationSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.ResponseReason = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }


        #endregion

        #region IndividualLegal


        /// <summary>
        /// Get Method to get IndividualLegalby key and IndividualId.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="IndividualId">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("IndividualLegalGetByIndividualId")]
        public IndividualLegalRequestResponce IndividualLegalResponseGetByIndividualId(string Key, int IndividualId)
        {
            LogingHelper.SaveAuditInfo(Key);

            IndividualLegalRequestResponce objResponse = new IndividualLegalRequestResponce();

            IndividualLegalBAL objIndividualLegalBAL = new IndividualLegalBAL();
            IndividualLegalResponse objIndividualLegalResponse = new IndividualLegalResponse();
            List<IndividualLegal> lstIndividualLegal = new List<IndividualLegal>();
            List<IndividualLegalResponse> lstIndividualLegalResponse = new List<IndividualLegalResponse>();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.IndividualLegalResponse = null;
                    return objResponse;
                }

                lstIndividualLegal = objIndividualLegalBAL.Get_IndividualLegal_By_IndividualId(IndividualId);
                if (lstIndividualLegal != null && lstIndividualLegal.Count > 0)
                {
                    lstIndividualLegalResponse = lstIndividualLegal.Select(obj => new IndividualLegalResponse
                    {
                        IndividualLegalId = obj.IndividualLegalId,
                        IndividualId = obj.IndividualId,
                        ContentItemLkId = obj.ContentItemLkId,
                        ContentItemNumber = obj.ContentItemNumber,
                        ContentItemResponse = obj.ContentItemResponse,
                        Desc = obj.Desc,
                        ContentDescription = obj.ContentDescription,
                        IsActive = obj.IsActive,

                    }).ToList();

                    objResponse.IndividualLegalResponse = lstIndividualLegalResponse;

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.IndividualLegalResponse = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualLegalGetByIndividualId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualLegalResponse = null;

            }
            return objResponse;
        }



        /// <summary>
        /// Save or Update the data For IndividualLegal
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objIndividualLegalResponse">Object of objIndividualLegalResponse</param>
        [AcceptVerbs("POST")]
        [ActionName("IndividualLegalSave")]
        public IndividualLegalRequestResponce IndividualLegalSave(string Key, IndividualLegalResponse objIndividualLegalResponse)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo(Key);
            IndividualLegalRequestResponce objResponse = new IndividualLegalRequestResponce();


            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.Message = null;
                objResponse.IndividualLegalResponse = null;
                return objResponse;
            }
            try
            {

                try
                {
                    if (System.Web.HttpContext.Current.IsDebuggingEnabled)
                    {
                        // this is executed only in the debug version
                        string requestStr = Newtonsoft.Json.JsonConvert.SerializeObject(objIndividualLegalResponse);
                        LogingHelper.SaveRequestJson(requestStr, "Save Individual Legal Request");
                    }

                }
                catch (Exception ex)
                {
                    LogingHelper.SaveExceptionInfo(Key, ex, "SaveIndividualLegal object serialization", ENTITY.Enumeration.eSeverity.Critical);
                }

                string ValidationResponse = "";//IndividualValidations.ValidateIndividualCommentLog(lstDocumentToUpload, objIndividualCommentLogResponse.IndividualId);

                if (!string.IsNullOrEmpty(ValidationResponse))
                {
                    objResponse.Message = "Validation Error";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    objResponse.IndividualLegalResponse = null;
                    return objResponse;
                }

                if (objIndividualLegalResponse != null)
                {
                    return IndividualLegalCS.SaveIndividualLegal(TokenHelper.GetTokenByKey(Key), objIndividualLegalResponse);
                }
                else
                {
                    objResponse.Message = "Individual Legal object cannot be null.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualLegalSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.ResponseReason = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }


        #endregion

        #region IndividualCEH_Save


        /// <summary>
        /// Save or Update the data For IndividualCEH
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objIndividualCEH">Object of IndividualCEH</param>
        [AcceptVerbs("POST")]
        [ActionName("IndividualCEHSave")]
        public IndividualCEHResponseRequest IndividualCEHSave(string Key, IndividualCEHResponse objIndividualCEH)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo(Key);

            IndividualCEHResponseRequest objResponse = new IndividualCEHResponseRequest();
            IndividualCEHoursBAL objBAL = new IndividualCEHoursBAL();
            IndividualCEHours objEntity = new IndividualCEHours();
            List<IndividualCEHResponse> lstEntity = new List<IndividualCEHResponse>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.IndividualCEHResponseList = null;
                return objResponse;
            }
            try
            {

                try
                {
                    if (System.Web.HttpContext.Current.IsDebuggingEnabled)
                    {
                        // this is executed only in the debug version
                        string requestStr = Newtonsoft.Json.JsonConvert.SerializeObject(objIndividualCEH);
                        LogingHelper.SaveRequestJson(requestStr, "Save Individual CEH Request");
                    }

                }
                catch (Exception ex)
                {
                    LogingHelper.SaveExceptionInfo(Key, ex, "IndividualCEHSave object serialization", ENTITY.Enumeration.eSeverity.Critical);
                }
                string ValidationResponse = "";// IndividualCECourseValidate.ValidateIndividualCECourseObject(objIndividualCECourse);

                if (!string.IsNullOrEmpty(ValidationResponse))
                {
                    objResponse.Message = "Validation Error";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }

                if (objIndividualCEH != null)
                {
                    return IndividualCEH.SaveIndividualCEH(TokenHelper.GetTokenByKey(Key), objIndividualCEH);
                }
                else
                {
                    objResponse.Message = "Individual CEH object cannot be null.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualCEHSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.IndividualCEHResponseList = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }


        /// <summary>
        /// Get Method to get list IndividualCEH by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="IndividualId">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("IndividualCEHBYIndividualId")]
        public IndividualCEHResponseRequest IndividualCEHBYIndividualId(string Key, int IndividualId)
        {
            LogingHelper.SaveAuditInfo(Key);

            IndividualCEHResponseRequest objResponse = new IndividualCEHResponseRequest();
            IndividualCEHoursBAL objBAL = new IndividualCEHoursBAL();
            IndividualCEHResponse objEntity = new IndividualCEHResponse();
            List<IndividualCEHResponse> lstEntity = new List<IndividualCEHResponse>();
            List<IndividualCEHours> lstIndividualCEHours = new List<IndividualCEHours>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.IndividualCEHResponseList = null;
                    return objResponse;
                }

                lstIndividualCEHours = objBAL.Get_IndividualCEHours_By_IndividualId(IndividualId);
                if (lstIndividualCEHours != null && lstIndividualCEHours.Count > 0)
                {

                    List<IndividualCEHResponse> lstCeCourseResponse = lstIndividualCEHours.Select(obj => new IndividualCEHResponse
                    {
                        IndividualCEHoursId = obj.IndividualCEHoursId,
                        IndividualId = obj.IndividualId,
                        ApplicationId = obj.ApplicationId,
                        CEHoursTypeId = obj.CEHoursTypeId,
                        CEHoursStartDate = obj.CEHoursStartDate,
                        CEHoursEndDate = obj.CEHoursEndDate,
                        CEHoursDueDate = obj.CEHoursDueDate,
                        CEHoursReportingYear = obj.CEHoursReportingYear,
                        CEHoursStatusId = obj.CEHoursStatusId,
                        CECarryInHours = obj.CECarryInHours,
                        CERequiredHours = obj.CERequiredHours,
                        CECurrentReportedHours = obj.CECurrentReportedHours,
                        CERolloverHours = obj.CERolloverHours,
                        //ReferenceNumber = obj.ReferenceNumber,
                        IsActive = obj.IsActive,
                        IndividualLicenseId = obj.IndividualLicenseId
                    }).ToList();


                    objResponse.IndividualCEHResponseList = lstCeCourseResponse;

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.IndividualCEHResponseList = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualCEHBYIndividualId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualCEHResponseList = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Get Method to get object of IndividualCEH by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="IndividualLicenseId">Individual License Id.</param>
        [AcceptVerbs("GET")]
        [ActionName("IndividualCEHBYIndividualLicenseId")]
        public IndividualCEHResponseRequest IndividualCEHByIndividualLicenseId(string Key, int IndividualLicenseId)
        {
            LogingHelper.SaveAuditInfo(Key);

            IndividualCEHResponseRequest objResponse = new IndividualCEHResponseRequest();
            IndividualCEHoursBAL objBAL = new IndividualCEHoursBAL();

            IndividualCEHResponse objEntity = new IndividualCEHResponse();
            IndividualCEHours objCeh = new IndividualCEHours();
            List<IndividualCEHResponse> lstEntity = new List<IndividualCEHResponse>();
            List<IndividualCEHours> lstIndividualCEHours = new List<IndividualCEHours>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.IndividualCEHResponseList = null;
                    return objResponse;
                }

                objCeh = objBAL.Get_IndividualCEHours_By_IndividualLicenseId(IndividualLicenseId);
                if (objCeh != null)
                {
                    lstIndividualCEHours.Add(objCeh);

                    List<IndividualCEHResponse> lstCeCourseResponse = lstIndividualCEHours.Select(obj => new IndividualCEHResponse
                    {
                        IndividualCEHoursId = obj.IndividualCEHoursId,
                        IndividualId = obj.IndividualId,
                        ApplicationId = obj.ApplicationId,
                        CEHoursTypeId = obj.CEHoursTypeId,
                        CEHoursStartDate = obj.CEHoursStartDate,
                        CEHoursEndDate = obj.CEHoursEndDate,
                        CEHoursDueDate = obj.CEHoursDueDate,
                        CEHoursReportingYear = obj.CEHoursReportingYear,
                        CEHoursStatusId = obj.CEHoursStatusId,
                        CECarryInHours = obj.CECarryInHours,
                        CERequiredHours = obj.CERequiredHours,
                        CECurrentReportedHours = obj.CECurrentReportedHours,
                        CERolloverHours = obj.CERolloverHours,
                       // ReferenceNumber = obj.ReferenceNumber,
                        IsActive = obj.IsActive,
                        IndividualLicenseId = obj.IndividualLicenseId
                    }).ToList();


                    objResponse.IndividualCEHResponseList = lstCeCourseResponse;

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.IndividualCEHResponseList = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualCEHBYIndividualLicenseId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualCEHResponseList = null;

            }
            return objResponse;
        }


        ///// <summary>
        ///// Create and Get object of IndividualCEH.
        ///// </summary>
        ///// <param name="Key">API security key.</param>
        ///// <param name="IndividualLicenseId">Individual License Id.</param>
        ///// <param name="IndividualId">Individual Id.</param>
        //[AcceptVerbs("GET")]
        //[ActionName("CreateIndividualCEH")]
        //public IndividualCEHResponseRequest CreateIndividualCEH(string Key, int IndividualLicenseId, int IndividualId)
        //{
        //    LogingHelper.SaveAuditInfo(Key);

        //    IndividualCEHResponseRequest objResponse = new IndividualCEHResponseRequest();
        //    IndividualCEHoursBAL objBAL = new IndividualCEHoursBAL();

        //    IndividualCEHResponse objEntity = new IndividualCEHResponse();
        //    IndividualCEHours objCeh = new IndividualCEHours();
        //    List<IndividualCEHResponse> lstEntity = new List<IndividualCEHResponse>();
        //    List<IndividualCEHours> lstIndividualCEHours = new List<IndividualCEHours>();
        //    try
        //    {
        //        if (!TokenHelper.ValidateToken(Key))
        //        {
        //            objResponse.Status = false;
        //            objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
        //            objResponse.Message = "User session has expired.";
        //            objResponse.IndividualCEHResponseList = null;
        //            return objResponse;
        //        }
        //        Token objToken = TokenHelper.GetTokenByKey(Key);

        //        objCeh = IndividualCEH.CreateIndividualCEH(objToken, IndividualLicenseId, IndividualId);
        //        if (objCeh != null)
        //        {
        //            lstIndividualCEHours.Add(objCeh);

        //            List<IndividualCEHResponse> lstCeCourseResponse = lstIndividualCEHours.Select(obj => new IndividualCEHResponse
        //            {
        //                IndividualCEHoursId = obj.IndividualCEHoursId,
        //                IndividualId = obj.IndividualId,
        //                ApplicationId = obj.ApplicationId,
        //                CEHoursTypeId = obj.CEHoursTypeId,
        //                CEHoursStartDate = obj.CEHoursStartDate,
        //                CEHoursEndDate = obj.CEHoursEndDate,
        //                CEHoursDueDate = obj.CEHoursDueDate,
        //                CEHoursReportingYear = obj.CEHoursReportingYear,
        //                CEHoursStatusId = obj.CEHoursStatusId,
        //                CECarryInHours = obj.CECarryInHours,
        //                CERequiredHours = obj.CERequiredHours,
        //                CECurrentReportedHours = obj.CECurrentReportedHours,
        //                CERolloverHours = obj.CERolloverHours,
        //                ReferenceNumber = obj.ReferenceNumber,
        //                IsActive = obj.IsActive,
        //                IndividualLicenseId = obj.IndividualLicenseId
        //            }).ToList();


        //            objResponse.IndividualCEHResponseList = lstCeCourseResponse;

        //            objResponse.Status = true;
        //            objResponse.Message = "";
        //            objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
        //        }
        //        else
        //        {
        //            objResponse.Status = false;
        //            objResponse.Message = "No record found.";
        //            objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
        //            objResponse.IndividualCEHResponseList = null;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        LogingHelper.SaveExceptionInfo(Key, ex, "CreateIndividualCEH", ENTITY.Enumeration.eSeverity.Error);

        //        objResponse.Status = false;
        //        objResponse.Message = ex.Message;
        //        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
        //        objResponse.IndividualCEHResponseList = null;

        //    }
        //    return objResponse;
        //}

        #endregion

    }
}
