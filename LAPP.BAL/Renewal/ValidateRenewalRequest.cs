using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAPP.ENTITY;
using LAPP.GlobalFunctions;
using LAPP.ENTITY.Enumeration;
using LAPP.BAL.ValidateClass;
namespace LAPP.BAL.Renewal
{
    public class ValidateRenewalRequest
    {
        public static IndividualRenewalResponse Validate(IndividualRenewalResponse objRenewalRequest)
        {
            //Individual 
            #region Individual
            IndividualResponse objIndividual = new IndividualResponse();
            objIndividual = objRenewalRequest.IndividualRenewal.Individual;
            List<ResponseReason> lstResponseReason = new List<ResponseReason>();
            if (objIndividual != null)
            {
                lstResponseReason = Validations.IsRequiredPropertyMaxLength(nameof(objIndividual.FirstName), objIndividual.FirstName, lstResponseReason, 128);
                lstResponseReason = Validations.IsRequiredPropertyMaxLength(nameof(objIndividual.LastName), objIndividual.LastName, lstResponseReason, 128);
                // lstResponseReason = Validations.IsValidOnlyMaxLength(nameof(objIndividual.MiddleName), objIndividual.MiddleName, lstResponseReason, 128);
                lstResponseReason = Validations.IsValidDateMMDDYYYYProperty(nameof(objIndividual.DateOfBirth), objIndividual.DateOfBirth.ToString(), lstResponseReason);
                if (!string.IsNullOrEmpty(objIndividual.Email))
                {
                    lstResponseReason = Validations.IsValidEmailProperty(nameof(objIndividual.Email), objIndividual.Email, lstResponseReason, 320);
                }
            }

            #endregion

            #region Address
            //Address 
            List<IndividualAddressResponse> lstAddress = new List<IndividualAddressResponse>();
            lstAddress = objRenewalRequest.IndividualRenewal.IndividualAddress;

            if (lstAddress != null && lstAddress.Count > 0)
            {
                foreach (IndividualAddressResponse objAdd in lstAddress)
                {
                    if (objAdd.AddressTypeId == 1)
                    {
                        lstResponseReason = Validations.IsRequiredPropertyMaxLength(nameof(objAdd.StreetLine1), objAdd.StreetLine1, lstResponseReason, 128);
                        lstResponseReason = Validations.IsRequiredPropertyMaxLength(nameof(objAdd.City), objAdd.City, lstResponseReason, 128);
                        lstResponseReason = Validations.IsValidOnlyMaxLength(nameof(objAdd.StateCode), objAdd.StateCode, lstResponseReason, 2);
                        lstResponseReason = Validations.IsValidUSZIPProperty(nameof(objAdd.Zip), objAdd.Zip, lstResponseReason, 15);
                    }
                    else if (objAdd.AddressTypeId == 2)
                    {
                        //lstResponseReason = Validations.IsRequiredPropertyMaxLength(nameof(objAdd.StreetLine1), objAdd.StreetLine1, lstResponseReason, 128);
                        // lstResponseReason = Validations.IsRequiredPropertyMaxLength(nameof(objAdd.City), objAdd.City, lstResponseReason, 128);
                        // lstResponseReason = Validations.IsValidOnlyMaxLength(nameof(objAdd.StateCode), objAdd.StateCode, lstResponseReason, 2);
                        if (!string.IsNullOrEmpty(objAdd.Zip))
                        {
                            lstResponseReason = Validations.IsValidUSZIPProperty(nameof(objAdd.Zip), objAdd.Zip, lstResponseReason, 15);
                        }

                    }
                }
            }
            #endregion

            #region Contacts

            List<IndividualContactResponse> lstContact = new List<IndividualContactResponse>();
            lstContact = objRenewalRequest.IndividualRenewal.Contact;

            if (lstContact != null && lstContact.Count > 0)
            {
                foreach (IndividualContactResponse objAdd in lstContact)
                {
                    if (!string.IsNullOrEmpty(objAdd.ContactInfo))
                    {
                        lstResponseReason = Validations.IsRequiredPropertyMaxLength(nameof(objAdd.Code), objAdd.Code, lstResponseReason, 1);
                        if (!string.IsNullOrEmpty(objAdd.Code))
                        {
                            if (objAdd.Code != (eContactType.E).ToString())
                            {
                                lstResponseReason = Validations.IsValidUSPhoneNoProperty(nameof(objAdd.ContactInfo), objAdd.ContactInfo, lstResponseReason, 14);
                            }
                            else
                            {
                                lstResponseReason = Validations.IsValidEmailProperty(nameof(objAdd.ContactInfo), objAdd.ContactInfo, lstResponseReason);
                            }
                        }
                    }
                }
            }
            #endregion

            #region Employment Information

            List<IndividualEmploymentResponse> lstIndividualEmploymentResponse = new List<IndividualEmploymentResponse>();
            IndividualEmploymentBAL objEmploymentBAL = new IndividualEmploymentBAL();

            lstIndividualEmploymentResponse = objRenewalRequest.IndividualRenewal.IndividualEmployment;
            if (lstIndividualEmploymentResponse != null)
            {

                foreach (IndividualEmploymentResponse objEmpResponse in lstIndividualEmploymentResponse)
                {
                    lstResponseReason = Validations.IsRequiredPropertyMaxLength(nameof(objEmpResponse.EmployerName), objEmpResponse.EmployerName, lstResponseReason, 128);

                    List<IndividualEmploymentContact> lstEmpContactResponse = objEmpResponse.EmploymentContact;
                    List<IndividualEmploymentAddress> lstEmpAddress = objEmpResponse.EmploymentAddress;
                    if (lstEmpAddress != null && lstEmpAddress.Count > 0)
                    {
                        AddressBAL objAddressBAL = new AddressBAL();
                        Address objAddress = new Address();
                        foreach (IndividualEmploymentAddress objAddressResponse in lstEmpAddress)
                        {

                            lstResponseReason = Validations.IsRequiredPropertyMaxLength(nameof(objAddressResponse.StreetLine1), objAddressResponse.StreetLine1, lstResponseReason, 128);
                            lstResponseReason = Validations.IsRequiredPropertyMaxLength(nameof(objAddressResponse.City), objAddressResponse.City, lstResponseReason, 128);
                            lstResponseReason = Validations.IsValidOnlyMaxLength(nameof(objAddressResponse.StateCode), objAddressResponse.StateCode, lstResponseReason, 2);
                            lstResponseReason = Validations.IsValidUSZIPProperty(nameof(objAddressResponse.Zip), objAddressResponse.Zip, lstResponseReason, 15);

                        }

                        if (lstEmpContactResponse != null && lstEmpContactResponse.Count > 0)
                        {
                            ContactBAL objContactBAL = new ContactBAL();
                            Contact objContact = new Contact();
                            foreach (IndividualEmploymentContactResponse objContactResponse in lstEmpContactResponse)
                            {
                                if (!string.IsNullOrEmpty(objContactResponse.ContactInfo.Trim()))
                                {
                                    lstResponseReason = Validations.IsRequiredPropertyMaxLength(nameof(objContactResponse.Code), objContactResponse.Code, lstResponseReason, 1);
                                    if (!string.IsNullOrEmpty(objContactResponse.Code))
                                    {
                                        if (objContactResponse.Code != (eContactType.E).ToString())
                                        {
                                            lstResponseReason = Validations.IsValidUSPhoneNoProperty(nameof(objContactResponse.ContactInfo), objContactResponse.ContactInfo, lstResponseReason, 14);
                                        }
                                        else
                                        {
                                            lstResponseReason = Validations.IsValidEmailProperty(nameof(objContactResponse.ContactInfo), objContactResponse.ContactInfo, lstResponseReason);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }


            #endregion

            #region Individual Certification
            //IndividualCertificationResponse objCertificationResponse = new IndividualCertificationResponse();
            //IndividualCertificationBAL objCertificationBAL = new IndividualCertificationBAL();

            //objCertificationResponse = objRenewalRequest.IndividualRenewal.IndividualCertification;
            //if (objCertificationResponse != null)
            //{
            //    if (objCertificationResponse.IsNBCHIS)
            //    {
            //        lstResponseReason = Validations.IsRequiredPropertyMaxLength(nameof(objCertificationResponse.NBCHISAccount), objCertificationResponse.NBCHISAccount, lstResponseReason, 128);
            //    }
            //}
            #endregion

            #region Sponsor

            List<SponsorInformationResponse> lstSupervisoryInfoRes = new List<SponsorInformationResponse>();
            IndividualSupervisoryInfoBAL objIndividualSupervisoryInfoBAL = new IndividualSupervisoryInfoBAL();
            lstSupervisoryInfoRes = objRenewalRequest.IndividualRenewal.SponsorInformation;
            if (lstSupervisoryInfoRes != null)
            {
                foreach (SponsorInformationResponse objSupervisoryInfoRes in lstSupervisoryInfoRes)
                {
                    if (!string.IsNullOrEmpty(objSupervisoryInfoRes.FirstName) && !string.IsNullOrEmpty(objSupervisoryInfoRes.LastName))
                    {
                        #region Sponsor Address

                        if (objSupervisoryInfoRes.SponsorAddress != null && objSupervisoryInfoRes.SponsorAddress.Count > 0)
                        {
                            SponsorAddressResponse objAddressResp = objSupervisoryInfoRes.SponsorAddress[0];

                            // lstResponseReason = Validations.IsRequiredPropertyMaxLength(nameof(objAddressResp.StreetLine1), objAddressResp.StreetLine1, lstResponseReason, 128);
                            //  lstResponseReason = Validations.IsRequiredPropertyMaxLength(nameof(objAddressResp.City), objAddressResp.City, lstResponseReason, 128);
                            //  lstResponseReason = Validations.IsValidOnlyMaxLength(nameof(objAddressResp.StateCode), objAddressResp.StateCode, lstResponseReason, 2);
                            //  lstResponseReason = Validations.IsValidUSZIPProperty(nameof(objAddressResp.Zip), objAddressResp.Zip, lstResponseReason, 15);
                        }

                        #endregion

                        #region Sponsor Name
                        lstResponseReason = Validations.IsRequiredPropertyMaxLength(nameof(objSupervisoryInfoRes.FirstName), objSupervisoryInfoRes.FirstName, lstResponseReason, 128);

                        lstResponseReason = Validations.IsRequiredPropertyMaxLength(nameof(objSupervisoryInfoRes.LastName), objSupervisoryInfoRes.LastName, lstResponseReason, 128);

                        #endregion
                    }
                }


            }



            #endregion

            #region Individual BusinessLicense

            List<IndividualNVBusinessLicenseResponse> objIndividualNVBusinessLicenseResponse = new List<IndividualNVBusinessLicenseResponse>();
            IndividualNVBusinessLicenseBAL objIndividualNVBusinessLicenseBAL = new IndividualNVBusinessLicenseBAL();

            objIndividualNVBusinessLicenseResponse = objRenewalRequest.IndividualRenewal.BusinessLicenseInformation;

            if (objIndividualNVBusinessLicenseResponse != null && objIndividualNVBusinessLicenseResponse.Count > 0)
            {

                bool result = false;
                foreach (IndividualNVBusinessLicenseResponse objNewBLResponse in objIndividualNVBusinessLicenseResponse)
                {
                    if (!result)
                    {
                        result = objNewBLResponse.ContentItemResponse;
                    }
                }

                if (!result)
                {

                    ResponseReason objValidationResponse = new ResponseReason();
                    objValidationResponse.Code = "NB";
                    objValidationResponse.PropertyName = "Nevada Business License Information";
                    objValidationResponse.Message = "Please select one option from section \"Nevada Business License Information\".";

                    lstResponseReason.Add(objValidationResponse);

                }
                else
                {
                    foreach (IndividualNVBusinessLicenseResponse objNewBLResponse in objIndividualNVBusinessLicenseResponse)
                    {
                        if (objNewBLResponse.ContentItemHash == 1 && objNewBLResponse.ContentItemResponse)
                        {
                            lstResponseReason = Validations.IsRequiredPropertyMaxLength("Name of business license", objNewBLResponse.NameonBusinessLicense, lstResponseReason, 150);
                            lstResponseReason = Validations.IsRequiredPropertyMaxLength("Business License # ", objNewBLResponse.BusinessLicenseNumber, lstResponseReason, 50);
                        }
                    }

                }
            }


            #endregion

            #region Child Support Declaration

            List<IndividualChildSupportResponse> lstChildSupportResponse = new List<IndividualChildSupportResponse>();
            IndividualChildSupportBAL objChildBAL = new IndividualChildSupportBAL();

            lstChildSupportResponse = objRenewalRequest.IndividualRenewal.IndividualChildSupport;
            if (lstChildSupportResponse != null)
            {
                bool result = false;
                foreach (IndividualChildSupportResponse objResp in lstChildSupportResponse)
                {
                    if (!result)
                    {
                        result = objResp.ContentItemResponse;
                    }
                }

                if (!result)
                {

                    ResponseReason objValidationResponse = new ResponseReason();
                    objValidationResponse.Code = "CS";
                    objValidationResponse.PropertyName = "Child Support Information";
                    objValidationResponse.Message = "Please select one option from section \"Child Support Information\".";

                    lstResponseReason.Add(objValidationResponse);

                }

            }


            #endregion


            #region Individual Vetran


            IndividualVeteranResponse objIndividualVetranResp = new IndividualVeteranResponse();
            IndividualVeteranBAL objIndividualVetranBAL = new IndividualVeteranBAL();
            objIndividualVetranResp = objRenewalRequest.IndividualRenewal.IndividualVeteran;
            if (objIndividualVetranResp != null)
            {


                if (objIndividualVetranResp.ServedInMilitary)
                {
                    List<IndividualVeteranBranchResponse> lstBranchResp = new List<IndividualVeteranBranchResponse>();
                    lstBranchResp = objIndividualVetranResp.VeteranBranches;

                    if (lstBranchResp != null && lstBranchResp.Count > 0)
                    {
                        IndividualVeteranBranchBAL objBranchBAL = new IndividualVeteranBranchBAL();


                        bool result = false;
                        foreach (IndividualVeteranBranchResponse objBranchResp in lstBranchResp)
                        {
                            if (!result)
                            {
                                result = objBranchResp.BranchofServicesIdResponse;
                            }
                        }

                        if (!result)
                        {

                            ResponseReason objValidationResponse = new ResponseReason();
                            objValidationResponse.Code = "VI";
                            objValidationResponse.PropertyName = "Veteran Information";
                            objValidationResponse.Message = "Check minimum one service that apply from list.";
                            lstResponseReason.Add(objValidationResponse);

                        }

                        lstResponseReason = Validations.IsRequiredPropertyMaxLength(nameof(objIndividualVetranResp.MilitaryOccupationSpeciality), objIndividualVetranResp.MilitaryOccupationSpeciality, lstResponseReason, 100);
                        lstResponseReason = Validations.IsValidDateMMDDYYYYProperty("Date(s) of Service :   From", objIndividualVetranResp.ServiceDateFrom != null ? objIndividualVetranResp.ServiceDateFrom.ToString() : "", lstResponseReason);
                        lstResponseReason = Validations.IsValidDateMMDDYYYYProperty("Date(s) of Service :   To", objIndividualVetranResp.ServiceDateTo != null ? objIndividualVetranResp.ServiceDateTo.ToString() : "", lstResponseReason);

                    }

                }


            }





            #endregion



            #region Individual Legal

            List<IndividualLegalResponse> objIndividualLegalResponse = new List<IndividualLegalResponse>();
            IndividualLegalBAL objIndividualLegalBAL = new IndividualLegalBAL();

            objIndividualLegalResponse = objRenewalRequest.IndividualRenewal.IndividualLegal;

            if (objIndividualLegalResponse != null && objIndividualLegalResponse.Count > 0)
            {
                foreach (IndividualLegalResponse objNewBLResponse in objIndividualLegalResponse)
                {
                    if (objNewBLResponse.ContentItemResponse != null && Convert.ToBoolean(objNewBLResponse.ContentItemResponse))
                    {
                        lstResponseReason = Validations.IsRequiredPropertyMaxLength("Legal Information#" + objNewBLResponse.ContentItemNumber + " - explanation", objNewBLResponse.Desc, lstResponseReason, 2000);
                    }
                }
            }

            #endregion

            #region Individual CE Course

            List<IndividualCECourseResponse> lstCECourseResponse = new List<IndividualCECourseResponse>();
            IndividualCECourseBAL objCECourseBAL = new IndividualCECourseBAL();
            lstCECourseResponse = objRenewalRequest.IndividualRenewal.IndividualCECourse;
            if (lstCECourseResponse != null && lstCECourseResponse.Count > 0)
            {
                foreach (IndividualCECourseResponse objCehResponse in lstCECourseResponse)
                {
                    lstResponseReason = Validations.IsRequiredPropertyMaxLength(nameof(objCehResponse.CourseNameTitle), objCehResponse.CourseNameTitle, lstResponseReason, 100);
                    lstResponseReason = Validations.IsValidDateMMDDYYYYProperty(nameof(objCehResponse.CECourseDate), objCehResponse.CECourseDate != null ? objCehResponse.CECourseDate.ToString() : "", lstResponseReason);
                    // lstResponseReason = Validations.IsValidIntDecimalProperty(nameof(objCehResponse.CECourseHours), objCehResponse.CECourseHours.ToString(), lstResponseReason);

                }

            }


            #endregion


            if (lstResponseReason != null && lstResponseReason.Count > 0)
            {
                objRenewalRequest.ResponseReason = GlobalFunctions.GeneralFunctions.GetJsonStringFromList(lstResponseReason);
                objRenewalRequest.Message = "Validation Error";
                objRenewalRequest.Status = false;
                objRenewalRequest.StatusCode = Convert.ToInt32(ResponseStatusCode.Validation).ToString("00");
                objRenewalRequest.IndividualRenewal = objRenewalRequest.IndividualRenewal;
                return objRenewalRequest;
            }
            else
            {
                return null;
            }
        }
    }
}
