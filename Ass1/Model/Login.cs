using System;
namespace Ass1.Model
{
    public class Login
    {
        public string LoginID { get; set; }
        public int CustomerID { get; set; }
        public string PasswordHash { get; set; }


        public Login(string LoginID, int CustomerID, string PasswordHash)
        {
            this.LoginID = LoginID;
            this.CustomerID = CustomerID;
            this.PasswordHash = PasswordHash;
        }
    }
}