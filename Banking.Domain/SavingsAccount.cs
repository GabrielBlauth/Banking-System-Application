using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Domain
{
    public class SavingsAccount : Account
    {
        public SavingsAccount(string firstName, string lastName) : base(firstName, lastName) { }  // CONSTRUCTOR FOR INITIALIZING A SAVINGS ACCOUNT WITH FIRST AND LAST NAME.

        public override void AddTransaction(string type, decimal amount)  // OVERRIDDEN METHOD TO ADD A TRANSACTION TO THE SAVINGS ACCOUNT.
        {
            if (amount <= 0)  // VALIDATE THAT THE TRANSACTION AMOUNT IS GREATER THAN ZERO.
            {
                throw new ArgumentException("Transaction amount must be greater than zero.");
            }

            if (type == "withdrawal" && amount > Balance)  // CHECK IF THE TRANSACTION IS A WITHDRAWAL AND THE BALANCE IS INSUFFICIENT.
            {
                throw new InvalidOperationException("Insufficient funds.");
            }

            Balance += (type == "lodgement" ? amount : -amount);  // UPDATE THE BALANCE BASED ON THE TRANSACTION TYPE (LODGEMENT OR WITHDRAWAL).
        }
    }
}
