using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.GlobalFunctions;

namespace LAPP.BAL.Renewal
{
    public class RenewalProcess
    {

        public static IndividualRenewalResponse SaveAndValidateRequest(Token objToken, IndividualRenewalResponse objRenewalRequest)
        {
            IndividualRenewalResponse objResponse = new IndividualRenewalResponse();
            IndividualRenewal objIndividualRenewal = new IndividualRenewal();
            objIndividualRenewal = objRenewalRequest.IndividualRenewal;
            try
            {
                int ApplicationId = objIndividualRenewal.Application.ApplicationId;
                int IndividualId = objIndividualRenewal.Individual.IndividualId;

                IndividualBAL objIndividualBAL = new IndividualBAL();
                IndividualResponse objIndividualResponse = new IndividualResponse();
                objIndividualResponse = objRenewalRequest.IndividualRenewal.Individual;
                if (objIndividualResponse != null)
                {
                    #region Individual Proccess
                    Individual objIndividual = new Individual();
                    objIndividual = objIndividualBAL.Get_Individual_By_IndividualId(IndividualId);
                    if (objIndividual != null)
                    {
                        objIndividual.FirstName = objIndividualResponse.FirstName;
                        objIndividual.LastName = objIndividualResponse.LastName;
                        objIndividual.MiddleName = objIndividualResponse.MiddleName;
                        objIndividual.DateOfBirth = objIndividualResponse.DateOfBirth;

                        objIndividual.ModifiedBy = objToken.UserId;
                        objIndividual.ModifiedOn = DateTime.Now;

                        objIndividualBAL.Save_Individual(objIndividual);
                    }
                    #endregion

                    //Application
                    #region Application Process
                    ApplicationResponse objApplicationResponse = new ApplicationResponse();
                    ApplicationBAL objApplicationBAL = new ApplicationBAL();

                    objApplicationResponse = objRenewalRequest.IndividualRenewal.Application;
                    if (objApplicationResponse != null)
                    {
                        Application objApplication = objApplicationBAL.Get_Application_By_ApplicationId(objApplicationResponse.ApplicationId);
                        if (objApplication != null)
                        {

                            objApplication.ApplicationStatusDate = DateTime.Now;
                            objApplication.ApplicationStatusId = 1;
                            objApplication.ApplicationStatusReasonId = 1;


                            objApplication.ModifiedBy = objToken.UserId;
                            objApplication.ModifiedOn = DateTime.Now;
                            objApplicationBAL.Save_Application(objApplication);
                        }

                    }
                    #endregion

                    #region Address
                    try
                    {
                        List<IndividualAddressResponse> lstIndividualAddress = new List<IndividualAddressResponse>();
                        IndividualAddressBAL objIndividualAddressBAL = new IndividualAddressBAL();
                        lstIndividualAddress = objIndividualRenewal.IndividualAddress;
                        if (lstIndividualAddress != null && lstIndividualAddress.Count > 0)
                        {
                            AddressBAL objAddressBAL = new AddressBAL();
                            Address objAddress = new Address();
                            foreach (IndividualAddressResponse objAddressResponse in lstIndividualAddress)
                            {



                                if (objAddressResponse.AddressId > 0)
                                {
                                    objAddress = new Address();
                                    objAddress = objAddressBAL.Get_address_By_AddressId(objAddressResponse.AddressId);
                                    if (objAddress != null)
                                    {
                                        objAddress.City = objAddressResponse.City;
                                        objAddress.StreetLine1 = objAddressResponse.StreetLine1;
                                        objAddress.StreetLine2 = objAddressResponse.StreetLine2;
                                        objAddress.StateCode = objAddressResponse.StateCode;
                                        objAddress.Zip = objAddressResponse.Zip;

                                        objAddress.ModifiedBy = objToken.UserId;
                                        objAddress.ModifiedOn = DateTime.Now;


                                        objAddressBAL.Save_address(objAddress);



                                        IndividualAddress objIndAddress = new IndividualAddress();
                                        IndividualAddressBAL objIndAddressBAL = new IndividualAddressBAL();

                                        objIndAddress.IndividualAddressId = objAddressResponse.IndividualAddressId;
                                        objIndAddress.AddressId = objAddress.AddressId;
                                        objIndAddress.AddressTypeId = objAddressResponse.AddressTypeId;
                                        objIndAddress.IsMailingSameasPhysical = objAddressResponse.IsMailingSameasPhysical;
                                        objIndAddress.ModifiedBy = objToken.UserId;
                                        objIndAddress.ModifiedOn = DateTime.Now;
                                        objIndAddress.IndividualId = IndividualId;
                                        objIndAddress.IndividualAddressGuid = Guid.NewGuid().ToString();

                                        objIndAddressBAL.Save_IndividualAddress(objIndAddress);
                                    }

                                }
                                else
                                {
                                    objAddress = new Address();
                                    objAddress.Addressee = "";
                                    objAddress.AddressGuid = Guid.NewGuid().ToString();
                                    objAddress.Authenticator = Guid.NewGuid().ToString();
                                    objAddress.CountryId = 235;
                                  
                                    objAddress.IsActive = true;
                                    objAddress.IsDeleted = false;
                                    objAddress.UseUserAddress = false;
                                    objAddress.UseVerifiedAddress = false;

                                    objAddress.City = objAddressResponse.City;
                                    objAddress.StreetLine1 = objAddressResponse.StreetLine1;
                                    objAddress.StreetLine2 = objAddressResponse.StreetLine2;
                                    objAddress.StateCode = objAddressResponse.StateCode;
                                    objAddress.Zip = objAddressResponse.Zip;

                                    objAddress.CreatedBy = objToken.UserId;
                                    objAddress.CreatedOn = DateTime.Now;


                                    objAddress.AddressId = objAddressBAL.Save_address(objAddress);

                                    IndividualAddress objIndAddress = new IndividualAddress();
                                    IndividualAddressBAL objIndAddressBAL = new IndividualAddressBAL();
                                    objIndAddress.AddressId = objAddress.AddressId;
                                    objIndAddress.AddressTypeId = objAddressResponse.AddressTypeId;
                                    objIndAddress.IsMailingSameasPhysical = objAddressResponse.IsMailingSameasPhysical;
                                    objIndAddress.CreatedBy = objToken.UserId;
                                    objIndAddress.CreatedOn = DateTime.Now;
                                    objIndAddress.IndividualId = IndividualId;
                                    objIndAddress.IndividualAddressGuid = Guid.NewGuid().ToString();

                                    objIndAddressBAL.Save_IndividualAddress(objIndAddress);

                                }



                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    #endregion

                    #region Contact
                    try
                    {
                        List<IndividualContactResponse> lstIndividualContact = new List<IndividualContactResponse>();
                        IndividualContactBAL objIndividualContactBAL = new IndividualContactBAL();
                        lstIndividualContact = objIndividualRenewal.Contact;
                        if (lstIndividualContact != null && lstIndividualContact.Count > 0)
                        {
                            ContactBAL objContactBAL = new ContactBAL();
                            Contact objContact = new Contact();
                            foreach (IndividualContactResponse objContactResponse in lstIndividualContact)
                            {
                                if (objContactResponse.ContactId > 0)
                                {
                                    objContact = new Contact();
                                    objContact = objContactBAL.Get_Contact_By_ContactId(objContactResponse.ContactId);
                                    if (objContact != null)
                                    {
                                        objContact.Code = objContactResponse.Code;
                                        objContact.ContactFirstName = "";
                                        objContact.ContactLastName = "";
                                        objContact.ContactMiddleName = "";
                                        objContact.ContactTypeId = objContactResponse.ContactTypeId;
                                        objContact.ContactInfo = objContactResponse.ContactInfo;



                                        objContact.ModifiedBy = objToken.UserId;
                                        objContact.ModifiedOn = DateTime.Now;


                                        objContactBAL.Save_Contact(objContact);



                                        IndividualContact objIndContact = new IndividualContact();
                                        IndividualContactBAL objIndContactBAL = new IndividualContactBAL();

                                        objIndContact.IndividualContactId = objContactResponse.IndividualContactId;
                                        objIndContact.ContactId = objContact.ContactId;
                                        objIndContact.ContactTypeId = objContactResponse.ContactTypeId;
                                        objIndContact.IsPreferredContact = objContactResponse.IsPreferredContact;
                                        objIndContact.IsMobile = objContactResponse.IsMobile;

                                        objIndContact.ModifiedBy = objToken.UserId;
                                        objIndContact.ModifiedOn = DateTime.Now;
                                        objIndContact.IndividualId = IndividualId;
                                        objIndContact.IndividualContactGuid = Guid.NewGuid().ToString();

                                        objIndContactBAL.Save_IndividualContact(objIndContact);
                                    }

                                }
                                else
                                {
                                    objContact = new Contact();

                                    objContact.ContactGuid = Guid.NewGuid().ToString();
                                    objContact.Authenticator = Guid.NewGuid().ToString();

                                    objContact.IsActive = true;
                                    objContact.IsDeleted = false;
                                    objContact.Code = objContactResponse.Code;
                                    objContact.ContactFirstName = "";
                                    objContact.ContactLastName = "";
                                    objContact.ContactMiddleName = "";
                                    objContact.ContactTypeId = objContactResponse.ContactTypeId;
                                    objContact.ContactInfo = objContactResponse.ContactInfo;


                                    objContact.CreatedBy = objToken.UserId;
                                    objContact.CreatedOn = DateTime.Now;


                                    objContact.ContactId = objContactBAL.Save_Contact(objContact);

                                    IndividualContact objIndContact = new IndividualContact();
                                    IndividualContactBAL objIndContactBAL = new IndividualContactBAL();

                                    objIndContact.IndividualId = IndividualId;
                                    objIndContact.IndividualContactGuid = Guid.NewGuid().ToString();
                                    objIndContact.IndividualContactId = objContactResponse.IndividualContactId;
                                    objIndContact.ContactId = objContact.ContactId;
                                    objIndContact.ContactTypeId = objContactResponse.ContactTypeId;
                                    objIndContact.IsPreferredContact = objContactResponse.IsPreferredContact;
                                    objIndContact.IsMobile = objContactResponse.IsMobile;

                                    objIndContact.ModifiedBy = objToken.UserId;
                                    objIndContact.ModifiedOn = DateTime.Now;
                                    objIndContact.IndividualId = IndividualId;
                                    objIndContactBAL.Save_IndividualContact(objIndContact);

                                }



                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }


                    #endregion


                    #region Individual Certification
                    IndividualCertificationResponse objCertificationResponse = new IndividualCertificationResponse();
                    IndividualCertificationBAL objCertificationBAL = new IndividualCertificationBAL();

                    objCertificationResponse = objIndividualRenewal.IndividualCertification;
                    if (objCertificationResponse != null)
                    {
                        if (objCertificationResponse.IndividualCertificationId > 0)
                        {
                            IndividualCertification objCertificate = new IndividualCertification();
                            objCertificate = objCertificationBAL.Get_IndividualCertification_By_IndividualId(IndividualId);
                            if (objCertificate != null)
                            {
                                objCertificate.IndividualCertificationId = objCertificationResponse.IndividualCertificationId;
                                objCertificate.IndividualId = IndividualId;
                                objCertificate.CertificationTypeId = objCertificationResponse.CertificationTypeId;
                                objCertificate.IsNBCHIS = objCertificationResponse.IsNBCHIS;
                                objCertificate.NBCHISAccount = objCertificationResponse.NBCHISAccount;
                                objCertificationBAL.Save_IndividualCertification(objCertificate);
                            }

                        }
                        else
                        {
                            IndividualCertification objCertificate = new IndividualCertification();
                            objCertificate.IndividualId = IndividualId;
                            objCertificate.CertificationTypeId = objCertificationResponse.CertificationTypeId;
                            objCertificate.IsNBCHIS = objCertificationResponse.IsNBCHIS;
                            objCertificate.NBCHISAccount = objCertificationResponse.NBCHISAccount;

                            objCertificate.IsNBCOTAppliedforRenewal = false;
                            objCertificate.IsNBCOTCertified = false;
                            objCertificate.IsNBCOTExamScheduled = false;
                            objCertificate.IsActive = true;
                            objCertificate.IsClinicalComptence = false;
                            objCertificate.IsDeleted = false;
                            objCertificate.PraxisExam = "";
                            objCertificate.IndividualCertificationGuid = Guid.NewGuid().ToString();
                            objCertificate.CreatedOn = DateTime.Now;
                            objCertificate.CreatedBy = objToken.UserId;
                            objCertificate.ClinicalComptence = "";
                            objCertificate.ASHA = "";
                            objCertificate.ABAMember = "";
                            objCertificate.ABA = "";

                            objCertificationBAL.Save_IndividualCertification(objCertificate);

                        }

                    }
                    #endregion

                    #region Individual CE Course

                    List<IndividualCECourseResponse> lstCECourseResponse = new List<IndividualCECourseResponse>();
                    IndividualCECourseBAL objCECourseBAL = new IndividualCECourseBAL();
                    lstCECourseResponse = objIndividualRenewal.IndividualCECourse;
                    if (lstCECourseResponse != null && lstCECourseResponse.Count > 0)
                    {
                        foreach (IndividualCECourseResponse objCehResponse in lstCECourseResponse)
                        {
                            if (objCehResponse.IndividualCECourseId > 0)
                            {
                                IndividualCECourse objCehCourse = objCECourseBAL.Get_IndividualCECourse_By_IndividualCECourseId(objCehResponse.IndividualCECourseId);
                                if (objCehCourse != null)
                                {
                                    objCehCourse.CECourseDate = objCehResponse.CECourseDate;
                                    objCehCourse.CECourseHours = objCehResponse.CECourseHours;
                                    objCehCourse.CourseNameTitle = objCehResponse.CourseNameTitle;

                                    objCehCourse.ModifiedBy = objToken.UserId;
                                    objCehCourse.ModifiedOn = DateTime.Now;

                                    objCECourseBAL.Save_IndividualCECourse(objCehCourse);

                                }
                            }
                            else
                            {
                                IndividualCECourse objCehCourse = new IndividualCECourse();
                                objCehCourse.CECourseDate = objCehResponse.CECourseDate;
                                objCehCourse.CECourseHours = objCehResponse.CECourseHours;
                                objCehCourse.CourseNameTitle = objCehResponse.CourseNameTitle;

                                objCehCourse.ModifiedBy = objToken.UserId;
                                objCehCourse.ModifiedOn = DateTime.Now;
                                objCehCourse.CECourseActivityTypeId = objCehResponse.CECourseActivityTypeId;
                                objCehCourse.IndividualCECourseGuid = Guid.NewGuid().ToString();
                                objCehCourse.IsActive = true;
                                objCehCourse.IsDeleted = false;
                                objCehCourse.CreatedBy = objToken.UserId;
                                objCehCourse.CreatedOn = DateTime.Now;

                                objCehCourse.IndividualId = IndividualId;
                                objCehCourse.ApplicationId = ApplicationId;
                                objCehCourse.CECourseTypeId = objCehResponse.CECourseTypeId;

                                objCECourseBAL.Save_IndividualCECourse(objCehCourse);

                            }


                        }

                    }


                    #endregion


                    #region Employment Information
                    try
                    {
                        List<IndividualEmploymentResponse> lstIndividualEmploymentResponse = new List<IndividualEmploymentResponse>();
                        IndividualEmploymentBAL objEmploymentBAL = new IndividualEmploymentBAL();

                        lstIndividualEmploymentResponse = objIndividualRenewal.IndividualEmployment;
                        if (lstIndividualEmploymentResponse != null)
                        {
                            IndividualEmploymentContactBAL objEmpContactBAL = new IndividualEmploymentContactBAL();
                            IndividualEmploymentAddressBAL objEmpAdressBAL = new IndividualEmploymentAddressBAL();

                            foreach (IndividualEmploymentResponse objEmpResponse in lstIndividualEmploymentResponse)
                            {
                                List<IndividualEmploymentContactResponse> lstEmpContactResponse = objEmpResponse.EmploymentContact;
                                List<IndividualEmploymentAddressResponse> lstEmpAddress = objEmpResponse.EmploymentAddress;

                                if (lstEmpAddress != null && lstEmpAddress.Count > 0)
                                {
                                    AddressBAL objAddressBAL = new AddressBAL();
                                    Address objAddress = new Address();
                                    foreach (IndividualEmploymentAddressResponse objAddressResponse in lstEmpAddress)
                                    {
                                        if (objAddressResponse.AddressId > 0)
                                        {
                                            objAddress = new Address();
                                            objAddress = objAddressBAL.Get_address_By_AddressId(objAddressResponse.AddressId);
                                            if (objAddress != null)
                                            {
                                                objAddress.City = objAddressResponse.City;
                                                objAddress.StreetLine1 = objAddressResponse.StreetLine1;
                                                objAddress.StreetLine2 = objAddressResponse.StreetLine2;
                                                objAddress.StateCode = objAddressResponse.StateCode;
                                                objAddress.Zip = objAddressResponse.Zip;

                                                objAddress.ModifiedBy = objToken.UserId;
                                                objAddress.ModifiedOn = DateTime.Now;


                                                objAddressBAL.Save_address(objAddress);



                                                IndividualEmploymentAddress objIndAddress = new IndividualEmploymentAddress();
                                                IndividualEmploymentAddressBAL objIndAddressBAL = new IndividualEmploymentAddressBAL();

                                                objIndAddress.IndividualEmploymentAddressId = objAddressResponse.IndividualEmploymentAddressId;
                                                objIndAddress.AddressId = objAddress.AddressId;
                                                objIndAddress.AddressTypeId = objAddressResponse.AddressTypeId;
                                                objIndAddress.IsMailingSameasPhysical = objAddressResponse.IsMailingSameasPhysical;
                                                objIndAddress.ModifiedBy = objToken.UserId;
                                                objIndAddress.ModifiedOn = DateTime.Now;
                                                objIndAddress.IndividualId = IndividualId;
                                                objIndAddress.IndividualEmploymentAddressGuid = Guid.NewGuid().ToString();

                                                objIndAddressBAL.Save_IndividualEmploymentAddress(objIndAddress);
                                            }

                                        }
                                        else
                                        {
                                            objAddress = new Address();
                                            objAddress.Addressee = "";
                                            objAddress.AddressGuid = Guid.NewGuid().ToString();
                                            objAddress.Authenticator = Guid.NewGuid().ToString();
                                            objAddress.CountryId = 235;
                                             
                                            objAddress.IsActive = true;
                                            objAddress.IsDeleted = false;
                                            objAddress.UseUserAddress = false;
                                            objAddress.UseVerifiedAddress = false;

                                            objAddress.City = objAddressResponse.City;
                                            objAddress.StreetLine1 = objAddressResponse.StreetLine1;
                                            objAddress.StreetLine2 = objAddressResponse.StreetLine2;
                                            objAddress.StateCode = objAddressResponse.StateCode;
                                            objAddress.Zip = objAddressResponse.Zip;

                                            objAddress.CreatedBy = objToken.UserId;
                                            objAddress.CreatedOn = DateTime.Now;


                                            objAddress.AddressId = objAddressBAL.Save_address(objAddress);


                                            IndividualEmploymentAddress objIndAddress = new IndividualEmploymentAddress();
                                            IndividualEmploymentAddressBAL objIndAddressBAL = new IndividualEmploymentAddressBAL();

                                            objIndAddress.AddressId = objAddress.AddressId;
                                            objIndAddress.AddressTypeId = objAddressResponse.AddressTypeId;
                                            objIndAddress.IsMailingSameasPhysical = objAddressResponse.IsMailingSameasPhysical;
                                            objIndAddress.CreatedBy = objToken.UserId;
                                            objIndAddress.CreatedOn = DateTime.Now;
                                            objIndAddress.IndividualId = IndividualId;
                                            objIndAddress.IndividualEmploymentAddressGuid = Guid.NewGuid().ToString();

                                            objIndAddressBAL.Save_IndividualEmploymentAddress(objIndAddress);

                                        }



                                    }

                                }

                                if (lstEmpContactResponse != null && lstEmpContactResponse.Count > 0)
                                {
                                    ContactBAL objContactBAL = new ContactBAL();
                                    Contact objContact = new Contact();
                                    foreach (IndividualEmploymentContactResponse objContactResponse in lstEmpContactResponse)
                                    {
                                        if (objContactResponse.ContactId > 0)
                                        {
                                            objContact = new Contact();
                                            objContact = objContactBAL.Get_Contact_By_ContactId(objContactResponse.ContactId);
                                            if (objContact != null)
                                            {
                                                objContact.Code = objContactResponse.Code;
                                                objContact.ContactFirstName = "";
                                                objContact.ContactLastName = "";
                                                objContact.ContactMiddleName = "";
                                                objContact.ContactTypeId = objContactResponse.ContactTypeId;
                                                objContact.ContactInfo = objContactResponse.ContactInfo;



                                                objContact.ModifiedBy = objToken.UserId;
                                                objContact.ModifiedOn = DateTime.Now;


                                                objContactBAL.Save_Contact(objContact);



                                                IndividualEmploymentContact objIndContact = new IndividualEmploymentContact();
                                                IndividualEmploymentContactBAL objIndContactBAL = new IndividualEmploymentContactBAL();


                                                objIndContact.IndividualEmploymentContactId = objContactResponse.IndividualEmploymentContactId;
                                                objIndContact.ContactId = objContact.ContactId;
                                                objIndContact.ContactTypeId = objContactResponse.ContactTypeId;
                                                objIndContact.IsPreferredContact = objContactResponse.IsPreferredContact;
                                                objIndContact.IsMobile = objContactResponse.IsMobile;

                                                objIndContact.ModifiedBy = objToken.UserId;
                                                objIndContact.ModifiedOn = DateTime.Now;
                                                objIndContact.IndividualId = IndividualId;
                                                objIndContact.IndividualEmploymentContactGuid = Guid.NewGuid().ToString();

                                                objIndContactBAL.Save_IndividualEmploymentContact(objIndContact);
                                            }

                                        }
                                        else
                                        {
                                            objContact = new Contact();

                                            objContact.ContactGuid = Guid.NewGuid().ToString();
                                            objContact.Authenticator = Guid.NewGuid().ToString();

                                            objContact.IsActive = true;
                                            objContact.IsDeleted = false;
                                            objContact.Code = objContactResponse.Code;
                                            objContact.ContactFirstName = "";
                                            objContact.ContactLastName = "";
                                            objContact.ContactMiddleName = "";
                                            objContact.ContactTypeId = objContactResponse.ContactTypeId;
                                            objContact.ContactInfo = objContactResponse.ContactInfo;


                                            objContact.CreatedBy = objToken.UserId;
                                            objContact.CreatedOn = DateTime.Now;


                                            objContact.ContactId = objContactBAL.Save_Contact(objContact);

                                            IndividualEmploymentContact objIndContact = new IndividualEmploymentContact();
                                            IndividualEmploymentContactBAL objIndContactBAL = new IndividualEmploymentContactBAL();

                                            objIndContact.IndividualId = IndividualId;
                                            objIndContact.IndividualEmploymentContactGuid = Guid.NewGuid().ToString();
                                            objIndContact.IndividualEmploymentContactId = objContactResponse.IndividualEmploymentContactId;
                                            objIndContact.ContactId = objContact.ContactId;
                                            objIndContact.ContactTypeId = objContactResponse.ContactTypeId;
                                            objIndContact.IsPreferredContact = objContactResponse.IsPreferredContact;
                                            objIndContact.IsMobile = objContactResponse.IsMobile;
                                         
                                            objIndContact.ModifiedBy = objToken.UserId;
                                            objIndContact.ModifiedOn = DateTime.Now;
                                            objIndContact.IndividualId = IndividualId;
                                            objIndContactBAL.Save_IndividualEmploymentContact(objIndContact);

                                        }



                                    }

                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                    #endregion

                    return SelectOrCreateResponse(objToken, IndividualId);

                }
            }
            catch (Exception ex)
            {

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualRenewal = null;


            }
            return objResponse;
        }

        public static IndividualRenewalResponse SelectOrCreateResponse(Token objToken, int IndividualId, int ApplicationTypeId = 1)
        {

            IndividualRenewalResponse objResponse = new IndividualRenewalResponse();
            try
            {


                IndividualBAL objIndividualBAL = new IndividualBAL();
                IndividualResponse objIndividualResponse = new IndividualResponse();
                objIndividualResponse = objIndividualBAL.Get_Individual_By_IndividualId(IndividualId);
                if (objIndividualResponse != null)
                {
                    IndividualRenewal objIndividualRenewal = new IndividualRenewal();
                    objResponse.IndividualRenewal = objIndividualRenewal;

                    #region Individual Proccess
                    objIndividualRenewal.Individual = objIndividualResponse;
                    #endregion

                    #region Individual License

                    try
                    {
                        List<IndividualLicense> lstIndividualLicense = new List<IndividualLicense>();
                        IndividualLicenseBAL objIndividualLicenseBAL = new IndividualLicenseBAL();

                        lstIndividualLicense = objIndividualLicenseBAL.Get_IndividualLicense_By_IndividualId(IndividualId);
                        if (lstIndividualLicense != null && lstIndividualLicense.Count > 0)
                        {
                            List<IndividualLicenseResponse> lstLicenseResponse = lstIndividualLicense.Select(obj => new IndividualLicenseResponse
                            {
                                ApplicationId = obj.ApplicationId,
                                ApplicationTypeId = obj.ApplicationTypeId,
                                IndividualId = obj.IndividualId,
                                IndividualLicenseId = obj.IndividualLicenseId,
                                IsActive = obj.IsActive,
                                IsLicenseActive = obj.IsLicenseActive,
                                IsLicenseTemporary = obj.IsLicenseTemporary,
                                LicenseEffectiveDate = obj.LicenseEffectiveDate,
                                LicenseExpirationDate = obj.LicenseExpirationDate,
                                LicenseNumber = obj.LicenseNumber,
                                LicenseStatusTypeId = obj.LicenseStatusTypeId,
                                LicenseTypeId = obj.LicenseTypeId,
                                OriginalLicenseDate = obj.OriginalLicenseDate

                            }).ToList();
                            objIndividualRenewal.IndividualLicense = lstLicenseResponse;
                        }
                        else
                        {
                            objIndividualRenewal.IndividualLicense = new List<IndividualLicenseResponse>();

                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;

                    }
                    #endregion

                    #region Check Individual Application Table for pending Application
                    int ApplicationId = 0;
                    IndividualApplication objIndividualApp = new IndividualApplication();
                    IndividualApplicationBAL objIndividualAppBAL = new IndividualApplicationBAL();
                    objIndividualApp = objIndividualAppBAL.Get_IndividualApplication_byIndividualId(IndividualId);
                    if (objIndividualApp != null)
                    {
                        ApplicationId = objIndividualApp.ApplicationId;
                    }

                    #endregion

                    //Application
                    #region Application Process
                    try
                    {
                        ApplicationResponse objApplicationResponse = new ApplicationResponse();
                        ApplicationBAL objApplicationBAL = new ApplicationBAL();

                        Application objApplication = objApplicationBAL.Get_Application_By_ApplicationId(ApplicationId);
                        if (objApplication != null)
                        {

                            //objApplication.ApplicationStatusDate = DateTime.Now;
                            //objApplication.ApplicationStatusId = 1;
                            //objApplication.ApplicationStatusReasonId = 1;


                            objApplication.ModifiedBy = objToken.UserId;
                            objApplication.ModifiedOn = DateTime.Now;
                            objApplicationBAL.Save_Application(objApplication);
                        }
                        else
                        {
                            objApplication = new Application();

                            objApplication.ApplicationGuid = Guid.NewGuid().ToString();
                            objApplication.ApplicationNumber = SerialsBAL.Get_Application_Number();
                            objApplication.ApplicationStatusId = 1;
                            objApplication.ApplicationTypeId = ApplicationTypeId;
                            objApplication.StartedDate = DateTime.Now;
                            objApplication.ApplicationStatusDate = DateTime.Now;
                            objApplication.IsFingerprintingNotRequired = true;
                            objApplication.IsPaymentRequired = false;
                            objApplication.CanProvisionallyHire = false;
                            objApplication.GoPaperless = false;
                            objApplication.IsActive = true;
                            objApplication.IsDeleted = false;
                            objApplication.CreatedBy = objToken.UserId;
                            objApplication.CreatedOn = DateTime.Now;
                            objApplication.LicenseTypeId = 1;
                            objApplication.IsArchive = false;

                            objApplication.ModifiedBy = objToken.UserId;
                            objApplication.ModifiedOn = DateTime.Now;



                            //objApplication.PaymentDeadlineDate = DateTime.Now;
                            //objApplication.PaymentDate = DateTime.Now;
                            //objApplication.SubmittedDate = DateTime.Now;
                            objApplication.ConfirmationNumber = "";
                            objApplication.ReferenceNumber = "";
                            objApplication.ApplicationSubmitMode = "";



                            objApplication.ApplicationId = objApplicationBAL.Save_Application(objApplication);

                        }
                        ApplicationId = objApplication.ApplicationId;
                        objIndividualRenewal.Application = objApplication;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    #endregion

                    #region Link Individual and Application Table
                    if (objIndividualApp == null && ApplicationId > 0)
                    {
                        objIndividualApp = new IndividualApplication();
                        objIndividualApp.ApplicationId = ApplicationId;
                        objIndividualApp.CreatedBy = objToken.UserId;
                        objIndividualApp.CreatedOn = DateTime.Now;
                        objIndividualApp.IndividualApplicationGuid = Guid.NewGuid().ToString();
                        objIndividualApp.IndividualId = IndividualId;
                        objIndividualAppBAL.Save_IndividualApplication(objIndividualApp);

                    }
                    #endregion

                    #region Address
                    try
                    {
                        List<IndividualAddress> lstIndividualAddress = new List<IndividualAddress>();
                        IndividualAddressBAL objIndividualAddressBAL = new IndividualAddressBAL();
                        lstIndividualAddress = objIndividualAddressBAL.Get_IndividualAddress_By_IndividualId(IndividualId);
                        if (lstIndividualAddress != null)
                        {
                            List<IndividualAddressResponse> lstAddressResponse = lstIndividualAddress
                                .Select(obj => new IndividualAddressResponse
                                {
                                    Addressee = obj.Addressee,
                                    AddressTypeId = obj.AddressTypeId,
                                    AddressId = obj.AddressId,
                                    BeginDate = obj.BeginDate,
                                    City = obj.City,
                                    CountryId = obj.CountryId,
                                    CountyId = obj.CountyId,
                                    EndDate = obj.EndDate,
                                    IndividualAddressId = obj.IndividualAddressId,
                                    IndividualId = obj.IndividualId,
                                    IsActive = obj.IsActive,
                                    IsMailingSameasPhysical = obj.IsMailingSameasPhysical,
                                    StateCode = obj.StateCode,
                                    StreetLine1 = obj.StreetLine1,
                                    StreetLine2 = obj.StreetLine2,
                                    Zip = obj.Zip


                                }).ToList();

                            objIndividualRenewal.IndividualAddress = lstAddressResponse;
                        }
                        else
                        {
                            objIndividualRenewal.IndividualAddress = new List<IndividualAddressResponse>();

                        }

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    #endregion

                    #region Contact
                    try
                    {
                        List<IndividualContact> lstIndividualContact = new List<IndividualContact>();
                        IndividualContactBAL objIndividualContactBAL = new IndividualContactBAL();
                        lstIndividualContact = objIndividualContactBAL.Get_IndividualContact_By_IndividualId(IndividualId);
                        if (lstIndividualContact != null)
                        {
                            List<IndividualContactResponse> lstContactResponse = lstIndividualContact
                                .Select(obj => new IndividualContactResponse
                                {
                                    BeginDate = obj.BeginDate,
                                    EndDate = obj.EndDate,
                                    IndividualId = obj.IndividualId,
                                    IsActive = obj.IsActive,
                                    Code = obj.Code,
                                    ContactFirstName = obj.ContactFirstName,
                                    ContactId = obj.ContactId,
                                    ContactInfo = obj.ContactInfo,
                                    ContactLastName = obj.ContactLastName,
                                    ContactMiddleName = obj.ContactMiddleName,
                                    ContactTypeId = obj.ContactTypeId,
                                    DateContactValidated = obj.DateContactValidated,
                                    IndividualContactId = obj.IndividualContactId,
                                    IsMobile = obj.IsMobile,
                                    IsPreferredContact = obj.IsPreferredContact


                                }).ToList();

                            objIndividualRenewal.Contact = lstContactResponse;
                        }
                        else
                        {
                            objIndividualRenewal.Contact = new List<IndividualContactResponse>();
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                    #endregion

                    #region Employment Information

                    try
                    {
                        List<IndividualEmployment> lstIndividualEmployment = new List<IndividualEmployment>();
                        IndividualEmploymentBAL objEmploymentBAL = new IndividualEmploymentBAL();

                        lstIndividualEmployment = objEmploymentBAL.Get_IndividualEmployment_by_ApplicationId(ApplicationId);
                        if (lstIndividualEmployment != null)
                        {
                            IndividualEmploymentContactBAL objEmpContactBAL = new IndividualEmploymentContactBAL();
                            IndividualEmploymentAddressBAL objEmpAdressBAL = new IndividualEmploymentAddressBAL();


                            foreach (IndividualEmployment objEmployment in lstIndividualEmployment)
                            {
                                List<IndividualEmploymentContact> lstEmpContact = new List<IndividualEmploymentContact>();
                                List<IndividualEmploymentAddress> lstEmpAddress = new List<IndividualEmploymentAddress>();


                                lstEmpContact = objEmpContactBAL.Get_IndividualEmploymentContact_By_IndividualEmploymentId(objEmployment.IndividualEmploymentId);

                                lstEmpAddress = objEmpAdressBAL.Get_IndividualEmploymentAddress_By_IndividualEmploymentId(objEmployment.IndividualEmploymentId);


                                if (lstEmpAddress != null)
                                {
                                    List<IndividualEmploymentAddressResponse> lstEmpAddressResponse = lstEmpAddress
                                    .Select(obj => new IndividualEmploymentAddressResponse
                                    {
                                        Addressee = obj.Addressee,
                                        AddressTypeId = obj.AddressTypeId,
                                        AddressId = obj.AddressId,
                                        BeginDate = obj.BeginDate,
                                        City = obj.City,
                                        CountryId = obj.CountryId,
                                        CountyId = obj.CountyId,
                                        EndDate = obj.EndDate,
                                        IndividualEmploymentAddressId = obj.IndividualEmploymentAddressId,
                                        IndividualEmploymentId = obj.IndividualEmploymentId,
                                        IndividualId = obj.IndividualId,
                                        IsActive = obj.IsActive,
                                        IsMailingSameasPhysical = obj.IsMailingSameasPhysical,
                                        StateCode = obj.StateCode,
                                        StreetLine1 = obj.StreetLine1,
                                        StreetLine2 = obj.StreetLine2,
                                        Zip = obj.Zip


                                    }).ToList();
                                    objEmployment.EmploymentAddress = lstEmpAddressResponse;
                                }
                                else
                                {
                                    objEmployment.EmploymentAddress = new List<IndividualEmploymentAddressResponse>();

                                }


                                if (lstEmpContact != null)
                                {
                                    List<IndividualEmploymentContactResponse> lstEmpContactResponse = lstEmpContact
                                    .Select(obj => new IndividualEmploymentContactResponse
                                    {
                                        BeginDate = obj.BeginDate,
                                        EndDate = obj.EndDate,
                                        IndividualId = obj.IndividualId,
                                        IsActive = obj.IsActive,
                                        Code = obj.Code,
                                        ContactFirstName = obj.ContactFirstName,
                                        ContactId = obj.ContactId,
                                        ContactInfo = obj.ContactInfo,
                                        ContactLastName = obj.ContactLastName,
                                        ContactMiddleName = obj.ContactMiddleName,
                                        ContactTypeId = obj.ContactTypeId,
                                        DateContactValidated = obj.DateContactValidated,
                                        IsMobile = obj.IsMobile,
                                        IndividualEmploymentContactId = obj.IndividualEmploymentContactId,
                                        IndividualEmploymentId = obj.IndividualEmploymentId,
                                        IsPreferredContact = obj.IsPreferredContact


                                    }).ToList();
                                    objEmployment.EmploymentContact = lstEmpContactResponse;
                                }
                                else
                                {
                                    objEmployment.EmploymentContact = new List<IndividualEmploymentContactResponse>();

                                }

                            }

                            List<IndividualEmploymentResponse> lstEmploymentResponse = lstIndividualEmployment.Select(obj => new IndividualEmploymentResponse
                            {
                                ApplicationId = obj.ApplicationId,
                                EmploymentAddress = obj.EmploymentAddress,
                                EmploymentContact = obj.EmploymentContact,
                                EmploymentEndDate = obj.EmploymentEndDate,
                                EmploymentHistoryTypeId = obj.EmploymentHistoryTypeId,
                                EmploymentStartDate = obj.EmploymentStartDate,
                                EmploymentStatusId = obj.EmploymentStatusId,
                                EmploymentTypeId = obj.EmploymentTypeId,
                                EverWorkedinFieldofApplication = obj.EverWorkedinFieldofApplication,
                                IndividualEmploymentId = obj.IndividualEmploymentId,
                                IndividualId = obj.IndividualId,
                                IsActive = obj.IsActive,
                                IsWorkinginFieldofApplication = obj.IsWorkinginFieldofApplication,
                                PositionId = obj.PositionId,
                                ProviderId = obj.ProviderId,
                                ReferenceNumber = obj.ReferenceNumber,
                                Role = obj.Role

                            }).ToList();

                            objIndividualRenewal.IndividualEmployment = lstEmploymentResponse;
                        }
                        else
                        {
                            objIndividualRenewal.IndividualEmployment = new List<IndividualEmploymentResponse>();

                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    #endregion

                    #region Individual Certification

                    try
                    {
                        IndividualCertification objCertification = new IndividualCertification();
                        IndividualCertificationBAL objCertificationBAL = new IndividualCertificationBAL();

                        objCertification = objCertificationBAL.Get_IndividualCertification_By_IndividualId(IndividualId);
                        if (objCertification != null)
                        {
                            objIndividualRenewal.IndividualCertification = objCertification;

                        }
                        else
                        {
                           // objIndividualRenewal.IndividualCertification = new IndividualCertification();
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                    #endregion

                    #region NV Business License

                    objIndividualRenewal.BusinessLicenseInformation = new List<IndividualNVBusinessLicenseResponse>();

                    #endregion

                    #region Fees Detail

                    try
                    {
                        FeesDetails objFees = new FeesDetails();
                        objFees.Amount = 100;
                        objFees.Description = "Renewal Fees";
                        objFees.FeeId = 1;
                        objFees.FeeType = "Renewal Fee";
                        objFees.Status = "Unpaid";

                        List<FeesDetails> lstFees = new List<FeesDetails>();
                        lstFees.Add(objFees);
                        objIndividualRenewal.FeesDetails = lstFees;

                    }
                    catch (Exception ex)
                    { throw ex; }
                    #endregion

                    #region Individual CE Hours

                    List<IndividualCEHours> lstCEHours = new List<IndividualCEHours>();
                    IndividualCEHoursBAL objCEHoursBAL = new IndividualCEHoursBAL();
                    lstCEHours = objCEHoursBAL.Get_IndividualCEHours_By_IndividualId(IndividualId);
                    if (lstCEHours != null && lstCEHours.Count > 0)
                    {
                        List<IndividualCEHResponse> lstCEHResponse = lstCEHours.Select(obj => new IndividualCEHResponse
                        {
                            ApplicationId = obj.ApplicationId,
                            CECarryInHours = obj.CECarryInHours,
                            CECurrentReportedHours = obj.CECurrentReportedHours,
                            CEHoursDueDate = obj.CEHoursDueDate,
                            CEHoursEndDate = obj.CEHoursEndDate,
                            CEHoursReportingYear = obj.CEHoursReportingYear,
                            CEHoursStartDate = obj.CEHoursStartDate,
                            CEHoursStatusId = obj.CEHoursStatusId,
                            CEHoursTypeId = obj.CEHoursTypeId,
                            CERequiredHours = obj.CERequiredHours,
                            CERolloverHours = obj.CERolloverHours,
                            IndividualCEHoursId = obj.IndividualCEHoursId,
                            IndividualId = obj.IndividualId,
                            IsActive = obj.IsActive,
                            ReferenceNumber = obj.ReferenceNumber

                        }).ToList();
                        objIndividualRenewal.IndividualCEH = lstCEHResponse;

                    }
                    else
                    {
                        objIndividualRenewal.IndividualCEH = new List<IndividualCEHResponse>();
                    }

                    #endregion

                    #region Individual CE Course

                    List<IndividualCECourse> lstCECourse = new List<IndividualCECourse>();
                    IndividualCECourseBAL objCECourseBAL = new IndividualCECourseBAL();
                    lstCECourse = objCECourseBAL.Get_IndividualCECourse_By_ApplicationId(ApplicationId);
                    if (lstCECourse != null && lstCECourse.Count > 0)
                    {

                        List<IndividualCECourseResponse> lstCeCourseResponse = lstCECourse.Select(obj => new IndividualCECourseResponse
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
                        objIndividualRenewal.IndividualCECourse = lstCeCourseResponse;

                    }
                    else
                    {
                        objIndividualRenewal.IndividualCECourse = new List<IndividualCECourseResponse>();

                    }
                    #endregion

                    #region Veteran

                   // objIndividualRenewal.IndividualVeteran = new IndividualVeteranResponse();
                    #endregion

                    #region Affidavit
                    IndividualAffidavit objAffidavit = new IndividualAffidavit();
                    IndividualAffidavitBAL objAffidavitBAL = new IndividualAffidavitBAL();
                    objAffidavit = objAffidavitBAL.Get_IndividualAffidavit_By_IndividualId(IndividualId);
                    if (objAffidavit != null)
                    {
                        objIndividualRenewal.IndividualAffidavit = objAffidavit;

                    }
                    else
                    {

                        objIndividualRenewal.IndividualAffidavit = new IndividualAffidavitResponse();
                    }
                    #endregion


                }
            }
            catch (Exception ex)
            {

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualRenewal = null;


            }
            return objResponse;


        }

    }
}
