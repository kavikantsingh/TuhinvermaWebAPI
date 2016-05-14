using LAPP.BAL;
using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.WS.App_Helper;
using LAPP.WS.App_Helper.Common;
using LAPP.WS.ValidateController.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LAPP.WS.Controllers.Common
{
    public class SourceController : ApiController
    {

        #region Source

        /// <summary>
        /// Looks up all data by Key For Source.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("GET")]
        [ActionName("SourceGetAll")]
        public SourceResponse SourceGetAll(string Key)
        {

            LogingHelper.SaveAuditInfo(Key);

            SourceResponse objResponse = new SourceResponse();
            SourceBAL objBAL = new SourceBAL();
            Source objEntity = new Source();
            List<Source> lstSource = new List<Source>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.Source = null;
                    return objResponse;

                }

                lstSource = objBAL.Get_All_Source();
                if (lstSource != null && lstSource.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstSourceSelected = lstSource.Select(obj => new
                    {
                        SourceId = obj.SourceId,
                        Name = obj.Name,
                        IsActive = obj.IsActive
                    }).ToList();


                    objResponse.Source = lstSourceSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Source = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "SourceGetAll", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.Source = null;

            }
            return objResponse;


        }


        /// <summary>
        /// Get Method to get Source by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ID">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("SourceGetBYID")]
        public SourceResponse SourceGetBYID(string Key, int ID)
        {
            LogingHelper.SaveAuditInfo(Key);

            SourceResponse objResponse = new SourceResponse();
            SourceBAL objBAL = new SourceBAL();
            Source objEntity = new Source();
            List<Source> lstSource = new List<Source>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.Source = null;
                    return objResponse;
                }

                objEntity = objBAL.Get_Source_bySourceId(ID);
                if (objEntity != null)
                {
                    lstSource.Add(objEntity);


                    var lstSourceSelected = lstSource.Select(obj => new
                    {
                        SourceId = obj.SourceId,
                        Name = obj.Name,
                        IsActive = obj.IsActive
                    }).ToList();

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Source = lstSourceSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Source = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "SourceGetBYID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Source = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Save or Update the data For Source
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objSource">Object of Source</param>
        [AcceptVerbs("POST")]
        [ActionName("SourceSave")]
        public SourceResponse SourceSave(string Key, SourceRequest objSource)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;
            LogingHelper.SaveAuditInfo(Key);

            SourceResponse objResponse = new SourceResponse();
            SourceBAL objBAL = new SourceBAL();
            Source objEntity = new Source();
            List<Source> lstEntity = new List<Source>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.Source = null;
                return objResponse;
            }

            string ValidationResponse = SourceValidation.ValidateSourceObject(objSource);

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
                if (objSource.SourceId > 0)
                {
                    objEntity = objBAL.Get_Source_bySourceId(objSource.SourceId);
                    if (objEntity != null)
                    {
                        objEntity.ModifiedOn = DateTime.Now;
                        objEntity.ModifiedBy = CreatedOrMoifiy;
                        objEntity.IsActive = objSource.IsActive;
                        objEntity.IsDeleted = objSource.IsDeleted;
                        objEntity.Name = objSource.Name;
                        objBAL.Save_Source(objEntity);
                        objResponse.Message = Messages.UpdateSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }

                    else
                    {
                        objEntity = new Source();
                        objEntity.IsActive = objSource.IsActive;
                        objEntity.IsDeleted = objSource.IsDeleted;
                        objEntity.Name = objSource.Name;

                        objEntity.CreatedOn = DateTime.Now;
                        objEntity.CreatedBy = CreatedOrMoifiy;
                        //objSource.ModifiedOn = null;
                        objEntity.SourceId = objBAL.Save_Source(objEntity);
                        objResponse.Message = Messages.SaveSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    }
                }
                else
                {
                    objEntity.CreatedBy = CreatedOrMoifiy;
                    objEntity.CreatedOn = DateTime.Now;
                    objEntity.IsActive = objSource.IsActive;
                    objEntity.IsDeleted = objSource.IsDeleted;
                    objEntity.Name = objSource.Name;
                    objEntity.SourceId = objBAL.Save_Source(objEntity);
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                }

                lstEntity.Add(objEntity);
                objResponse.Status = true;
                var lstSourceSelected = lstEntity.Select(obj => new
                {
                    SourceId = obj.SourceId,
                    Name = obj.Name,
                    IsActive = obj.IsActive
                }).ToList();
                objResponse.Source = lstSourceSelected;

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "SourceSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.Source = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }


        #endregion

    }
}
