using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAPP.DAL;
using LAPP.ENTITY;

namespace LAPP.BAL
{
    public class IndividualCommunicationLogBAL : BaseBAL
    {
        IndividualCommunicationLogDAL objDal = new IndividualCommunicationLogDAL();
        public int Save_IndividualCommunicationLog(IndividualCommunicationLog objIndividualCommunicationLog)
        {
            return objDal.Save_IndividualCommunicationLog(objIndividualCommunicationLog);
        }
    }
}
