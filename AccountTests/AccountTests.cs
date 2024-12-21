using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Banking.Domain;
using Microsoft.VisualBasic;

namespace BankingTests
{
    public class AccountTests
    {
        [Fact]  // TEST METHOD TO VALIDATE THE FORMAT OF GENERATED ACCOUNT NAMES.
        public void GenerateAccountName_ShouldGenerateCorrectFormat()
        {
            var account = new SavingsAccount("John", "Doe");  // CREATE A NEW SAVINGS ACCOUNT WITH GIVEN FIRST AND LAST NAME.
            Assert.Equal("jd-7-10-4", account.AccountName);  // ASSERT THAT THE GENERATED ACCOUNT NAME MATCHES THE EXPECTED FORMAT.
        }

        [Fact]  // TEST METHOD TO VERIFY THAT ADDING A TRANSACTION UPDATES THE ACCOUNT BALANCE.
        public void AddTransaction_ShouldUpdateBalance()
        {
            var account = new SavingsAccount("John", "Doe"); // CREATE A NEW SAVINGS ACCOUNT.
            account.AddTransaction("lodgement", 100);  // ADD A LODGEMENT TRANSACTION OF 100 UNITS.
            Assert.Equal(100, account.Balance);  // ASSERT THAT THE ACCOUNT BALANCE IS UPDATED TO REFLECT THE TRANSACTION.
        }
    }
}
