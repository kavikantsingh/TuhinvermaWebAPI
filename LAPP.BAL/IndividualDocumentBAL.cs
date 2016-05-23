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
    public class IndividualDocumentBAL : BaseBAL
    {
        IndividualDocumentDAL objDal = new IndividualDocumentDAL();

        public int Save_IndividualDocument(IndividualDocument objIndividualDocument)
        {
            return objDal.Save_IndividualDocument(objIndividualDocument);
        }

        public List<IndividualDocument> Get_All_IndividualDocument()
        {
            return objDal.Get_All_IndividualDocument();
        }

        public List<IndividualDocument> Get_IndividualDocument_by_IndividualId(int IndividualId)
        {
            return objDal.Get_IndividualDocument_by_IndividualId(IndividualId);
        }
        public List<IndividualDocument> Get_IndividualDocument_by_IndividualIdAndApplicationId(int IndividualId, int ApplicationId)
        {
            return objDal.Get_IndividualDocument_by_IndividualIdAndApplicationId(IndividualId, ApplicationId);
        }
        public IndividualDocument Get_IndividualDocument_By_IndividualDocumentId(int ID)
        {
            return objDal.Get_IndividualDocument_By_IndividualDocumentId(ID);
        }

    }
}
