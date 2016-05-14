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
using System.Web.Http.Description;

namespace LAPP.WS.Controllers.Common
{
    public class LookupController : ApiController
    {

        #region Lookup


        /// <summary>
        /// Get Method to get Lookup by key and LookupTypeID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="LookupTypeID">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("LookupGetBYLookupTypeID")]
        public LookupResponse LookupGetBYLookupTypeID(string Key, int LookupTypeID)
        {
            LogingHelper.SaveAuditInfo(Key);

            LookupResponse objResponse = new LookupResponse();
            LookupBAL objBAL = new LookupBAL();
            Lookup objEntity = new Lookup();
            List<Lookup> lstLookup = new List<Lookup>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.Lookup = null;
                    return objResponse;
                }

                lstLookup = objBAL.Get_All_Lookup_LookupTypeId(LookupTypeID);
                if (lstLookup != null && lstLookup.Count > 0)
                {
                    //lstBoard.Add(objEntity);

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstLookupSelected = lstLookup.Select(Lookup => new
                    {
                        LookupId = Lookup.LookupId,
                        LookupTypeId = Lookup.LookupTypeId,
                        LookupCode = Lookup.LookupCode,
                        LookupDesc = Lookup.LookupDesc,
                        SortOrder = Lookup.SortOrder,
                        IsEnabled = Lookup.IsEnabled,
                        IsActive = Lookup.IsActive,

                    }).ToList();

                    objResponse.Lookup = lstLookupSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Lookup = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "LookupGetBYID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Lookup = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Get Method to get Lookup by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ID">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("LookupGetBYID")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public LookupResponse LookupGetBYID(string Key, int ID)
        {
            LogingHelper.SaveAuditInfo(Key);

            LookupResponse objResponse = new LookupResponse();
            LookupBAL objBAL = new LookupBAL();
            Lookup objEntity = new Lookup();
            List<Lookup> lstLookup = new List<Lookup>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.Lookup = null;
                    return objResponse;
                }

                objEntity = objBAL.Get_Lookup_byLookupId(ID);
                if (objEntity != null)
                {
                    lstLookup.Add(objEntity);

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    var lstLookupSelected = lstLookup.Select(Lookup => new
                    {
                        LookupId = Lookup.LookupId,
                        LookupTypeId = Lookup.LookupTypeId,
                        LookupCode = Lookup.LookupCode,
                        LookupDesc = Lookup.LookupDesc,
                        SortOrder = Lookup.SortOrder,
                        IsEnabled = Lookup.IsEnabled,
                        IsActive = Lookup.IsActive,

                    }).ToList();

                    objResponse.Lookup = lstLookupSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Lookup = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "LookupGetBYID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Lookup = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Save or Update the data For Lookup
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objLookup">Object of Lookup</param>
        [AcceptVerbs("POST")]
        [ActionName("LookupSave")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        public LookupPostResponse LookupSave(string Key, LookupPost objLookup)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;
            LogingHelper.SaveAuditInfo(Key);

            LookupPostResponse objResponse = new LookupPostResponse();
            LookupBAL objBAL = new LookupBAL();
            Lookup objEntity = new Lookup();
            List<LookupPost> lstEntity = new List<LookupPost>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.Lookup = null;
                return objResponse;
            }

            string ValidationResponse = LookupValidation.ValidateLookupObject(objLookup);

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
                if (objLookup.LookupId > 0)
                {
                    objEntity = objBAL.Get_Lookup_byLookupId(objLookup.LookupId);
                    if (objEntity != null)
                    {
                        objEntity.ModifiedOn = DateTime.Now;
                        objEntity.ModifiedBy = CreatedOrMoifiy;

                        objEntity.IsActive = objLookup.IsActive;
                        objEntity.IsDeleted = objLookup.IsDeleted;
                        objEntity.IsEnabled = objLookup.IsEnabled;
                        objEntity.LookupCode = objLookup.LookupCode;
                        objEntity.LookupDesc = objLookup.LookupDesc;
                        objEntity.LookupTypeId = objLookup.LookupTypeId;
                        objEntity.SortOrder = objLookup.SortOrder;

                        objBAL.Save_Lookup(objEntity);

                        objResponse.Message = Messages.UpdateSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }
                }
                else
                {
                    objEntity = new Lookup();

                    objEntity.IsActive = objLookup.IsActive;
                    objEntity.CreatedOn = DateTime.Now;
                    objEntity.CreatedBy = CreatedOrMoifiy;
                    objEntity.IsDeleted = objLookup.IsDeleted;
                    objEntity.IsEnabled = objLookup.IsEnabled;
                    objEntity.LookupCode = objLookup.LookupCode;
                    objEntity.LookupDesc = objLookup.LookupDesc;
                    objEntity.LookupTypeId = objLookup.LookupTypeId;
                    //objEntity.ModifiedBy = null;
                    //objEntity.ModifiedOn = null;
                    objEntity.SortOrder = objLookup.SortOrder;

                    objEntity.LookupId = objBAL.Save_Lookup(objEntity);
                    objLookup.LookupId = objEntity.LookupId;

                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                }

                lstEntity.Add(objLookup);
                objResponse.Status = true;

                objResponse.Lookup = lstEntity;

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "LookupSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.Lookup = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }


        #endregion

        #region LookupType

        /// <summary>
        /// Looks up all data by Key For LookupType.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("GET")]
        [ActionName("LookupTypeGetAll")]
        public LookupTypeResponse LookupTypeGetAll(string Key)
        {

            LogingHelper.SaveAuditInfo(Key);

            LookupTypeResponse objResponse = new LookupTypeResponse();
            LookupTypeBAL objBAL = new LookupTypeBAL();
            LookupType objEntity = new LookupType();
            List<LookupType> lstLookupType = new List<LookupType>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.LookupType = null;
                    return objResponse;

                }

                lstLookupType = objBAL.Get_All_LookupType();
                if (lstLookupType != null && lstLookupType.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");


                    var lstLookupTypeSelected = lstLookupType.Select(LookupType => new
                    {
                        LookupTypeId = LookupType.LookupTypeId,
                        DivisionId = LookupType.DivisionId,
                        DepartmentId = LookupType.DepartmentId,
                        StateCode = LookupType.StateCode,
                        Name = LookupType.Name,
                        IsEditable = LookupType.IsEditable,
                        IsEncrypted = LookupType.IsEncrypted,
                        IsActive = LookupType.IsActive,

                    }).ToList();



                    objResponse.LookupType = lstLookupTypeSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.LookupType = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "LookupTypeGetAll", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.LookupType = null;

            }
            return objResponse;


        }


        /// <summary>
        /// Get Method to get LookupType by key and ID.
        /// </summary>
        /// <param name="Key">API security key.</param>
        /// <param name="ID">Record ID.</param>
        [AcceptVerbs("GET")]
        [ActionName("LookupTypeGetBYID")]
        public LookupTypeResponse LookupTypeGetBYID(string Key, int ID)
        {
            LogingHelper.SaveAuditInfo(Key);

            LookupTypeResponse objResponse = new LookupTypeResponse();
            LookupTypeBAL objBAL = new LookupTypeBAL();
            LookupType objEntity = new LookupType();
            List<LookupType> lstLookupType = new List<LookupType>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.LookupType = null;
                    return objResponse;
                }

                objEntity = objBAL.Get_LookupType_byLookupTypeId(ID);
                if (objEntity != null)
                {
                    lstLookupType.Add(objEntity);

                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstLookupTypeSelected = lstLookupType.Select(LookupType => new
                    {
                        LookupTypeId = LookupType.LookupTypeId,
                        DivisionId = LookupType.DivisionId,
                        DepartmentId = LookupType.DepartmentId,
                        StateCode = LookupType.StateCode,
                        Name = LookupType.Name,
                        IsEditable = LookupType.IsEditable,
                        IsEncrypted = LookupType.IsEncrypted,
                        IsActive = LookupType.IsActive,

                    }).ToList();

                    objResponse.LookupType = lstLookupTypeSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.LookupType = null;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "LookupTypeGetBYID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.LookupType = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Save or Update the data For LookupType
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objLookupType">Object of LookupType</param>
        [AcceptVerbs("POST")]
        [ActionName("LookupTypeSave")]
        public LookupTypePostResponse LookupTypeSave(string Key, LookupTypePost objLookupType)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;
            LogingHelper.SaveAuditInfo(Key);

            LookupTypePostResponse objResponse = new LookupTypePostResponse();
            LookupTypeBAL objBAL = new LookupTypeBAL();
            LookupType objEntity = new LookupType();
            List<LookupTypePost> lstEntity = new List<LookupTypePost>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.LookupType = null;
                return objResponse;
            }

            string ValidationResponse = LookupValidation.ValidateLookupTypeObject(objLookupType);

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
                if (objLookupType.LookupTypeId > 0)
                {
                    objEntity = objBAL.Get_LookupType_byLookupTypeId(objLookupType.LookupTypeId);
                    if (objEntity != null)
                    {
                        objEntity.ModifiedOn = DateTime.Now;
                        objEntity.ModifiedBy = CreatedOrMoifiy;
                        objEntity.DepartmentId = objLookupType.DepartmentId;
                        objEntity.DivisionId = objLookupType.DivisionId;
                        objEntity.IsActive = objLookupType.IsActive;
                        objEntity.IsDeleted = objLookupType.IsDeleted;
                        objEntity.IsEditable = objLookupType.IsEditable;
                        objEntity.IsEncrypted = objLookupType.IsEncrypted;
                        objEntity.Name = objLookupType.Name;
                        objEntity.StateCode = objLookupType.StateCode;

                        objBAL.Save_LookupType(objEntity);

                        objResponse.Message = Messages.UpdateSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }
                }
                else
                {
                    objEntity = new LookupType();

                    objEntity.CreatedOn = DateTime.Now;
                    objEntity.CreatedBy = CreatedOrMoifiy;
                    //objEntity.ModifiedOn = null;
                    //objEntity.ModifiedBy = 0;
                    objEntity.DepartmentId = objLookupType.DepartmentId;
                    objEntity.DivisionId = objLookupType.DivisionId;
                    objEntity.IsActive = objLookupType.IsActive;
                    objEntity.IsDeleted = objLookupType.IsDeleted;
                    objEntity.IsEditable = objLookupType.IsEditable;
                    objEntity.IsEncrypted = objLookupType.IsEncrypted;
                    objEntity.Name = objLookupType.Name;
                    objEntity.StateCode = objLookupType.StateCode;

                    objEntity.LookupTypeId = objBAL.Save_LookupType(objEntity);
                    objLookupType.LookupTypeId = objEntity.LookupTypeId;

                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                }

                lstEntity.Add(objLookupType);
                objResponse.Status = true;

                objResponse.LookupType = lstEntity;

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "LookupTypeSave", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.LookupType = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }


        #endregion
    }
}
