using System.Collections.Generic;
using System.Linq;

namespace GoFlow.InventoryPickingLocations
{
    public class Location
    {
        public int Id { get; set; }
        public int QuantityAvailable { get; set; }
    }

    public class InventoryToPick
    {
        public int LocationId { get; set; }
        public int Quantity { get; set; }

        public InventoryToPick(int locationId, int quantity)
        {
            LocationId = locationId;
            Quantity = quantity;
        }
    }

    public class PickingLocations
    {
        private List<Location> locations;
        private int defaultUnitOfMeasure;

        public PickingLocations(List<Location> locations, int defaultUnitOfMeasure = 1)
        {
            this.locations = locations;
            this.defaultUnitOfMeasure = defaultUnitOfMeasure;
        }

        public IEnumerable<InventoryToPick> Calculate(int quantityToPick)
        {
            // Implement the logic here
            var startIndex = 0;
            var locationsCount = locations.Count;
            var pickupLocations = new Dictionary<int, InventoryToPick>();
            var boxQuantityToPick = quantityToPick / defaultUnitOfMeasure;

            if (!locations.Any())
                return new List<InventoryToPick>();

            locations = locations.OrderByDescending(x => x.QuantityAvailable).ToList();
            var locationSelected = locations[0];

            for (int i = 1; i < locationsCount; i++)
            {
                if (locations[i].QuantityAvailable >= quantityToPick)
                {
                    if (locations[i].QuantityAvailable == quantityToPick)
                    {
                        pickupLocations[startIndex] = new InventoryToPick(locations[i].Id, quantityToPick);
                        return pickupLocations.Values;
                    }

                    else if (locations[i].QuantityAvailable < locationSelected.QuantityAvailable)
                    {
                        pickupLocations[startIndex] = new InventoryToPick(locations[i].Id, quantityToPick);
                        locationSelected = locations[i];
                    }
                }
            }

            if (pickupLocations.Count == 0)
                return FindBestPickUpLocations(locations, quantityToPick, boxQuantityToPick);

            return pickupLocations.Values;
        }

        public List<InventoryToPick> FindBestPickUpLocations(List<Location> locations, int quantityToPick, double boxQuantityToPick)
        {
            var bestPickUpLocations = new List<InventoryToPick>();

            if (locations[0].QuantityAvailable > quantityToPick)
            {
                bestPickUpLocations.Add(new InventoryToPick(locations[0].Id, quantityToPick));
                return bestPickUpLocations;
            }

            bestPickUpLocations.Add(new InventoryToPick(locations[0].Id, locations[0].QuantityAvailable));
            quantityToPick -= locations[0].QuantityAvailable;
            boxQuantityToPick -= locations[0].QuantityAvailable / defaultUnitOfMeasure;
            locations.RemoveAt(0);

            foreach (var location in locations)
            {
                int boxQuantityInLocation = location.QuantityAvailable / defaultUnitOfMeasure;

                if (boxQuantityInLocation == boxQuantityToPick)
                {
                    int i;
                    for (i = 0; i < boxQuantityInLocation;)
                    {
                        i++;
                        boxQuantityToPick--;
                        quantityToPick -= defaultUnitOfMeasure;

                        if (boxQuantityToPick == 0)
                            break;
                    }
                    var quantityToPickFromLocation = i * defaultUnitOfMeasure;
                    bestPickUpLocations.Add(new InventoryToPick(location.Id, quantityToPickFromLocation));
                    location.QuantityAvailable -= quantityToPickFromLocation;
                }

                if (boxQuantityToPick == 0)
                    break;
            }

            locations.RemoveAll(x => x.QuantityAvailable == 0);
            locations = locations.OrderBy(x => x.QuantityAvailable).ToList();

            foreach (var location in locations)
            {
                if (quantityToPick < 1)
                    break;

                if (location.QuantityAvailable > quantityToPick)
                {
                    bestPickUpLocations.Add(new InventoryToPick(location.Id, quantityToPick));
                    quantityToPick -= location.QuantityAvailable;
                }
            }

            return bestPickUpLocations;
        }
    }
}
