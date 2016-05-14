using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAPP.DAL;
using LAPP.ENTITY;
namespace LAPP.BAL
{
    public class UserSessionBAL : BaseBAL

    {
        UserSessionDAL objDAL = new UserSessionDAL();

        public int Save_UserSession(UserSession objUserSession)
        {
            return objDAL.Save_UserSession(objUserSession);
        }

        public int Update_UserSession(UserSession objUserSession)
        {
            return objDAL.Update_UserSession(objUserSession);
        }

        public List<UserSession> GetAll_UserSession()
        {
            return objDAL.GetAll_UserSession();
        }

        public UserSession Get_UserSession_By_UserSessionId(Int64 id)
        {
            return objDAL.Get_UserSession_By_TokenID(id);
        }

    }
}
