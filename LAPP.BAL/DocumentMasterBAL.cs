using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using LAPP.ENTITY;
using LAPP.DAL;

namespace LAPP.BAL
{
    public class DocumentMasterBAL
    {
        DocumentMasterDAL objDAL = new DocumentMasterDAL();
        public List<DocumentMasterGET> Get_DocumentMaster_By_DocId_And_DocCode(int DocId, string DocCode)
        {
            return objDAL.Get_DocumentMaster_By_DocId_And_DocCode(DocId, DocCode);
        }
        public List<DocumentMasterGET> Get_All_DocumentMaster()
        {
            return objDAL.Get_All_DocumentMaster();
        }
        public List<DocumentViewModel> GetDocumentResultSet()
        {
            return objDAL.GetDocumentResultSet();
        }
        public int Save_DocumentMaster(DocumentMaster objDocumentMaster)
        {
            return objDAL.Save_DocumentMaster(objDocumentMaster);
        }
    }
}
