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
    public class IndividualAddressCS
    {
        public static IndividualAddressResponseRequest SaveIndividualAddress(Token objToken, IndividualAddressResponse objAddressResponse)
        {
            IndividualAddressResponseRequest objResponse = new IndividualAddressResponseRequest();
            AddressBAL objAddressBAL = new AddressBAL();
            Address objAddress = new Address();
            IndividualAddress objIndAddress = new IndividualAddress();
            IndividualAddressBAL objIndAddressBAL = new IndividualAddressBAL();
            List<IndividualAddress> lstIndividualAddress = new List<IndividualAddress>();

            int individualID = objAddressResponse.IndividualId;
            int? applicantID = null;
            try
            {
                if (objAddressResponse.IndividualAddressId > 0)
                {
                    // Update IndividualAddress
                    objIndAddress = new IndividualAddress();
                    objIndAddress = objIndAddressBAL.Get_IndividualAddress_By_IndividualAddressId(objAddressResponse.IndividualAddressId);
                    if (objIndAddress != null)
                    {
                        objIndAddress.IndividualAddressId = objAddressResponse.IndividualAddressId;
                        objIndAddress.AddressId = objAddressResponse.AddressId;
                        objIndAddress.AddressTypeId = objAddressResponse.AddressTypeId;
                        objIndAddress.IsMailingSameasPhysical = objAddressResponse.IsMailingSameasPhysical;
                        objIndAddress.ModifiedBy = objToken.UserId;
                        objIndAddress.ModifiedOn = DateTime.Now;
                        objIndAddress.IndividualId = individualID;
                        objIndAddress.IndividualAddressGuid = Guid.NewGuid().ToString();
                        objIndAddress.IsActive = objAddressResponse.IsActive;
                        objIndAddress.EndDate = objAddressResponse.EndDate;
                        objIndAddress.BeginDate = objAddressResponse.BeginDate;
                        objIndAddress.IsDeleted = objAddressResponse.IsDeleted;
                        objIndAddress.AdressStatusId = objAddressResponse.AdressStatusId;
                        objIndAddress.UseVerifiedAddress = objAddressResponse.UseVerifiedAddress;
                        objIndAddress.UseUserAddress = objAddressResponse.UseUserAddress;
                        objIndAddressBAL.Save_IndividualAddress(objIndAddress);

                        //End  Update IndividualAddress

                        objAddress = new Address();
                        objAddress = objAddressBAL.Get_address_By_AddressId(objIndAddress.AddressId);
                        if (objAddress != null)
                        {
                            // Update Address

                            objAddress.Addressee = String.IsNullOrEmpty(objAddressResponse.Addressee) ? "" : objAddressResponse.Addressee;
                            objAddress.City = objAddressResponse.City;
                            objAddress.StreetLine1 = objAddressResponse.StreetLine1;
                            objAddress.StreetLine2 = objAddressResponse.StreetLine2;
                            objAddress.StateCode = objAddressResponse.StateCode;
                            objAddress.Zip = objAddressResponse.Zip;
                            objAddress.BadAddress = objAddressResponse.BadAddress;

                            objAddress.ModifiedBy = objToken.UserId;
                            objAddress.ModifiedOn = DateTime.Now;
                            objAddress.CountryId = 235;

                            objAddress.IsActive = objAddressResponse.IsActive;
                            objAddress.IsDeleted = objAddressResponse.IsDeleted;
                            objAddress.UseUserAddress = false;
                            objAddress.UseVerifiedAddress = false;

                            objAddressBAL.Save_address(objAddress);

                            //End Update Address

                            objResponse.Message = MessagesClass.UpdateSuccess;
                            objResponse.Status = true;
                            objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                            string logText = "Individual Address updated successfully. Updated on " + DateTime.Now.ToShortDateString();
                            string logSource = eCommentLogSource.WSAPI.ToString();
                            LogHelper.SaveIndividualLog(individualID, applicantID, logSource, logText, objToken.UserId, null, null, null);
                        }
                    }
                }
                else
                {

                    // Save Address

                    objAddress = new Address();

                    objAddress.Addressee = String.IsNullOrEmpty(objAddressResponse.Addressee) ? "" : objAddressResponse.Addressee;
                    objAddress.AddressGuid = Guid.NewGuid().ToString();
                    objAddress.Authenticator = Guid.NewGuid().ToString();
                    objAddress.CountryId = 235;

                    objAddress.IsActive = objAddressResponse.IsActive;
                    objAddress.IsDeleted = objAddressResponse.IsDeleted;

                    objAddress.UseUserAddress = false;
                    objAddress.UseVerifiedAddress = false;
                    objAddress.BadAddress = objAddressResponse.BadAddress;
                    objAddress.City = objAddressResponse.City;
                    objAddress.StreetLine1 = objAddressResponse.StreetLine1;
                    objAddress.StreetLine2 = objAddressResponse.StreetLine2;
                    objAddress.StateCode = objAddressResponse.StateCode;
                    objAddress.Zip = objAddressResponse.Zip;

                    objAddress.CreatedBy = objToken.UserId;
                    objAddress.CreatedOn = DateTime.Now;

                    objAddress.AddressId = objAddressBAL.Save_address(objAddress);

                    // End Save Address

                    // Save IndividualAddress

                    objIndAddress = new IndividualAddress();

                    objIndAddress.AddressId = objAddress.AddressId;
                    objIndAddress.AddressTypeId = objAddressResponse.AddressTypeId;
                    objIndAddress.IsMailingSameasPhysical = objAddressResponse.IsMailingSameasPhysical;
                    objIndAddress.CreatedBy = objToken.UserId;
                    objIndAddress.CreatedOn = DateTime.Now;
                    objIndAddress.IndividualId = individualID;
                    objIndAddress.IndividualAddressGuid = Guid.NewGuid().ToString();
                    objIndAddress.IsActive = objAddressResponse.IsActive;
                    objIndAddress.IsDeleted = objAddressResponse.IsDeleted;
                    objIndAddress.BeginDate = objAddressResponse.BeginDate;
                    objIndAddress.EndDate = objAddressResponse.EndDate;
                    objIndAddress.AdressStatusId = objAddressResponse.AdressStatusId;
                    objIndAddressBAL.Save_IndividualAddress(objIndAddress);

                    // End Save IndividualAddress

                    //SAVE LOG

                    string logText = "Individual Address saved successfully. Saved on " + DateTime.Now.ToShortDateString();
                    string logSource = eCommentLogSource.WSAPI.ToString();
                    LogHelper.SaveIndividualLog(individualID, applicantID, logSource, logText, objToken.UserId, null, null, null);

                    //END SAVE LOG

                    objResponse.Message = MessagesClass.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }


                #region AddressResponse

                List<IndividualAddressResponse> lstAddressResponse = new List<IndividualAddressResponse>();
                lstIndividualAddress = objIndAddressBAL.Get_IndividualAddress_By_IndividualId(individualID);
                if (lstIndividualAddress != null)
                {
                    lstAddressResponse = lstIndividualAddress
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
                           AdressStatusId = obj.AdressStatusId,
                           IsActive = obj.IsActive,
                           IsMailingSameasPhysical = obj.IsMailingSameasPhysical,
                           StateCode = obj.StateCode,
                           StreetLine1 = obj.StreetLine1,
                           StreetLine2 = obj.StreetLine2,
                           Zip = obj.Zip

                       }).ToList();
                }
                else
                {
                    lstAddressResponse = new List<IndividualAddressResponse>();
                }


                #endregion

                objResponse.Status = true;

                objResponse.IndividualAddressResponse = lstAddressResponse;

            }
            catch (Exception ex)
            {

                LogingHelper.SaveExceptionInfo("", ex, "SaveIndividualAddress", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualAddressResponse = null;
            }
            return objResponse;


        }

    }
}