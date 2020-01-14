using System;
using Ass1.Model;

namespace Ass1
{
    public interface IAccountFactory
    {
        Account CreateAccount(decimal balance);
    }
}
