using System;
using Ass1.Model;
namespace Ass1
{
    public class AccountFactory
    {
        
        public static Account createAccount(string accountType,decimal balance)
        {
            switch (accountType) {
                case "Saving":
                    SavingFactory sfactory = new SavingFactory();
                       return sfactory.CreateAccount(balance);
                    
                case "Checking":
                    CheckingFactory cfactory = new CheckingFactory();
                    return cfactory.CreateAccount(balance);

                default:
                    return null;

            }
        }
    }
}
