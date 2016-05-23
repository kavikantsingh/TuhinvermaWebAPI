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

            using (TransactionScope transScope = new TransactionScope())
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


                    foreach (FeeDetails fee in objRequest.FeeDetailsList)
                    {

                        RevFeeDueBAL objFeeDueBAL = new RevFeeDueBAL();
                        RevFeeDue objFeeDue = new RevFeeDue();
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

                    transScope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


            return objTransaction;
        }


    }
}
