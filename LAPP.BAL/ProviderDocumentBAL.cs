using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAPP.DAL;
using LAPP.ENTITY;
namespace LAPP.BAL
{
    public class ProviderDocumentBAL
    {
        ProviderDocumentDAL objDAL = new ProviderDocumentDAL();
        public List<ProviderDocumentGET> Get_ProviderDocument_By_ProviderId_DocumentId(int ProviderId, int DocumentId)
        {
            return objDAL.Get_ProviderDocument_By_ProviderId_DocumentId(ProviderId, DocumentId);
        }

        public int Save_ProviderDocument(ProviderDocument objProvDoc)
        {
            return objDAL.Save_ProviderDocument(objProvDoc);
        }

        public int Delete_ProviderDocument_By_ProviderDocId_And_ProviderId(int? ProviderDocId, int? ProviderId, int? ModBy)
        {
            return objDAL.Delete_ProviderDocument_By_ProviderDocId_And_ProviderId(ProviderDocId, ProviderId, ModBy);
        }
    }
}
