using System;
using Ass1.Model;
using SimpleHashing;
using System.Linq;
namespace Ass1
{

    
    //using  enum to define 4 transaction types 
    public enum TransactionType
    {
        Ddeposit = 'D',
        Withdrawal = 'W',
        transfer = 'T',
        serviceCharge = 'S'

    }
    
  
    public class NWBA_System
    {
        public NWBA_System()
        {

        }

        private double ATMWithdraw = 0.1;
        private double AccountTransfer = 0.2;
        public Customer customer { get; set; }
        private TransactionBuilderDirector TransactionBuilderDirector = new TransactionBuilderDirector();
        private TransactionBuilder Depositbuilder = new D_transactionBuilder();
        private TransactionBuilder Withdrawbuilder = new D_transactionBuilder();
        private TransactionBuilder Transferbuilder = new D_transactionBuilder();
        private TransactionBuilder Servicebuilder = new D_transactionBuilder();
        private NWBA_PersonalBanking personalBanking = new NWBA_PersonalBanking();
        private NWBA_Transfer NWBA_Transfer = new NWBA_Transfer();



        public void LoadData()
        {

       
            DataWarehouse.getInstance().LoadData();
          
        }

        //login
        public bool userAuthentication(string loginid, string password)
        {
            var _customer = DataWarehouse.getInstance().Logins.FirstOrDefault(x => x.LoginID == loginid);
            string passwordhash;

            if (!(_customer is null))
            {
                passwordhash = _customer.PasswordHash;
                return SimpleHashing.PBKDF2.Verify(passwordhash, password);
            }
            return false;

        }

        public void userLogOut(ref bool userLoggedin) {

            this.customer = null;
            Console.WriteLine("log out success");
            userLoggedin = false;

        }


        //transaction
        //public Transaction createTransaction(char transactiontype,
        //    int AccountNumber, int DestinationAccountNumber, decimal Amount,
        //    string Comment, string TransactionTimeUtc)
            
        //{
        //    switch (transactiontype)
        //    {
        //        case 'D':
        //            TransactionBuilderDirector.construct(Depositbuilder,
        //     AccountNumber, DestinationAccountNumber, Amount,
        //     TransactionTimeUtc);
        //            return Depositbuilder.Transaction;
        //        case 'w':
        //            TransactionBuilderDirector.construct(Withdrawbuilder,
        //     AccountNumber, DestinationAccountNumber, Amount,
        //     TransactionTimeUtc);
        //            return Withdrawbuilder.Transaction;

        //        case 'T':
        //            TransactionBuilderDirector.construct(Transferbuilder,
        //     AccountNumber, DestinationAccountNumber, Amount,
        //     TransactionTimeUtc);
        //            return Transferbuilder.Transaction;

        //        case 'S':
        //            TransactionBuilderDirector.construct(Servicebuilder,
        //     AccountNumber, DestinationAccountNumber, Amount,
        //     TransactionTimeUtc);
        //            return Servicebuilder.Transaction;

        //        default:
        //            return null;

        //    }
        //}


        public bool Deposit(Customer customer, decimal amount,bool istransfer) {
            return personalBanking.Deposit(customer, amount,istransfer);
        }

        public bool Withdraw(Customer customer, decimal amount)
        {
            return personalBanking.Withdrawl(customer, amount,false);
        }

    }
}
