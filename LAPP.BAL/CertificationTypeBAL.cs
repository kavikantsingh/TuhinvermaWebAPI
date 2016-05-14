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
    public class CertificationTypeBAL : BaseBAL
    {
        CertificationTypeDAL objDal = new CertificationTypeDAL();

        public int Save_CertificationType(CertificationType objaddress)
        {
            return objDal.Save_CertificationType(objaddress);
        }

        public List<CertificationType> Get_All_CertificationType()
        {
            return objDal.Get_All_CertificationType();
        }


        public CertificationType Get_address_By_CertificationTypeId(int ID)
        {
            return objDal.Get_CertificationType_byCertificationTypeId(ID);
        }

    }
}
