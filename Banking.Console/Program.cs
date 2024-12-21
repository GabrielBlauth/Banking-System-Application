using System;
using Banking.Domain;

namespace Banking.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("oop-assignment-3-A-2024-74154");
            FileManager.Initialize();

            while (true)  // ENTER AN INFINITE LOOP FOR THE MAIN MENU.
            {
                Console.Clear();  // CLEAR THE CONSOLE FOR A CLEAN INTERFACE.
                Console.WriteLine("Welcome to the Banking Application");
                Console.WriteLine("Enter your choice: ");
                Console.WriteLine("1. Bank Employee");
                Console.WriteLine("2. Customer");
                Console.WriteLine("3. Exit");
                

                string choice = Console.ReadLine();  // READ THE USER'S MENU CHOICE.

                if (choice == "1")  // USER CHOSE TO LOGIN AS A BANK EMPLOYEE.
                {
                    LoginAsEmployee();
                }
                else if (choice == "2")  // USER CHOSE TO LOGIN AS A CUSTOMER.
                {
                    LoginAsCustomer();
                }
                else if (choice == "3")  // USER CHOSE TO EXIT THE APPLICATION.
                {
                    Console.WriteLine("Exiting Banking Application...");
                    break;
                }
                else  // USER ENTERED AN INVALID CHOICE.
                {
                    Console.WriteLine("Invalid choice, try again.");
                }
            }
        }

        static void LoginAsEmployee()  // PROMPT THE USER TO ENTER THEIR EMPLOYEE PIN.
        {
            Console.Write("Enter PIN: ");
            string pin = Console.ReadLine();  

            if (pin == "A1234")  // VERIFY IF THE PROVIDED PIN MATCHES THE VALID ONE.
            {
                Console.WriteLine("Welcome, Bank Employee!");
                EmployeeMenu();
            }
            else
            {
                Console.WriteLine("** ACCESS DENIED **");  // DISPLAY AN ERROR MESSAGE IF THE PIN IS INCORRECT.
                Console.WriteLine("ERROR: invalid PIN.");
                Console.ReadKey();
            }
        }

        static void LoginAsCustomer()  // PROMPT THE USER FOR CUSTOMER LOGIN DETAILS.
        {  
            Console.WriteLine("Enter first name:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter last Name:");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter Account Number:");
            string accountNumber = Console.ReadLine();
            Console.WriteLine("Enter PIN:");
            string pin = Console.ReadLine();

            if (FileManager.ValidateCustomer(firstName, lastName, accountNumber, pin)) // VALIDATE THE CUSTOMER CREDENTIALS USING THE FILE MANAGER.
            {
                Console.WriteLine($"Login successful.");
                Console.WriteLine($"Welcome {firstName} {lastName}!");
                CustomerMenu();  // NAVIGATE TO THE CUSTOMER MENU.
            }
            else
            {
                Console.WriteLine("** LOGIN FAILED **");  // DISPLAY AN ERROR MESSAGE IF LOGIN FAILED.
                Console.WriteLine("ERROR: Invalid credentials");
                Console.ReadKey();
            }
        }

        static void EmployeeMenu()  
        {
            while (true)  // ENTER A LOOP FOR THE EMPLOYEE MENU.
            {
                Console.Clear();
                Console.WriteLine("** BANK EMPLOYEE MENU **");
                Console.WriteLine("Enter your choice: ");
                Console.WriteLine("1. Create Customer");
                Console.WriteLine("2. Delete Customer");
                Console.WriteLine("3. List Customers");
                Console.WriteLine("4. Perform Transaction");
                Console.WriteLine("5. Back to Main Menu");
                

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    CreateCustomer();
                }
                else if (choice == "2")
                {
                    DeleteCustomer();
                }
                else if (choice == "3")
                {
                    ListCustomers();
                }
                else if (choice == "4")
                {
                    PerformTransaction();
                }
                else if (choice == "5")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice, try again.");  // HANDLE INVALID INPUT.
                }
            }
        }

        static void CustomerMenu()  
        {
            while (true)  // ENTER A LOOP FOR THE CUSTOMER MENU.
            {
                Console.Clear();  // CLEAR THE CONSOLE FOR A CLEAN INTERFACE.
                Console.WriteLine("****** CUSTOMER MENU ******");
                Console.WriteLine("Enter your choice: ");
                Console.WriteLine("1. View Transaction History");
                Console.WriteLine("2. Deposit Money");
                Console.WriteLine("3. Withdraw Money");
                Console.WriteLine("4. Back to Main Menu");
                

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    ViewTransactionHistory();
                }
                else if (choice == "2")
                {
                    DepositMoney();
                }
                else if (choice == "3")
                {
                    WithdrawMoney();
                }
                else if (choice == "4")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice, try again.");
                }
            }
        }

        static void CreateCustomer()
        {
            Console.WriteLine("Enter Customer First Name:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter Customer Last Name:");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter Customer Email Address:");
            string email = Console.ReadLine();

            string accountName = new SavingsAccount(firstName, lastName).AccountName;

            FileManager.AddCustomer(accountName, firstName, lastName, email);
            FileManager.CreateAccountFiles(accountName);

            Console.WriteLine();
            Console.WriteLine("CUSTOMER CREATED SUCCESSFULLY!");
            Console.WriteLine($"Customer name: {firstName} {lastName}");
            Console.WriteLine($"Customer email: {email}");
            Console.WriteLine($"Account Name: {accountName}");
            Console.WriteLine($"The PIN for the account is: {accountName.Split('-')[2]}{accountName.Split('-')[3]}");
            Console.ReadKey();
        }

        static void DeleteCustomer()
        {
            Console.WriteLine("Insert the Account Name to Delete:");
            string accountName = Console.ReadLine();

            if (FileManager.DeleteCustomer(accountName))
            {
                Console.WriteLine($"Customer {accountName} deleted successfully!");
                Console.WriteLine($"There is no {accountName} in our database");
            }
            else
            {
                Console.WriteLine($"Unable to delete customer {accountName}.");
                Console.WriteLine($"ERROR: The Ensure balance is not iqual zero.");
            }

            Console.ReadKey();
        }

        static void ListCustomers()
        {
            FileManager.ListCustomers();
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }

        static void PerformTransaction()
        {
            Console.WriteLine("Enter the Account Name:");
            string accountName = Console.ReadLine();
            Console.WriteLine("Enter Transaction Type (lodgement or withdrawal):");
            string type = Console.ReadLine();
            Console.WriteLine("Enter Account Type (savings or current):");
            string accountType = Console.ReadLine().ToLower();
            Console.WriteLine("Enter Amount:");
            decimal amount = decimal.Parse(Console.ReadLine());

            if (FileManager.PerformTransaction(accountName, type, amount, accountType))
            {
                Console.WriteLine($"Transaction of {amount} completed!");
            }
            else
            {
                Console.WriteLine("** TRANSACTION FAILED **");
                Console.WriteLine("Please check the account and balance.");
            }

            Console.ReadKey();
        }

        static void ViewTransactionHistory()
        {
            Console.WriteLine("Enter the Account Name:");
            string accountName = Console.ReadLine();
            Console.WriteLine("Enter thee Account Type (savings or current):");
            string accountType = Console.ReadLine().ToLower();

            FileManager.ShowTransactionHistory(accountName, accountType);
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }

        static void DepositMoney()
        {
            Console.WriteLine("Enter the Account Name:");
            string accountName = Console.ReadLine();
            Console.WriteLine("Enter the Account Type (savings/current):");
            string accountType = Console.ReadLine().ToLower();
            Console.WriteLine("Enter Amount to Deposit:");
            decimal amount = decimal.Parse(Console.ReadLine());

            if (FileManager.PerformTransaction(accountName, "lodgement", amount, accountType))
            {
                Console.WriteLine($"Deposit of {amount:C} successful!");
            }
            else
            {
                Console.WriteLine("Deposit failed. Please try again.");
            }

            Console.ReadKey();
        }

        static void WithdrawMoney()
        {
            Console.WriteLine("Enter the Account Name:");
            string accountName = Console.ReadLine();
            Console.WriteLine("Enter the Account Type (savings/current):");
            string accountType = Console.ReadLine().ToLower();
            Console.WriteLine("Enter the Amount to Withdraw:");
            decimal amount = decimal.Parse(Console.ReadLine());

            if (FileManager.PerformTransaction(accountName, "withdrawal", amount, accountType))
            {
                Console.WriteLine($"Withdrawal of {amount:c} successful!");
            }
            else
            {
                Console.WriteLine("Withdrawal failed. Please check your balance and try again.");
            }

            Console.ReadKey();
        }
    }
}