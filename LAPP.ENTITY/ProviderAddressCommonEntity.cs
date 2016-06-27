using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.ENTITY
{
    public class ProviderAddressCommonResponse : BaseEntity
    {
        #region School Details
        public int ProviderId { get; set; }
        public string ProviderName { get; set; }
        public ProviderContact ObjProviderContactTelephone { get; set; }
        public ProviderContact ObjProviderWebsite { get; set; }
        #endregion

        #region School Address :
        public ProviderAddress ObjProviderAddress { get; set; }
        #endregion

        #region Mailing Address
        public ProviderAddress ObjMailingAddress { get; set; }
        #endregion

        #region Director/Administrator Name & Job Title
        public ProviderContact ObjAdminEmail { get; set; }
        public ProviderContact ObjAdminPrimaryNumber { get; set; }
        public ProviderContact ObjAdminSecondaryNumber { get; set; }
        #endregion

        #region Contact Name for this Application & Job Title
        public ProviderContact ObjApplicationEmail { get; set; }
        public ProviderContact ObjApplicationPrimaryNumber { get; set; }
        public ProviderContact ObjApplicationSecondaryNumber { get; set; }
        #endregion

    }

    public class ProviderAddressCommonResponseRequest : BaseEntityServiceResponse
    {
        public ProviderAddressCommonResponse ProviderAddressCommonResponse { get; set; }

        #region Previous School's Names (if any) :
        public List<Provider> ObjlstPreviousSchool { get; set; }
        #endregion

        #region Previous Address :
        public List<ProviderAddress> ObjlstPreviousAddress { get; set; }
        #endregion

        #region Satellite Address :
        public List<ProviderAddress> ObjlstSatelliteAddress { get; set; }
        #endregion
    }
}
