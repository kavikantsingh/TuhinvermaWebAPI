using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAPP.ENTITY;
using LAPP.DAL;
using LAPP.BAL;
using System.Transactions;

namespace LAPP.BAL.Payment
{
    public class InitiatePayment
    {
        public static LAPP.ENTITY.Transaction InitiatePaymentTransaction(InitiatePaymentRequest objRequest, int CreatedBy)
        {
            LAPP.ENTITY.Transaction objTransaction = new LAPP.ENTITY.Transaction();

            using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 20, 0)))
            {
                try
                {
                    string InvoiceNumber = SerialsBAL.Get_Receipt_No();

                    ShoppingCartBAL objShopingCartBAL = new ShoppingCartBAL();

                    ShoppingCart objShoppingCart = new ShoppingCart();
                    objShoppingCart.IndividualId = objRequest.IndividualId;
                    objShoppingCart.ProviderId = 0;
                    objShoppingCart.CreatedBy = CreatedBy;
                    objShoppingCart.CreatedOn = DateTime.Now;
                    objShoppingCart.ShoppingCartGuid = Guid.NewGuid().ToString();
                    objShoppingCart.ShoppingCartId = objShopingCartBAL.Save_ShoppingCart(objShoppingCart);

                    TransactionBAL objTransactionBAL = new TransactionBAL();

                    objTransaction = new LAPP.ENTITY.Transaction();
                    objTransaction.MasterTransactionId = 0;
                    objTransaction.ApplicationId = objRequest.ApplicationId;
                    objTransaction.IndividualId = objRequest.IndividualId;
                    objTransaction.ProviderId = 0;
                    objTransaction.IndividualLicenseId = objRequest.IndividualLicenseId;
                    objTransaction.ShoppingCartId = objShoppingCart.ShoppingCartId;
                    objTransaction.LicenseTypeId = objRequest.LicenseTypeId;
                    objTransaction.LicenseNumber = objRequest.LicenseNumber;
                    objTransaction.TransactionStartDatetime = DateTime.Now;
                    objTransaction.TransactionStatus = "P";
                    objTransaction.TransactionInterruptReasonId = 0;
                    objTransaction.TransactionDeviceTy = objTransaction.TransactionDeviceTy;
                    objTransaction.CreatedBy = CreatedBy;
                    objTransaction.CreatedOn = DateTime.Now;
                    objTransaction.TransactionGuid = Guid.NewGuid().ToString();
                    objTransaction.InvoiceNumber = InvoiceNumber;
                    objTransaction.TransactionId = objTransactionBAL.Save_Transaction(objTransaction);

                    RevFeeDue objFeeDue = null;
                    foreach (FeeDetails fee in objRequest.FeeDetailsList)
                    {

                        RevFeeDueBAL objFeeDueBAL = new RevFeeDueBAL();
                        objFeeDue = objFeeDueBAL.Get_RevFeeDue_by_IndividualIdAnd_ApplicationIdAndRevFeeMasterId(objTransaction.IndividualId, objTransaction.ApplicationId, fee.RevMstFeeId);

                        if (objFeeDue != null && objFeeDue.RevFeeDueId > 0)
                        {
                            objFeeDue.TransactionId = objTransaction.TransactionId;
                            objFeeDue.MasterTransactionId = 0;
                            objFeeDue.RevFeeMasterId = fee.RevMstFeeId;
                            objFeeDue.IndividualId = objRequest.IndividualId;
                            objFeeDue.ApplicationId = objRequest.ApplicationId;
                            objFeeDue.ProviderId = 0;
                            objFeeDue.IndividualLicenseId = fee.IndividualLicenseId;
                            objFeeDue.LicenseTypeId = fee.LicenseTypeId;
                            objFeeDue.BatchId = 0;
                            objFeeDue.TaskId = 0;
                            objFeeDue.FeeDueTypeId = 0;
                            objFeeDue.InvoiceNo = InvoiceNumber;
                            objFeeDue.InvoiceDate = DateTime.Now;
                            objFeeDue.ReferenceNumber = "";
                            objFeeDue.ControlNo = "";
                            objFeeDue.FeeAmount = fee.FeeAmount;
                            objFeeDue.FeeDueDate = null;
                            objFeeDue.CreatedBy = CreatedBy;
                            objFeeDue.CreatedOn = DateTime.Now;
                            objFeeDue.RevFeeDueGuid = Guid.NewGuid().ToString();
                            objFeeDue.RevFeeDueId = objFeeDueBAL.Save_RevFeeDue(objFeeDue);
                        }
                        else
                        {

                            objFeeDue = new RevFeeDue();
                            objFeeDue.TransactionId = objTransaction.TransactionId;
                            objFeeDue.MasterTransactionId = 0;
                            objFeeDue.RevFeeMasterId = fee.RevMstFeeId;
                            objFeeDue.IndividualId = objRequest.IndividualId;
                            objFeeDue.ApplicationId = objRequest.ApplicationId;
                            objFeeDue.ProviderId = 0;
                            objFeeDue.IndividualLicenseId = fee.IndividualLicenseId;
                            objFeeDue.LicenseTypeId = fee.LicenseTypeId;
                            objFeeDue.BatchId = 0;
                            objFeeDue.TaskId = 0;
                            objFeeDue.FeeDueTypeId = 0;
                            objFeeDue.InvoiceNo = InvoiceNumber;
                            objFeeDue.InvoiceDate = DateTime.Now;
                            objFeeDue.ReferenceNumber = "";
                            objFeeDue.ControlNo = "";
                            objFeeDue.FeeAmount = fee.FeeAmount;
                            objFeeDue.FeeDueDate = null;
                            objFeeDue.CreatedBy = CreatedBy;
                            objFeeDue.CreatedOn = DateTime.Now;
                            objFeeDue.RevFeeDueGuid = Guid.NewGuid().ToString();
                            objFeeDue.RevFeeDueId = objFeeDueBAL.Save_RevFeeDue(objFeeDue);
                        }

                    }

                    transScope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


            return objTransaction;
        }

        public static bool ProcessApprovedPayment(LAPP.ENTITY.Transaction objTrans, AuthorizeDotNetGateWayResponse objAuthResponse, Token objToken, int RequestedLicenseStatusTypeId)
        {
            bool Success = false;
            RevFeeDueBAL objFeeDueBAL = new RevFeeDueBAL();
            TransactionBAL objTranBal = new TransactionBAL();

            LAPP.ENTITY.Transaction objTransaction = new LAPP.ENTITY.Transaction();

            using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 20, 0)))
            {
                try
                {
                    if (objTrans != null)
                    {
                        string ReceiptNumber = SerialsBAL.Get_Receipt_No();
                        IndividualBAL objIndividualBAL = new IndividualBAL();
                        Individual objIndividual = objIndividualBAL.Get_Individual_By_IndividualId(objTrans.IndividualId);
                        if (objIndividual != null)
                        {


                            List<RevFeeDue> objFeedueList = objFeeDueBAL.Get_RevFeeDue_by_TransactionId(objTrans.TransactionId);

                            decimal AmountDue = objFeedueList.Sum(x => x.FeeAmount);

                            RevFeeCollectBAL objFeeCollectBAL = new RevFeeCollectBAL();
                            RevFeeCollect objFeeCollect = new RevFeeCollect();
                            objFeeCollect.ShoppingCartId = objTrans.ShoppingCartId;
                            objFeeCollect.IndividualId = objTrans.IndividualId;
                            objFeeCollect.ProviderId = 0;

                            //IndividualLicenseBAL objIndividualLicenseBAL = new IndividualLicenseBAL();
                            //IndividualLicense objIndiLicense = objIndividualLicenseBAL.get

                            objFeeCollect.IndividualLicenseId = objTrans.IndividualLicenseId;
                            objFeeCollect.LicenseTypeId = objTrans.LicenseTypeId;
                            objFeeCollect.ReceiptNo = ReceiptNumber;
                            objFeeCollect.AmountDue = AmountDue;
                            objFeeCollect.PaymentMode = "OL";
                            objFeeCollect.PaymentModeNumber = "";
                            objFeeCollect.PaidAmount = Convert.ToDecimal(objAuthResponse.Amount);
                            objFeeCollect.PaymentDate = DateTime.Now;
                            objFeeCollect.InvoiceNo = objTrans.InvoiceNumber;
                            objFeeCollect.UserDefinedRefNo = objTrans.InvoiceNumber;
                            objFeeCollect.UserDefinedPaymentNo = ReceiptNumber;
                            objFeeCollect.RevCollectFeeNum = "";
                            objFeeCollect.RevFeePaidSource = "AuthDot";
                            objFeeCollect.CardType = "";
                            objFeeCollect.ConfirmationNo = objAuthResponse.Authorization_Code;
                            objFeeCollect.TransactionRefNo = objAuthResponse.Transaction_ID;
                            objFeeCollect.PaymentBankName = "";
                            objFeeCollect.ControlNo = "";
                            objFeeCollect.PaymentNo = "";
                            objFeeCollect.ReferenceNumber = objAuthResponse.Transaction_ID;
                            objFeeCollect.CreatedBy = objToken.UserId;
                            objFeeCollect.CreatedOn = DateTime.Now;
                            objFeeCollect.RevFeeCollectGuid = Guid.NewGuid().ToString();

                            objFeeCollect.RevFeeCollectId = objFeeCollectBAL.Save_RevFeeCollect(objFeeCollect);


                            RevFeeDisbBAL objFeeDisbBAL = new RevFeeDisbBAL();

                            foreach (RevFeeDue FeeDue in objFeedueList)
                            {

                                RevFeeDisb objFeeDisb = new RevFeeDisb();
                                objFeeDisb.TransactionId = objTrans.TransactionId;
                                objFeeDisb.ShoppingCartId = objTrans.ShoppingCartId;
                                objFeeDisb.RevFeeMasterId = FeeDue.RevFeeMasterId;
                                objFeeDisb.IndividualId = FeeDue.IndividualId;
                                objFeeDisb.ApplicationId = FeeDue.ApplicationId;
                                objFeeDisb.ProviderId = 0;
                                objFeeDisb.IndividualLicenseId = FeeDue.IndividualLicenseId;
                                objFeeDisb.LicenseTypeId = FeeDue.LicenseTypeId;
                                objFeeDisb.RevFeeDueId = FeeDue.RevFeeDueId;
                                objFeeDisb.FinclTranDate = DateTime.Now;
                                objFeeDisb.PaymentPostDate = DateTime.Now;
                                objFeeDisb.InvoiceNo = FeeDue.InvoiceNo;
                                objFeeDisb.FeePaidAmount = FeeDue.FeeAmount;
                                objFeeDisb.OrigFeeAmount = FeeDue.FeeAmount;
                                objFeeDisb.ControlNo = FeeDue.ControlNo;
                                objFeeDisb.PaymentNo = ReceiptNumber;
                                objFeeDisb.ReferenceNumber = objAuthResponse.Authorization_Code;
                                objFeeDisb.CreatedBy = objToken.UserId;
                                objFeeDisb.CreatedOn = DateTime.Now;
                                objFeeDisb.MasterTransactionId = 0;
                                objFeeDisb.RevFeeDisbGuid = Guid.NewGuid().ToString();

                                objFeeDisb.RevFeeDisbId = objFeeDisbBAL.Save_RevFeeDisb(objFeeDisb);


                                RevFeeDueDtlBAL objFeeDueDetailBAL = new RevFeeDueDtlBAL();
                                RevFeeDueDtl objFeeDueDetail = new RevFeeDueDtl();
                                objFeeDueDetail.RevFeeDueId = FeeDue.RevFeeDueId;
                                objFeeDueDetail.IndividualId = FeeDue.IndividualId;
                                objFeeDueDetail.ProviderId = 0;
                                objFeeDueDetail.IndividualLicenseId = FeeDue.IndividualLicenseId;
                                objFeeDueDetail.LicenseTypeId = FeeDue.LicenseTypeId;
                                objFeeDueDetail.InvoiceNo = FeeDue.InvoiceNo;
                                objFeeDueDetail.ReferenceNumber = FeeDue.ReferenceNumber;
                                objFeeDueDetail.PaymentNo = objFeeDisb.PaymentNo;
                                objFeeDueDetail.ReceiptNo = ReceiptNumber;
                                objFeeDueDetail.ControlNo = "";
                                objFeeDueDetail.FeePaidAmount = FeeDue.FeeAmount;
                                objFeeDueDetail.FeeDuePaymentDate = null;
                                objFeeDueDetail.CreatedBy = objToken.UserId;
                                objFeeDueDetail.CreatedOn = DateTime.Now;
                                objFeeDueDetail.RevFeeDueDtlGuid = Guid.NewGuid().ToString();

                                objFeeDueDetail.RevFeeDueDtlId = objFeeDueDetailBAL.Save_RevFeeDueDtl(objFeeDueDetail);





                            }

                            objTrans.TransactionEndDatetime = DateTime.Now;
                            objTrans.TransactionStatus = "C";
                            objTrans.TransactionInterruptReasonId = 0;
                            objTranBal.Save_Transaction(objTrans);


                            LAPP.BAL.Renewal.RenewalProcess.ChangeLicenseStatus(objTrans.IndividualLicenseId, objTrans.ApplicationId, RequestedLicenseStatusTypeId, objToken);


                        }

                    }

                    transScope.Complete();
                    Success = true;
                    return Success;
                }
                catch (Exception ex)
                {
                    transScope.Dispose();
                    throw ex;
                }
            }


            return false;
        }
    }
}
