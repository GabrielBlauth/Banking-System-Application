using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Domain
{
    public class CurrentAccount : Account
    {
        public CurrentAccount(string firstName, string lastName) : base(firstName, lastName) { }  // CONSTRUCTOR FOR THE CURRENT ACCOUNT AND INITIALIZES THE ACCOUNT USING THE BASE CLASS CONSTRUCTOR WITH FIRST AND LAST NAME.

        public override void AddTransaction(string type, decimal amount)  // OVERRIDDEN METHOD TO ADD A TRANSACTION TO THE ACCOUNT (E.G., LODGEMENT OR WITHDRAWAL).
        {
            if (amount <= 0) // ENSURE THE TRANSACTION AMOUNT IS POSITIVE.
            {
                throw new ArgumentException("Transaction amount must be greater than zero.");
            }

            if (type == "withdrawal" && amount > Balance) // IF THE TRANSACTION IS A WITHDRAWAL, CHECK IF THE ACCOUNT HAS SUFFICIENT FUNDS.
            {
                throw new InvalidOperationException("Insufficient funds.");
            }

            Balance += (type == "lodgement" ? amount : -amount); //UPDATE THE BALANCE IF IT'S A "LODGEMENT", ADD THE AMOUNT. IF IT'S A "WITHDRAWAL", SUBTRACT THE AMOUNT.
        }
    }
}