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
public class ProviderIndividualBAL:BaseBAL
{
ProviderIndividualDAL objDal = new ProviderIndividualDAL();
public int Save_ProviderIndividual(ProviderIndividual objProviderIndividual)
{
return objDal.Save_ProviderIndividual(objProviderIndividual);
}

public List<ProviderIndividual> Get_All_ProviderIndividual()
{
return objDal.Get_All_ProviderIndividual();
}


}
}
