using LAPP.DAL;
using LAPP.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.BAL
{
    public class MessageBAL
    {
        MessageDAL objDAL = new MessageDAL();

        public int Save_Message(Message objMessage)
        {
            return objDAL.Save_Message(objMessage);
        }

        //public int Update_Message(Message objMessage)
        //{
        //    return objDAL.Update_Message(objMessage);
        //}

        public Message Get_Message_byMessageId(int ID)
        {
            return objDAL.Get_Message_byMessageId(ID);
        }

        public List<Message> Get_All_Message()
        {
            return objDAL.Get_All_Message();
        }

    }
}
