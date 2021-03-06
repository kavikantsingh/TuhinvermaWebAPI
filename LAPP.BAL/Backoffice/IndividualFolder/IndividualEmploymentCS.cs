﻿using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.LOGING;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LAPP.BAL.Backoffice.IndividualFolder
{
    public class IndividualEmploymentCS
    {

        public static IndividualEmploymentResponseRequest SaveIndividualEmployment(Token objToken, IndividualEmploymentResponse objEmploymentResponse)
        {
            ProviderBAL objProviderBAL = new ProviderBAL();
            Provider objProvider = new Provider();
            IndividualEmploymentResponseRequest objResponse = new IndividualEmploymentResponseRequest();
            IndividualEmploymentBAL objIndividualEmploymentBAL = new IndividualEmploymentBAL();
            IndividualEmploymentResponse objIndividualEmploymentResponse = new IndividualEmploymentResponse();
            IndividualEmployment objIndEmployment = new IndividualEmployment();
            List<IndividualEmployment> lstIndividualEmployment = new List<IndividualEmployment>();
            List<IndividualEmploymentResponse> lstEmploymentResponse = new List<IndividualEmploymentResponse>();
            try
            {
                int individualId = objEmploymentResponse.IndividualId;
                int? applicationId = objEmploymentResponse.ApplicationId;

                objIndEmployment = objIndividualEmploymentBAL.Get_IndividualEmployment_By_IndividualEmploymentId(objEmploymentResponse.IndividualEmploymentId);
                if (objIndEmployment != null)
                {
                    // Update Provider
                    objProvider = objProviderBAL.Get_Provider_By_ProviderId(objEmploymentResponse.ProviderId);
                    if (objProvider != null)
                    {
                        objProvider.ModifiedBy = objToken.UserId;
                        objProvider.ModifiedOn = DateTime.Now;
                        objProvider.ProviderName = objEmploymentResponse.EmployerName;
                        objProvider.IsActive = objEmploymentResponse.IsActive;
                        objProvider.IsDeleted = objEmploymentResponse.IsDeleted;
                        objProviderBAL.Save_Provider(objProvider);
                    }

                    //End Update Provider

                    // Update IndividualEmployment


                    objIndEmployment.ApplicationId = applicationId;
                    objIndEmployment.IndividualId = objEmploymentResponse.IndividualId;
                    objIndEmployment.PositionId = objEmploymentResponse.PositionId;
                    objIndEmployment.EmploymentStatusId = objEmploymentResponse.EmploymentStatusId;
                    objIndEmployment.EmploymentTypeId = objEmploymentResponse.EmploymentTypeId;
                    objIndEmployment.ReferenceNumber = "";
                    objIndEmployment.Role = "";
                    objIndEmployment.EmploymentHistoryTypeId = objEmploymentResponse.EmploymentHistoryTypeId;
                    objIndEmployment.ModifiedBy = objToken.UserId;
                    objIndEmployment.ModifiedOn = DateTime.Now;
                    objIndEmployment.EmployerName = objEmploymentResponse.EmployerName;
                    objIndEmployment.IsActive = objEmploymentResponse.IsActive;
                    objIndEmployment.IsDeleted = objEmploymentResponse.IsDeleted;
                    objIndEmployment.IndividualEmploymentId = objIndividualEmploymentBAL.Save_IndividualEmployment(objIndEmployment);

                    //SAVE LOG

                    string logText = "Individual Employment updated successfully. Updated on " + DateTime.Now.ToShortDateString();
                    string logSource = eCommentLogSource.WSAPI.ToString();
                    LogHelper.SaveIndividualLog(individualId, applicationId, logSource, logText, objToken.UserId, null, null, null);

                    //END SAVE LOG


                    objResponse.Message = MessagesClass.UpdateSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    lstIndividualEmployment.Add(objIndEmployment);

                }

                else
                {
                    // Save Provider
                    objProvider = new Provider();

                    objProvider.BillingNumber = "";

                    objProvider.DepartmentId = 2;
                    objProvider.CreatedBy = objToken.UserId;
                    objProvider.CreatedOn = DateTime.Now;
                    objProvider.IsActive = objEmploymentResponse.IsActive;
                    objProvider.IsDeleted = objEmploymentResponse.IsDeleted;
                    objProvider.IsEnabled = true;
                    objProvider.LicenseNumber = "";
                    objProvider.OwnershipCompany = "";
                    objProvider.ProviderDBAName = "";
                    objProvider.ProviderGuid = Guid.NewGuid().ToString();

                    objProvider.ProviderName = objEmploymentResponse.EmployerName;
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
                    objIndEmployment.IsActive = objEmploymentResponse.IsActive;
                    objIndEmployment.IsDeleted = objEmploymentResponse.IsDeleted;
                    objIndEmployment.EmploymentHistoryTypeId = 1;
                    objIndEmployment.IndividualEmploymentGuid = Guid.NewGuid().ToString();
                    objIndEmployment.ApplicationId = applicationId;
                    objIndEmployment.IndividualId = objEmploymentResponse.IndividualId;
                    objIndEmployment.PositionId = objEmploymentResponse.PositionId;
                    objIndEmployment.EmploymentStatusId = objEmploymentResponse.EmploymentStatusId;
                    objIndEmployment.EmploymentTypeId = objEmploymentResponse.EmploymentTypeId;
                    objIndEmployment.ReferenceNumber = "";
                    objIndEmployment.Role = "";
                    objIndEmployment.IsWorkinginFieldofApplication = false;
                    objIndEmployment.EverWorkedinFieldofApplication = false;
                    objIndEmployment.EmployerName = objEmploymentResponse.EmployerName;
                    //objIndEmployment.ModifiedBy = objToken.UserId;
                    //objIndEmployment.ModifiedOn = DateTime.Now;

                    objIndEmployment.IndividualEmploymentId = objIndividualEmploymentBAL.Save_IndividualEmployment(objIndEmployment);

                    //End save IndividualEmployment

                    objEmploymentResponse.IndividualEmploymentId = objIndEmployment.IndividualEmploymentId;


                    //SAVE LOG

                    string logText = "Individual Employment saved successfully. Saved on " + DateTime.Now.ToShortDateString();
                    string logSource = eCommentLogSource.WSAPI.ToString();
                    LogHelper.SaveIndividualLog(individualId, applicationId, logSource, logText, objToken.UserId, null, null, null);

                    //END SAVE LOG

                    objResponse.Message = MessagesClass.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    lstIndividualEmployment.Add(objIndEmployment);

                }




                //SAve EmployeeAddress
                IndividualEmploymentAddressBAL objEmpAddressBAL = new IndividualEmploymentAddressBAL();
                //List<IndividualEmploymentAddress> lstIndividualEmploymentAddress = new List<IndividualEmploymentAddress>();
                //lstIndividualEmploymentAddress = objEmpAddressBAL.Get_IndividualEmploymentAddress_By_IndividualEmploymentId(objEmploymentResponse.IndividualEmploymentId);
                //if (lstIndividualEmploymentAddress != null && lstIndividualEmploymentAddress.Count > 0)
                //{


                //}

                List<IndividualEmploymentAddress> lstEmpAddress = objEmploymentResponse.EmploymentAddress;
                List<IndividualEmploymentAddress> lstEmpAddressNew = new List<IndividualEmploymentAddress>();
                if (lstEmpAddress != null && lstEmpAddress.Count > 0)
                {
                    AddressBAL objAddressBAL = new AddressBAL();
                    Address objAddress = new Address();
                    foreach (IndividualEmploymentAddress objAddressResponse in lstEmpAddress)
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
                                objAddress.BadAddress = objAddressResponse.BadAddress;
                                objAddress.IsActive = objAddressResponse.IsActive;
                                objAddress.IsDeleted = objEmploymentResponse.IsDeleted;
                                objAddress.ModifiedBy = objToken.UserId;
                                objAddress.ModifiedOn = DateTime.Now;


                                objAddressBAL.Save_address(objAddress);



                                IndividualEmploymentAddress objIndAddress = new IndividualEmploymentAddress();
                                objIndAddress = objEmpAddressBAL.Get_IndividualEmploymentAddress_By_IndividualEmploymentAddressId(objAddressResponse.IndividualEmploymentAddressId);
                                if (objIndAddress != null)
                                {
                                    objIndAddress.IndividualEmploymentAddressId = objAddressResponse.IndividualEmploymentAddressId;
                                    objIndAddress.AddressId = objAddress.AddressId;
                                    objIndAddress.AddressTypeId = objAddressResponse.AddressTypeId;
                                    objIndAddress.IsMailingSameasPhysical = objAddressResponse.IsMailingSameasPhysical;
                                    objIndAddress.ModifiedBy = objToken.UserId;
                                    objIndAddress.ModifiedOn = DateTime.Now;
                                    objIndAddress.IsActive = objAddressResponse.IsActive;
                                    objIndAddress.IsDeleted = objEmploymentResponse.IsDeleted;
                                    objIndAddress.IndividualId = objEmploymentResponse.IndividualId;
                                    objIndAddress.IndividualEmploymentAddressGuid = Guid.NewGuid().ToString();
                                    objIndAddress.IndividualEmploymentId = objEmploymentResponse.IndividualEmploymentId;

                                    objIndAddress.IndividualEmploymentAddressId = objEmpAddressBAL.Save_IndividualEmploymentAddress(objIndAddress);
                                }

                                lstEmpAddressNew.Add(objIndAddress);
                            }

                        }
                        else
                        {
                            objAddress = new Address();
                            objAddress.Addressee = "";
                            objAddress.AddressGuid = Guid.NewGuid().ToString();
                            objAddress.Authenticator = Guid.NewGuid().ToString();
                            objAddress.CountryId = 235;

                            objAddress.IsActive = objAddressResponse.IsActive;
                            objAddress.IsDeleted = objAddressResponse.IsDeleted;
                            objAddress.UseUserAddress = false;
                            objAddress.UseVerifiedAddress = false;

                            objAddress.City = objAddressResponse.City;
                            objAddress.StreetLine1 = objAddressResponse.StreetLine1;
                            objAddress.StreetLine2 = objAddressResponse.StreetLine2;
                            objAddress.StateCode = objAddressResponse.StateCode;
                            objAddress.Zip = objAddressResponse.Zip;
                            objAddress.BadAddress = objAddressResponse.BadAddress;
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
                            objIndAddress.IndividualId = objEmploymentResponse.IndividualId;
                            objIndAddress.IndividualEmploymentAddressGuid = Guid.NewGuid().ToString();
                            objIndAddress.IsActive = objAddressResponse.IsActive;
                            objIndAddress.IsDeleted = objEmploymentResponse.IsDeleted;
                            objIndAddress.BeginDate = DateTime.Now;
                            objIndAddress.IndividualEmploymentId = objEmploymentResponse.IndividualEmploymentId;
                            objIndAddress.IndividualEmploymentAddressId = objIndAddressBAL.Save_IndividualEmploymentAddress(objIndAddress);

                            lstEmpAddressNew.Add(objIndAddress);
                        }
                    }
                }


                // Save/Update Employement Contact

                List<IndividualEmploymentContact> lstEmpContactResponse = objEmploymentResponse.EmploymentContact;

                List<IndividualEmploymentContact> lstEmpContactNewResponse = new List<IndividualEmploymentContact>();

                if (lstEmpContactResponse != null && lstEmpContactResponse.Count > 0)
                {
                    ContactBAL objContactBAL = new ContactBAL();
                    Contact objContact = new Contact();
                    foreach (IndividualEmploymentContact objContactResponse in lstEmpContactResponse)
                    {
                        if (objContactResponse.ContactId > 0)
                        {
                            objContact = new Contact();
                            objContact = objContactBAL.Get_Contact_By_ContactId(objContactResponse.ContactId);
                            if (objContact != null)
                            {
                                objContact.Code = objContactResponse.Code;
                                objContact.IsDeleted = objContactResponse.IsDeleted;
                                objContact.IsActive = objContactResponse.IsActive;
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
                                    objIndContact.IsDeleted = objEmploymentResponse.IsDeleted;
                                    objIndContact.IsActive = objContactResponse.IsActive;
                                    objIndContact.ModifiedBy = objToken.UserId;
                                    objIndContact.ModifiedOn = DateTime.Now;
                                    objIndContact.IndividualId = objEmploymentResponse.IndividualId;
                                    objIndContact.IndividualEmploymentContactGuid = Guid.NewGuid().ToString();
                                    objIndContact.IndividualEmploymentId = objEmploymentResponse.IndividualEmploymentId;
                                    objIndContact.IndividualEmploymentId = objIndContactBAL.Save_IndividualEmploymentContact(objIndContact);
                                }

                                lstEmpContactNewResponse.Add(objIndContact);
                            }

                        }
                        else
                        {
                            objContact = new Contact();

                            objContact.ContactGuid = Guid.NewGuid().ToString();
                            objContact.Authenticator = Guid.NewGuid().ToString();

                            objContact.IsDeleted = objContactResponse.IsDeleted;
                            objContact.IsActive = objContactResponse.IsActive;
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

                            objIndContact.IndividualEmploymentId = objEmploymentResponse.IndividualEmploymentId;
                            objIndContact.IndividualId = objEmploymentResponse.IndividualId;
                            objIndContact.IndividualEmploymentContactGuid = Guid.NewGuid().ToString();
                            objIndContact.IndividualEmploymentContactId = objContactResponse.IndividualEmploymentContactId;
                            objIndContact.ContactId = objContact.ContactId;
                            objIndContact.ContactTypeId = objContactResponse.ContactTypeId;
                            objIndContact.IsPreferredContact = objContactResponse.IsPreferredContact;
                            objIndContact.IsMobile = objContactResponse.IsMobile;
                            objIndContact.IsDeleted = objEmploymentResponse.IsDeleted;
                            objIndContact.IsActive = objContactResponse.IsActive;
                            objIndContact.BeginDate = DateTime.Now;
                            objIndContact.ModifiedBy = objToken.UserId;
                            objIndContact.ModifiedOn = DateTime.Now;
                            objIndContact.IndividualEmploymentId = objIndContactBAL.Save_IndividualEmploymentContact(objIndContact);

                            lstEmpContactNewResponse.Add(objIndContact);

                        }
                    }
                }

                objEmploymentResponse.EmploymentContact = lstEmpContactNewResponse;
                objEmploymentResponse.EmploymentAddress = lstEmpAddressNew;


                #region Response

                if (lstIndividualEmployment != null && lstIndividualEmployment.Count > 0)
                {
                    lstEmploymentResponse = lstIndividualEmployment.Select(obj => new IndividualEmploymentResponse
                    {
                        ApplicationId = obj.ApplicationId,
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
                        EmployerName = obj.EmployerName,
                        EmploymentAddress = lstEmpAddressNew,
                        EmploymentContact = lstEmpContactNewResponse

                    }).ToList();
                }

                #endregion

                objResponse.IndividualEmploymentResponseList = lstEmploymentResponse;


            }
            catch (Exception ex)
            {

                LogingHelper.SaveExceptionInfo("", ex, "SaveIndividualEmployment", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualEmploymentResponseList = null;
            }
            return objResponse;
        }

    }
}
