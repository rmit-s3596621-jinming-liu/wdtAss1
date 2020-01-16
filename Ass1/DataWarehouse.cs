using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Ass1.Model;
using System.Net.Http;
using Newtonsoft.Json;

namespace Ass1
{
    public class DataWarehouse
    {
        private static DataWarehouse instance;
        private DataWarehouse() { }
        public static DataWarehouse getInstance()
        {
            if (instance == null)
                instance = new DataWarehouse();
            return instance;
        }

        public List<Account> Accounts = new List<Account>();
        public List<Customer> Customers = new List<Customer>();
        public List<Login> Logins = new List<Login>();
        public List<Transaction> Transactions = new List<Transaction>();

      
       

        private static string ConnectionString = "Server=wdt2020.australiasoutheast.cloudapp.azure.com;Database=s3596621;Uid=s3596621;Password=abc123";
        private static SqlConnection connection = new SqlConnection(DataWarehouse.ConnectionString);

        public void LoadData()

        {
            // try to load data from db
            DataWarehouse.instance.LoadCustomer();
            DataWarehouse.instance.LoadAccount();
           // DataWarehouse.instance.LoadTransaction();
            DataWarehouse.instance.LoadLogin();

            // if no results are presented
            if (DataWarehouse.instance.Customers.Count == 0)
            //call API
            { DataWarehouse.instance.LoadCustomerFromAPI();

            }
    
                              
            if (DataWarehouse.instance.Logins.Count == 0)
                DataWarehouse.instance.LoadLoginFromAPI();



            foreach (Customer customer in DataWarehouse.instance.Customers)
                foreach (Account account in customer.Accounts)
                {
                    DataWarehouse.instance.Accounts.Add(account);
                    foreach (Transaction transaction in account.Transactions)
                    {
                        transaction.AccountNumber = account.AccountNumber;
                        transaction.DestinationAccountNumber = account.AccountNumber;
                        transaction.Comment = "no comment";
                        transaction.TransactionType = 'D';
                        transaction.Amount = account.Balance;
                        transaction.TransactionID = account.AccountNumber;
                        DataWarehouse.instance.Transactions.Add(transaction);
                    }
                }







        }

   


        private void LoadAccount()
        {

           

            var command = DataWarehouse.connection.CreateCommand();
            command.CommandText = "select * from Account";
            var table = new DataTable();
            new SqlDataAdapter(command).Fill(table);


            DataWarehouse.instance.Accounts = table.Select().Select(x =>
                new Account((int)x["AccountNumber"], (string)x["AccountType"], (int)x["CustomerID"], (decimal)x["Balance"])).ToList();
        }

        private void LoadCustomer()
        {

            

            var command = DataWarehouse.connection.CreateCommand();
            command.CommandText = "select * from customer";
            var table = new DataTable();
            new SqlDataAdapter(command).Fill(table);


            DataWarehouse.instance.Customers = table.Select().Select(x =>
                new Customer((int)x["CustomerID"], (string)x["Name"], (string)x["Address"], (string)x["City"], (string)x["PostCode"])).ToList();
        }

        private  void LoadLogin()
        {

          

            var command = DataWarehouse.connection.CreateCommand();
            command.CommandText = "select * from Login";
            var table = new DataTable();
            new SqlDataAdapter(command).Fill(table);


            DataWarehouse.instance.Logins = table.Select().Select(x =>
                new Login((string)x["LoginID"], (int)x["CustomerID"], (string)x["PasswordHash"])).ToList();
        }

        private void LoadTransaction()
        {

          

            var command = DataWarehouse.connection.CreateCommand();
            command.CommandText = "select * from Transaction";
            var table = new DataTable();
            new SqlDataAdapter(command).Fill(table);


            DataWarehouse.instance.Transactions = table.Select().Select(x =>
                new Transaction((int)x["TransactionID"], (char)x["TransactionType"], (int)x["AccountNumber"], (int)x["DestinationAccountNumber"], (decimal)x["Amount"], (string)x["Comment"], (string)x["TransactionTimeUtc"])).ToList();
        }



        public  void LoadCustomerFromAPI()
        {
            var client = new HttpClient();

            var json = client.GetStringAsync("https://coreteaching01.csit.rmit.edu.au/~e87149/wdt/services/customers/").Result;        
            DataWarehouse.instance.Customers = JsonConvert.DeserializeObject<List<Customer>>(json);
            


        }

        private  void LoadLoginFromAPI()
        {
            var client = new HttpClient();

            var json = client.GetStringAsync("https://coreteaching01.csit.rmit.edu.au/~e87149/wdt/services/logins/").Result;

            DataWarehouse.instance.Logins = JsonConvert.DeserializeObject<List<Login>>(json);
           
        }


        //insert account toDB
        //Drop + create+ insert more convernient than using Update
        private  void InsertAccountIntoDB( Account account)
        {
            string insertstatement = @"INSERT INTO Account(AccountNumber,AccountType,CustomerID,Balance)VALUES(@param1,@param2,@param3,@param4)";
            SqlCommand command = new SqlCommand(insertstatement, DataWarehouse.connection);
            command.Parameters.AddWithValue("@param1", account.AccountNumber);
            command.Parameters.AddWithValue("@param2", account.AccountType);
            command.Parameters.AddWithValue("@param3", account.CustomerID);
            command.Parameters.AddWithValue("@param4", account.Balance);

               try
            {
                DataWarehouse.connection.Open();
                //no select all need this
                command.ExecuteNonQuery();
                DataWarehouse.connection.Close();
                    
            }
            catch(SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DataWarehouse.connection.Close();
            }
        }

        private void InsertCustomerIntoDB(Customer customer)
        {
            string insertstatement = @"INSERT INTO Customer(CustomerID, Name, Address, City, PostCode)VALUES(@param5,@param6,@param7,@param8,@param9)";
            SqlCommand command = new SqlCommand(insertstatement, DataWarehouse.connection);
            command.Parameters.AddWithValue("@param5", customer.CustomerID);
            command.Parameters.AddWithValue("@param6", customer.Name);
            command.Parameters.AddWithValue("@param7", customer.Address??" - ");
            command.Parameters.AddWithValue("@param8", customer.City ?? " - ");
            command.Parameters.AddWithValue("@param9", customer.PostCode ?? " - ");

            try
            {
                DataWarehouse.connection.Open();
                //no select all need this
                command.ExecuteNonQuery();
                DataWarehouse.connection.Close();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DataWarehouse.connection.Close();
            }
        }


        private void InsertLoginIntoDB(Login login)
        {
            string insertstatement = @"INSERT INTO Login(LoginID, CustomerID, PasswordHash)VALUES(@param10,@param11,@param12)";
            SqlCommand command = new SqlCommand(insertstatement, DataWarehouse.connection);
            command.Parameters.AddWithValue("@param10", login.LoginID);
            command.Parameters.AddWithValue("@param11", login.CustomerID);
            command.Parameters.AddWithValue("@param12", login.PasswordHash);
            
            try
            {
                DataWarehouse.connection.Open();
                //no select all need this
                command.ExecuteNonQuery();
                DataWarehouse.connection.Close();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DataWarehouse.connection.Close();
            }
        }



        private void InsertTransactionIntoDB(Transaction transaction)
        {
            string insertstatement = @"INSERT INTO [Transaction]( TransactionType, AccountNumber,
DestinationAccountNumber, Amount, Comment, TransactionTimeUtc)VALUES(@param14,@param15,
@param16,@param17,@param18,@param19)";
            SqlCommand command = new SqlCommand(insertstatement, DataWarehouse.connection);
            //command.Parameters.AddWithValue("@param13",);
            command.Parameters.AddWithValue("@param14", transaction.TransactionType);
            command.Parameters.AddWithValue("@param15", transaction.AccountNumber);
            command.Parameters.AddWithValue("@param16", transaction.DestinationAccountNumber);
            command.Parameters.AddWithValue("@param17", transaction.Amount);
            if (transaction.Comment != null)
            {

                command.Parameters.AddWithValue("@param18", transaction.Comment);

            }
            else
            {
                command.Parameters.AddWithValue("@param18", "Please Write Your Comment");
            }
            DateTime date;
            try
            {
                date = DateTime.Parse(transaction.TransactionTimeUtc);
            }catch (DateNotCorrectException e)
            {
                date = DateTime.ParseExact(transaction.TransactionTimeUtc, "MM/dd/yyyy", null);
                Console.WriteLine(e.Message);
            }
           
            command.Parameters.AddWithValue("@param19", date);
            DataWarehouse.connection.Open();
            //no select all need this
            command.ExecuteNonQuery();
            DataWarehouse.connection.Close();
  
        }


        //create table

        public void CreateTable()
        {
            SqlCommand createtable = new SqlCommand
            {
                Connection = DataWarehouse.connection,
                CommandType = CommandType.Text
            };

            createtable.CommandText =
                                    "CREATE TABLE customer" +
                     "(" +
                         "CustomerID int not null," +
                        "Name nvarchar(50) not null," +
                       " Address nvarchar(50) null," +
                        "City nvarchar(40) null," +
                        "PostCode nvarchar(4) null," +
                        "constraint PK_Customer primary key (CustomerID)," +
                        "constraint CH_Customer_CustomerID check(len(CustomerID) = 4)" +
                    ");" +




                    "create table Login (" +
                       " LoginID nchar(8) not null," +
                       " CustomerID int not null," +
                       " PasswordHash nchar(64) not null," +
                       " constraint PK_Login primary key (LoginID)," +
                       " constraint FK_Login_Customer foreign key (CustomerID) references Customer(CustomerID)," +
                       " constraint CH_Login_LoginID check(len(LoginID) = 8)," +
                       " constraint CH_Login_PasswordHash check(len(PasswordHash) = 64));" +
                         "create table Account (" +
                    " AccountNumber int not null," +
                    " AccountType char not null," +
                    " CustomerID int not null," +
                    " Balance DECIMAL not null," +
                    " constraint PK_Account primary key (AccountNumber)," +
                    " constraint FK_Account_Customer foreign key (CustomerID) references Customer(CustomerID)," +
                    " constraint CH_Account_AccountType check(AccountType in ('C', 'S'))," +
                    " constraint CH_Account_Balance check(Balance >= 0)," +
                    " constraint CH_Account_AccountNumber check(len(AccountNumber) = 4));" +
            " create table[Transaction]" +
"("+
   " TransactionID int identity not null,"+
   " TransactionType char not null,"+
   " AccountNumber int not null,"+
   " DestinationAccountNumber int null,"+
   " Amount DECIMAL not null,"+
   " Comment nvarchar(255) null,"+
   " TransactionTimeUtc datetime2 not null,"+
   " constraint PK_Transaction primary key(TransactionID),"+
   " constraint FK_Transaction_Account_AccountNumber"+
   " foreign key(AccountNumber) references Account(AccountNumber),"+
   " constraint FK_Transaction_Account_DestinationAccountNumber"+
   " foreign key(DestinationAccountNumber) references Account(AccountNumber),"+
   " constraint CH_Transaction_TransactionType check(TransactionType in ('D', 'W', 'T', 'S')),"+
   " constraint CH_Transaction_Amount check(Amount > 0)"+
");"

            ;

            try
            {
                DataWarehouse.connection.Open();
                //no select all need this
                createtable.ExecuteNonQuery();
                DataWarehouse.connection.Close();

            }
            catch (SqlException e)

            {

                Console.WriteLine(e.Message);
            }
            finally
            {
                DataWarehouse.connection.Close();
            }
        }



        //drop table

        public void DropTable()
        {
            SqlCommand droptable = new SqlCommand
            {
                Connection = DataWarehouse.connection,
                CommandType = CommandType.Text
            };
            droptable.CommandText = "DROP TABLE [TRANSACTION];" + "DROP TABLE ACCOUNT;" + "DROP TABLE LOGIN;" + "DROP TABLE CUSTOMER;";

            try
            {
                DataWarehouse.connection.Open();
                //no select all need this
                droptable.ExecuteNonQuery();
                DataWarehouse.connection.Close();

            }
            catch (SqlException e)

            {

                Console.WriteLine(e.Message);
            }
            finally
            {
                DataWarehouse.connection.Close();
            }
        }



        public void InsertAccountData()
        {

     


            foreach (Account a in DataWarehouse.instance.Accounts)
                DataWarehouse.instance.InsertAccountIntoDB(a);
        }



        public void InsertCustomerData()
        {
            foreach (Customer c in DataWarehouse.instance.Customers)
                DataWarehouse.instance.InsertCustomerIntoDB(c);
        }

        public void InsertLoginData()
        {
            foreach (Login l in DataWarehouse.instance.Logins)
                DataWarehouse.instance.InsertLoginIntoDB(l);
        }

        public void InsertTransactionData()
        {
            foreach (Transaction t in DataWarehouse.instance.Transactions)
                DataWarehouse.instance.InsertTransactionIntoDB(t);

        }


    }
    

}