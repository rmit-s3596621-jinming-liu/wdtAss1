using System;
using System.Collections.Generic;
using Ass1.Model;

namespace Ass1
{
    
    public static class BuildingDemo
    {


        public static void Main(string[] args)
        {


            //Console.WriteLine(nWBA_System.userAuthentication("12345678", "abc123").ToString());
            // Console.WriteLine(nWBA_System.userAuthentication("1khkhk", "abc123").ToString());
            //  Console.WriteLine(nWBA_System.userAuthentication("117963428", "abc123").ToString());
            //var transac = nWBA_System.createTransaction('D',
            //1234, 1234, (decimal)100.0 ,"sdfsdf","this is time");
            // Console.WriteLine(transac.AccountNumber);

            // var account = AccountFactory.createAccount("Saving", (decimal)123.05);
            // Customer amy = new Customer(1111, "amy", "amy house","amy city", "3000");
            // amy.accounts = new List<Account>();
            // amy.accounts.Add(account);

            // ////// logged in user
            //nWBA_System.customer = amy;

            //nWBA_System.Deposit(amy, (decimal)100);

            //  Console.WriteLine(nWBA_System.customer.accounts[0].Balance);

            // nWBA_System.customer = amy;

            //nWBA_System.Withdraw(amy, (decimal)100);

            //Console.WriteLine(nWBA_System.customer.accounts[0].Balance);


            Menu menu = new Menu();
            menu.MainMenu();

        }

    }
    }

