using System;
using System.Collections.Generic;
using System.Linq;

namespace GoFlow.InventoryPickingLocations
{
    class Location
    {
        public int Id { get; set; }
        public int QuantityAvailable { get; set; }
    }

    class InventoryToPick
    {
        public int LocationId { get; set; }
        public int Quantity { get; set; }
    }

    class PickingLocations
    {
        private List<Location> locations;
        private int defaultUnitOfMeasure;

        public PickingLocations(List<Location> locations, int defaultUnitOfMeasure = 1)
        {
            this.locations = locations;
            this.defaultUnitOfMeasure = defaultUnitOfMeasure;
        }

        public List<InventoryToPick> Calculate(int quantityToPick)
        {
            // Implement the logic here
            var startIndex = 0;
            var totalLocations = locations.Count;
            var pickupLocations = new List<Location>() { new Location { Id = 0, QuantityAvailable = 0 } };

            if (!locations.Any())
                return new List<InventoryToPick>();

            for (int i = 0; i < totalLocations; i++)
            {
                if ((locations[i].QuantityAvailable >= quantityToPick))
                {
                    if (locations[i].QuantityAvailable == quantityToPick)
                    {
                        pickupLocations[startIndex] = locations[i];
                        return MapLocationToInventoryToPick(pickupLocations);
                    }

                    else if ((locations[i].QuantityAvailable < pickupLocations[startIndex].QuantityAvailable))
                    {
                        pickupLocations[startIndex] = locations[i];
                        continue;
                    }

                    else if (pickupLocations[startIndex].QuantityAvailable < quantityToPick)
                    {
                        pickupLocations[startIndex] = locations[i];
                        continue;
                    }
                }
                else if ((locations[i].QuantityAvailable < quantityToPick) && pickupLocations[startIndex].QuantityAvailable < quantityToPick)
                {
                    var bestPickUpLocations = FindBestPickUpLocations(locations, i, totalLocations, quantityToPick);

                    if (bestPickUpLocations.Any())
                        return MapLocationToInventoryToPick(bestPickUpLocations);
                }
            }
            return MapLocationToInventoryToPick(pickupLocations);
        }

        public List<Location> FindBestPickUpLocations(List<Location> locations, int startIndex, int endIndex, int quantityToPick)
        {
            var currentQuantity = 0;
            var bestPickUpLocations = new List<Location>();
            for (int i = startIndex; i < endIndex; i++)
            {
                var locationsToTake = endIndex - startIndex;
                currentQuantity += locations[i].QuantityAvailable;

                if (currentQuantity == quantityToPick)
                    bestPickUpLocations = locations.GetRange(startIndex, locationsToTake);
            }
            return bestPickUpLocations;
        }

        public List<InventoryToPick> MapLocationToInventoryToPick(List<Location> pickupLocations)
        {
            var inventoryToPick = new List<InventoryToPick>();

            foreach (var pickupLocation in pickupLocations)
            {
                inventoryToPick.Add(new InventoryToPick
                {
                    LocationId = pickupLocation.Id,
                    Quantity = pickupLocation.QuantityAvailable
                });
            }

            return inventoryToPick;
        }
    }
}