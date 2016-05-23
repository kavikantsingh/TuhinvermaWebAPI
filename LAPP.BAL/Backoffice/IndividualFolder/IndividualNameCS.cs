using LAPP.ENTITY;
using LAPP.ENTITY.Enumeration;
using LAPP.LOGING;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LAPP.BAL.Backoffice.IndividualFolder
{
    public class IndividualNameCS
    {

        public static IndividualNameRequestResponse SaveIndividualName(Token objToken, IndividualNameRequest objIndividualNameRequest)
        {
            IndividualNameRequestResponse objResponse = new IndividualNameRequestResponse();

            IndividualNameBAL objIndividualNameBAL = new IndividualNameBAL();
            IndividualNameRequest objIndividualNameRequestNew = new IndividualNameRequest();
            IndividualBAL objIndividualBAL = new IndividualBAL();
            Individual objIndividual = new Individual();
            IndividualName objIndividualName = new IndividualName();
            List<IndividualNameRequest> lstIndividualName = new List<IndividualNameRequest>();
            try
            {

                objIndividual = objIndividualBAL.Get_Individual_By_IndividualId(objIndividualNameRequest.IndividualId);

                if (objIndividual != null && objIndividual.FirstName.ToLower() != objIndividualNameRequest.FirstName.ToLower()
                    || objIndividual.LastName.ToLower() != objIndividualNameRequest.LastName.ToLower()
                    || objIndividual.MiddleName.ToLower() != objIndividualNameRequest.MiddleName.ToLower())
                {

                    // Save New Record In IndividualName From Individual Current Name

                    objIndividualName = new IndividualName();

                    objIndividualName.FirstName = objIndividual.FirstName;
                    objIndividualName.LastName = objIndividual.LastName;
                    objIndividualName.MiddleName = objIndividual.MiddleName;
                    objIndividualName.IndividualId = objIndividual.IndividualId;
                    objIndividualName.SuffixId = objIndividualNameRequest.SuffixId;
                    objIndividualName.IndividualNameStatusId = Convert.ToInt32(eIndividualNameStatus.Previous);
                    objIndividualName.IndividualNameTypeId = Convert.ToInt32(eIndividualNameType.Individual);

                    objIndividualName.IsActive = objIndividualNameRequest.IsActive;
                    objIndividualName.IsDeleted = false;
                    objIndividualName.CreatedBy = objToken.UserId;
                    objIndividualName.CreatedOn = DateTime.Now;
                    objIndividualName.IndividualNameGuid = Guid.NewGuid().ToString();
                    objIndividualName.IndividualNameId = objIndividualNameBAL.Save_IndividualName(objIndividualName);
                    objIndividualNameRequest.IndividualNameId = objIndividualName.IndividualNameId;



                    //End Save New Record In IndividualName From Individual Current Name

                    // Update New Name In  Individual AS Current Name

                    objIndividual.FirstName = objIndividualNameRequest.FirstName;
                    objIndividual.LastName = objIndividualNameRequest.LastName;
                    objIndividual.MiddleName = objIndividualNameRequest.MiddleName;

                    objIndividual.ModifiedBy = objToken.UserId;
                    objIndividual.ModifiedOn = DateTime.Now;

                    objIndividualBAL.Save_Individual(objIndividual);


                    objResponse.Message = MessagesClass.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");

                }
                else
                {
                    objResponse.Message = "Same name has been entered.";
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }

                lstIndividualName.Add(objIndividualNameRequest);
                objResponse.Status = true;

                objResponse.IndividualNameResponse = lstIndividualName;

            }
            catch (Exception ex)
            {

                LogingHelper.SaveExceptionInfo("", ex, "SaveIndividualName", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualNameResponse = null;
            }
            return objResponse;


        }
    }
}
