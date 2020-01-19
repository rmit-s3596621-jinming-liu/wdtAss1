using System;
using Ass1.Model;
using System.Linq;

namespace Ass1
{
    public class NWBA_PersonalBanking
    {
        public NWBA_PersonalBanking()
        {
        }

        private TransactionBuilderDirector transactionBuilderDirector = new TransactionBuilderDirector();
        private D_transactionBuilder d_TransactionBuilder = new D_transactionBuilder();
        private S_transactionBuilder s_TransactionBuilder = new S_transactionBuilder();
        private T_transactionBuilder t_TransactionBuilder = new T_transactionBuilder();
        private W_transactionBuilder w_TransactionBuilder = new W_transactionBuilder();

        public bool Deposit(Customer customer,decimal amount, bool istransfer)
        {
            Console.WriteLine("which account");
            var temp = Console.ReadLine();
            int accountnumber = int.Parse(temp);                   
            var account = customer.Accounts.FirstOrDefault(x => x.AccountNumber == accountnumber);

            if(account is null)
            {
                Console.WriteLine("No such account");
                return false;
            }
            else
            {
                account.Balance += amount;
                string time = DateTime.Now.ToString("MM/dd/yyyy");
                if (!istransfer)
                {
                    transactionBuilderDirector.construct(d_TransactionBuilder, accountnumber, accountnumber, amount, time);
                    return true;
                }
                else
                {
                    NWBA_Transfer.toaccount = accountnumber;
                    return true;
                }
            }

        }


        public bool Withdrawl(Customer customer, decimal amount,bool istransfer)
        {
            Console.WriteLine("which account ");
            var temp = Console.ReadLine();
            int accountnumber = int.Parse(temp);
            var account = customer.Accounts.FirstOrDefault(x => x.AccountNumber == accountnumber);
            //business rules 5 the minimum of saving is $0
            if (account == null)
            {
                Console.WriteLine("no such account");
                return false;
            }
               

            if (account.AccountType == "S") {
                if (account.Balance - amount <= 0)
                {
                    
                    Console.WriteLine("No enough money");
                    return false;
                }
               
                else
                {
                    account.Balance -= amount;
                    string time = DateTime.Now.ToString("MM/dd/yyyy");
                    if (istransfer)
                    {
                        if (!this.withinfour(account.AccountNumber))
                            account.Balance -= (decimal)0.2;
                        NWBA_Transfer.fromaccount = accountnumber;
                      
                        return true;
                    }
                    transactionBuilderDirector.construct(w_TransactionBuilder, accountnumber, accountnumber, amount, time);            
                    transactionBuilderDirector.construct(s_TransactionBuilder, accountnumber, accountnumber, (decimal)0.1, time);
                    if (!this.withinfour(account.AccountNumber))
                        account.Balance -= (decimal)0.1;
                    return true;
                }
            }
            // business rules 5 the minimum of checking is $200
            else
                 if (account.Balance - amount <= 200)
            {
                //account.Balance -= 0;
                Console.WriteLine("No enough money");
                return false;
            }
            else
            {
                account.Balance -= amount;
                string time = DateTime.Now.ToString("MM/dd/yyyy");
                transactionBuilderDirector.construct(w_TransactionBuilder, accountnumber, accountnumber, amount, time);
                if (istransfer)
                {
                    if (!this.withinfour(account.AccountNumber))
                        account.Balance -= (decimal)0.2;
                    NWBA_Transfer.fromaccount = accountnumber;

                    transactionBuilderDirector.construct(t_TransactionBuilder, accountnumber, accountnumber, (decimal)0.2, time);             
                    return true;
                }
                if (!this.withinfour(account.AccountNumber))
                    account.Balance -= (decimal)0.1;
                return true;
            }                     
            }
        public void ShowStatement(Customer customer) {


            Console.WriteLine("which account");
            var temp = Console.ReadLine();
            int accountnumber = int.Parse(temp);
            var account = customer.Accounts.FirstOrDefault(x => x.AccountNumber == accountnumber);

            if (account is null)
            {
                Console.WriteLine("No such account");
                return;
            }
            else
            {

                Console.WriteLine("balance is " + account.Balance);
                var transaclist = DataWarehouse.getInstance().Transactions.Where(x => x.AccountNumber == account.AccountNumber);
                foreach(Transaction transaction in transaclist)
                {
                    Console.WriteLine("-----------------------");
                    Console.WriteLine("TID "+transaction.TransactionID);
                    Console.WriteLine("Accout number "+transaction.AccountNumber);
                    Console.WriteLine("Destination accout number " + transaction.DestinationAccountNumber);
                    Console.WriteLine("Amount " + transaction.Amount);
                    Console.WriteLine("Time " + transaction.TransactionTimeUtc);
                    Console.WriteLine("Type" + transaction.TransactionType);
                    Console.WriteLine("Comment " + transaction.Comment);
                    Console.WriteLine("-----------------------");
                }
                
            }

        }

        private bool withinfour(int accountnumber)
        {
            var list = DataWarehouse.getInstance().Transactions.FindAll(x => x.AccountNumber == accountnumber).Count;
            return (list <= 4);
        }
    }

   
}
