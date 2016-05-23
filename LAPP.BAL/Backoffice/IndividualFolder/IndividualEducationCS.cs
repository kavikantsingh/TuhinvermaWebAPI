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

                objCehCourse = objIndividualCECourseBAL.Get_IndividualCECourse_By_IndividualCECourseId(objCECourseResponse.IndividualCECourseId);
                if (objCehCourse != null)
                {
                    objCehCourse.IndividualId = objCECourseResponse.IndividualId;
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
                   // objCehCourse.IndividualCECourseGuid = Guid.NewGuid().ToString();

                    objCehCourse.ModifiedBy = objToken.UserId;
                    objCehCourse.ModifiedOn = DateTime.Now;

                    objIndividualCECourseBAL.Save_IndividualCECourse(objCehCourse);


                    objResponse.Message = MessagesClass.UpdateSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }

                else
                {
                    objCehCourse = new IndividualCECourse();

                    objCehCourse.IndividualId = objCECourseResponse.IndividualId;
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
                    objCehCourse.IndividualCECourseGuid = Guid.NewGuid().ToString();
                    objCehCourse.IsActive = true;
                    objCehCourse.IsDeleted = false;
                    objCehCourse.CreatedBy = objToken.UserId;
                    objCehCourse.CreatedOn = DateTime.Now;
                    objCehCourse.IndividualCECourseId = objIndividualCECourseBAL.Save_IndividualCECourse(objCehCourse);

                    objCECourseResponse.IndividualCECourseId = objCehCourse.IndividualCECourseId;

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
