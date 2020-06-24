using System;
using System.Collections.Generic;

namespace GoFlow.InventoryPickingLocations
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Find the right pickup locations. \nNote: If you enter a letter the value will be 0.");
            PickupLocations();
        }

        public static void PickupLocations()
        {

            var id = 1;
            var addNewLocation = true;
            var locations = new List<Location>();

            Console.WriteLine("Enter the quantity to pick: ");
            int.TryParse(Console.ReadLine(), out int quantityToPick);

            Console.WriteLine("Note: To stop inserting locations use: x ");

            while (addNewLocation)
            {
                var input = Console.ReadLine();
                int.TryParse(input, out int locationQuantity);
                Console.WriteLine("Enter the location quantity: ");
                locations.Add(new Location { Id = id++, QuantityAvailable = locationQuantity });
                if (input.ToLower() == "x")
                    break;
            }

            PickingLocations pickingLocations = new PickingLocations(locations);
            var picks = pickingLocations.Calculate(quantityToPick);

            foreach (var pick in picks)
            {
                Console.WriteLine($"\nLocation id: {pick.LocationId} quantity: {pick.Quantity}");
            }
        }
    }
}