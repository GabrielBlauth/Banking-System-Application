using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Banking.Domain
{
    public static class FileManager  // STATIC CLASS RESPONSIBLE FOR FILE MANAGEMENT OPERATIONS.
    {
        private const string CustomersFile = "customers.txt";   // CONSTANT FOR THE FILENAME WHERE CUSTOMER DATA IS STORED.

        public static void Initialize()  // INITIALIZES THE FILE SYSTEM BY CREATING THE CUSTOMERS FILE IF IT DOESN'T EXIST.
        {
            if (!File.Exists(CustomersFile)) 
            {
                File.Create(CustomersFile).Close();  // CREATE THE CUSTOMERS FILE AND CLOSE IT IMMEDIATELY.
                Console.WriteLine("File 'customers.txt' created.");
            }
        }
      
        public static void AddCustomer(string accountName, string firstName, string lastName, string email)  // ADDS A NEW CUSTOMER TO THE CUSTOMERS FILE.
        {
            string[] accountParts = accountName.Split('-');  // SPLIT THE ACCOUNT NAME TO EXTRACT THE PIN INFORMATION.
            string pin = $"{accountParts[2]}{accountParts[3]}";  
            string customerRecord = $"{accountName}\t{firstName}\t{lastName}\t{email}\t{pin}";   // FORMAT THE CUSTOMER DATA AS A SINGLE RECORD.
            File.AppendAllLines(CustomersFile, new[] { customerRecord });  // APPEND THE CUSTOMER DATA TO THE CUSTOMERS FILE.
        }

        
        public static void ListCustomers()  // LISTS ALL CUSTOMERS ALONG WITH THEIR ACCOUNT BALANCES.
        {
            if (!File.Exists(CustomersFile))  // CHECK IF THE CUSTOMERS FILE EXISTS.
            {
                Console.WriteLine("No customers found. The file 'customers.txt' does not exist.");
                return;
            }

            string[] customers = File.ReadAllLines(CustomersFile);  // READ ALL LINES FROM THE CUSTOMERS FILE.

            if (customers.Length == 0)  // HANDLE THE CASE WHERE NO CUSTOMERS ARE REGISTERED.
            {
                Console.WriteLine("No customers found.");
                return;
            }

            Console.WriteLine("List of Customers with Balances:");
            foreach (string customer in customers)
            {
                string[] details = customer.Split('\t');  // SPLIT THE CUSTOMER RECORD INTO INDIVIDUAL DETAILS.
                string accountName = details[0];
                                
                decimal savingsBalance = GetBalance($"{accountName}-savings.txt"); // GET THE SAVINGS BALANCES FOR EACH ACCOUNT.
                decimal currentBalance = GetBalance($"{accountName}-current.txt"); // GET THE CURRENT BALANCES FOR EACH ACCOUNT.

                Console.WriteLine($"Account: {accountName}, Name: {details[1]} {details[2]}, Email: {details[3]}, Savings: {savingsBalance:C}, Current: {currentBalance:C}");    // DISPLAY THE CUSTOMER DETAILS.
            }
        }
        
        private static decimal GetBalance(string accountFile)  // RETRIEVES THE BALANCE FROM A GIVEN ACCOUNT FILE.
        {
            if (!File.Exists(accountFile)) return 0;   // RETURN ZERO IF THE ACCOUNT FILE DOES NOT EXIST.

            string[] transactions = File.ReadAllLines(accountFile);  // READ ALL TRANSACTIONS FROM THE FILE.
            if (transactions.Length == 0) return 0;    // RETURN ZERO IF THERE ARE NO TRANSACTIONS.

            string lastTransaction = transactions[^1];
            string[] parts = lastTransaction.Split('\t');
            return decimal.Parse(parts[^1]);
        }

        public static void CreateAccountFiles(string accountName)  // CREATES FILES FOR SAVINGS AND CURRENT ACCOUNTS IF THEY DON'T EXIST.
        {
            string savingsFile = $"{accountName}-savings.txt";
            string currentFile = $"{accountName}-current.txt";

            if (!File.Exists(savingsFile))  // CREATE THE SAVINGS ACCOUNT FILE IF IT DOES NOT EXIST.
            {
                File.Create(savingsFile).Close();
                Console.WriteLine($"File created: {savingsFile}");
            }

            if (!File.Exists(currentFile))  // CREATE THE CURRENT ACCOUNT FILE IF IT DOES NOT EXIST.
            {
                File.Create(currentFile).Close();
                Console.WriteLine($"File created: {currentFile}");
            }
        }

        public static bool DeleteCustomer(string accountName)  // DELETES A CUSTOMER AND THEIR ASSOCIATED ACCOUNT FILES IF THEY HAVE ZERO BALANCE.
        {
            string savingsFile = $"{accountName}-savings.txt";
            string currentFile = $"{accountName}-current.txt";

            if (!File.Exists(savingsFile) || !File.Exists(currentFile)) return false;   // CHECK IF BOTH ACCOUNT FILES EXIST.

            if (File.ReadAllLines(savingsFile).Length == 0 && File.ReadAllLines(currentFile).Length == 0)   // CHECK IF BOTH ACCOUNTS HAVE ZERO BALANCE.
            {
                File.Delete(savingsFile);  // DELETE SAVING ACCOUNT FILES.
                File.Delete(currentFile);  // DELETE CURRENT ACCOUNT FILES.

                string[] customers = File.ReadAllLines(CustomersFile);  // REMOVE THE CUSTOMER RECORD FROM THE CUSTOMERS FILE.
                File.WriteAllLines(CustomersFile, Array.FindAll(customers, line => !line.StartsWith(accountName)));

                return true;
            }

            return false;
        }
        
        public static bool PerformTransaction(string accountName, string type, decimal amount, string accountType)  // PERFORMS A TRANSACTION (LODGEMENT OR WITHDRAWAL) ON THE GIVEN ACCOUNT TYPE.
        {
            if (accountType != "savings" && accountType != "current")  // VALIDATE THE ACCOUNT TYPE.
            {
                Console.WriteLine("Invalid account type. Please choose 'savings' or 'current'.");
                return false;
            }

            string accountFile = $"{accountName}-{accountType}.txt";

            if (!File.Exists(accountFile))  // CHECK IF THE ACCOUNT FILE EXISTS.
            {
                Console.WriteLine($"Account file {accountFile} not found.");
                return false;
            }
            
            decimal balance = 0;
            string[] transactions = File.ReadAllLines(accountFile);   // RETRIEVE THE LAST BALANCE FROM THE FILE.
            if (transactions.Length > 0)
            {
                string lastTransaction = transactions[^1];
                string[] parts = lastTransaction.Split('\t');
                balance = decimal.Parse(parts[^1]);
            }

            if (type == "withdrawal" && amount > balance)  // HANDLE INSUFFICIENT FUNDS FOR WITHDRAWAL.
            {
                Console.WriteLine("Insufficient funds for withdrawal.");
                return false;
            }

            balance += (type == "lodgement" ? amount : -amount);  // UPDATE THE BALANCE BASED ON THE TRANSACTION TYPE.

            string transaction = $"{DateTime.Now:dd-MM-yyyy}\t{type}\t{amount:F2}\t{balance:F2}";  // RECORD THE TRANSACTION IN THE FILE.
            File.AppendAllLines(accountFile, new[] { transaction });

            Console.WriteLine($"Transaction successful! New balance: {balance:C}");
            return true;
        }

        public static bool ValidateCustomer(string firstName, string lastName, string accountNumber, string pin)  // VALIDATES A CUSTOMER'S CREDENTIALS (FIRST NAME, LAST NAME, ACCOUNT NUMBER, AND PIN).
        {
            if (!File.Exists(CustomersFile)) return false;  // CHECK IF THE CUSTOMERS FILE EXISTS.

            string[] customers = File.ReadAllLines(CustomersFile);  // SEARCH FOR A MATCHING CUSTOMER RECORD.
            foreach (string customer in customers)
            {
                string[] details = customer.Split('\t');
                if (details.Length == 5 &&
                    details[0] == accountNumber &&
                    details[1] == firstName &&
                    details[2] == lastName &&
                    details[4] == pin)
                {
                    return true;
                }
            }

            return false;
        }

        public static void ShowTransactionHistory(string accountName, string accountType)  // DISPLAYS THE TRANSACTION HISTORY FOR A SPECIFIC ACCOUNT TYPE.
        {
            if (accountType != "savings" && accountType != "current")
            {
                Console.WriteLine("Invalid account type. Please choose 'savings' or 'current'.");
                return;
            }

            string accountFile = $"{accountName}-{accountType}.txt";

            if (!File.Exists(accountFile))
            {
                Console.WriteLine($"No transactions found for {accountType} account.");
                return;
            }

            string[] transactions = File.ReadAllLines(accountFile);  // DISPLAY ALL TRANSACTIONS FROM THE FILE.
            Console.WriteLine($"Transaction history for {accountName} ({accountType} account):");
            foreach (var transaction in transactions)
            {
                Console.WriteLine(transaction);
            }
        }
    }
}