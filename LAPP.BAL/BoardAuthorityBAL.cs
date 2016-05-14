using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAPP.DAL;
using LAPP.ENTITY;
namespace LAPP.BAL
{
    public class BoardAuthorityBAL
    {
        BoardAuthorityDAL objDal = new BoardAuthorityDAL();

        public int Save_BoardAuthority(BoardAuthority objBoardAuthority)
        {
            return objDal.Save_BoardAuthority(objBoardAuthority,true);
        }

        public int Update_BoardAuthority(BoardAuthority objBoardAuthority)
        {
            return objDal.Update_BoardAuthority(objBoardAuthority, true);
        }

        public BoardAuthority Get_BoardAuthority_byID(int ID)
        {
            return objDal.Get_BoardAuthority_byID(ID, true);
        }

        public List<BoardAuthority> Get_All_BoardAuthority()
        {
            return objDal.Get_All_BoardAuthority( true);
        }
    }
}
