using LAPP.LOGING.ENTITY;
using LAPP.WS.App_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LAPP.LOGING.DAL;
namespace LAPP.WS.Controllers.Loging
{
    public class CategoryLogController : ApiController
    {
        /// <summary>
        /// Save or Update the data
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objCategoryLog">Object of BoardAuthority</param>
        [AcceptVerbs("POST")]
        [ActionName("SaveCategoryLog")]
        public CategoryLogResponse SaveCategoryLog(string Key, CategoryLog objCategoryLog)
        {


            CategoryLogResponse objResponse = new CategoryLogResponse();
            CategoryLog objEntity = new CategoryLog();
            List<CategoryLog> lstEntity = new List<CategoryLog>();
            CategoryLogDAL objdal = new CategoryLogDAL();

            if (ActionContext.ModelState.IsValid == false)
            {
                objResponse.Status = false;
                ActionContext.Response = ActionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, ActionContext.ModelState);

                objResponse.Message = Newtonsoft.Json.JsonConvert.SerializeObject(Validations.GetErrorListFromModelState(ActionContext.ModelState));
                return objResponse;

            }

            try
            {
                if (objEntity.CategoryLogID > 0)
                {
                    objEntity = objdal.CategoryLog_Get_By_CategoryLogID(objCategoryLog.CategoryLogID);
                    if (objEntity != null)
                    {
                        objdal.Update_CategoryLog(objCategoryLog);
                    }
                }
                else
                {
                    objCategoryLog.CategoryLogID = objdal.Save_categorylog(objCategoryLog);
                }

                lstEntity.Add(objCategoryLog);
                objResponse.Status = true;
                objResponse.Message = "";
                objResponse.categorylog = lstEntity;

            }
            catch (Exception ex)
            {
                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.categorylog = null;

            }
            return objResponse;
        }

    }
}
