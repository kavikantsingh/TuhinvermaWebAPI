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
    public class DocumentLkToPageTabSectionBAL : BaseBAL
    {
        DocumentLkToPageTabSectionDAL objDal = new DocumentLkToPageTabSectionDAL();
        public int Save_DocumentLkToPageTabSection(DocumentLkToPageTabSection objDocumentLkToPageTabSection)
        {
            return objDal.Save_DocumentLkToPageTabSection(objDocumentLkToPageTabSection);
        }

        public List<DocumentLkToPageTabSection> Get_All_DocumentLkToPageTabSection()
        {
            return objDal.Get_All_DocumentLkToPageTabSection();
        }


    }
}
