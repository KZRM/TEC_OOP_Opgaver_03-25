using static TEC_OOP_Opgaver.Tools.ConsoleHelper; // Namespace for PathHelper

// Custom exception klasser
namespace TEC_OOP_Opgaver.FileHandlerApp
{
    // Custom exception for navne
    public class InvalidNameException : Exception
    { public InvalidNameException(string message) : base(message) { } }

    // Custom exception for alder
    public class InvalidAgeException : Exception
    { public InvalidAgeException(string message) : base(message) { } }

    // Custom exception for email med inner exception
    public class InvalidEmailException : Exception
    { public InvalidEmailException(string message, Exception innerException) : base(message, innerException) { } }

    public class UserDataDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }

    internal class FileHandler
    {
        private static readonly string filePath = PathHelper.LocalDirectory("Files/Users.txt");
        public FileHandler()
        { RunUserRegistration(); }

        private void RunUserRegistration()  /// Det primære program...
        {
            string directory = Path.GetDirectoryName(filePath);

            try
            {
                PrintCol($"Gemmer fil i: {filePath}", ConsoleColor.Gray);
                Directory.CreateDirectory(directory);
                PrintCol($"Mappe oprettet eller findes allerede: {directory}", ConsoleColor.Gray);
            }

            catch (Exception ex)    // Hvis ikke den kan lave eller finde fil...
            {
                PrintCol($"Fejl ved oprettelse af mappe: {ex.Message}", ConsoleColor.Red);
                return;
            }

            while (true)
            {
                UserDataDTO user = new UserDataDTO();

                user.FirstName = GetValidInput("Fornavn");
                if (user.FirstName == null) break;
                user.LastName = GetValidInput("Efternavn");
                user.Age = GetValidAge(user);
                user.Email = GetValidInput("Email");

                SaveUser(user);

                // Spørg om at tilføje en ny bruger
                AskAddNewUser();
            }
        }

        private static void AskAddNewUser()
        {
            while (true)
            {
                PrintCol("\nWant to add another user? [Y] - Yes [N] - No: ", ConsoleColor.Cyan);
                var key = Console.ReadKey(true).Key; // true betyder, at tasten ikke vises i konsollen
                Console.WriteLine(); // Ny linje efter input

                if (key == ConsoleKey.Y)
                    break;  // Bryd ud af den indre løkke og fortsæt ydre løkke (ny bruger)

                else if (key == ConsoleKey.N)
                { PrintCol("Programmet afsluttes...", ConsoleColor.Green); return; } // Afslut hele metoden og dermed programmet

                else
                    PrintCol("Fejl: Tryk kun på Y eller N!", ConsoleColor.Red);
            }
        }

        private string GetValidInput(string inputType)
        {
            while (true)
            {
                try
                {
                    PrintCol($"{inputType}: ", ConsoleColor.Cyan, false);
                    PrintCol(inputType == "Fornavn" ? "(Skriv exit for at lukke)" : "", ConsoleColor.DarkGray);

                    string input = Console.ReadLine(); // Modtag input...

                    if (input.ToLower() == "exit" && inputType == "Fornavn")
                        return null;

                    if (string.IsNullOrWhiteSpace(input))
                        throw new InvalidNameException("Feltet må ikke være tomt!");

                    if (inputType == "Fornavn" || inputType == "Efternavn")
                    {
                        if (input.Length < 2)
                            throw new InvalidNameException("Navnet skal være mindst 2 tegn langt!");
                    }
                    else if (inputType == "Email")
                        if (!input.Contains("@") || !input.Contains("."))
                        {
                            throw new InvalidEmailException("Email skal indeholde '@' og '.'!",
                                new FormatException("Ugyldigt email-format"));
                        }

                    return input;
                }
                catch (InvalidNameException ex) when (inputType == "Fornavn" || inputType == "Efternavn")
                {
                    PrintCol($"Fejl: {ex.Message}", ConsoleColor.Red);
                    PrintCol("Prøv igen...", ConsoleColor.Yellow);
                }
                catch (InvalidEmailException ex)
                {
                    PrintCol($"Fejl: {ex.Message} (Detaljer: {ex.InnerException.Message})", ConsoleColor.Red);
                    PrintCol("Prøv igen...", ConsoleColor.Yellow);
                }
                catch (Exception ex)
                {
                    PrintCol($"Uventet fejl: {ex.Message}", ConsoleColor.Red);
                    PrintCol("Prøv igen...", ConsoleColor.Yellow);
                }
            }
        }

        private int GetValidAge(UserDataDTO user)
        {
            while (true)
            {
                PrintCol("Alder: ", ConsoleColor.Cyan);
                string ageInput = Console.ReadLine(); // Deklarer ageInput uden for try

                try
                {
                    if (!int.TryParse(ageInput, out int age))
                        throw new InvalidAgeException("Alder skal være et tal!");

                    if (age < 18 || age > 50)
                        throw new InvalidAgeException("Alder skal være mellem 18 og 50!");

                    return age;
                }
                catch (InvalidAgeException ex) when (user.FirstName + " " + user.LastName != "Niels Olesen")
                {
                    PrintCol($"Fejl: {ex.Message}", ConsoleColor.Red);
                    PrintCol("Prøv igen...", ConsoleColor.Yellow);
                }
                catch (InvalidAgeException ex) when (user.FirstName + " " + user.LastName == "Niels Olesen")
                {
                    PrintCol("Niels Olesen får lov til at have en mærkelig alder!", ConsoleColor.Green);
                    return int.Parse(ageInput); // Brug det oprindelige input
                }
                catch (Exception ex)
                {
                    PrintCol($"Uventet fejl: {ex.Message}", ConsoleColor.Red);
                    PrintCol("Prøv igen...", ConsoleColor.Yellow);
                }
            }
        }

        private void SaveUser(UserDataDTO user)
        {
            try
            {
                string userLine = $"{user.FirstName},{user.LastName},{user.Age},{user.Email}";
                File.AppendAllText(filePath, userLine + Environment.NewLine);
                PrintCol("Bruger gemt!", ConsoleColor.Green);
                ShowUsers();
            }
            catch (IOException ex)
            {
                PrintCol($"Fejl ved skrivning til fil: {ex.Message}", ConsoleColor.Red);
            }
            catch (Exception ex)
            {
                PrintCol($"Uventet fejl ved gemning: {ex.Message}", ConsoleColor.Red);
            }
        }

        private void ShowUsers()
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new FileLoadException("Filen 'Users.txt' blev ikke fundet!", filePath);


                string[] lines = File.ReadAllLines(filePath);
                PrintCol("\nRegistrerede brugere:", ConsoleColor.Cyan);
                foreach (string line in lines)
                    PrintCol(line, ConsoleColor.White);
            }
            catch (FileLoadException ex)
            { PrintCol($"Fejl: {ex.Message}", ConsoleColor.Red); }
            catch (IOException ex)
            { PrintCol($"Fejl ved læsning af fil: {ex.Message}", ConsoleColor.Red); }
            catch (Exception ex)
            { PrintCol($"Uventet fejl ved læsning: {ex.Message}", ConsoleColor.Red); }
        }
    }
}