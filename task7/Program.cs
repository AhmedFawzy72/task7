using System;
using System.Collections.Generic;

namespace task7
{
    public class Account
    {
        public string Name { get; set; }
        public double Balance { get; set; }

        public Account(string name = "Unnamed Account", double balance = 0.0)
        {
            Name = name;
            Balance = balance;
        }

        public virtual bool Deposit(double amount)
        {
            if (amount < 0)
                return false;
            else
            {
                Balance += amount;
                return true;
            }
        }

        public virtual bool Withdraw(double amount)
        {
            if (Balance - amount >= 0)
            {
                Balance -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }
        public override string ToString()
        {
            return $"{Name} with balance {Balance}";
        }
    }
    //-------------------------------------------
    class SavingAccount: Account
    {
        public double Rate { get; private set; }

        public SavingAccount(string name = "Unnamed Savings Account", double balance = 0.0, double rate = 0.0) :base(name,balance)
        {
            Rate = rate;
        }
        public void ApplyInterest()
        {
            Balance += Balance * Rate;
        }
    }
    //------------------------------------------------
    class CheckingAccount:Account
    {
        private const double WithdrawalFee = 1.50;

        public CheckingAccount(string name = "Unnamed Checking Account", double balance = 0.0) : base(name, balance)
        {

        }

        public override bool Withdraw(double amount)
        {
            return base.Withdraw(amount + WithdrawalFee);
        }

    }
    //-------------------------------------------------------------
     class TrustAccount:SavingAccount 

    {
        private int Withdrawals_Count;
        private const double BonusThreshold = 5000.00;
        private const double BonusAmount = 50.00;

        public TrustAccount(string name = "Unnamed Trust Account", double balance = 0.0, double interestRate = 0.0) : base(name, balance, interestRate)
        {
            Withdrawals_Count = 0;
        }

        public override bool Deposit(double amount)
        {
            bool success = base.Deposit(amount);
            if (success && amount >= BonusThreshold)
            {
                success = base.Deposit(BonusAmount); 
            }
            return success;
        }

        public override bool Withdraw(double amount)
        {
            if (Withdrawals_Count >= 3)
                return false;

            if (amount >= Balance * 0.2)
                return false;

            bool success = base.Withdraw(amount);
            if (success)
            {
                Withdrawals_Count++;
            }
            return success;
        }
    }
    //-------------------------------------------
    internal class Program
    {
        static void Main(string[] args)
        {
            var accounts = new List<Account>();
            accounts.Add(new Account());
            accounts.Add(new Account("Larry"));
            accounts.Add(new Account("Moe", 2000));
            accounts.Add(new Account("Curly", 5000));

            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);

            var savAccounts = new List<Account>();
            savAccounts.Add(new SavingAccount());
            savAccounts.Add(new SavingAccount("Superman"));
            savAccounts.Add(new SavingAccount("Batman", 2000));
            savAccounts.Add(new SavingAccount("Wonderwoman", 5000, 5.0));

            AccountUtil.Deposit(savAccounts, 1000);
            AccountUtil.Withdraw(savAccounts, 2000);

            // Checking
            var checAccounts = new List<Account>();
            checAccounts.Add(new CheckingAccount());
            checAccounts.Add(new CheckingAccount("Larry2"));
            checAccounts.Add(new CheckingAccount("Moe2", 2000));
            checAccounts.Add(new CheckingAccount("Curly2", 5000));

            AccountUtil.Deposit(checAccounts, 1000);
            AccountUtil.Withdraw(checAccounts, 2000);
            AccountUtil.Withdraw(checAccounts, 2000);

            // Trust
            var trustAccounts = new List<Account>();
            trustAccounts.Add(new TrustAccount());
            trustAccounts.Add(new TrustAccount("Superman2"));
            trustAccounts.Add(new TrustAccount("Batman2", 2000));
            trustAccounts.Add(new TrustAccount("Wonderwoman2", 5000, 5.0));

            AccountUtil.Deposit(trustAccounts, 1000);
            AccountUtil.Deposit(trustAccounts, 6000);
            AccountUtil.Withdraw(trustAccounts, 2000);
            AccountUtil.Withdraw(trustAccounts, 3000);
            AccountUtil.Withdraw(trustAccounts, 500);

            Console.WriteLine();
        }

    }
    //---------------------------------------------
    public static class AccountUtil
    {
        public static void Deposit(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing to Accounts =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void Withdraw(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing from Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }
    }

}
