namespace MyCollectionExamples
{

    internal partial class PersonCollection
    {
        /// DTO-klasse med IComparable for at kunne sortere efter SecondName (efternavn)
        public class Person_Comparable : IComparable<Person_Comparable>
        {
            public string FirstName { get; set; }
            public string SecondName { get; set; }
            public string FullName => $"{FirstName} {SecondName}";

            public Person_Comparable(string firstName, string secondName)
            { FirstName = firstName; SecondName = secondName; }

            // Sammenlign to Person_Comparable objekter baseret på SecondName (efternavn)
            public int CompareTo(Person_Comparable other)
            {
                if (other == null) return 1; // Hvis other er null, kommer "this" objekt først
                return string.Compare(this.SecondName, other.SecondName, StringComparison.OrdinalIgnoreCase);
            }
        }

    }

}
