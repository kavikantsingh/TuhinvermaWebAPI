using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAPP.DAL;
using LAPP.ENTITY;

namespace LAPP.BAL
{
    public class IndividualCommunicationToLogBAL : BaseBAL
    {
        IndividualCommunicationToLogDAL objDal = new IndividualCommunicationToLogDAL();

        public int Save_IndividualCommunicationToLog(IndividualCommunicationToLog objIndividualCommunicationToLog)
        {
            return objDal.Save_IndividualCommunicationToLog(objIndividualCommunicationToLog);
        }
    }
}
