using System;
using System.Collections.Generic;

namespace Ass1.Model
{
    public class Customer
    {
       public int CustomerID { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
       public string  PostCode { get; set; }   
        public List<Account> Accounts { get; set; }

        public Customer() {
            
        }

        public Customer(int CustomerID, string Name, string Address, string City, string PostCode)
        {
            this.CustomerID = CustomerID;
            this.Name = Name;
            this.Address = Address;
            this.City = City;
            this.PostCode = PostCode;
            this.Accounts = new List<Account>();

        }
        public Customer(int CustomerID, string Name, string Address, string City, string PostCode, List<Account> accounts)
        {
            this.CustomerID = CustomerID;
            this.Name = Name;
            this.Address = Address;
            this.City = City;
            this.PostCode = PostCode;
            this.Accounts = accounts;

        }
    }
}
