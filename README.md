# Banking System Application

**Author:** Gabriel Blauth de Araujo

## About

This project was built to apply core Object-Oriented Programming concepts — inheritance, encapsulation, and abstraction — to a realistic, real-world scenario: a simple banking system with two distinct user roles (Bank Employee and Customer), each with their own permissions and command-line workflow.

The goal was to model how a banking application handles account creation, balance management, and transaction processing, while practicing file-based data persistence and input validation without relying on an external database.

## Features

### As Bank Employee
- **Create Customer** — register a new customer (first name, last name, email), which automatically generates a unique account and a Savings account file
- **Delete Customer** — remove a customer's account (blocked if the account balance is not zero, to prevent accidental loss of funds)
- **List Customers** — view all registered customers in the system
- **Perform Transaction** — manually process a lodgement or withdrawal on any customer's Savings or Current account

### As Customer
- **View Transaction History** — see a full record of past deposits and withdrawals on your account
- **Deposit Money** — add funds to your Savings or Current account
- **Withdraw Money** — remove funds, with balance validation to prevent overdrawing

## Demo Credentials (for testing/evaluation)

To make it easy to test or evaluate this project, the Bank Employee login uses a fixed PIN:

- **Bank Employee PIN:** `A1234`

Customer accounts don't need a preset PIN — creating a new customer via the Employee Menu ("Create Customer") will generate and display a valid Account Name and PIN for that customer, which can then be used to log in as Customer.

## Technical Overview
- **Language:** C# (.NET)
- **Architecture:** separated into `Banking.Domain` (business logic), `Banking.Console` (CLI interface), and `AccountTests` (unit tests)
- **Data model:** abstract `Account` base class with `CurrentAccount` and `SavingsAccount` implementations
- **Persistence:** file-based storage (no external database) for customer records and transaction history

## Known Limitations
This was built as an academic OOP exercise, not a production system. The Employee PIN above is hardcoded intentionally for demo/evaluation purposes and should never be used as a real authentication pattern. A production version would need:
- Employee PIN moved to a secure config/environment variable, not hardcoded in source
- Customer PINs independently, randomly generated (rather than derived from account data) and stored hashed, not in plain text

## Challenges Faced
- **File Management** — keeping customer records and transaction history correctly synced across text files without data loss or overwrites required careful read/write validation
- **Transaction Logic** — ensuring withdrawals couldn't exceed the account balance, and that balances updated correctly after every transaction
- **Exception Handling** — validating user input (amounts, account details, account type) to avoid crashes and give clear error feedback
- **Path Handling** — ensuring file paths resolved correctly across multiple runs of the application
