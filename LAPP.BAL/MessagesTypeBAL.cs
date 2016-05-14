using LAPP.DAL;
using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.BAL
{
    public class MessagesTypeBAL
    {
        MessagesTypeDAL objDAL = new MessagesTypeDAL();

        public int Save_MessagesType(MessagesType objMessagesType)
        {
            return objDAL.Save_MessagesType(objMessagesType);
        }

        //public int Update_MessagesType(MessagesType objMessagesType)
        //{
        //    return objDAL.Update_MessagesType(objMessagesType);
        //}

        public MessagesType Get_MessagesType_byMessagesTypeId(int ID)
        {
            return objDAL.Get_MessagesType_byMessagesTypeId(ID);
        }

        public List<MessagesType> Get_All_MessagesType()
        {
            return objDAL.Get_All_MessagesType();
        }

    }
}
