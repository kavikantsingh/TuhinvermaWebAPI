using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAPP.DAL;
using LAPP.ENTITY;

namespace LAPP.BAL
{
    public class StateBAL : BaseBAL
    {
        StateDAL objDAL = new StateDAL();
        public int Save_State(State objState)
        {
            return objDAL.Save_State(objState);
        }

        //public int Update_State(State objState)
        //{
        //    return objDAL.Update_State(objState);
        //}
        public State Get_State_ByStateID(int StateID)
        {
            return objDAL.Get_State_ByStateID(StateID);
        }
        public List<State> Get_State_ByCountryID(int CountryId)
        {
            return objDAL.Get_State_ByCountryID(CountryId);
        }
        public void Delete_State_byID(int StateID)
        {
            objDAL.Delete_State_byID(StateID);
        }
    }
}
