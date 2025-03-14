using System.Media;
using static TEC_OOP_Opgaver.Tools.ConsoleHelper;

namespace TEC_OOP_Opgaver.OOP_Dag2.AnimalSounds
{
    internal class AnimalSounds
    {
        public AnimalSounds()
        {
            AnimalFactory af = new AnimalFactory();

            Animal myDog = af.CreateAnimal(enmAnimals.Dog, "Lassie");
            Animal myCat = af.CreateAnimal(enmAnimals.Cat, "Jim");
            Animal mySheep = af.CreateAnimal(enmAnimals.Sheep, "Pedro");

            myDog.MakeSound();
            myCat.MakeSound();
            mySheep.MakeSound();

        }
        private class AnimalFactory
        {
            public Animal CreateAnimal(enmAnimals animalType, string name)
            {
                switch (animalType)
                {
                    case enmAnimals.Dog:
                        return new Dog(name);
                    case enmAnimals.Cat:
                        return new Cat(name);
                    case enmAnimals.Sheep:
                        return new Sheep(name);
                    default:
                        return null;
                }
            }
        }

        private abstract class Animal   // Abstract parent class
        {
            public abstract void MakeSound();
            public abstract string Name { get; set; }
            public Animal(string name) { Name = name; }
        }

        private class Dog(string name) : Animal(name)
        {
            public override string Name { get; set; }
            public override void MakeSound()
            {
                PrintCol($"{Name}: Woof woof", ConsoleColor.Red);
                PlayAnimalSound("dog");
            }
        }

        private class Cat(string name) : Animal(name)
        {
            public override string Name { get; set; }
            public override void MakeSound()
            {
                PrintCol($"{Name}: Miaw", ConsoleColor.Blue);
                PlayAnimalSound("cat");
            }
        }

        private class Sheep(string name) : Animal(name)
        {
            public override string Name { get; set; }
            public override void MakeSound()
            {
                PrintCol($"{Name}: Baah", ConsoleColor.Magenta);
                PlayAnimalSound("sheep");
            }
        }

        public static void PlayAnimalSound(string soundName) 
        {
            // Bruger LocalDirectory for at få stien til lydmappen
            string soundPath = PathHelper.LocalDirectory("\\Files\\Sounds");
            string fullSoundFile = Path.Combine(soundPath, $"{soundName}.wav"); // Path.Combine sørger for korrekt sammensætning a de to paths med seperator, så vi ike får: Soundsdog.wav eller lign.

            if (!File.Exists(fullSoundFile))
            {
                Console.WriteLine($"Fejl: '{fullSoundFile}' blev ikke fundet.");
                return;
            }

            try
            {
                SoundPlayer player = new SoundPlayer(fullSoundFile);
                player.PlaySync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved afspilning: {ex.Message}");
            }
        }

        enum enmAnimals
        {
            Dog,
            Cat,
            Sheep
        }
    }
}
