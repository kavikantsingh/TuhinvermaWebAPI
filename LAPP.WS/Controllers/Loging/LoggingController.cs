using LAPP.LOGING.ENTITY;
using LAPP.WS.App_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LAPP.LOGING.DAL;
using System.Web;
using LAPP.WS.ValidateController.Loging;
using LAPP.WS.App_Helper.Common;
using LAPP.ENTITY.Enumeration;

namespace LAPP.WS.Controllers.Loging
{
    public class LoggingController : ApiController
    {
        /// <summary>
        /// Save or Update the data for AuditVisitInfo
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objAuditvisitInfo">Object of AuditVisitInfo</param>
        [AcceptVerbs("POST")]
        [ActionName("AuditvisitInfoSave")]
        public AuditvisitInfoResponse AuditvisitInfoSave(string Key, AuditvisitInfo objAuditvisitInfo)
        {
            int CreatedOrMoifiy = TokenHelper.GetTokenByKey(Key).UserId;

            AuditvisitInfoResponse objResponse = new AuditvisitInfoResponse();
            AuditvisitInfo objEntity = new AuditvisitInfo();
            List<AuditvisitInfo> lstEntity = new List<AuditvisitInfo>();
            AuditVisitInfoDAL objdal = new AuditVisitInfoDAL();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.AuditvisitInfo = null;
                return objResponse;
            }


            string ValidationResponse = LoggingValidation.ValidateAuditvisitInfoObject(objAuditvisitInfo);

            if (!string.IsNullOrEmpty(ValidationResponse))
            {
                objResponse.Message = "Validation Error";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                objResponse.ResponseReason = ValidationResponse;
                return objResponse;
            }

            LogingHelper.SaveAuditInfo(Key);

            try
            {
                if (objAuditvisitInfo.ID > 0)
                {
                    objEntity = objdal.AuditvisitInfo_Get_By_ID(objAuditvisitInfo.ID);
                    if (objEntity != null)
                    {
                        objdal.Update_AuditvisitInfo(objAuditvisitInfo);

                        lstEntity.Add(objAuditvisitInfo);
                        objResponse.Status = true;
                        objResponse.Message = Messages.UpdateSuccess;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                        objResponse.AuditvisitInfo = lstEntity;
                    }
                }
                else
                {
                    objAuditvisitInfo.ID = objdal.Save_AuditvisitInfo(objAuditvisitInfo);

                    lstEntity.Add(objAuditvisitInfo);
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.AuditvisitInfo = lstEntity;
                }



            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "SaveAuditvisitInfo", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");

                objResponse.Message = ex.Message;
                objResponse.AuditvisitInfo = null;

            }
            return objResponse;
        }


        /// <summary>
        /// Save or Update the data for category
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objCategory">Object of Category</param>
        [AcceptVerbs("POST")]
        [ActionName("CategorySave")]
        public CategoryResponse CategorySave(string Key, Category objCategory)
        {

            CategoryResponse objResponse = new CategoryResponse();
            Category objEntity = new Category();
            List<Category> lstEntity = new List<Category>();
            CategoryDAL objdal = new CategoryDAL();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.Category = null;
                return objResponse;
            }

            string ValidationResponse = LoggingValidation.ValidateCategoryObject(objCategory);

            if (!string.IsNullOrEmpty(ValidationResponse))
            {
                objResponse.Message = "Validation Error";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                objResponse.ResponseReason = ValidationResponse;
                return objResponse;
            }

            LogingHelper.SaveAuditInfo(Key);

            try
            {
                if (objCategory.CategoryID > 0)
                {
                    objEntity = objdal.Category_Get_By_CategoryID(objCategory.CategoryID);
                    if (objEntity != null)
                    {
                        objdal.Update_Category(objCategory);
                        lstEntity.Add(objCategory);
                        objResponse.Status = true;
                        objResponse.Message = Messages.UpdateSuccess;
                        objResponse.Category = lstEntity;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }
                }
                else
                {
                    objCategory.CategoryID = objdal.Save_Category(objCategory);
                    lstEntity.Add(objCategory);
                    objResponse.Status = true;
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Category = lstEntity;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "SaveCategory", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.Category = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
            }
            return objResponse;
        }


        /// <summary>
        /// Save or Update the data for CategoryLog
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objCategoryLog">Object of CategoryLog</param>
        [AcceptVerbs("POST")]
        [ActionName("CategoryLogSave")]
        public CategoryLogResponse CategoryLogSave(string Key, CategoryLog objCategoryLog)
        {

            CategoryLogResponse objResponse = new CategoryLogResponse();
            CategoryLog objEntity = new CategoryLog();
            List<CategoryLog> lstEntity = new List<CategoryLog>();
            CategoryLogDAL objdal = new CategoryLogDAL();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.categorylog = null;
                return objResponse;
            }

            string ValidationResponse = LoggingValidation.ValidateCategoryLogObject(objCategoryLog);

            if (!string.IsNullOrEmpty(ValidationResponse))
            {
                objResponse.Message = "Validation Error";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                objResponse.ResponseReason = ValidationResponse;
                return objResponse;
            }

            LogingHelper.SaveAuditInfo(Key);

            try
            {
                if (objCategoryLog.CategoryLogID > 0)
                {
                    objEntity = objdal.CategoryLog_Get_By_CategoryLogID(objCategoryLog.CategoryLogID);
                    if (objEntity != null)
                    {
                        objdal.Update_CategoryLog(objCategoryLog);

                        lstEntity.Add(objCategoryLog);
                        objResponse.Status = true;
                        objResponse.Message = Messages.UpdateSuccess;
                        objResponse.categorylog = lstEntity;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }
                }
                else
                {
                    objCategoryLog.CategoryLogID = objdal.Save_categorylog(objCategoryLog);

                    lstEntity.Add(objCategoryLog);
                    objResponse.Status = true;
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.categorylog = lstEntity;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                }


            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "SaveCategoryLog", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.categorylog = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");


            }
            return objResponse;
        }


        /// <summary>
        /// Save or Update the data for DataLog
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objDatalog">Object of DataLog</param>
        [AcceptVerbs("POST")]
        [ActionName("DatalogSave")]
        public DatalogResponse DatalogSave(string Key, Datalog objDatalog)
        {
            DatalogResponse objResponse = new DatalogResponse();
            Datalog objEntity = new Datalog();
            List<Datalog> lstEntity = new List<Datalog>();
            DatalogDAL objdal = new DatalogDAL();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.Datalog = null;
                return objResponse;
            }

            string ValidationResponse = LoggingValidation.ValidateDatalogObject(objDatalog);

            if (!string.IsNullOrEmpty(ValidationResponse))
            {
                objResponse.Message = "Validation Error";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                objResponse.ResponseReason = ValidationResponse;
                return objResponse;
            }

            LogingHelper.SaveAuditInfo(Key);

            try
            {
                if (objDatalog.DataLogId > 0)
                {
                    objEntity = objdal.Datalog_Get_By_DatalogID(objDatalog.DataLogId);
                    if (objEntity != null)
                    {
                        objdal.Update_Datalog(objDatalog);

                        lstEntity.Add(objDatalog);
                        objResponse.Status = true;
                        objResponse.Message = Messages.UpdateSuccess;
                        objResponse.Datalog = lstEntity;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }
                }
                else
                {
                    objDatalog.DataLogId = objdal.Save_Datalog(objDatalog);

                    lstEntity.Add(objDatalog);
                    objResponse.Status = true;
                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Datalog = lstEntity;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "SaveDatalog", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.Datalog = null;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");

            }
            return objResponse;
        }

        /// <summary>
        /// Save or Update the data for Log
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        /// <param name="objLog">Object of Log</param>
        [AcceptVerbs("POST")]
        [ActionName("LogSave")]
        public LogResponse LogSave(string Key, Log objLog)
        {

            LogResponse objResponse = new LogResponse();
            Log objEntity = new Log();
            List<Log> lstEntity = new List<Log>();
            LogDAL objdal = new LogDAL();

            if (!TokenHelper.ValidateToken(Key))
            {
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                objResponse.Message = "User session has expired.";
                objResponse.Log = null;
                return objResponse;
            }

            string ValidationResponse = LoggingValidation.ValidateLogObject(objLog);

            if (!string.IsNullOrEmpty(ValidationResponse))
            {
                objResponse.Message = "Validation Error";
                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                objResponse.ResponseReason = ValidationResponse;
                return objResponse;
            }

            LogingHelper.SaveAuditInfo(Key);


            try
            {
                if (objLog.LogID > 0)
                {
                    objEntity = objdal.Log_Get_By_LogID(objLog.LogID);
                    if (objEntity != null)
                    {
                        objdal.Update_Log(objLog);

                        lstEntity.Add(objLog);
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                        objResponse.Message = Messages.UpdateSuccess;
                        objResponse.Log = lstEntity;
                    }
                }
                else
                {
                    objLog.CreatedOn = DateTime.Now;
                    objLog.LogID = objdal.Save_Log(objLog);

                    lstEntity.Add(objLog);
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Log = lstEntity;
                }



            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "SaveLog", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");

                objResponse.Log = null;

            }
            return objResponse;
        }



    }

}
