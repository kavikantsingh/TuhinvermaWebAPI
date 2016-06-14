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
                int individualId =Convert.ToInt32( objIndividualNameRequest.IndividualId);
                int? applicationId = null;//objIndividualNameRequest.ApplicationId;
                if (objIndividualNameRequest.IndividualNameId > 0)
                {
                    objIndividualName = objIndividualNameBAL.Get_IndividualName_By_IndividualNameId(objIndividualNameRequest.IndividualNameId);
                    if (objIndividualName != null)
                    {

                        if (objIndividualNameRequest.IndividualNameStatusId == Convert.ToInt32(eIndividualNameStatus.Current))
                        {
                            List<IndividualName> lstIndividualNameS = new List<IndividualName>();
                            lstIndividualNameS = objIndividualNameBAL.Get_IndividualName_By_IndividualId(Convert.ToInt32(objIndividualNameRequest.IndividualId));
                            if (lstIndividualNameS != null && lstIndividualNameS.Count > 0)
                            {
                                if (objIndividualNameRequest.IndividualNameStatusId == Convert.ToInt32(eIndividualNameStatus.Current))
                                {
                                    foreach (IndividualName objname in lstIndividualNameS)
                                    {
                                        if (objname != null)
                                        {
                                            if (objname.IndividualNameStatusId == Convert.ToInt32(eIndividualNameStatus.Current))
                                            {
                                                objname.EndDate = DateTime.Now;
                                                objname.ModifiedBy = objToken.UserId;
                                                objname.ModifiedOn = DateTime.Now;
                                                objname.IndividualNameStatusId = Convert.ToInt32(eIndividualNameStatus.Previous);
                                                objname.IndividualNameId = objIndividualNameBAL.Save_IndividualName(objname);
                                            }
                                        }
                                    }
                                }
                            }



                            objIndividual = objIndividualBAL.Get_Individual_By_IndividualId(Convert.ToInt32(objIndividualNameRequest.IndividualId));
                            if (objIndividual != null)
                            {
                                objIndividual.FirstName = objIndividualNameRequest.FirstName;
                                objIndividual.LastName = objIndividualNameRequest.LastName;
                                objIndividual.MiddleName = objIndividualNameRequest.MiddleName;
                                objIndividual.ModifiedBy = objToken.UserId;
                                objIndividual.ModifiedOn = DateTime.Now;

                                objIndividualBAL.Save_Individual(objIndividual);
                            }
                        }


                        objIndividualName.FirstName = objIndividualNameRequest.FirstName;
                        objIndividualName.LastName = objIndividualNameRequest.LastName;
                        objIndividualName.MiddleName = objIndividualNameRequest.MiddleName;
                        objIndividualName.IndividualId = individualId;
                        objIndividualName.BeginDate = objIndividualNameRequest.BeginDate;
                        objIndividualName.EndDate = objIndividualNameRequest.EndDate;
                        objIndividualName.SuffixId = objIndividualNameRequest.SuffixId;
                        objIndividualName.IndividualNameStatusId = objIndividualNameRequest.IndividualNameStatusId;
                        objIndividualName.IndividualNameTypeId = Convert.ToInt32(eIndividualNameType.Individual);

                        objIndividualName.ModifiedBy = objToken.UserId; ;
                        objIndividualName.ModifiedOn = DateTime.Now;
                        objIndividualName.IsActive = objIndividualNameRequest.IsActive;
                        objIndividualName.IsDeleted = objIndividualNameRequest.IsDeleted;
                        objIndividualName.IndividualNameGuid = Guid.NewGuid().ToString();

                        objIndividualName.IndividualNameId = objIndividualNameBAL.Save_IndividualName(objIndividualName);

                        objIndividualNameRequest.IndividualNameId = objIndividualName.IndividualNameId;


                        //SAVE LOG

                        string logText = "Individual Name saved successfully. Saved on " + DateTime.Now.ToShortDateString();
                        string logSource = eCommentLogSource.WSAPI.ToString();
                        LogHelper.SaveIndividualLog(individualId, applicationId, logSource, logText, objToken.UserId, null, null, null);

                        //END SAVE LOG

                        objResponse.Message = MessagesClass.UpdateSuccess;
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }
                    else
                    {
                        objResponse.Message = "No record found with given IndividualNameId.";
                        objResponse.Status = true;
                        objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                    }
                }
                else
                {
                    objIndividual = objIndividualBAL.Get_Individual_By_IndividualId(Convert.ToInt32(objIndividualNameRequest.IndividualId));
                    #region nulltoempty
                    if (objIndividual != null)
                    {
                        if (objIndividual.FirstName == null)
                        {
                            objIndividual.FirstName = "";
                        }
                        if (objIndividual.MiddleName == null)
                        {
                            objIndividual.MiddleName = "";
                        }
                        if (objIndividual.LastName == null)
                        {
                            objIndividual.LastName = "";
                        }
                    }
                    #endregion
                    if (objIndividual != null
                        && objIndividual.FirstName.ToLower() != objIndividualNameRequest.FirstName.ToLower()
                        || objIndividual.LastName.ToLower() != objIndividualNameRequest.LastName.ToLower()
                        || objIndividual.MiddleName.ToLower() != objIndividualNameRequest.MiddleName.ToLower()
                        )
                    {
                        int RecordInName = 0;
                        // Save New Record In IndividualName From Individual Current Name
                        List<IndividualName> lstIndividualNameS = new List<IndividualName>();
                        lstIndividualNameS = objIndividualNameBAL.Get_IndividualName_By_IndividualId(Convert.ToInt32(objIndividualNameRequest.IndividualId));
                        if (lstIndividualNameS != null && lstIndividualNameS.Count > 0)
                        {
                            foreach (IndividualName objname in lstIndividualNameS)
                            {
                                if (objname != null
                                       && objname.FirstName.ToLower() == objIndividualNameRequest.FirstName.ToLower()
                                       && objname.LastName.ToLower() == objIndividualNameRequest.LastName.ToLower()
                                        && objname.MiddleName.ToLower() == objIndividualNameRequest.MiddleName.ToLower())
                                {

                                    objResponse.Message = "Name already exist.Please edit if you want to change.";
                                    objResponse.Status = true;
                                    objResponse.StatusCode = Convert.ToInt32(ResponseStatusCode.Success).ToString("00");
                                    return objResponse;
                                }
                            }

                            // update all previous
                            if (objIndividualNameRequest.IndividualNameStatusId == Convert.ToInt32(eIndividualNameStatus.Current))
                            {
                                foreach (IndividualName objname in lstIndividualNameS)
                                {
                                    if (objname != null)
                                    {
                                        if (objname.IndividualNameStatusId == Convert.ToInt32(eIndividualNameStatus.Current))
                                        {
                                            objname.EndDate = DateTime.Now;
                                            objname.ModifiedBy = objToken.UserId;
                                            objname.ModifiedOn = DateTime.Now;
                                            objname.IndividualNameStatusId = Convert.ToInt32(eIndividualNameStatus.Previous);
                                            objname.IndividualNameId = objIndividualNameBAL.Save_IndividualName(objname);
                                        }
                                    }
                                }
                            }
                        }

                        if (RecordInName == 0)
                        {
                            objIndividualName = new IndividualName();

                            objIndividualName.FirstName = objIndividualNameRequest.FirstName;
                            objIndividualName.LastName = objIndividualNameRequest.LastName;
                            objIndividualName.MiddleName = objIndividualNameRequest.MiddleName;
                            objIndividualName.IndividualId = objIndividual.IndividualId;
                            objIndividualName.BeginDate = objIndividualNameRequest.BeginDate;
                            objIndividualName.EndDate = objIndividualNameRequest.EndDate;
                            objIndividualName.SuffixId = objIndividualNameRequest.SuffixId;
                            objIndividualName.IndividualNameStatusId = objIndividualNameRequest.IndividualNameStatusId;
                            objIndividualName.IndividualNameTypeId = Convert.ToInt32(eIndividualNameType.Individual);

                            objIndividualName.IsActive = objIndividualNameRequest.IsActive;
                            objIndividualName.IsDeleted = objIndividualNameRequest.IsDeleted;
                            objIndividualName.CreatedBy = objToken.UserId;
                            objIndividualName.CreatedOn = DateTime.Now;
                            objIndividualName.IndividualNameGuid = Guid.NewGuid().ToString();
                            objIndividualName.IndividualNameId = objIndividualNameBAL.Save_IndividualName(objIndividualName);
                            objIndividualNameRequest.IndividualNameId = objIndividualName.IndividualNameId;
                        }

                        //End Save New Record In IndividualName From Individual Current Name
                        // Update New Name In  Individual AS Current Name


                        if (objIndividualNameRequest.IndividualNameStatusId == Convert.ToInt32(eIndividualNameStatus.Current))
                        {
                            //objIndividual = objIndividualBAL.Get_Individual_By_IndividualId(Convert.ToInt32(objIndividualNameRequest.IndividualId));
                            if (objIndividual != null)
                            {
                                objIndividual.FirstName = objIndividualNameRequest.FirstName;
                                objIndividual.LastName = objIndividualNameRequest.LastName;
                                objIndividual.MiddleName = objIndividualNameRequest.MiddleName;
                                objIndividual.ModifiedBy = objToken.UserId;
                                objIndividual.ModifiedOn = DateTime.Now;

                                objIndividualBAL.Save_Individual(objIndividual);
                            }
                        }

                        //SAVE LOG

                        string logText = "Individual Name saved successfully. Saved on " + DateTime.Now.ToShortDateString();
                        string logSource = eCommentLogSource.WSAPI.ToString();
                        LogHelper.SaveIndividualLog(individualId, applicationId, logSource, logText, objToken.UserId, null, null, null);

                        //END SAVE LOG

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
