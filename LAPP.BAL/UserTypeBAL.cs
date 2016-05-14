using LAPP.DAL;
using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.BAL
{
    public class UserTypeBAL
    {
        UserTypeDAL objDAL = new UserTypeDAL();

        public int Save_UserType(UserType objUserType)
        {
            return objDAL.Save_UserType(objUserType);
        }

        //public int Update_UserType(UserType objUserType)
        //{
        //    return objDAL.Update_UserType(objUserType);
        //}

        public UserType Get_UserType_byUserTypeId(int ID)
        {
            return objDAL.Get_UserType_byUserTypeId(ID);
        }

        public List<UserType> Get_All_UserType()
        {
            return objDAL.Get_All_UserType();
        }

    }
}
