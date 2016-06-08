using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.LOGING;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.BAL.Backoffice.IndividualFolder
{
    public class IndividualEducationCS
    {
        public static IndividualCECourseResponseRequest SaveIndividualEducation(Token objToken, IndividualCECourseResponse objCECourseResponse)
        {
            IndividualCECourseResponseRequest objResponse = new IndividualCECourseResponseRequest();
            IndividualCECourseBAL objIndividualCECourseBAL = new IndividualCECourseBAL();
            IndividualCECourseResponse objIndividualCECourseResponse = new IndividualCECourseResponse();
            IndividualCECourse objCehCourse = new IndividualCECourse();
            List<IndividualCECourse> lstIndividualCECourse = new List<IndividualCECourse>();
            List<IndividualCECourseResponse> lstCeCourseResponse = new List<IndividualCECourseResponse>();
            try
            {
                int individualId = objCECourseResponse.IndividualId;
                int? applicationId = objCECourseResponse.ApplicationId;

                objCehCourse = objIndividualCECourseBAL.Get_IndividualCECourse_By_IndividualCECourseId(objCECourseResponse.IndividualCECourseId);
                if (objCehCourse != null)
                {
                    objCehCourse.IndividualId = individualId;
                    objCehCourse.ApplicationId = objCECourseResponse.ApplicationId;
                    objCehCourse.CECourseTypeId = objCECourseResponse.CECourseTypeId;
                    objCehCourse.CECourseActivityTypeId = objCECourseResponse.CECourseActivityTypeId;
                    objCehCourse.CECourseStartDate = objCECourseResponse.CECourseStartDate;
                    objCehCourse.CECourseEndDate = objCECourseResponse.CECourseEndDate;
                    objCehCourse.CECourseDueDate = objCECourseResponse.CECourseDueDate;
                    objCehCourse.CECourseDate = objCECourseResponse.CECourseDate;
                    objCehCourse.CECourseHours = objCECourseResponse.CECourseHours;
                    objCehCourse.CECourseUnits = objCECourseResponse.CECourseUnits;
                    objCehCourse.ProgramSponsor = objCECourseResponse.ProgramSponsor;
                    objCehCourse.CourseNameTitle = objCECourseResponse.CourseNameTitle;
                    objCehCourse.CourseSponsor = objCECourseResponse.CourseSponsor;
                    objCehCourse.CECourseReportingYear = objCECourseResponse.CECourseReportingYear;
                    objCehCourse.CECourseStatusId = objCECourseResponse.CECourseStatusId;
                    objCehCourse.InstructorBiography = objCECourseResponse.InstructorBiography;
                    objCehCourse.ActivityDesc = objCECourseResponse.ActivityDesc;
                    objCehCourse.ReferenceNumber = objCECourseResponse.ReferenceNumber;
                    objCehCourse.IsActive = objCECourseResponse.IsActive;
                    objCehCourse.IsDeleted = objCECourseResponse.IsDeleted;
                    objCehCourse.IndividualLicenseId = objCECourseResponse.IndividualLicenseId;

                    objCehCourse.ModifiedBy = objToken.UserId;
                    objCehCourse.ModifiedOn = DateTime.Now;

                    objIndividualCECourseBAL.Save_IndividualCECourse(objCehCourse);

                    //SAVE LOG

                    string logText = "Individual Education updated successfully. Updated on " + DateTime.Now.ToShortDateString();
                    string logSource = eCommentLogSource.WSAPI.ToString();
                    LogHelper.SaveIndividualLog(individualId, applicationId, logSource, logText, objToken.UserId, null, null, null);

                    //END SAVE LOG

                    objResponse.Message = MessagesClass.UpdateSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }

                else
                {
                    objCehCourse = new IndividualCECourse();

                    objCehCourse.IndividualId = individualId;
                    objCehCourse.ApplicationId = objCECourseResponse.ApplicationId;
                    objCehCourse.CECourseTypeId = objCECourseResponse.CECourseTypeId;
                    objCehCourse.CECourseActivityTypeId = objCECourseResponse.CECourseActivityTypeId;
                    objCehCourse.CECourseStartDate = objCECourseResponse.CECourseStartDate;
                    objCehCourse.CECourseEndDate = objCECourseResponse.CECourseEndDate;
                    objCehCourse.CECourseDueDate = objCECourseResponse.CECourseDueDate;
                    objCehCourse.CECourseDate = objCECourseResponse.CECourseDate;
                    objCehCourse.CECourseHours = objCECourseResponse.CECourseHours;
                    objCehCourse.CECourseUnits = objCECourseResponse.CECourseUnits;
                    objCehCourse.ProgramSponsor = objCECourseResponse.ProgramSponsor;
                    objCehCourse.CourseNameTitle = objCECourseResponse.CourseNameTitle;
                    objCehCourse.CourseSponsor = objCECourseResponse.CourseSponsor;
                    objCehCourse.CECourseReportingYear = objCECourseResponse.CECourseReportingYear;
                    objCehCourse.CECourseStatusId = objCECourseResponse.CECourseStatusId;
                    objCehCourse.InstructorBiography = objCECourseResponse.InstructorBiography;
                    objCehCourse.ActivityDesc = objCECourseResponse.ActivityDesc;
                    objCehCourse.ReferenceNumber = objCECourseResponse.ReferenceNumber;
                    objCehCourse.IsActive = objCECourseResponse.IsActive;
                    objCehCourse.IsDeleted = objCECourseResponse.IsDeleted;
                    objCehCourse.IndividualLicenseId = objCECourseResponse.IndividualLicenseId;

                    objCehCourse.CreatedBy = objToken.UserId;
                    objCehCourse.CreatedOn = DateTime.Now;
                    objCehCourse.ModifiedBy = null;
                    objCehCourse.ModifiedOn = null;
                    objCehCourse.IndividualCECourseGuid = Guid.NewGuid().ToString();


                    objCehCourse.IndividualCECourseId = objIndividualCECourseBAL.Save_IndividualCECourse(objCehCourse);

                    objCECourseResponse.IndividualCECourseId = objCehCourse.IndividualCECourseId;

                    //SAVE LOG

                    string logText = "Individual Education saved successfully. Saved on " + DateTime.Now.ToShortDateString();
                    string logSource = eCommentLogSource.WSAPI.ToString();
                    LogHelper.SaveIndividualLog(individualId, applicationId, logSource, logText, objToken.UserId, null, null, null);

                    //END SAVE LOG

                    objResponse.Message = MessagesClass.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }
                lstIndividualCECourse.Add(objCehCourse);

                #region Response

                if (lstIndividualCECourse != null && lstIndividualCECourse.Count > 0)
                {
                    lstCeCourseResponse = lstIndividualCECourse.Select(obj => new IndividualCECourseResponse
                    {
                        IndividualCECourseId = obj.IndividualCECourseId,
                        IndividualId = obj.IndividualId,
                        ApplicationId = obj.ApplicationId,
                        CECourseTypeId = obj.CECourseTypeId,
                        CECourseActivityTypeId = obj.CECourseActivityTypeId,
                        CECourseStartDate = obj.CECourseStartDate,
                        CECourseEndDate = obj.CECourseEndDate,
                        CECourseDueDate = obj.CECourseDueDate,
                        CECourseDate = obj.CECourseDate,
                        CECourseHours = obj.CECourseHours,
                        CECourseUnits = obj.CECourseUnits,
                        ProgramSponsor = obj.ProgramSponsor,
                        CourseNameTitle = obj.CourseNameTitle,
                        CourseSponsor = obj.CourseSponsor,
                        CECourseReportingYear = obj.CECourseReportingYear,
                        CECourseStatusId = obj.CECourseStatusId,
                        InstructorBiography = obj.InstructorBiography,
                        ActivityDesc = obj.ActivityDesc,
                        ReferenceNumber = obj.ReferenceNumber,
                        IndividualLicenseId = obj.IndividualLicenseId,
                        IsActive = obj.IsActive,
                    }).ToList();
                }

                #endregion

                objResponse.IndividualCECourseResponseList = lstCeCourseResponse;
            }
            catch (Exception ex)
            {

                LogingHelper.SaveExceptionInfo("", ex, "SaveIndividualEducation", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualCECourseResponseList = null;
            }
            return objResponse;
        }
    }
}
