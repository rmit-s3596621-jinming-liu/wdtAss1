using System;
using Ass1.Model;
namespace Ass1
{
    public abstract class TransactionBuilder
    {
        public TransactionBuilder()
        {
        }

        protected Transaction transaction;
        public Transaction Transaction
        {
            get
            {
                return transaction;
            }
            set
            {
                transaction = value;
            }
        }

        public abstract void InitialTransaction();
        public abstract void SettingType();
        public abstract void Amount(decimal balance);       
        public abstract void AccountNumber(int accountnumber);
        public abstract void DestinationAccountNumber(int accountnumber);   
        public abstract void Settime(string time);


    }
}
