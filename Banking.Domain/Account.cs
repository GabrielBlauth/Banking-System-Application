using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace Banking.Domain
{
    public abstract class Account
    {
        public string AccountName { get; set; }  // PROPERTY TO STORE THE ACCOUNT NAME (AUTO-GENERATED).
        public decimal Balance { get; protected set; }  // PROPERTY TO STORE THE ACCOUNT BALANCE, PROTECTED SO ONLY THIS CLASS OR DERIVED CLASSES CAN MODIFY IT.

        public Account(string firstName, string lastName) // CONSTRUCTOR INITIALIZING THE ACCOUNT WITH A GENERATED NAME AND A ZERO BALANCE.
        {
            AccountName = GenerateAccountName(firstName, lastName); // GENERATE A UNIQUE ACCOUNT NAME BASED ON THE CUSTOMER'S FIRST AND LAST NAME.
            Balance = 0;  // INITIALIZE THE BALANCE TO ZERO.
        }

        public string GenerateAccountName(string firstName, string lastName)  // METHOD TO GENERATE A UNIQUE ACCOUNT NAME BASED ON A CUSTOMER'S FIRST AND LAST NAME.
        {
            char firstInitial = char.ToLower(firstName[0]);  // GET THE LOWERCASE INITIALS OF THE FIRST NAMES.
            char lastInitial = char.ToLower(lastName[0]);  // GET THE LOWERCASE INITIALS OF THE LAST NAMES.
            int nameLength = firstName.Length + lastName.Length;  // CALCULATE THE TOTAL LENGTH OF THE FULL NAME.
            int firstPosition = firstInitial - 'a' + 1;  // CALCULATE THE POSITION OF THE FIRST INITIAL IN THE ALPHABET (A=1, B=2, ETC.).
            int lastPosition = lastInitial - 'a' + 1;  // CALCULATE THE POSITION OF THE LAST INITIAL IN THE ALPHABET.

            return $"{firstInitial}{lastInitial}-{nameLength}-{firstPosition}-{lastPosition}";  // RETURN A UNIQUE STRING USING THE INITIALS, NAME LENGTH, AND ALPHABET POSITIONS.
        }

        public abstract void AddTransaction(string type, decimal amount); // ABSTRACT METHOD TO ADD A TRANSACTION, REQUIRING IMPLEMENTATION BY DERIVED CLASSES.
    }
}