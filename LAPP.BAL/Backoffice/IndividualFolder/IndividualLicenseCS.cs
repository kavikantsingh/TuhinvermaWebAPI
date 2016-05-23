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
    public class IndividualLicenseCS
    {
        public static IndividualLicenseResponseRequest SaveIndividualLicense(Token objToken, IndividualLicenseResponse objIndividualLicensePostResponse)
        {
            IndividualLicenseResponseRequest objResponse = new IndividualLicenseResponseRequest();
            IndividualLicenseBAL objIndividualLicenseBAL = new IndividualLicenseBAL();
            IndividualLicenseResponse objIndividualResponse = new IndividualLicenseResponse();
            IndividualLicense objIndividualLicense = new IndividualLicense();
            List<IndividualLicenseResponse> lstIndividualLicense = new List<IndividualLicenseResponse>();
            try
            {
                objIndividualLicense = objIndividualLicenseBAL.Get_IndividualLicense_By_IndividualLicenseId(objIndividualLicensePostResponse.IndividualLicenseId);
                if (objIndividualLicense != null)
                {
                    objIndividualLicense.IndividualId = objIndividualLicensePostResponse.IndividualId;
                    objIndividualLicense.ApplicationId = objIndividualLicensePostResponse.ApplicationId;
                    objIndividualLicense.ApplicationTypeId = objIndividualLicensePostResponse.ApplicationTypeId;
                    objIndividualLicense.LicenseTypeId = objIndividualLicensePostResponse.LicenseTypeId;
                    objIndividualLicense.IsLicenseTemporary = objIndividualLicensePostResponse.IsLicenseTemporary;
                    objIndividualLicense.IsLicenseActive = objIndividualLicensePostResponse.IsLicenseActive;
                    objIndividualLicense.LicenseNumber = objIndividualLicensePostResponse.LicenseNumber;
                    objIndividualLicense.OriginalLicenseDate = objIndividualLicensePostResponse.OriginalLicenseDate;
                    objIndividualLicense.LicenseEffectiveDate = objIndividualLicensePostResponse.LicenseEffectiveDate;
                    objIndividualLicense.LicenseExpirationDate = objIndividualLicensePostResponse.LicenseExpirationDate;
                    objIndividualLicense.LicenseStatusTypeId = objIndividualLicensePostResponse.LicenseStatusTypeId;

                    objIndividualLicense.ModifiedBy = objToken.UserId;
                    objIndividualLicense.ModifiedOn = DateTime.Now;

                    objIndividualLicenseBAL.Save_IndividualLicense(objIndividualLicense);


                    objResponse.Message = MessagesClass.UpdateSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }
                else
                {
                    objIndividualLicense = new IndividualLicense();
                    objIndividualLicense.IndividualId = objIndividualLicensePostResponse.IndividualId;
                    objIndividualLicense.ApplicationId = objIndividualLicensePostResponse.ApplicationId;
                    objIndividualLicense.ApplicationTypeId = objIndividualLicensePostResponse.ApplicationTypeId;
                    objIndividualLicense.LicenseTypeId = objIndividualLicensePostResponse.LicenseTypeId;
                    objIndividualLicense.IsLicenseTemporary = objIndividualLicensePostResponse.IsLicenseTemporary;
                    objIndividualLicense.IsLicenseActive = objIndividualLicensePostResponse.IsLicenseActive;
                    objIndividualLicense.LicenseNumber = objIndividualLicensePostResponse.LicenseNumber;
                    objIndividualLicense.OriginalLicenseDate = objIndividualLicensePostResponse.OriginalLicenseDate;
                    objIndividualLicense.LicenseEffectiveDate = objIndividualLicensePostResponse.LicenseEffectiveDate;
                    objIndividualLicense.LicenseExpirationDate = objIndividualLicensePostResponse.LicenseExpirationDate;
                    objIndividualLicense.LicenseStatusTypeId = objIndividualLicensePostResponse.LicenseStatusTypeId;
                    objIndividualLicense.IsDeleted = false;
                    objIndividualLicense.CreatedBy = objToken.UserId;
                    objIndividualLicense.CreatedOn = DateTime.Now;
                    objIndividualLicense.IndividualLicenseGuid = Guid.NewGuid().ToString();

                    objIndividualLicense.IndividualLicenseId = objIndividualLicenseBAL.Save_IndividualLicense(objIndividualLicense);
                    objIndividualLicensePostResponse.IndividualLicenseId = objIndividualLicense.IndividualLicenseId;
                    objResponse.Message = MessagesClass.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }
                lstIndividualLicense.Add(objIndividualLicensePostResponse);
                objResponse.Status = true;

                objResponse.IndividualLicenseList = lstIndividualLicense;

            }
            catch (Exception ex)
            {

                LogingHelper.SaveExceptionInfo("", ex, "SaveIndividualLicense", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualLicenseList = null;
            }
            return objResponse;


        }


    }
}
