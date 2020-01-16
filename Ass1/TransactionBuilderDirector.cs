using System;
using Ass1.Model;
namespace Ass1
{
    public class TransactionBuilderDirector
    {
        public TransactionBuilderDirector()
        {
        }

        public void construct(TransactionBuilder transactionBuilder,
            int AccountNumber, int DestinationAccountNumber, decimal Amount,
             string TransactionTimeUtc)
        {
            transactionBuilder.InitialTransaction();
            transactionBuilder.SettingType();
            transactionBuilder.AccountNumber(AccountNumber);
            transactionBuilder.DestinationAccountNumber(DestinationAccountNumber);
            transactionBuilder.Amount(Amount);
            transactionBuilder.Settime(TransactionTimeUtc);
            Transaction transaction = transactionBuilder.Transaction;
            DataWarehouse.getInstance().Transactions.Add(transaction);
            transactionBuilder.Transaction = null;
        }
    }
}
