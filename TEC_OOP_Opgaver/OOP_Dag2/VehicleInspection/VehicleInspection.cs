using static TEC_OOP_Opgaver.Tools.ConsoleHelper;
namespace TEC_OOP_Opgaver.OOP_Dag2.VehicleInspection
{
    internal class VehicleInspection
    {
        public VehicleInspection()
        {

            // My instanced Car
            Car myCar = new Car(
                "Toyota",
                "Something",
                new DateTime(2020, 5, 11),
                new DateTime(2008, 12, 1));

            myCar.DisplayInfo();
            myCar.InspectionStatus();


            // My instanced Truck
            Truck myTruck = new Truck(
                "Volvo",
                "BigTruck",
                new DateTime(2025, 03, 1),
                new DateTime(2025, 03, 1));

            myTruck.DisplayInfo();
            myTruck.InspectionStatus();

            Console.ReadKey();

        }

        // ABSTRACT BASE CLASS
        private abstract class Vehicle
        {
            public Vehicle(string brand, string model, DateTime lastInspection, DateTime productionDate)
            { Brand = brand; Model = model; LastInspection = lastInspection; ProductionDate = productionDate; }

            public abstract string Brand { get; set; }
            public abstract string Model { get; set; }
            public abstract DateTime LastInspection { get; set; }
            public abstract DateTime ProductionDate { get; set; }

            // Abstract Metode som SKAL overskrives i nedarvede klasser.
            public abstract void InspectionStatus();

            // Virtuel metode med grund logik, kan overskrives...
            public virtual void DisplayInfo()
            {PrintCol($"Brand: {Brand}, Model: {Model}, Production Date: {ProductionDate.ToShortDateString()}", ConsoleColor.Cyan);}
        }

        // NEDARVET KLASSE: "CAR"
        private class Car(string brand, string model, DateTime lastInspection, DateTime productionDate) : Vehicle(brand, model, lastInspection, productionDate)
        {
            public override string Brand { get; set; }
            public override string Model { get; set; }
            public override DateTime LastInspection { get; set; }
            public override DateTime ProductionDate { get; set; }

            // Overskrivning af abstrakt metode:
            public override void InspectionStatus()
            {
                bool needsInspection =
                    DateIsOlderThan(ProductionDate, 4) &&
                    DateIsOlderThan(LastInspection, 2);

                Print_ShouldBeInspected(needsInspection);
            }
            public virtual void DisplayInfo()
            { PrintCol($"[ CAR ] - Brand: {Brand}, Model: {Model}, Production Date: {ProductionDate.ToShortDateString()}", ConsoleColor.Cyan); }
        }

        // NEDARVET KLASSE: "TRUCK"
        private class Truck(string brand, string model, DateTime lastInspection, DateTime productionDate) : Vehicle(brand, model, lastInspection, productionDate)
        {
            public override string Brand { get; set; }
            public override string Model { get; set; }
            public override DateTime LastInspection { get; set; }
            public override DateTime ProductionDate { get; set; }

            public override void InspectionStatus()
            {
                bool needsInspection =
                    DateIsOlderThan(ProductionDate, 1) &&
                    DateIsOlderThan(LastInspection, 1);

                Print_ShouldBeInspected(needsInspection);
            }
            public virtual void DisplayInfo()
            { PrintCol($"[ TRUCK ] - Brand: {Brand}, Model: {Model}, Production Date: {ProductionDate.ToShortDateString()}", ConsoleColor.Cyan); }
        }

        // små helper metoder:
        public static bool DateIsOlderThan(DateTime dateTime, int years)
        { return dateTime.AddYears(years) < DateTime.Now; }

        public static void Print_ShouldBeInspected(bool shouldBeInspected)
        {
            if (shouldBeInspected)
                PrintCol("Needs inspection\n", ConsoleColor.Red);
            else
                PrintCol("Does not need inspection\n", ConsoleColor.Green);
        }
    }
}
