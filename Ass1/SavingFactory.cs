using System;
using Ass1.Model;
namespace Ass1
{
    public class SavingFactory: IAccountFactory
    {
        public Account CreateAccount(decimal balance)
        {
            return  new SavingAccount(2000, "s", 5000, balance) ;
        }
    }
}
