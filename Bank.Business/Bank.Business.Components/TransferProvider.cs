using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bank.Business.Components.Interfaces;
using Bank.Business.Entities;
using System.Transactions;

namespace Bank.Business.Components
{
    public class TransferProvider : ITransferProvider
    {

        // new code start: declare private queue address for reponse messages
        private const String sResponseAddress = "net.msmq://localhost/private/Responseq";
        
        public void Transfer(decimal pAmount, int pFromAcctNumber, int pToAcctNumber, String pDescription)
        {

            bool lOutcome = true;
            String lMessage = "TransferSuccessful";

            // new code start: processing elements of the description parameter used in response
            char[] delimiters = { ',' };
            string[] messageParts = pDescription.Split(delimiters);
            IOperationOutcomeService responseOutcome = OperationOutcomeServiceFactory.GetOperationOutcomeService(sResponseAddress);
            
            try
            {
                using (TransactionScope lScope = new TransactionScope())
                using (BankEntityModelContainer lContainer = new BankEntityModelContainer())
                {
                    Account lFromAcct = GetAccountFromNumber(pFromAcctNumber);
                    Account lToAcct = GetAccountFromNumber(pToAcctNumber);
                    lFromAcct.Withdraw(pAmount);
                    lToAcct.Deposit(pAmount);
                    lContainer.Attach(lFromAcct);
                    lContainer.Attach(lToAcct);
                    lContainer.ObjectStateManager.ChangeObjectState(lFromAcct, System.Data.EntityState.Modified);
                    lContainer.ObjectStateManager.ChangeObjectState(lToAcct, System.Data.EntityState.Modified);
                    lContainer.SaveChanges();
                    lScope.Complete();
                }
            }
            catch (Exception lException)
            {
                // new code start: no need to set exception message as it will not be thrown
                Console.WriteLine("Error occured while transferring money:  " + lException.Message);
                //lMessage = lException.Message;
                lOutcome = false;
                //throw;
            }
            finally
            {
                // new code start: here the outcome is logged in the bank console and returned to videoStore
                //here you should know if the outcome of the transfer was successful or not
               
                Console.WriteLine(lMessage);
                responseOutcome.NotifyOperationOutcome(new OperationOutcome()
                {
                    Message = lMessage + "," + messageParts[0] + "," + messageParts[0]
                });
            }
        }

        private Account GetAccountFromNumber(int pToAcctNumber)
        {
            using (BankEntityModelContainer lContainer = new BankEntityModelContainer())
            {
                return lContainer.Accounts.Where((pAcct) => (pAcct.AccountNumber == pToAcctNumber)).FirstOrDefault();
            }
        }
    }
}
