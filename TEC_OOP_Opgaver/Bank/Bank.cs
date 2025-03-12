namespace TEC_OOP_Opgaver.Bank
{
    internal class Bank
    {
        public Bank()
        {

            BankAccount account = new BankAccount(
                new AccountHolder("Peter", "Petersen", 123456),
                new AccountAdministrator("Lisa", "Lisasen", 654321),
                1000);

            account.Deposit(400);
            account.Withdraw(50);

            /// Vil ikke fungere:
            account.Withdraw(3000);
            account.Deposit(-50);

            account.ShowBalance();
        }

        /// PRIVAT konto
        private class BankAccount
        {
            private decimal _balance;
            private AccountHolder _holder;
            private AccountAdministrator _admin;

            public BankAccount(AccountHolder holder, AccountAdministrator admin, decimal startBalance)
            {
                // Eksempel på "min. værdi" => hvis under 0, så sæt den til 0
                _balance = startBalance < 0 ? 0 : startBalance;

                _holder = holder;
                _admin = admin;

                // Ved konto-oprettelse: skriv "Hej {fuldeNavn}. Din konto er oprettet med {fuldeNavn} som admin."
                Console.WriteLine($"Hej {_holder.FullName}. Din konto er oprettet med {_admin.FullName} som admin.");
            }

            public void Deposit(decimal amount)
            {
                if (amount > 0)
                {
                    _balance += amount;
                    HappyLog($"Deposited {amount}. Now you have {_balance}");
                }
                else
                {
                    ErrorLog("Deposit amount cant be less than zero");
                }
            }

            public void Withdraw(decimal amount)
            {
                if (amount > 0 && amount <= _balance)
                {
                    _balance -= amount;
                    HappyLog($"Withdrew {amount}. Now you have {_balance}");
                }
                else
                {
                    ErrorLog("Withdraw amount cant be less than zero, or over account balance");
                }
            }

            public void ShowBalance()
            {
                Console.WriteLine($"{_holder.FullName}'s account balance: {_balance}");
                Console.WriteLine($"(Admin: {_admin.FullName})");
            }
        }

        // Fælles baseklasse for som IKKE er abstract
        private class Person
        {
            private int _id;
            public virtual string FirstName { get; set; }
            public virtual string LastName { get; set; }
            public virtual string FullName => FirstName + " " + LastName;       ///

            public int ID => _id;

            public Person(string first, string last, int id)
            {
                if (id.ToString().Length != 6) // Tjekker længden
                    throw new ArgumentException("ID must be 6 digits");
                _id = id;
                FirstName = first;
                LastName = last;
            }
        }

        // Konto-ejer
        private class AccountHolder(string firstName, string lastName, int id) : Person(firstName, lastName, id) { }

        // Konto-administrator
        private class AccountAdministrator(string firstName, string lastName, int id) : Person(firstName, lastName, id) { }

        public static void ErrorLog(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void HappyLog(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
