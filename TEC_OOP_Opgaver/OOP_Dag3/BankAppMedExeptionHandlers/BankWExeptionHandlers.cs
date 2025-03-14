using static TEC_OOP_Opgaver.Tools.ConsoleHelper;
namespace TEC_OOP_Opgaver.OOP_Dag2.Bank
{
    internal class BankWExeptionHandlers
    {
        public BankWExeptionHandlers()
        {
            // Opret en konto med startværdier
            BankAccount account = new BankAccount(
                new AccountHolder("Peter", "Petersen", 123456),
                new AccountAdministrator("Lisa", "Lisasen", 654321),
                1000);

            // Start bank-sessionen hvor brugeren kan gøre ting
            BankInterface(account);
        }

        // Metode til at køre en interaktiv bank-session
        private void BankInterface(BankAccount account)
        {
            while (true)
            {
                Console.WriteLine("\nHvad vil du gøre? (1: Indsæt, 2: Hæv, 3: Vis saldo, 4: Afslut)");
                string choice = Console.ReadLine();

                if (choice == "4") break; // Hop ud af løkken og afslut

                // Håndter valget med det samme
                if (choice == "1")
                {
                    decimal amount = GetValidAmount("Indtast beløb til indsætning: ");
                    if (amount != decimal.MinValue) // Hvis vi fik et gyldigt beløb
                    {
                        try
                        { account.Deposit(amount); } // Kan smide ArgumentException hvis negativt

                        catch (ArgumentException ex)
                        { ErrorLog(ex.Message); }
                    }
                }
                else if (choice == "2")
                {
                    decimal amount = GetValidAmount("Indtast beløb til hævning: ");
                    if (amount != decimal.MinValue) // Hvis vi fik et gyldigt beløb
                    {
                        try
                        { account.Withdraw(amount); } // Kan smide InvalidOperationException eller ArgumentException
                        catch (ArgumentException ex)
                        { ErrorLog(ex.Message); }
                        catch (InvalidOperationException ex)
                        { ErrorLog(ex.Message); }
                    }
                }
                else if (choice == "3")
                {
                    account.ShowBalance();
                }
                else
                {
                    ErrorLog("Forkert valg, prøv igen!");
                }

                // Vis saldoen efter hver handling (som en slags finally)
                account.ShowBalance();
            }
        }

        // Helper metode; sørger for at få et gyldigt beløb fra brugeren:
        private decimal GetValidAmount(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            try
            {
                decimal amount = decimal.Parse(input); // Kan smide FormatException hvis det ikke er et tal
                return amount;
            }
            catch (FormatException)
            {
                ErrorLog("Du skal skrive et rigtigt tal, ikke noget pjat!");
                return decimal.MinValue; // Bruger dette som en "fejlværdi" for at vise, det gik galt
            }
            catch (Exception ex)
            {
                ErrorLog($"Noget gik galt: {ex.Message}");
                return decimal.MinValue;
            }
        }

        // Privat klasse til bankkontoen
        private class BankAccount
        {
            private decimal _balance;
            private AccountHolder _holder;
            private AccountAdministrator _admin;

            public BankAccount(AccountHolder holder, AccountAdministrator admin, decimal startBalance)
            {
                // Hvis startbeløbet er under 0, sætter vi det bare til 0
                _balance = startBalance < 0 ? 0 : startBalance;
                _holder = holder;
                _admin = admin;
                Console.WriteLine($"Hej {_holder.FullName}. Din konto er oprettet med {_admin.FullName} som admin.");
            }

            public void Deposit(decimal amount)
            {
                if (amount <= 0)
                    throw new ArgumentException("Du kan ikke indsætte 0 eller et negativt beløb!");
                _balance += amount;
                HappyLog($"Indsat {amount}. Nu har du {_balance}");
            }

            public void Withdraw(decimal amount)
            {
                if (amount <= 0)
                    throw new ArgumentException("Du kan ikke hæve 0 eller et negativt beløb!");
                if (amount > _balance)
                    throw new InvalidOperationException("Du har ikke nok penge til at hæve så meget!");

                _balance -= amount;
                HappyLog($"Hævet {amount}. Nu har du {_balance}");
            }

            public void ShowBalance()
            {
                PrintCol("\nDin nuværende saldo er:" +
                    $"{_holder.FullName}'s kontobalance: {_balance}" +
                    $"(Admin: {_admin.FullName})");
            }
        }

        private class Person // Baseklasse til personer
        {
            private int _id;
            public virtual string FirstName { get; set; }
            public virtual string LastName { get; set; }
            public virtual string FullName => FirstName + " " + LastName;
            public int ID => _id;
            public Person(string first, string last, int id)
            {
                if (id.ToString().Length != 6) throw new ArgumentException("ID skal være 6 cifre!");
                _id = id;
                FirstName = first;
                LastName = last;
            }
        }

        // Konto-ejer og admin klasser
        private class AccountHolder(string firstName, string lastName, int id) : Person(firstName, lastName, id) { }
        private class AccountAdministrator(string firstName, string lastName, int id) : Person(firstName, lastName, id) { }

        // Hjælpemetoder til at logge med farver
        public static void ErrorLog(string text)
        { PrintCol(text, ConsoleColor.Red); }

        public static void HappyLog(string text)
        { PrintCol(text, ConsoleColor.Green); }
    }
}