namespace MyCollectionExamples
{

    internal partial class PersonCollection
    {
        /// Mutable kan have sit navn ændret EFTER instantiering
        public class Person_mutable
        {
            public string FirstName { get; set; }
            public string SecondName { get; set; }
            public string FullName => ($"{FirstName} {SecondName}");
            public Person_mutable(string firstName, string secondName)
            { FirstName = firstName; SecondName = secondName; }
        }

    }

}
