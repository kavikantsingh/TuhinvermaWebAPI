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
                string ValidationResponse = IndividualValidations.ValidateIndividualAddressObject(objIndividualAddress);

                if (!string.IsNullOrEmpty(ValidationResponse))
                {
                    objResponse.Message = "Validation Error";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }

                return IndividualAddressCS.SaveIndividualAddress(TokenHelper.GetTokenByKey(Key), objIndividualAddress);

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
        /// Save or Update the data For IndividualAddress
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objIndividual">Object of IndividualAddress</param>
        [AcceptVerbs("POST")]
        [ActionName("IndividualSave")]
        public IndividualResponseRequest IndividualSave(string Key, IndividualResponse objIndividual)
        {
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

                if (!string.IsNullOrEmpty(ValidationResponse))
                {
                    objResponse.Message = "Validation Error";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }

                return IndividualCS.SaveIndividual(TokenHelper.GetTokenByKey(Key), objIndividual);

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
                        SuffixId = obj.SuffixId,
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
                string ValidationResponse = "";// IndividualNameAddressValidate.ValidateIndividualNameAddressObject(objIndividualNameAddress);

                if (!string.IsNullOrEmpty(ValidationResponse))
                {
                    objResponse.Message = "Validation Error";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }

                return IndividualNameCS.SaveIndividualName(TokenHelper.GetTokenByKey(Key), objIndividualName);

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
                string ValidationResponse = "";// IndividualCECourseValidate.ValidateIndividualCECourseObject(objIndividualCECourse);

                if (!string.IsNullOrEmpty(ValidationResponse))
                {
                    objResponse.Message = "Validation Error";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }


                return IndividualEducationCS.SaveIndividualEducation(TokenHelper.GetTokenByKey(Key), objIndividualCECourse);

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
                string ValidationResponse = "";// IndividualEmploymentValidate.ValidateIndividualEmploymentObject(objIndividualEmployment);

                if (!string.IsNullOrEmpty(ValidationResponse))
                {
                    objResponse.Message = "Validation Error";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }


                return IndividualEmploymentCS.SaveIndividualEmployment(TokenHelper.GetTokenByKey(Key), objIndividualEmployment);

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
                        OriginalLicenseDate = obj.OriginalLicenseDate

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
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualLicenseBYIndividualLicenseId", ENTITY.Enumeration.eSeverity.Error);

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
                string ValidationResponse = "";// IndividualLicenseAddressValidate.ValidateIndividualLicenseAddressObject(objIndividualLicenseAddress);

                if (!string.IsNullOrEmpty(ValidationResponse))
                {
                    objResponse.Message = "Validation Error";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }

                return IndividualLicenseCS.SaveIndividualLicense(TokenHelper.GetTokenByKey(Key), objIndividualLicense);

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
                        ReferenceNumber = obj.ReferenceNumber,
                        IsActive = obj.IsActive,

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
                    objResponse.Message = "No record to delete.";
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
        /// <param name="objIndividualDocument">Object of IndividualDocument</param>
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

                return SaveIndividualDocument(TokenHelper.GetTokenByKey(Key), objIndividualDocumentResponse);
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
                            string DocFileName = Guid.NewGuid().ToString() + objDtU.DocNameWithExtention; // Guid.NewGuid().ToString() + ".pdf";
                            string DocPath = FileHelper.Base64ToFile(objDtU.DocStrBase64, FilePath + DocFileName); // (FilePath + DocFileName);

                            objIndividualDocument = new IndividualDocument();

                            objIndividualDocument.IndividualId = IndividualId;
                            objIndividualDocument.ApplicationId = ApplicationId;
                            objIndividualDocument.DocumentLkToPageTabSectionId = objDtU.DocumentLkToPageTabSectionId;
                            objIndividualDocument.DocumentLkToPageTabSectionCode = objDtU.DocumentLkToPageTabSectionCode;

                            objIndividualDocument.DocumentTypeName = objDtU.DocumentTypeName;
                            objIndividualDocument.DocumentPath = DocPath;
                            objIndividualDocument.EffectiveDate = objDtU.EffectiveDate;
                            objIndividualDocument.EndDate = objDtU.EndDate;
                            objIndividualDocument.IsDocumentUploadedbyIndividual = objDtU.IsDocumentUploadedbyIndividual;
                            objIndividualDocument.IsDocumentUploadedbyStaff = objDtU.IsDocumentUploadedbyStaff;
                            objIndividualDocument.ReferenceNumber = objDtU.ReferenceNumber;
                            objIndividualDocument.IsActive = true;
                            objIndividualDocument.IsDeleted = false;
                            objIndividualDocument.CreatedBy = CreatedOrMoifiy;
                            objIndividualDocument.CreatedOn = DateTime.Now;
                            objIndividualDocument.ModifiedOn = null;
                            objIndividualDocument.ModifiedBy = null;
                            objIndividualDocument.IndividualDocumentGuid = Guid.NewGuid().ToString();

                            if (objIndividualDocument != null)
                            {
                                objIndividualDocument.IndividualDocumentId = objIndividualDocumentBAL.Save_IndividualDocument(objIndividualDocument);
                                objDtU.IndividualDocumentId = objIndividualDocument.IndividualDocumentId;

                                // objIndividualDocumentUpload = new IndividualDocumentUpload();

                                lstDocumentToUploadNEW.Add(objDtU);

                                Attachment objAttachment = new Attachment(DocPath);
                                lstAttachment.Add(objAttachment);
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
                            lstFeeDisb = objFeeDisbBAL.Get_RevFeeDisb_by_ApplicationId(objApplication.ApplicationId);
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
                                LogHelper.LogCommunication(objIndividual.IndividualId, objApplication.ApplicationId, eCommunicationType.Email, "Renewal Confirmation", eCommunicationStatus.Success, "Public", "Renewal Confirmation email has been sent", "", objIndividualDocumentResponse.Email, null, null, objToken.UserId, null, null, null);
                            }
                            else
                            {
                                LogHelper.LogCommunication(objIndividual.IndividualId, objApplication.ApplicationId, eCommunicationType.Email, "Online License Renewal and Payment", eCommunicationStatus.Fail, "Public", "Renewal Confirmation email sending failed.", "", objIndividualDocumentResponse.Email, null, null, objToken.UserId, null, null, null);
                            }

                        }
                    }
                    #endregion



                }
            }
            catch (Exception ex)
            {

                LogingHelper.SaveExceptionInfo("", ex, "SaveIndividualName", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualDocumentUploadList = null;
            }
            return objResponse;


        }

        #endregion

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
                string ValidationResponse = "";//IndividualValidations.ValidateIndividualContactObject(objIndividualContact);

                if (!string.IsNullOrEmpty(ValidationResponse))
                {
                    objResponse.Message = "Validation Error";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                    objResponse.ResponseReason = ValidationResponse;
                    return objResponse;
                }

                return IndividualContactCS.SaveIndividualContact(TokenHelper.GetTokenByKey(Key), objIndividualContact);

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
    }
}
