using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Ass1.Model;

namespace Ass1
{
    public class DataWarehouse
    {
        private static DataWarehouse instance;
        private DataWarehouse() { }
        public static DataWarehouse getInstance()
        {
            if(instance==null)
                instance = new DataWarehouse();
            return instance;
        }
        
        public static List<Account> Accounts { get; set; }
        public List<Customer> customers { get; set; }
        public List<Login> logins { get; set; }
        public List<Transaction> transactions { get; set; }


    private static string ConnectionString = "Server=wdt2020.australiasoutheast.cloudapp.azure.com;Database=s3596621;Uid=s3596621;Password=abc123";
        
        public static void LoadData() { }
        public static void ConnectDB() { }
        public static void UpdateDatabase() { }

      
        public static void LoadAccount()
        {
            
            SqlConnection connection = new SqlConnection(ConnectionString);

            var command = connection.CreateCommand();
            command.CommandText = "select * from Account";
            var table = new DataTable();
            new SqlDataAdapter(command).Fill(table);

            
            DataWarehouse.Accounts = table.Select().Select(x =>
                new Account((int)x["AccountNumber"], (string)x["AccountType"], (int)x["CustomerID"], (decimal)x["Balance"])).ToList();
        }

    }
}
