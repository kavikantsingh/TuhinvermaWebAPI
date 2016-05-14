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

namespace LAPP.WS.Controllers.Misc
{
    /// <summary>
    /// Populate State data
    /// </summary>
    public class StateController : ApiController
    {

        /// <summary>
        /// Looks up all data for State by Key.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="CountryID">The Country ID of the states.</param>
        [HttpGet]
        [ActionName("StateGetByCountryID")]
        public StateResponse GetStateByCountryID(string Key, int CountryID)
        {
            LogingHelper.SaveAuditInfo(Key);

            StateResponse objResponse = new StateResponse();
            StateBAL objBAL = new StateBAL();
            State objEntity = new State();
            try
            {

                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.Message = "User session has expired.";
                    objResponse.State = null;
                    return objResponse;
                }

                List<State> lstState = objBAL.Get_State_ByCountryID(CountryID);
                if (lstState != null && lstState.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.State = lstState;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Message = "No record found.";
                    objResponse.State = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "GetStateByCountryID", ENTITY.Enumeration.eSeverity.Error);

                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.State = null;

            }
            return objResponse;


        }

        /// <summary>
        /// Save or Update the data for State
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objState">Object of State</param>
        [AcceptVerbs("POST")]
        [ActionName("StateSave")]
        public StateResponse SaveState(string Key, State objState)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;
            LogingHelper.SaveAuditInfo(Key);

            StateResponse objResponse = new StateResponse();
            StateBAL objBAL = new StateBAL();
            State objEntity = new State();
            List<State> lstEntity = new List<State>();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.State = null;
                return objResponse;
            }

            string ValidationResponse = StateValidation.ValidateStateObject(objState);

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
                if (objState.StateId > 0)
                {
                    objEntity = objBAL.Get_State_ByStateID(objState.StateId);
                    if (objEntity != null)
                    {
                        objState.ModifiedBy = CreatedOrMoifiy;

                        objBAL.Save_State(objState);
                        objResponse.Message = Messages.UpdateSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    }
                }
                else
                {
                    objState.CreatedBy = CreatedOrMoifiy;

                    objState.StateId = objBAL.Save_State(objState);
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }

                lstEntity.Add(objState);
                objResponse.Status = true;
                objResponse.State = lstEntity;

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "SaveState", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.State = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }

        ///// <summary>
        ///// Delete data by State Id.
        ///// </summary>
        ///// <param name="StateId">The State Id of the data.</param>
        ///// <param name="Key">The Key of the data.</param>
        //[HttpGet]
        //[ActionName("DeleteStateByID")]
        //public StateResponse DeleteStateByID(string Key, int StateId)
        //{
        //    LogingHelper.SaveAuditInfo(Key);

        //    StateResponse objResponse = new StateResponse();
        //    StateBAL objBAL = new StateBAL();

        //    try
        //    {
        //        objBAL.Delete_State_byID(StateId);
        //        objResponse.Status = true;
        //        objResponse.Message = Messages.DeleteSuccess;
        //        objResponse.State = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogingHelper.SaveExceptionInfo(Key, ex, "DeleteStateByID", ENTITY.Enumeration.eSeverity.Error);

        //        objResponse.Status = false;
        //        objResponse.Message = ex.Message;
        //        objResponse.State = null;

        //    }
        //    return objResponse;


        //}


    }
}
