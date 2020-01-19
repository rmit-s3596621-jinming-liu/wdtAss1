using System;
using Ass1.Model;
using System.Linq;

namespace Ass1
{
    public class NWBA_Transfer
    {
        public NWBA_Transfer()
        {
        }

        private TransactionBuilderDirector transactionBuilderDirector = new TransactionBuilderDirector();
        private D_transactionBuilder d_TransactionBuilder = new D_transactionBuilder();
        private S_transactionBuilder s_TransactionBuilder = new S_transactionBuilder();
        private T_transactionBuilder t_TransactionBuilder = new T_transactionBuilder();
        private W_transactionBuilder w_TransactionBuilder = new W_transactionBuilder();
        public static int fromaccount { get; set; }
        public static int toaccount { get; set; }



        public bool Transfer(NWBA_PersonalBanking NWBA_PersonalBanking,Customer fromcustomer, decimal amount)
        {
          
            NWBA_PersonalBanking.Withdrawl(fromcustomer, amount,true);
            Console.WriteLine("to which customer");
            var temp = Console.ReadLine();
            int customerid = int.Parse(temp);
            var tocustomer = DataWarehouse.getInstance().Customers.FirstOrDefault(x => x.CustomerID == customerid);
            
            if (tocustomer is null)
            {
                Console.WriteLine("No such customer");
                return false;
            }
            else
            {
                NWBA_PersonalBanking.Deposit(tocustomer,amount,true);
                string time = DateTime.Now.ToString("MM/dd/yyyy");
                transactionBuilderDirector.construct(t_TransactionBuilder, NWBA_Transfer.fromaccount, NWBA_Transfer.toaccount, amount, time);
                
                return true;
            }


           


        }
    }
}
