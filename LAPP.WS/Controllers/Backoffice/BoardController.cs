using LAPP.BAL;
using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.WS.App_Helper;
using LAPP.WS.App_Helper.Common;
using LAPP.WS.ValidateController.Backoffice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace LAPP.WS.Controllers.Board
{
    /// <summary>
    /// To access BoardAuthority Controller
    /// </summary>
    public class BoardController : ApiController
    {



        /// <summary>
        /// Looks up all data for BoardAuthority by Key.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("GET")]
        [ActionName("BoardAuthorityGet")]
        public BoardAuthorityResponse GetAllBoardAuthority(string Key)
        {

            LogingHelper.SaveAuditInfo(Key);

            BoardAuthorityResponse objResponse = new BoardAuthorityResponse();
            BoardAuthorityBAL objBAL = new BoardAuthorityBAL();
            BoardAuthority objEntity = new BoardAuthority();

            List<BoardAuthority> lstBoardAuthority = objBAL.Get_All_BoardAuthority();

            try
            {

                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.BoardAuthority = null;
                    return objResponse;
                }

                var lstBoardAuthoaritySelected = lstBoardAuthority.Select(obj => new
                {
                    BoardAuthorityId = obj.BoardAuthorityId,
                    StateCode = obj.StateCode,
                    Name = obj.Name,
                    Code = obj.Code,
                    Acronym = obj.Acronym,
                    Url = obj.Url,
                    PhysicalAddressLine1 = obj.PhysicalAddressLine1,
                    PhysicalAddressLine2 = obj.PhysicalAddressLine2,
                    PhysicalAddressCity = obj.PhysicalAddressCity,
                    PhysicalAddressState = obj.PhysicalAddressState,
                    PhysicalAddressZip = obj.PhysicalAddressZip,
                    IsMailingSameasPhysical = obj.IsMailingSameasPhysical,
                    MailingAddressLine1 = obj.MailingAddressLine1,
                    MailingAddressLine2 = obj.MailingAddressLine2,
                    MailingAddressCity = obj.MailingAddressCity,
                    MailingAddressState = obj.MailingAddressState,
                    MailingAddressZip = obj.MailingAddressZip,
                    ContactPhone = obj.ContactPhone,
                    ContactEmail = obj.ContactEmail,
                    ContactFax = obj.ContactFax,
                    AlternatePhone = obj.AlternatePhone,
                    SystemName = obj.SystemName,
                    SystemAbbreviation = obj.SystemAbbreviation,
                    SystemUrl = obj.SystemUrl,
                    ApplicationSystemUrl = obj.ApplicationSystemUrl,
                    SystemContact = obj.SystemContact,

                    IsActive = obj.IsActive
                }).ToList();

                objResponse.Status = true;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                objResponse.Message = "";
                objResponse.BoardAuthority = lstBoardAuthoaritySelected;

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "GetBoardAuthority", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.BoardAuthority = null;

            }
            return objResponse;


        }

        /// <summary>
        /// Save or Update the data for BoardAuthority
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objBoardAuthority">Object of BoardAuthority</param>
        [AcceptVerbs("POST")]
        [ActionName("BoardAuthoritySave")]
        public BoardAuthorityResponse SaveBoardAuthority(string Key, BoardAuthorityRequest objBoardAuthority)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;
            LogingHelper.SaveAuditInfo(Key);

            BoardAuthorityResponse objResponse = new BoardAuthorityResponse();
            BoardAuthorityBAL objBAL = new BoardAuthorityBAL();
            BoardAuthority objEntity = new BoardAuthority();
            List<BoardAuthority> lstEntity = new List<BoardAuthority>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.BoardAuthority = null;
                return objResponse;
            }

            string ValidationResponse = BoardValidation.ValidateBoardAuthorityObject(objBoardAuthority);

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
                if (objBoardAuthority.BoardAuthorityId > 0)
                {
                    objEntity = objBAL.Get_BoardAuthority_byID(objBoardAuthority.BoardAuthorityId);
                    if (objEntity != null)
                    {
                        objEntity.Acronym = objBoardAuthority.Acronym;
                        objEntity.AlternatePhone = objBoardAuthority.AlternatePhone;
                        objEntity.ApplicationSystemUrl = objBoardAuthority.ApplicationSystemUrl;
                     
                        objEntity.Code = objBoardAuthority.Code;
                        objEntity.ContactEmail = objBoardAuthority.ContactEmail;
                        objEntity.ContactFax = objBoardAuthority.ContactFax;
                        objEntity.ContactPhone = objBoardAuthority.ContactPhone;
                        objEntity.IsMailingSameasPhysical = objBoardAuthority.IsMailingSameasPhysical;
                        objEntity.MailingAddressCity = objBoardAuthority.MailingAddressLine1;

                        objEntity.MailingAddressLine2 = objBoardAuthority.MailingAddressLine2;
                        objEntity.MailingAddressState = objBoardAuthority.MailingAddressState;
                        objEntity.MailingAddressZip = objBoardAuthority.MailingAddressZip;
                        objEntity.Name = objBoardAuthority.Name;
                        objEntity.PhysicalAddressCity = objBoardAuthority.PhysicalAddressCity;
                        objEntity.PhysicalAddressLine1 = objBoardAuthority.PhysicalAddressLine1;
                        objEntity.PhysicalAddressLine2 = objBoardAuthority.PhysicalAddressLine2;
                        objEntity.PhysicalAddressState = objBoardAuthority.PhysicalAddressState;
                        objEntity.PhysicalAddressZip = objBoardAuthority.PhysicalAddressZip;
                        objEntity.StateCode = objBoardAuthority.StateCode;
                        objEntity.SystemAbbreviation = objBoardAuthority.SystemAbbreviation;
                        objEntity.SystemContact = objBoardAuthority.SystemContact;
                        objEntity.SystemName = objBoardAuthority.SystemName;
                        objEntity.SystemUrl = objBoardAuthority.SystemUrl;
                        objEntity.Url = objBoardAuthority.Url;

                        objEntity.ModifiedOn = DateTime.Now;
                        objEntity.ModifiedBy = CreatedOrMoifiy;

                        objBAL.Update_BoardAuthority(objEntity);
                        objResponse.Message = Messages.UpdateSuccess;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }
                }
                else
                {
                    objEntity = new BoardAuthority();
                    objEntity.Acronym = objBoardAuthority.Acronym;
                    objEntity.AlternatePhone = objBoardAuthority.AlternatePhone;
                    objEntity.ApplicationSystemUrl = objBoardAuthority.ApplicationSystemUrl;
                  
                    objEntity.Code = objBoardAuthority.Code;
                    objEntity.ContactEmail = objBoardAuthority.ContactEmail;
                    objEntity.ContactFax = objBoardAuthority.ContactFax;
                    objEntity.ContactPhone = objBoardAuthority.ContactPhone;
                    objEntity.IsMailingSameasPhysical = objBoardAuthority.IsMailingSameasPhysical;
                    objEntity.MailingAddressCity = objBoardAuthority.MailingAddressLine1;

                    objEntity.MailingAddressLine2 = objBoardAuthority.MailingAddressLine2;
                    objEntity.MailingAddressState = objBoardAuthority.MailingAddressState;
                    objEntity.MailingAddressZip = objBoardAuthority.MailingAddressZip;
                    objEntity.Name = objBoardAuthority.Name;
                    objEntity.PhysicalAddressCity = objBoardAuthority.PhysicalAddressCity;
                    objEntity.PhysicalAddressLine1 = objBoardAuthority.PhysicalAddressLine1;
                    objEntity.PhysicalAddressLine2 = objBoardAuthority.PhysicalAddressLine2;
                    objEntity.PhysicalAddressState = objBoardAuthority.PhysicalAddressState;
                    objEntity.PhysicalAddressZip = objBoardAuthority.PhysicalAddressZip;
                    objEntity.StateCode = objBoardAuthority.StateCode;
                    objEntity.SystemAbbreviation = objBoardAuthority.SystemAbbreviation;
                    objEntity.SystemContact = objBoardAuthority.SystemContact;
                    objEntity.SystemName = objBoardAuthority.SystemName;
                    objEntity.SystemUrl = objBoardAuthority.SystemUrl;
                    objEntity.Url = objBoardAuthority.Url;


                    objEntity.BoardAuthorityGuid = Guid.NewGuid();
                    objEntity.CreatedOn = DateTime.Now;
                    objEntity.ModifiedOn = null;
                    objEntity.CreatedBy = CreatedOrMoifiy;

                    objEntity.BoardAuthorityId = objBAL.Save_BoardAuthority(objEntity);
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                }

                lstEntity.Add(objEntity);
                objResponse.Status = true;


                var lstBoardAuthoaritySelected = lstEntity.Select(obj => new
                {
                    BoardAuthorityId = obj.BoardAuthorityId,
                    StateCode = obj.StateCode,
                    Name = obj.Name,
                    Code = obj.Code,
                    Acronym = obj.Acronym,
                    Url = obj.Url,
                    PhysicalAddressLine1 = obj.PhysicalAddressLine1,
                    PhysicalAddressLine2 = obj.PhysicalAddressLine2,
                    PhysicalAddressCity = obj.PhysicalAddressCity,
                    PhysicalAddressState = obj.PhysicalAddressState,
                    PhysicalAddressZip = obj.PhysicalAddressZip,
                    IsMailingSameasPhysical = obj.IsMailingSameasPhysical,
                    MailingAddressLine1 = obj.MailingAddressLine1,
                    MailingAddressLine2 = obj.MailingAddressLine2,
                    MailingAddressCity = obj.MailingAddressCity,
                    MailingAddressState = obj.MailingAddressState,
                    MailingAddressZip = obj.MailingAddressZip,
                    ContactPhone = obj.ContactPhone,
                    ContactEmail = obj.ContactEmail,
                    ContactFax = obj.ContactFax,
                    AlternatePhone = obj.AlternatePhone,
                    SystemName = obj.SystemName,
                    SystemAbbreviation = obj.SystemAbbreviation,
                    SystemUrl = obj.SystemUrl,
                    ApplicationSystemUrl = obj.ApplicationSystemUrl,
                    SystemContact = obj.SystemContact,

                    IsActive = obj.IsActive
                }).ToList();
                objResponse.BoardAuthority = lstBoardAuthoaritySelected;

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "SaveBoardAuthority", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.BoardAuthority = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");

            }

            return objResponse;
        }


    }



}
