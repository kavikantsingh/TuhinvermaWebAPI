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
    public class IndividualCS
    {
        public static IndividualResponseRequest SaveIndividual(Token objToken, IndividualResponse objIndividualPostResponse)
        {
            IndividualResponseRequest objResponse = new IndividualResponseRequest();
            IndividualBAL objIndividualBAL = new IndividualBAL();
            IndividualResponse objIndividualResponse = new IndividualResponse();
            Individual objIndividual = new Individual();
            List<IndividualResponse> lstIndividual = new List<IndividualResponse>();

            try
            {
                int individualId = objIndividualPostResponse.IndividualId;
                int? applicantID = null;
                objIndividual = objIndividualBAL.Get_Individual_By_IndividualId(objIndividualPostResponse.IndividualId);
                if (objIndividual != null)
                {
                    objIndividual.FirstName = objIndividualPostResponse.FirstName;
                    objIndividual.LastName = objIndividualPostResponse.LastName;
                    objIndividual.MiddleName = objIndividualPostResponse.MiddleName;
                    objIndividual.DateOfBirth = objIndividualPostResponse.DateOfBirth;
                    objIndividual.PlaceOfBirth = objIndividualPostResponse.PlaceOfBirth;
                    objIndividual.CitizenshipId = objIndividualPostResponse.CitizenshipId;
                    objIndividual.ExternalId = objIndividualPostResponse.ExternalId;
                    objIndividual.ExternalId2 = objIndividualPostResponse.ExternalId2;
                    objIndividual.EyeColorId = objIndividualPostResponse.EyeColorId;
                    objIndividual.Gender = objIndividualPostResponse.Gender;
                    objIndividual.HairColorId = objIndividualPostResponse.HairColorId;
                    objIndividual.SuffixId = objIndividualPostResponse.SuffixId;
                    objIndividual.Height = objIndividualPostResponse.Height;
                    objIndividual.SSN = objIndividualPostResponse.SSN;
                    objIndividual.Weight = objIndividualPostResponse.Weight;
                    objIndividual.IsActive = objIndividualPostResponse.IsActive;
                    objIndividual.IsArchived = objIndividualPostResponse.IsArchived;

                    objIndividual.ModifiedBy = objToken.UserId;
                    objIndividual.ModifiedOn = DateTime.Now;

                    objIndividualBAL.Save_Individual(objIndividual);


                    //SAVE LOG

                    string logText = "Individual updated successfully. Updated on " + DateTime.Now.ToShortDateString();
                    string logSource = eCommentLogSource.WSAPI.ToString();
                    LogHelper.SaveIndividualLog(individualId, applicantID, logSource, logText, objToken.UserId, null, null, null);

                    //END SAVE LOG

                    objResponse.Message = MessagesClass.UpdateSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }
                else
                {
                    objIndividual = new Individual();
                    objIndividual.FirstName = objIndividualPostResponse.FirstName;
                    objIndividual.LastName = objIndividualPostResponse.LastName;
                    objIndividual.MiddleName = objIndividualPostResponse.MiddleName;
                    objIndividual.DateOfBirth = objIndividualPostResponse.DateOfBirth;
                    objIndividual.PlaceOfBirth = objIndividualPostResponse.PlaceOfBirth;
                    objIndividual.CitizenshipId = objIndividualPostResponse.CitizenshipId;
                    objIndividual.ExternalId = objIndividualPostResponse.ExternalId;
                    objIndividual.ExternalId2 = objIndividualPostResponse.ExternalId2;
                    objIndividual.EyeColorId = objIndividualPostResponse.EyeColorId;
                    objIndividual.Gender = objIndividualPostResponse.Gender;
                    objIndividual.HairColorId = objIndividualPostResponse.HairColorId;
                    objIndividual.SuffixId = objIndividualPostResponse.SuffixId;
                    objIndividual.Height = objIndividualPostResponse.Height;
                    objIndividual.SSN = objIndividualPostResponse.SSN;
                    objIndividual.Weight = objIndividualPostResponse.Weight;
                    objIndividual.IsActive = objIndividualPostResponse.IsActive;
                    objIndividual.IsArchived = objIndividualPostResponse.IsArchived;
                    objIndividual.IsDeleted = false;
                    objIndividual.CreatedBy = objToken.UserId;
                    objIndividual.CreatedOn = DateTime.Now;
                    objIndividual.IndividualGuid = Guid.NewGuid().ToString();
                    objIndividual.Authenticator = Guid.NewGuid().ToString();
                    objIndividual.IndividualId = objIndividualBAL.Save_Individual(objIndividual);
                    objIndividualPostResponse.IndividualId = objIndividual.IndividualId;

                    individualId = objIndividual.IndividualId;

                    //SAVE LOG

                    string logText = "Individual saved successfully. Saved on " + DateTime.Now.ToShortDateString();
                    string logSource = eCommentLogSource.WSAPI.ToString();
                    LogHelper.SaveIndividualLog(individualId, applicantID, logSource, logText, objToken.UserId, null, null, null);

                    //END SAVE LOG

                    objResponse.Message = MessagesClass.SaveSuccess;
                    objResponse.Status = true;
                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                }
                lstIndividual.Add(objIndividualPostResponse);
                objResponse.Status = true;

                objResponse.IndividualResponse = lstIndividual;

            }
            catch (Exception ex)
            {

                LogingHelper.SaveExceptionInfo("", ex, "SaveIndividual", ENTITY.Enumeration.eSeverity.Error);

                objResponse.Status = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Exception).ToString("00");
                objResponse.IndividualResponse = null;
            }
            return objResponse;


        }


    }
}
