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
        serviceProvider = serviceCollection.BuildServiceProvider();
    }

    private static void Main(string[] args)
    {
        bool programActive = true;
        while (programActive)
        {
            PrintCol(
                "Choose your project:\n" +
                "1  -  Word counting app            (D.1)\n" +
                "2  -  Basic Bank                   (D.2)\n" +
                "3  -  Vehicle inspection           (D.2)\n" +
                "4  -  Animal sounds                (D.2)\n" +
                "5  -  Person types                 (D.3)\n" +
                "6  -  Bank Exception handling      (D.3)\n" +
                "7  -  FileHandler Except-handling  (D.3)\n", ConsoleColor.Yellow);

            ConsoleKey keyInput = Console.ReadKey().Key;
            Console.Clear();
            switch (keyInput)
            {
                case ConsoleKey.D1:
                    new WordCountingApp(); break;           /// Bør jeg bruge min Service collecion istedet?

                case ConsoleKey.D2:
                    new BasicBank(); break;

                case ConsoleKey.D3:
                    new VehicleInspection(); break;

                case ConsoleKey.D4:
                    new AnimalSounds(); break;

                case ConsoleKey.D5:
                    new PersonCollection(); break;

                case ConsoleKey.D6:
                    new BankWExeptionHandlers(); break;

                case ConsoleKey.D7:
                    new FileHandler(); break;

                case ConsoleKey.Escape: programActive = false; break;

                default: break;
            }
            Console.Clear();
        }
    }
}