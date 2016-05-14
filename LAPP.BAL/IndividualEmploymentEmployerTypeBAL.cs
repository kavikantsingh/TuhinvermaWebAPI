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
public class IndividualEmploymentEmployerTypeBAL:BaseBAL
{
IndividualEmploymentEmployerTypeDAL objDal = new IndividualEmploymentEmployerTypeDAL();
public int Save_IndividualEmploymentEmployerType(IndividualEmploymentEmployerType objIndividualEmploymentEmployerType)
{
return objDal.Save_IndividualEmploymentEmployerType(objIndividualEmploymentEmployerType);
}

public List<IndividualEmploymentEmployerType> Get_All_IndividualEmploymentEmployerType()
{
return objDal.Get_All_IndividualEmploymentEmployerType();
}


public IndividualEmploymentEmployerType Get_IndividualEmploymentEmployerType_By_IndividualEmploymentEmployerTypeId(int ID)
{
return objDal.Get_IndividualEmploymentEmployerType_By_IndividualEmploymentEmployerTypeId(ID);
}

}
}
