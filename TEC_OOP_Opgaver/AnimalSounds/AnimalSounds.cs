using System;
using System.Media;

namespace TEC_OOP_Opgaver.AnimalSounds
{
    internal class AnimalSounds
    {
        public AnimalSounds()
        {
            


            AnimalFactory animalFactory = new AnimalFactory();

            Animal myDog = animalFactory.CreateAnimal(enmAnimals.Dog, "Lassie");
            Animal myCat = animalFactory.CreateAnimal(enmAnimals.Cat, "Jim");
            Animal mySheep = animalFactory.CreateAnimal(enmAnimals.Sheep, "Pedro");

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

        private abstract class Animal
        {
            public abstract void MakeSound();
            public abstract string Name { get; set; }

            public Animal(string name)
            {
                Name = name;
            }
        }

        private class Dog(string name) : Animal(name)
        {
            public override string Name { get; set; }

            public override void MakeSound()
            {
                Console.WriteLine("Woof woof");
                PlayAnimalSound(@"C:\Users\kalle\Documents\SkoleOpgaver\TEC_OOP_Opgaver\TEC_OOP_Opgaver\AnimalSounds\Sounds\dog.wav");
            }
        }
        private class Cat(string name) : Animal(name)
        {
            public override string Name { get; set; }

            public override void MakeSound()
            {
                Console.WriteLine("Miaw");
                PlayAnimalSound(@"C:\Users\kalle\Documents\SkoleOpgaver\TEC_OOP_Opgaver\TEC_OOP_Opgaver\AnimalSounds\Sounds\cat.wav");
            }
        }
        private class Sheep(string name) : Animal(name)
        {
            public override string Name { get; set; }

            public override void MakeSound()
            {
                Console.WriteLine("Baah");
                PlayAnimalSound(@"C:\Users\kalle\Documents\SkoleOpgaver\TEC_OOP_Opgaver\TEC_OOP_Opgaver\AnimalSounds\Sounds\sheep.wav");
            }
        }

        public static void PlayAnimalSound(string soundPathLocation)
        {
            SoundPlayer player = new SoundPlayer(soundPathLocation); 
            player.PlaySync();
        }

        enum enmAnimals
        {
            Dog,
            Cat,
            Sheep
        }
    }
}
