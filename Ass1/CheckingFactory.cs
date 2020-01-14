using System;
using Ass1.Model;

namespace Ass1
{
    public class CheckingFactory : IAccountFactory
    {

        public Account CreateAccount(decimal balance)
        {
            return  new CheckingAccount(2000, "s", 5000, balance);
        }
    }
}
