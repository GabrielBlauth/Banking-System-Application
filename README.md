Gabriel Blauth de Araujo

DESCRIPITION OF WORK:
I have been working on a C# banking application whereby the user, whether an employee or customer, is able to interact with the banking application in performing account management, transactions, and transaction history.

- Account Management: I created several classes to manage different types of accounts, including Account, CurrentAccount, and SavingsAccount. These classes handle the basic properties and behaviors of bank accounts, such as account name generation, balance management, and transaction handling-deposits and withdrawals.
- File Operations: For persistent storage, I implemented a simple file-based system using the FileManager class, where customer information and transaction history are stored and accessed from text files. The system allows adding customers, deleting them, performing transactions, and showing transaction history.
- User Activity: I will now create a command line interface for employees at the bank and customers. The following program asks for the customer's or bank employee's information like name, account number, and pin number for various banking operations the user opts for. Example: the customers can deposit money in their accounts, view their transaction history, among others.
- Error Handling and Validation: I made sure to handle potential errors regarding the input of amounts, account details, or any kind of transaction. Besides, I have validated user inputs for security reasons-for instance, verifying whether the PIN entered by the user actually matches the stored value of the PIN.

CHALLENGES FACED:
- File Management: Among the major challenges was the proper storage of customer data and transaction history in text files. Managing the file read/write operations and ensuring that they were correctly updated without data loss or overwriting at times proved to be a little tricky. I had to implement checks and validations to ensure the integrity of the files.
- Transaction Logic: Another challenge was how to manage transactions like deposits and withdrawals while maintaining an accurate account balance. I had to make sure that operations like withdrawals could not be performed if the balance in the account was inadequate, and I needed to correctly update the account balance after each transaction.
- Exception Handling: Some challenges arose in the implementation of strong exception handling. For instance, invalid input for amount, account name, or account type had to be handled with extreme care to avoid application crashes and display an appropriate error message instead.
- Path handling: to make sure the file paths were correctly dealt with and the files created in the right directories, was tricky, especially when running an application several times. I had to verify that files were saved correctly and transactions appended properly to the respective account files.
- User Interface Design: Even though this project is to run on a console application, it had to ensure that the interface would be friendly and intuitively work for employees and customers alike. The text input and output should be clear to understand and easy to use. In addition, giving messages for feedback wherever appropriate enhances the user experience.

CONCLUSION:
This project has, overall, been a great opportunity to apply concepts regarding object-oriented programming in real application. A number of challenges were encountered, which included file management, error handling, validation of user input, and implementation of secure password handling. Despite all these challenges, I was able to implement a functional banking application with basic transaction capabilities and learned quite a bit about the handling of real-world problems in software development.
