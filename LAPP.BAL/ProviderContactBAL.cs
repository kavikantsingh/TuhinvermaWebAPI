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
public class ProviderContactBAL:BaseBAL
{
ProviderContactDAL objDal = new ProviderContactDAL();
public int Save_ProviderContact(ProviderContact objProviderContact)
{
return objDal.Save_ProviderContact(objProviderContact);
}

public List<ProviderContact> Get_All_ProviderContact()
{
return objDal.Get_All_ProviderContact();
}


public ProviderContact Get_ProviderContact_By_ProviderContactId(int ID)
{
return objDal.Get_ProviderContact_By_ProviderContactId(ID);
}

}
}
