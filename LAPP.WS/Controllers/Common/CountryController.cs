using LAPP.BAL;
using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.LOGING;
using LAPP.WS.App_Helper;
using LAPP.WS.App_Helper.Common;
using LAPP.WS.ValidateController.Backoffice;
using LAPP.WS.ValidateController.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace LAPP.WS.Controllers.Misc
{
    /// <summary>
    /// Populate Country data
    /// </summary>
    public class CountryController : ApiController
    {
        /// <summary>
        /// Looks up all data for Country by Key.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>

        [HttpGet]
        [ActionName("CountryGetAll")]
        public CountryResponse GetAllCountry(string Key)
        {
            //Audit Request
            LogingHelper.SaveAuditInfo(Key);

            CountryResponse objResponse = new CountryResponse();
            CountryBAL objBAL = new CountryBAL();

            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.Country = null;
                    return objResponse;
                }


                List<Country> lstEntity = objBAL.GetAll_Country();
                if (lstEntity != null && lstEntity.Count > 0)
                {

                    var lstCountrySelected = lstEntity.Select(obj => new
                    {
                        CountryId = obj.CountryId,
                        Code = obj.Code,
                        Name = obj.Name,
                        StateLabel = obj.StateLabel,
                        ZipLabel = obj.ZipLabel,
                        ZipRegex = obj.ZipRegex,

                        IsActive = obj.IsActive
                    }).ToList();


                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Country = lstCountrySelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Message = "No record found.";
                    objResponse.Country = null;

                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "GetAllCountry", ENTITY.Enumeration.eSeverity.Error);
                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Country = null;

            }
            return objResponse;


        }

        /// <summary>
        /// Save or Update the data for Country
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objCountry">Object of Country</param>
        [AcceptVerbs("POST")]
        [ActionName("CountrySave")]
        public CountryResponse SaveCountry(string Key, CountryRequest objCountry)
        {

            //Audit Request
            LogingHelper.SaveAuditInfo(Key);
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;
            CountryResponse objResponse = new CountryResponse();
            CountryBAL objBAL = new CountryBAL();
            Country objEntity = new Country();
            List<Country> lstEntity = new List<Country>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.Country = null;
                return objResponse;
            }

            string ValidationResponse = CountryValidation.ValidateCountryObject(objCountry);

            if (!string.IsNullOrEmpty(ValidationResponse))
            {


                objResponse.Message = "Validation Error";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                objResponse.ResponseReason = ValidationResponse;
                return objResponse;

            }

            try
            {
                if (objCountry.CountryId > 0)
                {
                    objEntity = objBAL.Get_Country_byID(objCountry.CountryId);
                    if (objEntity != null)
                    {
                        objEntity.Code = objCountry.Code;
                        objEntity.CountryId = objCountry.CountryId;
                        objEntity.IsActive = objCountry.IsActive;
                        objEntity.IsDelete = objCountry.IsDelete;
                        objEntity.ModifiedOn = DateTime.Now;
                        objEntity.Name = objCountry.Name;
                        objEntity.StateLabel = objCountry.StateLabel;
                        objEntity.ZipLabel = objCountry.ZipLabel;
                        objEntity.ZipRegex = objCountry.ZipRegex;


                        objEntity.ModifiedBy = CreatedOrMoifiy;

                        objBAL.Save_Country(objEntity);
                        objResponse.Message = Messages.UpdateSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }
                }
                else
                {
                    objEntity = new Country();
                    objEntity.Code = objCountry.Code;
                    objEntity.CountryId = objCountry.CountryId;
                    objEntity.IsActive = objCountry.IsActive;
                    objEntity.IsDelete = objCountry.IsDelete;
                    objEntity.CreatedOn = DateTime.Now;
                    objEntity.Name = objCountry.Name;
                    objEntity.StateLabel = objCountry.StateLabel;
                    objEntity.ZipLabel = objCountry.ZipLabel;
                    objEntity.ZipRegex = objCountry.ZipRegex;

                    objEntity.CreatedBy = CreatedOrMoifiy;

                    objEntity.CountryId = objBAL.Save_Country(objEntity);

                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }

                lstEntity.Add(objEntity);
                objResponse.Status = true;


                var lstCountrySelected = lstEntity.Select(obj => new
                {
                    CountryId = obj.CountryId,
                    Code = obj.Code,
                    Name = obj.Name,
                    StateLabel = obj.StateLabel,
                    ZipLabel = obj.ZipLabel,
                    ZipRegex = obj.ZipRegex,

                    IsActive = obj.IsActive
                }).ToList();
                objResponse.Country = lstCountrySelected;

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "SaveCountry", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.Country = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");

            }
            return objResponse;
        }


        ///// <summary>
        ///// Delete data by Country Id.
        ///// </summary> 
        ///// /// <param name="Key">The Key of the data.</param>
        ///// <param name="CountryId">The Country Id of the data.</param>

        //[HttpGet]
        //[ActionName("DeleteCountryByID")]
        //public CountryResponse DeleteCountryByID(string Key, int CountryId)
        //{
        //    //Audit Request
        //    LogingHelper.SaveAuditInfo(Key);

        //    CountryResponse objResponse = new CountryResponse();
        //    CountryBAL objBAL = new CountryBAL();

        //    try
        //    {
        //        objBAL.Delete_Country_byID(CountryId);
        //        objResponse.Status = true;
        //        objResponse.Message = Messages.DeleteSuccess;
        //        objResponse.Country = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogingHelper.SaveExceptionInfo(Key, ex, "DeleteCountryByID", ENTITY.Enumeration.eSeverity.Error);

        //        objResponse.Status = false;
        //        objResponse.Message = ex.Message;
        //        objResponse.Country = null;

        //    }
        //    return objResponse;


        //}

    }
}
