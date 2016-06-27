using LAPP.BAL;
using LAPP.BAL.Common;
using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.WS.App_Helper;
using LAPP.WS.App_Helper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using LAPP.WS.ValidateController.Login;
using LAPP.GlobalFunctions;
using LAPP.LOGING;
using LAPP.WS.ValidateController.User;

namespace LAPP.WS.Controllers.Common
{
    public class ProviderCurriculumController : ApiController
    {
        /// <summary>
        /// This API used for Provider Login.
        /// </summary>
        /// <param name="Key">API Security Key</param>
        [AcceptVerbs("GET")]
        [ActionName("ProvReqCourseOfStudyGetAll")]
        public ProvReqCourseOfStudyResponse ProvReqCourseOfStudyGetAll(string Key)
        {
            int CreateOrModify = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo();

            ProvReqCourseOfStudyResponse objResponse = new ProvReqCourseOfStudyResponse();

            ProvReqCourseOfStudyBAL objProvReqCourseOfStudyBAL = new ProvReqCourseOfStudyBAL();
            List<ProvReqCourseOfStudy> lstProvReqCourseOfStudy = new List<ProvReqCourseOfStudy>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Message = "User session has expired.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
                lstProvReqCourseOfStudy = objProvReqCourseOfStudyBAL.Get_All_ProvReqCourseOfStudy();
                if(lstProvReqCourseOfStudy!=null && lstProvReqCourseOfStudy.Count>0)
                {
                    objResponse.ResponseReason = "To Get All ProvReqCourseOfStudy";
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstProvReqCourseOfStudySelected = lstProvReqCourseOfStudy.Select(CourseOfStudy => new
                    {
                        ProvReqCourseofStudyId = CourseOfStudy.ProvReqCourseofStudyId,
                        ProviderId = CourseOfStudy.ProviderId,
                        ApplicationId = CourseOfStudy.ApplicationId,
                        ReqCourseofStudyNameId = CourseOfStudy.ReqCourseofStudyNameId,
                        ReqCourseofStudyName = CourseOfStudy.ReqCourseofStudyName,
                        MinimumReqCourseHours = CourseOfStudy.MinimumReqCourseHours,
                        ReferenceNumber = CourseOfStudy.ReferenceNumber,
                        IsActive = CourseOfStudy.IsActive,
                        IsDeleted = CourseOfStudy.IsDeleted,
                        CreatedBy = CourseOfStudy.CreatedBy,
                        CreatedOn = CourseOfStudy.CreatedOn,
                        ModifiedBy = CourseOfStudy.ModifiedBy,
                        ModifiedOn = CourseOfStudy.ModifiedOn,
                        ProvReqCourseofStudyGuid = CourseOfStudy.ProvReqCourseofStudyGuid
                    }
                    ).ToList();

                    objResponse.ProvReqCourseOfStudy = lstProvReqCourseOfStudySelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ProvReqCourseOfStudy = null;
                }
            }
            catch(Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ProvReqCourseOfStudyGetAll", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ProvReqCourseOfStudy = null;
            }
            return objResponse;
        }


        /// <summary>
        /// This API used for Provider Login.
        /// </summary>
        /// <param name="Key">API Security Key</param>
        /// <param name="objProvReqCourseTitle">object to receive all values</param>
       // /// <param name="CourseTitleName">Course Title Name</param>
        ///// <param name="CourseHours">No of Hours for a course</param>

        [AcceptVerbs("POST")]
        [ActionName("ProvReqCourseTitle")]
        public ProvReqCourseTitleResponse ProvReqCourseTitle(string Key, ProvReqCourseTitle objProvReqCourseTitle)
        {
            int CreateOrModify = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo();

            ProvReqCourseTitleResponse objResponse = new ProvReqCourseTitleResponse();

            ProvReqCourseTitleBAL objProvReqCourseTitleBAL = new ProvReqCourseTitleBAL();
            //List<ProvReqCourseTitle> lstProvReqCourseOfStudy = new List<ProvReqCourseTitle>();
            ProvReqCourseTitle objProvReqCourseTitleEntity = new ProvReqCourseTitle();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Message = "User session has expired.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
                if(objProvReqCourseTitle != null)// && CourseTitleName!=null && CourseHours!=null)
                {
                    //objProvReqCourseTitleEntity.ProvReqCourseTitleId = objProvReqCourseTitle.ProvReqCourseTitleId;
                    objProvReqCourseTitleEntity.ProvReqCourseofStudyId = objProvReqCourseTitle.ProvReqCourseofStudyId;
                    objProvReqCourseTitleEntity.ProviderId = objProvReqCourseTitle.ProviderId;
                    objProvReqCourseTitleEntity.ApplicationId = objProvReqCourseTitle.ApplicationId;
                    objProvReqCourseTitleEntity.CourseTitleName = objProvReqCourseTitle.CourseTitleName;
                    objProvReqCourseTitleEntity.CourseHours = objProvReqCourseTitle.CourseHours;
                    objProvReqCourseTitleEntity.ReferenceNumber = objProvReqCourseTitle.ReferenceNumber;
                    objProvReqCourseTitleEntity.IsActive = objProvReqCourseTitle.IsActive;
                    objProvReqCourseTitleEntity.IsDeleted = objProvReqCourseTitle.IsDeleted;
                    objProvReqCourseTitleEntity.CreatedBy = objProvReqCourseTitle.CreatedBy;
                    objProvReqCourseTitleEntity.CreatedOn = objProvReqCourseTitle.CreatedOn;
                    //objProvReqCourseTitleEntity.ModifiedBy = objProvReqCourseTitle.ModifiedBy;
                    //objProvReqCourseTitleEntity.ModifiedOn = objProvReqCourseTitle.ModifiedOn;
                    objProvReqCourseTitleEntity.ProviderOtherProgramGuid = Guid.NewGuid().ToString();//objProvReqCourseTitle.ProviderOtherProgramGuid;

                    objProvReqCourseTitleBAL.Save_ProvReqCourseTitle(objProvReqCourseTitleEntity);

                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    //objProvReqCourseTitleEntity.
                }
                else
                {
                    objResponse.Message = "Invalid Object.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
                
            }
            catch(Exception ex)
            {
                LogingHelper.SaveExceptionInfo("", ex, "ProvReqCourseTitle", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
            }
            return objResponse;
        }

        /// <summary>
        /// This API used for Provider Login.
        /// </summary>
        /// <param name="Key">API Security Key</param>
        /// <param name="CourseOfStudyId">Course of Study Id</param>
        /// <param name="ProviderId">Provider Id</param>
        [AcceptVerbs("GET")]
        [ActionName("ProvReqCourseTitleGetAllByCourseOfStudyId")]
        public ProvReqCourseTitleResponse ProvReqCourseTitleGetAllByCourseOfStudyId(string Key, int CourseOfStudyId, int ProviderId)
        {
            int CreateOrModify = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo();

            ProvReqCourseTitleResponse objResponse = new ProvReqCourseTitleResponse();

            ProvReqCourseTitleBAL objProvReqCourseTitleBAL = new ProvReqCourseTitleBAL();
            List<ProvReqCourseTitle> lstProvReqCourseTitle = new List<ProvReqCourseTitle>();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Message = "User session has expired.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
                lstProvReqCourseTitle = objProvReqCourseTitleBAL.Get_All_ProvReqCourseTitle_By_CourseOfStudyId(CourseOfStudyId, ProviderId);
                if (lstProvReqCourseTitle != null && lstProvReqCourseTitle.Count > 0)
                {
                    objResponse.ResponseReason = "To Get All ProvReqCourseTitle by Course of Study Id";
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    var lstProvReqCourseTitleSelected = lstProvReqCourseTitle.Select(CourseTitle => new
                    {
                        ProvReqCourseTitleId = CourseTitle.ProvReqCourseTitleId,
                        ProvReqCourseofStudyId = CourseTitle.ProvReqCourseofStudyId,
                        ProviderId = CourseTitle.ProviderId,
                        ApplicationId = CourseTitle.ApplicationId,
                        CourseTitleName = CourseTitle.CourseTitleName,
                        CourseHours = CourseTitle.CourseHours,
                        ReferenceNumber = CourseTitle.ReferenceNumber,
                        IsActive = CourseTitle.IsActive,
                        IsDeleted = CourseTitle.IsDeleted,
                        CreatedBy = CourseTitle.CreatedBy,
                        CreatedOn = CourseTitle.CreatedOn,
                        ModifiedBy = CourseTitle.ModifiedBy,
                        ModifiedOn = CourseTitle.ModifiedOn,
                        ProviderOtherProgramGuid = CourseTitle.ProviderOtherProgramGuid
                    }
                    ).ToList();

                    objResponse.ProvReqCourseTitle= lstProvReqCourseTitleSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ProvReqCourseTitle = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ProvReqCourseTitleGetAllByCourseOfStudyId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ProvReqCourseTitle = null;
            }
            return objResponse;
        }

        /// <summary>
        /// This API used to save clinic hours in ProvClinicHours table in database.
        /// </summary>
        /// <param name="Key">API Security Key</param>
        /// <param name="objProvClinicHours">object to receive all values</param>
        [AcceptVerbs("POST")]
        [ActionName("ProvClinicHours")]
        public ProvClinicHoursResponse ProvClinicHours(string Key, ProvClinicHours objProvClinicHours)
        {
            int CreateOrModify = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo();

            ProvClinicHoursResponse objResponse = new ProvClinicHoursResponse();

            ProvClinicHoursBAL objProvReqCourseTitleBAL = new ProvClinicHoursBAL();
            //List<ProvReqCourseTitle> lstProvReqCourseOfStudy = new List<ProvReqCourseTitle>();
            ProvClinicHours objProvClinicHoursEntity = new ProvClinicHours();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Message = "User session has expired.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
                if (objProvClinicHours != null)// && CourseTitleName!=null && CourseHours!=null)
                {
                    //objProvClinicHoursEntity.ProvClinicHoursId = objProvClinicHours.ProvClinicHoursId;
                    objProvClinicHoursEntity.ProviderId = objProvClinicHours.ProviderId;
                    objProvClinicHoursEntity.ApplicationId = objProvClinicHours.ApplicationId;
                    objProvClinicHoursEntity.ClinicHours = objProvClinicHours.ClinicHours;
                    objProvClinicHoursEntity.ReferenceNumber = objProvClinicHours.ReferenceNumber;
                    objProvClinicHoursEntity.IsActive = objProvClinicHours.IsActive;
                    objProvClinicHoursEntity.IsDeleted = objProvClinicHours.IsDeleted;
                    objProvClinicHoursEntity.CreatedBy = objProvClinicHours.CreatedBy;
                    objProvClinicHoursEntity.CreatedOn = objProvClinicHours.CreatedOn;
                    //objProvClinicHoursEntity.ModifiedBy = objProvClinicHours.ModifiedBy;
                    //objProvClinicHoursEntity.ModifiedOn = objProvClinicHours.ModifiedOn;
                    objProvClinicHoursEntity.ProvClinicHoursGuid = Guid.NewGuid().ToString();// objProvClinicHours.ProvClinicHoursGuid;

                    objProvReqCourseTitleBAL.Save_ProvClinicHours(objProvClinicHoursEntity);

                    objResponse.Message = Messages.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    //objProvReqCourseTitleEntity.
                }
                else
                {
                    objResponse.Message = "Invalid Object.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.InvalidRequestObject).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }

            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo("", ex, "ProvClinicHours", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
            }
            return objResponse;
        }

        /// <summary>
        /// This API used to save clinic hours in ProvClinicHours table in database.
        /// </summary>
        /// <param name="Key">API Security Key</param>
        /// <param name="ProviderId">Provider Id</param>
        [AcceptVerbs("GET")]
        [ActionName("ProvClinicHoursGetByProviderId")]
        public ProvClinicHoursResponse ProvClinicHoursGetByProviderId(string Key, int ProviderId)
        {
            int CreateOrModify = TokenHelper.GetTokenByKey(Key).UserId;

            LogingHelper.SaveAuditInfo();

            ProvClinicHoursResponse objResponse = new ProvClinicHoursResponse();

            ProvClinicHoursBAL objProvClinicHoursBAL = new ProvClinicHoursBAL();
            ProvClinicHours objEntity = new ProvClinicHours();
            try
            {
                if (!TokenHelper.ValidateToken(Key))
                {
                    objResponse.Message = "User session has expired.";
                    objResponse.Status = false;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                    objResponse.ResponseReason = "";
                    return objResponse;
                }
                objEntity = objProvClinicHoursBAL.Get_ProvClinicHours(ProviderId);
                if (objEntity != null)
                {
                    objResponse.ResponseReason = "To Get ProvClinicHours by ProviderId";
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ProvClinicHours = objEntity;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ProvClinicHours = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ProvClinicHoursGetByProviderId", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ProvClinicHours= null;
            }
            return objResponse;
        }


    }
}
