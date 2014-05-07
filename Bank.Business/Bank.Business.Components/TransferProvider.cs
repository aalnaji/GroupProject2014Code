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


        public void Transfer(decimal pAmount, int pFromAcctNumber, int pToAcctNumber, String pDescription)
        {
            bool lOutcome = true;
            String lMessage = "TransferSuccessful";
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
                Console.WriteLine("Error occured while transferring money:  " + lException.Message);
                lMessage = lException.Message;
                lOutcome = false;
                throw;
            }
            finally
            {
                //here you should know if the outcome of the transfer was successful or not
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
