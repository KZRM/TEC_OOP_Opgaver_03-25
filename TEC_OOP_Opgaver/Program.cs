using Microsoft.Extensions.DependencyInjection;
using MyCollectionExamples;
using TEC_OOP_Opgaver.FileHandlerApp;
using TEC_OOP_Opgaver.OOP_Dag1.TypeCountingApp;
using TEC_OOP_Opgaver.OOP_Dag2.AnimalSounds;
using TEC_OOP_Opgaver.OOP_Dag2.Bank;
using TEC_OOP_Opgaver.OOP_Dag2.VehicleInspection;
using static TEC_OOP_Opgaver.Tools.ConsoleHelper;

internal class Program
{
    // Make serviceProvider a static field so it’s accessible everywhere
    public static readonly IServiceProvider serviceProvider;

    static Program()
    {
        // Configure services in a static constructor
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<WordCounter>();
        serviceCollection.AddTransient<WordCountingApp>();
        serviceCollection.AddTransient<BasicBank>();
        serviceCollection.AddTransient<VehicleInspection>();
        serviceCollection.AddTransient<AnimalSounds>();
        serviceCollection.AddTransient<PersonCollection>();
        serviceCollection.AddTransient<BankWExeptionHandlers>();
        serviceCollection.AddTransient<FileHandler>();
        serviceProvider = serviceCollection.BuildServiceProvider();
    }

    private static void Main(string[] args)
    {
        bool programActive = true;
        while (programActive)
        {
            PrintCol("Choose your project, press a number:\n", ConsoleColor.Yellow);
            PrintProjectName(1, "Word counting app", 1);
            PrintProjectName(2, "Basic Bank", 2);
            PrintProjectName(3, "Vehicle inspection", 2);
            PrintProjectName(4, "Animal sounds", 2);
            PrintProjectName(5, "Person types", 3);
            PrintProjectName(6, "Bank Exception handling", 3);
            PrintProjectName(7, "FileHandler Except-handling", 3);
            Line();

            ConsoleKey keyInput = Console.ReadKey().Key;
            Console.Clear();
            switch (keyInput)
            {
                case ConsoleKey.D1:
                    //new WordCountingApp();            /// Bør jeg bruge min Service collecion istedet?
                    serviceProvider.GetService<WordCountingApp>(); break;

                case ConsoleKey.D2:
                    serviceProvider.GetService<BasicBank>(); break;

                case ConsoleKey.D3:
                    serviceProvider.GetService<VehicleInspection>(); break;

                case ConsoleKey.D4:
                    serviceProvider.GetService<AnimalSounds>(); break;

                case ConsoleKey.D5:
                    serviceProvider.GetService<PersonCollection>(); break;

                case ConsoleKey.D6:
                    serviceProvider.GetService<BankWExeptionHandlers>(); break;

                case ConsoleKey.D7:
                    serviceProvider.GetService<FileHandler>(); break;

                case ConsoleKey.Escape: programActive = false; break;

                default: break;
            }
            Console.Clear();
        }
    }

    private static void PrintProjectName(int index, string projectName, int day)
    {
        Line();
        PrintCol($"{index}", ConsoleColor.Yellow, false);
        PrintCol($" - {projectName}", ConsoleColor.White, false);
        Console.CursorLeft = 40;
        PrintCol($"(D.{day})", ConsoleColor.DarkGray);
    }

    private static void Line()
    {
        PrintCol(new string('-', 46), ConsoleColor.DarkGray);
    }
}