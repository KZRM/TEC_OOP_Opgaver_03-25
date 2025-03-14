using System;
using static TEC_OOP_Opgaver.Tools.ConsoleHelper;
namespace MyCollectionExamples
{

    internal partial class PersonCollection
    {
        public PersonCollection()
        {
            // Lav fire Lists...
            List<Person_imutable> pImutables = new List<Person_imutable>();
            List<Person_mutable> pMutables = new List<Person_mutable>();

            // |||||||||||||||||||||||| Demonstration af Mutability: ||||||||||||||||||||||||

            // IMUTABLE - Kan IKKE ændre navn, da det er imutable...
            pImutables.Add(new Person_imutable("Peter", "Petersen"));
            // pImutables[0].FirstName = "Lars";

            // MUTABLE - Kan GODT ændre navn, da det er mutable:
            pMutables.Add(new Person_mutable("Hanne", "Hannesen"));
            pMutables[0].FirstName = "Rikke";



            // |||||||||||||||||||||||| Demonstration af IEquatable: ||||||||||||||||||||||||
            PrintCol("Sammenligning af Personer: ");
            
            List<Person_Equatable> pEquatables = new List<Person_Equatable>{ 
                new Person_Equatable("Eva", "Nielsen"), 
                new Person_Equatable("eva", "nielsen"), 
                new Person_Equatable("Eva", "Jensen") };

            // Forventet: Equal, da data er ens
            PrintCol(
                pEquatables[0].Equals(pEquatables[1]) ?
                $"{pEquatables[0].FullName} og {pEquatables[1]} er Equal" :
                $"{pEquatables[0].FullName} og {pEquatables[1]} er IKKE Equal");

            PrintCol(
                pEquatables[0].Equals(pEquatables[2]) ?
                $"{pEquatables[0].FullName} og {pEquatables[2].FullName} er Equal" :
                $"{pEquatables[0].FullName} og {pEquatables[2].FullName} er IKKE Equal");



            // |||||||||||||||||||||||| Demonstration af IComparable: ||||||||||||||||||||||||
            Console.WriteLine("\n\nSortering med IComparable:\n");

            var persons = new List<Person_Comparable>
            {
                new Person_Comparable("Flemming", "Østergaard"),    new Person_Comparable("Gitte", "Andersen"),
                new Person_Comparable("Henrik", "Berg"),            new Person_Comparable("Anna", "Hansen")
            };

            // Sorterer listen efter SecondName (efternavn), baseret på logikken i metoden "CompareTo" som "Sort" benytter sig af.
            PrintCol("Liste pre-sort:\n");
            foreach (var p in persons)
            { PrintCol(p.FullName); }

            PrintCol("\nListe post-sort (baseret på efternavn):\n");
            persons.Sort();

            foreach (var p in persons)
            { PrintCol(p.FullName); }

            Console.ReadLine();



        }

    }

}
