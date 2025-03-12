namespace TEC_OOP_Opgaver.VehicleInspection
{
    internal class VehicleInspection
    {
        public VehicleInspection()
        {
            // My instanced Car
            Car myCar = new Car(
                "Toyota",
                "Something",
                new DateTime(2020, 5, 11),  // LastInspection
                new DateTime(2008, 12, 1)  // ProductionDate
            );
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(myCar.DisplayInfo());
            myCar.InspectionStatus();

            // My instanced Truck
            Truck myTruck = new Truck(
                "Volvo",
                "BigTruck",
                new DateTime(2025, 03, 1), // LastInspection
                new DateTime(2025, 03, 1)  // ProductionDate
            );
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(myTruck.DisplayInfo());
            myTruck.InspectionStatus();

            Console.ResetColor();
        }

        // ABSTRACT BASE CLASS
        private abstract class Vehicle
        {
            public Vehicle(string brand, string model, DateTime lastInspection, DateTime productionDate)
            {
                Brand = brand;
                Model = model;
                LastInspection = lastInspection;
                ProductionDate = productionDate;
            }

            public abstract string Brand { get; set; }
            public abstract string Model { get; set; }
            public abstract DateTime LastInspection { get; set; }
            public abstract DateTime ProductionDate { get; set; }

            // Abstract method (must be overridden in derived classes)
            public abstract void InspectionStatus();

            // Virtual method (has a default implementation in the base, can be overridden)
            public virtual string DisplayInfo()
            {
                return $"Brand: {Brand}, Model: {Model}, Production Date: {ProductionDate.ToShortDateString()}";
            }
        }

        // DERIVED CLASS: CAR
        private class Car(string brand, string model, DateTime lastInspection, DateTime productionDate) : Vehicle(brand, model, lastInspection, productionDate)
        {
            public override string Brand { get; set; }
            public override string Model { get; set; }
            public override DateTime LastInspection { get; set; }
            public override DateTime ProductionDate { get; set; }

            // Overriding the abstract method
            public override void InspectionStatus()
            {
                bool needsInspection =
                    DateIsOlderThan(ProductionDate, 4) &&
                    DateIsOlderThan(LastInspection, 2);

                Print_ShouldBeInspected(needsInspection);
            }

            // Overriding the virtual method
            public override string DisplayInfo() { return $"Car: {Brand} {Model}"; }
        }

        // DERIVED CLASS: TRUCK
        private class Truck(string brand, string model, DateTime lastInspection, DateTime productionDate) : Vehicle(brand, model, lastInspection, productionDate)
        {
            public override string Brand { get; set; }
            public override string Model { get; set; }
            public override DateTime LastInspection { get; set; }
            public override DateTime ProductionDate { get; set; }

            // Overriding the abstract method
            public override void InspectionStatus()
            {
                bool needsInspection =
                    DateIsOlderThan(ProductionDate, 1) &&
                    DateIsOlderThan(LastInspection, 1);

                Print_ShouldBeInspected(needsInspection);
            }

            // Overriding my virtual method
            public override string DisplayInfo()
            {
                return $"Truck: {Brand} {Model}";
            }
        }

        // MINE HELPER METHODS
        public static bool DateIsOlderThan(DateTime dateTime, int years)
        {
            // True if dateTime + X years is still in the past
            return dateTime.AddYears(years) < DateTime.Now;
        }

        public static void Print_ShouldBeInspected(bool shouldBeInspected)
        {
            Console.WriteLine(
                shouldBeInspected
                    ? "Needs inspection\n"
                    : "Does not need inspection\n"
            );
        }
    }
}
