namespace MyCollectionExamples
{

    internal partial class PersonCollection
    {
        /// DTO-klasse med IEquatable
        public class Person_Equatable : IEquatable<Person_Equatable>
        {
            public string FirstName { get; set; }
            public string SecondName { get; set; }
            public string FullName => $"{FirstName} {SecondName}";

            public Person_Equatable(string firstName, string secondName)
            { FirstName = firstName; SecondName = secondName; }

            // Krævet: Sammenlign baseret på data (case-insensitive)
            public bool Equals(Person_Equatable other)
            {
                if (other == null)
                    return false;
                return FirstName.Equals(other.FirstName, StringComparison.OrdinalIgnoreCase) &&
                       SecondName.Equals(other.SecondName, StringComparison.OrdinalIgnoreCase);
            }
        }

    }

}
