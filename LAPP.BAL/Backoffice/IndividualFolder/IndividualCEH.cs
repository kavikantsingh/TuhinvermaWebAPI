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
    public class IndividualCEH
    {
        public static IndividualCEHResponseRequest SaveIndividualCEH(Token objToken, IndividualCEHResponse objCECourseResponse)
        {
            IndividualCEHResponseRequest objResponse = new IndividualCEHResponseRequest();
            IndividualCEHoursBAL objIndividualCEHoursBAL = new IndividualCEHoursBAL();
            IndividualCEHResponse objIndividualCEHoursResponse = new IndividualCEHResponse();
            IndividualCEHours objCehCourse = new IndividualCEHours();
            List<IndividualCEHours> lstIndividualCEHours = new List<IndividualCEHours>();
            List<IndividualCEHResponse> lstCeCourseResponse = new List<IndividualCEHResponse>();
            try
            {
                int individualId = objCECourseResponse.IndividualId;
                int? applicationId = objCECourseResponse.ApplicationId;

                objCehCourse = objIndividualCEHoursBAL.Get_IndividualCEHours_By_IndividualCEHoursId(objCECourseResponse.IndividualCEHoursId);
                if (objCehCourse != null)
                {
                    objCehCourse.IndividualId = individualId;
                    objCehCourse.ApplicationId = objCECourseResponse.ApplicationId;
                    objCehCourse.CEHoursTypeId = objCECourseResponse.CEHoursTypeId;
                    objCehCourse.CEHoursStartDate = objCECourseResponse.CEHoursStartDate;
                    objCehCourse.CEHoursEndDate = objCECourseResponse.CEHoursEndDate;
                    objCehCourse.CEHoursDueDate = objCECourseResponse.CEHoursDueDate;
                    objCehCourse.CEHoursReportingYear = objCECourseResponse.CEHoursReportingYear;
                    objCehCourse.CEHoursStatusId = objCECourseResponse.CEHoursStatusId;
                    objCehCourse.CECarryInHours = objCECourseResponse.CECarryInHours;
                    objCehCourse.CERequiredHours = objCECourseResponse.CERequiredHours;
                    objCehCourse.CECurrentReportedHours = objCECourseResponse.CECurrentReportedHours;
                    objCehCourse.CERolloverHours = objCECourseResponse.CERolloverHours;
                    objCehCourse.ReferenceNumber = objCECourseResponse.ReferenceNumber;
                    objCehCourse.ReferenceNumber = objCECourseResponse.ReferenceNumber;
                    objCehCourse.IsActive = objCECourseResponse.IsActive;
                    objCehCourse.IsDeleted = objCECourseResponse.IsDeleted;
                    objCehCourse.IndividualLicenseId = objCECourseResponse.IndividualLicenseId;

                    objCehCourse.ModifiedBy = objToken.UserId;
                    objCehCourse.ModifiedOn = DateTime.Now;

                    objIndividualCEHoursBAL.Save_IndividualCEHours(objCehCourse);

                    //SAVE LOG

                    string logText = "Individual CEH updated successfully. Updated on " + DateTime.Now.ToShortDateString();
                    string logSource = eCommentLogSource.WSAPI.ToString();
                    LogHelper.SaveIndividualLog(individualId, applicationId, logSource, logText, objToken.UserId, null, null, null);

                    //END SAVE LOG

                    objResponse.Message = MessagesClass.UpdateSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }

                else
                {
                    objCehCourse = new IndividualCEHours();

                    objCehCourse.IndividualId = individualId;
                    objCehCourse.ApplicationId = objCECourseResponse.ApplicationId;
                    objCehCourse.CEHoursTypeId = objCECourseResponse.CEHoursTypeId;
                    objCehCourse.CEHoursStartDate = objCECourseResponse.CEHoursStartDate;
                    objCehCourse.CEHoursEndDate = objCECourseResponse.CEHoursEndDate;
                    objCehCourse.CEHoursDueDate = objCECourseResponse.CEHoursDueDate;
                    objCehCourse.CEHoursReportingYear = objCECourseResponse.CEHoursReportingYear;
                    objCehCourse.CEHoursStatusId = objCECourseResponse.CEHoursStatusId;
                    objCehCourse.CECarryInHours = objCECourseResponse.CECarryInHours;
                    objCehCourse.CERequiredHours = objCECourseResponse.CERequiredHours;
                    objCehCourse.CECurrentReportedHours = objCECourseResponse.CECurrentReportedHours;
                    objCehCourse.CERolloverHours = objCECourseResponse.CERolloverHours;
                    objCehCourse.ReferenceNumber = objCECourseResponse.ReferenceNumber;
                    objCehCourse.ReferenceNumber = objCECourseResponse.ReferenceNumber;
                    objCehCourse.IsActive = objCECourseResponse.IsActive;
                    objCehCourse.IsDeleted = objCECourseResponse.IsDeleted;
                    objCehCourse.IndividualLicenseId = objCECourseResponse.IndividualLicenseId;

                    objCehCourse.CreatedBy = objToken.UserId;
                    objCehCourse.CreatedOn = DateTime.Now;
                    objCehCourse.ModifiedBy = null;
                    objCehCourse.ModifiedOn = null;
                    objCehCourse.IndividualCEHoursGuid = Guid.NewGuid();


                    objCehCourse.IndividualCEHoursId = objIndividualCEHoursBAL.Save_IndividualCEHours(objCehCourse);

                    objCECourseResponse.IndividualCEHoursId = objCehCourse.IndividualCEHoursId;

                    //SAVE LOG

                    string logText = "Individual CEH saved successfully. Saved on " + DateTime.Now.ToShortDateString();
                    string logSource = eCommentLogSource.WSAPI.ToString();
                    LogHelper.SaveIndividualLog(individualId, applicationId, logSource, logText, objToken.UserId, null, null, null);

                    //END SAVE LOG

                    objResponse.Message = MessagesClass.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }
                lstIndividualCEHours.Add(objCehCourse);

                #region Response

                if (lstIndividualCEHours != null && lstIndividualCEHours.Count > 0)
                {
                    lstCeCourseResponse = lstIndividualCEHours.Select(obj => new IndividualCEHResponse
                    {
                        IndividualCEHoursId = obj.IndividualCEHoursId,
                        IndividualId = obj.IndividualId,
                        ApplicationId = obj.ApplicationId,
                        CEHoursTypeId = obj.CEHoursTypeId,
                        CEHoursStartDate = obj.CEHoursStartDate,
                        CEHoursEndDate = obj.CEHoursEndDate,
                        CEHoursDueDate = obj.CEHoursDueDate,
                        CEHoursReportingYear = obj.CEHoursReportingYear,
                        CEHoursStatusId = obj.CEHoursStatusId,
                        CECarryInHours = obj.CECarryInHours,
                        CERequiredHours = obj.CERequiredHours,
                        CECurrentReportedHours = obj.CECurrentReportedHours,
                        CERolloverHours = obj.CERolloverHours,
                        ReferenceNumber = obj.ReferenceNumber,
                        IsActive = obj.IsActive,
                        IndividualLicenseId = obj.IndividualLicenseId
                    }).ToList();
                }

                #endregion

                objResponse.IndividualCEHResponseList = lstCeCourseResponse;
            }
            catch (Exception ex)
            {

                LogingHelper.SaveExceptionInfo("", ex, "SaveIndividualCEH", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualCEHResponseList = null;
            }
            return objResponse;
        }

    }
}
