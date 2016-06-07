using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.LOGING;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.BAL.Common
{
    public class ProviderCS 
    {

        public static ProviderResponseRequest ProviderSave(Token objToken, ProviderResponse objProviderResponse)
        {
            ProviderBAL objProviderBAL = new ProviderBAL();
            Provider objProvider = new Provider();
            ProviderResponseRequest objResponse = new ProviderResponseRequest();
            Provider objIndEmployment = new Provider();
            List<Provider> lstProvider = new List<Provider>();
            List<ProviderResponse> lstEmploymentResponse = new List<ProviderResponse>();
            try
            {
                //int individualId = objProviderResponse.IndividualId;
                //int? applicationId = objProviderResponse.ApplicationId;

                objIndEmployment = objProviderBAL.Get_Provider_By_ProviderId(objProviderResponse.ProviderId);
                if (objIndEmployment != null)
                {
                    // Update Provider
                    objProvider = objProviderBAL.Get_Provider_By_ProviderId(objProviderResponse.ProviderId);
                    if (objProvider != null)
                    {
                        objProvider.ModifiedBy = objToken.UserId;
                        objProvider.ModifiedOn = DateTime.Now;
                        objProvider.ProviderName = objProviderResponse.ProviderName;

                        objProviderBAL.Save_Provider(objProvider);
                    }

                    //End Update Provider

                    ////SAVE LOG

                    //string logText = "Individual Employment updated successfully. Updated on " + DateTime.Now.ToShortDateString();
                    //string logSource = eCommentLogSource.WSAPI.ToString();
                    //LogHelper.SaveIndividualLog(individualId, applicationId, logSource, logText, objToken.UserId, null, null, null);

                    ////END SAVE LOG


                    objResponse.Message = MessagesClass.UpdateSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    lstProvider.Add(objIndEmployment);

                }

                else
                {
                    // Save Provider
                    objProvider = new Provider();

                    objProvider.BillingNumber = "";

                    objProvider.DepartmentId = 2;
                    objProvider.CreatedBy = objToken.UserId;
                    objProvider.CreatedOn = DateTime.Now;
                    objProvider.IsActive = true;
                    objProvider.IsDeleted = false;
                    objProvider.IsEnabled = true;
                    objProvider.LicenseNumber = "";
                    objProvider.OwnershipCompany = "";
                    objProvider.ProviderDBAName = "";
                    objProvider.ProviderGuid = Guid.NewGuid().ToString();

                    objProvider.ProviderName = objProviderResponse.ProviderName;
                    objProvider.ProviderNumber = "";
                    objProvider.ProviderStatusTypeId = 1;
                    objProvider.ProviderTypeId = 5;
                    objProvider.ReferenceNumber = "";
                    objProvider.TaxId = "";

                    objProvider.ProviderId = objProviderBAL.Save_Provider(objProvider);

                    //End Save provider

                    
                    ////SAVE LOG

                    //string logText = "Individual Employment saved successfully. Saved on " + DateTime.Now.ToShortDateString();
                    //string logSource = eCommentLogSource.WSAPI.ToString();
                    //LogHelper.SaveIndividualLog(individualId, applicationId, logSource, logText, objToken.UserId, null, null, null);

                    ////END SAVE LOG

                    objResponse.Message = MessagesClass.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    lstProvider.Add(objIndEmployment);

                }




                ////SAve EmployeeAddress

                //List<ProviderAddressResponse> lstEmpAddress = objProviderResponse.EmploymentAddress;
                //List<ProviderAddressResponse> lstEmpAddressNew = new List<ProviderAddressResponse>();
                //if (lstEmpAddress != null && lstEmpAddress.Count > 0)
                //{
                //    AddressBAL objAddressBAL = new AddressBAL();
                //    Address objAddress = new Address();
                //    foreach (ProviderAddressResponse objAddressResponse in lstEmpAddress)
                //    {
                //        if (objAddressResponse.AddressId > 0)
                //        {
                //            objAddress = new Address();
                //            objAddress = objAddressBAL.Get_address_By_AddressId(objAddressResponse.AddressId);
                //            if (objAddress != null)
                //            {
                //                objAddress.City = objAddressResponse.City;
                //                objAddress.StreetLine1 = objAddressResponse.StreetLine1;
                //                objAddress.StreetLine2 = objAddressResponse.StreetLine2;
                //                objAddress.StateCode = objAddressResponse.StateCode;
                //                objAddress.Zip = objAddressResponse.Zip;

                //                objAddress.ModifiedBy = objToken.UserId;
                //                objAddress.ModifiedOn = DateTime.Now;


                //                objAddressBAL.Save_address(objAddress);



                //                ProviderAddress objIndAddress = new ProviderAddress();
                //                ProviderAddressBAL objIndAddressBAL = new ProviderAddressBAL();
                //                objIndAddress = objIndAddressBAL.Get_ProviderAddress_By_ProviderAddressId(objAddressResponse.ProviderAddressId);
                //                if (objIndAddress != null)
                //                {
                //                    objIndAddress.ProviderAddressId = objAddressResponse.ProviderAddressId;
                //                    objIndAddress.AddressId = objAddress.AddressId;
                //                    objIndAddress.AddressTypeId = objAddressResponse.AddressTypeId;
                //                    objIndAddress.IsMailingSameasPhysical = objAddressResponse.IsMailingSameasPhysical;
                //                    objIndAddress.ModifiedBy = objToken.UserId;
                //                    objIndAddress.ModifiedOn = DateTime.Now;
                //                    objIndAddress.IndividualId = objProviderResponse.IndividualId;
                //                    objIndAddress.ProviderAddressGuid = Guid.NewGuid().ToString();
                //                    objIndAddress.ProviderId = objProviderResponse.ProviderId;

                //                    objIndAddress.ProviderAddressId = objIndAddressBAL.Save_ProviderAddress(objIndAddress);
                //                }

                //                lstEmpAddressNew.Add(objIndAddress);
                //            }

                //        }
                //        else
                //        {
                //            objAddress = new Address();
                //            objAddress.Addressee = "";
                //            objAddress.AddressGuid = Guid.NewGuid().ToString();
                //            objAddress.Authenticator = Guid.NewGuid().ToString();
                //            objAddress.CountryId = 235;

                //            objAddress.IsActive = true;
                //            objAddress.IsDeleted = false;
                //            objAddress.UseUserAddress = false;
                //            objAddress.UseVerifiedAddress = false;

                //            objAddress.City = objAddressResponse.City;
                //            objAddress.StreetLine1 = objAddressResponse.StreetLine1;
                //            objAddress.StreetLine2 = objAddressResponse.StreetLine2;
                //            objAddress.StateCode = objAddressResponse.StateCode;
                //            objAddress.Zip = objAddressResponse.Zip;

                //            objAddress.CreatedBy = objToken.UserId;
                //            objAddress.CreatedOn = DateTime.Now;


                //            objAddress.AddressId = objAddressBAL.Save_address(objAddress);


                //            ProviderAddress objIndAddress = new ProviderAddress();
                //            ProviderAddressBAL objIndAddressBAL = new ProviderAddressBAL();

                //            objIndAddress.AddressId = objAddress.AddressId;
                //            objIndAddress.AddressTypeId = objAddressResponse.AddressTypeId;
                //            objIndAddress.IsMailingSameasPhysical = objAddressResponse.IsMailingSameasPhysical;
                //            objIndAddress.CreatedBy = objToken.UserId;
                //            objIndAddress.CreatedOn = DateTime.Now;
                //            objIndAddress.IndividualId = objProviderResponse.IndividualId;
                //            objIndAddress.ProviderAddressGuid = Guid.NewGuid().ToString();
                //            objIndAddress.IsActive = true;
                //            objIndAddress.BeginDate = DateTime.Now;
                //            objIndAddress.ProviderId = objProviderResponse.ProviderId;
                //            objIndAddress.ProviderAddressId = objIndAddressBAL.Save_ProviderAddress(objIndAddress);

                //            lstEmpAddressNew.Add(objIndAddress);
                //        }
                //    }
                //}


                //// Save/Update Employement Contact

                //List<ProviderContactResponse> lstEmpContactResponse = objProviderResponse.EmploymentContact;

                //List<ProviderContactResponse> lstEmpContactNewResponse = new List<ProviderContactResponse>();

                //if (lstEmpContactResponse != null && lstEmpContactResponse.Count > 0)
                //{
                //    ContactBAL objContactBAL = new ContactBAL();
                //    Contact objContact = new Contact();
                //    foreach (ProviderContactResponse objContactResponse in lstEmpContactResponse)
                //    {
                //        if (objContactResponse.ContactId > 0)
                //        {
                //            objContact = new Contact();
                //            objContact = objContactBAL.Get_Contact_By_ContactId(objContactResponse.ContactId);
                //            if (objContact != null)
                //            {
                //                objContact.Code = objContactResponse.Code;
                //                objContact.ContactFirstName = "";
                //                objContact.ContactLastName = "";
                //                objContact.ContactMiddleName = "";
                //                objContact.ContactTypeId = objContactResponse.ContactTypeId;
                //                objContact.ContactInfo = objContactResponse.ContactInfo;
                //                objContact.ModifiedBy = objToken.UserId;
                //                objContact.ModifiedOn = DateTime.Now;

                //                objContactBAL.Save_Contact(objContact);



                //                ProviderContact objIndContact = new ProviderContact();
                //                ProviderContactBAL objIndContactBAL = new ProviderContactBAL();

                //                objIndContact = objIndContactBAL.Get_ProviderContact_By_ProviderContactId(objContactResponse.ProviderContactId);
                //                if (objIndContact != null)
                //                {
                //                    objIndContact.ProviderContactId = objContactResponse.ProviderContactId;
                //                    objIndContact.ContactId = objContact.ContactId;
                //                    objIndContact.ContactTypeId = objContactResponse.ContactTypeId;
                //                    objIndContact.IsPreferredContact = objContactResponse.IsPreferredContact;
                //                    objIndContact.IsMobile = objContactResponse.IsMobile;

                //                    objIndContact.ModifiedBy = objToken.UserId;
                //                    objIndContact.ModifiedOn = DateTime.Now;
                //                    objIndContact.IndividualId = objProviderResponse.IndividualId;
                //                    objIndContact.ProviderContactGuid = Guid.NewGuid().ToString();
                //                    objIndContact.ProviderId = objProviderResponse.ProviderId;
                //                    objIndContact.ProviderId = objIndContactBAL.Save_ProviderContact(objIndContact);
                //                }

                //                lstEmpContactNewResponse.Add(objIndContact);
                //            }

                //        }
                //        else
                //        {
                //            objContact = new Contact();

                //            objContact.ContactGuid = Guid.NewGuid().ToString();
                //            objContact.Authenticator = Guid.NewGuid().ToString();

                //            objContact.IsActive = true;
                //            objContact.IsDeleted = false;
                //            objContact.Code = objContactResponse.Code;
                //            objContact.ContactFirstName = "";
                //            objContact.ContactLastName = "";
                //            objContact.ContactMiddleName = "";
                //            objContact.ContactTypeId = objContactResponse.ContactTypeId;
                //            objContact.ContactInfo = objContactResponse.ContactInfo;
                //            objContact.CreatedBy = objToken.UserId;
                //            objContact.CreatedOn = DateTime.Now;

                //            objContact.ContactId = objContactBAL.Save_Contact(objContact);

                //            ProviderContact objIndContact = new ProviderContact();
                //            ProviderContactBAL objIndContactBAL = new ProviderContactBAL();

                //            objIndContact.ProviderId = objProviderResponse.ProviderId;
                //            objIndContact.IndividualId = objProviderResponse.IndividualId;
                //            objIndContact.ProviderContactGuid = Guid.NewGuid().ToString();
                //            objIndContact.ProviderContactId = objContactResponse.ProviderContactId;
                //            objIndContact.ContactId = objContact.ContactId;
                //            objIndContact.ContactTypeId = objContactResponse.ContactTypeId;
                //            objIndContact.IsPreferredContact = objContactResponse.IsPreferredContact;
                //            objIndContact.IsMobile = objContactResponse.IsMobile;
                //            objIndContact.IsActive = true;
                //            objIndContact.BeginDate = DateTime.Now;
                //            objIndContact.ModifiedBy = objToken.UserId;
                //            objIndContact.ModifiedOn = DateTime.Now;
                //            objIndContact.ProviderId = objIndContactBAL.Save_ProviderContact(objIndContact);

                //            lstEmpContactNewResponse.Add(objIndContact);

                //        }
                //    }
                //}

                //objProviderResponse.EmploymentContact = lstEmpContactNewResponse;
                //objProviderResponse.EmploymentAddress = lstEmpAddressNew;


                //#region Response

                //if (lstProvider != null && lstProvider.Count > 0)
                //{
                //    lstEmploymentResponse = lstProvider.Select(obj => new ProviderResponse
                //    {
                //        ApplicationId = obj.ApplicationId,
                //        EmploymentEndDate = obj.EmploymentEndDate,
                //        EmploymentHistoryTypeId = obj.EmploymentHistoryTypeId,

                //        EmploymentStartDate = obj.EmploymentStartDate,
                //        EmploymentStatusId = obj.EmploymentStatusId,
                //        EmploymentTypeId = obj.EmploymentTypeId,
                //        EverWorkedinFieldofApplication = obj.EverWorkedinFieldofApplication,
                //        ProviderId = obj.ProviderId,
                //        IndividualId = obj.IndividualId,
                //        IsActive = obj.IsActive,
                //        IsWorkinginFieldofApplication = obj.IsWorkinginFieldofApplication,
                //        PositionId = obj.PositionId,
                //        ProviderId = obj.ProviderId,
                //        ReferenceNumber = obj.ReferenceNumber,
                //        Role = obj.Role,
                //        EmployerName = obj.EmployerName,
                //        EmploymentAddress = lstEmpAddressNew,
                //        EmploymentContact = lstEmpContactNewResponse

                //    }).ToList();
                //}

                //#endregion

                objResponse.ProviderResponseList = lstEmploymentResponse;


            }
            catch (Exception ex)
            {

                LogingHelper.SaveExceptionInfo("", ex, "SaveProvider", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.ProviderResponseList = null;
            }
            return objResponse;
        }


    }
}
