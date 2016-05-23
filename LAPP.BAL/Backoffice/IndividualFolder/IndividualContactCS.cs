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
    public class IndividualContactCS
    {
        public static IndividualContactResponseRequest SaveIndividualContact(Token objToken, IndividualContactResponse objContactResponse)
        {
            IndividualContactResponseRequest objResponse = new IndividualContactResponseRequest();
            IndividualContactBAL objIndividualContactBAL = new IndividualContactBAL();
            IndividualContactResponse objIndividualContactResponse = new IndividualContactResponse();
            IndividualContact objIndividualContact = new IndividualContact();
            List<IndividualContact> lstIndividualContact = new List<IndividualContact>();
            List<IndividualContactResponse> lstIndividualContactResponse = new List<IndividualContactResponse>();
            Contact objContact = new Contact();
            ContactBAL objContactBAL = new ContactBAL();

            int IndividualId = objContactResponse.IndividualId;
            try
            {

                objIndividualContact = objIndividualContactBAL.Get_IndividualContact_By_IndividualContactId(objContactResponse.IndividualContactId);
                if (objIndividualContact != null && objIndividualContact.ContactId > 0)
                {
                    // Update Contact

                    objContact = new Contact();
                    objContact = objContactBAL.Get_Contact_By_ContactId(objContactResponse.ContactId);
                    if (objContact != null)
                    {
                        objContact.Code = objContactResponse.Code;
                        objContact.ContactFirstName = objContactResponse.ContactFirstName;
                        objContact.ContactLastName = objContactResponse.ContactLastName;
                        objContact.ContactMiddleName = objContactResponse.ContactMiddleName;
                        objContact.ContactTypeId = objContactResponse.ContactTypeId;
                        objContact.ContactInfo = objContactResponse.ContactInfo;
                        objContact.ModifiedBy = objToken.UserId;
                        objContact.ModifiedOn = DateTime.Now;

                        objContactBAL.Save_Contact(objContact);

                        //END Update Contact

                        // Update IndividualContact
                        objIndividualContact = new IndividualContact();
                        objIndividualContact = objIndividualContactBAL.Get_IndividualContact_By_IndividualContactId(objContactResponse.IndividualContactId);
                        if (objIndividualContact != null)
                        {
                            objIndividualContact.IndividualContactId = objContactResponse.IndividualContactId;
                            objIndividualContact.ContactId = objContact.ContactId;
                            objIndividualContact.ContactTypeId = objContactResponse.ContactTypeId;
                            objIndividualContact.IsPreferredContact = objContactResponse.IsPreferredContact;
                            objIndividualContact.IsMobile = objContactResponse.IsMobile;

                            objIndividualContact.ModifiedBy = objToken.UserId;
                            objIndividualContact.ModifiedOn = DateTime.Now;
                            objIndividualContact.IndividualId = IndividualId;
                            //objIndividualContact.IndividualContactGuid = Guid.NewGuid().ToString();

                            objIndividualContactBAL.Save_IndividualContact(objIndividualContact);
                        }
                        //END Update IndividualContact

                    }
                    objResponse.Message = MessagesClass.UpdateSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }
                else
                {

                    // save Contact
                    objContact = new Contact();

                    objContact.ContactGuid = Guid.NewGuid().ToString();
                    objContact.Authenticator = Guid.NewGuid().ToString();
                    objContact.IsActive = true;
                    objContact.IsDeleted = false;
                    objContact.Code = objContactResponse.Code;
                    objContact.ContactFirstName = objContactResponse.ContactFirstName;
                    objContact.ContactLastName = objContactResponse.ContactLastName;
                    objContact.ContactMiddleName = objContactResponse.ContactMiddleName;
                    objContact.ContactTypeId = objContactResponse.ContactTypeId;
                    objContact.ContactInfo = objContactResponse.ContactInfo;
                    objContact.CreatedBy = objToken.UserId;
                    objContact.CreatedOn = DateTime.Now;

                    objContact.ContactId = objContactBAL.Save_Contact(objContact);

                    //END  save Contact

                    // save IndividualContact
                    objIndividualContact = new IndividualContact();
                    objIndividualContact.IndividualContactGuid = Guid.NewGuid().ToString();
                    objIndividualContact.ContactId = objContact.ContactId;
                    objIndividualContact.ContactTypeId = objContactResponse.ContactTypeId;
                    objIndividualContact.IsPreferredContact = objContactResponse.IsPreferredContact;
                    objIndividualContact.IsMobile = objContactResponse.IsMobile;
                    objIndividualContact.IsActive = true;
                    objIndividualContact.IsDeleted = false;
                    objIndividualContact.BeginDate = DateTime.Now;
                    objIndividualContact.CreatedBy = objToken.UserId;
                    objIndividualContact.CreatedOn = DateTime.Now;
                    objIndividualContact.IndividualId = IndividualId;

                    objIndividualContact.IndividualContactId = objIndividualContactBAL.Save_IndividualContact(objIndividualContact);

                    //END save IndividualContact
                    objResponse.Message = MessagesClass.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }

                #region Response

                // Response
                objIndividualContact.Code = objContact.Code;
                objIndividualContact.ContactFirstName = objContact.ContactFirstName;
                objIndividualContact.ContactInfo = objContact.ContactInfo;
                objIndividualContact.ContactLastName = objContact.ContactLastName;
                objIndividualContact.ContactMiddleName = objContact.ContactMiddleName;
                objIndividualContact.DateContactValidated = objContact.DateContactValidated;
                //

                lstIndividualContact.Add(objIndividualContact);

                if (lstIndividualContact != null && lstIndividualContact.Count > 0)
                {
                    lstIndividualContactResponse = lstIndividualContact
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
                }

                #endregion

                objResponse.Status = true;
                objResponse.IndividualContactResponse = lstIndividualContactResponse;
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo("", ex, "SaveIndividualEducation", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualContactResponse = null;
            }
            return objResponse;
        }
    }
}
