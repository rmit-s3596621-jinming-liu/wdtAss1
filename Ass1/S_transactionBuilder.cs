using System;
namespace Ass1
{

    //Builder for service charge
    public class S_transactionBuilder : TransactionBuilder
    { 
        public S_transactionBuilder()
        {
        }

        public override void InitialTransaction()
        {
            transaction = new Model.Transaction();
        }



        public override void Amount(decimal balance)
        {
            transaction.Amount = balance;
        }

        public override void AccountNumber(int accountnumber)
        {
            transaction.AccountNumber = accountnumber;
        }

        public override void DestinationAccountNumber(int accountnumber)
        {
            transaction.DestinationAccountNumber = accountnumber;
        }

        public override void Settime(string time)
        {
            transaction.TransactionTimeUtc = time;
        }

        public override void SettingType()
        {
            transaction.TransactionType = (char)TransactionType.serviceCharge;
        }
    }
}
