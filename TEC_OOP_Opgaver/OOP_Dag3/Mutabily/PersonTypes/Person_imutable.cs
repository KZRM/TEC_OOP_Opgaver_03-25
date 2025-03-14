namespace MyCollectionExamples
{

    internal partial class PersonCollection
    {
        /// Imutable kan IKKE have sit navn ændret EFTER instantiering
        public class Person_imutable
        {
            public string FirstName { get; }    // props er uden setter
            public string SecondName { get; }
            public string FullName => $"{FirstName} {SecondName}";
            public Person_imutable(string firstName, string secondName)
            { FirstName = firstName; SecondName = secondName; }
        }

    }

}
