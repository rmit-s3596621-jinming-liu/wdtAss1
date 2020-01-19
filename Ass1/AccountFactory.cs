using System;
using Ass1.Model;
namespace Ass1
{
    public class AccountFactory
    {
        //different account type
        public static Account createAccount(string accountType,decimal balance)
        {
            switch (accountType) {
                case "Saving":
                    if (balance > 100)
                    {
                        SavingFactory sfactory = new SavingFactory();
                        return sfactory.CreateAccount(balance);
                    }
                    else { Console.WriteLine("error");
                        return null;
                    }

                case "Checking":
                    if (balance > 500)
                    {
                        CheckingFactory cfactory = new CheckingFactory();
                        return cfactory.CreateAccount(balance);

                    }
                    else
                    {
                        Console.WriteLine("error");
                        return null;
                    }
                    
                default:
                    return null;

            }
        }
    }
}
