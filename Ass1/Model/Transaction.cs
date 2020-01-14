using System;
namespace Ass1.Model
{
    public class Transaction
    {
        public int TransactionID {get;set;}
        public char TransactionType { get; set; }
        public int AccountNumber { get; set; }
        public int DestinationAccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public DateTime TransactionTimeUtc { get; set; }



        public Transaction(int TransactionID, char TransactionType, int AccountNumber, int DestinationAccountNumber,decimal Amount,string Comment, DateTime TransactionTimeUtc)
        {
            this.TransactionID = TransactionID;
            this.TransactionType = TransactionType;
            this.AccountNumber = AccountNumber;
            this.DestinationAccountNumber = DestinationAccountNumber;
            this.Amount = Amount;
            this.Comment = Comment;
            this.TransactionTimeUtc = TransactionTimeUtc;
        }
    }
}
