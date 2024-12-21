using Banking.Domain;

namespace Banking.Tests
{
    public class FileManagerTests
    {
        [Fact]  // TEST TO VERIFY THAT A CUSTOMER ENTRY IS PROPERLY CREATED AND ADDED TO THE FILE.
        public void AddCustomer_ShouldCreateCustomerEntry()
        {
           
            FileManager.Initialize();  // ARRANGE: INITIALIZE FILE, DEFINE CUSTOMER DETAILS.
            string accountName = "js-8-10-19";
            string firstName = "John";
            string lastName = "Smith";
            string email = "john.smith@example.com";

            FileManager.AddCustomer(accountName, firstName, lastName, email);  // ACT: ADD CUSTOMER ENTRY TO THE FILE.
            string[] customers = File.ReadAllLines("customers.txt");

            Assert.Contains(customers, line => line.StartsWith(accountName));  // ASSERT: CHECK IF THE FILE CONTAINS THE NEWLY ADDED CUSTOMER.
        }

        [Fact]  // TEST TO VERIFY THAT A CUSTOMER ENTRY AND ASSOCIATED FILES ARE REMOVED SUCCESSFULLY.
        public void DeleteCustomer_ShouldRemoveCustomer()
        {
    
            FileManager.Initialize();  // ARRANGE: INITIALIZE FILE AND CREATE ACCOUNT FILES FOR THE CUSTOMER.
            string accountName = "js-8-10-19";
            FileManager.CreateAccountFiles(accountName);  

            FileManager.DeleteCustomer(accountName);  // ACT: DELETE THE CUSTOMER AND ASSOCIATED FILES.
            string[] customers = File.ReadAllLines("customers.txt");

            Assert.DoesNotContain(customers, line => line.StartsWith(accountName));  // ASSERT: VERIFY THAT THE CUSTOMER ENTRY AND FILES NO LONGER EXIST.
            Assert.False(File.Exists($"{accountName}-savings.txt"));
            Assert.False(File.Exists($"{accountName}-current.txt"));
        }

        [Fact]
        public void PerformTransaction_ShouldUpdateAccountBalance()  // TEST TO VERIFY THAT PERFORMING A TRANSACTION UPDATES THE ACCOUNT BALANCE CORRECTLY.
        {
            
            FileManager.Initialize();  // ARRANGE: INITIALIZE FILE AND CREATE ACCOUNT FILES FOR THE CUSTOMER.
            string accountName = "js-8-10-19";
            FileManager.CreateAccountFiles(accountName);

            bool result = FileManager.PerformTransaction(accountName, "lodgement", 100.00m, "savings");  // ACT: PERFORM A LODGEMENT TRANSACTION.

            Assert.True(result);  // ASSERT: VERIFY THAT THE TRANSACTION WAS SUCCESSFUL AND THE BALANCE UPDATED.

            string[] transactions = File.ReadAllLines($"{accountName}-savings.txt");
            Assert.Contains(transactions, line => line.Contains("100.00"));
        }

        [Fact]  // TEST TO VERIFY THAT TRANSACTION HISTORY IS DISPLAYED CORRECTLY.
        public void ShowTransactionHistory_ShouldDisplayTransactions()
        {

            FileManager.Initialize();  // ARRANGE: INITIALIZE FILE, CREATE ACCOUNT FILES, AND PERFORM A TRANSACTION.
            string accountName = "js-8-10-19";
            FileManager.CreateAccountFiles(accountName);
            FileManager.PerformTransaction(accountName, "lodgement", 100.00m, "savings");

            using (var sw = new StringWriter())  // CAPTURE CONSOLE OUTPUT TO VERIFY DISPLAYED TRANSACTION HISTORY.
            {
                Console.SetOut(sw);

                FileManager.ShowTransactionHistory(accountName, "savings");  // ACT: DISPLAY THE TRANSACTION HISTORY.

                var output = sw.ToString();  // ASSERT: VERIFY THAT THE OUTPUT CONTAINS TRANSACTION DETAILS.
                Assert.Contains("Transaction history for js-8-10-19 (savings account):", output);
                Assert.Contains("100.00", output);
            }
        }
    }
}