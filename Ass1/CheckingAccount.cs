using System;
using Ass1.Model;
namespace Ass1
{
    public class CheckingAccount : Account
    {
        public CheckingAccount(int AccountNumber, string AccountType, int CustomerID, decimal Balance) : base(AccountNumber, AccountType, CustomerID, Balance)
        {
     
        }
    }
}
