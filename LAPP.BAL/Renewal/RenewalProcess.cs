using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.GlobalFunctions;
using LAPP.LOGING;

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
                #region Validation
                IndividualRenewalResponse objValidationResponse = new IndividualRenewalResponse();
                objValidationResponse = ValidateRenewalRequest.Validate(objRenewalRequest);
                if (objValidationResponse != null)
                {
                    return objValidationResponse;
                }

                #endregion
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

                        Users objUseres = new Users();
                        UsersBAL objUsersBAL = new UsersBAL();
                        objUseres = objUsersBAL.Get_Users_byIndividualId(objIndividual.IndividualId);
                        if (objUseres != null)
                        {
                            objUseres.Email = objIndividualResponse.Email;
                            objUseres.ModifiedBy = objToken.UserId;
                            objUseres.ModifiedOn = DateTime.Now;
                            objUsersBAL.Save_Users(objUseres);
                        }

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
                            objApplication.LicenseTypeId = 1;

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
                                        objAddress.Addressee = "";
                                        objAddress.City = objAddressResponse.City;
                                        objAddress.StreetLine1 = objAddressResponse.StreetLine1;
                                        objAddress.StreetLine2 = objAddressResponse.StreetLine2;
                                        objAddress.StateCode = objAddressResponse.StateCode;
                                        objAddress.Zip = objAddressResponse.Zip;

                                        objAddress.ModifiedBy = objToken.UserId;
                                        objAddress.ModifiedOn = DateTime.Now;
                                        objAddress.CountryId = 235;

                                        objAddress.IsActive = true;
                                        objAddress.IsDeleted = false;
                                        objAddress.UseUserAddress = false;
                                        objAddress.UseVerifiedAddress = false;

                                        objAddressBAL.Save_address(objAddress);



                                        IndividualAddress objIndAddress = new IndividualAddress();
                                        IndividualAddressBAL objIndAddressBAL = new IndividualAddressBAL();
                                        objIndAddress = objIndAddressBAL.Get_IndividualAddress_By_IndividualAddressId(objAddressResponse.IndividualAddressId);
                                        if (objIndAddress != null)
                                        {
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
                                    objIndAddress.IsActive = true;
                                    objIndAddress.BeginDate = DateTime.Now;
                                    objIndAddressBAL.Save_IndividualAddress(objIndAddress);

                                }



                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogingHelper.SaveExceptionInfo("", ex, "Renewal Process - Individual Address", ENTITY.Enumeration.eSeverity.Error);
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
                                        objIndContact = objIndContactBAL.Get_IndividualContact_By_IndividualContactId(objContactResponse.IndividualContactId);
                                        if (objIndContact != null)
                                        {
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
                                    objIndContact.IsActive = true;
                                    objIndContact.BeginDate = DateTime.Now;
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
                        LogingHelper.SaveExceptionInfo("", ex, "Renewal Process - Contact", ENTITY.Enumeration.eSeverity.Error);
                        throw ex;
                    }


                    #endregion

                    #region Individual Legal
                    try
                    {
                        List<IndividualLegalResponse> objIndividualLegalResponse = new List<IndividualLegalResponse>();
                        IndividualLegalBAL objIndividualLegalBAL = new IndividualLegalBAL();

                        objIndividualLegalResponse = objIndividualRenewal.IndividualLegal;

                        if (objIndividualLegalResponse != null && objIndividualLegalResponse.Count > 0)
                        {
                            foreach (IndividualLegalResponse objNewBLResponse in objIndividualLegalResponse)
                            {
                                if (objNewBLResponse.IndividualLegalId > 0 && objNewBLResponse != null)
                                {
                                    IndividualLegal objLegal = new IndividualLegal();

                                    objLegal = objIndividualLegalBAL.Get_address_By_IndividualLegalId(objNewBLResponse.IndividualLegalId);

                                    if (objLegal != null)
                                    {
                                        objLegal.IndividualId = objNewBLResponse.IndividualId;
                                        objLegal.ContentItemLkId = objNewBLResponse.ContentItemLkId;
                                        objLegal.ContentItemNumber = objNewBLResponse.ContentItemNumber;
                                        objLegal.ContentItemResponse = objNewBLResponse.ContentItemResponse;
                                        objLegal.Desc = objNewBLResponse.Desc;
                                        objLegal.ModifiedBy = objToken.UserId;
                                        objLegal.ModifiedOn = DateTime.Now;

                                        objIndividualLegalBAL.Save_IndividualLegal(objLegal);

                                    }
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        LogingHelper.SaveExceptionInfo("", ex, "Renewal Process - Individual Legal", ENTITY.Enumeration.eSeverity.Error);
                        throw ex;
                    }
                    #endregion


                    #region Individual BusinessLicense
                    try
                    {
                        List<IndividualNVBusinessLicenseResponse> objIndividualNVBusinessLicenseResponse = new List<IndividualNVBusinessLicenseResponse>();
                        IndividualNVBusinessLicenseBAL objIndividualNVBusinessLicenseBAL = new IndividualNVBusinessLicenseBAL();

                        objIndividualNVBusinessLicenseResponse = objIndividualRenewal.BusinessLicenseInformation;

                        if (objIndividualNVBusinessLicenseResponse != null && objIndividualNVBusinessLicenseResponse.Count > 0)
                        {
                            foreach (IndividualNVBusinessLicenseResponse objNewBLResponse in objIndividualNVBusinessLicenseResponse)
                            {
                                if (objNewBLResponse.IndividualNVBusinessLicenseId > 0 && objNewBLResponse != null)
                                {
                                    IndividualNVBusinessLicense objBusinessLice = new IndividualNVBusinessLicense();

                                    objBusinessLice = objIndividualNVBusinessLicenseBAL.Get_address_By_IndividualNVBusinessLicenseId(objNewBLResponse.IndividualNVBusinessLicenseId);

                                    if (objBusinessLice != null)
                                    {
                                        objBusinessLice.IndividualId = objNewBLResponse.IndividualId;
                                        objBusinessLice.ContentItemLkId = objNewBLResponse.ContentItemLkId;
                                        objBusinessLice.ContentItemHash = objNewBLResponse.ContentItemHash;
                                        objBusinessLice.ContentItemResponse = objNewBLResponse.ContentItemResponse;
                                        objBusinessLice.Status = objNewBLResponse.Status;
                                        objBusinessLice.NameonBusinessLicense = objNewBLResponse.NameonBusinessLicense;
                                        objBusinessLice.BusinessLicenseNumber = objNewBLResponse.BusinessLicenseNumber;
                                        objBusinessLice.IsActive = objNewBLResponse.IsActive;
                                        objBusinessLice.ModifiedBy = objToken.UserId;
                                        objBusinessLice.ModifiedOn = DateTime.Now;

                                        objIndividualNVBusinessLicenseBAL.Save_IndividualNVBusinessLicense(objBusinessLice);

                                    }
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        LogingHelper.SaveExceptionInfo("", ex, "Renewal Process - NV Business License", ENTITY.Enumeration.eSeverity.Error);
                        throw ex;
                    }
                    #endregion



                    #region Individual Certification

                    try
                    {
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
                                objCertificate.PraxisExam = objCertificationResponse.PraxisExam;
                                objCertificate.IndividualCertificationGuid = Guid.NewGuid().ToString();
                                objCertificate.CreatedOn = DateTime.Now;
                                objCertificate.CreatedBy = objToken.UserId;
                                objCertificate.ClinicalComptence = objCertificationResponse.ClinicalComptence;
                                objCertificate.ASHA = objCertificationResponse.ASHA;
                                objCertificate.ABAMember = objCertificationResponse.ABAMember;
                                objCertificate.ABA = objCertificationResponse.ABA;

                                objCertificationBAL.Save_IndividualCertification(objCertificate);

                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        LogingHelper.SaveExceptionInfo("", ex, "Renewal Process - Certification", ENTITY.Enumeration.eSeverity.Error);
                        throw ex;
                    }
                    #endregion

                    #region Individual CE Course
                    try
                    {
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
                                        objCehCourse.IsDeleted = objCehResponse.IsDeleted;
                                        objCehCourse.ModifiedBy = objToken.UserId;
                                        objCehCourse.ModifiedOn = DateTime.Now;
                                        objCehCourse.CECourseReportingYear = objCehResponse.CECourseReportingYear;
                                        objCehCourse.IndividualId = IndividualId;
                                        objCehCourse.ApplicationId = ApplicationId;
                                        objCehCourse.CECourseTypeId = objCehResponse.CECourseTypeId;
                                        objCehCourse.CECourseStatusId = objCehResponse.CECourseStatusId;
                                        objCehCourse.CECourseTypeId = objCehResponse.CECourseTypeId;
                                        objCehCourse.CECourseActivityTypeId = objCehResponse.CECourseActivityTypeId;
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

                                    objCehCourse.IndividualCECourseGuid = Guid.NewGuid().ToString();
                                    objCehCourse.IsActive = true;
                                    objCehCourse.IsDeleted = objCehResponse.IsDeleted;
                                    objCehCourse.CreatedBy = objToken.UserId;
                                    objCehCourse.CreatedOn = DateTime.Now;
                                    objCehCourse.CECourseReportingYear = objCehResponse.CECourseReportingYear;
                                    objCehCourse.IndividualId = IndividualId;
                                    objCehCourse.ApplicationId = ApplicationId;
                                    objCehCourse.CECourseTypeId = objCehResponse.CECourseTypeId;
                                    objCehCourse.CECourseStatusId = objCehResponse.CECourseStatusId;
                                    objCehCourse.CECourseTypeId = objCehResponse.CECourseTypeId;
                                    objCehCourse.CECourseActivityTypeId = objCehResponse.CECourseActivityTypeId;
                                    objCECourseBAL.Save_IndividualCECourse(objCehCourse);

                                }


                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        LogingHelper.SaveExceptionInfo("", ex, "Renewal Process - CE Course", ENTITY.Enumeration.eSeverity.Error);
                        throw ex;
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
                            ProviderBAL objProviderBAL = new ProviderBAL();
                            Provider objProvider = new Provider();

                            foreach (IndividualEmploymentResponse objEmpResponse in lstIndividualEmploymentResponse)
                            {
                                List<IndividualEmploymentContactResponse> lstEmpContactResponse = objEmpResponse.EmploymentContact;
                                List<IndividualEmploymentAddressResponse> lstEmpAddress = objEmpResponse.EmploymentAddress;

                                IndividualEmployment objIndEmployment = new IndividualEmployment();
                                IndividualEmploymentBAL objIndEmploymentBAL = new IndividualEmploymentBAL();

                                if (!string.IsNullOrEmpty(objEmpResponse.EmployerName))
                                {
                                    objProvider = objProviderBAL.Get_Provider_By_ProviderId(objEmpResponse.ProviderId);
                                    if (objProvider != null)
                                    {
                                        // Update Provider

                                        objProvider.ModifiedBy = objToken.UserId;
                                        objProvider.ModifiedOn = DateTime.Now;
                                        objProvider.ProviderName = objEmpResponse.EmployerName;
                                        objProvider.IsDeleted = objEmpResponse.IsDeleted;
                                        objProviderBAL.Save_Provider(objProvider);


                                        //End Update Provider

                                        // Update IndividualEmployment

                                        objIndEmployment = objIndEmploymentBAL.Get_IndividualEmployment_By_IndividualEmploymentId(objEmpResponse.IndividualEmploymentId);
                                        if (objIndEmployment != null)
                                        {
                                            objIndEmployment.ApplicationId = objEmpResponse.ApplicationId;
                                            objIndEmployment.IndividualId = objEmpResponse.IndividualId;
                                            objIndEmployment.ReferenceNumber = "";
                                            objIndEmployment.Role = "";
                                            objIndEmployment.IsWorkinginFieldofApplication = false;
                                            objIndEmployment.EverWorkedinFieldofApplication = false;
                                            objIndEmployment.EmploymentHistoryTypeId = 1;
                                            objIndEmployment.ModifiedBy = objToken.UserId;
                                            objIndEmployment.ModifiedOn = DateTime.Now;
                                            objIndEmployment.IsDeleted = objEmpResponse.IsDeleted;
                                            objIndEmployment.IndividualEmploymentId = objIndEmploymentBAL.Save_IndividualEmployment(objIndEmployment);
                                        }
                                    }
                                    else
                                    {
                                        // Save Provider
                                        objProvider = new Provider();

                                        objProvider.BillingNumber = "";
                                        objProvider.IsDeleted = objEmpResponse.IsDeleted;
                                        objProvider.DepartmentId = 2;
                                        objProvider.CreatedBy = objToken.UserId;
                                        objProvider.CreatedOn = DateTime.Now;
                                        objProvider.IsActive = true;

                                        objProvider.IsEnabled = true;
                                        objProvider.LicenseNumber = "";
                                        objProvider.OwnershipCompany = "";
                                        objProvider.ProviderDBAName = "";
                                        objProvider.ProviderGuid = Guid.NewGuid().ToString();

                                        objProvider.ProviderName = objEmpResponse.EmployerName;
                                        objProvider.ProviderNumber = "";
                                        objProvider.ProviderStatusTypeId = 1;
                                        objProvider.ProviderTypeId = 5;
                                        objProvider.ReferenceNumber = "";
                                        objProvider.TaxId = "";

                                        objProvider.ProviderId = objProviderBAL.Save_Provider(objProvider);

                                        //End Save provider

                                        // save IndividualEmployment

                                        objIndEmployment = new IndividualEmployment();

                                        objIndEmployment.ProviderId = objProvider.ProviderId;
                                        objIndEmployment.CreatedBy = objToken.UserId;
                                        objIndEmployment.CreatedOn = DateTime.Now;
                                        objIndEmployment.EmploymentStartDate = DateTime.Now;
                                        objIndEmployment.IsActive = true;
                                        objIndEmployment.IsDeleted = objEmpResponse.IsDeleted;
                                        objIndEmployment.EmploymentHistoryTypeId = 1;
                                        objIndEmployment.IndividualEmploymentGuid = Guid.NewGuid().ToString();
                                        objIndEmployment.ApplicationId = objEmpResponse.ApplicationId;
                                        objIndEmployment.IndividualId = objEmpResponse.IndividualId;
                                        objIndEmployment.PositionId = objEmpResponse.PositionId;
                                        objIndEmployment.EmploymentStatusId = objEmpResponse.EmploymentStatusId;
                                        objIndEmployment.EmploymentTypeId = objEmpResponse.EmploymentTypeId;
                                        objIndEmployment.ReferenceNumber = "";
                                        objIndEmployment.Role = "";
                                        objIndEmployment.IsWorkinginFieldofApplication = false;
                                        objIndEmployment.EverWorkedinFieldofApplication = false;

                                        //objIndEmployment.ModifiedBy = objToken.UserId;
                                        //objIndEmployment.ModifiedOn = DateTime.Now;

                                        objIndEmployment.IndividualEmploymentId = objIndEmploymentBAL.Save_IndividualEmployment(objIndEmployment);

                                        //End save IndividualEmployment
                                    }


                                    objEmpResponse.IndividualEmploymentId = objIndEmployment.IndividualEmploymentId;
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
                                                    objIndAddress = objIndAddressBAL.Get_IndividualEmploymentAddress_By_IndividualEmploymentAddressId(objAddressResponse.IndividualEmploymentAddressId);
                                                    if (objIndAddress != null)
                                                    {
                                                        objIndAddress.IndividualEmploymentAddressId = objAddressResponse.IndividualEmploymentAddressId;
                                                        objIndAddress.AddressId = objAddress.AddressId;
                                                        objIndAddress.AddressTypeId = objAddressResponse.AddressTypeId;
                                                        objIndAddress.IsMailingSameasPhysical = objAddressResponse.IsMailingSameasPhysical;
                                                        objIndAddress.ModifiedBy = objToken.UserId;
                                                        objIndAddress.ModifiedOn = DateTime.Now;
                                                        objIndAddress.IndividualId = IndividualId;
                                                        objIndAddress.IndividualEmploymentAddressGuid = Guid.NewGuid().ToString();
                                                        objIndAddress.IndividualEmploymentId = objEmpResponse.IndividualEmploymentId;

                                                        objIndAddressBAL.Save_IndividualEmploymentAddress(objIndAddress);
                                                    }
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
                                                objIndAddress.IsActive = true;
                                                objIndAddress.BeginDate = DateTime.Now;
                                                objIndAddress.IndividualEmploymentId = objEmpResponse.IndividualEmploymentId;
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

                                                    objIndContact = objIndContactBAL.Get_IndividualEmploymentContact_By_IndividualEmploymentContactId(objContactResponse.IndividualEmploymentContactId);
                                                    if (objIndContact != null)
                                                    {
                                                        objIndContact.IndividualEmploymentContactId = objContactResponse.IndividualEmploymentContactId;
                                                        objIndContact.ContactId = objContact.ContactId;
                                                        objIndContact.ContactTypeId = objContactResponse.ContactTypeId;
                                                        objIndContact.IsPreferredContact = objContactResponse.IsPreferredContact;
                                                        objIndContact.IsMobile = objContactResponse.IsMobile;

                                                        objIndContact.ModifiedBy = objToken.UserId;
                                                        objIndContact.ModifiedOn = DateTime.Now;
                                                        objIndContact.IndividualId = IndividualId;
                                                        objIndContact.IndividualEmploymentContactGuid = Guid.NewGuid().ToString();
                                                        objIndContact.IndividualEmploymentId = objEmpResponse.IndividualEmploymentId;
                                                        objIndContactBAL.Save_IndividualEmploymentContact(objIndContact);
                                                    }
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

                                                objIndContact.IndividualEmploymentId = objEmpResponse.IndividualEmploymentId;
                                                objIndContact.IndividualId = IndividualId;
                                                objIndContact.IndividualEmploymentContactGuid = Guid.NewGuid().ToString();
                                                objIndContact.IndividualEmploymentContactId = objContactResponse.IndividualEmploymentContactId;
                                                objIndContact.ContactId = objContact.ContactId;
                                                objIndContact.ContactTypeId = objContactResponse.ContactTypeId;
                                                objIndContact.IsPreferredContact = objContactResponse.IsPreferredContact;
                                                objIndContact.IsMobile = objContactResponse.IsMobile;
                                                objIndContact.IsActive = true;
                                                objIndContact.BeginDate = DateTime.Now;
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
                    }
                    catch (Exception ex)
                    {
                        LogingHelper.SaveExceptionInfo("", ex, "Renewal Process - Employer Information", ENTITY.Enumeration.eSeverity.Error);
                        throw ex;
                    }
                    #endregion


                    #region IndividualAffidavit
                    try
                    {
                        IndividualAffidavitResponse objIndividualAffidavitResponse = new IndividualAffidavitResponse();
                        IndividualAffidavitBAL objIndividualAffidavitBAL = new IndividualAffidavitBAL();

                        objIndividualAffidavitResponse = objIndividualRenewal.IndividualAffidavit;

                        if (objIndividualAffidavitResponse != null)
                        {
                            IndividualAffidavit objIndividualAffidavit = new IndividualAffidavit();

                            objIndividualAffidavit = objIndividualAffidavitBAL.Get_address_By_IndividualAffidavitId(objIndividualAffidavitResponse.IndividualAffidavitId);

                            if (objIndividualAffidavit != null)
                            {
                                objIndividualAffidavit.IndividualId = objIndividualAffidavitResponse.IndividualId;
                                objIndividualAffidavit.ContentItemLkId = objIndividualAffidavitResponse.ContentItemLkId;
                                objIndividualAffidavit.ContentItemNumber = objIndividualAffidavitResponse.ContentItemNumber;
                                objIndividualAffidavit.ContentItemResponse = objIndividualAffidavitResponse.ContentItemResponse;
                                objIndividualAffidavit.Desc = objIndividualAffidavitResponse.Desc;
                                objIndividualAffidavit.ModifiedBy = objToken.UserId;
                                objIndividualAffidavit.ModifiedOn = DateTime.Now;

                                objIndividualAffidavitBAL.Save_IndividualAffidavit(objIndividualAffidavit);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogingHelper.SaveExceptionInfo("", ex, "Renewal Process - Individual Affidavit", ENTITY.Enumeration.eSeverity.Error);
                        throw ex;
                    }

                    #endregion

                    #region Child Support Declaration
                    try
                    {
                        List<IndividualChildSupportResponse> lstChildSupportResponse = new List<IndividualChildSupportResponse>();
                        IndividualChildSupportBAL objChildBAL = new IndividualChildSupportBAL();

                        lstChildSupportResponse = objIndividualRenewal.IndividualChildSupport;
                        if (lstChildSupportResponse != null)
                        {
                            foreach (IndividualChildSupportResponse objResp in lstChildSupportResponse)
                            {
                                IndividualChildSupport objChild = objChildBAL.Get_address_By_IndividualChildSupportId(objResp.IndividualChildSupportId);
                                if (objChild != null)
                                {
                                    objChild.ModifiedBy = objToken.UserId;
                                    objChild.ModifiedOn = DateTime.Now;
                                    objChild.ContentItemResponse = objResp.ContentItemResponse;

                                    objChildBAL.Save_IndividualChildSupport(objChild);
                                }
                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        LogingHelper.SaveExceptionInfo("", ex, "Renewal Process - Child Support", ENTITY.Enumeration.eSeverity.Error);
                        throw ex;
                    }
                    #endregion


                    #region Individual Vetran

                    try
                    {
                        IndividualVeteranResponse objIndividualVetranResp = new IndividualVeteranResponse();
                        IndividualVeteranBAL objIndividualVetranBAL = new IndividualVeteranBAL();
                        objIndividualVetranResp = objIndividualRenewal.IndividualVeteran;
                        if (objIndividualVetranResp != null)
                        {
                            IndividualVeteran objVeteran = new IndividualVeteran();
                            objVeteran = objIndividualVetranBAL.Get_IndividualVeteran_By_IndividualId(IndividualId);
                            if (objVeteran != null)
                            {

                                objVeteran.ModifiedBy = objToken.UserId;
                                objVeteran.ModifiedOn = DateTime.Now;
                                objVeteran.IndividualId = IndividualId;
                                objVeteran.MilitaryOccupationSpeciality = objIndividualVetranResp.MilitaryOccupationSpeciality != null ? objIndividualVetranResp.MilitaryOccupationSpeciality : "";
                                objVeteran.ServedInMilitary = objIndividualVetranResp.ServedInMilitary;
                                objVeteran.ServiceDateFrom = objIndividualVetranResp.ServiceDateFrom;
                                objVeteran.ServiceDateTo = objIndividualVetranResp.ServiceDateTo;
                                objVeteran.SpouseofActiveMilitaryMember = objIndividualVetranResp.SpouseofActiveMilitaryMember;
                                objIndividualVetranBAL.Save_IndividualVeteran(objVeteran);

                                List<IndividualVeteranBranchResponse> lstBranchResp = new List<IndividualVeteranBranchResponse>();
                                lstBranchResp = objIndividualVetranResp.VeteranBranches;

                                if (lstBranchResp != null && lstBranchResp.Count > 0)
                                {
                                    IndividualVeteranBranchBAL objBranchBAL = new IndividualVeteranBranchBAL();
                                    foreach (IndividualVeteranBranchResponse objBranchResp in lstBranchResp)
                                    {
                                        IndividualVeteranBranch objBranch = new IndividualVeteranBranch();
                                        objBranch = objBranchBAL.Get_address_By_IndividualVeteranBranchId(objBranchResp.IndividualVeteranBranchId);
                                        if (objBranch != null)
                                        {
                                            objBranch.ModifiedBy = objToken.UserId;
                                            objBranch.ModifiedOn = DateTime.Now;
                                            objBranch.BranchofServicesIdResponse = objBranchResp.BranchofServicesIdResponse;
                                            objBranchBAL.Save_IndividualVeteranBranch(objBranch);
                                        }
                                    }
                                }
                            }



                        }


                    }
                    catch (Exception ex)
                    {
                        LogingHelper.SaveExceptionInfo("", ex, "Renewal Process - Veteran", ENTITY.Enumeration.eSeverity.Error);
                        throw ex;
                    }


                    #endregion


                    #region Sponsor
                    try
                    {
                        List<SponsorInformationResponse> lstSupervisoryInfoRes = new List<SponsorInformationResponse>();

                        IndividualSupervisoryInfoBAL objIndividualSupervisoryInfoBAL = new IndividualSupervisoryInfoBAL();
                        lstSupervisoryInfoRes = objIndividualRenewal.SponsorInformation;
                        if (lstSupervisoryInfoRes != null)
                        {
                            foreach (SponsorInformationResponse objSupervisoryInfoRes in lstSupervisoryInfoRes)
                            {

                                #region Sponsor Address
                                Address objAddress = new Address();
                                AddressBAL objAddressBAL = new AddressBAL();
                                if (objSupervisoryInfoRes.SponsorAddress != null && objSupervisoryInfoRes.SponsorAddress.Count > 0)
                                {
                                    SponsorAddressResponse objAddressResp = objSupervisoryInfoRes.SponsorAddress[0];
                                    if (objAddressResp.AddressId > 0)
                                    {
                                        objAddress = objAddressBAL.Get_address_By_AddressId(objAddressResp.AddressId);
                                        if (objAddress != null)
                                        {

                                            objAddress.City = objAddressResp.City;
                                            objAddress.StreetLine1 = objAddressResp.StreetLine1;
                                            objAddress.StreetLine2 = !string.IsNullOrEmpty(objAddressResp.StreetLine2) ? objAddressResp.StreetLine2 : "";
                                            objAddress.StateCode = objAddressResp.StateCode;
                                            objAddress.Zip = objAddressResp.Zip;

                                            objAddress.ModifiedBy = objToken.UserId;
                                            objAddress.ModifiedOn = DateTime.Now;
                                            objAddress.CountryId = 235;

                                            objAddress.IsActive = true;
                                            objAddress.IsDeleted = false;
                                            objAddress.UseUserAddress = false;
                                            objAddress.UseVerifiedAddress = false;

                                            objAddressBAL.Save_address(objAddress);
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

                                        objAddress.City = objAddressResp.City;
                                        objAddress.StreetLine1 = objAddressResp.StreetLine1;
                                        objAddress.StreetLine2 = !string.IsNullOrEmpty(objAddressResp.StreetLine2) ? objAddressResp.StreetLine2 : "";
                                        objAddress.StateCode = objAddressResp.StateCode;
                                        objAddress.Zip = objAddressResp.Zip;

                                        objAddress.CreatedBy = objToken.UserId;
                                        objAddress.CreatedOn = DateTime.Now;


                                        objAddress.AddressId = objAddressBAL.Save_address(objAddress);


                                    }
                                }

                                #endregion

                                #region Sponsor Name
                                IndividualName objIndividualName = new IndividualName();
                                IndividualNameBAL objIndividualNameBAL = new IndividualNameBAL();
                                objIndividualName = objIndividualNameBAL.Get_IndividualName_By_IndividualNameId(objSupervisoryInfoRes.IndividualNameId);
                                if (objIndividualName != null)
                                {
                                    objIndividualName.FirstName = objSupervisoryInfoRes.FirstName;
                                    objIndividualName.LastName = objSupervisoryInfoRes.LastName;
                                    objIndividualName.MiddleName = !string.IsNullOrEmpty(objSupervisoryInfoRes.MiddleName) ? objSupervisoryInfoRes.MiddleName : "";
                                    //objIndividualName.IndividualId = IndividualId;

                                    objIndividualName.IndividualNameStatusId = Convert.ToInt32(eIndividualNameStatus.Current);
                                    objIndividualName.IndividualNameTypeId = Convert.ToInt32(eIndividualNameType.Sponsor);

                                    objIndividualName.IsActive = true;
                                    objIndividualName.IsDeleted = false;
                                    objIndividualName.ModifiedBy = objToken.UserId;
                                    objIndividualName.ModifiedOn = DateTime.Now;

                                    objIndividualName.IndividualNameId = objIndividualNameBAL.Save_IndividualName(objIndividualName);


                                }
                                else
                                {
                                    objIndividualName = new IndividualName();
                                    objIndividualName.FirstName = objSupervisoryInfoRes.FirstName;
                                    objIndividualName.LastName = objSupervisoryInfoRes.LastName;
                                    objIndividualName.MiddleName = !string.IsNullOrEmpty(objSupervisoryInfoRes.MiddleName) ? objSupervisoryInfoRes.MiddleName : "";
                                    //objIndividualName.IndividualId = IndividualId;

                                    objIndividualName.IndividualNameStatusId = Convert.ToInt32(eIndividualNameStatus.Previous);
                                    objIndividualName.IndividualNameTypeId = Convert.ToInt32(eIndividualNameType.Sponsor);

                                    objIndividualName.IsActive = true;
                                    objIndividualName.IsDeleted = false;
                                    objIndividualName.CreatedBy = objToken.UserId;
                                    objIndividualName.CreatedOn = DateTime.Now;
                                    objIndividualName.IndividualNameGuid = Guid.NewGuid().ToString();
                                    objIndividualName.IndividualNameId = objIndividualNameBAL.Save_IndividualName(objIndividualName);


                                }

                                #endregion


                                IndividualSupervisoryInfo objSupervisory = new IndividualSupervisoryInfo();

                                objSupervisory = objIndividualSupervisoryInfoBAL.Get_IndividualSupervisoryInfo_By_IndividualSupervisoryInfoId(objSupervisoryInfoRes.IndividualSupervisoryInfoId);
                                if (objSupervisory != null)
                                {
                                    objSupervisory.SupervisorType = objSupervisoryInfoRes.SupervisorType;
                                    objSupervisory.ApplicationId = ApplicationId;
                                    objSupervisory.IndividualId = objSupervisoryInfoRes.IndividualId;
                                    objSupervisory.IsActive = objSupervisoryInfoRes.IsActive;
                                    objSupervisory.IsDeleted = objSupervisoryInfoRes.IsDeleted;
                                    objSupervisory.SupervisorLicenseNumber = objSupervisoryInfoRes.SupervisorLicenseNumber;
                                    objSupervisory.ModifiedBy = objToken.UserId;
                                    objSupervisory.ModifiedOn = DateTime.Now;
                                    objSupervisory.IndividualNameId = objIndividualName.IndividualNameId;
                                    objSupervisory.SupervisorWorkAddressId = objAddress.AddressId;
                                    objIndividualSupervisoryInfoBAL.Save_IndividualSupervisoryInfo(objSupervisory);
                                }

                                else
                                {
                                    objSupervisory = new IndividualSupervisoryInfo();
                                    objSupervisory.ApplicationId = ApplicationId;
                                    objSupervisory.Areyousupervised = false;
                                    objSupervisory.CreatedBy = objToken.UserId;
                                    objSupervisory.CreatedOn = DateTime.Now;
                                    objSupervisory.Doyousupervise = false;
                                    objSupervisory.IndividualSupervisoryInfoGuid = Guid.NewGuid().ToString();
                                    objSupervisory.ReferenceNumber = "";
                                    objSupervisory.SupervisedLicenseNumber = "";
                                    objSupervisory.SupervisedStateLicensed = "";
                                    objSupervisory.SupervisorStateLicensed = "";
                                    objSupervisory.IndividualId = objSupervisoryInfoRes.IndividualId;
                                    objSupervisory.IsActive = objSupervisoryInfoRes.IsActive;
                                    objSupervisory.SupervisorLicenseNumber = objSupervisoryInfoRes.SupervisorLicenseNumber;
                                    objSupervisory.IsDeleted = objSupervisoryInfoRes.IsDeleted;
                                    objSupervisory.IndividualNameId = objIndividualName.IndividualNameId;
                                    objSupervisory.SupervisorWorkAddressId = objAddress.AddressId;
                                    objSupervisory.SupervisorType = objSupervisoryInfoRes.SupervisorType;
                                    objIndividualSupervisoryInfoBAL.Save_IndividualSupervisoryInfo(objSupervisory);
                                }
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        LogingHelper.SaveExceptionInfo("", ex, "Renewal Process - Individual Sponsor", ENTITY.Enumeration.eSeverity.Error);
                        throw ex;
                    }
                    #endregion

                    return SelectOrCreateResponse(objToken, IndividualId);

                }
            }
            catch (Exception ex)
            {

                LogingHelper.SaveExceptionInfo("", ex, "SaveAndValidateRequest", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualRenewal = null;


            }
            return objResponse;
        }

        public static IndividualRenewalResponse SelectOrCreateResponse(Token objToken, int IndividualId, int ApplicationTypeId = 1)
        {
            int ApplicationId = 0;
            IndividualRenewalResponse objResponse = new IndividualRenewalResponse();
            try
            {


                IndividualBAL objIndividualBAL = new IndividualBAL();
                IndividualResponse objIndividualResponse = new IndividualResponse();
                objIndividualResponse = objIndividualBAL.Get_Individual_By_IndividualId(IndividualId);
                if (objIndividualResponse != null)
                {

                    Users objUseres = new Users();
                    UsersBAL objUsersBAL = new UsersBAL();
                    objUseres = objUsersBAL.Get_Users_byIndividualId(objIndividualResponse.IndividualId);
                    if (objUseres != null)
                    {
                        objIndividualResponse.Email = objUseres.Email;
                    }

                    IndividualRenewal objIndividualRenewal = new IndividualRenewal();
                    objResponse.IndividualRenewal = objIndividualRenewal;

                    #region Individual Proccess
                    objIndividualRenewal.Individual = objIndividualResponse;

                    #endregion

                    #region Validate License and Application 

                    IndividualLicense objLatestLicense = new IndividualLicense();
                    IndividualLicenseBAL objIndividualLicenseBAL = new IndividualLicenseBAL();

                    objLatestLicense = objIndividualLicenseBAL.Get_Latest_IndividualLicense_By_IndividualId(IndividualId);
                    if (objLatestLicense != null)
                    {
                        if (objLatestLicense.LicenseStatusTypeId == 6)
                        {
                            objResponse.ResponseReason = "";
                            objResponse.Message = "Either you are not allowed to renew or you have submitted your renewal. Please contact to the board.";
                            objResponse.Status = false;
                            objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.RenewalDenied).ToString("00");
                            objResponse.IndividualRenewal = null;
                            return objResponse;

                        }



                        bool NotValidLicenseRequestMax = false;
                        bool NotValidLicenseRequestMin = false;

                        int AllowedDaysMax = 0;
                        int AllowedDaysMin = 0;
                        ConfigurationTypeBAL objBAL = new ConfigurationTypeBAL();
                        ConfigurationType objEntity = new ConfigurationType();
                        ConfigurationType objConfigurationType = new ConfigurationType();
                        objConfigurationType = objBAL.Get_Configuration_By_Settings_object("Noofdaysallowedforrenewalfromlicenseexpdate".ToLower());
                        if (objConfigurationType != null)
                        {
                            AllowedDaysMax = Convert.ToInt32(objConfigurationType.Value);
                        }
                        objConfigurationType = new ConfigurationType();
                        objConfigurationType = objBAL.Get_Configuration_By_Settings_object("Noofdaysallowedforrenewalbeforelicenseexp".ToLower());
                        if (objConfigurationType != null)
                        {
                            AllowedDaysMin = Convert.ToInt32(objConfigurationType.Value);
                        }




                        List<IndividualLicense> lstIndividualLicense = new List<IndividualLicense>();
                        try
                        {



                            lstIndividualLicense = objIndividualLicenseBAL.Get_IndividualLicense_By_IndividualId(IndividualId);
                            if (lstIndividualLicense != null && lstIndividualLicense.Count > 0)
                            {

                                IndividualLicense objLicense = lstIndividualLicense[0];
                                if (objLicense.LicenseExpirationDate.Date.AddDays(-AllowedDaysMin) >= DateTime.Now)
                                {
                                    NotValidLicenseRequestMin = true;
                                }

                                if (objLicense.LicenseExpirationDate.Date.AddDays(AllowedDaysMax) <= DateTime.Now)
                                {
                                    NotValidLicenseRequestMax = true;
                                }





                                #region Check Renewal Denieal Status

                                if (NotValidLicenseRequestMin)
                                {
                                    objResponse.ResponseReason = "";// GlobalFunctions.GeneralFunctions.GetJsonStringFromList(lstResponseReason);
                                    objResponse.Message = "License renewal is not open yet. License can only be renewed starting  " + AllowedDaysMin + " days before the expiration date. ";
                                    objResponse.Status = false;
                                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.RenewalDenied).ToString("00");
                                    objResponse.IndividualRenewal = null;
                                    return objResponse;

                                }
                                if (NotValidLicenseRequestMax)
                                {
                                    objResponse.ResponseReason = "";// GlobalFunctions.GeneralFunctions.GetJsonStringFromList(lstResponseReason);
                                    objResponse.Message = "License can only be renewed within " + AllowedDaysMax + " days of the license expiration date. Please contact office.";
                                    objResponse.Status = false;
                                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.RenewalDenied).ToString("00");
                                    objResponse.IndividualRenewal = null;
                                    return objResponse;

                                }

                                #endregion




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
                                objResponse.ResponseReason = "";
                                objResponse.Message = "Either you are not allowed to renew or you have submitted your renewal. Please contact to the board.";
                                objResponse.Status = false;
                                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.RenewalDenied).ToString("00");
                                objResponse.IndividualRenewal = null;
                                return objResponse;

                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;

                        }



                        #region Check Individual Application Table for pending Application

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

                                objApplication.LicenseTypeId = objIndividualRenewal.IndividualLicense[0].LicenseTypeId;
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
                                objApplication.LicenseTypeId = objIndividualRenewal.IndividualLicense[0].LicenseTypeId;
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
                            LogingHelper.SaveExceptionInfo("", ex, "Renewal Process Response - Application Proccess", ENTITY.Enumeration.eSeverity.Error);
                            throw ex;
                        }
                        #endregion


                        #region Create and select Pending License
                        IndividualLicense objPendingLicense = new IndividualLicense();

                        try
                        {
                            objPendingLicense = objIndividualLicenseBAL.Get_Pending_IndividualLicense_By_IndividualId(IndividualId);
                            if (objPendingLicense != null)
                            {
                                lstIndividualLicense = new List<IndividualLicense>();
                                objPendingLicense.IsActive = true;
                                objPendingLicense.IndividualLicenseId = objIndividualLicenseBAL.Save_IndividualLicense(objPendingLicense);

                                lstIndividualLicense.Add(objPendingLicense);
                            }
                            else
                            {
                                if (objIndividualRenewal.IndividualLicense != null)
                                {
                                    objPendingLicense = new IndividualLicense();
                                    objPendingLicense.IndividualId = IndividualId;
                                    objPendingLicense.ApplicationId = ApplicationId;
                                    objPendingLicense.ApplicationTypeId = ApplicationTypeId;
                                    objPendingLicense.LicenseTypeId = objLatestLicense.LicenseTypeId;
                                    objPendingLicense.IsLicenseTemporary = false;
                                    objPendingLicense.IsLicenseActive = false;
                                    objPendingLicense.LicenseNumber = objLatestLicense.LicenseNumber;
                                    objPendingLicense.OriginalLicenseDate = objLatestLicense.OriginalLicenseDate;
                                    objPendingLicense.LicenseEffectiveDate = objLatestLicense.LicenseExpirationDate.AddDays(1);
                                    objPendingLicense.LicenseExpirationDate = objLatestLicense.LicenseExpirationDate.AddYears(1);
                                    objPendingLicense.LicenseStatusTypeId = objLatestLicense.LicenseStatusTypeId == 1 ? 5 : objLatestLicense.LicenseStatusTypeId == 2 ? 5 : 8;
                                    objPendingLicense.IsDeleted = false;
                                    objPendingLicense.CreatedBy = objToken.UserId;
                                    objPendingLicense.CreatedOn = DateTime.Now;
                                    objPendingLicense.IndividualLicenseGuid = Guid.NewGuid().ToString();
                                    objPendingLicense.IsActive = true;
                                    objPendingLicense.IndividualLicenseId = objIndividualLicenseBAL.Save_IndividualLicense(objPendingLicense);

                                    lstIndividualLicense = new List<IndividualLicense>();
                                    lstIndividualLicense.Add(objPendingLicense);

                                    LogHelper.SaveIndividualLog(objPendingLicense.IndividualId, objPendingLicense.ApplicationId, (eCommentLogSource.WSAPI).ToString(), "Penidng Renewal created, Start Date: " + objPendingLicense.LicenseEffectiveDate.ToShortDateString() + ", End Date: " + objPendingLicense.LicenseExpirationDate.ToShortDateString() + ", Status: Pending Renewal, Updated To: Pending Renewal, Updated On: " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"), objToken.UserId);

                                }
                            }

                            List<IndividualLicenseResponse> lstLicenseResponsePending = lstIndividualLicense.Select(obj => new IndividualLicenseResponse
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
                            objIndividualRenewal.IndividualLicense = lstLicenseResponsePending;
                        }
                        catch (Exception ex)
                        {
                            LogingHelper.SaveExceptionInfo("", ex, "Renewal Process Response - Pending License", ENTITY.Enumeration.eSeverity.Error);
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
                        LogingHelper.SaveExceptionInfo("", ex, "Renewal Process Response - Address", ENTITY.Enumeration.eSeverity.Error);
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
                        LogingHelper.SaveExceptionInfo("", ex, "Renewal Process Response - Contact", ENTITY.Enumeration.eSeverity.Error);
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
                                Role = obj.Role,
                                EmployerName = obj.EmployerName

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
                        LogingHelper.SaveExceptionInfo("", ex, "Renewal Process Response - Employment Information", ENTITY.Enumeration.eSeverity.Error);
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
                        LogingHelper.SaveExceptionInfo("", ex, "Renewal Process Response - Individual Certification", ENTITY.Enumeration.eSeverity.Error);
                        throw ex;
                    }
                    #endregion

                    #region NV Business License

                    //objIndividualRenewal.BusinessLicenseInformation = new List<IndividualNVBusinessLicenseResponse>();
                    try
                    {

                        List<IndividualNVBusinessLicense> lstIndividualNVBusinessLicense = new List<IndividualNVBusinessLicense>();
                        IndividualNVBusinessLicenseBAL objIndividualAddressBAL = new IndividualNVBusinessLicenseBAL();
                        lstIndividualNVBusinessLicense = objIndividualAddressBAL.Get_IndividualNVBusinessLicense_By_IndividualId(IndividualId);
                        if (lstIndividualNVBusinessLicense != null && lstIndividualNVBusinessLicense.Count > 0)
                        {
                            List<IndividualNVBusinessLicenseResponse> lstBusinessResponse = lstIndividualNVBusinessLicense
                                .Select(obj => new IndividualNVBusinessLicenseResponse
                                {
                                    IndividualNVBusinessLicenseId = obj.IndividualNVBusinessLicenseId,
                                    IndividualId = obj.IndividualId,
                                    ContentItemLkId = obj.ContentItemLkId,
                                    ContentItemHash = obj.ContentItemHash,
                                    ContentItemResponse = obj.ContentItemResponse,
                                    Status = obj.Status,
                                    NameonBusinessLicense = obj.NameonBusinessLicense,
                                    BusinessLicenseNumber = obj.BusinessLicenseNumber,
                                    ContentDescription = obj.ContentDescription,
                                    IsActive = obj.IsActive,
                                }).ToList();

                            objIndividualRenewal.BusinessLicenseInformation = lstBusinessResponse;
                        }
                        else
                        {
                            objIndividualRenewal.BusinessLicenseInformation = new List<IndividualNVBusinessLicenseResponse>();

                        }

                    }
                    catch (Exception ex)
                    {
                        LogingHelper.SaveExceptionInfo("", ex, "Renewal Process Response - NV Business License", ENTITY.Enumeration.eSeverity.Error);
                        throw ex;
                    }



                    #endregion

                    #region Individual Legal

                    try
                    {
                        List<IndividualLegal> lstIndividualLegal = new List<IndividualLegal>();
                        IndividualLegalBAL objIndividualLegalBAL = new IndividualLegalBAL();
                        lstIndividualLegal = objIndividualLegalBAL.Get_IndividualLegal_By_IndividualId(IndividualId);
                        if (lstIndividualLegal != null && lstIndividualLegal.Count > 0)
                        {
                            List<IndividualLegalResponse> lstIndividualLegalResponse = lstIndividualLegal
                                .Select(obj => new IndividualLegalResponse
                                {
                                    IndividualLegalId = obj.IndividualLegalId,
                                    IndividualId = obj.IndividualId,
                                    ContentItemLkId = obj.ContentItemLkId,
                                    ContentItemNumber = obj.ContentItemNumber,
                                    ContentItemResponse = obj.ContentItemResponse,
                                    Desc = obj.Desc,
                                    ContentDescription = obj.ContentDescription,
                                    IsActive = obj.IsActive,
                                }).ToList();

                            objIndividualRenewal.IndividualLegal = lstIndividualLegalResponse;
                        }


                    }
                    catch (Exception ex)
                    {
                        LogingHelper.SaveExceptionInfo("", ex, "Renewal Process Response - Individual Legal", ENTITY.Enumeration.eSeverity.Error);
                        throw ex;
                    }


                    #endregion


                    #region Fees Detail

                    try
                    {
                        List<FeeDetails> lstFees = new List<FeeDetails>();
                        List<RevFeeMaster> lstFeeMaster = new List<RevFeeMaster>();
                        RevFeeMasterBAL objFeeMasterBAL = new RevFeeMasterBAL();
                        lstFeeMaster = objFeeMasterBAL.Get_RevFeeMaster_By_LicenseTypeId(objIndividualRenewal.Application.LicenseTypeId);
                        if (lstFeeMaster != null)
                        {
                            foreach (RevFeeMaster objFeeMaster in lstFeeMaster)
                            {
                                FeeDetails objFees = new FeeDetails();
                                objFees.FeeAmount = objFeeMaster.FeeAmount;
                                objFees.Description = "";
                                objFees.RevMstFeeId = objFeeMaster.RevFeeMasterId;
                                objFees.FeeTypeID = objFeeMaster.FeeTypeId;
                                objFees.LicenseTypeId = objFeeMaster.LicenseTypeId;
                                objFees.FeeName = objFeeMaster.FeeName;
                                objFees.IndividualLicenseId = objIndividualRenewal.IndividualLicense[0].IndividualLicenseId;

                                lstFees.Add(objFees);
                            }
                        }

                        objIndividualRenewal.FeesDetails = lstFees;

                    }
                    catch (Exception ex)
                    {
                        LogingHelper.SaveExceptionInfo("", ex, "Renewal Process Response - Fees Detail", ENTITY.Enumeration.eSeverity.Error);
                        throw ex;
                    }
                    #endregion

                    #region Individual CE Hours

                    //List<IndividualCEHours> lstCEHours = new List<IndividualCEHours>();
                    //IndividualCEHoursBAL objCEHoursBAL = new IndividualCEHoursBAL();
                    //lstCEHours = objCEHoursBAL.Get_IndividualCEHours_By_IndividualId(IndividualId);
                    //if (lstCEHours != null && lstCEHours.Count > 0)
                    //{
                    //    List<IndividualCEHResponse> lstCEHResponse = lstCEHours.Select(obj => new IndividualCEHResponse
                    //    {
                    //        ApplicationId = obj.ApplicationId,
                    //        CECarryInHours = obj.CECarryInHours,
                    //        CECurrentReportedHours = obj.CECurrentReportedHours,
                    //        CEHoursDueDate = obj.CEHoursDueDate,
                    //        CEHoursEndDate = obj.CEHoursEndDate,
                    //        CEHoursReportingYear = obj.CEHoursReportingYear,
                    //        CEHoursStartDate = obj.CEHoursStartDate,
                    //        CEHoursStatusId = obj.CEHoursStatusId,
                    //        CEHoursTypeId = obj.CEHoursTypeId,
                    //        CERequiredHours = obj.CERequiredHours,
                    //        CERolloverHours = obj.CERolloverHours,
                    //        IndividualCEHoursId = obj.IndividualCEHoursId,
                    //        IndividualId = obj.IndividualId,
                    //        IsActive = obj.IsActive,
                    //        ReferenceNumber = obj.ReferenceNumber

                    //    }).ToList();
                    //    objIndividualRenewal.IndividualCEH = lstCEHResponse;

                    //}
                    //else
                    //{
                    //    objIndividualRenewal.IndividualCEH = new List<IndividualCEHResponse>();
                    //}

                    #endregion

                    #region Individual CE Course
                    try
                    {
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
                    }
                    catch (Exception ex)
                    {
                        LogingHelper.SaveExceptionInfo("", ex, "Renewal Process Response - CE Course", ENTITY.Enumeration.eSeverity.Error);
                        throw ex;
                    }
                    #endregion


                    #region Affidavit
                    try
                    {
                        IndividualAffidavit objAffidavit = new IndividualAffidavit();
                        List<IndividualAffidavit> lstobjAffidavit = new List<IndividualAffidavit>();
                        IndividualAffidavitBAL objAffidavitBAL = new IndividualAffidavitBAL();
                        objAffidavit = objAffidavitBAL.Get_IndividualAffidavit_By_IndividualId(IndividualId);
                        if (objAffidavit != null)
                        {
                            lstobjAffidavit.Add(objAffidavit);
                            List<IndividualAffidavitResponse> objAffidavitReesp = lstobjAffidavit
                                   .Select(obj => new IndividualAffidavitResponse
                                   {
                                       IndividualAffidavitId = obj.IndividualAffidavitId,
                                       IndividualId = obj.IndividualId,
                                       ContentItemLkId = obj.ContentItemLkId,
                                       ContentItemNumber = obj.ContentItemNumber,
                                       ContentItemResponse = obj.ContentItemResponse,
                                       Desc = obj.Desc,
                                       ContentDescription = obj.ContentDescription,
                                       IsActive = obj.IsActive,
                                   }).ToList();

                            objIndividualRenewal.IndividualAffidavit = lstobjAffidavit[0];
                        }
                    }
                    catch (Exception ex)
                    {
                        LogingHelper.SaveExceptionInfo("", ex, "Renewal Process Response - Affidavit", ENTITY.Enumeration.eSeverity.Error);
                        throw ex;
                    }
                    #endregion

                    #region Child Support Declaration
                    try
                    {
                        List<IndividualChildSupport> lstChildSupport = new List<IndividualChildSupport>();
                        IndividualChildSupportBAL objChildBAL = new IndividualChildSupportBAL();

                        lstChildSupport = objChildBAL.Get_IndividualChildSupport_By_IndividualId(IndividualId);
                        if (lstChildSupport != null)
                        {
                            List<IndividualChildSupportResponse> lstChildResponse = lstChildSupport.Select(obj => new IndividualChildSupportResponse
                            {
                                ContentItemNumber = obj.ContentItemNumber,
                                ContentItemLkId = obj.ContentItemLkId,
                                ContentItemResponse = obj.ContentItemResponse,
                                IndividualChildSupportId = obj.IndividualChildSupportId,
                                IndividualId = obj.IndividualId,
                                IsActive = obj.IsActive,
                                ContentDescription = obj.ContentDescription,
                                ContentItemLkCode = obj.ContentItemLkCode
                            }).ToList();

                            objIndividualRenewal.IndividualChildSupport = lstChildResponse;

                        }
                    }
                    catch (Exception ex)
                    {
                        LogingHelper.SaveExceptionInfo("", ex, "Renewal Process Response - Child Support", ENTITY.Enumeration.eSeverity.Error);
                        throw ex;
                    }
                    #endregion


                    #region Individual Vetran

                    try
                    {
                        IndividualVeteran objIndividualVetran = new IndividualVeteran();
                        IndividualVeteranBAL objIndividualVetranBAL = new IndividualVeteranBAL();
                        objIndividualVetran = objIndividualVetranBAL.Get_IndividualVeteran_By_IndividualId(IndividualId);
                        if (objIndividualVetran != null)
                        {

                            //List<IndividualVeteran> lstIndVeteran = new List<IndividualVeteran>();
                            //lstIndVeteran.Add(objIndividualVetran);

                            //IndividualVeteranResponse objIndividualVetranResponse = (IndividualVeteranResponse)lstIndVeteran
                            //   .Select(obj => new IndividualVeteranResponse
                            //   {

                            //       IndividualId = obj.IndividualId,

                            //       IndividualVeteranId = obj.IndividualVeteranId,
                            //       MilitaryOccupationSpeciality = obj.MilitaryOccupationSpeciality,
                            //       ServedInMilitary = obj.ServedInMilitary,
                            //       ServiceDateFrom = obj.ServiceDateFrom,
                            //       ServiceDateTo = obj.ServiceDateTo,
                            //       SpouseofActiveMilitaryMember = obj.SpouseofActiveMilitaryMember,
                            //       VeteranBranches = obj.VeteranBranches,
                            //       IsActive = obj.IsActive,
                            //   });
                            objIndividualRenewal.IndividualVeteran = objIndividualVetran;
                            List<IndividualVeteranBranch> lstBranchs = new List<IndividualVeteranBranch>();
                            IndividualVeteranBranchBAL objBranchBAL = new IndividualVeteranBranchBAL();
                            lstBranchs = objBranchBAL.Get_IndividualVeteranBranch_By_IndividualId_VeteranId(IndividualId, objIndividualVetran.IndividualVeteranId);
                            if (lstBranchs != null)
                            {
                                List<IndividualVeteranBranchResponse> lstBranchResp = lstBranchs.Select(obj => new IndividualVeteranBranchResponse
                                {
                                    BranchofServicesId = obj.BranchofServicesId,
                                    BranchofServicesIdResponse = obj.BranchofServicesIdResponse,
                                    ContentDescription = obj.ContentDescription,
                                    ContentItemLkCode = obj.ContentItemLkCode,
                                    IndividualId = obj.IndividualId,
                                    IndividualVeteranBranchId = obj.IndividualVeteranBranchId,
                                    IndividualVeteranId = obj.IndividualVeteranId,
                                    IsActive = obj.IsActive
                                }).ToList();

                                objIndividualRenewal.IndividualVeteran.VeteranBranches = lstBranchResp;

                            }


                        }


                    }
                    catch (Exception ex)
                    {
                        LogingHelper.SaveExceptionInfo("", ex, "Renewal Process Response - Veteran", ENTITY.Enumeration.eSeverity.Error);
                        throw ex;
                    }

                    #endregion

                    #region Sponsor
                    try
                    {
                        List<IndividualSupervisoryInfo> lstIndividualSupervisoryInfo = new List<IndividualSupervisoryInfo>();

                        List<SponsorInformationResponse> lstSponsorInformationResponse = new List<SponsorInformationResponse>();
                        IndividualSupervisoryInfoBAL objIndividualSupervisoryInfoBAL = new IndividualSupervisoryInfoBAL();
                        lstIndividualSupervisoryInfo = objIndividualSupervisoryInfoBAL.Get_IndividualSupervisoryInfo_By_ApplicationId(ApplicationId);
                        if (lstIndividualSupervisoryInfo != null && lstIndividualSupervisoryInfo.Count > 0)
                        {
                            foreach (IndividualSupervisoryInfo objIndividualSupervisoryInfo in lstIndividualSupervisoryInfo)
                            {
                                SponsorInformationResponse objSupervisoryResp = new SponsorInformationResponse();
                                objSupervisoryResp.ApplicationId = objIndividualSupervisoryInfo.ApplicationId;
                                // objSupervisoryResp.Areyousupervised = objIndividualSupervisoryInfo.Areyousupervised;
                                //  objSupervisoryResp.Doyousupervise = objIndividualSupervisoryInfo.Doyousupervise;
                                objSupervisoryResp.FirstName = objIndividualSupervisoryInfo.FirstName;
                                objSupervisoryResp.IndividualId = objIndividualSupervisoryInfo.IndividualId;
                                objSupervisoryResp.IndividualSupervisoryInfoId = objIndividualSupervisoryInfo.IndividualSupervisoryInfoId;
                                objSupervisoryResp.IsActive = objIndividualSupervisoryInfo.IsActive;
                                objSupervisoryResp.IsDeleted = objIndividualSupervisoryInfo.IsDeleted;

                                objSupervisoryResp.LastName = objIndividualSupervisoryInfo.LastName;
                                objSupervisoryResp.MiddleName = objIndividualSupervisoryInfo.MiddleName;
                                //objSupervisoryResp.ReferenceNumber = objIndividualSupervisoryInfo.ReferenceNumber;
                                //  objSupervisoryResp.SupervisedIndividualId = objIndividualSupervisoryInfo.SupervisedIndividualId;
                                //objSupervisoryResp.SupervisedLicenseExpirationDate = objIndividualSupervisoryInfo.SupervisedLicenseExpirationDate;
                                // objSupervisoryResp.SupervisedLicenseNumber = objIndividualSupervisoryInfo.SupervisedLicenseNumber;
                                //objSupervisoryResp.SupervisedMailingAddressId = objIndividualSupervisoryInfo.SupervisedMailingAddressId;
                                // objSupervisoryResp.SupervisedStateLicensed = objIndividualSupervisoryInfo.SupervisedStateLicensed;

                                //objSupervisoryResp.SupervisedWorkAddressId = objIndividualSupervisoryInfo.SupervisedWorkAddressId;
                                //objSupervisoryResp.SupervisedWorkEmailContactId = objIndividualSupervisoryInfo.SupervisedWorkEmailContactId;
                                //objSupervisoryResp.SupervisedWorkFaxContactId = objIndividualSupervisoryInfo.SupervisedWorkFaxContactId;
                                // objSupervisoryResp.SupervisedWorkPhoneContactId = objIndividualSupervisoryInfo.SupervisedWorkPhoneContactId;
                                // objSupervisoryResp.SupervisorIndividualId = objIndividualSupervisoryInfo.SupervisorIndividualId;
                                // objSupervisoryResp.SupervisorLicenseExpirationDate = objIndividualSupervisoryInfo.SupervisorLicenseExpirationDate;
                                objSupervisoryResp.SupervisorLicenseNumber = objIndividualSupervisoryInfo.SupervisorLicenseNumber;
                                objSupervisoryResp.SupervisorWorkAddressId = objIndividualSupervisoryInfo.SupervisorWorkAddressId;
                                objSupervisoryResp.IndividualNameId = objIndividualSupervisoryInfo.IndividualNameId;

                                objSupervisoryResp.SupervisorType = objIndividualSupervisoryInfo.SupervisorType;



                                List<Address> lstAddress = new List<Address>();
                                AddressBAL objAddressBAL = new AddressBAL();
                                Address objAddress = objAddressBAL.Get_address_By_AddressId((objIndividualSupervisoryInfo.SupervisorWorkAddressId != null ? Convert.ToInt32(objIndividualSupervisoryInfo.SupervisorWorkAddressId) : 0));
                                if (objAddress != null)
                                {
                                    lstAddress.Add(objAddress);
                                }

                                List<SponsorAddressResponse> lstsponsorAddress = lstAddress.Select(obj => new SponsorAddressResponse
                                {
                                    AddressId = obj.AddressId,
                                    City = obj.City,
                                    StateCode = obj.StateCode,
                                    StreetLine1 = obj.StreetLine1,
                                    StreetLine2 = obj.StreetLine2,
                                    Zip = obj.Zip
                                }).ToList();

                                if (objSupervisoryResp != null)
                                {

                                    objSupervisoryResp.SponsorAddress = lstsponsorAddress;
                                }
                                lstSponsorInformationResponse.Add(objSupervisoryResp);
                            }
                            objIndividualRenewal.SponsorInformation = lstSponsorInformationResponse;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogingHelper.SaveExceptionInfo("", ex, "Renewal Process Response - Sponsor", ENTITY.Enumeration.eSeverity.Error);
                        throw ex;
                    }
                    #endregion


                    objResponse.Status = true;
                    objResponse.Message = "Success";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo("", ex, "SelectOrCreateResponse", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualRenewal = null;


            }
            return objResponse;


        }

        public static void ChangeLicenseStatus(int IndividualLicenseId, int ApplicationId, int RequestedLicenseStatusTypeId, Token objToken)
        {
            try
            {
                bool RenewalApprovalRequired = false;
                bool RenewalApprovalRequiredifLegalInfoIsYes = false;


                ConfigurationTypeBAL objBAL = new ConfigurationTypeBAL();
                ConfigurationType objEntity = new ConfigurationType();
                ConfigurationType objConfigurationType = new ConfigurationType();
                objConfigurationType = objBAL.Get_Configuration_By_Settings_object("RenewalApprovalRequired".ToLower());
                if (objConfigurationType != null)
                {
                    RenewalApprovalRequired = Convert.ToBoolean(objConfigurationType.Value);
                }
                objConfigurationType = new ConfigurationType();
                objConfigurationType = objBAL.Get_Configuration_By_Settings_object("RenewalApprovalRequiredifLegalInfoIsYes".ToLower());
                if (objConfigurationType != null)
                {
                    RenewalApprovalRequiredifLegalInfoIsYes = Convert.ToBoolean(objConfigurationType.Value);
                }

                IndividualLicense objPedningLicense = new IndividualLicense();
                IndividualLicenseBAL objPendingLicenseBAL = new IndividualLicenseBAL();

                objPedningLicense = objPendingLicenseBAL.Get_IndividualLicense_By_IndividualLicenseId(IndividualLicenseId);
                if (objPedningLicense != null)
                {

                    if (!RenewalApprovalRequired)
                    {
                        if (!RenewalApprovalRequiredifLegalInfoIsYes)
                        {
                            objPedningLicense.LicenseStatusTypeId = RequestedLicenseStatusTypeId;// 1;
                        }
                        else
                        {
                            objPedningLicense.LicenseStatusTypeId = 6;
                        }
                    }
                    else
                    {
                        objPedningLicense.LicenseStatusTypeId = 6;

                    }
                    objPedningLicense.ModifiedOn = DateTime.Now;
                    objPedningLicense.ModifiedBy = objToken.UserId;



                    //
                    #region  Change Status of Previous renewal
                    List<IndividualLicense> lstIndividualLicense = new List<IndividualLicense>();
                    try
                    {



                        lstIndividualLicense = objPendingLicenseBAL.Get_IndividualLicense_By_IndividualId(objPedningLicense.IndividualId);
                        if (lstIndividualLicense != null && lstIndividualLicense.Count > 0)
                        {
                            IndividualLicense objLicense = lstIndividualLicense[0];
                            if (objLicense != null && objPedningLicense.LicenseStatusTypeId != 6)
                            {
                                objLicense.LicenseStatusTypeId = 7;//Renewed
                                objLicense.ModifiedOn = DateTime.Now;
                                objLicense.ModifiedBy = objToken.UserId;
                                objPendingLicenseBAL.Save_IndividualLicense(objLicense);

                                LogHelper.SaveIndividualLog(objPedningLicense.IndividualId, objPedningLicense.ApplicationId, (eCommentLogSource.WSAPI).ToString(), "License Status Changed, Start Date:" + objLicense.LicenseEffectiveDate.ToShortDateString() + ", End Date: " + objLicense.LicenseExpirationDate.ToShortDateString() + ", Status: " + (objLicense.LicenseStatusTypeId == 1 ? "Active" : "Expired") + " , Updated To: " + (RequestedLicenseStatusTypeId == 1 ? "Active" : "Inactive") + ", Updated On: " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"), objToken.UserId);

                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        LogingHelper.SaveExceptionInfo("", ex, "ChangeLicenseStatus - Previous Renewal", eSeverity.Critical);

                    }
                    #endregion

                    objPendingLicenseBAL.Save_IndividualLicense(objPedningLicense);
                    if (objPedningLicense.LicenseStatusTypeId != 6)
                    {
                        LogHelper.SaveIndividualLog(objPedningLicense.IndividualId, objPedningLicense.ApplicationId, (eCommentLogSource.WSAPI).ToString(), "License Renewed, Start Date:" + objPedningLicense.LicenseEffectiveDate.ToShortDateString() + ", End Date: " + objPedningLicense.LicenseExpirationDate.ToShortDateString() + ", Status: Pending Renewal , Updated To: " + (RequestedLicenseStatusTypeId == 1 ? "Active" : "Inactive") + ", Updated On: " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"), objToken.UserId);
                    }
                    else
                    {
                        LogHelper.SaveIndividualLog(objPedningLicense.IndividualId, objPedningLicense.ApplicationId, (eCommentLogSource.WSAPI).ToString(), "License  Status Changed, Start Date:" + objPedningLicense.LicenseEffectiveDate.ToShortDateString() + ", End Date: " + objPedningLicense.LicenseExpirationDate.ToShortDateString() + ", Status: Pending Renewal , Updated To: Pending Approval, Updated On: " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"), objToken.UserId);


                    }
                }
                try
                {
                    Application objApplication = new Application();
                    ApplicationBAL objApplicationBAL = new ApplicationBAL();
                    objApplication = objApplicationBAL.Get_Application_By_ApplicationId(ApplicationId);
                    if (objApplication != null)
                    {
                        objApplication.SubmittedDate = DateTime.Now;
                        objApplication.ApplicationStatusDate = DateTime.Now;
                        if (!RenewalApprovalRequired)
                        {
                            if (!RenewalApprovalRequiredifLegalInfoIsYes)
                            {
                                objApplication.ApplicationStatusId = 3;
                            }
                            else
                            {

                                objApplication.ApplicationStatusId = 2;

                            }
                        }
                        else
                        {
                            objApplication.ApplicationStatusId = 2;

                        }

                        objApplicationBAL.Save_Application(objApplication);


                    }

                }
                catch (Exception ex)
                {
                    LogingHelper.SaveExceptionInfo("", ex, "ChangeLicenseStatus - Application Status", eSeverity.Critical);

                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo("", ex, "ChangeLicenseStatus", eSeverity.Critical);

            }
        }
    }
}
