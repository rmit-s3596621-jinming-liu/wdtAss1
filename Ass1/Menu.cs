using System;
using System.Linq;
namespace Ass1
{
    
    public class Menu
    {
        public Menu()
        {
        }

        private NWBA_System NWBA_System = new NWBA_System();
        private NWBA_PersonalBanking NWBA_PersonalBanking = new NWBA_PersonalBanking();
        private NWBA_Transfer NWBA_Transfer = new NWBA_Transfer();
        private bool userloggedin = false;
        private string loginid = "";
        //for programming is view

        public void MainMenu()

        {
            Console.WriteLine("---------------------------");

            Console.WriteLine("Welcome to NWBA system");
            Console.WriteLine("---------------------------");
            NWBA_System.LoadData();

            

            while (!userloggedin) {

                Console.WriteLine("Please input loginID"); 
                this.loginid= Console.ReadLine();
                Console.WriteLine("Please input password");
                string password = Console.ReadLine();
                this.userloggedin = NWBA_System.userAuthentication(loginid, password);
            }
            var customerid = DataWarehouse.getInstance().Logins.FirstOrDefault(x => x.LoginID == this.loginid).CustomerID;
            NWBA_System.customer = DataWarehouse.getInstance().Customers.FirstOrDefault(x => x.CustomerID == customerid);
            this.SubMenu();


            //Console.WriteLine("Main Menu:     1.Login      2.Deposit      3.Withdrawal   4.Transaction 5.Check Statement     6.Logout   7.Exit");
            //Console.WriteLine("Plaese Enter your input:");

            //Console.WriteLine("Your option is: {0}", input);

        }
        public void SubMenu()
        {
            Console.WriteLine("1.Deposit    2.Withdrawal   3.Transfer   4.Check Statement    5.Logout   6.Exit");
            Console.WriteLine("Please Enter your option: ");
            string option = Console.ReadLine();
            Console.WriteLine("Your option is: {0}", option);


            switch (option)
            {


                case "1":

                    //Deposit
                    {
                        deposit();
                        SubMenu();
                        break;
                    }

                case "2":

                    //Withdrawal
                    {
                        withdraw();
                        SubMenu();
                        break;
                    }



                case "3":

                  //transfer
                    {
                        transfer();
                        SubMenu();
                        break;
                    }



                case "4":

                    //check statement
                    {
                        statement();
                        SubMenu();
                        break;
                    }



                case "5":

                    //logout
                    {
                        logout();
                       
                        break;
                    }


                case "6":

                    //exit
                    {
                        //end the program
                        DataWarehouse.getInstance().DropTable();
                        DataWarehouse.getInstance().CreateTable();
                        DataWarehouse.getInstance().InsertCustomerData();
                        DataWarehouse.getInstance().InsertAccountData();
                      //  DataWarehouse.getInstance().InsertTransactionData();
                        DataWarehouse.getInstance().InsertLoginData();
                        Environment.Exit(0);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Wrong Option,  Please Try Again");
                        MainMenu();
                        break;
                    }
            }
        }

        private void logout()
        {
            NWBA_System.customer = null;
            this.userloggedin = false;
            Console.Clear();
            this.MainMenu();
        }

        private void transfer()
        {
            Console.WriteLine("input amount");
            string input = Console.ReadLine();
            decimal amount = Convert.ToDecimal(input);
            NWBA_Transfer.Transfer(NWBA_PersonalBanking, NWBA_System.customer, amount);
        }

        private void withdraw()
        {
            Console.WriteLine("input amount");
            string input = Console.ReadLine();
            decimal amount = Convert.ToDecimal(input);
            NWBA_PersonalBanking.Withdrawl(NWBA_System.customer, amount,false);
        }

        private void deposit()
        {
            Console.WriteLine("input amount");
            string input = Console.ReadLine();
            decimal amount = Convert.ToDecimal(input);
            NWBA_PersonalBanking.Deposit(NWBA_System.customer, amount,false);
          
        }

        
        private void statement()
        {
            NWBA_PersonalBanking.ShowStatement(NWBA_System.customer);
        }
    }
}