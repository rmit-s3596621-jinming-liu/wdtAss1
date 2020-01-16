using System;
using System.Collections.Generic;

namespace Ass1.Model
{
    public class Account
    {
      public int AccountNumber { get; set; }
        public string AccountType { get;  set; }
        public int CustomerID { get; set; }
        public decimal Balance { get; set; }      
        public List<Transaction> Transactions { get; set; }

        public Account() {

           

        }
        public Account(int AccountNumber,string AccountType,int CustomerID, decimal Balance)
        {
            this.AccountNumber = AccountNumber;
            this.AccountType = AccountType;
            this.CustomerID = CustomerID;
            this.Balance = Balance;
            this.Transactions = new List<Transaction>();
        }

        public Account(int AccountNumber, string AccountType, int CustomerID, decimal Balance, List<Transaction> transactions)
        {
            this.AccountNumber = AccountNumber;
            this.AccountType = AccountType;
            this.CustomerID = CustomerID;
            this.Balance = Balance;
            this.Transactions = transactions;

        }


    }
}
