using static TEC_OOP_Opgaver.Tools.ConsoleHelper;

namespace TEC_OOP_Opgaver.OOP_Dag2.Bank
{
    internal class BasicBank
    {
        public BasicBank()
        {

            BankAccount account = new BankAccount(
                new AccountHolder("Peter", "Petersen", 123456),
                new AccountAdministrator("Lisa", "Madsen", 654321),
                1000);

            account.Deposit(400);
            account.Withdraw(50);

            /// Vil ikke fungere:
            account.Withdraw(3000);
            account.Deposit(-50);

            account.ShowBalance();

            Console.ReadKey();
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
                    PrintCol($"Deposited {amount}. Now you have {_balance}", ConsoleColor.Green);
                }
                else { PrintCol("Deposit amount cant be less than zero", ConsoleColor.Red); }
            }

            public void Withdraw(decimal amount)
            {
                if (amount > 0 && amount <= _balance)
                {
                    _balance -= amount;
                    PrintCol($"Withdrew {amount}. Now you have {_balance}", ConsoleColor.Green);
                }
                else  { PrintCol("Withdraw amount cant be less than zero, or over account balance", ConsoleColor.Red);}
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
            public virtual string FullName => FirstName + " " + LastName;       

            public int ID => _id;

            public Person(string first, string last, int id)
            {
                // Sørger for længden er 6!
                if (id.ToString().Length != 6) 
                    throw new ArgumentException("ID must be 6 digits");
                
                _id = id; FirstName = first; LastName = last;
            }
        }

        // Konto-ejer
        private class AccountHolder(string firstName, string lastName, int id) : Person(firstName, lastName, id) { }

        // Konto-administrator
        private class AccountAdministrator(string firstName, string lastName, int id) : Person(firstName, lastName, id) { }
    }
}
