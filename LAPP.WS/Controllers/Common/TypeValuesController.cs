using LAPP.BAL;
using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.LOGING;
using LAPP.WS.App_Helper;
using LAPP.WS.App_Helper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LAPP.WS.Controllers.Common
{
    public class TypeValuesController : ApiController
    {

        #region AddressType

        /// <summary>
        /// Looks up all data by Key For AddressType.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("GET")]
        [ActionName("AddressTypeGetAll")]
        public AddressTypeGetResponse AddressTypeGetAll(string Key)
        {
            LogingHelper.SaveAuditInfo(Key);

            AddressTypeGetResponse objResponse = new AddressTypeGetResponse();
            AddressTypeBAL objBAL = new AddressTypeBAL();
            AddressType objEntity = new AddressType();
            List<AddressType> lstAddressType = new List<AddressType>();
            try
            {
                //if (!TokenHelper.ValidateToken(Key))
                //{
                //    objResponse.Status = false;
                //    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                //    objResponse.Message = "User session has expired.";
                //    objResponse.AddressTypeGetList = null;
                //    return objResponse;

                //}

                lstAddressType = objBAL.Get_All_AddressType();
                if (lstAddressType != null && lstAddressType.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    List<AddressTypeGet> lstAddressTySelected = lstAddressType.Select(AddressTy => new AddressTypeGet
                    {
                        AddressTypeId = AddressTy.AddressTypeId,
                        AddressTypeCode = AddressTy.AddressTypeCode,
                        AddressTypeDesc = AddressTy.AddressTypeDesc,
                        IsActive = AddressTy.IsActive
                    }).ToList();

                    objResponse.AddressTypeGetList = lstAddressTySelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.AddressTypeGetList = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "AddressTypeGetAll", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.AddressTypeGetList = null;
            }
            return objResponse;


        }

        #endregion

        #region ContactType

        /// <summary>
        /// Looks up all data by Key For ContactType.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("GET")]
        [ActionName("ContactTypeGetAll")]
        public ContactTypeGetResponse ContactTypeGetAll(string Key)
        {
            LogingHelper.SaveAuditInfo(Key);

            ContactTypeGetResponse objResponse = new ContactTypeGetResponse();
            ContactTypeBAL objBAL = new ContactTypeBAL();
            ContactType objEntity = new ContactType();
            List<ContactType> lstContactType = new List<ContactType>();
            try
            {
                //if (!TokenHelper.ValidateToken(Key))
                //{
                //    objResponse.Status = false;
                //    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                //    objResponse.Message = "User session has expired.";
                //    objResponse.ContactTypeGetList = null;
                //    return objResponse;

                //}

                lstContactType = objBAL.Get_All_Contacttype();
                if (lstContactType != null && lstContactType.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    List<ContactTypeGet> lstContactTypeSelected = lstContactType.Select(ContactType => new ContactTypeGet
                    {
                        ContactTypeId = ContactType.ContactTypeId,
                        Code = ContactType.Code,
                        Desc = ContactType.Desc,
                        IsActive = ContactType.IsActive
                    }).ToList();

                    objResponse.ContactTypeGetList = lstContactTypeSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ContactTypeGetList = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ContactTypeGetAll", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ContactTypeGetList = null;
            }
            return objResponse;


        }

        #endregion

        #region LicenseType

        /// <summary>
        /// Looks up all data by Key For LicenseType.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("GET")]
        [ActionName("LicenseTypeGetAll")]
        public LicenseTypeGetResponse LicenseTypeGetAll(string Key)
        {
            LogingHelper.SaveAuditInfo(Key);

            LicenseTypeGetResponse objResponse = new LicenseTypeGetResponse();
            LicenseTypeBAL objBAL = new LicenseTypeBAL();
            LicenseType objEntity = new LicenseType();
            List<LicenseType> lstLicenseType = new List<LicenseType>();
            try
            {
                //if (!TokenHelper.ValidateToken(Key))
                //{
                //    objResponse.Status = false;
                //    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                //    objResponse.Message = "User session has expired.";
                //    objResponse.LicenseTypeGetList = null;
                //    return objResponse;

                //}

                lstLicenseType = objBAL.Get_All_LicenseType();
                if (lstLicenseType != null && lstLicenseType.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    List<LicenseTypeGet> lstLicenseTypeSelected = lstLicenseType.Select(LicenseType => new LicenseTypeGet
                    {
                        LicenseTypeId = LicenseType.LicenseTypeId,
                        LicenseTypeCode = LicenseType.LicenseTypeCode,
                        LicenseTypeName = LicenseType.LicenseTypeName,
                        IsActive = LicenseType.IsActive
                    }).ToList();

                    objResponse.LicenseTypeGetList = lstLicenseTypeSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.LicenseTypeGetList = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "LicenseTypeGetAll", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.LicenseTypeGetList = null;
            }
            return objResponse;


        }

        #endregion

        #region LicenseStatusType

        /// <summary>
        /// Looks up all data by Key For LicenseStatusType.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("GET")]
        [ActionName("LicenseStatusTypeGetAll")]
        public LicenseStatusTypeGetResponse LicenseStatusTypeGetAll(string Key)
        {
            LogingHelper.SaveAuditInfo(Key);

            LicenseStatusTypeGetResponse objResponse = new LicenseStatusTypeGetResponse();
            LicenseStatusTypeBAL objBAL = new LicenseStatusTypeBAL();
            LicenseStatusType objEntity = new LicenseStatusType();
            List<LicenseStatusType> lstLicenseStatusType = new List<LicenseStatusType>();
            try
            {
                //if (!TokenHelper.ValidateToken(Key))
                //{
                //    objResponse.Status = false;
                //    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                //    objResponse.Message = "User session has expired.";
                //    objResponse.LicenseStatusTypeGetList = null;
                //    return objResponse;

                //}

                lstLicenseStatusType = objBAL.Get_All_LicenseStatusType();
                if (lstLicenseStatusType != null && lstLicenseStatusType.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    List<LicenseStatusTypeGet> lstLicenseStatusTypeSelected = lstLicenseStatusType.Select(LicenseStatusType => new LicenseStatusTypeGet
                    {
                        LicenseStatusTypeId = LicenseStatusType.LicenseStatusTypeId,
                        LicenseStatusTypeCode = LicenseStatusType.LicenseStatusTypeCode,
                        LicenseStatusTypeName = LicenseStatusType.LicenseStatusTypeName,
                        StatusTypeColorCode = LicenseStatusType.StatusTypeColorCode,
                        EffectiveDate = LicenseStatusType.EffectiveDate,
                        EndDate = LicenseStatusType.EndDate,
                        IsActive = LicenseStatusType.IsActive

                    }).ToList();

                    objResponse.LicenseStatusTypeGetList = lstLicenseStatusTypeSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.LicenseStatusTypeGetList = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "LicenseStatusTypeGetAll", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.LicenseStatusTypeGetList = null;
            }
            return objResponse;


        }

        #endregion

        #region ApplicationType

        /// <summary>
        /// Looks up all data by Key For ApplicationType.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("GET")]
        [ActionName("ApplicationTypeGetAll")]
        public ApplicationTypeGetResponse ApplicationTypeGetAll(string Key)
        {
            LogingHelper.SaveAuditInfo(Key);

            ApplicationTypeGetResponse objResponse = new ApplicationTypeGetResponse();
            ApplicationTypeBAL objBAL = new ApplicationTypeBAL();
            ApplicationType objEntity = new ApplicationType();
            List<ApplicationType> lstApplicationType = new List<ApplicationType>();
            try
            {
                //if (!TokenHelper.ValidateToken(Key))
                //{
                //    objResponse.Status = false;
                //    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                //    objResponse.Message = "User session has expired.";
                //    objResponse.ApplicationTypeGetList = null;
                //    return objResponse;

                //}

                lstApplicationType = objBAL.Get_All_ApplicationType();
                if (lstApplicationType != null && lstApplicationType.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    List<ApplicationTypeGet> lstApplicationTypeSelected = lstApplicationType.Select(ApplicationType => new ApplicationTypeGet
                    {
                        ApplicationTypeId = ApplicationType.ApplicationTypeId,
                        Code = ApplicationType.Code,
                        Name = ApplicationType.Name,
                        IsActive = ApplicationType.IsActive
                    }).ToList();

                    objResponse.ApplicationTypeGetList = lstApplicationTypeSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ApplicationTypeGetList = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ApplicationTypeGetAll", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ApplicationTypeGetList = null;
            }
            return objResponse;


        }

        #endregion

        #region ApplicationStatus

        /// <summary>
        /// Looks up all data by Key For Application Status.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("GET")]
        [ActionName("ApplicationStatusGetAll")]
        public ApplicationStatusGetResponse ApplicationStatusGetAll(string Key)
        {
            LogingHelper.SaveAuditInfo(Key);

            ApplicationStatusGetResponse objResponse = new ApplicationStatusGetResponse();
            ApplicationStatusBAL objBAL = new ApplicationStatusBAL();
            ApplicationStatus objEntity = new ApplicationStatus();
            List<ApplicationStatus> lstApplicationType = new List<ApplicationStatus>();
            try
            {
                //if (!TokenHelper.ValidateToken(Key))
                //{
                //    objResponse.Status = false;
                //    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                //    objResponse.Message = "User session has expired.";
                //    objResponse.ApplicationTypeGetList = null;
                //    return objResponse;

                //}

                lstApplicationType = objBAL.Get_All_ApplicationStatus();
                if (lstApplicationType != null && lstApplicationType.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    List<ApplicationStatusGet> lstApplicationStatusSelected = lstApplicationType.Select(ApplicationStatus => new ApplicationStatusGet
                    {
                        ApplicationStatusId = ApplicationStatus.ApplicationStatusId,
                        SortOrder = ApplicationStatus.SortOrder,
                        Name = ApplicationStatus.Name,
                        IsActive = ApplicationStatus.IsActive
                    }).ToList();

                    objResponse.ApplicationStatusList = lstApplicationStatusSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.ApplicationStatusList = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "ApplicationStatusGetAll", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.ApplicationStatusList = null;
            }
            return objResponse;


        }

        #endregion

        #region CertificationType

        /// <summary>
        /// Looks up all data by Key For CertificationType.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("GET")]
        [ActionName("CertificationTypeGetAll")]
        public CertificationTypeGetResponse CertificationTypeGetAll(string Key)
        {
            LogingHelper.SaveAuditInfo(Key);

            CertificationTypeGetResponse objResponse = new CertificationTypeGetResponse();
            CertificationTypeBAL objBAL = new CertificationTypeBAL();
            CertificationType objEntity = new CertificationType();
            List<CertificationType> lstCertificationType = new List<CertificationType>();
            try
            {
                //if (!TokenHelper.ValidateToken(Key))
                //{
                //    objResponse.Status = false;
                //    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                //    objResponse.Message = "User session has expired.";
                //    objResponse.CertificationTypeGetList = null;
                //    return objResponse;

                //}

                lstCertificationType = objBAL.Get_All_CertificationType();
                if (lstCertificationType != null && lstCertificationType.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    List<CertificationTypeGet> lstCertificationTypeSelected = lstCertificationType.Select(CertificationType => new CertificationTypeGet
                    {
                        CertificationTypeId = CertificationType.CertificationTypeId,
                        Code = CertificationType.Code,
                        Name = CertificationType.Name,
                        IsActive = CertificationType.IsActive
                    }).ToList();

                    objResponse.CertificationTypeGetList = lstCertificationTypeSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.CertificationTypeGetList = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "CertificationTypeGetAll", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.CertificationTypeGetList = null;
            }
            return objResponse;


        }

        #endregion

        #region IndividualEmploymentEmployerType

        /// <summary>
        /// Looks up all data by Key For IndividualEmploymentEmployerType.
        /// </summary>
        /// <param name="Key">The Key of the data.</param>
        [AcceptVerbs("GET")]
        [ActionName("IndividualEmploymentEmployerTypeGetAll")]
        public IndividualEmploymentEmployerTypeGetResponse IndividualEmploymentEmployerTypeGetAll(string Key)
        {
            LogingHelper.SaveAuditInfo(Key);

            IndividualEmploymentEmployerTypeGetResponse objResponse = new IndividualEmploymentEmployerTypeGetResponse();
            IndividualEmploymentEmployerTypeBAL objBAL = new IndividualEmploymentEmployerTypeBAL();
            IndividualEmploymentEmployerType objEntity = new IndividualEmploymentEmployerType();
            List<IndividualEmploymentEmployerType> lstIndividualEmploymentEmployerType = new List<IndividualEmploymentEmployerType>();
            try
            {
                //if (!TokenHelper.ValidateToken(Key))
                //{
                //    objResponse.Status = false;
                //    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.ValidateToken).ToString("00");
                //    objResponse.Message = "User session has expired.";
                //    objResponse.IndividualEmploymentEmployerTypeGetList = null;
                //    return objResponse;

                //}

                lstIndividualEmploymentEmployerType = objBAL.Get_All_IndividualEmploymentEmployerType();
                if (lstIndividualEmploymentEmployerType != null && lstIndividualEmploymentEmployerType.Count > 0)
                {
                    objResponse.Status = true;
                    objResponse.Message = "";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                    List<IndividualEmploymentEmployerTypeGet> lstIndividualEmploymentEmployerTypeSelected = lstIndividualEmploymentEmployerType.Select(IndividualEmploymentEmployerType => new IndividualEmploymentEmployerTypeGet
                    {
                        IndividualEmploymentEmployerTypeId = IndividualEmploymentEmployerType.IndividualEmploymentEmployerTypeId,
                        IndividualId = IndividualEmploymentEmployerType.IndividualId,
                        IndividualEmploymentId = IndividualEmploymentEmployerType.IndividualEmploymentId,
                        EmployerTypeId = IndividualEmploymentEmployerType.EmployerTypeId,
                        EmployerTypeValue = IndividualEmploymentEmployerType.EmployerTypeValue,
                        IsActive = IndividualEmploymentEmployerType.IsActive
                    }).ToList();

                    objResponse.IndividualEmploymentEmployerTypeGetList = lstIndividualEmploymentEmployerTypeSelected;
                }
                else
                {
                    objResponse.Status = false;
                    objResponse.Message = "No record found.";
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    objResponse.IndividualEmploymentEmployerTypeGetList = null;
                }
            }
            catch (Exception ex)
            {
                LogingHelper.SaveExceptionInfo(Key, ex, "IndividualEmploymentEmployerTypeGetAll", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.Message = ex.Message;
                objResponse.IndividualEmploymentEmployerTypeGetList = null;
            }
            return objResponse;


        }

        #endregion
    }
}
