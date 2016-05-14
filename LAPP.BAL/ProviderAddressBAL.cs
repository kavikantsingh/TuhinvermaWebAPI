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
public class ProviderAddressBAL:BaseBAL
{
ProviderAddressDAL objDal = new ProviderAddressDAL();
public int Save_ProviderAddress(ProviderAddress objProviderAddress)
{
return objDal.Save_ProviderAddress(objProviderAddress);
}

public List<ProviderAddress> Get_All_ProviderAddress()
{
return objDal.Get_All_ProviderAddress();
}


public ProviderAddress Get_ProviderAddress_By_ProviderAddressId(int ID)
{
return objDal.Get_ProviderAddress_By_ProviderAddressId(ID);
}

}
}
