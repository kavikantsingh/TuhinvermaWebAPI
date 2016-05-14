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
public class IndividualDeclarationBAL:BaseBAL
{
IndividualDeclarationDAL objDal = new IndividualDeclarationDAL();
public int Save_IndividualDeclaration(IndividualDeclaration objIndividualDeclaration)
{
return objDal.Save_IndividualDeclaration(objIndividualDeclaration);
}

public List<IndividualDeclaration> Get_All_IndividualDeclaration()
{
return objDal.Get_All_IndividualDeclaration();
}


public IndividualDeclaration Get_IndividualDeclaration_By_IndividualDeclarationId(int ID)
{
return objDal.Get_IndividualDeclaration_By_IndividualDeclarationId(ID);
}

}
}
